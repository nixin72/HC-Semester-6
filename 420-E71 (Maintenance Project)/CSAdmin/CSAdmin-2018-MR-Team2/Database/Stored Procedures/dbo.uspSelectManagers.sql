SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectManagers]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT
		 u.FirstName
		,u.LastName
		,u.IDUser
		,MAX(CASE WHEN r.Code IN ('ASM') THEN 1 ELSE 0 END) AS 'IsManager'
	FROM [Users].[EmployeeUser] e
		 Join [Users].[User] u ON e.IDUser = u.IDUser
		 JOIN [Applications].[UserRole] ur ON u.IDUser = ur.IDUser
		 JOIN [Applications].[Role] r ON ur.IDRole = r.IDRole
	GROUP BY u.FirstName,u.LastName,u.IDUser
	HAVING MAX(CASE WHEN r.Code IN ('ASM') THEN 1 ELSE 0 END) = 1
	ORDER BY LastName
END

GO
GRANT EXECUTE ON  [dbo].[uspSelectManagers] TO [CSAdmin]
GO
