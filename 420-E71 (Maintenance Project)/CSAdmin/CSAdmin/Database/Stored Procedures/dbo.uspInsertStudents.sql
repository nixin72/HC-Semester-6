SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertStudents
Description: 	Inserts students and their student role into  if they
                do not yet exist in  and are students this semester.
Author:	    	Myriam Derome
Create Date: 	March 1, 2012

Param:			@CurrentSemester = 5 number value of current semester

Revision History:
Name			     Date Modified	  Revision   
==================== ================ =======================================
Myriam Derome 		 March 1, 2012	  Initial Creation
Susan Turanyi		 Septr 29, 2016	  Changed the username to be the student number
									  because of changes made by Computer Services
****************************************************************************/
CREATE PROCEDURE [dbo].[uspInsertStudents]
	@Semester int 
AS
BEGIN

DECLARE @current_id int = 1 --used to iterate through records later
DECLARE @last_id int --used to store last record id later
DECLARE @IDUser int
DECLARE @IDRole int -- used to get the value of the teacher role, in case it changes

--create a temporary table to contain this information
CREATE TABLE #TempStudent(
IDTemp int IDENTITY(1,1),
IDEtudiant int,
FirstName nvarchar(max),
LastName nvarchar(max),
Username nvarchar(max) )

CREATE TABLE #keys(
IDEtudiant int,
IDUser int)

SELECT @IDRole = r.IDRole
FROM Applications.Role r
     JOIN Applications.ApplicationRole ar ON r.IDRole = ar.IDRole
     JOIN Applications.Application a	  ON a.IDApplication = ar.IDApplication
WHERE a.Code = 'CES'
      AND r.Code = 'ST' --Coordinator Role

INSERT INTO #TempStudent (IDEtudiant, LastName, FirstName, Username)
SELECT DISTINCT es.IDEtudiant
			    ,e.nom as LastName
			    ,e.prenom as FirstName
			    ,e.Numero7 AS Username
FROM ClaraEtudiants.EtudiantSession es
     JOIN ClaraEtudiants.Etudiant e ON es.IDEtudiant = e.IDEtudiant
WHERE es.etat = 1 --This gets active students for this semester
      AND es.AnSession = @Semester --current semester (must be changed in procedure)
      AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
      AND e.IDEtudiant NOT IN (SELECT su.IDEtudiant
                              FROM Users.StudentUser su
                             );
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
		FROM #TempStudent
		WHERE IDTemp = @current_id
		
		SET @IDUser = SCOPE_IDENTITY() --get the latest IDUser
		
		
		INSERT INTO [Users].StudentUser(IDUser, IDEtudiant)
		SELECT @IDUser
		      ,IDEtudiant
		FROM #TempStudent
		WHERE IDTemp = @current_id
		
		INSERT INTO [Applications].[UserRole] (IDUser, IDRole, IsActive)
		VALUES (@IDUser
		, @IDRole
		, 1)
		
		SET @current_id = @current_id + 1
	END --END WHILE
END --END IF

SELECT * 
FROM [Users].[StudentUser] su
	 JOIN [Users].[User] u			  ON u.IDUser = su.IDUser
	 JOIN [Applications].[UserRole] ur ON ur.IDUser = su.IDUser
WHERE ur.IDRole = @IDRole

DROP TABLE #TempStudent;
END
GO
GRANT EXECUTE ON  [dbo].[uspInsertStudents] TO [CSAdmin]
GO
