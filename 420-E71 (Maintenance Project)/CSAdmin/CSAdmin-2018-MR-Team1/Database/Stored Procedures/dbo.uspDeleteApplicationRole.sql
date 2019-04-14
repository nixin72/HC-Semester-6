SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Adrien Pyke
-- Create date: 2014/05/07
-- Description:	delete application role
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteApplicationRole] 
	-- Add the parameters for the stored procedure here
	@IDApplicationRole int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT @IDApplicationRole
	DELETE FROM Applications.ApplicationRole
	WHERE IDApplicationRole = @IDApplicationRole
END
GO
GRANT EXECUTE ON  [dbo].[uspDeleteApplicationRole] TO [CSAdmin]
GO
