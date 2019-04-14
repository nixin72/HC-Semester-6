SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================================
-- Author:		Erica Richard
-- Create date: May 17, 2017
-- Description:	Selects the number of students for each program
--				that are to be added to CSAdmin
-- 
-- =============================================================
CREATE PROCEDURE [dbo].[uspSelectNumNewCSAdminStudents]
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
		AND (es.IDEtudiant NOT IN (SELECT IDEtudiant FROM Users.StudentUser) OR es.IDEtudiant IS NULL)
	GROUP BY p.name
	ORDER BY 'Program Name'
	

END
GO
