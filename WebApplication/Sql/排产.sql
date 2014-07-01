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
CREATE TABLE [dbo].[MRP_ShipPlanDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShipPlanId] [int] NULL,
	[Flow] [varchar](50) NULL,
	[Item] [varchar](50) NULL,
	[ItemDesc] [varchar](50) NULL,
	[RefItemCode] [varchar](50) NULL,
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
alter table Item add LeadTime int
go
alter table Item add SafeStock int
go
alter table Region add Plant varchar(50)
go
