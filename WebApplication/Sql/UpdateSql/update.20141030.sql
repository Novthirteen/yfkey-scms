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

DROP INDEX[IX_PriceListDet] ON [PriceListDet] 
GO

CREATE UNIQUE CLUSTERED INDEX [IX_PriceListDet] ON [PriceListDet]
(
	[PriceList] ASC,
	[Item] ASC,
	[Currency] ASC,
	[Uom] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO







