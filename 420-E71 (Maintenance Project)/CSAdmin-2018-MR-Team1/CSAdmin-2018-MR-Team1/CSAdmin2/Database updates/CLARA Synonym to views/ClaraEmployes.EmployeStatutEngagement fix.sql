/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraEmployes.EmployeStatutEngagement to a view
 */
 
drop synonym [ClaraEmployes].[EmployeStatutEngagement];
go
CREATE VIEW [ClaraEmployes].[EmployeStatutEngagement] AS
SELECT
	[IDEmployeStatutEngagement],
	[IDEmploye],
	[AnSessionDebut],
	[AnSessionFin],
	[IDStatutEngagement]
FROM
	[CLARA].[CLARA].[ReportClient].[EmployeStatutEngagement];