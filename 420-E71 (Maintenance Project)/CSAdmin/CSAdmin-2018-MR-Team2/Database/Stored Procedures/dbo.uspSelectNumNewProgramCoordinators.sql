
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- =============================================================
-- Author:		Louis Cloutier
-- Create date: April 25, 2012
-- Description:	Selects the number of coordinators for each
--				program that are to be added to CSAdmin
--				(whether that means as a new user or as a new
--				coordinator role)
-- =============================================================
CREATE PROCEDURE [dbo].[uspSelectNumNewProgramCoordinators]
	@AnSession	smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Get number of new coordinators
	SELECT count(DISTINCT c.IDEmploye) 'Number of Coordinators'
	FROM ClaraEmployes.Coordonnateur c
		JOIN ClaraEmployes.Employe e ON e.IDEmploye = c.IDEmploye
		AND (c.AnSessionDebut IS NULL OR c.AnSessionDebut <= @AnSession)
		AND (c.AnSessionFin IS NULL OR c.AnSessionFin >= @AnSession)
		AND (c.IDUniteOrg = 235 OR c.IDUniteOrg = 525)
		AND (
			--Check if they exist in CSAdmin at all
			c.IDEmploye NOT IN	(	SELECT eu.IDEmploye
									FROM Users.EmployeeUser eu
								)
			--check if they exist in CSAdmin but not as a coordinator
			OR c.IDEmploye NOT IN	(
									SELECT eu.IDEmploye
									FROM Users.EmployeeUser eu
											JOIN Applications.UserRole ur			ON ur.IDUser = eu.IDUser
											JOIN Applications.Role r				ON r.IDRole = ur.IDRole
											JOIN Applications.ApplicationRole ar	ON ar.IDRole = r.IDRole
											JOIN Applications.Application a			ON a.IDApplication = ar.IDApplication
									WHERE a.Code = 'CES'
											AND r.Code = 'CO'
									)
        )




END

/****** Object:  StoredProcedure [dbo].[uspInsertCoordinators]    Script Date: 05/04/2015 16:06:08 ******/
SET ANSI_NULLS ON
GO

GRANT EXECUTE ON  [dbo].[uspSelectNumNewProgramCoordinators] TO [CSAdmin]
GO
