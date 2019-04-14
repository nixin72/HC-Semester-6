CREATE TABLE [Resources].[ProvinceState]
(
[IDProvinceState] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
[IDCountry] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Resources].[ProvinceState] ADD CONSTRAINT [PK_ProvinceState] PRIMARY KEY CLUSTERED  ([IDProvinceState]) ON [PRIMARY]
GO
ALTER TABLE [Resources].[ProvinceState] ADD CONSTRAINT [FK_ProvinceState_Country] FOREIGN KEY ([IDCountry]) REFERENCES [Resources].[Country] ([IDCountry])
GO
GRANT CONTROL ON  [Resources].[ProvinceState] TO [Alumni]
GRANT REFERENCES ON  [Resources].[ProvinceState] TO [Alumni]
GRANT SELECT ON  [Resources].[ProvinceState] TO [Alumni]
GRANT INSERT ON  [Resources].[ProvinceState] TO [Alumni]
GRANT DELETE ON  [Resources].[ProvinceState] TO [Alumni]
GO
