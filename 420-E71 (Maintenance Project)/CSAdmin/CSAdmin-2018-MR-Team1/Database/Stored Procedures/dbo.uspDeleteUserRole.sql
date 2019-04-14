SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspDeleteUserRole
Description: 	Disables an employee of a user role 
Author:	    	Matt Riopel
Create Date: 	April 23, 2012

Revision History:
Name					Date Modified		Revision   
=======================	==================	=======================================
Matt Riopel 			April 23, 2012		Initial Creation
Matt Riopel				April 30, 2012		Removed the parameter @ApplicationCode
****************************************************************************/
CREATE PROCEDURE [dbo].[uspDeleteUserRole]
	@IDEmploye int,
	@RoleCode varchar(max)
	--@ApplicationCode varchar(max)
AS
BEGIN

DECLARE @IDUser int --used to get the value of the employees new user number and not IDEmploye
DECLARE @IDRole int -- used to get the value of the teacher role, in case it changes

Select 
	@IDUser = eu.IDUser
from [Users].[EmployeeUser] eu
where eu.IDEmploye = @IDEmploye

-- gets the role for the application
SELECT @IDRole = r.IDRole
FROM   applications.Role r
WHERE  r.Code = @RoleCode  --Coop Coordinator Role

UPDATE [Applications].[UserRole]
SET IsActive = 0
WHERE IDUser = @IDUser
	and IDRole = @IDRole

END

GO
GRANT EXECUTE ON  [dbo].[uspDeleteUserRole] TO [CSAdmin]
GRANT EXECUTE ON  [dbo].[uspDeleteUserRole] TO [HERITAGE1\CSTEST$]
GRANT EXECUTE ON  [dbo].[uspDeleteUserRole] TO [IIS APPPOOL\DefaultAppPool]
GO
