SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Nicholas, Renaud
-- Create date: April 16, 2012
-- Description:	Updates the semester in the settings table.
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateCurrentSemester]
	@NewYearSemester as int, 
	@NewSemesterEndDate as date
AS
BEGIN
	SET NOCOUNT ON;

	Update Applications.Settings
	SET CurrentYearSemester = @NewYearSemester,
		SemesterEndDate     = @NewSemesterEndDate
	WHERE @NewYearSemester IN
            (SELECT AnSession FROM ClaraGroupes.groupe) 
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateCurrentSemester] TO [CSAdmin]
GO
