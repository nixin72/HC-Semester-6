SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspSelectSpecialRoles
Description: 	Gets 3 roles CES administrator, Continuing Education Coordinator
				and CS Systems Administrator
Author:	    	Matt Riopel
Create Date: 	April 30, 2012

Param:			None
Return:      	IDRole, Code, Description

Revision History:
Name					Date Modified		Revision   
=======================	===================	=======================================
Matt Riopel 			April 30, 2012	Initial Creation
Susan Turanyi			February 26, 2013	Added Alumni System roles
****************************************************************************/
CREATE PROCEDURE [dbo].[uspSelectSpecialRoles]

AS
BEGIN

SELECT r.IDRole, r.Code, r.[Description]
FROM Applications.[Role] r
WHERE r.Code IN ('CEC', 'AD', 'CSA','ASM','ASA')

END

GO
GRANT EXECUTE ON  [dbo].[uspSelectSpecialRoles] TO [CSAdmin]
GO
