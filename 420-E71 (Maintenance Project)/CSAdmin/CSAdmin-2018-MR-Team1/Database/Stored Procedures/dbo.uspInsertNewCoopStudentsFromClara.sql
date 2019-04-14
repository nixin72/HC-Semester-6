SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
 Author:		Kevin Brascoupe
 Create date: March 31, 2011
 Description:	Extracts new student data from Clara every year based on the given AnSession
Revision History:
Name					Date Modified		Revision   
====================	================	=======================================
Louis Cloutier			May 2, 2012			Added a call to createUsername function

====================	================	=======================================
****************************************************************************/
CREATE PROCEDURE [dbo].[uspInsertNewCoopStudentsFromClara]
	@AnSession	smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	  --PRINT 'Insert Students into user/studentuser tables.
  DECLARE @CurStudentID as int;
  DECLARE @CurStudentNom as varchar(100);
  DECLARE @CurStudentPrenom as varchar(100);
  DECLARE @CurUserID as integer;
  
  DECLARE Students CURSOR FOR
  SELECT DISTINCT e.IDEtudiant, e.nom, e.prenom
  FROM ClaraEtudiants.EtudiantSession es
  JOIN ClaraEtudiants.Etudiant e	ON es.IDEtudiant = e.IDEtudiant
  JOIN ClaraProgrammes.Programme p	ON es.IDProgramme = p.IDProgramme
  WHERE es.IDProgramme IN (SELECT ProgramID FROM eCoop.dbo.Program) 
  AND es.AnSession = @AnSession
							--select students enrolled IN current semester
  AND es.Etat > 0 --Indique l'état de l'enregistrement: 0 = Supprimé,1 = Actif
  AND e.IDEtudiant NOT IN (SELECT StudentID from eCoop.dbo.Student) 
  ORDER BY IDEtudiant
  
  OPEN Students
  
  -----CURSOR LOOP-----
  Fetch next from Students
  into @CurStudentID, @CurStudentNom, @CurStudentPrenom
  
  WHILE @@FETCH_STATUS = 0
  BEGIN
  
  SELECT IDUser 
  FROM Users.[User]
  Where LOWER(FirstName) = LOWER(@CurStudentPrenom)
  and LOWER(LastName) = LOWER(@CurStudentNom)
  
  IF @@ROWCOUNT = 0
  BEGIN
  INSERT INTO Users.[User]
           (LastName
           ,FirstName
           ,Username
           ,IsActive)
  VALUES (@CurStudentNom, @CurStudentPrenom, dbo.CreateUsername(@CurStudentPrenom,@CurStudentNom), 1)
  
  select @CurUserID = SCOPE_IDENTITY()
  from Users.[User];
  
  INSERT into Users.StudentUser(IDUser, IDEtudiant)
  VALUES(@CurUserID, @CurStudentID)
  
  INSERT into Applications.UserRole(IDUser, IDRole, IsActive)
  VALUES(@CurUserID, 5, 1)
  END
  
  Fetch next from Students
  into @CurStudentID, @CurStudentNom, @CurStudentPrenom
  
  END
  ----END CURSOR LOOP----
  
  CLOSE Students
  DEALLOCATE Students   

    --PRINT 'Insert Student rows'  
  INSERT INTO eCoop.[dbo].[Student]
           ([StudentID]
           ,[LastName]
           ,[FirstName]
           ,[StudentNumber]
           ,[Email])
SELECT DISTINCT e.IDEtudiant, e.nom, e.prenom, e.Numero7, NULL
  FROM ClaraEtudiants.EtudiantSession es
  JOIN ClaraEtudiants.Etudiant e	ON es.IDEtudiant = e.IDEtudiant
  JOIN ClaraProgrammes.Programme p	ON es.IDProgramme = p.IDProgramme
  WHERE es.IDProgramme IN (SELECT ProgramID FROM eCoop.dbo.Program) 
  AND es.AnSession = @AnSession
							--select students enrolled IN current semester
  AND es.Etat > 0 --Indique l'état de l'enregistrement: 0 = Supprimé,1 = Actif
  AND e.IDEtudiant NOT IN (SELECT StudentID from eCoop.dbo.Student) 
  ORDER BY IDEtudiant     

