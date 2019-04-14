SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Myriam Derome
-- Create date: March 1 2012
-- Description:	Activates students and their student roles in  if they
--               exist in , were disabled and are a student this semester
-- =============================================
CREATE PROCEDURE [dbo].[uspActivateStudents]
	-- Add the parameters for the stored procedure here
	@Semester int
AS
	DECLARE @current_id int --used to iterate through records later
	DECLARE @IDRole int -- used to get the value of the student role, in case it changes
	DECLARE @studentCursor CURSOR
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

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
      AND r.Code = 'ST' --Student Role

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
      AND e.IDEtudiant IN (SELECT su.IDEtudiant
                              FROM Users.StudentUser su
                                   JOIN Applications.UserRole ur ON ur.IDUser = su.IDUser
                                   JOIN Applications.Role r ON r.IDRole = ur.IDRole
                                   JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
                                   JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
                              JOIN [Users].[User] u ON u.IDUser = su.IDUser
                              WHERE ur.IsActive = 0 OR u.IsActive = 0
                                    AND r.Code = 'ST'
							 );
                                    
SET @studentCursor = CURSOR FOR
	SELECT IDEtudiant FROM #TempStudent
	OPEN @studentCursor;
	
	FETCH NEXT FROM @studentCursor INTO @current_id;
	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		UPDATE [Users].[User] 
		SET IsActive = 1 --isActive must be true
		WHERE IDUser IN (SELECT su.IDUser FROM Users.StudentUser su WHERE IDEtudiant = @current_id);

		UPDATE [Applications].[UserRole]
		SET IsActive = 1
		WHERE IDRole = @IDRole 
		AND IDUser IN (SELECT su.IDUser FROM Users.StudentUser su WHERE IDEtudiant = @current_id);
		
		
		FETCH NEXT FROM @studentCursor INTO @current_id;
	END --END WHILE

SELECT u.IDUser
	   , u.FirstName
	   , u.LastName
	   , u.IsActive As "UserActive"
	   , ur.IsActive "UserRoleActive" 
FROM [Users].StudentUser su
	 JOIN [Users].[User] u			   ON u.IDUser = su.IDUser
	 JOIN [Applications].[UserRole] ur ON ur.IDUser = su.IDUser
WHERE (u.IsActive = 0 OR ur.IsActive = 0) 
	   AND ur.IDRole = @IDRole;
--Returns nothing if this is working. Test procedure by setting an IsActive to 0 for a person.

CLOSE @studentCursor;
DEALLOCATE @studentCursor;
DROP TABLE #TempStudent;

END
GO
GRANT EXECUTE ON  [dbo].[uspActivateStudents] TO [CSAdmin]
GO
