SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Adrien Pyke
-- Create date: 2014/05/06
-- Description:	delete a role, the application, and user roles
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteRole] 
	-- Add the parameters for the stored procedure here
	@IDRole int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Applications.ApplicationRole
	WHERE IDRole = @IDRole
	
	DELETE FROM Applications.UserRole
	WHERE IDRole = @IDRole
	
	DELETE FROM Applications.Role
	WHERE IDRole = @IDRole
END
GO
GRANT EXECUTE ON  [dbo].[uspDeleteRole] TO [CSAdmin]
GO
