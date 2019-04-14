CREATE TABLE [Resources].[Country]
(
[IDCountry] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Resources].[Country] ADD CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED  ([IDCountry]) ON [PRIMARY]
GO
ALTER TABLE [Resources].[Country] ADD CONSTRAINT [UniqueName] UNIQUE NONCLUSTERED  ([Name]) ON [PRIMARY]
GO
GRANT CONTROL ON  [Resources].[Country] TO [Alumni]
GRANT REFERENCES ON  [Resources].[Country] TO [Alumni]
GRANT SELECT ON  [Resources].[Country] TO [Alumni]
GRANT INSERT ON  [Resources].[Country] TO [Alumni]
GRANT DELETE ON  [Resources].[Country] TO [Alumni]
GRANT CONTROL ON  [Resources].[Country] TO [CSAdmin]
GRANT REFERENCES ON  [Resources].[Country] TO [CSAdmin]
GRANT SELECT ON  [Resources].[Country] TO [CSAdmin]
GRANT INSERT ON  [Resources].[Country] TO [CSAdmin]
GRANT DELETE ON  [Resources].[Country] TO [CSAdmin]
GO
