IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'CES')
CREATE LOGIN [CES] WITH PASSWORD = 'p@ssw0rd'
GO
CREATE USER [CES] FOR LOGIN [CES]
GO