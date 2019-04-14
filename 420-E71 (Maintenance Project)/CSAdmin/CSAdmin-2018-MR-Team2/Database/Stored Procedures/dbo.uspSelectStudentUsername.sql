SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspSelectStudentUsername
Description: 	Gets the username of the current student
Author:	    	Louis Cloutier
Create Date: 	March 14, 2012

Param:			@IDEtudiant = The student ID to refer to
Return:      	Username

Revision History:
Name			     Date Modified	  Revision   
==================== ================ =======================================

****************************************************************************/
CREATE PROCEDURE [dbo].[uspSelectStudentUsername]
	@IDEtudiant int
AS
BEGIN
		 SELECT u.Username
		 FROM Users.StudentUser su
		      JOIN Users.[User] u		ON u.IDUser = su.IDUser
		 WHERE su.IDEtudiant = @IDEtudiant
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectStudentUsername] TO [CES]
GRANT EXECUTE ON  [dbo].[uspSelectStudentUsername] TO [CSAdmin]
GO
