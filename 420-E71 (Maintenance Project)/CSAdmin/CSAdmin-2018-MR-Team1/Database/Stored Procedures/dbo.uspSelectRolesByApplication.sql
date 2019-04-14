SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Alex Desbiens
-- Create date: 16 APR 2013
-- Description:	Selects roles based on a given IDApplication
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectRolesByApplication] 
	-- Add the parameters for the stored procedure here
	@IDApplication VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	r.IDRole
			,r.Code
			,r.Description
			,ar.isActive
			,ar.IDApplicationRole
    FROM Applications.Role r
    JOIN Applications.ApplicationRole ar ON r.IDRole = ar.IDRole
    WHERE ar.IDApplication = @IDApplication
    ORDER BY Description
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectRolesByApplication] TO [CSAdmin]
GO
