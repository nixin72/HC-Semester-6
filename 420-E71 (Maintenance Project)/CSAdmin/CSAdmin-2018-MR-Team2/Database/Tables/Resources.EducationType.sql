CREATE TABLE [Resources].[EducationType]
(
[IDEducationType] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Resources].[EducationType] ADD CONSTRAINT [PK_EducationType] PRIMARY KEY CLUSTERED  ([IDEducationType]) ON [PRIMARY]
GO
GRANT CONTROL ON  [Resources].[EducationType] TO [Alumni]
GRANT REFERENCES ON  [Resources].[EducationType] TO [Alumni]
GRANT SELECT ON  [Resources].[EducationType] TO [Alumni]
GRANT INSERT ON  [Resources].[EducationType] TO [Alumni]
GRANT DELETE ON  [Resources].[EducationType] TO [Alumni]
GRANT CONTROL ON  [Resources].[EducationType] TO [CSAdmin]
GRANT REFERENCES ON  [Resources].[EducationType] TO [CSAdmin]
GRANT SELECT ON  [Resources].[EducationType] TO [CSAdmin]
GRANT INSERT ON  [Resources].[EducationType] TO [CSAdmin]
GRANT DELETE ON  [Resources].[EducationType] TO [CSAdmin]
GO
