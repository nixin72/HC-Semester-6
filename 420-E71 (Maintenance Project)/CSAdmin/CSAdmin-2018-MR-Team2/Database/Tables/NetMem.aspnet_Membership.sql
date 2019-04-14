CREATE TABLE [NetMem].[aspnet_Membership]
(
[ApplicationId] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[Password] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PasswordFormat] [int] NOT NULL CONSTRAINT [DF__aspnet_Me__Passw__1FB8AE52] DEFAULT ((0)),
[PasswordSalt] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MobilePIN] [nvarchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LoweredEmail] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PasswordQuestion] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PasswordAnswer] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsApproved] [bit] NOT NULL,
[IsLockedOut] [bit] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[LastLoginDate] [datetime] NOT NULL,
[LastPasswordChangedDate] [datetime] NOT NULL,
[LastLockoutDate] [datetime] NOT NULL,
[FailedPasswordAttemptCount] [int] NOT NULL,
[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
[Comment] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_Membership] ADD CONSTRAINT [PK__aspnet_M__1788CC4D405A880E] PRIMARY KEY NONCLUSTERED  ([UserId]) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_Membership] ADD CONSTRAINT [FK__aspnet_Me__Appli__2B2A60FE] FOREIGN KEY ([ApplicationId]) REFERENCES [NetMem].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [NetMem].[aspnet_Membership] ADD CONSTRAINT [FK__aspnet_Me__UserI__2C1E8537] FOREIGN KEY ([UserId]) REFERENCES [NetMem].[aspnet_Users] ([UserId])
GO
GRANT SELECT ([UserId]) ON [NetMem].[aspnet_Membership] TO [Alumni]
GRANT SELECT ([IsApproved]) ON [NetMem].[aspnet_Membership] TO [Alumni]
GO
