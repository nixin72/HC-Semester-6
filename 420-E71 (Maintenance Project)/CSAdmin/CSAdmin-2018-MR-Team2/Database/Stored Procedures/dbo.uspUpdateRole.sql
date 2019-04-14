SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Louis Cloutier
-- Create date: April 30, 2012
-- Description:	Updates a role based on IDRole
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateRole]
	@IDRole int,
	@Code VARCHAR(MAX),
	@Description VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    UPDATE Applications.Role
    SET Code = @Code
		,Description = @Description
	WHERE IDRole = @IDRole
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateRole] TO [CSAdmin]
GO
