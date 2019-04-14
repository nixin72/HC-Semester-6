SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/*SQL Code from App_Code\Competencies.vb in RACApplication*/
CREATE VIEW dbo.vwCompetencyDescription
AS
SELECT     o.Numero AS CompetencyCode, CASE WHEN (o.LangueOrigine = 'FR' AND o.NomTraduit IS NOT NULL) THEN o.NomTraduit WHEN (o.LangueOrigine = 'AN' AND 
                      o.Nom IS NOT NULL) THEN o.Nom WHEN (o.LangueOrigine = 'FR' AND o.NomTraduit IS NULL AND o.Nom IS NOT NULL) 
                      THEN o.Nom WHEN (o.LangueOrigine = 'AN' AND o.NomTraduit IS NOT NULL AND o.Nom IS NULL) THEN o.NomTraduit END AS Description, 
                      p.Numero AS ProgramCode, o.IDObjectif AS AnID, bop.IDBrancheObjectifProgramme AS branch, bop.IDBrancheObjectifProgrammeParent AS branchparent
FROM         ClaraObjectifs.BrancheObjectifProgramme AS bop INNER JOIN
                      ClaraProgrammes.Programme AS p ON bop.IDProgramme = p.IDProgramme LEFT OUTER JOIN
                      ClaraObjectifs.BrancheObjectifProgramme AS bop2 ON bop.IDBrancheObjectifProgramme = bop2.IDBrancheObjectifProgrammeParent INNER JOIN
                      ClaraObjectifs.FeuilleObjectifProgramme AS fop ON bop.IDBrancheObjectifProgramme = fop.IDBrancheObjectifProgramme INNER JOIN
                      ClaraObjectifs.Objectif AS o ON fop.IDObjectif = o.IDObjectif
WHERE     (p.DateDesactivation IS NULL)
GO
GRANT SELECT ON  [dbo].[vwCompetencyDescription] TO [RAC]
GO
EXEC sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[34] 4[27] 2[20] 3) )"
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
         Left = -650
      End
      Begin Tables = 
         Begin Table = "bop"
            Begin Extent = 
               Top = 209
               Left = 693
               Bottom = 328
               Right = 956
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 61
               Left = 1048
               Bottom = 191
               Right = 1349
            End
            DisplayFlags = 280
            TopColumn = 32
         End
         Begin Table = "bop2"
            Begin Extent = 
               Top = 67
               Left = 23
               Bottom = 186
               Right = 286
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fop"
            Begin Extent = 
               Top = 227
               Left = 317
               Bottom = 344
               Right = 548
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "o"
            Begin Extent = 
               Top = 229
               Left = 4
               Bottom = 348
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 5610
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3375
         Alias = 3285
         Table = 1170
', 'SCHEMA', N'dbo', 'VIEW', N'vwCompetencyDescription', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_DiagramPane2', N'         Output = 720
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
', 'SCHEMA', N'dbo', 'VIEW', N'vwCompetencyDescription', NULL, NULL
GO
DECLARE @xp int
SELECT @xp=2
EXEC sp_addextendedproperty N'MS_DiagramPaneCount', @xp, 'SCHEMA', N'dbo', 'VIEW', N'vwCompetencyDescription', NULL, NULL
GO
