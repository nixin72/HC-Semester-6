SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           upsSelectAllTeachers
Description: 	Gets all the teachers specified in the search text
Author:	    	Matt Riopel
Create Date: 	Feburary 8, 2012

Param:			@AnSession = The current semester
Return:      	IDEmploye, FirstName, LastName

Revision History:
Name					Date Modified		Revision   
=======================	===================	=======================================
Renee Ghattas 			February 8, 2012	Initial Creation
Matt Riopel				April 22, 2012		Removed the Searching and the order by clauses
											to only retrieve a list of teachers.
****************************************************************************/
CREATE PROCEDURE [dbo].[uspSelectAllTeachers]
	--defaults in case nothing is passed to the parameter
	@AnSession smallint = 0
AS
BEGIN
SELECT DISTINCT e.IDEmploye, e.Nom, e.prenom
		FROM ClaraEmployes.Employe e
			JOIN ClaraGroupes.EmployeGroupe eg ON eg.IDEmploye = e.IDEmploye
			JOIN ClaraGroupes.Groupe g ON g.IDGroupe = eg.IDGroupe
		WHERE g.AnSession = @AnSession
			AND e.Etat = 1 
		UNION
		 SELECT DISTINCT e.IDEmploye, e.Nom, e.prenom
		FROM ClaraEmployes.Employe e
		Join Users.EmployeeUser eu on eu.IDEmploye = e.IDEmploye
		Join Users.[User] u on u.IDUser = eu.IDUser
		WHERE e.Etat = 1
		ORDER BY e.nom
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectAllTeachers] TO [CSAdmin]
GRANT EXECUTE ON  [dbo].[uspSelectAllTeachers] TO [eCoop]
GO
