SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/***************************************************************************
Name:           uspSelectIDEmploye
Description: 	Gets the id of a user
Author:	    	Renee Ghattas
Create Date: 	March 1, 2012

Param:			@nomPrenom = The full name of the user
Return:      	IDEmploye

Revision History:
Name			     Date Modified	  Revision   
==================== ================ =======================================
Renee Ghattas         March 19, 2012  Modified input parameters to access nom and prenom seperately
****************************************************************************/
CREATE PROCEDURE [dbo].[uspSelectIDEmploye]
	@nom varchar(max) = '',
	@prenom varchar(max) = ''
AS
BEGIN
		 SELECT DISTINCT e.IDEmploye
		FROM ClaraEmployes.Employe e
		WHERE UPPER(e.nom) = UPPER(@nom)
		AND UPPER(e.prenom) = UPPER(@prenom)
		AND e.Etat = 1	
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectIDEmploye] TO [CES]
GRANT EXECUTE ON  [dbo].[uspSelectIDEmploye] TO [CSAdmin]
GO
