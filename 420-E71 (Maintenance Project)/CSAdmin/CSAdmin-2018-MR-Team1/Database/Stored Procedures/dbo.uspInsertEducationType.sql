SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspInsertEducationTypes
Description: 	Updates the education type based on the IDEudcationType
Author:	    	Catherine Losinger
Create Date: 	April 29, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspInsertEducationType]
	@Name varchar(50)
AS 
     
BEGIN
SET NOCOUNT ON;

  INSERT INTO [Resources].[EducationType]
		(Name)
  VALUES (@Name)

END
GO
GRANT EXECUTE ON  [dbo].[uspInsertEducationType] TO [CSAdmin]
GO
