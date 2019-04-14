
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- =============================================================
-- Author:		Louis Cloutier
-- Create date: April 23, 2012
-- Description:	Selects the number of coordinators that are to be 
--				deactivated in CSAdmin
-- =============================================================
CREATE PROCEDURE [dbo].[uspSelectNumDelCESCoordinators]
	@AnSession	smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Select all coordinators currently in CES and
	--compare it to the list of coordinators that exist 
	--for the given semester, return the difference 
	SELECT count(u.IDUser) 'Number of Coordinators'
	FROM Users.EmployeeUser eu
		JOIN Applications.UserRole ur			ON ur.IDUser = eu.IDUser
		JOIN Applications.Role r				ON r.IDRole = ur.IDRole
		JOIN Applications.ApplicationRole ar	ON ar.IDRole = r.IDRole
		JOIN Applications.Application a			ON a.IDApplication = ar.IDApplication
		JOIN [Users].[User] u					ON eu.IDUser = u.IDUser
	WHERE ur.IsActive = 1
		AND r.Code = 'CO'
		AND eu.IDEmploye NOT IN 
		(
		SELECT DISTINCT c.IDEmploye 
		FROM ClaraEmployes.Coordonnateur c
			JOIN ClaraEmployes.Employe e			ON e.IDEmploye = c.IDEmploye
			AND (c.AnSessionDebut IS NULL OR c.AnSessionDebut <= @AnSession)
			AND (c.AnSessionFin IS NULL OR c.AnSessionFin >= @AnSession)
			AND c.IDUniteOrg = 235
		)

END
GO

GRANT EXECUTE ON  [dbo].[uspSelectNumDelCESCoordinators] TO [CSAdmin]
GO
