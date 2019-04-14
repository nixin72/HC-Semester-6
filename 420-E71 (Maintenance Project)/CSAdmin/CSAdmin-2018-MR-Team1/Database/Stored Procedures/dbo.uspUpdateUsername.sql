SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Nicholas Renaud
-- Create date: April 19 2012
-- Description:	Select duplicate usernames from the Users table in CSAdmin
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateUsername]
		@Username varchar(100),
		@IsActive bit,
		@IDUser integer
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Users.[User]
	Set Username = @Username, IsActive = @IsActive
	Where IDUser = @IDUser
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateUsername] TO [CSAdmin]
GO
