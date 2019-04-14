SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Gage Geneau
-- Create date: March 13, 2013
-- Description:	Adds a row to the userpasswordreset table to authorize and authenticate a user resetting their password
-- =============================================
CREATE PROCEDURE [dbo].[aspnet_Membership_InsertUserPasswordReset] 
	-- Add the parameters for the stored procedure here
	@PasswordResetToken UNIQUEIDENTIFIER,
	@PasswordResetExpiration DATETIME,
	@NetMembershipUser UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO CSAdmin.NetMem.aspnet_UserPasswordReset
	VALUES (@PasswordResetToken, @PasswordResetExpiration, @NetMembershipUser)
END
GO
GRANT EXECUTE ON  [dbo].[aspnet_Membership_InsertUserPasswordReset] TO [CSAdmin]
GO
