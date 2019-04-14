CREATE TABLE [Applications].[ApplicationRole]
(
[IDApplicationRole] [int] NOT NULL IDENTITY(1, 1),
[IDApplication] [int] NOT NULL,
[IDRole] [int] NOT NULL,
[isActive] [bit] NOT NULL CONSTRAINT [dv_isActive] DEFAULT ('1')
) ON [PRIMARY]
GO
ALTER TABLE [Applications].[ApplicationRole] ADD CONSTRAINT [PK_AppRole] PRIMARY KEY CLUSTERED  ([IDApplicationRole]) ON [PRIMARY]
GO
ALTER TABLE [Applications].[ApplicationRole] ADD CONSTRAINT [FK_AppRole_App] FOREIGN KEY ([IDApplication]) REFERENCES [Applications].[Application] ([IDApplication])
GO
ALTER TABLE [Applications].[ApplicationRole] ADD CONSTRAINT [FK_AppRole_Role] FOREIGN KEY ([IDRole]) REFERENCES [Applications].[Role] ([IDRole])
GO
