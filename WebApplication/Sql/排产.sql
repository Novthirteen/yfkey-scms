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

/****** Object:  Table [dbo].[MRP_ShipPlanMstr]    Script Date: 2014/7/2 9:30:43 ******/
DROP TABLE [dbo].[MRP_ShipPlanMstr]
GO
/****** Object:  Table [dbo].[MRP_ShipPlanInitLocationDet]    Script Date: 2014/7/2 9:30:43 ******/
DROP TABLE [dbo].[MRP_ShipPlanInitLocationDet]
GO
/****** Object:  Table [dbo].[MRP_ShipPlanDetTrace]    Script Date: 2014/7/2 9:30:43 ******/
DROP TABLE [dbo].[MRP_ShipPlanDetTrace]
GO
/****** Object:  Table [dbo].[MRP_ShipPlanDet]    Script Date: 2014/7/2 9:30:43 ******/
DROP TABLE [dbo].[MRP_ShipPlanDet]
GO
/****** Object:  Table [dbo].[MRP_RunShipPlanLog]    Script Date: 2014/7/2 9:30:43 ******/
DROP TABLE [dbo].[MRP_RunShipPlanLog]
GO
/****** Object:  Table [dbo].[MRP_RunProductionPlanLog]    Script Date: 2014/7/2 9:30:43 ******/
DROP TABLE [dbo].[MRP_RunProductionPlanLog]
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanMstr]    Script Date: 2014/7/2 9:30:43 ******/
DROP TABLE [dbo].[MRP_ProductionPlanMstr]
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanDet]    Script Date: 2014/7/2 9:30:43 ******/
DROP TABLE [dbo].[MRP_ProductionPlanDet]
GO
/****** Object:  Table [dbo].[MRP_ProductionPlanDet]    Script Date: 2014/7/2 9:30:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRP_ProductionPlanDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductionPlanId] [int] NULL,
	[Item] [varchar](50) NULL,
	[ItemDesc] [varchar](50) NULL,
	[RefItemCode] [varchar](50) NULL,
	[OrgQty] [decimal](18, 8) NULL,
	[Qty] [decimal](18, 8) NULL,
	[Uom] [varchar](5) NULL,
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
/****** Object:  Table [dbo].[MRP_ProductionPlanMstr]    Script Date: 2014/7/2 9:30:43 ******/
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
/****** Object:  Table [dbo].[MRP_RunProductionPlanLog]    Script Date: 2014/7/2 9:30:43 ******/
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
/****** Object:  Table [dbo].[MRP_RunShipPlanLog]    Script Date: 2014/7/2 9:30:43 ******/
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
/****** Object:  Table [dbo].[MRP_ShipPlanDet]    Script Date: 2014/7/2 9:30:43 ******/
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
/****** Object:  Table [dbo].[MRP_ShipPlanDetTrace]    Script Date: 2014/7/2 9:30:43 ******/
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
/****** Object:  Table [dbo].[MRP_ShipPlanInitLocationDet]    Script Date: 2014/7/2 9:30:43 ******/
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
/****** Object:  Table [dbo].[MRP_ShipPlanMstr]    Script Date: 2014/7/2 9:30:43 ******/
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

/****** Object:  Table [dbo].[MRP_ShiftPlanMstr]    Script Date: 2014/7/2 10:28:08 ******/
DROP TABLE [dbo].[MRP_ShiftPlanMstr]
GO

/****** Object:  Table [dbo].[MRP_ShiftPlanMstr]    Script Date: 2014/7/2 10:28:08 ******/
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


/****** Object:  Table [dbo].[MRP_ShiftPlanDet]    Script Date: 2014/7/2 10:35:03 ******/
DROP TABLE [dbo].[MRP_ShiftPlanDet]
GO

/****** Object:  Table [dbo].[MRP_ShiftPlanDet]    Script Date: 2014/7/2 10:35:03 ******/
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

