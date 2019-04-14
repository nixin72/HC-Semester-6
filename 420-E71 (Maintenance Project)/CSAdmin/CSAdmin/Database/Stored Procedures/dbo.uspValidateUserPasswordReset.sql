SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Gage Geneau
-- Create date: March 14, 2013
-- Description:	Validates the users link to reset their forgotton password
-- =============================================
CREATE PROCEDURE [dbo].[uspValidateUserPasswordReset] 
	-- Add the parameters for the stored procedure here
	@PasswordResetToken UNIQUEIDENTIFIER, 
	@PasswordExpirationDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) AS "validation" FROM NetMem.aspnet_UserPasswordReset upr
	WHERE upr.PasswordResetToken = @PasswordResetToken
	AND upr.PasswordResetExpiration > @PasswordExpirationDate
END
GO
GRANT EXECUTE ON  [dbo].[uspValidateUserPasswordReset] TO [CSAdmin]
GO
