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

	declare @coordinatorRoleId int = (select top 1 IDRole from Applications.Role where Code = 'CO');
	declare @currentSemesterYear smallint = (select top 1 [CurrentYearSemester] from [Applications].[Settings])
	declare @rowsChanged int = 0;
	
	/** this is for debugging only

	declare @removedCoordinators table(
		IDUser int not null
	)
	insert into @removedCoordinators (IDUser)
	select top 10 u.IDUser from Users.[User] u join Applications.UserRole ur on u.IDUser = ur.IDUser where IDRole = @coordinatorRoleId

	delete from Applications.UserRole where IDUser in (select IDUser from @removedCoordinators)
	delete from Users.EmployeeUser where IDUser in (select IDUser from @removedCoordinators)
	delete from Users.[User] where IDUser in (select IDUser from @removedCoordinators)
	**/
	update Applications.UserRole set IsActive = 0 where idrole = @coordinatorRoleId;
	set @rowsChanged += @@ROWCOUNT

	declare @tempCoordinators table(
		action nvarchar(10),
		iduser int not null,
		IDEmploye int not null
	)

	merge into users.[user] old
		using (
			SELECT
				DISTINCT e.IDEmploye AS [IDEmploye],
				e.numero AS [username],
				e.Nom AS [lastName],
				e.Prenom AS [firstName],
				eu.IDUser AS [IDUser]
			FROM
				ClaraEmployes.Coordonnateur AS c
					JOIN ClaraEmployes.Employe AS e ON c.IDEmploye = e.IDEmploye
					FULL OUTER JOIN [Users].EmployeeUser AS eu ON C.IDEmploye = eu.IDEmploye
			WHERE
				e.Etat = 1 AND --This gets active teachers for this semester
				c.IDUniteOrg = 235 AND
				(c.AnSessionDebut IS NULL OR c.AnSessionDebut <= @currentSemesterYear) AND
				(c.AnSessionFin IS NULL OR c.AnSessionFin >= @currentSemesterYear)
			) new
			on old.IDUser = new.IDUser
	when not matched then
		insert (username, lastname, firstname, isactive)
			values (new.username, new.lastname, new.firstname, 1)
	when matched then
		update set IsActive = 1
	OUTPUT $action as [Action], inserted.IDUser, new.IDEmploye
	into @tempCoordinators (action, IDUser, IDEmploye);
	set @rowsChanged += @@ROWCOUNT

	insert into [Users].EmployeeUser (IDUser,IDEmploye)
		select IDUser, IDEmploye
		from @tempCoordinators
		where [Action] = 'INSERT';
	set @rowsChanged += @@ROWCOUNT

	merge into [Applications].[UserRole] old 
		using @tempCoordinators new
			on old.IDRole = @coordinatorRoleId and
				old.IDUser = new.IDUser
	when matched then
		update set isactive = 1
	when not matched then
		insert (IDUser,IDRole,isActive)
			values (new.IDUser,@coordinatorRoleId,1);
	set @rowsChanged += @@ROWCOUNT
	
	select @rowsChanged
	union
	select IDUser
		from @tempCoordinators
		where [Action] = 'INSERT'

	set nocount off

--rollback transaction