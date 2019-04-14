SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [NetMem].[vw_aspnet_MembershipUsers]
AS
SELECT     NetMem.aspnet_Membership.UserId, NetMem.aspnet_Membership.PasswordFormat, NetMem.aspnet_Membership.MobilePIN, NetMem.aspnet_Membership.Email, 
                      NetMem.aspnet_Membership.LoweredEmail, NetMem.aspnet_Membership.PasswordQuestion, NetMem.aspnet_Membership.PasswordAnswer, 
                      NetMem.aspnet_Membership.IsApproved, NetMem.aspnet_Membership.IsLockedOut, NetMem.aspnet_Membership.CreateDate, 
                      NetMem.aspnet_Membership.LastLoginDate, NetMem.aspnet_Membership.LastPasswordChangedDate, NetMem.aspnet_Membership.LastLockoutDate, 
                      NetMem.aspnet_Membership.FailedPasswordAttemptCount, NetMem.aspnet_Membership.FailedPasswordAttemptWindowStart, 
                      NetMem.aspnet_Membership.FailedPasswordAnswerAttemptCount, NetMem.aspnet_Membership.FailedPasswordAnswerAttemptWindowStart, 
                      NetMem.aspnet_Membership.Comment, NetMem.aspnet_Users.ApplicationId, NetMem.aspnet_Users.UserName, NetMem.aspnet_Users.MobileAlias, 
                      NetMem.aspnet_Users.IsAnonymous, NetMem.aspnet_Users.LastActivityDate
FROM         NetMem.aspnet_Membership INNER JOIN
                      NetMem.aspnet_Users ON NetMem.aspnet_Membership.UserId = NetMem.aspnet_Users.UserId
GO
EXEC sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[21] 4[40] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "aspnet_Membership (NetMem)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 338
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aspnet_Users (NetMem)"
            Begin Extent = 
               Top = 6
               Left = 376
               Bottom = 125
               Right = 555
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 4215
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', 'SCHEMA', N'NetMem', 'VIEW', N'vw_aspnet_MembershipUsers', NULL, NULL
GO
DECLARE @xp int
SELECT @xp=1
EXEC sp_addextendedproperty N'MS_DiagramPaneCount', @xp, 'SCHEMA', N'NetMem', 'VIEW', N'vw_aspnet_MembershipUsers', NULL, NULL
GO
