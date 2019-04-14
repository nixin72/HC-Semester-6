SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Louis Cloutier
-- Create date: April 30, 2012
-- Description:	Selects the application roles in CSAdmin
--				based on an IDRole
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectApplicationRoles]
	@IDRole VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    SELECT	ar.IDApplicationRole
			,ar.IDRole
			,a.IDApplication
			,a.Description 'AppDescription'
			,ar.isActive
    FROM Applications.ApplicationRole ar
				JOIN Applications.Application a		ON a.IDApplication = ar.IDApplication
	WHERE ar.IDRole =  @IDRole
	ORDER BY a.Description
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectApplicationRoles] TO [CSAdmin]
GO
