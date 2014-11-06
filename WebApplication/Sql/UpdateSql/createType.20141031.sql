CREATE TYPE ReceiveOrderType AS TABLE (
	OrderDetId int not null,
	RecQty decimal(18, 8) not null
)
go

CREATE TYPE ReceiveIpType AS TABLE (
	IpDetId int not null,
	RecQty decimal(18, 8) not null
)
go