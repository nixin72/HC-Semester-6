SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspSelectProvinceStates
Description: 	Returns all the provinces and states for drop down list population
Author:	    	Catherine Losinger
Create Date: 	April 29, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
Alex Desbiens		 MAY 09 2013	  Added @IDCountry parameter
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspSelectProvinceStates]
@IDCountry INTEGER = null
AS 
BEGIN
	SET NOCOUNT ON;
	IF @IDCountry = NULL
		SELECT IDProvinceState
				,[Name]
		FROM [Resources].[ProvinceState]
	ELSE
		SELECT [IDProvinceState], [Name]
		FROM [Resources].[ProvinceState]
		WHERE IDCountry = @IDCountry
		ORDER BY [Name]
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectProvinceStates] TO [CSAdmin]
GO
