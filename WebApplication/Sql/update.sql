--begin wangxiang 20120330
alter table item add plant varchar(50);

insert into codemstr values ('PlantType',1,10,1,'SW')
insert into codemstr values ('PlantType',2,20,0,'AB')
insert into codemstr values ('PlantType',3,30,0,'SB')
insert into codemstr values ('PlantType',4,40,0,'气袋')
insert into codemstr values ('PlantType',5,50,0,'皮革包覆')

alter table kp_order alter column InvoiceAmount decimal(18,8)
--end wangxiang 20120330

--begin wangxiang 20120326
insert into acc_permission values ('~/Main.aspx?mid=Mes.ByMaterial','借料','Production')
insert into acc_permission values ('~/Main.aspx?mid=Mes.Shelf','工位货架','Production')
insert into acc_permission values ('~/Main.aspx?mid=Mes.ProductLineUser','生产线用户分配','Production')
--end wangxiang 20120326

--begin wangxiang 20120216
alter table  kp_order add  InvoiceAmountWithoutTax decimal(18,8);
--end wangxiang 20120216

--begin wangxiang 20120109
--begin wangxiang 上回改回冲的
alter table OrderPlanBackflush add flow varchar(50);
update OrderPlanBackflush set flow=
m.flow from ipmstr m where OrderPlanBackflush.ipno=m.ipno
--end wangxiang
alter table location add IsAutoConfirm bit;
update location set IsAutoConfirm=0;
--end wangxiang 20120109

--begin wangxiang 20111228
alter table ordermstr add Version int;
update ordermstr set Version=1;
--end wangxiang 20111228

--begin wangxiang 20111116
insert into codemstr values ('FlowStrategy','Mes',70,0,'Mes')
alter table flowmstr alter column interval decimal(18,8)
--end wangxiang 20111116

--begin wangxiang 20111102
alter table kp_order add InvoiceCount int,InvoiceDate datetime,InvoiceTax varchar(50),InvoiceAmount decimal,InvoiceNumber text,InvoiceRemark varchar(100),InvoiceStatus varchar(50); 

insert into acc_permission values ('SubmitInvoice','提交发票','BillOperation');
insert into acc_permission values ('RejectInvoice','否决发票','BillOperation');
insert into acc_permission values ('ApproveInvoice','确认发票','BillOperation');

insert into codemstr values ('InvoiceStatus','In-Process',10,0,'提交');
insert into codemstr values ('InvoiceStatus','Rejected',20,0,'否决');
insert into codemstr values ('InvoiceStatus','Approved',30,0,'确认');
--end wangxiang 20111102

--begin wangxiang 20111010
alter table orderloctrans add  TagNo varchar(50),Shelf varchar(50),Cartons int;
update orderloctrans set cartons=0;
alter table item add IsMes bit,TFlag bit;
update item set ismes=0,tflag=0;

alter table hudet add TFlag bit;
update hudet set tflag=0;

alter table hudet add IsMes bit;
update hudet set IsMes=0;

alter table orderdet add TFlag bit;
update orderdet set tflag=0;

alter table ordermstr add IsAdditional bit;
update ordermstr set isadditional=0

alter table routingdet add tagNo varchar(50);
alter table flowmstr add ismes bit;
alter table flowmstr add Interval int;
alter table ordermstr add ismes bit;
--end wangxiang 20111010

--begin tiansu 20110926 
--2.	工位货架存放物料：工位货架号、物料号。

set IDENTITY_INSERT BatchJobDet on;
insert into BatchJobDet(Id,Name,Desc1,ServiceName) values(50,'MesInJob','Job of In data from Mes','MesInJob');
insert into BatchJobDet(Id,Name,Desc1,ServiceName) values(51,'MesOutJob','Job of Out data from Mes','MesOutJob');
set IDENTITY_INSERT BatchJobDet off;
go
set IDENTITY_INSERT BatchTrigger on;
insert into [BatchTrigger](Id,Name,Desc1,JobId,NextFireTime,PrevFireTime,RepeatCount,Interval,IntervalType,TimesTriggered,Status) values(50,'MesInTrigger','Trigger of Mes In',50,GETDATE(),GETDATE(),0,2,'Hours',0,'Pause');
insert into [BatchTrigger](Id,Name,Desc1,JobId,NextFireTime,PrevFireTime,RepeatCount,Interval,IntervalType,TimesTriggered,Status) values(51,'MesOutTrigger','Trigger of Mes Out',51,GETDATE(),GETDATE(),0,2,'Hours',0,'Pause');
set IDENTITY_INSERT BatchTrigger off;

