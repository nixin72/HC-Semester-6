/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraInscriptions.Inscription to a view
 */

drop synonym [ClaraInscriptions].[Inscription];
go
CREATE VIEW [ClaraInscriptions].[Inscription] AS
SELECT
	[IDInscription],
	[IDEtudiantSession],
	[IDCours],
	[IDGroupe],
	[IDUniteOrg],
	[Etat],
	[TypeRAF],
	[SourceFinancement],
	[DateHeureCreation]
FROM
	[CLARA].[CLARA].[ReportClient].[Inscription];