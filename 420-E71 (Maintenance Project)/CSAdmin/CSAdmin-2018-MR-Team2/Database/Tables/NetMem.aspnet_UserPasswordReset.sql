CREATE TABLE [NetMem].[aspnet_UserPasswordReset]
(
[PasswordResetToken] [uniqueidentifier] NOT NULL,
[PasswordResetExpiration] [datetime] NOT NULL,
[NetMembershipUser] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
