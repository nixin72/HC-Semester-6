SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- Stored Procedure

-- =============================================
-- Author:		Myriam Derome
-- Create date: March 1 2012
-- Description:	Deactivates students and their student role in  if they
--               exist in , were enabled and are not students this semester
-- =============================================
CREATE PROCEDURE [dbo].[uspDeactivateStudents]
	-- Add the parameters for the stored procedure here
	@Semester int
AS
	DECLARE @current_id int --used to iterate through records later
	DECLARE @IDRole int -- used to get the value of the teacher role, in case it changes
	DECLARE @studentCursor CURSOR
	DECLARE @roleCursor CURSOR
	DECLARE @current_IDUserRole int

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --create a temporary table to contain this information
CREATE TABLE #TempStudent(
IDUser int PRIMARY KEY CLUSTERED,
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
     JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
WHERE a.Code = 'CES'
      AND r.Code = 'ST' --Student Role

INSERT INTO #TempStudent (IDUser, IDEtudiant, LastName, FirstName)
SELECT DISTINCT su.IDUser
	  ,su.IDEtudiant
      ,u.FirstName
      ,u.LastName
FROM Users.StudentUser su
     JOIN Applications.UserRole ur ON ur.IDUser = su.IDUser
     JOIN Applications.Role r ON r.IDRole = ur.IDRole
     JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
     JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
     JOIN [Users].[User] u ON su.IDUser = u.IDUser
WHERE ur.IsActive = 1
      AND r.Code = 'ST'
      AND su.IDEtudiant NOT IN(SELECT DISTINCT es.IDEtudiant
							FROM ClaraEtudiants.EtudiantSession es
								 JOIN ClaraEtudiants.Etudiant e ON es.IDEtudiant = e.IDEtudiant
							WHERE es.etat = 1 --This gets active students for this semester
								  AND es.AnSession = @Semester --current semester (must be changed in procedure)
								  AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
								  );

SET @studentCursor = CURSOR FOR
	SELECT IDUser FROM #TempStudent
	OPEN @studentCursor;

	FETCH NEXT FROM @studentCursor INTO @current_id;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		UPDATE [Applications].[UserRole]
		SET IsActive = 0
		WHERE IDRole = @IDRole 
		AND IDUser = @current_id;
		--Set the role to inactive, since the student is inactive.
		SET @roleCursor = CURSOR FOR
		SELECT IDUserRole FROM Applications.[UserRole] 
		WHERE IDUser = @current_id 
			  AND IsActive = 1;
		--If there are no other roles (including in other applications), deactivate user.
		OPEN @roleCursor;
		FETCH NEXT FROM @roleCursor INTO @current_IDUserRole;
			IF @@FETCH_STATUS != 0
				BEGIN
				UPDATE [Users].[User]
				SET IsActive = 0
				WHERE IDUser = @current_id;
				--Set the user to inactive, since they have no active roles.

			END --END ROLE IF
			CLOSE @roleCursor;
			DEALLOCATE @roleCursor;
		FETCH NEXT FROM @studentCursor INTO @current_id;
	END --END WHILE

SELECT DISTINCT u.IDUser
	   , u.FirstName
	   , u.LastName
	   , u.IsActive As "UserActive"
	   , ur.IsActive "UserRoleActive" 
FROM [Users].StudentUser su
	 JOIN [Users].[User] u			  ON u.IDUser = su.IDUser
	 JOIN [Applications].[UserRole] ur ON ur.IDUser = su.IDUser
WHERE ur.IsActive = 1 
	  AND ur.IDRole = @IDRole 
	  AND su.IDEtudiant NOT IN (SELECT DISTINCT es.IDEtudiant
							FROM ClaraEtudiants.EtudiantSession es
								 JOIN ClaraEtudiants.Etudiant e ON es.IDEtudiant = e.IDEtudiant
							WHERE es.etat = 1 --This gets active students for this semester
								  AND es.AnSession = @Semester --current semester (must be changed in procedure)
								  AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525));
--Returns nothing if this is working. Test procedure by setting an IsActive to 0 for a person.

CLOSE @studentCursor;
DEALLOCATE @studentCursor;
DROP TABLE #TempStudent;

END
GO
GRANT EXECUTE ON  [dbo].[uspDeactivateStudents] TO [CSAdmin]
GO
