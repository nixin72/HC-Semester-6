SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Nicholas Renaud
-- Create date: April 19 2012
-- Description:	Select duplicate usernames from the Users table in CSAdmin
-- Revision History:
-- Name			         Date Modified	  Revision   
-- ==================== ================ =======================================
-- Brian Farias Tavares    04/17/2013    Altered stored proc to accept null parameters.
-- Brian Farias Tavares	   04/18/2013	 Altered stored proc to return the AD Username
-- Gage Geneau			   04/25/2013	 Rewrote the search to be more efficient and easier to maintain. Added an option to search by username
-- Brian Farias Tavares	   05/09/2013    Switched back to using a linked server select statement, because tech support made the page size higher.
-- Susan Turanyi		   09/04/2013	 Changed the server for active directory	
-- =============================================================================
CREATE PROCEDURE [dbo].[uspSelectDuplicateUsernames]
	@SortExpression varchar(50) = null,
	@SortDirection int = 0,
	@SearchLastName varchar(50) = null,
	@SearchFirstName varchar(50) = null,
	@SearchUsername varchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Creating temp table
	CREATE TABLE #LocalTempTable(
				  ADUsername varchar(255),
				  FirstName varchar(255), 
				  LastName varchar(255))
				  
	-- Inserting all AD users into the temp table	
	/*
	BFT 05/09/2013: 
	The following is selecting all users from AD. This is using a linked server. There was an issue with this not working 
	beacuse the AD page size was too short. Although the page size has be raised to 3000. If you exprience issues with it 
	not returning all rows. Its because you need to raise the page size once again. 
	*/				  
    INSERT INTO #LocalTempTable
   	SELECT *
	FROM OPENQUERY(ADSI, 'SELECT  SN, givenname, mailnickname FROM ''LDAP://DC1.cegep-heritage.qc.ca'' 
						  WHERE objectClass = ''user'' AND objectCategory=''person''') 
						  
	/*
		BFT 05/09/2013: This uses the stored proc named uspQueryAD which uses system stored procs. In order to run this stored proc
		the database user executing it must be apart of the sysadmin group.
		
	   	INSERT #LocalTempTable EXEC dbo.uspQueryAD 'SELECT  SN, givenname, mailnickname FROM ''LDAP://heritage-srv-5.cegep-heritage.qc.ca'' 
		WHERE objectClass = ''user'' AND objectCategory=''person'''
	*/
		
	/*
	BFT - 04/18/2013:
	
	In the following select statements we need to make sure our queries are case insensitive and accent insensitive. This is done using:
	COLLATE SQL_Latin1_General_CP1_CI_AI
	
	In addition, when users are brought from Clara to CSAdmin, they might have Dr. infront of their first name. To avoid this we are using:
	REPLACE(u.FirstName,'Dr. ','')
	
	We are left joining because a user can exist in CSAdmin and not in Active Directory.
	*/

	SELECT selected.IDUser, selected.LastName, selected.FirstName, selected.Username, selected.IsActive, selected.ADUsername
		FROM (
			select u.IDUser, u.LastName, u.FirstName, u.Username, u.IsActive, ad.ADUsername
			from Users.[User] u
			LEFT JOIN #LocalTempTable ad ON u.LastName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ad.LastName COLLATE SQL_Latin1_General_CP1_CI_AI 
			AND REPLACE(u.FirstName,'Dr. ','') COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ad.FirstName COLLATE SQL_Latin1_General_CP1_CI_AI
			WHERE u.IsActive = 1
			AND ((u.FirstName COLLATE SQL_Latin1_General_CP1_CI_AI like '%' + @SearchFirstName + '%' COLLATE SQL_Latin1_General_CP1_CI_AI)
		OR (@SearchFirstName IS NULL))
		AND ((u.LastName COLLATE SQL_Latin1_General_CP1_CI_AI like '%' + @SearchLastName + '%' COLLATE SQL_Latin1_General_CP1_CI_AI)
		OR (@SearchLastName IS NULL))
		AND ((u.Username COLLATE SQL_Latin1_General_CP1_CI_AI like '%' + @SearchUsername + '%' COLLATE SQL_Latin1_General_CP1_CI_AI)
		OR (@SearchUsername IS NULL))
		) selected
		WHERE selected.Username IN 
		(
			SELECT distinct Username
			FROM Users.[User]
			Group By Username
			HAVING (COUNT(Username) > 1)
		)
		ORDER BY
		CASE
			WHEN @SortExpression is NULL THEN LastName + FirstName
		END ASC,
		CASE
			WHEN @SortExpression = 'Name' AND @SortDirection = 0 THEN LastName + FirstName
			WHEN @SortExpression = 'Username' AND @SortDirection = 0 THEN Username
		END ASC,
		CASE
			WHEN @SortExpression = 'Name' AND @SortDirection = 1 THEN LastName + FirstName
			WHEN @SortExpression = 'Username' AND @SortDirection = 1 THEN Username
		END DESC

	--Dropping temp table
	DROP TABLE #LocalTempTable
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectDuplicateUsernames] TO [CSAdmin]
GO
