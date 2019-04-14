SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Louis Cloutier
-- Create date: April 30, 2012
-- Description:	Selects all the roles in CSAdmin
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectAllRoles]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    SELECT	IDRole
			,Code
			,Description
    FROM Applications.Role
    ORDER BY Description
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectAllRoles] TO [CSAdmin]
GO
