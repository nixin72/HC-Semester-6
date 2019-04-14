CREATE TABLE [Applications].[Role]
(
[IDRole] [int] NOT NULL IDENTITY(1, 1),
[Code] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Applications].[Role] ADD CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED  ([IDRole]) ON [PRIMARY]
GO
GRANT SELECT ON  [Applications].[Role] TO [Alumni]
GO
