CREATE TABLE [Applications].[PageRoleSecurity]
(
[IDApplication] [int] NOT NULL,
[Role] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PageName] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Applications].[PageRoleSecurity] ADD CONSTRAINT [FK_PageRoleSecurity_Application] FOREIGN KEY ([IDApplication]) REFERENCES [Applications].[Application] ([IDApplication])
GO
