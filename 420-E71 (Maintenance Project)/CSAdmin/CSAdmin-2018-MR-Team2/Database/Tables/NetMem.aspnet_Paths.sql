CREATE TABLE [NetMem].[aspnet_Paths]
(
[ApplicationId] [uniqueidentifier] NOT NULL,
[PathId] [uniqueidentifier] NOT NULL CONSTRAINT [DF__aspnet_Pa__PathI__20ACD28B] DEFAULT (newid()),
[Path] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LoweredPath] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
ALTER TABLE [NetMem].[aspnet_Paths] WITH NOCHECK ADD
CONSTRAINT [FK__aspnet_Pa__Appli__2D12A970] FOREIGN KEY ([ApplicationId]) REFERENCES [NetMem].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [NetMem].[aspnet_Paths] ADD CONSTRAINT [PK__aspnet_P__CD67DC58278EDA44] PRIMARY KEY NONCLUSTERED  ([PathId]) ON [PRIMARY]
GO
