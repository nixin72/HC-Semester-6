CREATE TABLE [Resources].[Program]
(
[IDProgram] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
[IDEducationType] [int] NOT NULL,
[IsActive] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Resources].[Program] ADD CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED  ([IDProgram]) ON [PRIMARY]
GO
ALTER TABLE [Resources].[Program] ADD CONSTRAINT [FK_Program_EducationType] FOREIGN KEY ([IDEducationType]) REFERENCES [Resources].[EducationType] ([IDEducationType])
GO
GRANT CONTROL ON  [Resources].[Program] TO [CSAdmin]
GRANT REFERENCES ON  [Resources].[Program] TO [CSAdmin]
GRANT SELECT ON  [Resources].[Program] TO [CSAdmin]
GRANT INSERT ON  [Resources].[Program] TO [CSAdmin]
GRANT DELETE ON  [Resources].[Program] TO [CSAdmin]
GO
