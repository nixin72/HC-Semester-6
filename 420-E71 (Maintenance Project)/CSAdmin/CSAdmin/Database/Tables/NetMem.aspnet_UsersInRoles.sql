CREATE TABLE [NetMem].[aspnet_UsersInRoles]
(
[UserId] [uniqueidentifier] NOT NULL,
[RoleId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
ALTER TABLE [NetMem].[aspnet_UsersInRoles] WITH NOCHECK ADD
CONSTRAINT [FK__aspnet_Us__UserI__34B3CB38] FOREIGN KEY ([UserId]) REFERENCES [NetMem].[aspnet_Users] ([UserId])
GO
ALTER TABLE [NetMem].[aspnet_UsersInRoles] ADD CONSTRAINT [PK__aspnet_U__AF2760AD3C89F72A] PRIMARY KEY CLUSTERED  ([UserId], [RoleId]) ON [PRIMARY]
GO
ALTER TABLE [NetMem].[aspnet_UsersInRoles] ADD CONSTRAINT [FK__aspnet_Us__RoleI__33BFA6FF] FOREIGN KEY ([RoleId]) REFERENCES [NetMem].[aspnet_Roles] ([RoleId])
GO
