SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Louis Cloutier
-- Create date: April 30, 2012
-- Description:	Deactivates user roles for the given
--				IDRole
-- =============================================
CREATE PROCEDURE [dbo].[uspDeactivateUserRoles]
	@IDRole int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Applications.UserRole
    SET IsActive = 0
    WHERE IDRole = @IDRole
END
GO
GRANT EXECUTE ON  [dbo].[uspDeactivateUserRoles] TO [CSAdmin]
GO
