SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Myriam Derome
-- Create date: March 1 2012
-- Description:	Activates coordinators and their coordinator role in  if they
--               exist in , were disabled and are a coordinator this semester
-- =============================================
CREATE PROCEDURE [dbo].[uspActivateCoordinators]
	-- Add the parameters for the stored procedure here
	@Semester int
AS
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
     JOIN Applications.Application a	  ON a.IDApplication = ar.IDApplication
WHERE a.Code = 'CES'
      AND r.Code = 'CO' --Coordinator Role

INSERT INTO #TempEmployee (IDEmploye, LastName, FirstName, Username)
SELECT DISTINCT c.IDEmploye
			    ,e.nom as LastName
			    ,e.prenom as FirstName
			    ,dbo.CreateUsername(prenom,nom) AS Username
      FROM ClaraEmployes.Coordonnateur c
		   JOIN ClaraEmployes.Employe e ON e.IDEmploye = c.IDEmploye
      WHERE c.AnSessionDebut <= @Semester 
		    AND (c.AnSessionFin IS NULL OR c.AnSessionFin >= @Semester)
		    AND c.IDUniteOrg = 235
		    AND c.IDEmploye IN (SELECT eu.IDEmploye
                              FROM Users.EmployeeUser eu
                                   JOIN Applications.UserRole ur ON ur.IDUser = eu.IDUser
                                   JOIN Applications.Role r ON r.IDRole = ur.IDRole
                                   JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
                                   JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
								   JOIN [Users].[User] u ON eu.IDUser = u.IDUser
                              WHERE ur.IsActive = 0 OR u.IsActive = 0
                                    AND r.Code = 'CO'
							 );
                                    
SET @employeeCursor = CURSOR FOR
	SELECT IDEmploye FROM #TempEmployee
	OPEN @employeeCursor;
	
	FETCH NEXT FROM @employeeCursor INTO @current_id;
	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		UPDATE [Users].[User] 
		SET IsActive = 1 --isActive must be true
		WHERE IDUser IN (SELECT eu.IDUser FROM Users.EmployeeUser eu WHERE IDEmploye = @current_id);

		UPDATE [Applications].[UserRole]
		SET IsActive = 1
		WHERE IDRole = @IDRole 
		AND IDUser IN (SELECT eu.IDUser FROM Users.EmployeeUser eu WHERE IDEmploye = @current_id);
		
		
		FETCH NEXT FROM @employeeCursor INTO @current_id;
	END --END WHILE

SELECT u.IDUser
	   , u.FirstName
	   , u.LastName
	   , u.IsActive As "UserActive"
	   , ur.IsActive "UserRoleActive"  
FROM [Users].[EmployeeUser] eu
	 JOIN [Users].[User] u			  ON u.IDUser = eu.IDUser
	 JOIN [Applications].[UserRole] ur ON ur.IDUser = eu.IDUser
WHERE (u.IsActive = 0 OR ur.IsActive = 0) 
	   AND ur.IDRole = @IDRole;
--Returns nothing if this is working. Test procedure by setting an IsActive to 0 for a person.

CLOSE @employeeCursor;
DEALLOCATE @employeeCursor;
DROP TABLE #TempEmployee;

END
GO
GRANT EXECUTE ON  [dbo].[uspActivateCoordinators] TO [CSAdmin]
GO
