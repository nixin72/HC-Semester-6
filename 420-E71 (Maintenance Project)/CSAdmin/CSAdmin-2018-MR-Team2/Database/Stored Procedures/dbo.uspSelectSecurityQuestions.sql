SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Myriam Derome
-- Create date: April 18, 2012
-- Description:	Selects all of the security questions from the security
-- questions table.
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectSecurityQuestions] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  IDSecurityQuestion, Text
	FROM Resources.SecurityQuestions
	ORDER BY Text ASC
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectSecurityQuestions] TO [CSAdmin]
GO
