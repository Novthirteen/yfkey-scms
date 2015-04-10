go
alter table TPriceListDet add TransportMethod varchar(50) null
go
insert into CodeMstr values('TransportMethod','LandType',10,1,'陆运')
go
insert into CodeMstr values('TransportMethod','ShipType',20,0,'海运')
go
insert into CodeMstr values('TransportMethod','PlaneType',30,0,'空运')
go
alter table TOrderMstr add TransportMethod varchar(50) null
go
alter table TActBill add TransportMethod varchar(50) null

