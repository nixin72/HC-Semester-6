IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'eCoop')
CREATE LOGIN [eCoop] WITH PASSWORD = 'p@ssw0rd'
GO
CREATE USER [eCoop] FOR LOGIN [eCoop]
GO
