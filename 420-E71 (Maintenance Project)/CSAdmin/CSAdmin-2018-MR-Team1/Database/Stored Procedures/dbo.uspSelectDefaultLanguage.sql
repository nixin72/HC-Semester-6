SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectDefaultLanguage] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT LanguageID, Language, IsDefault 
	From Resources.Language 
	where IsDefault =1
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectDefaultLanguage] TO [CSAdmin]
GO
