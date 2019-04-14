SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Myriam Derome
-- Create date: March 14 2012
-- Description:	Deletes students and their student evaluation rows in CES if they
--              exist in CES and are not students this semester
-- =============================================
CREATE PROCEDURE [dbo].[uspDeleteCESStudents]
	-- Add the parameters for the stored procedure here
	@Semester int
AS
	DECLARE @current_id int --used to iterate through records later
	DECLARE @studentCursor CURSOR
	
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --create a temporary table to contain this information
CREATE TABLE #TempStudent(
IDEtudiant int PRIMARY KEY CLUSTERED);

INSERT INTO #TempStudent (IDEtudiant)
SELECT s.IDEtudiant
FROM CES.Users.Student s
WHERE  s.IDEtudiant NOT IN(SELECT DISTINCT es.IDEtudiant
							FROM ClaraEtudiants.EtudiantSession es
								 JOIN ClaraEtudiants.Etudiant e ON es.IDEtudiant = e.IDEtudiant
							WHERE es.etat = 1 --This gets active students for this semester
								  AND es.AnSession = @Semester --current semester (must be changed in procedure)
								  AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
								  );
                                    
SET @studentCursor = CURSOR FOR
	SELECT IDEtudiant FROM #TempStudent
	OPEN @studentCursor;
	
	FETCH NEXT FROM @studentCursor INTO @current_id;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		DELETE FROM CES.Evaluations.StudentEvaluation
		WHERE IDEtudiant = @current_id;
	
		DELETE FROM CES.[Users].Student
		WHERE IDEtudiant = @current_id;
		
		FETCH NEXT FROM @studentCursor INTO @current_id;
	END --END WHILE

CLOSE @studentCursor;
DEALLOCATE @studentCursor;
DROP TABLE #TempStudent;
SELECT s.IDEtudiant
FROM CES.[Users].Student s;

END
GO
GRANT EXECUTE ON  [dbo].[uspDeleteCESStudents] TO [CSAdmin]
GO
