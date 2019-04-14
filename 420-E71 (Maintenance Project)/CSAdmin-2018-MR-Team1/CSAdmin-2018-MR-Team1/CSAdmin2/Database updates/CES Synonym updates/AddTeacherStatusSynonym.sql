/* AUTHOR: guillaume Mercier
 * CREATED: 2018-May-10
 *
 * DESCRIPTION:
 * Adds a new synonym to the ces student evaluation table
 */

CREATE SYNONYM [CESUsers].[StudentEvaluation] FOR [CES].[Evaluations].[StudentEvaluation];
go