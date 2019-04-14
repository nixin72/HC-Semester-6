SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspDeleteProvinceState
Description: 	Deletes the province or state based on the IDProvinceState
Author:	    	Catherine Losinger
Create Date: 	May 01, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspDeleteProvinceState]
	 @IDProvinceState int

AS 
     
BEGIN
SET NOCOUNT ON;

  DELETE FROM [Resources].[ProvinceState]
  WHERE IDProvinceState = @IDProvinceState
END
GO
GRANT EXECUTE ON  [dbo].[uspDeleteProvinceState] TO [CSAdmin]
GO
