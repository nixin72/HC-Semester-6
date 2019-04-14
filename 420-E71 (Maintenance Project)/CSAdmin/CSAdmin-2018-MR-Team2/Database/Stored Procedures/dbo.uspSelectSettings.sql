SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:      Susan Turanyi
-- Create date: March 13, 2012
-- Description: This stored procedure returns the current YearSemester 
--				  and the SemesterEndDate as output parameters.
--                The YearSemester is in the same format as the AnSession in Clara.
--                There should be exactly one row in the table.
--                If fewer or more than 1 rows are found, the default 0 is returned
--                If the CurrentYearSemester is not in the Groupe table in Clara, nothing is returned.
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectSettings]
      @CurrentYearSemester SMALLINT = 0 OUTPUT,
      @SemesterEndDate DATE = NULL OUTPUT
AS
BEGIN
      DECLARE @SettingsCount SMALLINT = 0
      
      -- SET NOCOUNT ON added to prevent extra result sets from
      -- interfering with SELECT statements.
      SET NOCOUNT ON;

      SELECT @SettingsCount = COUNT(*)
      FROM Applications.Settings; 
      
      IF @SettingsCount = 1  --There should only be one row in the table
            SELECT @CurrentYearSemester = CurrentYearSemester, 
            @SemesterEndDate = SemesterEndDate
            FROM Applications.Settings
            WHERE CurrentyearSemester IN
            (SELECT AnSession FROM ClaraGroupes.groupe)
            --Ensures that the CurrentYearSemester is a valid YearSemester in Clara.
            --This should eventually be done when the data is inserted into the database.
 END
GO
GRANT EXECUTE ON  [dbo].[uspSelectSettings] TO [CES]
GRANT EXECUTE ON  [dbo].[uspSelectSettings] TO [CSAdmin]
GO
