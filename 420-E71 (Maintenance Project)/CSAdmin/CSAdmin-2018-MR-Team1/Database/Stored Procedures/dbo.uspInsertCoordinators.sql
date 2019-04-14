
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertCoordinators
Description: 	Inserts coordinators and their coordinator role into  if they
                do not yet exist in  and are coordinators this semester.
Author:	    	Myriam Derome
Create Date: 	March 1, 2012

Param:			@CurrentSemester = 5 number value of current semester

Revision History:
Name			     Date Modified	  Revision   
==================== ================ =======================================
Myriam Derome 		 March 1, 2012	  Initial Creation

****************************************************************************/
CREATE PROCEDURE [dbo].[uspInsertCoordinators]
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


SELECT @IDRole = r.IDRole
FROM Applications.Role r
     JOIN Applications.ApplicationRole ar ON r.IDRole = ar.IDRole
     JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
WHERE a.Code = 'CES'
      AND r.Code = 'CO' --Coordinator Role

INSERT INTO #TempEmployee (IDEmploye, LastName, FirstName, Username)
SELECT DISTINCT c.IDEmploye
			    ,e.nom as LastName
			    ,e.prenom as FirstName
			    ,dbo.CreateUsername(prenom,nom) AS Username
FROM ClaraEmployes.Coordonnateur c
     JOIN ClaraEmployes.Employe e ON e.IDEmploye = c.IDEmploye
      AND (c.AnSessionDebut IS NULL OR c.AnSessionDebut <= @Semester) 
      AND (c.AnSessionFin IS NULL OR c.AnSessionFin >= @Semester)
      AND c.IDUniteOrg = 235
      AND c.IDEmploye NOT IN (SELECT eu.IDEmploye
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

-- Make user coordinator of the application if they are not
INSERT INTO [Applications].[UserRole] (IDUser, IDRole, IsActive)
SELECT IDUser, IDRole = @IDRole, IsActive = 1
FROM Users.EmployeeUser
WHERE IDEmploye IN
(
SELECT DISTINCT c.IDEmploye 'Number of Coordinators'
	FROM ClaraEmployes.Coordonnateur c
		JOIN ClaraEmployes.Employe e ON e.IDEmploye = c.IDEmploye
		AND (c.AnSessionDebut IS NULL OR c.AnSessionDebut <= @Semester)
		AND (c.AnSessionFin IS NULL OR c.AnSessionFin >= @Semester)
		AND (c.IDUniteOrg = 235 OR c.IDUniteOrg = 525)
		AND (
			--Check if they exist in CSAdmin at all
			c.IDEmploye NOT IN	(	SELECT eu.IDEmploye
									FROM Users.EmployeeUser eu
								)
			--check if they exist in CSAdmin but not as a coordinator
			OR c.IDEmploye NOT IN	(
									SELECT eu.IDEmploye
									FROM Users.EmployeeUser eu
											JOIN Applications.UserRole ur			ON ur.IDUser = eu.IDUser
											JOIN Applications.Role r				ON r.IDRole = ur.IDRole
											JOIN Applications.ApplicationRole ar	ON ar.IDRole = r.IDRole
											JOIN Applications.Application a			ON a.IDApplication = ar.IDApplication
									WHERE a.Code = 'CES'
											AND r.Code = 'CO'
									)
        )
        )

DROP TABLE #TempEmployee;
END

GO

GRANT EXECUTE ON  [dbo].[uspInsertCoordinators] TO [CSAdmin]
GO
