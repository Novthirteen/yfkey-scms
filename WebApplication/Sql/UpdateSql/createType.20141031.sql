CREATE TYPE ReceiveOrderInputType AS TABLE (
	OrderDetId int not null,
	HuId varchar(50),
	RecQty decimal(18, 8) not null
)
go

CREATE TYPE ReceiveIpInputType AS TABLE (
	IpDetId int not null,
	HuId varchar(50),
	RecQty decimal(18, 8) not null
)
go