IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'Alumni')
CREATE LOGIN [Alumni] WITH PASSWORD = 'p@ssw0rd'
GO
CREATE USER [Alumni] FOR LOGIN [Alumni]
GO
