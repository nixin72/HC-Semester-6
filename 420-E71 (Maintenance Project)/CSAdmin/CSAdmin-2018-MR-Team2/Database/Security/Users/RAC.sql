IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'RAC')
CREATE LOGIN [RAC] WITH PASSWORD = 'p@ssw0rd'
GO
CREATE USER [RAC] FOR LOGIN [RAC]
GO
GRANT SELECT TO [RAC]
GO
