alter table PlanBill add RecDetId int
go

CREATE TYPE RecordInventoryDetail AS TABLE 
( 
	Item varchar(50),
	Location varchar(50),
	Qty DECIMAL(18, 8),
	PlannedBillId int,
	HuId varchar(50)
)
GO

alter table OrderLocTrans add OrderNo varchar(50)
go

alter table OrderDet add ItemDesc varchar(100)
go