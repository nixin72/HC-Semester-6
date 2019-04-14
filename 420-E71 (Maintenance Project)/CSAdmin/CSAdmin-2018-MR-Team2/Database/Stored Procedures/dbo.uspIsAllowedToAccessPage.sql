SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Nicholas Renaud
-- Create date: April 23, 2012
-- Description:	Determines if a user role has access to a page
-- =============================================
CREATE PROCEDURE [dbo].[uspIsAllowedToAccessPage] 
	-- Add the parameters for the stored procedure here
	@Role varchar(50),
	@IDApplication as int,
	@PageName as varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT PageName
	From Applications.PageRoleSecurity prc
	Where prc.[Role] IN (@Role, 'Generic', 'NotLogin')
	AND prc.IDApplication = @IDApplication
	and prc.PageName = @PageName
	
	IF @@ROWCOUNT > 0 
	BEGIN
		Return 1
	END
	ELSE
	BEGIN
		Return -1
	END
END
GO
GRANT EXECUTE ON  [dbo].[uspIsAllowedToAccessPage] TO [CSAdmin]
GO