insert into BatchJobParam(JobId,ParamName,ParamValue) values(50,'MES_SCMS_STATION_SHELF','ShelfMgr.service');
insert into BatchJobParam(JobId,ParamName,ParamValue) values(50,'MES_SCMS_SHELF_PART','ShelfItemMgr.service');
insert into BatchJobParam(JobId,ParamName,ParamValue) values(50,'MES_SCMS_COMPLETED_ORDER','OrderMgr.service');
insert into BatchJobParam(JobId,ParamName,ParamValue) values(50,'MES_SCMS_STATION_BOX','OrderLocationTransactionMgr.service');
insert into BatchJobParam(JobId,ParamName,ParamValue) values(50,'MES_SCMS_BOM','BomDetailMgr.service');
insert into BatchJobParam(JobId,ParamName,ParamValue) values(50,'MES_SCMS_COMPLETED_BOX','OrderMgr.service');
insert into BatchJobParam(JobId,ParamName,ParamValue) values(50,'MES_SCMS_REPAIR_MATERIAL','InspectOrderMgr.service');

insert into BatchJobParam(JobId,ParamName,ParamValue) values(51,'SCMS_PART','ItemMgr.service');
insert into BatchJobParam(JobId,ParamName,ParamValue) values(51,'SCMS_MATERIAL_BARCODE','HuMgr.service');
insert into BatchJobParam(JobId,ParamName,ParamValue) values(51,'SCMS_WO','OrderDetailMgr.service');
insert into BatchJobParam(JobId,ParamName,ParamValue) values(51,'SCMS_BOM','MesBomDetailMgr.service');
--end tiansu 20110926


update acc_permission set pm_code='~/Main.aspx?mid=MRP.Schedule.ProductionSchedule' where pm_code='~/Main.aspx?mid=MRP__mp--ModuleType-MPS';
update acc_permission set pm_code='~/Main.aspx?mid=MRP.Schedule.DemandSchedule' where pm_code='~/Main.aspx?mid=MRP__mp--ModuleType-MRP';

update codemstr set codevalue='Daily' where code='TimePeriodType' and codevalue='Day';
update codemstr set codevalue='Weekly' where code='TimePeriodType' and codevalue='Week';
update codemstr set codevalue='Monthly' where code='TimePeriodType' and codevalue='Month';
update codemstr set codevalue='Quarterly' where code='TimePeriodType' and codevalue='Quarter';
update codemstr set codevalue='Yearly' where code='TimePeriodType' and codevalue='Year';
update codemstr set codevalue='Hourly' where code='TimePeriodType' and codevalue='Hour';

