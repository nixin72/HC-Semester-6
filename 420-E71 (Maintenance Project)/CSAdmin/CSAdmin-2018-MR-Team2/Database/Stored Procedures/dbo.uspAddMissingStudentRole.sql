SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- Batch submitted through debugger: SQLQuery53.sql|7|0|C:\Users\mderome\AppData\Local\Temp\~vs848F.sql
-- =============================================
-- Author:		Myriam Derome
-- Create date: March 1, 2012
-- Description:	Adds student roles in  if they
--               exist in , are students and do not have a student role.
-- =============================================
CREATE PROCEDURE [dbo].[uspAddMissingStudentRole]
	-- Add the parameters for the stored procedure here
	@Semester int
AS
	DECLARE @IDUser int--used for inserts
	DECLARE @current_id int --used to iterate through records later
	DECLARE @IDRole int -- used to get the value of the teacher role, in case it changes
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
     JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
WHERE a.Code = 'CES'
      AND r.Code = 'ST' --Student Role

INSERT INTO #TempStudent (IDEtudiant, LastName, FirstName, Username)
SELECT DISTINCT es.IDEtudiant
			    ,e.nom as LastName
			    ,e.prenom as FirstName
			    ,dbo.CreateUsername(prenom,nom) AS Username
FROM ClaraEtudiants.EtudiantSession es
     JOIN ClaraEtudiants.Etudiant e ON es.IDEtudiant = e.IDEtudiant
WHERE es.etat = 1 --This gets active students for this semester
      AND es.AnSession = @Semester --current semester (must be changed in procedure)
      AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
      AND e.IDEtudiant NOT IN (SELECT su.IDEtudiant
                              FROM Users.StudentUser su
                                   JOIN Applications.UserRole ur ON ur.IDUser = su.IDUser
                                   JOIN Applications.Role r ON r.IDRole = ur.IDRole
                                   JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
                                   JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
                              WHERE r.Code = 'ST'
                             );
	SET @studentCursor = CURSOR FOR
	SELECT IDEtudiant FROM #TempStudent;
	OPEN @studentCursor;
	
	FETCH NEXT FROM @studentCursor INTO @current_id;
		WHILE @@FETCH_STATUS = 0
		BEGIN
		SET @IDUser = (SELECT IDUser FROM Users.StudentUser WHERE IDEtudiant = @current_id);
			If (@IDUser IS NOT NULL)
			BEGIN
			INSERT INTO Applications.UserRole (IDUser, IDRole, IsActive)
			VALUES
			(@IDUser
			,@IDRole
			,1);
			END--END IF
		
		FETCH NEXT FROM @studentCursor INTO @current_id;
		
		END --END WHILE
	
CLOSE @studentCursor;
DEALLOCATE @studentCursor;
	SELECT u.IDUser
		   , u.FirstName
		   , u.LastName 
FROM [Users].[User] u
WHERE u.IDUser NOT IN (
SELECT u.IDUser 
FROM [Users].StudentUser su
JOIN [Users].[User] u			  ON u.IDUser = su.IDUser
JOIN [Applications].[UserRole] ur ON ur.IDUser = su.IDUser 
WHERE ur.IDRole = @IDRole
);
--returns no students when everything works

DROP TABLE #TempStudent;
END
GO
GRANT EXECUTE ON  [dbo].[uspAddMissingStudentRole] TO [CSAdmin]
GO
