/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraEtudiants.Citoyennete to a view
 */
 
drop synonym [ClaraEtudiants].[Citoyennete];
go
CREATE VIEW [ClaraEtudiants].[Citoyennete] AS
SELECT
	[IDCitoyennete],
	[IDEtudiant],
	[StatutLegal],
	[IDPaysCitoyennete]
FROM
	[CLARA].[CLARA].[ReportClient].[Citoyennete];