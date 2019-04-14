
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspDeleteCountry
Description: 	Deletes the country based on the IDCountry
Author:	    	Catherine Losinger
Create Date: 	May 01, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
Alex Desbiens		 MAY 10 2013	  Blocked deleting countries that have provinces, I.E. Canada and US
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspDeleteCountry]
	 @IDCountry int

AS 
     
BEGIN
DECLARE
	@count INT = 0
SET NOCOUNT ON;

	SELECT COUNT(*) 
	FROM Resources.ProvinceState 
	WHERE IDCountry = @IDCountry
	
	IF @count > 0 
	BEGIN
		SELECT @count "Count"
		RETURN @count
	END

	DELETE FROM [Resources].[Country]
	WHERE IDCountry= @IDCountry
	
END
GO

GRANT EXECUTE ON  [dbo].[uspDeleteCountry] TO [CSAdmin]
GO
