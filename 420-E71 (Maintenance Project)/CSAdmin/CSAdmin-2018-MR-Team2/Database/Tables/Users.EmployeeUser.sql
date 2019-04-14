CREATE TABLE [Users].[EmployeeUser]
(
[IDUser] [int] NOT NULL,
[IDEmploye] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Users].[EmployeeUser] ADD CONSTRAINT [PK_EmployeeUser] PRIMARY KEY CLUSTERED  ([IDUser]) ON [PRIMARY]
GO
ALTER TABLE [Users].[EmployeeUser] ADD CONSTRAINT [FK_EmployeeUser_User] FOREIGN KEY ([IDUser]) REFERENCES [Users].[User] ([IDUser])
GO
GRANT SELECT ON  [Users].[EmployeeUser] TO [Alumni]
GO
