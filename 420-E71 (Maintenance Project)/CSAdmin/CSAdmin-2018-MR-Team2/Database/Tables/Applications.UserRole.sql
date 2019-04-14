CREATE TABLE [Applications].[UserRole]
(
[IDUserRole] [int] NOT NULL IDENTITY(1, 1),
[IDUser] [int] NOT NULL,
[IDRole] [int] NOT NULL,
[IsActive] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Applications].[UserRole] ADD CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED  ([IDUserRole]) ON [PRIMARY]
GO
ALTER TABLE [Applications].[UserRole] ADD CONSTRAINT [FK_UserRole_UserRole] FOREIGN KEY ([IDRole]) REFERENCES [Applications].[Role] ([IDRole])
GO
ALTER TABLE [Applications].[UserRole] ADD CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([IDUser]) REFERENCES [Users].[User] ([IDUser])
GO
GRANT SELECT ON  [Applications].[UserRole] TO [Alumni]
GRANT INSERT ON  [Applications].[UserRole] TO [Alumni]
GRANT DELETE ON  [Applications].[UserRole] TO [Alumni]
GO
