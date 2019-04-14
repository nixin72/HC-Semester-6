CREATE TABLE [NetMem].[aspnet_PersonalizationPerUser]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__aspnet_Perso__Id__21A0F6C4] DEFAULT (newid()),
[PathId] [uniqueidentifier] NULL,
[UserId] [uniqueidentifier] NULL,
[PageSettings] [image] NOT NULL,
[LastUpdatedDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [NetMem].[aspnet_PersonalizationPerUser] WITH NOCHECK ADD
CONSTRAINT [FK__aspnet_Pe__UserI__2FEF161B] FOREIGN KEY ([UserId]) REFERENCES [NetMem].[aspnet_Users] ([UserId])
GO
ALTER TABLE [NetMem].[aspnet_PersonalizationPerUser] ADD CONSTRAINT [PK__aspnet_P__3214EC0648EFCE0F] PRIMARY KEY NONCLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_PersonalizationPerUser] ADD CONSTRAINT [FK__aspnet_Pe__PathI__2EFAF1E2] FOREIGN KEY ([PathId]) REFERENCES [NetMem].[aspnet_Paths] ([PathId])
GO
