SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Thomas Farley
-- Create date: April 4, 2011
-- Description:	insert a language into the Language table.
-- =============================================
CREATE PROCEDURE [dbo].[uspInsertLanguage]
	@Language varchar(255) = ''
	,@ReturnValue INT Output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
		SET NOCOUNT ON;
	
	DECLARE @counter         INT;

	-- Insert statements for procedure here
	
	SET @ReturnValue = 0
	Select @counter = COUNT(*)
	FROM Resources.Language
	WHERE Language = @Language;

	
	IF (@counter > 0)
	BEGIN

		SET @ReturnValue = -1
	END
	
	ELSE
	BEGIN
	 INSERT INTO Resources.Language(Language) VALUES (@Language);
	 
	END
END
GO
GRANT EXECUTE ON  [dbo].[uspInsertLanguage] TO [CSAdmin]
GO
