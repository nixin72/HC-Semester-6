SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertNewTeachers
Description: 	Inserts teacher into ces if they
                do not yet exist in ces and are teachers this semester.
Author:	    	Myriam Derome
Create Date: 	March 14, 2012

Param:			@Semester = 5 number value of current semester

Revision History:
Name			     Date Modified	  Revision   
==================== ================ =======================================
Myriam Derome 		 March 14, 2012	  Initial Creation
Louis Cloutier		 April 25, 2012	  Moved to CSAdmin database
Gage Geneau			 April 19, 2012	  Added check for nulls on IDTeacherStatus - Changed the column so that it is not nullable so that teachers always appear in the manager teachers page on the coordinator side
****************************************************************************/
CREATE PROCEDURE [dbo].[uspInsertNewCESTeachers]
	@Semester int 
AS
BEGIN
DECLARE @IDTeacherStatus int

SELECT @IDTeacherStatus = IDTeacherStatus 
FROM CES.Users.TeacherStatus 
WHERE Code = 'NT';

-- If it is null set the value anyways
IF @IDTeacherStatus IS NULL
	SELECT @IDTeacherStatus = 2

--Add teachers that have other roles but not teacher
	EXECUTE uspAddMissingTeacherRole @Semester
	SELECT @@ROWCOUNT As 'Rows Affected';
--Add teachers that were no longer active teachers
	EXECUTE uspActivateTeachers @Semester
	SELECT @@ROWCOUNT As 'Rows Affected';
--Insert teachers that don't yet exist
INSERT INTO CES.[Users].Teacher (IDEmploye, LastName, FirstName, IDTeacherStatus)
SELECT DISTINCT eg.IDEmploye
			    ,e.nom as LastName
			    ,e.prenom as FirstName
			    ,@IDTeacherStatus
FROM ClaraGroupes.EmployeGroupe eg
     JOIN ClaraGroupes.Groupe g			  ON eg.IDGroupe = g.IDGroupe
     JOIN ClaraEmployes.Employe e		  ON e.IDEmploye = eg.IDEmploye
	 JOIN ClaraInscriptions.Inscription i ON i.IDGroupe = g.IDGroupe
     JOIN ClaraReference.UniteOrg uo	  ON uo.IDUniteOrg = i.IDUniteOrg
WHERE g.etat = 1 --This gets active teachers for this semester
      AND g.AnSession = @Semester
      AND e.IDEmploye NOT IN (SELECT t.IDEmploye
                              FROM CES.Users.Teacher t
                             );
  SELECT @@ROWCOUNT As 'Rows Affected';
END
GO
GRANT EXECUTE ON  [dbo].[uspInsertNewCESTeachers] TO [CSAdmin]
GO
