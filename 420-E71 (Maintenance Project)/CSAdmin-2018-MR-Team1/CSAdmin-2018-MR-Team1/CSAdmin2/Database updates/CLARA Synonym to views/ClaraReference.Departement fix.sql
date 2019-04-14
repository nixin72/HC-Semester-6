/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraReference.Departement to a view
 */

drop synonym [ClaraReference].[Departement];
go
CREATE VIEW [ClaraReference].[Departement] AS
SELECT
	[IDDepartement],
	[Numero],
	[TitreLong],
	[TitreMoyen],
	[IndicateurActif]
FROM
	[CLARA].[CLARA].[ReportClient].[Departement];