/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraGroupes.Groupe to a view
 */

drop synonym [ClaraGroupes].[Groupe];
go
CREATE VIEW [ClaraGroupes].[Groupe] AS
SELECT
	[IDGroupe],
	[IDCours],
	[IDDiscipline],
	[IDUniteOrg],
	[IDCohorteFC],
	[Numero],
	[NumeroGroupeEvaluation],
	[AnSession],
	[Etat]
FROM
	[CLARA].[CLARA].[ReportClient].[Groupe];