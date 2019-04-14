SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspSelectAllUserWithSpecialRoles
Description: 	Gets all the users with the 3 special roles
				(CES administrator, Continuing Education Coordinator)
Author:	    	Matt Riopel
Create Date: 	April 30, 2012

Param:			None
Return:      	IDEmploye, FirstName, LastName, IDUser,
				IDUserRole,IDRole, Role Code, and Role Description

Revision History:
Name					Date Modified		Revision   
=======================	===================	=======================================
Matt Riopel 			April 30, 2012	    Initial Creation
Susan Turanyi			February 26, 2013	Added Alumni System roles
Catherine Losinger      May 02, 2013        Changed it so that it would get any role
****************************************************************************/
CREATE PROCEDURE [dbo].[uspSelectAllUserWithSpecialRoles]
	@IDRoles AS VARCHAR(100)
AS
BEGIN
PRINT(@IDRoles)
SELECT eu.IDEmploye, u.FirstName, u.LastName, u.Username, ur.IDUser, ur.IDUserRole, r.IDRole, r.Code, r.[Description]
FROM [Applications].[UserRole] ur
	JOIN [Users].[EmployeeUser] eu
		ON ur.IDUser = eu.IDUser
	JOIN [Users].[User] u
		ON ur.IDUser = u.IDUser
	JOIN [Applications].[Role] r
		ON ur.IDRole = r.IDRole 
WHERE ur.IsActive = 1 AND @IDRoles LIKE '%|' + CAST(r.IDRole AS VARCHAR(100)) + '|%'
ORDER BY r.Description, u.LastName, u.FirstName
--where ur.IDRole in 
--(SELECT r.IDRole
--FROM Applications.[Role] r
--WHERE r.Code IN ('CEC', 'AD', 'CSA','ASM','ASA')) and ur.IsActive = 1

END
GO
GRANT EXECUTE ON  [dbo].[uspSelectAllUserWithSpecialRoles] TO [CSAdmin]
GO
