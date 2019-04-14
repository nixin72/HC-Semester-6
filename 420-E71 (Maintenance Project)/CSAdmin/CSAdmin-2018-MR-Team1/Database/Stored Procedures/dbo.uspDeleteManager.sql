SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Alex Desbiens
-- Create date: APR 04 2013
-- Description:	Deletes a row from UserRole for a given IDUser, where that row describes a manager status.
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteManager]
	-- Add the parameters for the stored procedure here
	@IDUser Integer
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM [Applications].[UserRole]
	WHERE IDUser = @IDUser
	AND IDRole = (SELECT IDRole FROM [Applications].[Role] WHERE Code = 'ASM')
END

GO
GRANT EXECUTE ON  [dbo].[uspDeleteManager] TO [CSAdmin]
GO
