SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================================
-- Author:		Louis Cloutier
-- Create date: April 23, 2012
-- Description:	Selects the number of faculty for each program
--				that are to be added to CES and CSAdmin
-- =============================================================
CREATE PROCEDURE [dbo].[uspSelectNumNewCESFaculty]
	@AnSession	smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	CREATE TABLE #tempEmploye
	(
	IDEmploye Integer);


	--Get number of new teachers
	INSERT INTO #tempEmploye
	SELECT DISTINCT eg.IDEmploye
	FROM ClaraGroupes.EmployeGroupe eg
		JOIN ClaraGroupes.Groupe g			  ON eg.IDGroupe = g.IDGroupe
		JOIN ClaraEmployes.Employe e		  ON e.IDEmploye = eg.IDEmploye
		JOIN ClaraInscriptions.Inscription i ON i.IDGroupe = g.IDGroupe
		JOIN ClaraReference.UniteOrg uo	  ON uo.IDUniteOrg = i.IDUniteOrg
	WHERE g.etat = 1 --This gets active teachers for this semester
		AND g.AnSession = @AnSession
		AND (uo.IDUniteOrg = 235 OR uo.IDUniteOrg = 525)
		AND e.IDEmploye NOT IN	(
								SELECT t.IDEmploye
								FROM CES.Users.Teacher t
								)

	--Return number of new faculty
	SELECT COUNT(IDEmploye) 'New CES Faculty'
	FROM #tempEmploye
	
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectNumNewCESFaculty] TO [CSAdmin]
GO
