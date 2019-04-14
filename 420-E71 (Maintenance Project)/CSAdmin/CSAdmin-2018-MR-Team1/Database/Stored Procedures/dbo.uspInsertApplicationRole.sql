SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Louis Cloutier
-- Create date: April 30, 2012
-- Description:	Inserts an application role
-- =============================================
CREATE PROCEDURE [dbo].[uspInsertApplicationRole]
	@IDApplication int,
	@IDRole int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    INSERT INTO Applications.ApplicationRole (IDApplication, IDRole, isActive)
    VALUES (@IDApplication, @IDRole, 1)
    
    SELECT SCOPE_IDENTITY() --return IDApplicationRole
END
GO
GRANT EXECUTE ON  [dbo].[uspInsertApplicationRole] TO [CSAdmin]
GO
