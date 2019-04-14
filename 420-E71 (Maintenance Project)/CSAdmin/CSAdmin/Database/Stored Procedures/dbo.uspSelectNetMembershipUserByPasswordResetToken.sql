SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Gage Geneau
-- Create date: March 14, 2013
-- Description:	Selects the NetMembershipUser associated to the password reset token
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectNetMembershipUserByPasswordResetToken] 
	-- Add the parameters for the stored procedure here
	@PasswordResetToken UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT upr.NetMembershipUser AS "NetMembershipUser" FROM NetMem.aspnet_UserPasswordReset upr
	WHERE upr.PasswordResetToken = @PasswordResetToken
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectNetMembershipUserByPasswordResetToken] TO [CSAdmin]
GO
