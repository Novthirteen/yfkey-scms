alter table IpDet add ItemDesc varchar(100)
go

alter table IpDet add OrderDetId int
go

create index IX_IpDet_OrderDetId on IpDet(OrderDetId asc)
go

alter table IpDet add OrderNo varchar(50)
go

create index IX_IpDet_OrderNo on IpDet(OrderNo asc)
go

create index IX_IpDet_Item on IpDet(Item asc)
go






