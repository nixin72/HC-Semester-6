/**
Update: Guillaume Mercier
Date: 2018-May-05
Changes: Added CES update

always surround this entire script with a transaction rollback when debugging it:
begin transaction
	[code here]
rollback transaction
**/
--begin transaction

	-- find the csadmin student role
	declare @studentRoleId int;
	select top(1) @studentRoleId = IDRole from Applications.Role where Code = 'ST'


	/*

	declare @removedTeachers table(
	IDUser int not null
	)
	insert into @removedTeachers (idUser)
	select top 10 u.IDUser from Users.[User] u join Applications.UserRole ur on u.IDUser = ur.IDUser where IDRole = @studentRoleId

	delete from Applications.UserRole where IDUser in (select IDUser from @removedTeachers)
	delete from Users.StudentUser where IDUser in (select IDUser from @removedTeachers)
	delete from Users.[User] where IDUser in (select IDUser from @removedTeachers)
	
	*/

	-- Deactivate all old students
	update Users.[User] set
		IsActive = 0
	where
		IDUser in (
			select
				distinct IDUser
			from
				Applications.UserRole
			where
				IDRole = @studentRoleId
		);
	
	-- Deactivate all old student roles
	update Applications.UserRole set IsActive = 0 where idrole = @studentRoleId;

	-- A list of students that got added to the list
	declare @tempStudents table(
		action nvarchar(10),
		iduser int not null,
		idetudiant int not null
	)

	-- now get the students from clara and add them to the users in csadmin
	-- record the ones you get through the output clause so that we can add
	-- them to the other tables and set their roles
	merge into users.[user] old
		using (
			SELECT
				DISTINCT es.[IDEtudiant] as [IDEtudiant],
				e.numero7 as [username],
				e.Nom as [lastName],
				e.Prenom as [firstName],
				st.iduser as iduser
			FROM
				ClaraEtudiants.EtudiantSession es
					JOIN ClaraEtudiants.Etudiant e ON es.IDEtudiant = e.IDEtudiant
					full outer join [Users].[studentuser] st on e.IDEtudiant = st.IDEtudiant
			WHERE
				es.etat = 1 AND --This gets active students for this semester
				es.AnSession = (select top 1 [CurrentYearSemester] from [Applications].[Settings]) AND --current semester (must be changed in procedure)
				(
					es.IDUniteOrg = 235 OR -- REGULAR EDUCATION
					es.IDUniteOrg = 525 -- CONTINUED EDUCATION
				) --and e.idetudiant not in (select idetudiant from [users].[studentuser])
			) new
			on old.iduser = new.iduser
	when not matched then
		insert (username, lastname, firstname, isactive)
			values (new.username, new.lastname, new.firstname, 1)
	when matched then
		update set isactive = 1
	OUTPUT $action as [Action], inserted.iduser, new.idetudiant
	into @tempStudents (action, iduser, idetudiant);
	
	-- insert the new students to the Users.StudentUser table
	insert into [Users].[StudentUser] (IDUser,IDEtudiant)
		select iduser, idetudiant
		from @tempStudents
		where [Action] = 'INSERT';


	-- add the roles for the new students and reactivate the role for returning students
	merge into [applications].[userrole] old 
		using @tempStudents new
			on old.idrole = @studentRoleId and
				old.iduser = new.iduser
	when matched then
		update set isactive = 1
	when not matched then
		insert (iduser,idrole,isActive)
			values (new.iduser,@studentRoleId,1);

	-- add the new students to CES
	INSERT INTO [CESUsers].Student (IDEtudiant)
		select idetudiant
		from @tempStudents
		where
			[Action] = 'INSERT' and
			idetudiant not in (select idetudiant from CESUsers.Student);

	-- delete the removed students from CES
	delete from [CESUsers].[StudentEvaluation] where idetudiant not in (select idetudiant from @tempStudents)
	delete from [CESUsers].Student where idetudiant not in (select idetudiant from @tempStudents)
	-- congratz you are done

--rollback transaction