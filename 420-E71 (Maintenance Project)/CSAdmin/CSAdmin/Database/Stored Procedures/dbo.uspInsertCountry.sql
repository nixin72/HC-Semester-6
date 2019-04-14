SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertCountry
Description: 	Updates the education type based on the IDEudcationType
Author:	    	Catherine Losinger
Create Date: 	April 29, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspInsertCountry]
	@Name varchar(50)
AS 
     
BEGIN
SET NOCOUNT ON;

  INSERT INTO [Resources].[Country]
		(Name)
  VALUES (@Name)

END
GO
GRANT EXECUTE ON  [dbo].[uspInsertCountry] TO [CSAdmin]
GO
