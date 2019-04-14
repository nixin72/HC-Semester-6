SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================================
-- Author:		Louis Cloutier
-- Create date: April 23, 2012
-- Description:	Selects the number of students for each program
--				that are to be added to CES
-- 
-- Updates:		Gage Geneau		May 1, 2013		Added a case statement to select the translated version, and if there is none select the normal name and have all the results returned in one column for improved sorting performance		
-- =============================================================
CREATE PROCEDURE [dbo].[uspSelectNumNewCESStudents]
	@AnSession	smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT COUNT(es.IDEtudiant) 'Number of Students', 
	p.name AS 'Program Name'
	FROM ClaraEtudiants.EtudiantSession es
	LEFT JOIN Resources.ProgramVersion pv ON pv.IDProgramClara = es.IDProgramme
	LEFT JOIN Resources.Program p ON pv.IDProgram = p.IDProgram 
	WHERE (es.AnSession = @AnSession OR es.AnSession IS NULL) 
		AND (es.Etat > 0 OR es.Etat IS NULL) --Indique l'état de l'enregistrement: 0 = Supprimé,1 = Actif
		AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
		AND (es.IDEtudiant NOT IN (SELECT IDEtudiant FROM CES.Users.Student) OR es.IDEtudiant IS NULL)
	GROUP BY p.name
	ORDER BY 'Program Name'
	

END
GO
GRANT EXECUTE ON  [dbo].[uspSelectNumNewCESStudents] TO [CSAdmin]
GO