alter table mrp_shipplan add sourceItem varchar(50);
alter table mrp_shipplan add sourceItemDesc varchar(50);
alter table mrp_recplan add sourceItem varchar(50);
alter table mrp_recplan add sourceItemDesc varchar(50);
USE [scms]
GO
/****** 对象:  View [dbo].[Mrp_ShipPlanView]    脚本日期: 08/31/2011 08:37:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Mrp_ShipPlanView]
AS
SELECT     TOP (100) PERCENT Flow, FlowType, Item, ItemDesc, ItemRef, BaseUom AS Uom, UC, LocTo AS Loc, CONVERT(dateTime, CONVERT(varchar(10), 
                      StartTime, 23)) AS StartTime, CONVERT(dateTime, CONVERT(varchar(10), WindowTime, 23)) AS WindowTime, SUM(Qty * UnitQty) AS Qty, EffDate
FROM         dbo.MRP_ShipPlan
GROUP BY Flow, FlowType, Item, ItemDesc, ItemRef, BaseUom, UC, LocTo, CONVERT(varchar(10), StartTime, 23), CONVERT(varchar(10), WindowTime, 23), 
                      EffDate
ORDER BY EffDate, Flow, Item, CONVERT(varchar(10), StartTime, 23)

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "MRP_ShipPlan"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 215
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
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
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
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Mrp_ShipPlanView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Mrp_ShipPlanView'


alter table item add defaultsupplier varchar(50);
alter table flowdet add routing varchar(50);
alter table flowdet add returnrouting varchar(50);

/****** 对象:  Table [dbo].[TransitInventory]    脚本日期: 08/12/2011 15:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TransitInventory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](50) NOT NULL,
	[Flow] [varchar](50) NOT NULL,
	[Loc] [varchar](50) NOT NULL,
	[Item] [varchar](50) NOT NULL,
	[Uom] [varchar](5) NOT NULL,
	[UC] [decimal](18, 8) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[WindowTime] [datetime] NOT NULL,
	[TransitQty] [decimal](18, 8) NOT NULL,
	[EffDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TransitInv] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TransitInventory]  WITH CHECK ADD  CONSTRAINT [FK_TransitInventory_FlowMstr] FOREIGN KEY([Flow])
REFERENCES [dbo].[FlowMstr] ([Code])
GO
ALTER TABLE [dbo].[TransitInventory] CHECK CONSTRAINT [FK_TransitInventory_FlowMstr]
GO
ALTER TABLE [dbo].[TransitInventory]  WITH CHECK ADD  CONSTRAINT [FK_TransitInventory_Item] FOREIGN KEY([Item])
REFERENCES [dbo].[Item] ([Code])
GO
ALTER TABLE [dbo].[TransitInventory] CHECK CONSTRAINT [FK_TransitInventory_Item]
GO
ALTER TABLE [dbo].[TransitInventory]  WITH CHECK ADD  CONSTRAINT [FK_TransitInventory_Location] FOREIGN KEY([Loc])
REFERENCES [dbo].[Location] ([Code])
GO
ALTER TABLE [dbo].[TransitInventory] CHECK CONSTRAINT [FK_TransitInventory_Location]
GO
ALTER TABLE [dbo].[TransitInventory]  WITH CHECK ADD  CONSTRAINT [FK_TransitInventory_OrderMstr] FOREIGN KEY([OrderNo])
REFERENCES [dbo].[OrderMstr] ([OrderNo])
GO
ALTER TABLE [dbo].[TransitInventory] CHECK CONSTRAINT [FK_TransitInventory_OrderMstr]
GO
ALTER TABLE [dbo].[TransitInventory]  WITH CHECK ADD  CONSTRAINT [FK_TransitInventory_Uom] FOREIGN KEY([Uom])
REFERENCES [dbo].[Uom] ([Code])
GO
ALTER TABLE [dbo].[TransitInventory] CHECK CONSTRAINT [FK_TransitInventory_Uom]
GO
/****** 对象:  View [dbo].[TransitInventoryView]    脚本日期: 08/12/2011 15:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TransitInventoryView]
WITH SCHEMABINDING 
AS
SELECT     MAX(Id) AS Id, Flow, Item, Uom, UC, Loc, CONVERT(dateTime, CONVERT(varchar(10), StartTime, 23)) AS StartTime, CONVERT(dateTime, CONVERT(varchar(10), 
                      WindowTime, 23)) AS WindowTime, SUM(TransitQty) AS TransitQty, EffDate
FROM         dbo.TransitInventory
GROUP BY Flow, Item, Uom, UC, Loc, StartTime, WindowTime, EffDate

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "TransitInventory"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 195
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
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransitInventoryView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransitInventoryView'
GO
/****** 对象:  Table [dbo].[CustScheduleDet]    脚本日期: 08/12/2011 09:01:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustScheduleDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScheduleId] [int] NOT NULL,
	[Item] [varchar](50) NOT NULL,
	[ItemDesc] [varchar](510) NULL,
	[ItemRef] [varchar](50) NULL,
	[Type] [varchar](50) NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTo] [datetime] NOT NULL,
	[Uom] [varchar](5) NOT NULL,
	[UC] [decimal](18, 8) NOT NULL,
	[Qty] [decimal](18, 8) NOT NULL,
	[Loc] [varchar](50) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[Bom] [varchar](50) NULL,
 CONSTRAINT [PK_CustScheduleDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CustScheduleDet]  WITH CHECK ADD  CONSTRAINT [FK_CustScheduleDet_BomMstr] FOREIGN KEY([Bom])
REFERENCES [dbo].[BomMstr] ([Code])
GO
ALTER TABLE [dbo].[CustScheduleDet] CHECK CONSTRAINT [FK_CustScheduleDet_BomMstr]
GO
ALTER TABLE [dbo].[CustScheduleDet]  WITH CHECK ADD  CONSTRAINT [FK_CustScheduleDet_CustScheduleMstr] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[CustScheduleMstr] ([Id])
GO
ALTER TABLE [dbo].[CustScheduleDet] CHECK CONSTRAINT [FK_CustScheduleDet_CustScheduleMstr]
GO
ALTER TABLE [dbo].[CustScheduleDet]  WITH CHECK ADD  CONSTRAINT [FK_CustScheduleDet_Item] FOREIGN KEY([Item])
REFERENCES [dbo].[Item] ([Code])
GO
ALTER TABLE [dbo].[CustScheduleDet] CHECK CONSTRAINT [FK_CustScheduleDet_Item]
GO
ALTER TABLE [dbo].[CustScheduleDet]  WITH CHECK ADD  CONSTRAINT [FK_CustScheduleDet_Location] FOREIGN KEY([Loc])
REFERENCES [dbo].[Location] ([Code])
GO
ALTER TABLE [dbo].[CustScheduleDet] CHECK CONSTRAINT [FK_CustScheduleDet_Location]
GO
ALTER TABLE [dbo].[CustScheduleDet]  WITH CHECK ADD  CONSTRAINT [FK_CustScheduleDet_Uom] FOREIGN KEY([Uom])
REFERENCES [dbo].[Uom] ([Code])
GO
ALTER TABLE [dbo].[CustScheduleDet] CHECK CONSTRAINT [FK_CustScheduleDet_Uom]
GO
/****** 对象:  Table [dbo].[CustScheduleMstr]    脚本日期: 08/12/2011 09:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustScheduleMstr](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefScheduleNo] [varchar](50) NULL,
	[Flow] [varchar](50) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](50) NOT NULL,
	[ReleaseDate] [datetime] NULL,
	[ReleaseUser] [varchar](50) NULL,
	[CancelDate] [datetime] NOT NULL,
	[CancelUser] [varchar](50) NOT NULL,
	[LastModifyDate] [datetime] NOT NULL,
	[LastModifyUser] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CustScheduleMstr] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CustScheduleMstr]  WITH CHECK ADD  CONSTRAINT [FK_CustScheduleMstr_Create_User] FOREIGN KEY([CreateUser])
REFERENCES [dbo].[ACC_User] ([USR_Code])
GO
ALTER TABLE [dbo].[CustScheduleMstr] CHECK CONSTRAINT [FK_CustScheduleMstr_Create_User]
GO
ALTER TABLE [dbo].[CustScheduleMstr]  WITH CHECK ADD  CONSTRAINT [FK_CustScheduleMstr_FlowMstr] FOREIGN KEY([Flow])
REFERENCES [dbo].[FlowMstr] ([Code])
GO
ALTER TABLE [dbo].[CustScheduleMstr] CHECK CONSTRAINT [FK_CustScheduleMstr_FlowMstr]
GO
ALTER TABLE [dbo].[CustScheduleMstr]  WITH CHECK ADD  CONSTRAINT [FK_CustScheduleMstr_LastModify_User] FOREIGN KEY([LastModifyUser])
REFERENCES [dbo].[ACC_User] ([USR_Code])
GO
ALTER TABLE [dbo].[CustScheduleMstr] CHECK CONSTRAINT [FK_CustScheduleMstr_LastModify_User]
GO
ALTER TABLE [dbo].[CustScheduleMstr]  WITH CHECK ADD  CONSTRAINT [FK_CustScheduleMstr_Release_User] FOREIGN KEY([ReleaseUser])
REFERENCES [dbo].[ACC_User] ([USR_Code])
GO
ALTER TABLE [dbo].[CustScheduleMstr] CHECK CONSTRAINT [FK_CustScheduleMstr_Release_User]
GO
/****** 对象:  Table [dbo].[MRP_ShipPlan]    脚本日期: 08/11/2011 15:18:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ShipPlan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Flow] [varchar](50) NOT NULL,
	[FlowType] [varchar](50) NOT NULL,
	[Item] [varchar](50) NOT NULL,
	[ItemDesc] [varchar](100) NULL,
	[ItemRef] [varchar](50) NULL,
	[Qty] [decimal](18, 8) NOT NULL,
	[BaseUom] [varchar](5) NOT NULL,
	[Uom] [varchar](5) NOT NULL,
	[UC] [decimal](18, 8) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[LocFrom] [varchar](50) NULL,
	[LocTo] [varchar](50) NULL,
	[SourceType] [varchar](50) NOT NULL,
	[SourceDateType] [varchar](50) NOT NULL,
	[SourceId] [varchar](50) NULL,
	[IsExpire] [bit] NOT NULL,
	[ExpireStartTime] [datetime] NULL,
	[EffDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](50) NOT NULL,
	[WindowTime] [datetime] NOT NULL,
	[Bom] [varchar](50) NULL,
	[Routing] [varchar](50) NULL,
	[UnitQty] [decimal](18, 8) NOT NULL,
	[SourceUnitQty] [decimal](18, 8) NOT NULL,
 CONSTRAINT [PK_MRP_ShipPlan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[MRP_ShipPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_ShipPlan_ACC_User] FOREIGN KEY([CreateUser])
REFERENCES [dbo].[ACC_User] ([USR_Code])
GO
ALTER TABLE [dbo].[MRP_ShipPlan] CHECK CONSTRAINT [FK_MRP_ShipPlan_ACC_User]
GO
ALTER TABLE [dbo].[MRP_ShipPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_ShipPlan_BaseUom] FOREIGN KEY([BaseUom])
REFERENCES [dbo].[Uom] ([Code])
GO
ALTER TABLE [dbo].[MRP_ShipPlan] CHECK CONSTRAINT [FK_MRP_ShipPlan_BaseUom]
GO
ALTER TABLE [dbo].[MRP_ShipPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_ShipPlan_BomMstr] FOREIGN KEY([Bom])
REFERENCES [dbo].[BomMstr] ([Code])
GO
ALTER TABLE [dbo].[MRP_ShipPlan] CHECK CONSTRAINT [FK_MRP_ShipPlan_BomMstr]
GO
ALTER TABLE [dbo].[MRP_ShipPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_ShipPlan_FlowMstr] FOREIGN KEY([Flow])
REFERENCES [dbo].[FlowMstr] ([Code])
GO
ALTER TABLE [dbo].[MRP_ShipPlan] CHECK CONSTRAINT [FK_MRP_ShipPlan_FlowMstr]
GO
ALTER TABLE [dbo].[MRP_ShipPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_ShipPlan_Item] FOREIGN KEY([Item])
REFERENCES [dbo].[Item] ([Code])
GO
ALTER TABLE [dbo].[MRP_ShipPlan] CHECK CONSTRAINT [FK_MRP_ShipPlan_Item]
GO
ALTER TABLE [dbo].[MRP_ShipPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_ShipPlan_Location_From] FOREIGN KEY([LocFrom])
REFERENCES [dbo].[Location] ([Code])
GO
ALTER TABLE [dbo].[MRP_ShipPlan] CHECK CONSTRAINT [FK_MRP_ShipPlan_Location_From]
GO
ALTER TABLE [dbo].[MRP_ShipPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_ShipPlan_Location_To] FOREIGN KEY([LocTo])
REFERENCES [dbo].[Location] ([Code])
GO
ALTER TABLE [dbo].[MRP_ShipPlan] CHECK CONSTRAINT [FK_MRP_ShipPlan_Location_To]
GO
ALTER TABLE [dbo].[MRP_ShipPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_ShipPlan_RoutingMstr] FOREIGN KEY([Routing])
REFERENCES [dbo].[RoutingMstr] ([Code])
GO
ALTER TABLE [dbo].[MRP_ShipPlan] CHECK CONSTRAINT [FK_MRP_ShipPlan_RoutingMstr]
GO
ALTER TABLE [dbo].[MRP_ShipPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_ShipPlan_Uom] FOREIGN KEY([Uom])
REFERENCES [dbo].[Uom] ([Code])
GO
ALTER TABLE [dbo].[MRP_ShipPlan] CHECK CONSTRAINT [FK_MRP_ShipPlan_Uom]
GO
/****** 对象:  Table [dbo].[MRP_RunLog]    脚本日期: 08/11/2011 15:18:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_RunLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RunDate] [datetime] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MRP_RunLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** 对象:  Table [dbo].[MRP_RecPlan]    脚本日期: 08/11/2011 15:16:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_RecPlan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Item] [varchar](50) NOT NULL,
	[ItemDesc] [varchar](100) NULL,
	[ItemRef] [varchar](50) NULL,
	[Qty] [decimal](18, 8) NOT NULL,
	[Uom] [varchar](5) NOT NULL,
	[UC] [decimal](18, 8) NOT NULL,
	[ReceiveTime] [datetime] NOT NULL,
	[Location] [varchar](50) NOT NULL,
	[SourceType] [varchar](50) NOT NULL,
	[SourceDateType] [varchar](50) NOT NULL,
	[SourceId] [varchar](50) NULL,
	[IsExpire] [bit] NOT NULL,
	[ExpireStartTime] [datetime] NULL,
	[EffDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [varchar](50) NOT NULL,
	[SourceUnitQty] [decimal](18, 8) NOT NULL,
 CONSTRAINT [PK_MRP_RecPlan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[MRP_RecPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_RecPlan_ACC_User] FOREIGN KEY([CreateUser])
REFERENCES [dbo].[ACC_User] ([USR_Code])
GO
ALTER TABLE [dbo].[MRP_RecPlan] CHECK CONSTRAINT [FK_MRP_RecPlan_ACC_User]
GO
ALTER TABLE [dbo].[MRP_RecPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_RecPlan_Item] FOREIGN KEY([Item])
REFERENCES [dbo].[Item] ([Code])
GO
ALTER TABLE [dbo].[MRP_RecPlan] CHECK CONSTRAINT [FK_MRP_RecPlan_Item]
GO
ALTER TABLE [dbo].[MRP_RecPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_RecPlan_Location] FOREIGN KEY([Location])
REFERENCES [dbo].[Location] ([Code])
GO
ALTER TABLE [dbo].[MRP_RecPlan] CHECK CONSTRAINT [FK_MRP_RecPlan_Location]
GO
ALTER TABLE [dbo].[MRP_RecPlan]  WITH CHECK ADD  CONSTRAINT [FK_MRP_RecPlan_Uom] FOREIGN KEY([Uom])
REFERENCES [dbo].[Uom] ([Code])
GO
ALTER TABLE [dbo].[MRP_RecPlan] CHECK CONSTRAINT [FK_MRP_RecPlan_Uom]
GO
alter table IpMstr add ArriveTime datetime;
alter table IpMstr add DepartTime datetime;
alter table InspectMstr add EstInspectDate datetime;
go
update IpMstr set DepartTime = CreateDate, ArriveTime = CreateDate;
update InspectMstr set EstInspectDate = CreateDate;
go
alter table IpMstr alter column ArriveTime datetime not null;
alter table IpMstr alter column DepartTime datetime not null;
alter table InspectMstr alter column EstInspectDate datetime not null;
go
alter table location add IsMRP bit;
alter table FlowMstr add MRPOpt varchar(50);
go
update location set IsMRP = 1
update FlowMstr set MRPOpt = 'OrderBeforePlan' where type = 'Production'
go
alter table location alter column IsMRP bit not null
go

alter table FlowMstr add IsMRP bit
go
update FlowMstr set IsMRP = 1
go
alter table FlowMstr alter column IsMRP bit not null 
go
alter table FlowDet add MRPWeight int;
go
update FlowDet set MRPWeight = 1;
go
alter table FlowDet alter column MRPWeight int not null;
go
insert into CodeMstr values('MRPOpt', 'OrderBeforePlan', 10, 1, '定单优先');
insert into CodeMstr values('MRPOpt', 'OrderOnly', 20, 0, '只看定单');
insert into CodeMstr values('MRPOpt', 'PlanOnly', 30, 0, '只看计划');

------------ 以上为MRP所用！-------------

---账单修改用

/****** Object:  View [dbo].[ActBillView]    Script Date: 07/20/2014 19:16:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ActBillView]
AS
SELECT     MAX(Id) AS Id, OrderNo, RecNo, ExtRecNo, TransType, BillAddr, Item, Uom, UC, EffDate, SUM(BillQty - ISNULL(BilledQty, 0)) AS Qty,Isprovest,0 as IsCreateBill
FROM         dbo.ActBill
WHERE     (BillQty > 0) AND (BillQty > BilledQty) OR
                      (BillQty < 0) AND (BillQty < BilledQty)
GROUP BY OrderNo, RecNo, ExtRecNo, TransType, BillAddr, Item, Uom, UC, EffDate,Isprovest

union all

SELECT     MAX(b.Id) AS Id, b.OrderNo, b.RecNo, b.ExtRecNo, m.TransType, b.BillAddr, b.Item, b.Uom, b.UC, b.EffDate,sum(d.BilledQty) as Qty,d.Isprovest,1 as IsCreateBill
FROM         dbo.BillDet d 
inner join dbo.BillMstr m on d.BillNo = m.BillNo
inner join dbo.ActBill b on d.TransId = b.Id
WHERE    m.isexport = 0 and m.TransType = 'SO' 
GROUP BY b.OrderNo, b.RecNo, b.ExtRecNo, m.TransType, b.BillAddr, b.Item, b.Uom, b.UC, b.EffDate,d.Isprovest

GO



insert into  codemstr values ('YesOrNo','Y',10,1,'是')
insert into  codemstr values ('YesOrNo','N',20,0,'否')