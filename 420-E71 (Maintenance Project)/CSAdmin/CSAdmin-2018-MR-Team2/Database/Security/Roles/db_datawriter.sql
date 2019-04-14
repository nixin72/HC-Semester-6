EXEC sp_addrolemember N'db_datawriter', N'HERITAGE1\CSTEST$'
GO
EXEC sp_addrolemember N'db_datawriter', N'IIS APPPOOL\DefaultAppPool'
GO
EXEC sp_addrolemember N'db_datawriter', N'RAC'
GO
