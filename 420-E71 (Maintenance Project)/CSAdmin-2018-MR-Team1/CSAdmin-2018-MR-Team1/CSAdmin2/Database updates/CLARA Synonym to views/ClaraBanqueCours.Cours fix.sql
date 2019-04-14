/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraBanqueCours.Cours to a view
 */
 
drop synonym [ClaraBanqueCours].[Cours];
go
CREATE VIEW [ClaraBanqueCours].[Cours] AS
SELECT
	[IDCours],
	[IDCategorieCours],
	[Numero],
	[TitreLong],
	[NumeroOfficiel],
	[TitreCourt],
	[TitreCourtOfficiel],
	[TitreMoyen],
	[TitreMoyenTraduit],
	[LangueOrigine],
	[PonderationTheo],
	[PonderationLab],
	[TitreLongTraduit],
	[IDCoursMinistere],
	[IndicateurLocal]
FROM
	[CLARA].[CLARA].[ReportClient].[Cours]