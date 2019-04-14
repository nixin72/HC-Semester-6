SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertNewStudents
Description: 	Inserts students into ces if they
                do not yet exist in ces and are students this semester.
Author:	    	Myriam Derome
Create Date: 	March 14, 2012

Param:			@Semester = 5 number value of current semester

Revision History:
Name			     Date Modified	  Revision   
==================== ================ =======================================
Myriam Derome 		 March 14, 2012	  Initial Creation
Louis Cloutier		 April 25, 2012	  Moved to CSAdmin database
****************************************************************************/
CREATE PROCEDURE [dbo].[uspInsertNewCESStudents]
	@Semester int 
AS
BEGIN


--Add students that have other roles but not teacher
	EXECUTE uspAddMissingStudentRole @Semester
	SELECT @@ROWCOUNT As 'Rows Affected';
--Add students that were no longer active teachers
	EXECUTE uspActivatestudents @Semester
	SELECT @@ROWCOUNT As 'Rows Affected';
	
--Insert students that did not exist in the system yet
INSERT INTO CES.[Users].Student (IDEtudiant)
SELECT DISTINCT es.IDEtudiant
FROM ClaraEtudiants.EtudiantSession es
WHERE es.etat = 1 --This gets active students for this semester
      AND es.AnSession = @Semester --current semester
      AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
      AND es.IDEtudiant NOT IN (SELECT s.IDEtudiant
                              FROM CES.[Users].Student s
                             );
SELECT @@ROWCOUNT As 'Rows Affected';
END
GO
GRANT EXECUTE ON  [dbo].[uspInsertNewCESStudents] TO [CSAdmin]
GO
