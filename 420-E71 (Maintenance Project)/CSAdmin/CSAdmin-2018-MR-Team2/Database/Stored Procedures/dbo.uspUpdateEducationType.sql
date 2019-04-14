SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspUpdateEducationTypes
Description: 	Updates the education type based on the IDEudcationType
Author:	    	Catherine Losinger
Create Date: 	April 29, 2013

Revision History:
Name			     Date Modified	  Revision   
==================== ================ ======================================
***************************************************************************/  
CREATE PROCEDURE [dbo].[uspUpdateEducationType]
	 @IDEducationType int
	,@Name varchar(255)
AS 
     
BEGIN
SET NOCOUNT ON;

  UPDATE [Resources].[EducationType]
  SET 
	Name = @Name
  WHERE IDEducationType = @IDEducationType
END
GO
GRANT EXECUTE ON  [dbo].[uspUpdateEducationType] TO [CSAdmin]
GO
