CREATE TABLE [Resources].[Province]
(
[IDProvince] [int] NOT NULL IDENTITY(1, 1),
[Text] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Abbreviation] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Province_Abbreviation] DEFAULT ('XX')
) ON [PRIMARY]
GO
ALTER TABLE [Resources].[Province] ADD CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED  ([IDProvince]) ON [PRIMARY]
GO
