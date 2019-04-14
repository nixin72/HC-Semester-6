CREATE TABLE [Resources].[Language]
(
[LanguageID] [int] NOT NULL IDENTITY(1, 1),
[Language] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDefault] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [Resources].[Language] ADD CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED  ([LanguageID]) ON [PRIMARY]
GO
GRANT CONTROL ON  [Resources].[Language] TO [CSAdmin]
GRANT REFERENCES ON  [Resources].[Language] TO [CSAdmin]
GRANT SELECT ON  [Resources].[Language] TO [CSAdmin]
GRANT INSERT ON  [Resources].[Language] TO [CSAdmin]
GRANT DELETE ON  [Resources].[Language] TO [CSAdmin]
GRANT UPDATE ON  [Resources].[Language] TO [CSAdmin]
GRANT CONTROL ON  [Resources].[Language] TO [eCoop]
GRANT REFERENCES ON  [Resources].[Language] TO [eCoop]
GRANT SELECT ON  [Resources].[Language] TO [eCoop]
GRANT INSERT ON  [Resources].[Language] TO [eCoop]
GRANT DELETE ON  [Resources].[Language] TO [eCoop]
GRANT UPDATE ON  [Resources].[Language] TO [eCoop]
GO
