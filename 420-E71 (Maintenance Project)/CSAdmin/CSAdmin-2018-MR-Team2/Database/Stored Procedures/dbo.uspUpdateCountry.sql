SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspUpdateCountry
Description: 	Updates the country based on the IDCountry
Author:	    	Catherine Losinger
Create Date: 	May 01, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspUpdateCountry]
	 @IDCountry int
	,@Name varchar(255)
AS 
     
BEGIN
SET NOCOUNT ON;

  UPDATE [Resources].[Country]
  SET 
	Name = @Name
  WHERE IDCountry = @IDCountry
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateCountry] TO [CSAdmin]
GO
