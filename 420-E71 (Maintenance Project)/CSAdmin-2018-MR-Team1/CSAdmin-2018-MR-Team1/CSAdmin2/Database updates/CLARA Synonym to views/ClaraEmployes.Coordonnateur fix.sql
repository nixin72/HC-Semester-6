/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraEmployes.Coordonnateur to a view
 */
 
drop synonym [ClaraEmployes].[Coordonnateur];
go
CREATE VIEW [ClaraEmployes].[Coordonnateur] AS
SELECT
	[IDCoordonnateur],
	[IDEmploye],
	[IDDepartement],
	[IDUniteOrg],
	[AnSessionDebut],
	[AnSessionFin]
FROM
	[CLARA].[CLARA].[ReportClient].[Coordonnateur];