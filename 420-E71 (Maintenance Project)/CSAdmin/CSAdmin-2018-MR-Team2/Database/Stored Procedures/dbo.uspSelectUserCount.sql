SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		David Magee
-- Create date: 04-24-14
-- Description:	Count the non approved emails in .NET membership in an application
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectUserCount]

	@ApplicationName Varchar(100) = '',
	@Email Varchar(100) = ''
	
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT COUNT(UserId) 
	FROM [CSAdmin].[NetMem].[aspnet_Membership] mem
	JOIN [CSAdmin].[NetMem].[aspnet_Applications] ap ON ap.ApplicationId = mem.ApplicationId
	WHERE mem.IsApproved = 1
	AND mem.Email LIKE '%' + @Email + '%'
	AND ap.ApplicationName LIKE '%' + @ApplicationName + '%'
	
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectUserCount] TO [CSAdmin]
GO
