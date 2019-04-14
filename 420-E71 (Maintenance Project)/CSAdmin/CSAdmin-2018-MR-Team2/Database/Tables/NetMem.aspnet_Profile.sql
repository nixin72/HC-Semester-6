CREATE TABLE [NetMem].[aspnet_Profile]
(
[UserId] [uniqueidentifier] NOT NULL,
[PropertyNames] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PropertyValuesString] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PropertyValuesBinary] [image] NOT NULL,
[LastUpdatedDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [NetMem].[aspnet_Profile] WITH NOCHECK ADD
CONSTRAINT [FK__aspnet_Pr__UserI__30E33A54] FOREIGN KEY ([UserId]) REFERENCES [NetMem].[aspnet_Users] ([UserId])
GO
ALTER TABLE [NetMem].[aspnet_Profile] ADD CONSTRAINT [PK__aspnet_P__1788CC4C4CC05EF3] PRIMARY KEY CLUSTERED  ([UserId]) ON [PRIMARY]
GO
