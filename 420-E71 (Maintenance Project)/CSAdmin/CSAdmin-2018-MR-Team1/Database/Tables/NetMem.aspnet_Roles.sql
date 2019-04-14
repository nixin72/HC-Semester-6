CREATE TABLE [NetMem].[aspnet_Roles]
(
[ApplicationId] [uniqueidentifier] NOT NULL,
[RoleId] [uniqueidentifier] NOT NULL CONSTRAINT [DF__aspnet_Ro__RoleI__22951AFD] DEFAULT (newid()),
[RoleName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LoweredRoleName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_Roles] ADD CONSTRAINT [PK__aspnet_R__8AFACE1B3118447E] PRIMARY KEY NONCLUSTERED  ([RoleId]) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_Roles] ADD CONSTRAINT [FK__aspnet_Ro__Appli__31D75E8D] FOREIGN KEY ([ApplicationId]) REFERENCES [NetMem].[aspnet_Applications] ([ApplicationId])
GO
