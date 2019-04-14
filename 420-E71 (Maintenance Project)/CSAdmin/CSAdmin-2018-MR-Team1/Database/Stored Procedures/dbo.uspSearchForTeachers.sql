SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           upsSearchForTeachers
Description: 	Gets all the teachers specified in the search text
Author:	    	Renee Ghattas
Create Date: 	Feburary 8, 2012

Param:			@AnSession = The current semester
				@SearchText = The prefix of teachers names
Return:      	FirstName, LastName

Revision History:
Name			     Date Modified	  Revision   
==================== ================ =======================================
Renee Ghattas 		 February 8, 2012 Initial Creation
****************************************************************************/
CREATE PROCEDURE [dbo].[uspSearchForTeachers]
	--defaults in case nothing is passed to the parameter
	@AnSession smallint = 0,
	@SearchText varchar(max) = ''
AS
BEGIN
	 SELECT DISTINCT e.IDEmploye, e.Nom, e.prenom
		FROM ClaraEmployes.Employe e
			JOIN ClaraGroupes.EmployeGroupe eg ON eg.IDEmploye = e.IDEmploye
			JOIN ClaraGroupes.Groupe g ON g.IDGroupe = eg.IDGroupe
		WHERE g.AnSession = @AnSession
			AND e.Etat = 1 
			AND UPPER(e.nomPrenom) LIKE '%' + UPPER(@SearchText) + '%'
		UNION
		 SELECT DISTINCT e.IDEmploye, e.Nom, e.prenom
		FROM ClaraEmployes.Employe e
		Join Users.EmployeeUser eu on eu.IDEmploye = e.IDEmploye
		Join Users.[User] u on u.IDUser = eu.IDUser
		JOIN Applications.UserRole ur on ur.IDUser = u.IDUser
		JOIN Applications.Role r on r.IDRole = ur.IDRole
		WHERE r.Code = 'CEC'
		and ur.IsActive = 1
		AND UPPER(e.nomPrenom) LIKE '%' + UPPER(@SearchText) + '%'
		AND e.Etat = 1
		ORDER BY e.nom
		
END
GO
GRANT EXECUTE ON  [dbo].[uspSearchForTeachers] TO [CES]
GRANT EXECUTE ON  [dbo].[uspSearchForTeachers] TO [CSAdmin]
GO
