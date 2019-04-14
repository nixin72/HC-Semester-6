SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vw_SelectActiveUserRoles]
	as
	SELECT eu.IDEmploye, eu.IDUser, ur.IsActive 
	FROM [Users].[EmployeeUser] eu
		join [Applications].[UserRole] ur
			on eu.IDUser = ur.IDUser
		join [Applications].[Role] r
			on ur.IDRole = r.IDRole 
		join [Applications].ApplicationRole ar
			on r.IDRole = ar.IDRole 
		join [Applications].Application a
			on ar.IDApplication  = a.IDApplication 
	WHERE ur.IsActive = 1
		and r.Code = 'CC'
		and a.Code = 'ECO'
GO
