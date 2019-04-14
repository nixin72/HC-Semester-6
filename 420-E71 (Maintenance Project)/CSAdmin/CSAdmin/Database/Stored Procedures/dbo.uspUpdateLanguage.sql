SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateLanguage]
	-- Add the parameters for the stored procedure here
	@Languageid int
	,@language varchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	Update Resources.Language 
	set [Language] =@language
	where LanguageID=@languageid
	
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateLanguage] TO [CSAdmin]
GO
