SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Kevin Brascoupe
-- Create date: February 1, 2011
-- Description:	Returns all languages
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectAllLanguages]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT l.LanguageID
			,l.Language
			,l.IsDefault 
			
	FROM Resources.Language l
	
	ORDER BY l.Language
	
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectAllLanguages] TO [CSAdmin]
GO
