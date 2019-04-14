SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Gage Geneau
-- Create date: March 14, 2013
-- Description:	Selects a users NetMembershipUser based on their email
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectNetMembershipUserByEmail]
	-- Add the parameters for the stored procedure here
	@email VARCHAR(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT m.UserId AS "NetMembershipUser" FROM NetMem.aspnet_Membership m
	WHERE m.LoweredEmail = LOWER(@email)
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectNetMembershipUserByEmail] TO [CSAdmin]
GO
