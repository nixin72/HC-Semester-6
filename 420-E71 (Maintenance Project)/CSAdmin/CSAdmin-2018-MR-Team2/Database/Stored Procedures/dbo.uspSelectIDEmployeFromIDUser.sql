SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Adrien Pyke
-- Create date: 2014/12/14
-- Description:	select the IDEmploye from the ID User
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectIDEmployeFromIDUser] 
	-- Add the parameters for the stored procedure here
	@IDUser int = -1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 IDEmploye FROM Users.EmployeeUser
	WHERE IDUser = @IDUser
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectIDEmployeFromIDUser] TO [CES]
GRANT EXECUTE ON  [dbo].[uspSelectIDEmployeFromIDUser] TO [CSAdmin]
GO
