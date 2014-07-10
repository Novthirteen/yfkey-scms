alter table CustScheduleMstr add Type varchar(50) null
go
alter table CustScheduleMstr add Version int null
go
alter table CustScheduleDet add Version int null 
go
alter table CustScheduleDet add Flow varchar(50) null
go
alter table FlowMstr add ShipFlow varchar(50) null
go
alter table FlowMstr add MRPLeadTime decimal(18, 8) null
go
alter table CustScheduleDet add ShipQty decimal(18, 8) null
go
alter table CustScheduleDet add FordPlanId int null
go
alter table CustScheduleMstr add ShipFlow varchar(50) null
go
alter table Item add LeadTime int
go
alter table Item add SafeStock int
go
alter table Region add Plant varchar(50)
go
alter table Location add IsFG bit
go
alter table Item add MaxStock int
go
alter table Item add IsMRP bit
go
alter table Item add MinLotSize int
go
alter table FlowMstr add MrpCode varchar(50) null
go
alter table FlowMstr add DateFst int
go
alter table FlowMstr add WorkDate varchar(50)
go
/****** Object:  Table [dbo].[MRP_WeeklyShipPlan]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_WeeklyShipPlan]
GO
/****** Object:  Table [dbo].[MRP_WeeklyPurchasePlanOpenOrder]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_WeeklyPurchasePlanOpenOrder]
GO
/****** Object:  Table [dbo].[MRP_WeeklyPurchasePlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_WeeklyPurchasePlanMstr]
GO
/****** Object:  Table [dbo].[MRP_WeeklyPurchasePlanDet]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_WeeklyPurchasePlanDet]
GO
/****** Object:  Table [dbo].[MRP_WeeklyProductionPlan]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_WeeklyProductionPlan]
GO
/****** Object:  Table [dbo].[MRP_ShipPlanOpenOrder]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ShipPlanOpenOrder]
GO
/****** Object:  Table [dbo].[MRP_ShipPlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ShipPlanMstr]
GO
/****** Object:  Table [dbo].[MRP_ShipPlanIpDet]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ShipPlanIpDet]
GO
/****** Object:  Table [dbo].[MRP_ShipPlanInitLocationDet]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ShipPlanInitLocationDet]
GO
/****** Object:  Table [dbo].[MRP_ShipPlanDetTrace]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ShipPlanDetTrace]
GO
/****** Object:  Table [dbo].[MRP_ShipPlanDet]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ShipPlanDet]
GO
/****** Object:  Table [dbo].[MRP_ShiftPlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ShiftPlanMstr]
GO
/****** Object:  Table [dbo].[MRP_ShiftPlanDet]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ShiftPlanDet]
GO
/****** Object:  Table [dbo].[MRP_RunWeeklyMRPLog]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_RunWeeklyMRPLog]
GO
/****** Object:  Table [dbo].[MRP_RunShipPlanLog]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_RunShipPlanLog]
GO
/****** Object:  Table [dbo].[MRP_RunPurchasePlanLog]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_RunPurchasePlanLog]
GO
/****** Object:  Table [dbo].[MRP_RunProductionPlanLog]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_RunProductionPlanLog]
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanOpenOrder]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_PurchasePlanOpenOrder]
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_PurchasePlanMstr]
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanInitLocationDet]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_PurchasePlanInitLocationDet]
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanDetTrace]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_PurchasePlanDetTrace]
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanDet]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_PurchasePlanDet]
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanOpenOrder]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ProductionPlanOpenOrder]
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ProductionPlanMstr]
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanInitLocationDet]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ProductionPlanInitLocationDet]
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanDetTrace]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ProductionPlanDetTrace]
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanDet]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_ProductionPlanDet]
GO
/****** Object:  Table [dbo].[MRP_OpenOrderSnapShot]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_OpenOrderSnapShot]
GO
/****** Object:  Table [dbo].[MRP_LocationDetSnapShot]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_LocationDetSnapShot]
GO
/****** Object:  Table [dbo].[MRP_IpDetSnapShot]    Script Date: 2014/7/10 11:18:47 ******/
DROP TABLE [dbo].[MRP_IpDetSnapShot]
GO
/****** Object:  Table [dbo].[MRP_IpDetSnapShot]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_IpDetSnapShot](
	[IpDetId] [int] NOT NULL,
	[IpNo] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[Plant] [varchar](50) NULL,
	[Qty] [decimal](18, 8) NULL,
	[EffDate] [datetime] NULL,
 CONSTRAINT [PK_MRP_IpDetSnapShot] PRIMARY KEY CLUSTERED 
(
	[IpDetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_LocationDetSnapShot]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_LocationDetSnapShot](
	[Item] [varchar](50) NOT NULL,
	[Location] [varchar](50) NOT NULL,
	[Plant] [varchar](50) NULL,
	[Qty] [decimal](18, 8) NULL,
	[InTransitQty] [decimal](18, 8) NULL,
	[PurchaseInTransitQty] [decimal](18, 8) NULL,
	[InspectQty] [decimal](18, 8) NULL,
	[EffDate] [datetime] NULL,
 CONSTRAINT [PK_MRP_LocationDetSnapShot] PRIMARY KEY CLUSTERED 
(
	[Item] ASC,
	[Location] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_OpenOrderSnapShot]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_OpenOrderSnapShot](
	[OrderNo] [varchar](50) NOT NULL,
	[Item] [varchar](50) NOT NULL,
	[Flow] [varchar](50) NULL,
	[OrderType] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[OrderQty] [decimal](18, 8) NULL,
	[ShipQty] [decimal](18, 8) NULL,
	[RecQty] [decimal](18, 8) NULL,
	[EffDate] [datetime] NULL,
 CONSTRAINT [PK_MRP_OpenOrderSnapShot] PRIMARY KEY CLUSTERED 
(
	[OrderNo] ASC,
	[Item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanDet]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ProductionPlanDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductionPlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[ItemDesc] [varchar](50) NULL,
	[RefItemCode] [varchar](50) NULL,
	[ReqQty] [decimal](18, 8) NULL,
	[OrgQty] [decimal](18, 8) NULL,
	[Qty] [decimal](18, 8) NULL,
	[OrderQty] [decimal](18, 8) NULL,
	[Uom] [varchar](5) NULL,
	[UC] [decimal](18, 8) NULL,
	[MinLotSize] [decimal](18, 8) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[LastModifyUser] [varchar](50) NULL,
	[LastModifyDate] [datetime] NULL,
	[Version] [int] NULL,
	
 CONSTRAINT [PK_MRP_ProductionPlanDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanDetTrace]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ProductionPlanDetTrace](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductionPlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[Bom] [varchar](50) NULL,
	[ReqDate] [datetime] NULL,
	[ReqQty] [decimal](18, 8) NULL,
	[RateQty] [decimal](18, 8) NULL,
	[ScrapPct] [decimal](18, 8) NULL,
	[Uom] [varchar](5) NULL,
	[UnitQty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_ProductionPlanDetTrace] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanInitLocationDet]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ProductionPlanInitLocationDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductionPlanId] [int] NULL,
	[Item] [varchar](50) NULL,
	[InitStock] [decimal](18, 8) NULL,
	[SafeStock] [decimal](18, 8) NULL,
	[MaxStock] [decimal](18, 8) NULL,
	[InTransitQty] [decimal](18, 8) NULL,
	[InspectQty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_ProductionPlanInitLocationDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ProductionPlanMstr](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReleaseNo] [int] NULL,
	[BatchNo] [int] NULL,
	[EffDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_ProdutcionPlanMstr] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanOpenOrder]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ProductionPlanOpenOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductionPlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[OrderNo] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[OrderQty] [decimal](18, 8) NULL,
	[RecQty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_ProductionPlanOpenOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanDet]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_PurchasePlanDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PurchasePlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[ItemDesc] [varchar](100) NULL,
	[RefItemCode] [varchar](50) NULL,
	[ReqQty] [decimal](18, 8) NULL,
	[OrgPurchaseQty] [decimal](18, 8) NULL,
	[PurchaseQty] [decimal](18, 8) NULL,
	[OrderQty] [decimal](18, 8) NULL,
	[Uom] [varchar](5) NULL,
	[BaseUom] [varchar](5) NULL,
	[UnitQty] [decimal](18, 8) NULL,
	[UC] [decimal](18, 8) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyUser] [varchar](50) NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_MRP_PurchasePlanDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanDetTrace]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_PurchasePlanDetTrace](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PurchasePlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[ShiftPlanDetId] [int] NULL,
	[ShiftPlanMstrId] [int] NULL,
	[RefPlanNo] [varchar](50) NULL,
	[ProdLine] [varchar](50) NULL,
	[ProdItem] [varchar](50) NULL,
	[ProdQty] [decimal](18, 8) NULL,
	[RateQty] [decimal](18, 8) NULL,
	[ScrapPct] [decimal](18, 8) NULL,
	[BomUnitQty] [decimal](18, 8) NULL,
	[PurchaseUnitQty] [decimal](18, 8) NULL,
	[BomUom] [varchar](5) NULL,
	[PurchaseUom] [varchar](5) NULL,
	[PlanDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_PurchasePlanDetTrace] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanInitLocationDet]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_PurchasePlanInitLocationDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PurchasePlanId] [int] NULL,
	[Item] [varchar](50) NULL,
	[InitStock] [decimal](18, 8) NULL,
	[SafeStock] [decimal](18, 8) NULL,
	[MaxStock] [decimal](18, 8) NULL,
	[InTransitQty] [decimal](18, 8) NULL,
	[InspectQty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_PurchasePlanInitLocationDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_PurchasePlanMstr](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReleaseNo] [int] NULL,
	[BatchNo] [int] NULL,
	[EffDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyUser] [varchar](50) NULL,
	[ReleaseDate] [datetime] NULL,
	[ReleaseUser] [varchar](50) NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_MRP_PurchasePlanMstr] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_PurchasePlanOpenOrder]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_PurchasePlanOpenOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PurchasePlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[OrderNo] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[OrderQty] [decimal](18, 8) NULL,
	[ShipQty] [decimal](18, 8) NULL,
	[RecQty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_PurchasePlanOpenOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_RunProductionPlanLog]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_RunProductionPlanLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchNo] [int] NULL,
	[Lvl] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[Bom] [varchar](50) NULL,
	[EffDate] [datetime] NULL,
	[Msg] [varchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_RunProductionPlanLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_RunPurchasePlanLog]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_RunPurchasePlanLog](
	[Id] [int] NULL,
	[BatchNo] [int] NULL,
	[Lvl] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[Uom] [varchar](50) NULL,
	[Qty] [decimal](18, 8) NULL,
	[PlanDate] [datetime] NULL,
	[Bom] [varchar](50) NULL,
	[Msg] [varchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_RunShipPlanLog]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_RunShipPlanLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchNo] [int] NULL,
	[EffDate] [datetime] NULL,
	[Lvl] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[Qty] [decimal](18, 8) NULL,
	[Uom] [varchar](5) NULL,
	[LocFrom] [varchar](50) NULL,
	[LocTo] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[Msg] [varchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_RunShipPlanLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_RunWeeklyMRPLog]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_RunWeeklyMRPLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchNo] [int] NULL,
	[Lvl] [varchar](50) NULL,
	[Phase] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[Msg] [varchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_RunWeeklyMRPLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ShiftPlanDet]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ShiftPlanDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlanId] [int] NULL,
	[RefPlanNo] [varchar](50) NULL,
	[ProdLine] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[ItemDesc] [varchar](100) NULL,
	[RefItemCode] [varchar](50) NULL,
	[Qty] [decimal](18, 8) NULL,
	[Uom] [varchar](5) NULL,
	[UnitQty] [decimal](18, 8) NULL,
	[PlanDate] [datetime] NULL,
	[Shift] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_ShiftPlanDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ShiftPlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ShiftPlanMstr](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefPlanNo] [varchar](50) NULL,
	[ProdLine] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[ReleaseDate] [datetime] NULL,
	[ReleaseUser] [varchar](50) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyUser] [varchar](50) NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_MRP_ShiftPlanMstr] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ShipPlanDet]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ShipPlanDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShipPlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[ItemDesc] [varchar](50) NULL,
	[RefItemCode] [varchar](50) NULL,
	[ReqQty] [decimal](18, 8) NULL,
	[OrgShipQty] [decimal](18, 8) NULL,
	[ShipQty] [decimal](18, 8) NULL,
	[OrderQty] [decimal](18, 8) NULL,
	[Uom] [varchar](5) NULL,
	[BaseUom] [varchar](5) NULL,
	[UnitQty] [decimal](18, 8) NULL,
	[UC] [decimal](18, 8) NULL,
	[LocFrom] [varchar](50) NULL,
	[LocTo] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyUser] [varchar](50) NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_MRP_ShipPlanDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ShipPlanDetTrace]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ShipPlanDetTrace](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShipPlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[DistributionFlow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[ReqDate] [datetime] NULL,
	[ReqQty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_ShipPlanDetTrace] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ShipPlanInitLocationDet]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ShipPlanInitLocationDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShipPlanId] [int] NULL,
	[Location] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[InitStock] [decimal](18, 8) NULL,
	[SafeStock] [decimal](18, 8) NULL,
	[MaxStock] [decimal](18, 8) NULL,
	[InTransitQty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_ShipPlanInitLocationDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ShipPlanIpDet]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ShipPlanIpDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShipPlanId] [int] NULL,
	[IpNo] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[Plant] [varchar](50) NULL,
	[Qty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_ShipPlanIpDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ShipPlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ShipPlanMstr](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReleaseNo] [int] NULL,
	[BatchNo] [int] NULL,
	[EffDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyUser] [varchar](50) NULL,
	[ReleaseDate] [datetime] NULL,
	[ReleaseUser] [varchar](50) NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_MRP_ShipPlanMstr] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_ShipPlanOpenOrder]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ShipPlanOpenOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShipPlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[OrderNo] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[OrgStartTime] [datetime] NULL,
	[OrgWindowTime] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[OrgStartTime] [datetime] NULL,
	[OrgWindowTime] [datetime] NULL,
	[OrderQty] [decimal](18, 8) NULL,
	[ShipQty] [decimal](18, 8) NULL,
	[RecQty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_ShipPlanOpenOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_WeeklyProductionPlan]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_WeeklyProductionPlan](
	[UUID] [varchar](50) NOT NULL,
	[Item] [varchar](50) NULL,
	[ItemDesc] [varchar](100) NULL,
	[RefItemCode] [varchar](50) NULL,
	[Uom] [varchar](5) NULL,
	[Qty] [decimal](18, 8) NULL,
	[Bom] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
 CONSTRAINT [PK_MRP_WeeklyProductionPlan] PRIMARY KEY CLUSTERED 
(
	[UUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_WeeklyPurchasePlanDet]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_WeeklyPurchasePlanDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PurchasePlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[ItemDesc] [varchar](100) NULL,
	[RefItemCode] [varchar](50) NULL,
	[ReqQty] [decimal](18, 8) NULL,
	[OrgPurchaseQty] [decimal](18, 8) NULL,
	[PurchaseQty] [decimal](18, 8) NULL,
	[OrderQty] [decimal](18, 8) NULL,
	[Uom] [varchar](5) NULL,
	[BaseUom] [varchar](5) NULL,
	[UnitQty] [decimal](18, 8) NULL,
	[UC] [decimal](18, 8) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyUser] [varchar](50) NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_MRP_WeeklyPurchasePlanDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_WeeklyPurchasePlanMstr]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_WeeklyPurchasePlanMstr](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReleaseNo] [int] NULL,
	[BatchNo] [int] NULL,
	[EffDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[LastModifyDate] [datetime] NULL,
	[LastModifyUser] [varchar](50) NULL,
	[ReleaseDate] [datetime] NULL,
	[ReleaseUser] [varchar](50) NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_MRP_WeeklyPurchasePlanMstr] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_WeeklyPurchasePlanOpenOrder]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_WeeklyPurchasePlanOpenOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PurchasePlanId] [int] NULL,
	[UUID] [varchar](50) NULL,
	[Flow] [varchar](50) NULL,
	[OrderNo] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
	[OrderQty] [decimal](18, 8) NULL,
	[ShipQty] [decimal](18, 8) NULL,
	[RecQty] [decimal](18, 8) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
 CONSTRAINT [PK_MRP_WeeklyPurchasePlanOpenOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MRP_WeeklyShipPlan]    Script Date: 2014/7/10 11:18:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_WeeklyShipPlan](
	[UUID] [varchar](50) NOT NULL,
	[Flow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[ItemDesc] [varchar](50) NULL,
	[RefItemCode] [varchar](50) NULL,
	[ReqQty] [decimal](18, 8) NULL,
	[Uom] [varchar](5) NULL,
	[BaseUom] [varchar](5) NULL,
	[UnitQty] [decimal](18, 8) NULL,
	[UC] [decimal](18, 8) NULL,
	[LocFrom] [varchar](50) NULL,
	[LocTo] [varchar](50) NULL,
	[StartTime] [datetime] NULL,
	[WindowTime] [datetime] NULL,
 CONSTRAINT [PK_MRP_WeeklyShipPlan] PRIMARY KEY CLUSTERED 
(
	[UUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
