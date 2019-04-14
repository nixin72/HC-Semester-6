
USE [RAC2017]
Drop view [dbo].[Clara.Competencies];
Drop view [dbo].[Clara.FeuilleObjectifProgramme];
Drop view [dbo].[Clara.BrancheObjectifProgramme];
Drop view [dbo].[Clara.Programmes];
Drop view [dbo].[Clara.Grilles];
Drop view [dbo].[Clara.CoursGrilles];
Drop view [dbo].[Clara.Cours];
GO

CREATE VIEW [Clara.Objectifs] AS
	SELECT
	  [IDObjectif]
      ,[Numero]
      ,[Nom]
      ,[LangueOrigine]
      ,[NomTraduit]
  FROM [CLARA].[Clara].[Objectifs].[Objectif]
 GO

 CREATE VIEW [Clara.FeuilleObjectifProgramme] AS
	SELECT [IDObjectif]
      ,[IDBrancheObjectifProgramme]
  FROM [CLARA].[Clara].[Objectifs].[FeuilleObjectifProgramme]
GO

 CREATE VIEW [Clara.BrancheObjectifProgramme] AS
	SELECT [IDBrancheObjectifProgramme]
      ,[IDProgramme]
  FROM [CLARA].[Clara].[Objectifs].[BrancheObjectifProgramme]
GO

CREATE VIEW [Clara.Programmes] AS
	SELECT [IDProgramme]
      ,[Numero]
      ,[AnneeVersion]
      ,[TitreLong]
      ,[TitreLongTraduit]
	  ,[TypeProgramme]
      ,[LangueOrigine]
      ,[TitreTraduit]
  FROM [CLARA].[Clara].[Programmes].[Programme]
GO

CREATE VIEW [Clara.Grille] AS
	 SELECT [IDGrille]
      ,[Numero]
      ,[Titre]
      ,[IDProgramme]
  FROM [CLARA].[Clara].[Grilles].[Grille]
GO

CREATE VIEW [Clara.CoursGrille] AS
	SELECT [IDCoursGrille]
      ,[IDGrille]    
	  ,[IDCours]
  FROM [CLARA].[Clara].[Grilles].[CoursGrille]
GO


CREATE VIEW [Clara.Cours] AS
	SELECT [IDCours]
      ,[Numero]
      ,[TitreLong]
      ,[TitreLongTraduit]
      ,[LangueOrigine]
      ,[TitreTraduit]
      ,[IDCategorieCours]

  FROM [CLARA].[Clara].[BanqueCours].[Cours]
GO

--ONLY THIS VIEW IS SPECIFIC FOR THE HERITAGE COLLEGE SERVER CONFIGURATION. NEED THIS TO ADD THE ENGLISH OBJECTIVES
--CREATE VIEW [ReportClient].[ObjectifENG]
--AS
--SELECT
--	  [IDObjectif]
--      ,[Numero]
--      ,[Nom]
--      ,[LangueOrigine]
--      ,[NomTraduit]
--  FROM [Objectifs].[Objectif]




GO
















