SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Alex Desbiens
-- Create date: APR 19 2013
-- Description:	Selects all applications for a given role
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectApplicationsByRole]
	-- Add the parameters for the stored procedure here
	@IDRole INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		a.IDApplication,
		a.Description,
		a.Code,
		ar.isActive
	FROM Applications.Application a
	JOIN Applications.ApplicationRole ar ON a.IDApplication = ar.IDApplication
	WHERE ar.IDRole = @IDRole
	AND ar.isActive = 1
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectApplicationsByRole] TO [CSAdmin]
GO
