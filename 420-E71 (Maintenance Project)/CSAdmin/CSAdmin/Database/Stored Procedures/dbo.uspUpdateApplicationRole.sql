SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Louis Cloutier
-- Create date: April 30, 2012
-- Description:	Changes an application role's
--				isactive state
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateApplicationRole]
	@IDApplicationRole int,
	@isActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Applications.ApplicationRole
    SET isActive = @isActive
    WHERE IDApplicationRole = @IDApplicationRole
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateApplicationRole] TO [CSAdmin]
GO
