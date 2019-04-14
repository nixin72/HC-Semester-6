SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Adrien Pyke
-- Create date: 2014/04/29
-- Description:	delete a language
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteLanguage] 
	-- Add the parameters for the stored procedure here
	@LanguageID int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Resources.Language
	WHERE LanguageID = @LanguageID
END
GO
GRANT EXECUTE ON  [dbo].[uspDeleteLanguage] TO [CSAdmin]
GO
