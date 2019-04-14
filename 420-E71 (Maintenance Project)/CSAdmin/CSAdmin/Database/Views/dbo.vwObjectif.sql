SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[vwObjectif]
AS
SELECT * FROM [ClaraObjectifs].[Objectif]
GO
GRANT SELECT ON  [dbo].[vwObjectif] TO [RAC]
GO
