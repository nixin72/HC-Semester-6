IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'HERITAGE1\CSTEST$')
CREATE LOGIN [HERITAGE1\CSTEST$] FROM WINDOWS
GO
CREATE USER [HERITAGE1\CSTEST$] FOR LOGIN [HERITAGE1\CSTEST$]
GO
