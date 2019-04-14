/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraObjectifs.FeuilleObjectifProgramme to a view
 */

drop synonym [ClaraObjectifs].[FeuilleObjectifProgramme];
go
CREATE VIEW [ClaraObjectifs].[FeuilleObjectifProgramme] AS
SELECT
	[IDObjectif],
	[IDBrancheObjectifProgramme],
	[IndicateurOptionnel]
FROM
	[CLARA].[CLARA].[ReportClient].[FeuilleObjectifProgramme];