/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraReference.StatutEngagement to a view
 */

drop synonym [ClaraReference].[StatutEngagement];
go
CREATE VIEW [ClaraReference].[StatutEngagement] AS
SELECT
	[IDStatutEngagement],
	[Numero],
	[TitreCourt],
	[TitreLong],
	[TypeCalculETC],
	[TitreMoyen]
FROM
	[CLARA].[CLARA].[ReportClient].[StatutEngagement];