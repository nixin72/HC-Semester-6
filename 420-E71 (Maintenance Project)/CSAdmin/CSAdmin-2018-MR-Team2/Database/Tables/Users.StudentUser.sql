CREATE TABLE [Users].[StudentUser]
(
[IDUser] [int] NOT NULL,
[IDEtudiant] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Users].[StudentUser] ADD CONSTRAINT [PK_StudentUser] PRIMARY KEY CLUSTERED  ([IDUser]) ON [PRIMARY]
GO
ALTER TABLE [Users].[StudentUser] ADD CONSTRAINT [FK_StudentUser_User] FOREIGN KEY ([IDUser]) REFERENCES [Users].[User] ([IDUser])
GO
