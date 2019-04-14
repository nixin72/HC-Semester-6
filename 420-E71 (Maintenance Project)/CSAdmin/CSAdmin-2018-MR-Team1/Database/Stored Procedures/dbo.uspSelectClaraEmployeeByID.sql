SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/********************************************************************************************
Name:           uspInsertUserRole
Author:			Cedric Burgins
Create date:	March 31, 2011
Description:	This stored procedure returns an Employees from the Clara database

=============================================
Revision History:
Name						Date Modified		Revision   
=========================	==================	============================================
Matt Riopel 				May 07, 2012		Changed the access to the Clara database
												to use the synonym in CSAdmin
********************************************************************************************/

CREATE PROCEDURE [dbo].[uspSelectClaraEmployeeByID]
	-- Add the parameters for the stored procedure here
	@EmployeeID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IDEmploye, Nom, Prenom
	FROM ClaraEmployes.Employe
	WHERE IDEmploye = @EmployeeID
	ORDER BY IDEmploye
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectClaraEmployeeByID] TO [CSAdmin]
GRANT EXECUTE ON  [dbo].[uspSelectClaraEmployeeByID] TO [eCoop]
GO
