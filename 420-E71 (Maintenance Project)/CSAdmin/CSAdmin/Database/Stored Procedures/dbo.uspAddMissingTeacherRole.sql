SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Myriam Derome
-- Create date: February 23, 2012
-- Description:	Adds teachers roles in  if they
--               exist in , are teachers and do not have a teacher role.
-- =============================================
CREATE PROCEDURE [dbo].[uspAddMissingTeacherRole]
	-- Add the parameters for the stored procedure here
	@Semester int
AS
	DECLARE @IDUser int--used for inserts
	DECLARE @current_id int --used to iterate through records later
	DECLARE @IDRole int -- used to get the value of the teacher role, in case it changes
	DECLARE @employeeCursor CURSOR
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

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
      AND r.Code = 'TE' --Teacher Role

INSERT INTO #TempEmployee (IDEmploye, LastName, FirstName, Username)
SELECT DISTINCT eg.IDEmploye
			    ,e.nom as LastName
			    ,e.prenom as FirstName
			    ,dbo.CreateUsername(prenom,nom) AS Username
FROM ClaraGroupes.EmployeGroupe eg
     JOIN ClaraGroupes.Groupe g ON eg.IDGroupe = g.IDGroupe
     JOIN ClaraEmployes.Employe e ON e.IDEmploye = eg.IDEmploye
	 JOIN ClaraInscriptions.Inscription i ON i.IDGroupe = g.IDGroupe
     JOIN ClaraReference.UniteOrg uo ON uo.IDUniteOrg = i.IDUniteOrg
WHERE g.etat = 1 --This gets active teachers for this semester
      AND g.AnSession = @Semester --current semester (must be changed in procedure)
      AND e.IDEmploye NOT IN (SELECT eu.IDEmploye
                              FROM Users.EmployeeUser eu
                                   JOIN Applications.UserRole ur ON ur.IDUser = eu.IDUser
                                   JOIN Applications.Role r ON r.IDRole = ur.IDRole
                                   JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
                                   JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
                              WHERE r.Code = 'TE'
                             );
	SET @employeeCursor = CURSOR FOR
	SELECT IDEmploye FROM #TempEmployee
	OPEN @employeeCursor;
	
	FETCH NEXT FROM @employeeCursor INTO @current_id;
	WHILE @@FETCH_STATUS = 0
	BEGIN
	SET @IDUser = (SELECT IDUser FROM Users.EmployeeUser WHERE IDEmploye = @current_id);
	If (@IDUser IS NOT NULL)
	BEGIN
	INSERT INTO Applications.UserRole (IDUser, IDRole, IsActive)
	VALUES
	(@IDUser
	,@IDRole
	,1)
	END--END IF
	
	FETCH NEXT FROM @employeeCursor INTO @current_id;
	
	END --END WHILE
	
CLOSE @employeeCursor;
DEALLOCATE @employeeCursor;
	SELECT * FROM #TempEmployee;

DROP TABLE #TempEmployee;
END
GO
GRANT EXECUTE ON  [dbo].[uspAddMissingTeacherRole] TO [CSAdmin]
GO
