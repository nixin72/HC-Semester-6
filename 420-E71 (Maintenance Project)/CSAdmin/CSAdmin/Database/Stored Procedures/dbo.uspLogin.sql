SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Jean-Michel Dunn>
-- Create date: <2012-02-06>
-- Description:	<Select system users depending on their username and application.>
-- =============================================
CREATE PROCEDURE [dbo].[uspLogin]
	-- Add the parameters for the stored procedure here
	@Username varchar(255),
	--@app int
	@ApplicationCode char(3)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
--SELECT u.IDUser, u.Username, u.LastName, u.FirstName, su.IDEtudiant, eu.IDEmploye, r.IDRole, r.Description AS "Role", r.Code, a.IDApplication, a.Description AS "Application"
--	FROM Users.[User] u JOIN Applications.UserRole ur ON ur.IDUser = u.IDUser
--JOIN Applications.[Role] r ON ur.IDRole = r.IDRole
--JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
--JOIN Applications.[Application] a ON a.IDApplication = ar.IDApplication
--LEFT JOIN Users.StudentUser su ON u.IDUser = su.IDUser
--LEFT JOIN Users.EmployeeUser eu ON u.IDUSer = eu.IDUser 
--WHERE u.Username = @username
--AND a.IDApplication = @app
--AND u.IsActive = 1
--AND ur.IsActive = 1

--Author: Renee Ghattas
SELECT u.Username, u.LastName, u.FirstName, su.IDEtudiant, eu.IDEmploye, r.Code, r.Description AS "Role", u.IDUser
	FROM Users.[User] u JOIN Applications.UserRole ur ON ur.IDUser = u.IDUser
JOIN Applications.[Role] r ON ur.IDRole = r.IDRole
JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
JOIN Applications.[Application] a ON a.IDApplication = ar.IDApplication
LEFT JOIN Users.StudentUser su ON u.IDUser = su.IDUser
LEFT JOIN Users.EmployeeUser eu ON u.IDUSer = eu.IDUser 
WHERE u.Username = @Username
AND a.Code = @ApplicationCode
AND u.IsActive = 1
AND ur.IsActive = 1
END

GO
GRANT EXECUTE ON  [dbo].[uspLogin] TO [Alumni]
GRANT EXECUTE ON  [dbo].[uspLogin] TO [CES]
GRANT EXECUTE ON  [dbo].[uspLogin] TO [CSAdmin]
GRANT EXECUTE ON  [dbo].[uspLogin] TO [eCoop]
GRANT EXECUTE ON  [dbo].[uspLogin] TO [HERITAGE1\CSTEST$]
GRANT EXECUTE ON  [dbo].[uspLogin] TO [IIS APPPOOL\DefaultAppPool]
GO
