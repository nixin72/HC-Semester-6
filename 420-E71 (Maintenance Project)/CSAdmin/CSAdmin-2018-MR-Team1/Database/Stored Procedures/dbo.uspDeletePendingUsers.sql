SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Adrien Pyke
-- Create date: 2014/04/29
-- Description:	delete users that are awaiting approval
-- =============================================
CREATE PROCEDURE [dbo].[uspDeletePendingUsers] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DELETE FROM RAC.dbo.RAC_Applicant WHERE email NOT IN (SELECT Email FROM CSAdmin.NetMem.aspnet_Membership WHERE IsApproved = 1)
	DELETE FROM Alumni.Alumni.Account WHERE NetMembershipUser NOT IN (SELECT UserId FROM CSAdmin.NetMem.aspnet_Membership WHERE IsApproved = 1)
    DELETE FROM CSAdmin.NetMem.aspnet_Membership WHERE IsApproved = 0
END
GO
GRANT EXECUTE ON  [dbo].[uspDeletePendingUsers] TO [CSAdmin]
GO
