SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspSelectEducationTypes
Description: 	Returns all the educationTypes
Author:	    	Catherine Losinger
Create Date: 	April 29, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE  PROCEDURE [dbo].[uspSelectEducationTypes]
AS 
BEGIN
	SET NOCOUNT ON;
	SELECT IDEducationType
	,[Name]
  FROM [Resources].[EducationType]
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectEducationTypes] TO [CSAdmin]
GO
