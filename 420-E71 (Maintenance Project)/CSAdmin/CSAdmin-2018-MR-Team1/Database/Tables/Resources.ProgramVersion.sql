CREATE TABLE [Resources].[ProgramVersion]
(
[IDProgramVersion] [int] NOT NULL IDENTITY(1, 1),
[IDProgram] [int] NOT NULL,
[IDProgramClara] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Resources].[ProgramVersion] ADD CONSTRAINT [PK_ProgramVersion] PRIMARY KEY CLUSTERED  ([IDProgramVersion]) ON [PRIMARY]
GO
ALTER TABLE [Resources].[ProgramVersion] ADD CONSTRAINT [CK_ProgramVersion] UNIQUE NONCLUSTERED  ([IDProgram], [IDProgramClara]) ON [PRIMARY]
GO
ALTER TABLE [Resources].[ProgramVersion] ADD CONSTRAINT [UC_ProgramVersion] UNIQUE NONCLUSTERED  ([IDProgram], [IDProgramClara]) ON [PRIMARY]
GO
ALTER TABLE [Resources].[ProgramVersion] ADD CONSTRAINT [FK_ProgramVersion_Program] FOREIGN KEY ([IDProgram]) REFERENCES [Resources].[Program] ([IDProgram])
GO
