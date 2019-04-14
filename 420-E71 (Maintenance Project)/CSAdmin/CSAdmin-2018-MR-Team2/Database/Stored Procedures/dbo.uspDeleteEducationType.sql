
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspDeleteEducationType
Description: 	Deletes the education type based on the IDEudcationType
Author:	    	Catherine Losinger
Create Date: 	May 01, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
Alex Desbiens		 MAY 10 2013	  Blocked deletion of types with associated programs
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspDeleteEducationType]
	 @IDEducationType int

AS 
     
BEGIN
DECLARE @count INT = 0
SET NOCOUNT ON;

	SELECT @count = COUNT(*) 
	FROM Resources.Program 
	WHERE IDEducationType = @IDEducationType
	
	IF @count > 0 
	BEGIN
		SELECT @count "Count"
		RETURN @count
	END
	
	DELETE FROM [Resources].[EducationType]
	WHERE IDEducationType = @IDEducationType
		
END
GO

GRANT EXECUTE ON  [dbo].[uspDeleteEducationType] TO [CSAdmin]
GO
