CREATE TABLE [Users].[User]
(
[IDUser] [int] NOT NULL IDENTITY(1, 1),
[LastName] [varchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FirstName] [varchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Username] [varchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsActive] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Users].[User] ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED  ([IDUser]) ON [PRIMARY]
GO
ALTER TABLE [Users].[User] ADD CONSTRAINT [UNIQUE_Username] UNIQUE NONCLUSTERED  ([Username]) ON [PRIMARY]
GO
GRANT SELECT ON  [Users].[User] TO [Alumni]
GO
