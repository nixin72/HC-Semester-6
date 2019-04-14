/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraReference.UniteOrg to a view
 */

drop synonym [ClaraReference].[UniteOrg];
go
CREATE VIEW [ClaraReference].[UniteOrg] AS
SELECT
	[IDUniteOrg],
	[Numero],
	[TitreLong]
FROM
	[CLARA].[CLARA].[ReportClient].[UniteOrg];