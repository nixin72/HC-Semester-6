CREATE TABLE [NetMem].[aspnet_Users]
(
[ApplicationId] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL CONSTRAINT [DF__aspnet_Us__UserI__23893F36] DEFAULT (newid()),
[UserName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LoweredUserName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MobileAlias] [nvarchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF__aspnet_Us__Mobil__247D636F] DEFAULT (NULL),
[IsAnonymous] [bit] NOT NULL CONSTRAINT [DF__aspnet_Us__IsAno__257187A8] DEFAULT ((0)),
[LastActivityDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_Users] ADD CONSTRAINT [PK__aspnet_U__1788CC4D2D47B39A] PRIMARY KEY NONCLUSTERED  ([UserId]) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_Users] ADD CONSTRAINT [FK__aspnet_Us__Appli__32CB82C6] FOREIGN KEY ([ApplicationId]) REFERENCES [NetMem].[aspnet_Applications] ([ApplicationId])
GO
GRANT SELECT ON  [NetMem].[aspnet_Users] TO [Alumni]
GRANT SELECT ON  [NetMem].[aspnet_Users] TO [RAC]
GRANT UPDATE ([UserName]) ON [NetMem].[aspnet_Users] TO [RAC]
GRANT UPDATE ([LoweredUserName]) ON [NetMem].[aspnet_Users] TO [RAC]
GO
