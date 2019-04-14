SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- Stored Procedure

-- Batch submitted through debugger: SQLQuery53.sql|7|0|C:\Users\mderome\AppData\Local\Temp\~vs848F.sql
-- =============================================
-- Author:		Myriam Derome
-- Create date: March 1, 2012
-- Description:	Adds coordinators roles in  if they
--               exist in , are coordinators and do not have a coordinator role.
-- =============================================
CREATE PROCEDURE [dbo].[uspAddMissingCoordinatorRole]
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
      AND r.Code = 'CO' --Coordinator Role

INSERT INTO #TempEmployee (IDEmploye, LastName, FirstName, Username)
	SELECT DISTINCT c.IDEmploye
				    ,e.nom as LastName
				    ,e.prenom as FirstName
				    ,dbo.CreateUsername(prenom,nom) AS Username
FROM ClaraEmployes.Coordonnateur c
     JOIN ClaraEmployes.Employe e ON e.IDEmploye = c.IDEmploye
      AND (c.AnSessionDebut IS NULL OR c.AnSessionDebut <= @Semester)
      AND (c.AnSessionFin IS NULL OR c.AnSessionFin >= @Semester)
      AND c.IDUniteOrg = 235
      AND c.IDEmploye NOT IN (SELECT eu.IDEmploye
                              FROM Users.EmployeeUser eu
                                   JOIN Applications.UserRole ur ON ur.IDUser = eu.IDUser
                                   JOIN Applications.Role r ON r.IDRole = ur.IDRole
                                   JOIN Applications.ApplicationRole ar ON ar.IDRole = r.IDRole
                                   JOIN Applications.Application a ON a.IDApplication = ar.IDApplication
                              WHERE r.Code = 'CO'
                             );
	SET @employeeCursor = CURSOR FOR
	SELECT IDEmploye FROM #TempEmployee;
	OPEN @employeeCursor;

	FETCH NEXT FROM @employeeCursor INTO @current_id;
		WHILE @@FETCH_STATUS = 0
		BEGIN
		SET @IDUser = (
		SELECT IDUser 
		FROM Users.EmployeeUser 
		WHERE IDEmploye = @current_id
		);
			IF (@IDUser IS NOT NULL)
			BEGIN
			INSERT INTO Applications.UserRole (IDUser, IDRole, IsActive)
			VALUES
			(@IDUser
			,@IDRole
			,1);
			END--END IF

		FETCH NEXT FROM @employeeCursor INTO @current_id;

		END --END WHILE

CLOSE @employeeCursor;
DEALLOCATE @employeeCursor;
	SELECT @IDUser;


DROP TABLE #TempEmployee;
END
GO
GRANT EXECUTE ON  [dbo].[uspAddMissingCoordinatorRole] TO [CSAdmin]
GO
