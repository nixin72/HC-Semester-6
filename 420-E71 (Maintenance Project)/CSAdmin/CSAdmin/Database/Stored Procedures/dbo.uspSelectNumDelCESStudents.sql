SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================================
-- Author:		Louis Cloutier
-- Create date: April 23, 2012
-- Description:	Selects the number of students that are to be 
--				deleted from CES and CSAdmin (deactivated)
-- =============================================================
CREATE PROCEDURE [dbo].[uspSelectNumDelCESStudents]
	@AnSession	smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 
	
	--Select all students currently in CES and
	--compare it to the list of students that exist 
	--for the given semester, return the difference
	SELECT COUNT(IDEtudiant) 'Number of Students'
	FROM CES.Users.student s
	WHERE IDEtudiant NOT IN
		(
		SELECT DISTINCT es.IDEtudiant
		FROM ClaraEtudiants.EtudiantSession es
			JOIN ClaraProgrammes.Programme p	ON p.IDProgramme = es.IDProgramme
		WHERE (es.AnSession = @AnSession OR es.AnSession IS NULL)                  
			AND (es.Etat > 0 OR es.Etat IS NULL) --Indique l'état de l'enregistrement: 0 = Supprimé,1 = Actif
		)
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectNumDelCESStudents] TO [CSAdmin]
GO
