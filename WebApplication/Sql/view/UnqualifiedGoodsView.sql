USE [sconit]
GO
/****** 对象:  View [dbo].[UnqualifiedGoodsView]    脚本日期: 08/19/2010 10:49:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UnqualifiedGoodsView]
AS
SELECT     dbo.InspectDet.InspNo, SUM(dbo.InspectDet.InspQty) AS InspQty, SUM(dbo.InspectDet.QualifyQty) AS QualifyQty, SUM(dbo.InspectDet.RejectQty) 
                      AS RejectQty, dbo.InspectDet.Disposition, dbo.LocationLotDet.Item, MAX(dbo.InspectDet.Id) AS Id, dbo.InspectDet.DefectClassification, 
                      dbo.InspectDet.FGCode, dbo.InspectDet.LocFrom
FROM         dbo.InspectDet INNER JOIN
                      dbo.LocationLotDet ON dbo.InspectDet.LocLotDetId = dbo.LocationLotDet.Id
GROUP BY dbo.InspectDet.InspNo, dbo.InspectDet.InspQty, dbo.InspectDet.Disposition, dbo.LocationLotDet.Item, dbo.InspectDet.DefectClassification, 
                      dbo.InspectDet.FGCode, dbo.InspectDet.LocFrom
HAVING      (SUM(dbo.InspectDet.RejectQty) > 0)

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[36] 4[21] 2[33] 3) )"
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
         Top = -112
         Left = 0
      End
      Begin Tables = 
         Begin Table = "InspectDet"
            Begin Extent = 
               Top = 36
               Left = 122
               Bottom = 269
               Right = 367
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LocationLotDet"
            Begin Extent = 
               Top = 33
               Left = 420
               Bottom = 258
               Right = 703
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
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1770
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UnqualifiedGoodsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UnqualifiedGoodsView'