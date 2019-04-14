SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Louis Cloutier
-- Create date: April 30, 2012
-- Description:	Inserts an Application
-- =============================================
CREATE PROCEDURE [dbo].[uspInsertApplication]
	@Code VARCHAR(MAX),
	@Description VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    INSERT INTO Applications.Application (Code, Description)
    VALUES (@Code, @Description)
    
    SELECT SCOPE_IDENTITY() --return IDApplication
END
GO
GRANT EXECUTE ON  [dbo].[uspInsertApplication] TO [CSAdmin]
GO
