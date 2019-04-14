SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Jean-Michel Dunn>
-- Create date: <2012-02-01>
-- Description:	<Select system users depending on their username and application.>
-- =============================================
CREATE PROCEDURE [dbo].[uspLoginToBeDeleted]
	-- Add the parameters for the stored procedure here
	@username varchar(255),
	@app int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT u.IDUser, u.Username, u.LastName, u.FirstName, r.IDRole, r.Description AS "Role", r.Code, a.IDApplication, a.Description AS "Application"
	FROM Users.[User] u JOIN Applications.UserRole ur ON ur.IDUser = u.IDUser
JOIN Applications.[Role] r ON ur.IDRole = r.IDRole
JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
JOIN Applications.[Application] a ON a.IDApplication = ar.IDApplication
WHERE u.Username = @username
AND a.IDApplication = @app
AND u.IsActive = 1
AND ur.IsActive = 1
END
GO
GRANT EXECUTE ON  [dbo].[uspLoginToBeDeleted] TO [CES]
GRANT EXECUTE ON  [dbo].[uspLoginToBeDeleted] TO [CSAdmin]
GO
