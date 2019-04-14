SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Gage Geneau
-- Create date: February 21, 2013
-- Description:	Update Alumni email (their username)
-- Returns:		Old Email, used to change their email in the Alumni database if the contact email is the same as their original account email
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateAlumniEmail] 
	-- Add the parameters for the stored procedure here
	@ApplicationName varchar(256),
	@NetMembershipUser UNIQUEIDENTIFIER,
	@NewEmail varchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @ApplicationId UNIQUEIDENTIFIER, @OldEmail VARCHAR(256)

	SELECT @ApplicationId = a.ApplicationId, @OldEmail = u.UserName
	FROM   NetMem.aspnet_Users u, NetMem.aspnet_Applications a
	WHERE u.UserId = @NetMembershipUser AND
		 u.ApplicationId = a.ApplicationId AND
		 LOWER(@ApplicationName) = a.LoweredApplicationName

	-- Change the username
	UPDATE NetMem.aspnet_Users SET
		UserName = @NewEmail,
		LoweredUserName = LOWER(@NewEmail)
	WHERE UserId = @NetMembershipUser AND ApplicationId = @ApplicationId
	
	-- Change the email
	UPDATE NetMem.aspnet_Membership SET
		Email = @NewEmail,
		LoweredEmail = LOWER(@NewEmail)
	WHERE UserId = @NetMembershipUser AND ApplicationId = @ApplicationId
	
	-- Return the old email
	SELECT @OldEmail AS "OldEmail"
	
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateAlumniEmail] TO [Alumni]
GRANT EXECUTE ON  [dbo].[uspUpdateAlumniEmail] TO [CSAdmin]
GO
