/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraGroupes.Grille to a view
 */

drop synonym [ClaraGrilles].[Grille];
go
CREATE VIEW [ClaraGrilles].[Grille] AS
SELECT
	[IDGrille],
	[Numero],
	[Titre],
	[Etat],
	[IDProgramme],
	[IDUniteOrg],
	[AnSessionDebut],
	[AnSessionFin],
	[IDTypeSanction]
FROM
	[CLARA].[CLARA].[ReportClient].[Grille];