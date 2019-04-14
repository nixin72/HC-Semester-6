CREATE TABLE [Applications].[Application]
(
[IDApplication] [int] NOT NULL IDENTITY(1, 1),
[Code] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Applications].[Application] ADD CONSTRAINT [PK_App] PRIMARY KEY CLUSTERED  ([IDApplication]) ON [PRIMARY]
GO