--PRINT 'Insert Student confirmation rows'
INSERT INTO eCoop.[dbo].[StudentConfirmation]
           ([StudentID]
           ,[CoopProgramID]
           ,[IsCanadianCitizen]
           ,[IsEligibleToWork]
           ,[ConfirmationStatusID]
           ,[DateLastModified]
           ,[DeclinedComment])
  SELECT DISTINCT e.IDEtudiant, cp.CoopProgramID, CASE WHEN IDPaysCitoyennete = 1
    THEN 'Y'
    ELSE 'N' END, 'N', 1, GETDATE(), NULL
  FROM ClaraEtudiants.Etudiant e
  JOIN ClaraEtudiants.EtudiantSession es	ON es.IDEtudiant = e.IDEtudiant
  JOIN ClaraProgrammes.Programme p			ON es.IDProgramme = p.IDProgramme
  LEFT JOIN ClaraEtudiants.Citoyennete c	ON e.IDEtudiant = c.IDEtudiant
  JOIN eCoop.[dbo].Program pr				ON pr.ProgramID = p.IDProgramme
  JOIN eCoop.[dbo].CoopProgram cp			ON cp.CoopProgramID = pr.CoopProgramID
  WHERE es.IDProgramme IN (SELECT ProgramID FROM eCoop.dbo.Program) -- only for co-op programs
  AND AnSession = @AnSession
  AND es.Etat > 0 --Indique l'état de l'enregistrement: 0 = Supprimé,1 = Actif
  AND e.IDEtudiant IN (SELECT StudentID from eCoop.dbo.Student)
  AND e.IDEtudiant + cp.CoopProgramID NOT IN (SELECT sc.StudentID + sc.CoopProgramID FROM eCoop.dbo.StudentConfirmation sc)
 
--PRINT 'Insert Student eligibility rows'
 --Need to figure out how not to exclude students who have already completed co-op
 --This will be handled by an eligibility status of 'Completed'
INSERT INTO eCoop.[dbo].[StudentEligibility]
           ([StudentConfirmationID]
           ,[YearSemester]
           ,[YearInProgram]
           ,[EligibilityStatusID]
           ) 
SELECT StudentConfirmationID
		,CASE WHEN SUBSTRING(CAST(es.AnSession AS varchar(5)),5,1) = 1 OR SUBSTRING(CAST(es.AnSession AS varchar(5)),5,1) = 2
			THEN ansession + 1
			WHEN SUBSTRING(CAST(es.AnSession AS varchar(5)),5,1) = 3
			THEN AnSession + 8
		END
		,CASE es.SPE
			WHEN 0 THEN 1
			WHEN 1 THEN 1
			WHEN 2 THEN 1
			WHEN 3 THEN 2
			WHEN 4 THEN 2
			WHEN 5 THEN 3
			WHEN 6 THEN 3
		END
		,1  --Pending students, since it is not known when the rows are created
 FROM eCoop.[dbo].Student s
 JOIN eCoop.[dbo].StudentConfirmation sc	ON s.StudentID = sc.StudentID
 JOIN eCoop.[dbo].Program p					ON sc.CoopProgramID = p.CoopProgramID
 JOIN ClaraEtudiants.EtudiantSession es ON sc.StudentID = es.IDEtudiant
											AND p.ProgramID = es.IDProgramme
WHERE es.AnSession = @AnSession 
AND es.Etat > 0 
AND CAST(CASE WHEN SUBSTRING(CAST(es.AnSession AS varchar(5)),5,1) = 1 OR SUBSTRING(CAST(es.AnSession AS varchar(5)),5,1) = 2
			THEN ansession + 1
			WHEN SUBSTRING(CAST(es.AnSession AS varchar(5)),5,1) = 3
			THEN AnSession + 8
		END AS char(5)) + CAST(StudentConfirmationID AS char(5))
	NOT IN (SELECT   CAST(YearSemester AS char(5))+ CAST(StudentConfirmationID AS char(5))FROM eCoop.[dbo].StudentEligibility se)
ORDER BY s.StudentID


END
GO
GRANT EXECUTE ON  [dbo].[uspInsertNewCoopStudentsFromClara] TO [CSAdmin]
GO
