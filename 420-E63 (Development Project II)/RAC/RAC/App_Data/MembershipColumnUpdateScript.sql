USE [RAC2017]
GO
sp_rename 'AspNetUserRoles.AspNetRoles_Id', 'RoleId', 'COLUMN';
GO
sp_rename 'AspNetUserRoles.AspNetUsers_Id', 'UserId', 'COLUMN';