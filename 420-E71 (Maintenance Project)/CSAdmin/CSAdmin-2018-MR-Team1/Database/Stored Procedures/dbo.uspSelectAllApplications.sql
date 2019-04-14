SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Louis Cloutier
-- Create date: April 30, 2012
-- Description:	Selects all the applications in CSAdmin
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectAllApplications]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    SELECT	IDApplication
			,Code
			,Description
    FROM Applications.Application
    ORDER BY Description
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectAllApplications] TO [CSAdmin]
GO
