SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- Stored Procedure

-- =============================================
-- Author:		Myriam Derome
-- Create date: March 1 2012
-- Description:	Deactivates coordinators and their coordinator role in  if they
--               exist in , were enabled and are not coordinators this semester
-- =============================================
CREATE PROCEDURE [dbo].[uspDeactivateCoordinators]
	-- Add the parameters for the stored procedure here
	@Semester int
AS
	DECLARE @current_id int --used to iterate through records later
	DECLARE @IDRole int -- used to get the value of the teacher role, in case it changes
	DECLARE @employeeCursor CURSOR
	DECLARE @roleCursor CURSOR
	DECLARE @current_IDUserRole int

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --create a temporary table to contain this information
CREATE TABLE #TempEmployee(
IDUser int PRIMARY KEY CLUSTERED,
IDEmploye int,
FirstName nvarchar(max),
LastName nvarchar(max),
Username nvarchar(max) )

SELECT @IDRole = r.IDRole
FROM Applications.Role r
     JOIN Applications.ApplicationRole ar ON r.IDRole = ar.IDRole
     JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
WHERE a.Code = 'CES'
      AND r.Code = 'CO' --Teacher Role

INSERT INTO #TempEmployee (IDUser, IDEmploye, LastName, FirstName) 
SELECT DISTINCT u.IDUser
	  ,eu.IDEmploye
      ,u.FirstName
      ,u.LastName
FROM Users.EmployeeUser eu
     JOIN Applications.UserRole ur ON ur.IDUser = eu.IDUser
     JOIN Applications.Role r ON r.IDRole = ur.IDRole
     JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
     JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
     JOIN [Users].[User] u ON eu.IDUser = u.IDUser
WHERE ur.IsActive = 1
      AND r.Code = 'CO'
      AND eu.IDEmploye NOT IN (SELECT DISTINCT c.IDEmploye 
							  FROM ClaraEmployes.Coordonnateur c
							JOIN ClaraEmployes.Employe e ON e.IDEmploye = c.IDEmploye
							--AND c.AnSessionDebut <= @Semester --current semester (must be changed in procedure)
							--AND (c.AnSessionFin IS NULL OR c.AnSessionFin >= @Semester)
							--current semester (CHANGE BOTH)
							AND c.IDUniteOrg = 235
							);

SET @employeeCursor = CURSOR FOR
	SELECT IDUser FROM #TempEmployee
	OPEN @employeeCursor;

	FETCH NEXT FROM @employeeCursor INTO @current_id;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		UPDATE [Applications].[UserRole]
		SET IsActive = 0
		WHERE IDRole = @IDRole 
		AND IDUser = @current_id;
		--Set the role to inactive, since the teacher is inactive.
		SET @roleCursor = CURSOR FOR
		SELECT IDUserRole FROM Applications.[UserRole] 
		WHERE IDUser = @current_id AND IsActive = 1;
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
		FETCH NEXT FROM @employeeCursor INTO @current_id;
	END --END WHILE

SELECT u.IDUser
	   , u.FirstName
	   , u.LastName
	   , u.IsActive As "UserActive"
	   , ur.IsActive "UserRoleActive" 
FROM [Users].[EmployeeUser] eu
	 JOIN [Users].[User] u			   ON u.IDUser = eu.IDUser
	 JOIN [Applications].[UserRole] ur ON ur.IDUser = eu.IDUser
WHERE ur.IsActive = 1 AND ur.IDRole = @IDRole AND eu.IDEmploye NOT IN 
	(SELECT DISTINCT c.IDEmploye 
	 FROM ClaraEmployes.Coordonnateur c
	 JOIN ClaraEmployes.Employe e ON e.IDEmploye = c.IDEmploye
	 WHERE 
			--(c.AnSessionDebut IS NULL OR c.AnSessionDebut <= @Semester) 
			--AND (c.AnSessionFin IS NULL OR c.AnSessionFin >= @Semester)
			--AND 
			c.IDUniteOrg = 235);
--Returns nothing if this is working. Test procedure by setting an IsActive to 0 for a person.

CLOSE @employeeCursor;
DEALLOCATE @employeeCursor;
DROP TABLE #TempEmployee;

END
GO
GRANT EXECUTE ON  [dbo].[uspDeactivateCoordinators] TO [CSAdmin]
GO
