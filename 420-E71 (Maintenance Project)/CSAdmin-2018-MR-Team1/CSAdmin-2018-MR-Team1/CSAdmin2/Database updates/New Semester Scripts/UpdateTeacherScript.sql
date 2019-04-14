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

	set nocount on

	declare @teacherRoleId int = (select top 1 IDRole from Applications.Role where Code = 'TE');
	declare @currentSemesterYear smallint = (select top 1 [CurrentYearSemester] from [Applications].[Settings])
	declare @rowsChanged int = 0;
	declare @IDTeacherStatus int;

	-- get the ces teacher status id for the nt code
	SELECT top(1) @IDTeacherStatus = IDTeacherStatus 
		FROM [CESUsers].[TeacherStatus]
		WHERE Code = 'NT';
	
	-- default to id 2
	IF @IDTeacherStatus IS NULL
		SELECT @IDTeacherStatus = 2

	/* -- this is used to test the query
	declare @removedTeachers table(
		IDUser int not null
	)
	insert into @removedTeachers (idUser)
	select top 10 u.IDUser from Users.[User] u join Applications.UserRole ur on u.IDUser = ur.IDUser where IDRole = @teacherRoleId

	delete from Applications.UserRole where IDUser in (select IDUser from @removedTeachers)
	delete from Users.EmployeeUser where IDUser in (select IDUser from @removedTeachers)
	delete from Users.[User] where IDUser in (select IDUser from @removedTeachers)
	*/
	
	update Users.[User] set
		IsActive = 0
	where
		IDUser in (
			select
				distinct IDUser
			from
				Applications.UserRole
			where
				IDRole = @teacherRoleId
		);
	set @rowsChanged += @@ROWCOUNT
	update Applications.UserRole set IsActive = 0 where idrole = @teacherRoleId;
	set @rowsChanged += @@ROWCOUNT

	declare @tempTeachers table(
		action nvarchar(10),
		iduser int not null,
		IDEmploye int not null,
		FirstName nvarchar(60),
		LastName nvarchar(60)
	)

	merge into users.[user] old
		using (
			SELECT
				DISTINCT e.IDEmploye as [IDEmploye],
				e.numero as [username],
				e.Nom as [lastName],
				e.Prenom as [firstName],
				eu.IDUser as [IDUser]
			FROM
				ClaraEmployes.Employe e
					JOIN ClaraGroupes.EmployeGroupe eg ON e.IDEmploye = eg.IDEmploye
					JOIN ClaraGroupes.Groupe g ON eg.IDGroupe = g.IDGroupe
					full outer join [Users].EmployeeUser eu on e.idemploye = eu.IDEmploye
			WHERE
				e.Etat = 1 AND --This gets active teachers for this semester
				g.AnSession = @currentSemesterYear
			) new
			on old.IDUser = new.IDUser
	when not matched then
		insert (username, lastname, firstname, isactive)
			values (new.username, new.lastname, new.firstname, 1)
	when matched then
		update set IsActive = 1
	OUTPUT $action as [Action], inserted.IDUser, new.IDEmploye, new.FirstName, new.lastName 
	into @tempTeachers (action, IDUser, IDEmploye, FirstName, LastName);
	set @rowsChanged += @@ROWCOUNT

	insert into [Users].EmployeeUser (IDUser,IDEmploye)
		select IDUser, IDEmploye
		from @tempTeachers
		where [Action] = 'INSERT';
	set @rowsChanged += @@ROWCOUNT

	merge into [Applications].[UserRole] old 
		using @tempTeachers new
			on old.IDRole = @teacherRoleId and
				old.IDUser = new.IDUser
	when matched then
		update set isactive = 1
	when not matched then
		insert (IDUser,IDRole,isActive)
			values (new.IDUser,@teacherRoleId,1);
	set @rowsChanged += @@ROWCOUNT
	
	
	
	-- add new teaches to ces
	insert into CESUsers.Teacher (IDEmploye,FirstName,LastName,IDTeacherStatus)
		SELECT IDEmploye, FirstName, LastName, @IDTeacherStatus
		from @tempTeachers
		where
			[Action] = 'INSERT' and
			IDEmploye not in (select IDEmploye from CESUsers.Teacher)
	set @rowsChanged += @@ROWCOUNT

	-- return the new items and rows changed
	select @rowsChanged
	union
	select IDUser
		from @tempTeachers
		where [Action] = 'INSERT'

	set nocount off
--rollback transaction