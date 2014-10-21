alter table PlanBill add RecDetId int
go

alter table OrderLocTrans add OrderNo varchar(50)
go

create index IX_OrderLocTrans_OrderNo on OrderLocTrans(OrderNo asc)
go

alter table OrderDet add ItemDesc varchar(100)
go

alter table ReceiptDet add OrderDetId int
go

create index IX_ReceiptDet_OrderDetId on ReceiptDet(OrderDetId asc)
go

alter table ReceiptDet add OrderNo varchar(50)
go

create index IX_ReceiptDet_OrderNo on ReceiptDet(OrderNo asc)
go

alter table ReceiptDet add Item varchar(50)
go

create index IX_ReceiptDet_Item on ReceiptDet(Item asc)
go

alter table ReceiptDet add ItemDesc varchar(100)
go

IF EXISTS(SELECT * FROM sys.objects WHERE type='U' AND name='WOBomBackflush') 
     DROP TABLE WOBomBackflush
GO

Create table WOBomBackflush
(
	Id int identity(1, 1) Primary Key,
	Flow varchar(50), 
	OrderNo varchar(50), 
	OrderDetId int, 
	OrderLocTransId int,
	RecNo varchar(50),
	Item varchar(50),
	HuId varchar(50),
	BackflushQty decimal(18, 8), 
	Location varchar(50), 
	DssImpHisId int,
	EffDate datetime,
	CreateDate datetime,
	CreateUser varchar(50)
)
go

IF EXISTS(SELECT * FROM sys.objects WHERE type='U' AND name='WOBomBackflushArch') 
     DROP TABLE WOBomBackflushArch
GO

Create table WOBomBackflushArch
(
	Id int Primary Key,
	Flow varchar(50), 
	OrderNo varchar(50), 
	OrderDetId int, 
	OrderLocTransId int,
	RecNo varchar(50),
	Item varchar(50),
	HuId varchar(50),
	BackflushQty decimal(18, 8), 
	Location varchar(50), 
	DssImpHisId int,
	EffDate datetime,
	CreateDate datetime,
	CreateUser varchar(50),
	BackflushDate datetime,
	BackflushUser varchar(50)
)
go

IF EXISTS(SELECT * FROM sys.objects WHERE type='U' AND name='WOBomBackflushLog') 
     DROP TABLE WOBomBackflushLog
GO

create table WOBomBackflushLog
(
	Id int identity(1, 1) Primary Key,
	Item varchar(50),
	Location varchar(50),
	BackflushQty decimal(18, 8), 
	Lvl tinyint,
	Msg varchar(500),
	CreateDate datetime,
	CreateUser varchar(50)
)
go

alter table DssImpHis add LastModifyUser varchar(50)
go

alter table DssImpHis add LastModifyDate datetime
go

alter table OrderPlanBackflush alter column IpNo varchar(50) null
go

