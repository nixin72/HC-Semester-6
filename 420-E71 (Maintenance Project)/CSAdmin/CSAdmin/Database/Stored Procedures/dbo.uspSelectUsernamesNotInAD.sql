SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Nicholas Renaud
-- Create date: May 2, 2012
-- Description:	Selects the usernames that are not currently in the active directory.
-- Revision History:
-- Name			         Date Modified	  Revision   
-- ==================== ================ =======================================
-- Brian Farias Tavares		04/17/2013	Fixed comparison of CSAdmin w/ AD. Also, bypassed the AD query cap.
-- Gage Geneau				04/17/2013	Rewrote the search to be more efficient and easier to maintain. Added an option to search by username
-- Brian Farias Tavares		05/09/2013	Switched back to using a linked server select statement, because tech support made the page size higher.
-- Susan Turanyi			09/04/2013	Changed the server for active directory
-- =============================================================================

CREATE PROCEDURE [dbo].[uspSelectUsernamesNotInAD]
	-- Add the parameters for the stored procedure here
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
				  Username varchar(255),
				  FirstName varchar(255), 
				  LastName varchar(255))

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

	SELECT * FROM [Users].[User] u WHERE 
	((u.FirstName COLLATE SQL_Latin1_General_CP1_CI_AI like '%' + @SearchFirstName + '%' COLLATE SQL_Latin1_General_CP1_CI_AI)
		OR (@SearchFirstName IS NULL))
		AND ((u.LastName COLLATE SQL_Latin1_General_CP1_CI_AI like '%' + @SearchLastName + '%' COLLATE SQL_Latin1_General_CP1_CI_AI)
		OR (@SearchLastName IS NULL))
		AND ((u.Username COLLATE SQL_Latin1_General_CP1_CI_AI like '%' + @SearchUsername + '%' COLLATE SQL_Latin1_General_CP1_CI_AI)
		OR (@SearchUsername IS NULL))
		AND Username NOT IN (SELECT Username FROM #LocalTempTable WHERE Username IS NOT NULL)
		AND IsActive = 1
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

	--Dropping Temp Table
	DROP TABLE #LocalTempTable
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectUsernamesNotInAD] TO [CSAdmin]
GO
