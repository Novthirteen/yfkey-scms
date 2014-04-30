
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_FORD_EDI_830]') AND type in (N'U'))
DROP TABLE [dbo].[TEMP_FORD_EDI_830]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TEMP_FORD_EDI_830](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	BatchNo	int not null,
	[Message_Type_Code] [nvarchar](255) NULL,
	[Message_Type] [nvarchar](255) NULL,
	[Sender_ID_Title] [nvarchar](255) NULL,
	[Sender_ID] [nvarchar](255) NULL,
	[Receiver_ID_Title] [nvarchar](255) NULL,
	[Receiver_ID] [nvarchar](255) NULL,
	[Interchange_Control_Num] [nvarchar](255) NULL,
	[Message_Release_Num] [nvarchar](255) NULL,
	[Message_Release_Date] [nvarchar](255) NULL,
	[Message_Purpose] [nvarchar](255) NULL,
	[Schedule_Type] [nvarchar](255) NULL,
	[Horizon_Start_Date] [nvarchar](255) NULL,
	[Horizon_End_Date] [nvarchar](255) NULL,
	[Comment_Note] [nvarchar](255) NULL,
	[Ship_To_GSDB_Code] [nvarchar](255) NULL,
	[Ship_From_GSDB_Code] [nvarchar](255) NULL,
	[Intermediate_Consignee] [nvarchar](255) NULL,
	[Part_Num] [nvarchar](255) NULL,
	[Purchase_Order_Num] [nvarchar](255) NULL,
	[Part_Release_Status] [nvarchar](255) NULL,
	[Dock_Code] [nvarchar](255) NULL,
	[Line_Feed] [nvarchar](255) NULL,
	[Reserve_Line_Feed] [nvarchar](255) NULL,
	[Contact_Name] [nvarchar](255) NULL,
	[Contact_Telephone] [nvarchar](255) NULL,
	[Fab_Auth_Qty] [nvarchar](255) NULL,
	[Fab_Auth_Start_Date] [nvarchar](255) NULL,
	[Fab_Auth_End_Date] [nvarchar](255) NULL,
	[Mat_Auth_Qty] [nvarchar](255) NULL,
	[Mat_Auth_Start_Date] [nvarchar](255) NULL,
	[Mat_Auth_End_Date] [nvarchar](255) NULL,
	[Last_Received_ASN_Num] [nvarchar](255) NULL,
	[Last_Shipped_Qty] [nvarchar](255) NULL,
	[Last_Shipped_Date] [nvarchar](255) NULL,
	[Cum_Shipped_Qty] [nvarchar](255) NULL,
	[Cum_Start_Date] [nvarchar](255) NULL,
	[Cum_End_Date] [nvarchar](255) NULL,
	[Forecast_Cum_Qty] [nvarchar](255) NULL,
	[Forecast_Net_Qty] [nvarchar](255) NULL,
	[UOM] [nvarchar](255) NULL,
	[Forecast_Status] [nvarchar](255) NULL,
	[Forecast_Date] [nvarchar](255) NULL,
	[Flexible_Forcast_Start_Date] [nvarchar](255) NULL,
	[Flexible_Forcast_End_Date] [nvarchar](255) NULL,
	[Forecast_Date_Qual_r] [nvarchar](255) NULL,
	CreateDate	datetime  not null,
	CreateUserName	varchar(255)  not null,
	IsHandle		bit   not null default(0),
	ReadFileName   varchar(255)  not null,
 CONSTRAINT [PK_TEMP_FORD_EDI_830] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

----------------------------
---------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_FORD_EDI_856]') AND type in (N'U'))
DROP TABLE [dbo].[TEMP_FORD_EDI_856]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TEMP_FORD_EDI_856](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BatchNo] [int] NOT NULL,
	[Message_Type_Code] [nvarchar](255) NULL,
	[Message_Type] [nvarchar](255) NULL,
	ReleaseVersion [nvarchar](255) NULL,
	[Sender_ID] [nvarchar](255) NULL,
	[Receiver_ID] [nvarchar](255) NULL,
	[Interchange_Control_Num] [varchar](255) NULL,
	[ASN_Creation_DateTime] [varchar](255) NULL,
	[Ship_To_GSDB_Code] [varchar](255) NULL,
	[Ship_From_GSDB_Code] [varchar](255) NULL,
	[Intermediate_Consignee_Code] [varchar](255) NULL,
	[Message_Purpose_Code] [varchar](255) NULL,
	[Shipment_ID] [varchar](255) NULL,
	[Shipped_DateTime] [varchar](255) NULL,
	[Gross_Weight] [varchar](255) NULL,
	[Net_Weight] [varchar](255) NULL,
	[UOM] [varchar](255) NULL,
	[Packaging_Type_Code] [varchar](255) NULL,
	[Lading_Qty] [varchar](255) NULL,
	[Carrier_SCAC_Code] [varchar](255) NULL,
	[Transportation_Method_Code] [varchar](255) NULL,
	[Equipment_Desc_Code] [varchar](255) NULL,
	[Equipment_Num] [varchar](255) NULL,
	[LadingNum] [varchar](255) NULL,
	[Part_Num] [varchar](255) NULL,
	[Purchase_Order_Num] [varchar](255) NULL,
	[Shipped_Qty] [varchar](255) NULL,
	[Cum_Shipped_Qty] [varchar](255) NULL,
	[Cum_Shipped_UOM] [varchar](255) NULL,
	[Number_of_Loads] [varchar](255) NULL,
	[Qty_Per_Load] [varchar](255) NULL,
	[Packaging_Code] [varchar](255) NULL,
	[Airport_Code] [varchar](255) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserName] [varchar](255) NOT NULL,
	[IsHandle] [bit] NOT NULL,
	[ReadFileName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TEMP_FORD_EDI_856] ADD  DEFAULT ((0)) FOR [IsHandle]
