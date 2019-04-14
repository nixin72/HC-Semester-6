/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraEtudiants.EtudiantSession to a view
 */
 
drop synonym [ClaraEtudiants].[EtudiantSession];
go
CREATE VIEW [ClaraEtudiants].[EtudiantSession] AS
SELECT
	[IDEtudiantSession],
	[IDEtudiant],
	[IDProgramme],
	[IDUniteOrg],
	IDAdmission,
	[AnSession],
	[Etat],
	[SPE],
	[TypeFrequentation],
	[IDCohorteFC],
	[DateHeureCreation]
FROM
	[CLARA].[CLARA].[ReportClient].[EtudiantSession];