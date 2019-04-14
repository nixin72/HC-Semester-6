SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertUserRole
Description: 	Inserts a teacher and assigned them a role based on the application 
Author:	    	Matt Riopel
Create Date: 	April 23, 2012

Revision History:
Name					Date Modified		Revision   
=======================	==================	=======================================
Matt Riopel 			April 23, 2012		Initial Creation
Matt Riopel				April 30, 2012		Removed the parameter @ApplicationCode
											and all references to the parameter
****************************************************************************/
CREATE PROCEDURE [dbo].[uspInsertUserRole]
	@IDEmploye int,
	@RoleCode varchar(max)
AS
BEGIN

DECLARE @IDUser int --used to get the value of the employees new user number and not IDEmploye
DECLARE @IDRole int -- used to get the value of the teacher role, in case it changes
DECLARE @Count int = 0
DECLARE @firstname varchar(max)
DECLARE @lastname varchar(max)
DECLARE @username varchar(max)

Select 
	@Count =COUNT(*),
	@IDUser = eu.IDUser 
from [Users].[EmployeeUser] eu
where eu.IDEmploye = @IDEmploye
group by eu.IDUser 

-- gets the role for the application
SELECT @IDRole = r.IDRole
FROM   applications.[Role] r 
WHERE  r.Code = @RoleCode  --Coop Coordinator Role
       
if (@Count = 0) 
BEGIN

	INSERT INTO [Users].[User] 
				(LastName, FirstName, Username, IsActive) 
	SELECT  Nom, prenom, dbo.CreateUsername(prenom, nom) AS username, 1
	FROM   [ClaraEmployes].[Employe]
	WHERE  IDEmploye = @IDEmploye 
	
	SET @IDUser = Scope_identity() 

	INSERT INTO [Users].[EmployeeUser] 
				(IDUser, IDEmploye) 
	VALUES      (@IDUser, @IDEmploye )

	INSERT INTO [Applications].UserRole 
				(IDUser, IDRole, IsActive) 
	VALUES      (@IDUser, @IDRole, 1)

	--sets the IDUser from the IDEmploye
	SELECT @IDUser = eu.IDUser 
	FROM   [Users].[EmployeeUser] eu 
	WHERE  eu.IDEmploye = @IDEmploye
END
ELSE 
BEGIN

	UPDATE [Users].[User]
	SET IsActive = 1
	where IDUser = @IDUser 
	
	Select @Count = COUNT(*)
	from [Applications].[UserRole] ur
		join [Applications].[Role] r
			on r.IDRole = ur.IDRole
	where ur.IDUser = @IDUser
		and r.Code = @RoleCode
	
	--if the user does not exist insert it into the UserRole table
	if (@Count = 0)
	BEGIN
		INSERT INTO [Applications].[UserRole] (IDUser, IDRole, IsActive)
		values (@IDUser, @IDRole,1)
	END
	ELSE IF (@Count =1) --if it does exist just update the row
	BEGIN
		UPDATE [Applications].[UserRole]
		SET IsActive = 1
		where IDUser =@IDUser 
			and IDRole = @IDRole 
	END
END

END

GO
GRANT EXECUTE ON  [dbo].[uspInsertUserRole] TO [CSAdmin]
GRANT EXECUTE ON  [dbo].[uspInsertUserRole] TO [HERITAGE1\CSTEST$]
GRANT EXECUTE ON  [dbo].[uspInsertUserRole] TO [IIS APPPOOL\DefaultAppPool]
GO