GO


---------------------------
---------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_FORD_EDI_862]') AND type in (N'U'))
DROP TABLE [dbo].[TEMP_FORD_EDI_862]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TEMP_FORD_EDI_862](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BatchNo] [int] NOT NULL,
	[Message_Type_Code] [nvarchar](255) NULL,
	[Message_Type] [nvarchar](255) NULL,
	[Sender_ID_Title] [nvarchar](255) NULL,
	[Sender_ID] [nvarchar](255) NULL,
	[Receiver_ID_Title] [nvarchar](255) NULL,
	[Receiver_ID] [nvarchar](255) NULL,
	[Interchange_Control_Num] [varchar](255) NULL,
	[Message_Release_Num] [varchar](255) NULL,
	[Message_Release_Date] [varchar](255) NULL,
	[Message_Purpose] [varchar](255) NULL,
	[Schedule_Type] [varchar](255) NULL,
	[Horizon_Start_Date] [varchar](255) NULL,
	[Horizon_End_Date] [varchar](255) NULL,
	[Message_Reference_Num] [varchar](255) NULL,
	[Ship_To_GSDB_Code] [varchar](255) NULL,
	[Ship_From_GSDB_Code] [varchar](255) NULL,
	[Intermediate_Consignee] [varchar](255) NULL,
	[Part_Num] [varchar](255) NULL,
	[Purchase_Order_Num] [varchar](255) NULL,
	[Dock_Code] [varchar](255) NULL,
	[Line_Feed] [varchar](255) NULL,
	[Reserve_Line_Feed] [varchar](255) NULL,
	[Contact_Name] [varchar](255) NULL,
	[Contact_Telephone] [varchar](255) NULL,
	[Last_Received_ASN_Num] [varchar](255) NULL,
	[Last_Shipped_Qty] [varchar](255) NULL,
	[Last_Shipped_Date] [varchar](255) NULL,
	[Cum_Shipped_Qty] [varchar](255) NULL,
	[Cum_Start_Date] [varchar](255) NULL,
	[Cum_End_Date] [varchar](255) NULL,
	[Forecast_Cum_Qty] [varchar](255) NULL,
	[Forecast_Net_Qty] [varchar](255) NULL,
	[UOM] [varchar](255) NULL,
	[Forecast_Status] [varchar](255) NULL,
	[Forecast_Date] [varchar](255) NULL,
	[Forecast_Time] [varchar](255) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserName] [varchar](255) NOT NULL,
	[IsHandle] [bit] NOT NULL,
	[ReadFileName] [varchar](255) NOT NULL,
	
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TEMP_FORD_EDI_862] ADD  DEFAULT ((0)) FOR [IsHandle]
GO
-------------------
----------------

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EDI_FordPlan]') AND type in (N'U'))
DROP TABLE [dbo].[EDI_FordPlan]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
create table EDI_FordPlan 
(
[Id] [bigint] IDENTITY(1,1) PRIMARY key,
TempId	int not null, --临时文件Id
Control_Num varchar(50) not null, --文件版本
BatchNo	int not null, --les版本
SupplierCode varchar(50) not null, --供应商代码
CustomerCode varchar(50) not null, --客户代码
ReleaseIssueDate datetime not null, --文件开始日期
Item varchar(50)  null,  --物料代码
ItemDesc varchar(100)  null,  --物料描述
RefItem varchar(50) not null,  --福特物料号
Uom varchar(50) not null,  --单位
LastShippedQuantity decimal not null,  --最近一次收到的ASN中发货净值
LastShippedCumulative decimal not null,--最近一次收到的ASN中累计发货量
LastShippedDate datetime not null,--最近一次收到的ASN中发货日期
DockCode varchar(50) null, --收货点
LineFeed varchar(50) null,--收货地
StorageLocation varchar(50) null,--存储仓库
IntermediateConsignee varchar(50) null,--中间商
ForecastQty decimal not null,--本发货周的需求净值
ForecastCumQty decimal not null,--截至至本发货周的累计需求量
ForecastDate datetime not null,--发货需求日期
CreateDate	datetime  not null,
CreateUserName	varchar(50)  not null,
[Type] varchar(50)  not null,
PurchaseOrder varchar(50) null,--采购订单号
CurrenCumQty decimal not null,


)



