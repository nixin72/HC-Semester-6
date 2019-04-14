SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		David Magee
-- Create date: 04-28-2014
-- Description:	Deletes inacvite users with a specific username and application
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteInactiveUserByUserName]
	
	@ApplicationName Varchar(100) = '',
	@UserName Varchar(100) = ''
	
AS
BEGIN
Declare @UserId Varchar(100), @Email Varchar(100)
	SET NOCOUNT ON;

	SELECT @UserId = u.UserId, @Email = Email
	FROM [CSAdmin].[NetMem].[aspnet_Users] u
	JOIN [CSAdmin].[NetMem].[aspnet_Applications] ap ON ap.ApplicationId = u.ApplicationId
	JOIN [CSAdmin].[NetMem].[aspnet_Membership] mem ON u.UserId = mem.UserId
	WHERE UserName = @UserName
	AND ap.ApplicationName  LIKE '%' + @ApplicationName + '%'
	
	Select @UserId, @Email
	
	DELETE FROM [CSAdmin].[NetMem].[aspnet_UsersInRoles] WHERE UserId = @UserId
	DELETE FROM [CSAdmin].[NetMem].[aspnet_Membership] WHERE UserId = @UserId AND IsApproved = 0
	
	IF @@ROWCOUNT > 0
	BEGIN
		
		DELETE FROM [CSAdmin].[NetMem].[aspnet_Users] WHERE UserId = @UserId
		
		IF '/AlumniShowcase' LIKE '%' + @ApplicationName + '%'
		BEGIN
			DELETE FROM [Alumni].[Alumni].[Account] WHERE NetMembershipUser = @UserId
		END
		
		IF '/RAC' LIKE '%' + @ApplicationName + '%'
		BEGIN
			DELETE FROM [RAC].[dbo].[RAC_Applicant] WHERE Email = @Email
		END
		
	END
		

END
GO
GRANT EXECUTE ON  [dbo].[uspDeleteInactiveUserByUserName] TO [CSAdmin]
GO
