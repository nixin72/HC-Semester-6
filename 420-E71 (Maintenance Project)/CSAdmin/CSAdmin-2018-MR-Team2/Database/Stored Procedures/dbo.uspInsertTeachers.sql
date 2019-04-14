SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertTeachers
Description: 	Inserts teachers and their teacher role into  if they
                do not yet exist in  and teach this semester
Author:	    	Louis Cloutier
Create Date: 	January 31, 2012

Param:			@CurrentSemester = 5 number value of current semester

Revision History:
Name			     Date Modified	  Revision   
==================== ================ =======================================
Louis Cloutier 		 February 2, 2012 Initial Creation
Louis Cloutier       February 6, 2012 Started adding logic for creating new teacher roles
****************************************************************************/
CREATE PROCEDURE [dbo].[uspInsertTeachers]
	@Semester int 
AS
BEGIN

DECLARE @current_id int = 1 --used to iterate through records later
DECLARE @last_id int --used to store last record id later
DECLARE @IDUser int
DECLARE @IDRole int -- used to get the value of the teacher role, in case it changes

--create a temporary table to contain this information
CREATE TABLE #TempEmployee(
IDTemp int IDENTITY(1,1),
IDEmploye int,
FirstName nvarchar(max),
LastName nvarchar(max),
Username nvarchar(max) )

CREATE TABLE #keys(
IDEmploye int,
IDUser int)

SELECT @IDRole = r.IDRole
FROM Applications.Role r
     JOIN Applications.ApplicationRole ar ON r.IDRole = ar.IDRole
     JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
WHERE a.Code = 'CES'
      AND r.Code = 'TE' --Teacher Role

INSERT INTO #TempEmployee (IDEmploye, LastName, FirstName, Username)
SELECT DISTINCT eg.IDEmploye
			    ,e.nom as LastName
			    ,e.prenom as FirstName
			    ,dbo.CreateUsername(prenom,nom) AS Username
FROM ClaraGroupes.EmployeGroupe eg
     JOIN ClaraGroupes.Groupe g			  ON eg.IDGroupe = g.IDGroupe
     JOIN ClaraEmployes.Employe e		  ON e.IDEmploye = eg.IDEmploye
	 JOIN ClaraInscriptions.Inscription i ON i.IDGroupe = g.IDGroupe
     --JOIN ClaraReference.UniteOrg uo	  ON uo.IDUniteOrg = i.IDUniteOrg   
     --This last JOIN does not seem to be required at the moment
WHERE g.etat = 1 --This gets active teachers for this semester
      AND g.AnSession = @Semester
      AND e.IDEmploye NOT IN (SELECT eu.IDEmploye
                              FROM Users.EmployeeUser eu
                             )
SET @last_id = SCOPE_IDENTITY() --get the last ID
IF @last_id IS NOT NULL
BEGIN
	WHILE @current_id <= @last_id
	BEGIN
		INSERT INTO [Users].[User] (LastName,FirstName,Username,IsActive)
		SELECT LastName
		      ,FirstName
		      ,Username
		      ,1 --isActive must be true
		FROM #TempEmployee
		WHERE IDTemp = @current_id
		
		SET @IDUser = SCOPE_IDENTITY() --get the latest IDUser
		
		
		INSERT INTO [Users].[EmployeeUser] (IDUser, IDEmploye)
		SELECT @IDUser
		      ,IDEmploye
		FROM #TempEmployee
		WHERE IDTemp = @current_id
		
		INSERT INTO [Applications].[UserRole] (IDUser, IDRole, IsActive)
		VALUES (@IDUser
				, @IDRole
				, 1)
				
		SET @current_id = @current_id + 1
	END --END WHILE
END --END IF

SELECT * 
FROM [Users].[EmployeeUser] eu
	 JOIN [Users].[User] u			   ON u.IDUser = eu.IDUser
	 JOIN [Applications].[UserRole] ur ON ur.IDUser = eu.IDUser

DROP TABLE #TempEmployee;
END
GO
GRANT EXECUTE ON  [dbo].[uspInsertTeachers] TO [CSAdmin]
GO