------------------------------
------------------------------ 配置项
--insert into EntityOpt values('SourceFilePath','D:\\CovisintHttpsClient_v3.2_EP4TA\\fromcovisint','读取EDI文件目录',1)
--go
--insert into EntityOpt values('BakFilePath','D:\\CovisintHttpsClient_v3.2_EP4TA\\backup','读取EDI文件成功备份目录',1)
--go
--insert into EntityOpt values('ErrorFilePath','D:\\error','读取EDI文件失败备份目录',1)
--go
--insert into EntityOpt values('BatPath','D:\\CovisintHttpsClient_v3.2_EP4TA\\run.bat','运行Bat文件目录',1)
--go
--insert into EntityOpt values('TempFolder','D:\\TempFolder','ASN临时目录',1)
--go
--insert into EntityOpt values('ArchiveFolder','D:\\ArchiveFolder','ASN存档目录',1)
--go
--insert into EntityOpt values('OutFolder','D:\\CovisintHttpsClient_v3.2_EP4TA\\tocovisint','输出文件目录',1)
--go
--insert into EntityOpt values('IsTestSystem','1','测试系统标识',1)
--go


------------------
----------------- job
set identity_insert BatchJobDet on
insert into BatchJobDet(Id, Name, Desc1, ServiceName) values(60,'ReadEDIFordPlanASNJob','Job of Create EDI ASN','ReadEDIFordPlanASNJob')
go
insert into BatchJobDet(Id, Name, Desc1, ServiceName) values(61,'RunBatJob','Job of Run EDI bat File','RunBatJob')
go
insert into BatchJobDet(Id, Name, Desc1, ServiceName) values(62,'LoadEDIJob','Job of Load Ford EDI File','LoadEDIJob')
go
set identity_insert BatchJobDet off
go
insert into BatchTrigger values('ReadEDIFordPlanASNTrigger','Trigger of Create EDI ASN',60,'2014-04-02 00:05:00.000','2014-04-02 00:05:00.000',0,5,'Minutes',0,'Pause')
go
insert into BatchTrigger values('RunBatTrigger','Trigger of Run EDI bat File',61,'2014-04-02 00:05:00.000','2014-04-02 00:05:00.000',0,5,'Minutes',0,'Pause')
go
insert into BatchTrigger values('LoadEDITrigger','Trigger of Load Ford EDI File',62,'2014-04-02 00:05:00.000','2014-04-02 00:05:00.000',0,5,'Minutes',0,'Pause')


------------------路线明细上的默认值
go
alter table FlowDet add TransModeCode varchar(50) null--运输方式
go
alter table FlowDet add ConveyanceNumber varchar(50) null--车牌号
go
alter table FlowDet add CarrierCode varchar(50) null--承运商
go
alter table FlowDet add GrossWeight varchar(50) null--净重
go
alter table FlowDet add NetWeight varchar(50) null--毛重
go
alter table FlowDet add WeightUom varchar(50) null--净重毛重单位
go
alter table FlowDet add PackagingCode varchar(50) null--包装类型
go
alter table FlowDet add LadingQuantity varchar(50) null--包装类型数量
go
alter table FlowDet add UnitsPerContainer varchar(50) null--单箱件数
go
----------------------
-------------------






