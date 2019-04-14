SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertProvinceState
Description: 	Updates the province or state based on the IDProvinceState
Author:	    	Catherine Losinger
Create Date: 	April 29, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspProvinceState]
	@Name varchar(50)
	,@IDCountry Integer
AS 
     
BEGIN
SET NOCOUNT ON;

  INSERT INTO [Resources].[Country]
		(Name
		,IDCountry)
  VALUES (@Name
		 ,@IDCountry)

END
GO
GRANT EXECUTE ON  [dbo].[uspProvinceState] TO [CSAdmin]
GO
