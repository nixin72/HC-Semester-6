
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Adrien Pyke
-- Create date: 2014/05/06
-- Description:	delete an application and application roles
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteApplication] 
	-- Add the parameters for the stored procedure here
	@IDApplication int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Applications.ApplicationRole
	WHERE IDApplication = @IDApplication
	
	DELETE FROM Applications.Application
	WHERE IDApplication = @IDApplication
END
GO

GRANT EXECUTE ON  [dbo].[uspDeleteApplication] TO [CSAdmin]
GO
