SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================================
-- Author:		Erica Richard
-- Create date: May 17, 2017
-- Description:	Selects the number of students for each program
--				that need to be activated in CSAdmin
-- 
-- =============================================================
CREATE PROCEDURE [dbo].[uspSelectNumDeactivateStudents]
	@AnSession	smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT COUNT(su.IDUser) 'Number of Students'
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
								  AND es.AnSession = @AnSession --current semester (must be changed in procedure)
								  AND (es.IDUniteOrg = 235 OR es.IDUniteOrg = 525)
								  );
	

END
GO
