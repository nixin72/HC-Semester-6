SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspUpdateProvinceState
Description: 	Updates the province or state based on the IDProvinceState
Author:	    	Catherine Losinger
Create Date: 	May 01, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspUpdateProvinceState]
	 @IDProvinceState int
	,@Name varchar(255)
AS 
     
BEGIN
SET NOCOUNT ON;

  UPDATE [Resources].[ProvinceState]
  SET 
	Name = @Name
  WHERE IDProvinceState = @IDProvinceState
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateProvinceState] TO [CSAdmin]
GO
