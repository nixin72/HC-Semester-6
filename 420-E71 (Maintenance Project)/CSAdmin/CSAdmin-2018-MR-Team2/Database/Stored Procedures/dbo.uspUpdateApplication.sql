SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Louis Cloutier
-- Create date: April 30, 2012
-- Description:	Updates an application based on IDApplication
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateApplication]
	@IDApplication int,
	@Code VARCHAR(MAX),
	@Description VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    UPDATE Applications.Application
    SET Code = @Code
		,Description = @Description
	WHERE IDApplication = @IDApplication
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateApplication] TO [CSAdmin]
GO
