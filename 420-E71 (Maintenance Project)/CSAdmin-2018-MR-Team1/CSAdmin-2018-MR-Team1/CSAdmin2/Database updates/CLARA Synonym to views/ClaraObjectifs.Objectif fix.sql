/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraObjectifs.Objectif to a view
 */

drop synonym [ClaraObjectifs].[Objectif];
go
CREATE VIEW [ClaraObjectifs].[Objectif] AS
SELECT
	[IDObjectif],
	[Numero],
	[Nom]
FROM
	[CLARA].[CLARA].[ReportClient].[Objectif];