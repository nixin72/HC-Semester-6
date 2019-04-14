/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraReference.CategorieCours to a view
 */

drop synonym [ClaraReference].[CategorieCours];
go
CREATE VIEW [ClaraReference].[CategorieCours] AS
SELECT
	[IDCategorieCours],
	[Numero],
	[Titre]
FROM
	[CLARA].[CLARA].[ReportClient].[CategorieCours];