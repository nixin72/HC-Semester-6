SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspSelectCountries
Description: 	Returns all countries for drop down list population
Author:	    	Catherine Losinger
Create Date: 	April 26, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspSelectCountries]
AS
BEGIN
SELECT IDCountry
	,[Name]
  FROM [Resources].[Country]
  ORDER BY CASE WHEN Name = 'Canada' THEN 1 ELSE 2 END, CASE WHEN Name = 'United States' THEN 1 ELSE 2 END, Name
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectCountries] TO [CSAdmin]
GO
