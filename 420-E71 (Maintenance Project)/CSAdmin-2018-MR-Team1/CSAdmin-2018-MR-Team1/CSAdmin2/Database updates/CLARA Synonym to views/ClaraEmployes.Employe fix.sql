/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraEmployes.Employe to a view
 */
 
drop synonym [ClaraEmployes].[Employe];
go
CREATE VIEW [ClaraEmployes].[Employe] AS
SELECT
	[IDEmploye],
	[Numero],
	[Nom],
	[Prenom],
	[NomPrenom],
	[NomAbrege],
	[Origine],
	[DateHeureCreation],
	[Etat]
FROM
	[CLARA].[CLARA].[ReportClient].[Employe];