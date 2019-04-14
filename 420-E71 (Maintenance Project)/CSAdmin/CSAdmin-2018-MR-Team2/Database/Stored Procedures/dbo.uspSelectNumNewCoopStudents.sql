SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[uspSelectNumNewCoopStudents]
	@AnSession	smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	SELECT COUNT(DISTINCT e.IDEtudiant) 'NumStudents', p.IDProgramme 'ProgramID'
	FROM ClaraEtudiants.EtudiantSession es
		JOIN ClaraEtudiants.Etudiant e		ON es.IDEtudiant = e.IDEtudiant
		JOIN ClaraProgrammes.Programme p		ON es.IDProgramme = p.IDProgramme	
	WHERE es.IDProgramme IN (SELECT ProgramID FROM eCoop.dbo.Program) 
		AND es.AnSession = @AnSession
							--select students enrolled IN current semester
		AND es.Etat > 0 --Indique l'état de l'enregistrement: 0 = Supprimé,1 = Actif
		AND e.IDEtudiant NOT IN (SELECT StudentID from eCoop.dbo.Student) 
	GROUP BY p.IDProgramme
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectNumNewCoopStudents] TO [CSAdmin]
GO
