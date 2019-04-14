/* AUTHOR: guillaume Mercier
 * CREATED: 2018-02-26
 *
 * DESCRIPTION:
 * turns the old clara synonym for ClaraGroupes.EmployeGroupe to a view
 */

drop synonym [ClaraGroupes].[EmployeGroupe];
go
CREATE VIEW [ClaraGroupes].[EmployeGroupe] AS
SELECT
	[IDEmployeGroupe],
	[IDEmploye],
	[IDGroupe]
FROM
	[CLARA].[CLARA].[ReportClient].[EmployeGroupe];