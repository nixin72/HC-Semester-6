SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspSetDefaultLanguage]
	-- Add the parameters for the stored procedure here
	@languageid int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Resources.Language 
	SET IsDefault =0
	
	UPDATE Resources.Language
	SET IsDefault =1
	where LanguageID=@languageid
END
GO
GRANT EXECUTE ON  [dbo].[uspSetDefaultLanguage] TO [CSAdmin]
GO
