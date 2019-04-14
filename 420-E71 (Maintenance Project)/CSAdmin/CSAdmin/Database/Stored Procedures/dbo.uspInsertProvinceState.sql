SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertProvinceState
Description: 	Updates the province or state based on the IDProvinceState
Author:	    	Catherine Losinger
Create Date: 	May 01, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspInsertProvinceState]
	@Name varchar(50)	
   ,@Country varchar(50)
AS 
     
BEGIN
SET NOCOUNT ON;

  INSERT INTO [Resources].[ProvinceState]
		(Name
		,IDCountry)
  VALUES (@Name
		 ,(SELECT IDCountry
		 FROM [Resources].[Country]
		 WHERE Name = @Country))

END
GO
GRANT EXECUTE ON  [dbo].[uspInsertProvinceState] TO [CSAdmin]
GO
