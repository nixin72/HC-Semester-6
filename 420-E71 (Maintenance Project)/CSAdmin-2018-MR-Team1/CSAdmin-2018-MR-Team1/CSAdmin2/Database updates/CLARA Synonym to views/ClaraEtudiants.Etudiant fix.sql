/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraEtudiants.Etudiant to a view
 */
 
drop synonym [ClaraEtudiants].[Etudiant];
go
CREATE VIEW [ClaraEtudiants].[Etudiant] AS
SELECT
	[IDEtudiant],
	[Numero7],
	[Nom],
	[Prenom],
	[DateNaissance],
	[Sexe]
FROM
	[CLARA].[CLARA].[ReportClient].[Etudiant];