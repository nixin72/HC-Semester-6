/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraObjectifs.BrancheObjectifProgramme to a view
 */

drop synonym [ClaraObjectifs].[BrancheObjectifProgramme];
go
CREATE VIEW [ClaraObjectifs].[BrancheObjectifProgramme] AS
SELECT
	[IDBrancheObjectifProgramme],
	[IDBrancheObjectifProgrammeParent],
	[IDProgramme]
FROM
	[CLARA].[CLARA].[ReportClient].[BrancheObjectifProgramme];