SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Name:        uspSelectEmployeeUsers
-- Description: Select all the Employees in the college.
-- Author:		Catherine Losinger
-- Create date: March 20 2013

-- Param:       
--             
-- Return:

-- Revision History:
-- Name                Date Modified     Revision
-- =================== ================  =====================   
-- Alex Desbiens		APR 04 2013			Added check for manager status
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectEmployeeUsers]
	
AS
BEGIN
	SET NOCOUNT ON;

  SELECT DISTINCT
		 u.FirstName
		,u.LastName
		,u.IDUser
		,MAX(CASE WHEN r.Code IN ('ASM', 'ASA') THEN 1 ELSE 0 END) AS 'IsManager'
	FROM [Users].[EmployeeUser] e
		 Join [Users].[User] u ON e.IDUser = u.IDUser
		 JOIN [Applications].[UserRole] ur ON u.IDUser = ur.IDUser
		 JOIN [Applications].[Role] r ON ur.IDRole = r.IDRole
	GROUP BY u.FirstName,u.LastName,u.IDUser
	ORDER BY LastName
	
END

GO
GRANT EXECUTE ON  [dbo].[uspSelectEmployeeUsers] TO [CSAdmin]
GO
