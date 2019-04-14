SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Dorothy Lacroix
-- Create date: Jan 27, 2015
-- Description:	Get users - By Application Code
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectAllUsersByApplicationCode]
	@ApplicationCode CHAR(3)
AS
BEGIN
SELECT eu.IDEmploye, u.LastName, u.FirstName, ur.IsActive, r.Code, r.Description
FROM Users.[User] u
JOIN Applications.UserRole ur ON u.IDUser = ur.IDUser
JOIN Users.EmployeeUser eu ON eu.IDUser = u.IDUser AND eu.IDUser = ur.IDUser
JOIN Applications.Role r ON r.IDRole = ur.IDRole
JOIN Applications.ApplicationRole ar ON r.IDRole = ar.IDRole
JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
WHERE ur.IsActive = 1 
AND ar.IDApplicationRole IN 
(
	SELECT ar2.IDApplicationRole
	FROM Applications.Application a2
	JOIN Applications.ApplicationRole ar2 ON a2.IDApplication = ar2.IDApplication
	JOIN Applications.Role r2 ON r2.IDRole = ar2.IDRole
	WHERE a2.Code = @ApplicationCode
)
ORDER BY u.LastName, u.FirstName;
END;
GO
GRANT EXECUTE ON  [dbo].[uspSelectAllUsersByApplicationCode] TO [CSAdmin]
GO
