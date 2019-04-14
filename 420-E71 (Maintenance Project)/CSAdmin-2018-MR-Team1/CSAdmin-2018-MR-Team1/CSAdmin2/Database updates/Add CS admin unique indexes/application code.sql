/* AUTHOR: guillaume Mercier
 * CREATED: 2018-03-12
 *
 * DESCRIPTION:
 * Adds a unique constraint to the code column of the application table
 */


ALTER TABLE [Applications].[Application] ADD UNIQUE (Code);