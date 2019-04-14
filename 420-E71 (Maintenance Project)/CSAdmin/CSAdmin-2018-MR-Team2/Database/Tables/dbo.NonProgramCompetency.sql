CREATE TABLE [dbo].[NonProgramCompetency]
(
[NonProgramCompetencyID] [int] NOT NULL,
[Domain] [tinyint] NOT NULL,
[CompetencyCode] [varchar] (4) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsProgramSpecific] [tinyint] NULL
) ON [PRIMARY]
GO
