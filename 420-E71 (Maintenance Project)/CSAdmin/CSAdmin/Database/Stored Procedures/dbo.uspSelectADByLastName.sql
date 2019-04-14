SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		Brian Farias Tavares
-- Create date: April 18 2013
-- Description:	Select a user in ad 
-- Revision History:
-- Name			         Date Modified	  Revision   
-- ==================== ================ =======================================
-- Brian Farias Tavares    04/17/2013    Altered stored proc to accept null parameters.
-- Brian Farias Tavares	   04/18/2013	 Altered stored proc to return the AD Username
-- =============================================================================
CREATE PROCEDURE [dbo].[uspSelectADByLastName]
	-- Add the parameters for the stored procedure here
	@LastName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Creating temp table
	CREATE TABLE #LocalTempTable(
				  Username varchar(255),
				  FirstName varchar(255), 
				  LastName varchar(255),
				  Department varchar(255))
				  
	/*
	BFT 05/09/2013: 
	The following is selecting all users from AD. This is using a linked server. There was an issue with this not working 
	beacuse the AD page size was too short. Although the page size has be raised to 3000. If you exprience issues with it 
	not returning all rows. Its because you need to raise the page size once again. 
	*/	
			  
	INSERT INTO #LocalTempTable
   	SELECT *
	FROM OPENQUERY(ADSI, 'SELECT  department,SN, givenname, mailnickname FROM ''LDAP://DC1.cegep-heritage.qc.ca'' 
						  WHERE objectClass = ''user'' AND objectCategory=''person''') 
						  
	/*
		BFT 05/09/2013: This uses the stored proc named uspQueryAD which uses system stored procs. In order to run this stored proc
		the database user executing it must be apart of the sysadmin group.
		
	   	INSERT #LocalTempTable EXEC dbo.uspQueryAD 'SELECT  SN, givenname, mailnickname FROM ''LDAP://heritage-srv-5.cegep-heritage.qc.ca'' 
		WHERE objectClass = ''user'' AND objectCategory=''person'''
	*/
						  
	SELECT * FROM #LocalTempTable WHERE LastName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%'+@LastName+'%' COLLATE SQL_Latin1_General_CP1_CI_AI
	ORDER BY LastName, FirstName
	--Dropping Temp Table
	DROP TABLE #LocalTempTable
    
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectADByLastName] TO [CSAdmin]
GO
