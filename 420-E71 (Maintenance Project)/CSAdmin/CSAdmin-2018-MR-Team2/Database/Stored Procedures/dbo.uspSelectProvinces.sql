SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Myriam Derome
-- Create date: April 18, 2012
-- Description:	Selects all of the provinces from the table
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectProvinces]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IDProvince, Text FROM Resources.Province
	ORDER BY TEXT ASC
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectProvinces] TO [CSAdmin]
GO
