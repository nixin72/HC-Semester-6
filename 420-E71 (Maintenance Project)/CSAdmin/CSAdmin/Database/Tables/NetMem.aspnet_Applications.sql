CREATE TABLE [NetMem].[aspnet_Applications]
(
[ApplicationName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LoweredApplicationName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ApplicationId] [uniqueidentifier] NOT NULL CONSTRAINT [DF__aspnet_Ap__Appli__1EC48A19] DEFAULT (newid()),
[Description] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_Applications] ADD CONSTRAINT [PK__aspnet_A__C93A4C986B79F03D] PRIMARY KEY NONCLUSTERED  ([ApplicationId]) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_Applications] ADD CONSTRAINT [UQ__aspnet_A__3091033165C116E7] UNIQUE NONCLUSTERED  ([ApplicationName]) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_Applications] ADD CONSTRAINT [UQ__aspnet_A__17477DE4689D8392] UNIQUE NONCLUSTERED  ([LoweredApplicationName]) ON [PRIMARY]
GO
