/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraProgrammes.Programme to a view
 */

drop synonym [ClaraProgrammes].[Programme];
go
CREATE VIEW [ClaraProgrammes].[Programme] AS
SELECT
	[IDProgramme],
	[IDTypeSanction],
	[Numero],
	[TitreLong],
	[TitreTraduit],
	[TitreLongTraduit],
	[TitreLongOfficiel],
	[TypeProgramme],
	[IndicateurLocal],
	[AnneeVersion],
	[TitreCourtTraduit],
	[IDProgrammeSuperieur],
	[TitreMoyen],
	[TitreMoyenTraduit],
	[IDTypeFormation],
	[TitreCourt]
FROM
	[CLARA].[CLARA].[ReportClient].[Programme];