SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Adrien Pyke
-- Create date: 2014/05/01
-- Description:	Check if username that's being changed to is duplicate
-- =============================================
CREATE PROCEDURE [dbo].[uspCheckDuplicateUsername] 
	-- Add the parameters for the stored procedure here
	@IDUser int = 0, 
	@Username varchar(60) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(IDUser) AS CountDuplicate FROM [Users].[User] WHERE IDUser <> @IDUser AND Username = @Username
END
GO
GRANT EXECUTE ON  [dbo].[uspCheckDuplicateUsername] TO [CSAdmin]
GO
