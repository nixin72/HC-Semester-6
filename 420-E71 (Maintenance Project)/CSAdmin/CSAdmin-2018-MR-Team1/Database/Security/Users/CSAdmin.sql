IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'CSAdmin')
CREATE LOGIN [CSAdmin] WITH PASSWORD = 'p@ssw0rd'
GO
CREATE USER [CSAdmin] FOR LOGIN [CSAdmin]
GO
