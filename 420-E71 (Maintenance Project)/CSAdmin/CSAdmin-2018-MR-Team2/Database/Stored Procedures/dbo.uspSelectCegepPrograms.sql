SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspSelectCegepPrograms]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
select distinct(p.numero),g.titre, g.IDProgramme
from ClaraGrilles.Grille g
left join ClaraProgrammes.Programme p on p.IDProgramme = g.IDProgramme
where p.IDTypeSanction <11 --11 is for cont. ED
order by p.Numero
END
GO
GRANT EXECUTE ON  [dbo].[uspSelectCegepPrograms] TO [CSAdmin]
GO
