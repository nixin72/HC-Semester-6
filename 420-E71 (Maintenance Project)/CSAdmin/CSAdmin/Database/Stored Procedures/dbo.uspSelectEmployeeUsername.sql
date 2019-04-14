SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspSelectEmployeeUsername
Description: 	Gets the username of the current employee
Author:	    	Renee Ghattas
Create Date: 	March 14, 2012

Param:			@IDEtudiant = The employee ID to refer to
Return:      	Username

Revision History:
Name			     Date Modified	  Revision   
==================== ================ =======================================

****************************************************************************/
CREATE PROCEDURE [dbo].[uspSelectEmployeeUsername]
	@IDEmploye int
AS
BEGIN
		 SELECT u.Username
		 FROM Users.EmployeeUser eu
		      JOIN Users.[User] u		ON u.IDUser = eu.IDUser
		 WHERE eu.IDEmploye = @IDEmploye
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectEmployeeUsername] TO [CES]
GRANT EXECUTE ON  [dbo].[uspSelectEmployeeUsername] TO [CSAdmin]
GO
