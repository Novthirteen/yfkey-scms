insert into codemstr (code,codevalue,seq,isDefault,desc1) values ('TPriceListDetailType','Transportation',10,1,'运输')
insert into codemstr (code,codevalue,seq,isDefault,desc1) values ('TPriceListDetailType','Operation',20,0,'操作费')
insert into codemstr (code,codevalue,seq,isDefault,desc1) values ('TPriceListDetailType','WarehouseLease',30,0,'固定费用')
insert into codemstr(code,codevalue,seq,isdefault,desc1) values ('BillingMethod','Out',10,1,'出库计费')
insert into codemstr(code,codevalue,seq,isdefault,desc1) values ('BillingMethod','In',20,0,'入库计费')


--begin sunwanpeng 20110325

INSERT INTO "ACC_PermissionCategory" (PMC_Code,PMC_Desc,PMC_Type) VALUES ('Transportation','运输管理','Menu')

INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.Carrier','承运商','Transportation')

INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.Vehicle','运输工具','Transportation')

INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.TransportationAddress','运输地址','Transportation')

INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.TransportationRoute','运输路线','Transportation')

INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.TransportPriceList','运输价格单','Transportation')

INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.Expense','零星费用','Transportation')

INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.TransportationOrder','运输单','Transportation')

INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.TransportationFeesApply','费用申请','Transportation')

INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.Bill','运输账单','Transportation')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('VehicleType','Scattered',10,1,'零担')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('VehicleType','5T',20,0,'5T')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('VehicleType','8T',30,0,'8T')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('VehicleType','10T',40,0,'10T')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('VehicleType','12T',50,0,'12T')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('VehicleType','20FOOT',60,0,'20FOOT')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('VehicleType','40FOOT',70,0,'40FOOT')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('PricingMethod','M3',10,1,'M3')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('PricingMethod','KG',20,0,'KG')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('PricingMethod','SHIPT',30,0,'SHIPT')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('RegionType','Normal',10,1,'普通')

INSERT INTO "CodeMstr" (Code,CodeValue,Seq,IsDefault,Desc1) VALUES ('RegionType','Remote',20,0,'异地仓库')

set identity_insert BatchJobDet on;
INSERT INTO "BatchJobDet" (Id,Name,Desc1,ServiceName) VALUES (13,'TransportationOrderCloseJob','Job of Automatic Close Transportation Orders','TransportationOrderCloseJob')
set identity_insert BatchJobDet off;

set identity_insert BatchTrigger on;
INSERT INTO "BatchTrigger" (Id,Name,Desc1,JobId,NextFireTime,PrevFireTime,RepeatCount,Interval,IntervalType,TimesTriggered,Status) VALUES (26,'TransportationOrderCloseTrigger','Trigger of Automatic Close Transportation Orders',13,'2011-04-18 13:09:42','2011-04-18 00:00:00',0,1,'Days',0,'Pause')
set identity_insert BatchTrigger off;

INSERT INTO "EntityOpt" (PreCode,PreValue,CodeDesc,Seq) VALUES ('RecalculateWhenTransportationBill','True','运输开票时重新计价',0)
INSERT INTO "EntityOpt" (PreCode,PreValue,CodeDesc,Seq) VALUES ('PallentVolume','1.2','托盘体积',0)
INSERT INTO "EntityOpt" (PreCode,PreValue,CodeDesc,Seq) VALUES ('ValuateWhenComplete','True','运单完成时自动计价',0)
--end sunwanpeng20110325

--begin sunwanpeng 20110426
INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('~/Main.aspx?mid=Transportation.ValuateOrder','运单计价','Transportation')
--end sunwanpeng20110426

--begin sunwanpeng 20110427
set identity_insert BatchJobDet on;
INSERT INTO "BatchJobDet" (Id,Name,Desc1,ServiceName) VALUES (14,'TPriceList2TActBillJob','Job of Automatic Create TActBill by TPriceList of WarehouseLease','TPriceList2TActBillJob')
set identity_insert BatchJobDet off;

set identity_insert BatchTrigger on;
INSERT INTO "BatchTrigger" (Id,Name,Desc1,JobId,NextFireTime,PrevFireTime,RepeatCount,Interval,IntervalType,TimesTriggered,Status) VALUES (27,'TPriceList2TActBillTrigger','Trigger of Automatic Create TActBill',14,'2011-04-27 10:09:42','2011-04-27 00:00:00',0,1,'Months',0,'Pause')
set identity_insert BatchTrigger off;
--end sunwanpeng20110427

--begin sunwanpeng 20110427
set identity_insert BatchJobDet on;
INSERT INTO "BatchJobDet" (Id,Name,Desc1,ServiceName) VALUES (15,'BillCreateJob','Job of Automatic Create Bill','BillCreateJob')
set identity_insert BatchJobDet off;

set identity_insert BatchJobParam on;
INSERT INTO "BatchJobParam" (Id,JobId,ParamName,ParamValue) VALUES (1,15,'Customers','QNGC1')
set identity_insert BatchJobParam off;

set identity_insert BatchTrigger on;
INSERT INTO "BatchTrigger" (Id,Name,Desc1,JobId,NextFireTime,PrevFireTime,RepeatCount,Interval,IntervalType,TimesTriggered,Status) VALUES (28,'BillCreateTrigger','Trigger of Automatic Create Bill',15,'2011-04-29 10:09:42','2011-04-29 00:00:00',0,1,'Days',0,'Pause')
set identity_insert BatchTrigger off;
--end sunwanpeng20110427


--party 承运商
INSERT INTO [Party] ([Code],[Name],[IsActive]) VALUES('AX','安鑫',1);
INSERT INTO [Party] ([Code],[Name],[IsActive]) VALUES('DZ','大中',1);
INSERT INTO [Party] ([Code],[Name],[IsActive]) VALUES('JJ','杰记',1);
INSERT INTO [Party] ([Code],[Name],[IsActive]) VALUES('LY','联运',1);
INSERT INTO [Party] ([Code],[Name],[IsActive]) VALUES('PY','浦运',1);

--Carrier 承运商
INSERT INTO [Carrier] ([Code]) VALUES('AX');
INSERT INTO [Carrier] ([Code]) VALUES('DZ');
INSERT INTO [Carrier] ([Code]) VALUES('JJ');
INSERT INTO [Carrier] ([Code]) VALUES('LY');
INSERT INTO [Carrier] ([Code]) VALUES('PY');

--承运商权限
insert into "acc_permissioncategory" (PMC_Code,PMC_Desc,PMC_Type) Values ('Carrier','承运商','Organization');
INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('AX','安鑫','Carrier');
INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('DZ','大中','Carrier');
INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('JJ','杰记','Carrier');
INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('LY','联运','Carrier');
INSERT INTO "ACC_Permission" (PM_Code,PM_Desc,PM_CateCode) VALUES ('PY','浦运','Carrier');

--PartyAddr 承运商-开票地址
INSERT INTO [PartyAddr]([Code],[PartyCode],[AddrType],[SeqNo],[IsPrimary],[Address],[PostalCode],[TelNumber],[MobilePhone],[ContactPsnName],[Fax],[Email],[WebSite],[IsActive]) VALUES('B_AX','AX','BillAddr',1,1,'','','','','','','','',1);
INSERT INTO [PartyAddr]([Code],[PartyCode],[AddrType],[SeqNo],[IsPrimary],[Address],[PostalCode],[TelNumber],[MobilePhone],[ContactPsnName],[Fax],[Email],[WebSite],[IsActive]) VALUES('B_DZ','DZ','BillAddr',1,1,'','','','','','','','',1);
INSERT INTO [PartyAddr]([Code],[PartyCode],[AddrType],[SeqNo],[IsPrimary],[Address],[PostalCode],[TelNumber],[MobilePhone],[ContactPsnName],[Fax],[Email],[WebSite],[IsActive]) VALUES('B_JJ','JJ','BillAddr',1,1,'','','','','','','','',1);
INSERT INTO [PartyAddr]([Code],[PartyCode],[AddrType],[SeqNo],[IsPrimary],[Address],[PostalCode],[TelNumber],[MobilePhone],[ContactPsnName],[Fax],[Email],[WebSite],[IsActive]) VALUES('B_LY','LY','BillAddr',1,1,'','','','','','','','',1);
INSERT INTO [PartyAddr]([Code],[PartyCode],[AddrType],[SeqNo],[IsPrimary],[Address],[PostalCode],[TelNumber],[MobilePhone],[ContactPsnName],[Fax],[Email],[WebSite],[IsActive]) VALUES('B_PY','PY','BillAddr',1,1,'','','','','','','','',1);

-- 运输工具

--TAddress 地址
set identity_insert TAddress on;
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(1,'','','','','包头');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(2,'','','','','北京');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(3,'','','','','成都');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(4,'','','','','合肥');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(5,'','','','','花都');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(6,'','','','','柳州');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(7,'','','','','南京');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(8,'','','','','上海安亭');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(9,'','','','','上海金桥');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(10,'','','','','上海康桥');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(11,'','','','','上海临港');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(12,'','','','','上海下沙');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(13,'','','','','沈阳');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(14,'','','','','台州');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(15,'','','','','泰州');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(16,'','','','','芜湖');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(17,'','','','','武汉');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(18,'','','','','襄樊');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(19,'','','','','休宁');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(20,'','','','','烟台');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(21,'','','','','盐城');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(22,'','','','','仪征');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(23,'','','','','长春');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(24,'','','','','郑州');
INSERT INTO [TAddress]([Id],[Country],[Province],[City],[District],[Address]) VALUES(25,'','','','','重庆');
set identity_insert TAddress off;

--运输路线TRouteMstr
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_NMGBT',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='包头');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_BJ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='北京');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_SCCD',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='成都');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_AHHF',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='合肥');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_GDHD',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='花都');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_GXLZ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='柳州');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_JSNJ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='南京');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_SHAT',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='上海安亭');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_SHJQ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='上海金桥');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_SHKQ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='上海康桥');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_SHLG',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='上海临港');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_SHXS',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='上海下沙');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_LNSY',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='沈阳');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_ZJTZ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='台州');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_JSTZ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='泰州');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_AHWH',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='芜湖');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_HBWH',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='武汉');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_HBXF',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='襄樊');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_AHXN',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='休宁');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_SDYT',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='烟台');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_JSYC',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='盐城');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_JSYZ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='仪征');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_JLCS',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='长春');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_HNZZ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='郑州');
INSERT INTO [TRouteMstr]([Code],[IsActive],[Description],[ShipFrom],[ShipTo]) Select 'SH_CQ',1,'',(select id from Taddress where Address='上海康桥'),(select id from Taddress where Address='重庆');

--运输路线明细

--运输价格单TPriceListMstr
INSERT INTO [TPriceListMstr]([Code],[Party],[IsActive]) VALUES('AXWL','AX',1);
INSERT INTO [TPriceListMstr]([Code],[Party],[IsActive]) VALUES('DZWL','DZ',1);
INSERT INTO [TPriceListMstr]([Code],[Party],[IsActive]) VALUES('JJWL','JJ',1);
INSERT INTO [TPriceListMstr]([Code],[Party],[IsActive]) VALUES('LYWL','LY',1);
INSERT INTO [TPriceListMstr]([Code],[Party],[IsActive]) VALUES('PYWL','PY',1);

--运输价格单-操作费

--运输价格单-运输费TPriceListDet
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'AXWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='上海安亭'),'RMB',700,1,'SHIPT','12T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'DZWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='上海下沙'),'RMB',180,1,'SHIPT','2T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'JJWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='上海康桥'),'RMB',30,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'JJWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='上海安亭'),'RMB',60,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='重庆'),'RMB',229.9,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='成都'),'RMB',285,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='长春'),'RMB',240,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='北京'),'RMB',140,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='南京'),'RMB',90,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='仪征'),'RMB',80,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='芜湖'),'RMB',80,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='合肥'),'RMB',104.5,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='柳州'),'RMB',247,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='郑州'),'RMB',185,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='武汉'),'RMB',185,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='烟台'),'RMB',185,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='盐城'),'RMB',104.5,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='沈阳'),'RMB',220,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='泰州'),'RMB',80,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='台州'),'RMB',125,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='休宁'),'RMB',160,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='包头'),'RMB',300,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='花都'),'RMB',200,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='襄樊'),'RMB',190,1,'M3','零担','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='重庆'),'RMB',8500,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='成都'),'RMB',9000,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='长春'),'RMB',8000,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='北京'),'RMB',4900,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='南京'),'RMB',1700,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='南京'),'RMB',2600,1,'SHIPT','10T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='南京'),'RMB',3400,1,'SHIPT','12T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='仪征'),'RMB',1700,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='芜湖'),'RMB',1700,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='合肥'),'RMB',2700,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='合肥'),'RMB',3800,1,'SHIPT','10T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='合肥'),'RMB',4800,1,'SHIPT','12T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='柳州'),'RMB',7000,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='郑州'),'RMB',4800,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='武汉'),'RMB',4800,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='烟台'),'RMB',4800,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='烟台'),'RMB',6000,1,'SHIPT','10T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='烟台'),'RMB',8000,1,'SHIPT','12T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='盐城'),'RMB',2400,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='沈阳'),'RMB',7000,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='泰州'),'RMB',1500,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='台州'),'RMB',3800,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='花都'),'RMB',8000,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='襄樊'),'RMB',7000,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='上海临港'),'RMB',450,1,'SHIPT','5T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'LYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='上海临港'),'RMB',800,1,'SHIPT','8T','Transportation',0;
INSERT INTO [TPriceListDet]([TPriceList],[StartDate],[EndDate],[ShipFrom],[ShipTo],[Currency],[UnitPrice],[IsProvEst],[PricingMethod],[VehicleType],[Type],[IsIncludeTax]) Select 'PYWL','2011-01-01','2011-12-31',(select Id from TAddress where Address='上海康桥'),(select Id from TAddress where Address='上海金桥'),'RMB',690,1,'SHIPT','8T','Transportation',0;

UPDATE [TPriceListDet] set VehicleType='Scattered' where VehicleType='零担';
update tpricelistdet set isprovEst=0;

--运输价格单-固定费

--移库路线
update flowmstr set troute='SH_SHJQ',tpricelist='PYWL',carrier='PY',carrierbilladdr='B_PY' where code='FG_WTY';
update flowmstr set troute='SH_BJ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W001';
update flowmstr set troute='SH_AHWH',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W002';
update flowmstr set troute='SH_JLCS',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W003';
update flowmstr set troute='SH_JSYZ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W004';
update flowmstr set troute='SH_CQ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W005';
update flowmstr set troute='SH_GXLZ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W007';
update flowmstr set troute='SH_JSYC',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W009';
update flowmstr set troute='SH_SCCD',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W010';
update flowmstr set troute='SH_HBWH',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W011'
update flowmstr set troute='SH_SHLG',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W012';
update flowmstr set troute='SH_AHHF',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W013';
update flowmstr set troute='SH_JSNJ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W014';
update flowmstr set troute='SH_ZJTZ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W015';
update flowmstr set troute='SH_JSNJ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W017';
update flowmstr set troute='SH_NMGBT',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W020';
update flowmstr set troute='SH_LNSY',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W021';
update flowmstr set troute='SH_SHLG',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='FG_W022';


--销售路线
update flowmstr set troute='SH_SHJQ',tpricelist='PYWL',carrier='PY',carrierbilladdr='B_PY' where code='SGM-DY';
update flowmstr set troute='SH_SHAT',tpricelist='AXWL',carrier='AX',carrierbilladdr='B_AX' where code='FZQC';
update flowmstr set troute='SH_SHKQ',tpricelist='JJWL',carrier='JJ',carrierbilladdr='B_JJ' where code='SGM-AS';
update flowmstr set troute='SH_SHAT',tpricelist='AXWL',carrier='AX',carrierbilladdr='B_AX' where code='SVW-SH';
update flowmstr set troute='SH_JSYZ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='SAIC-AS';
update flowmstr set troute='SH_SHAT',tpricelist='JJWL',carrier='JJ',carrierbilladdr='B_JJ' where code='SVW-AS';
update flowmstr set troute='SH_JSYZ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='SYQC';
update flowmstr set troute='SH_SHJQ',tpricelist='PYWL',carrier='PY',carrierbilladdr='B_PY' where code='SGM-SH';
update flowmstr set troute='SH_SHJQ',tpricelist='PYWL',carrier='PY',carrierbilladdr='B_PY' where code='SGM-SH-S';
update flowmstr set troute='SH_SHJQ',tpricelist='PYWL',carrier='PY',carrierbilladdr='B_PY' where code='SGM-SY';
update flowmstr set troute='SH_BJ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='BBDC';    
update flowmstr set troute='SH_HNZZ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='NISSAN';
update flowmstr set troute='SH_AHHF',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='YHJS';
update flowmstr set troute='SH_JSNJ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='SVW-NJ-W';
update flowmstr set troute='SH_JSYC',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='DYKMC-AS';
update flowmstr set troute='SH_SHLG',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='saic-yfv';
update flowmstr set troute='SH_ZJTZ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='CQNLG';
update flowmstr set troute='SH_AHHF',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='CQCDQ';
update flowmstr set troute='SH_SHJQ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='YFV-YT';
update flowmstr set troute='SH_SHXS',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='YFJC-YT';
update flowmstr set troute='SH_BJ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='CXDBJ';   
update flowmstr set troute='SH_AHHF',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='XCQC';
update flowmstr set troute='SH_AHHF',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='YHJS';
update flowmstr set troute='SH_AHHF',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='JAC-AS';
update flowmstr set troute='SH_JSTZ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='TZJS1_S';
update flowmstr set troute='SH_GXLZ',tpricelist='LYWL',carrier='LY',carrierbilladdr='B_LY' where code='YFKLZ';

--路线明细

update flowdet set packvol=0.15557,hulotsize=30.00   where flow='BBDC' and item='2300001008450' and uc=30.00        
update flowdet set packvol=0.22418,hulotsize=15.00   where flow='BBDC' and item='2300301008485' and uc=15.00        
update flowdet set packvol=0.22418,hulotsize=15.00   where flow='BBDC' and item='2300301008486' and uc=15.00        
update flowdet set packvol=0.12672,hulotsize=3.00    where flow='BBDC' and item='1301061008700' and uc=3.00         
update flowdet set packvol=0.15557,hulotsize=30.00   where flow='BBDC' and item='2300001008450' and uc=30.00               
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='CXDBJ' and item='1311011010297' and uc=5.00              
update flowdet set packvol=0.0336 ,hulotsize=400.00  where flow='CXDBJ' and item='1610001019278' and uc=400.00       
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='DYKMC' and item='1472001012661' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='DYKMC' and item='1475001012661' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='DYKMC' and item='1476001012661' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='DYKMC' and item='1471001012661' and uc=5.00         
update flowdet set packvol=0.05675,hulotsize=200.00  where flow='FZQC' and item='1030101009894' and uc=200.00       
update flowdet set packvol=0.0636 ,hulotsize=200.00  where flow='FZQC' and item='1040101005037' and uc=200.00       
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='JAC-AS' and item='3240001007852' and uc=6.00         
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='JAC-AS' and item='3240001007853' and uc=6.00         
update flowdet set packvol=0.12   ,hulotsize=6.00    where flow='JAC-AS' and item='3240001007851' and uc=6.00         
update flowdet set packvol=0.06541,hulotsize=500.00  where flow='JAC-AS' and item='3240001008180' and uc=500.00       
update flowdet set packvol=0.06541,hulotsize=500.00  where flow='JAC-AS' and item='3240001008180' and uc=500.00       
update flowdet set packvol=0.03795,hulotsize=51.00   where flow='JAC-AS' and item='3240001007848' and uc=51.00        
update flowdet set packvol=0.02705,hulotsize=54.00   where flow='JAC-AS' and item='3240001007849' and uc=54.00        
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='JAC-AS' and item='3240001007855' and uc=24.00        
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='JAC-AS' and item='3240001007854' and uc=24.00        
update flowdet set packvol=0.0551 ,hulotsize=36.00   where flow='JAC-AS' and item='3240001007847' and uc=36.00        
update flowdet set packvol=0.0551 ,hulotsize=36.00   where flow='JAC-AS' and item='3240001007850' and uc=36.00        
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='JAC-AS' and item='3244001007852' and uc=6.00         
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='JAC-AS' and item='3244001007853' and uc=6.00         
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='JAC-AS' and item='3244001007855' and uc=24.00        
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='JAC-AS' and item='3244001007854' and uc=24.00        
update flowdet set packvol=0.02789,hulotsize=72.00   where flow='JAC-AS' and item='3244001007847' and uc=72.00        
update flowdet set packvol=0.0551 ,hulotsize=36.00   where flow='JAC-AS' and item='3244001007850' and uc=36.00        
update flowdet set packvol=0.0072 ,hulotsize=400.00  where flow='JAC-AS' and item='3244001013432' and uc=400.00       
update flowdet set packvol=0.03795,hulotsize=51.00   where flow='JAC-AS' and item='3244001007848' and uc=51.00        
update flowdet set packvol=0.02705,hulotsize=54.00   where flow='JAC-AS' and item='3244001007849' and uc=54.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='JAC-AS' and item='3181001020123' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='JAC-AS' and item='3181001020124' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='JAC-AS' and item='3181001013384' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='JAC-AS' and item='3181001013385' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='JAC-AS' and item='3181001013386' and uc=18.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='JAC-AS' and item='3181001013387' and uc=18.00        
update flowdet set packvol=0.02789,hulotsize=72.00   where flow='JAC-AS' and item='3181001013380' and uc=72.00        
update flowdet set packvol=0.02789,hulotsize=24.00   where flow='JAC-AS' and item='3181001013383' and uc=24.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='JAC-AS' and item='3183001013386' and uc=18.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='JAC-AS' and item='3183001013387' and uc=18.00        
update flowdet set packvol=0.02789,hulotsize=72.00   where flow='JAC-AS' and item='3183001013380' and uc=72.00        
update flowdet set packvol=0.02789,hulotsize=24.00   where flow='JAC-AS' and item='3183001013383' and uc=24.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='JAC-AS' and item='3183001020124' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='JAC-AS' and item='3183001020123' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='JAC-AS' and item='3183001013385' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='JAC-AS' and item='3183001013384' and uc=16.00        
update flowdet set packvol=0.03878,hulotsize=1000.00 where flow='JAC-AS' and item='3183001013382' and uc=1000.00     
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='JAC-AS' and item='3181001013378' and uc=60.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='JAC-AS' and item='3181001013379' and uc=60.00               
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='JAC-AS' and item='3183001013378' and uc=60.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='JAC-AS' and item='3183001013379' and uc=60.00        
update flowdet set packvol=0.11497,hulotsize=4.00    where flow='NISSAN' and item='1321001010103' and uc=4.00         
update flowdet set packvol=0.03751,hulotsize=300.00  where flow='NISSAN' and item='1320001010087' and uc=300.00       
update flowdet set packvol=0.03751,hulotsize=300.00  where flow='NISSAN' and item='1320001010088' and uc=300.00       
update flowdet set packvol=0.09676,hulotsize=100.00  where flow='NISSAN' and item='1520201013135' and uc=100.00       
update flowdet set packvol=0.11497,hulotsize=4.00    where flow='NISSAN' and item='1521001012960' and uc=4.00         
update flowdet set packvol=0.11497,hulotsize=4.00    where flow='NISSAN' and item='1521001013826' and uc=4.00         
update flowdet set packvol=0.08   ,hulotsize=100.00  where flow='NISSAN' and item='1520301013857' and uc=100.00       
update flowdet set packvol=0.0672 ,hulotsize=8.00    where flow='SAIC-A' and item='2580001016389' and uc=8.00         
update flowdet set packvol=0.0672 ,hulotsize=6.00    where flow='SAIC-A' and item='2580101016707' and uc=6.00               
update flowdet set packvol=0.00888,hulotsize=200.00  where flow='SAIC-A' and item='2330301016749' and uc=200.00       
update flowdet set packvol=0.2952 ,hulotsize=24.00   where flow='SAIC-A' and item='2660101021821' and uc=24.00        
update flowdet set packvol=0.0672 ,hulotsize=8.00    where flow='SAIC-A' and item='2660001022099' and uc=8.00         
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SAIC-A' and item='2330301015353' and uc=12.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SAIC-A' and item='2330301015354' and uc=12.00        
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1173051017259' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1173051017259' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1172051012047' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=1.00    where flow='SAIC-A' and item='1172051015156' and uc=1.00         
update flowdet set packvol=0.096  ,hulotsize=1.00    where flow='SAIC-A' and item='1170051008959' and uc=1.00         
update flowdet set packvol=0.096  ,hulotsize=1.00    where flow='SAIC-A' and item='1170051008959' and uc=1.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1172051012047' and uc=5.00         
update flowdet set packvol=0.02505,hulotsize=1.00    where flow='SAIC-A' and item='1170001006862' and uc=1.00         
update flowdet set packvol=0.12867,hulotsize=1.00    where flow='SAIC-A' and item='1172051015156' and uc=1.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1172051012047' and uc=5.00         
update flowdet set packvol=0.02505,hulotsize=1.00    where flow='SAIC-A' and item='1170001006861' and uc=1.00         
update flowdet set packvol=0.02505,hulotsize=1.00    where flow='SAIC-A' and item='1172001006861' and uc=1.00         
update flowdet set packvol=0.02505,hulotsize=1.00    where flow='SAIC-A' and item='1172001006862' and uc=1.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1341021021150' and uc=5.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1160051008893' and uc=1.00         
update flowdet set packvol=0.0672 ,hulotsize=32.00   where flow='SAIC-A' and item='1341021022742' and uc=32.00        
update flowdet set packvol=0.00132,hulotsize=1.00    where flow='SAIC-A' and item='1161001010835' and uc=1.00         
update flowdet set packvol=0.00264,hulotsize=1.00    where flow='SAIC-A' and item='1163001010835' and uc=1.00         
update flowdet set packvol=0.00264,hulotsize=1.00    where flow='SAIC-A' and item='1161001010837' and uc=1.00         
update flowdet set packvol=0.00264,hulotsize=1.00    where flow='SAIC-A' and item='1163001010837' and uc=1.00         
update flowdet set packvol=0.00264,hulotsize=1.00    where flow='SAIC-A' and item='1160001010841' and uc=1.00              
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1341021022743' and uc=5.00               
update flowdet set packvol=0.096  ,hulotsize=5.00    where flow='SAIC-A' and item='1164001021093' and uc=5.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1161151021092' and uc=1.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1163041021092' and uc=1.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1163031008892' and uc=1.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1161031008892' and uc=1.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1161051018584' and uc=1.00         
update flowdet set packvol=0.14016,hulotsize=5.00    where flow='SAIC-A' and item='1163051018584' and uc=5.00         
update flowdet set packvol=0.14016,hulotsize=5.00    where flow='SAIC-A' and item='1161051018583' and uc=5.00         
update flowdet set packvol=0.14016,hulotsize=5.00    where flow='SAIC-A' and item='1163051018583' and uc=5.00         
update flowdet set packvol=0.00264,hulotsize=1.00    where flow='SAIC-A' and item='1161001010265' and uc=1.00         
update flowdet set packvol=0.00264,hulotsize=1.00    where flow='SAIC-A' and item='1163001010265' and uc=1.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1163051021092' and uc=1.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1161151021092' and uc=1.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1163041021092' and uc=1.00         
update flowdet set packvol=0.14016,hulotsize=5.00    where flow='SAIC-A' and item='1161051021093' and uc=5.00         
update flowdet set packvol=0.14016,hulotsize=1.00    where flow='SAIC-A' and item='1163051021093' and uc=1.00         
update flowdet set packvol=0.096  ,hulotsize=1.00    where flow='SAIC-A' and item='1343001021151' and uc=1.00              
update flowdet set packvol=0.096  ,hulotsize=1.00    where flow='SAIC-A' and item='1343021021150' and uc=1.00         
update flowdet set packvol=0.096  ,hulotsize=1.00    where flow='SAIC-A' and item='1343021021340' and uc=1.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1341021021340' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1341021021341' and uc=5.00         
update flowdet set packvol=0.096  ,hulotsize=1.00    where flow='SAIC-A' and item='1343021021341' and uc=1.00         
update flowdet set packvol=0.096  ,hulotsize=1.00    where flow='SAIC-A' and item='1341021025459' and uc=1.00         
update flowdet set packvol=0.096  ,hulotsize=1.00    where flow='SAIC-A' and item='1343021025459' and uc=1.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1660011021940' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1660011021939' and uc=5.00                
update flowdet set packvol=0.096  ,hulotsize=5.00    where flow='SAIC-A' and item='1170021022543' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1341011021151' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1341021021341' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1341021021340' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1341021021150' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1580011018883' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1580011017074' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1580021018453' and uc=5.00             
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='SAIC-A' and item='1581021023864' and uc=5.00         
update flowdet set packvol=0.00888,hulotsize=300.00  where flow='SAIC-A' and item='1580301018465' and uc=300.00       
update flowdet set packvol=0.00888,hulotsize=300.00  where flow='SAIC-A' and item='1580401018466' and uc=300.00             
update flowdet set packvol=1.27401,hulotsize=1.00    where flow='SGM-AS' and item='1061011004363' and uc=1.00         
update flowdet set packvol=1.27401,hulotsize=1.00    where flow='SGM-AS' and item='1061021004368' and uc=1.00         
update flowdet set packvol=1.27401,hulotsize=1.00    where flow='SGM-AS' and item='1062021004381' and uc=1.00         
update flowdet set packvol=1.27401,hulotsize=1.00    where flow='SGM-AS' and item='1062011004347' and uc=1.00            
update flowdet set packvol=1.27401,hulotsize=1.00    where flow='SGM-AS' and item='1062021004367' and uc=1.00         
update flowdet set packvol=1.27401,hulotsize=1.00    where flow='SGM-AS' and item='1062021004380' and uc=1.00                   
update flowdet set packvol=0.018  ,hulotsize=1.00    where flow='SGM-AS' and item='1062401004383' and uc=1.00         
update flowdet set packvol=0.018  ,hulotsize=1.00    where flow='SGM-AS' and item='1062401004385' and uc=1.00         
update flowdet set packvol=0.11739,hulotsize=4.00    where flow='SGM-AS' and item='1081021006546' and uc=4.00         
update flowdet set packvol=0.11739,hulotsize=4.00    where flow='SGM-AS' and item='1081021006554' and uc=4.00         
update flowdet set packvol=0.11739,hulotsize=4.00    where flow='SGM-AS' and item='1081011009387' and uc=4.00         
update flowdet set packvol=0.0081 ,hulotsize=200.00  where flow='SGM-AS' and item='1080301006564' and uc=200.00       
update flowdet set packvol=0.0081 ,hulotsize=200.00  where flow='SGM-AS' and item='1080401006565' and uc=200.00       
update flowdet set packvol=1.27401,hulotsize=42.00   where flow='SGM-AS' and item='1261021007967' and uc=42.00        
update flowdet set packvol=0.00048,hulotsize=1.00    where flow='SGM-AS' and item='1060001010617' and uc=1.00         
update flowdet set packvol=0.0072 ,hulotsize=60.00   where flow='SGM-AS' and item='1060001004357' and uc=60.00        
update flowdet set packvol=0.0072 ,hulotsize=60.00   where flow='SGM-AS' and item='1060001004358' and uc=60.00        
update flowdet set packvol=0.018  ,hulotsize=400.00  where flow='SGM-AS' and item='1062401004384' and uc=400.00       
update flowdet set packvol=0.018  ,hulotsize=400.00  where flow='SGM-AS' and item='1062401004386' and uc=400.00       
update flowdet set packvol=1.2    ,hulotsize=42.00   where flow='SGM-AS' and item='1291011010140' and uc=42.00        
update flowdet set packvol=1.2    ,hulotsize=1.00    where flow='SGM-AS' and item='1291021010139' and uc=1.00         
update flowdet set packvol=0.018  ,hulotsize=500.00  where flow='SGM-AS' and item='1260301007972' and uc=500.00       
update flowdet set packvol=0.018  ,hulotsize=500.00  where flow='SGM-AS' and item='1260401007976' and uc=500.00       
update flowdet set packvol=1.27401,hulotsize=42.00   where flow='SGM-AS' and item='1061021013760' and uc=42.00        
update flowdet set packvol=1.27401,hulotsize=42.00   where flow='SGM-AS' and item='1491021015860' and uc=42.00        
update flowdet set packvol=1.2    ,hulotsize=1.00    where flow='SGM-AS' and item='1294021017106' and uc=1.00         
update flowdet set packvol=1.2    ,hulotsize=42.00   where flow='SGM-AS' and item='1294011017107' and uc=42.00        
update flowdet set packvol=0.01776,hulotsize=400.00  where flow='SGM-AS' and item='1294301017111' and uc=400.00       
update flowdet set packvol=0.01776,hulotsize=400.00  where flow='SGM-AS' and item='1294401017112' and uc=400.00       
update flowdet set packvol=1.27401,hulotsize=42.00   where flow='SGM-AS' and item='1261021007966' and uc=42.00        
update flowdet set packvol=0.00005,hulotsize=1.00    where flow='SGM-AS' and item='1601011022834' and uc=1.00         
update flowdet set packvol=1.2    ,hulotsize=42.00   where flow='SGM-AS' and item='1601011013931' and uc=42.00              
update flowdet set packvol=0.0672 ,hulotsize=1.00    where flow='SGM-AS' and item='2061001004640' and uc=1.00         
update flowdet set packvol=0.0672 ,hulotsize=1.00    where flow='SGM-AS' and item='2061011004640' and uc=1.00         
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='SGM-AS' and item='2260001008975' and uc=48.00        
update flowdet set packvol=0.33187,hulotsize=1.00    where flow='SGM-AS' and item='2060101004674' and uc=1.00         
update flowdet set packvol=0.01925,hulotsize=1.00    where flow='SGM-AS' and item='2061401005772' and uc=1.00         
update flowdet set packvol=0.0672 ,hulotsize=1.00    where flow='SGM-AS' and item='2062001004656' and uc=1.00         
update flowdet set packvol=0.33187,hulotsize=1.00    where flow='SGM-AS' and item='2060101004657' and uc=1.00         
update flowdet set packvol=0.01225,hulotsize=1.00    where flow='SGM-AS' and item='2060111004657' and uc=1.00         
update flowdet set packvol=0.112  ,hulotsize=4.00    where flow='SGM-AS' and item='2270111008610' and uc=4.00         
update flowdet set packvol=0.00589,hulotsize=1.00    where flow='SGM-AS' and item='2270001008459' and uc=1.00         
update flowdet set packvol=0.00589,hulotsize=1.00    where flow='SGM-AS' and item='2274001008459' and uc=1.00         
update flowdet set packvol=0.00589,hulotsize=1.00    where flow='SGM-AS' and item='2270011008459' and uc=1.00         
update flowdet set packvol=0.01802,hulotsize=1.00    where flow='SGM-AS' and item='2062011004656' and uc=1.00               
update flowdet set packvol=0.01225,hulotsize=1.00    where flow='SGM-AS' and item='2060111004674' and uc=1.00         
update flowdet set packvol=0.0672 ,hulotsize=6.00    where flow='SGM-AS' and item='2600001014358' and uc=6.00         
update flowdet set packvol=0.112  ,hulotsize=4.00    where flow='SGM-AS' and item='2600101014353' and uc=4.00         
update flowdet set packvol=0.112  ,hulotsize=4.00    where flow='SGM-AS' and item='2270101008610' and uc=4.00               
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-AS' and item='2261011008511' and uc=12.00             
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-AS' and item='2261001020761' and uc=12.00               
update flowdet set packvol=0.01802,hulotsize=1.00    where flow='SGM-AS' and item='2261001008511' and uc=1.00         
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-AS' and item='3083001006802' and uc=9.00         
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-AS' and item='3083001006803' and uc=9.00         
update flowdet set packvol=0.00025,hulotsize=40.00   where flow='SGM-AS' and item='3083101007514' and uc=40.00        
update flowdet set packvol=0.00346,hulotsize=16.00   where flow='SGM-AS' and item='3083101006841' and uc=16.00        
update flowdet set packvol=0.00536,hulotsize=9.00    where flow='SGM-AS' and item='3083001006804' and uc=9.00         
update flowdet set packvol=0.00536,hulotsize=12.00   where flow='SGM-AS' and item='3083001006842' and uc=12.00        
update flowdet set packvol=0.00536,hulotsize=12.00   where flow='SGM-AS' and item='3083001006839' and uc=12.00        
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-AS' and item='3084001006802' and uc=9.00         
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-AS' and item='3084001006803' and uc=9.00         
update flowdet set packvol=0.00025,hulotsize=40.00   where flow='SGM-AS' and item='3084101007514' and uc=40.00        
update flowdet set packvol=0.00346,hulotsize=16.00   where flow='SGM-AS' and item='3084101006841' and uc=16.00        
update flowdet set packvol=0.00536,hulotsize=9.00    where flow='SGM-AS' and item='3084001006804' and uc=9.00         
update flowdet set packvol=0.00536,hulotsize=12.00   where flow='SGM-AS' and item='3084001006842' and uc=12.00        
update flowdet set packvol=0.00536,hulotsize=12.00   where flow='SGM-AS' and item='3084001006839' and uc=12.00        
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-AS' and item='3083001011994' and uc=9.00         
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-AS' and item='3084001011994' and uc=9.00         
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-AS' and item='3083001011995' and uc=9.00         
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-AS' and item='3084001011995' and uc=9.00         
update flowdet set packvol=0.01802,hulotsize=1.00    where flow='SGM-AS' and item='3277001010340' and uc=1.00         
update flowdet set packvol=0.01802,hulotsize=1.00    where flow='SGM-AS' and item='3277001010341' and uc=1.00               
update flowdet set packvol=0.00583,hulotsize=8.00    where flow='SGM-AS' and item='3277001010332' and uc=8.00         
update flowdet set packvol=0.00583,hulotsize=8.00    where flow='SGM-AS' and item='3277001010333' and uc=8.00         
update flowdet set packvol=0.00583,hulotsize=9.00    where flow='SGM-AS' and item='3277001010334' and uc=9.00         
update flowdet set packvol=0.00583,hulotsize=9.00    where flow='SGM-AS' and item='3277001010336' and uc=9.00         
update flowdet set packvol=0.00211,hulotsize=12.00   where flow='SGM-AS' and item='3277001010346' and uc=12.00        
update flowdet set packvol=0.00211,hulotsize=12.00   where flow='SGM-AS' and item='3277001010337' and uc=12.00        
update flowdet set packvol=0.00211,hulotsize=1.00    where flow='SGM-AS' and item='3273001010338' and uc=1.00         
update flowdet set packvol=0.00211,hulotsize=1.00    where flow='SGM-AS' and item='3271001010346' and uc=1.00         
update flowdet set packvol=0.00211,hulotsize=1.00    where flow='SGM-AS' and item='3273001010346' and uc=1.00         
update flowdet set packvol=0.00211,hulotsize=1.00    where flow='SGM-AS' and item='3271001010337' and uc=1.00         
update flowdet set packvol=0.00211,hulotsize=1.00    where flow='SGM-AS' and item='3273001010337' and uc=1.00         
update flowdet set packvol=0.00583,hulotsize=1.00    where flow='SGM-AS' and item='3271001010334' and uc=1.00         
update flowdet set packvol=0.00583,hulotsize=1.00    where flow='SGM-AS' and item='3273001010334' and uc=1.00         
update flowdet set packvol=0.00583,hulotsize=1.00    where flow='SGM-AS' and item='3271001010336' and uc=1.00         
update flowdet set packvol=0.00583,hulotsize=1.00    where flow='SGM-AS' and item='3273001010336' and uc=1.00         
update flowdet set packvol=0.01802,hulotsize=1.00    where flow='SGM-AS' and item='3271001010340' and uc=1.00         
update flowdet set packvol=0.01802,hulotsize=1.00    where flow='SGM-AS' and item='3271001010341' and uc=1.00         
update flowdet set packvol=0.00583,hulotsize=1.00    where flow='SGM-AS' and item='3271001010332' and uc=1.00         
update flowdet set packvol=0.00583,hulotsize=1.00    where flow='SGM-AS' and item='3271001010333' and uc=1.00         
update flowdet set packvol=0.00583,hulotsize=1.00    where flow='SGM-AS' and item='3273001010332' and uc=1.00         
update flowdet set packvol=0.00027,hulotsize=1.00    where flow='SGM-AS' and item='3273101010335' and uc=1.00         
update flowdet set packvol=0.01802,hulotsize=1.00    where flow='SGM-AS' and item='3273001010340' and uc=1.00         
update flowdet set packvol=0.01802,hulotsize=1.00    where flow='SGM-AS' and item='3273001010341' and uc=1.00         
update flowdet set packvol=0.00583,hulotsize=8.00    where flow='SGM-AS' and item='3273001010333' and uc=8.00               
update flowdet set packvol=0.00492,hulotsize=1.00    where flow='SGM-AS' and item='3083001010919' and uc=1.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-AS' and item='3084001010919' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-AS' and item='3084001010920' and uc=9.00         
update flowdet set packvol=0.00492,hulotsize=1.00    where flow='SGM-AS' and item='3083001010920' and uc=1.00                 
update flowdet set packvol=0.00211,hulotsize=1.00    where flow='SGM-AS' and item='3273001010338' and uc=1.00               
update flowdet set packvol=0.11739,hulotsize=4.00    where flow='SGM-DY' and item='1081011009387' and uc=4.00               
update flowdet set packvol=1.2    ,hulotsize=42.00   where flow='SGM-DY' and item='1710021027899' and uc=42.00        
update flowdet set packvol=0.0081 ,hulotsize=200.00  where flow='SGM-DY' and item='1080301006564' and uc=200.00       
update flowdet set packvol=0.0081 ,hulotsize=200.00  where flow='SGM-DY' and item='1080401006565' and uc=200.00       
update flowdet set packvol=1.27401,hulotsize=42.00   where flow='SGM-DY' and item='1294021017106' and uc=42.00        
update flowdet set packvol=1.27401,hulotsize=42.00   where flow='SGM-DY' and item='1294011017107' and uc=42.00        
update flowdet set packvol=0.01776,hulotsize=400.00  where flow='SGM-DY' and item='1294301017111' and uc=400.00       
update flowdet set packvol=0.01776,hulotsize=400.00  where flow='SGM-DY' and item='1294401017112' and uc=400.00          
update flowdet set packvol=1.27401,hulotsize=42.00   where flow='SGM-DY' and item='1601011013931' and uc=42.00        
update flowdet set packvol=0.00888,hulotsize=200.00  where flow='SGM-DY' and item='1600301015694' and uc=200.00       
update flowdet set packvol=0.00888,hulotsize=200.00  where flow='SGM-DY' and item='1600401015695' and uc=200.00       
update flowdet set packvol=0.0672 ,hulotsize=6.00    where flow='SGM-DY' and item='2274001020846' and uc=6.00         
update flowdet set packvol=0.0672 ,hulotsize=6.00    where flow='SGM-DY' and item='2600001014358' and uc=6.00         
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-DY' and item='3083001006802' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3083001010919' and uc=9.00         
update flowdet set packvol=0.00492,hulotsize=9.00    where flow='SGM-DY' and item='3083001006803' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3083001010920' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3081001011994' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3081001011995' and uc=9.00                
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3081001006804' and uc=9.00         
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3081001006842' and uc=12.00        
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3081001006839' and uc=12.00        
update flowdet set packvol=0.00888,hulotsize=40.00   where flow='SGM-DY' and item='3083101007514' and uc=40.00        
update flowdet set packvol=0.03552,hulotsize=16.00   where flow='SGM-DY' and item='3083101006841' and uc=16.00        
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3083001006804' and uc=9.00         
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3083001006842' and uc=12.00        
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3083001006839' and uc=12.00        
update flowdet set packvol=0.00888,hulotsize=40.00   where flow='SGM-DY' and item='3084101007514' and uc=40.00        
update flowdet set packvol=0.03552,hulotsize=16.00   where flow='SGM-DY' and item='3084101006841' and uc=16.00        
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3084001006804' and uc=9.00         
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3084001006842' and uc=12.00        
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3084001006839' and uc=12.00        
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3083001011994' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3084001011994' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3083001011995' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3084001011995' and uc=9.00         
update flowdet set packvol=0.00888,hulotsize=60.00   where flow='SGM-DY' and item='3277001010340' and uc=60.00        
update flowdet set packvol=0.00888,hulotsize=60.00   where flow='SGM-DY' and item='3277001010341' and uc=60.00        
update flowdet set packvol=0.03552,hulotsize=8.00    where flow='SGM-DY' and item='3277001021811' and uc=8.00         
update flowdet set packvol=0.03552,hulotsize=8.00    where flow='SGM-DY' and item='3277001021812' and uc=8.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3277001010334' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3277001010336' and uc=9.00         
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3277001010346' and uc=12.00        
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3277001010337' and uc=12.00        
update flowdet set packvol=0.03552,hulotsize=16.00   where flow='SGM-DY' and item='3273101010335' and uc=16.00        
update flowdet set packvol=0.00888,hulotsize=100.00  where flow='SGM-DY' and item='3608101016686' and uc=100.00       
update flowdet set packvol=0.03552,hulotsize=8.00    where flow='SGM-DY' and item='3608001015581' and uc=8.00         
update flowdet set packvol=0.03552,hulotsize=8.00    where flow='SGM-DY' and item='3608001015609' and uc=8.00         
update flowdet set packvol=0.03552,hulotsize=8.00    where flow='SGM-DY' and item='3608001015687' and uc=8.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3608001015611' and uc=9.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3608001015610' and uc=9.00         
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3608001015643' and uc=12.00        
update flowdet set packvol=0.03552,hulotsize=8.00    where flow='SGM-DY' and item='3608001015608' and uc=8.00         
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3608001017924' and uc=9.00         
update flowdet set packvol=0.0198 ,hulotsize=12.00   where flow='SGM-DY' and item='3608001015645' and uc=12.00        
update flowdet set packvol=0.03552,hulotsize=9.00    where flow='SGM-DY' and item='3608001020230' and uc=9.00         
update flowdet set packvol=0.018  ,hulotsize=500.00  where flow='SGM-SH' and item='1260301007972' and uc=500.00       
update flowdet set packvol=0.018  ,hulotsize=500.00  where flow='SGM-SH' and item='1260401007976' and uc=500.00       
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SH' and item='2061001004640' and uc=12.00        
update flowdet set packvol=0.0203 ,hulotsize=30.00   where flow='SGM-SH' and item='2490401017513' and uc=30.00             
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SH' and item='2061011004640' and uc=12.00        
update flowdet set packvol=0.0203 ,hulotsize=30.00   where flow='SGM-SH' and item='2490001026598' and uc=30.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SH' and item='2261011008511' and uc=12.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SH' and item='2261001020761' and uc=12.00        
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='SGM-SH' and item='2260001008975' and uc=48.00        
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='SGM-SH' and item='2061401005772' and uc=48.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SH' and item='2062001004656' and uc=12.00        
update flowdet set packvol=0.33187,hulotsize=16.00   where flow='SGM-SH' and item='2060101004657' and uc=16.00        
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='SGM-SH' and item='2061401005772' and uc=48.00           
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SH' and item='2062011004656' and uc=12.00        
update flowdet set packvol=0.0203 ,hulotsize=30.00   where flow='SGM-SH' and item='2490401017513' and uc=30.00        
update flowdet set packvol=0.0203 ,hulotsize=30.00   where flow='SGM-SH' and item='2490001026598' and uc=30.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SH' and item='2261011008511' and uc=12.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SH' and item='2261001020761' and uc=12.00        
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='SGM-SH' and item='2260001008975' and uc=48.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SH' and item='2261001008511' and uc=12.00        
update flowdet set packvol=0.018  ,hulotsize=500.00  where flow='SGM-SH' and item='1260301007972' and uc=500.00       
update flowdet set packvol=0.018  ,hulotsize=500.00  where flow='SGM-SH' and item='1260401007976' and uc=500.00       
update flowdet set packvol=1.2    ,hulotsize=36.00   where flow='SGM-SY' and item='1280001020645' and uc=36.00        
update flowdet set packvol=1.2    ,hulotsize=36.00   where flow='SGM-SY' and item='1280001020665' and uc=36.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='SGM-SY' and item='2280001019674' and uc=12.00        
update flowdet set packvol=1.2    ,hulotsize=72.00   where flow='SGM-SY' and item='2280101018201' and uc=72.00        
update flowdet set packvol=1.2    ,hulotsize=49.00   where flow='SGM-SY' and item='2280301017698' and uc=49.00        
update flowdet set packvol=1.2    ,hulotsize=49.00   where flow='SGM-SY' and item='2280301017699' and uc=49.00        
update flowdet set packvol=0.0672 ,hulotsize=6.00    where flow='SGM-SY' and item='2070001022979' and uc=6.00         
update flowdet set packvol=1.2    ,hulotsize=48.00   where flow='SGM-SY' and item='2070101019939' and uc=48.00        
update flowdet set packvol=0.09918,hulotsize=6.00    where flow='SVW-AS' and item='1021011010133' and uc=6.00         
update flowdet set packvol=0.0684 ,hulotsize=30.00   where flow='SVW-AS' and item='1020201009961' and uc=30.00        
update flowdet set packvol=0.09918,hulotsize=6.00    where flow='SVW-AS' and item='1011011009943' and uc=6.00         
update flowdet set packvol=0.05675,hulotsize=200.00  where flow='SVW-AS' and item='1010101009895' and uc=200.00       
update flowdet set packvol=0.0609 ,hulotsize=200.00  where flow='SVW-AS' and item='1020101009891' and uc=200.00       
update flowdet set packvol=0.02505,hulotsize=6.00    where flow='SVW-AS' and item='1011011009975' and uc=6.00         
update flowdet set packvol=1.28359,hulotsize=48.00   where flow='SVW-AS' and item='1021011009942' and uc=48.00        
update flowdet set packvol=1      ,hulotsize=100.00  where flow='SVW-AS' and item='1020001009939' and uc=100.00       
update flowdet set packvol=0.07122,hulotsize=80.00   where flow='SVW-AS' and item='1040101005458' and uc=80.00           
update flowdet set packvol=0.9408 ,hulotsize=60.00   where flow='SVW-AS' and item='1020201010258' and uc=60.00        
update flowdet set packvol=0.00434,hulotsize=1.00    where flow='SVW-AS' and item='1050011011924' and uc=1.00         
update flowdet set packvol=0.00434,hulotsize=1.00    where flow='SVW-AS' and item='1050051012497' and uc=1.00         
update flowdet set packvol=0.00434,hulotsize=1.00    where flow='SVW-AS' and item='1050021017970' and uc=1.00         
update flowdet set packvol=0.07998,hulotsize=60.00   where flow='SVW-AS' and item='1010001020353' and uc=60.00            
update flowdet set packvol=0.00434,hulotsize=1.00    where flow='SVW-AS' and item='2050001011902' and uc=1.00            
update flowdet set packvol=0.00434,hulotsize=1.00    where flow='SVW-AS' and item='2020001020804' and uc=1.00                 
update flowdet set packvol=0.0729 ,hulotsize=200.00  where flow='SVW-NJ' and item='1020101009891' and uc=200.00       
update flowdet set packvol=1.2    ,hulotsize=42.00   where flow='SVW-SH' and item='1021011009942' and uc=42.00        
update flowdet set packvol=0.0672 ,hulotsize=15.00   where flow='SVW-SH' and item='1020201009961' and uc=15.00        
update flowdet set packvol=0.0672 ,hulotsize=15.00   where flow='SVW-SH' and item='1040101005458' and uc=15.00        
update flowdet set packvol=0.0609 ,hulotsize=200.00  where flow='SVW-SH' and item='1020101009891' and uc=200.00       
update flowdet set packvol=1.232  ,hulotsize=20.00   where flow='SVW-SH' and item='1050011011924' and uc=20.00        
update flowdet set packvol=1.232  ,hulotsize=20.00   where flow='SVW-SH' and item='1050051012497' and uc=20.00        
update flowdet set packvol=1.232  ,hulotsize=20.00   where flow='SVW-SH' and item='1050011015452' and uc=20.00        
update flowdet set packvol=1.232  ,hulotsize=20.00   where flow='SVW-SH' and item='1050021017970' and uc=20.00        
update flowdet set packvol=1.2    ,hulotsize=36.00   where flow='SVW-SH' and item='1620001020988' and uc=36.00        
update flowdet set packvol=1.2    ,hulotsize=36.00   where flow='SVW-SH' and item='1620001020987' and uc=36.00        
update flowdet set packvol=1.2    ,hulotsize=36.00   where flow='SVW-SH' and item='1620021023094' and uc=36.00        
update flowdet set packvol=1.2    ,hulotsize=36.00   where flow='SVW-SH' and item='1640011022384' and uc=36.00        
update flowdet set packvol=1.2    ,hulotsize=36.00   where flow='SVW-SH' and item='1640021022984' and uc=36.00              
update flowdet set packvol=1.056  ,hulotsize=50.00   where flow='SVW-SH' and item='2350301019012' and uc=50.00        
update flowdet set packvol=1.056  ,hulotsize=50.00   where flow='SVW-SH' and item='2350301019013' and uc=50.00        
update flowdet set packvol=1.056  ,hulotsize=75.00   where flow='SVW-SH' and item='2620001020953' and uc=75.00        
update flowdet set packvol=0.9856 ,hulotsize=75.00   where flow='SVW-SH' and item='2050001016794' and uc=75.00        
update flowdet set packvol=0.9856 ,hulotsize=75.00   where flow='SVW-SH' and item='2640001022381' and uc=75.00        
update flowdet set packvol=0.9856 ,hulotsize=75.00   where flow='SVW-SH' and item='2050001011902' and uc=75.00        
update flowdet set packvol=0.096  ,hulotsize=4.00    where flow='SYQC  ' and item='1151011009908' and uc=4.00         
update flowdet set packvol=0.096  ,hulotsize=4.00    where flow='SYQC  ' and item='1151021009909' and uc=4.00         
update flowdet set packvol=0.09918,hulotsize=60.00   where flow='SYQC  ' and item='1151201009912' and uc=60.00        
update flowdet set packvol=0.0672 ,hulotsize=4.00    where flow='TZJS1_' and item='2540101011180' and uc=4.00         
update flowdet set packvol=0.1359 ,hulotsize=8.00    where flow='XCQC  ' and item='2180101011192' and uc=8.00                 
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3083001006838' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3083001006837' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3083001022036' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3084001022036' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3084001006837' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3277001018434' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3277001019252' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3271001019252' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3271001018434' and uc=40.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YFJC-Y' and item='3271001010338' and uc=60.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3273001010338' and uc=40.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YFJC-Y' and item='3273001018434' and uc=60.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3273001019252' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3081001022036' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3081001006837' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3608001015845' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3608001021338' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3271001018434' and uc=40.00        
update flowdet set packvol=0.02789,hulotsize=40.00   where flow='YFJC-Y' and item='3083001006838' and uc=40.00        
update flowdet set packvol=0.05387,hulotsize=24.00   where flow='YFJC-Y' and item='2080201013770' and uc=24.00        
update flowdet set packvol=0.05387,hulotsize=24.00   where flow='YFJC-Y' and item='2080201013771' and uc=24.00          
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W00' and item='1311011010297' and uc=5.00         
update flowdet set packvol=0.10976,hulotsize=2.00    where flow='FG_W00' and item='1140011026364' and uc=2.00         
update flowdet set packvol=0.10976,hulotsize=2.00    where flow='FG_W00' and item='1284011022617' and uc=2.00         
update flowdet set packvol=0.10976,hulotsize=2.00    where flow='FG_W00' and item='1144011026364' and uc=2.00         
update flowdet set packvol=0.10976,hulotsize=2.00    where flow='FG_W00' and item='1280001022617' and uc=2.00         
update flowdet set packvol=0.10238,hulotsize=5.00    where flow='FG_W00' and item='1461011022486' and uc=5.00         
update flowdet set packvol=0.10238,hulotsize=5.00    where flow='FG_W00' and item='1461021022485' and uc=5.00         
update flowdet set packvol=0.12   ,hulotsize=6.00    where flow='FG_W00' and item='1091011010035' and uc=6.00         
update flowdet set packvol=0.06541,hulotsize=24.00   where flow='FG_W00' and item='1090201009966' and uc=24.00        
update flowdet set packvol=0.03399,hulotsize=6.00    where flow='FG_W00' and item='2530101011637' and uc=6.00         
update flowdet set packvol=0.03399,hulotsize=6.00    where flow='FG_W00' and item='2560101019415' and uc=6.00         
update flowdet set packvol=0.1272 ,hulotsize=20.00   where flow='FG_W00' and item='2100001006238' and uc=20.00        
update flowdet set packvol=0.02683,hulotsize=40.00   where flow='FG_W00' and item='3530001008270' and uc=40.00        
update flowdet set packvol=0.02683,hulotsize=40.00   where flow='FG_W00' and item='3530001023782' and uc=40.00        
update flowdet set packvol=0.02683,hulotsize=45.00   where flow='FG_W00' and item='3560301023927' and uc=45.00        
update flowdet set packvol=0.02683,hulotsize=45.00   where flow='FG_W00' and item='3560001010456' and uc=45.00        
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W00' and item='1541011011573' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W00' and item='1541051011583' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W00' and item='1172051015156' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W00' and item='1170021006860' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W00' and item='1173051017259' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W00' and item='1172051012047' and uc=5.00         
update flowdet set packvol=0.0672 ,hulotsize=8.00    where flow='FG_W00' and item='2540001011297' and uc=8.00         
update flowdet set packvol=0.0672 ,hulotsize=10.00   where flow='FG_W00' and item='3544101012629' and uc=10.00        
update flowdet set packvol=0.00888,hulotsize=40.00   where flow='FG_W00' and item='3544201012630' and uc=40.00        
update flowdet set packvol=0.0672 ,hulotsize=10.00   where flow='FG_W00' and item='3543101012629' and uc=10.00        
update flowdet set packvol=0.00888,hulotsize=40.00   where flow='FG_W00' and item='3543201012630' and uc=40.00        
update flowdet set packvol=0.1059 ,hulotsize=10.00   where flow='FG_W00' and item='1191011006184' and uc=10.00        
update flowdet set packvol=0.05994,hulotsize=200.00  where flow='FG_W00' and item='1190001006194' and uc=200.00       
update flowdet set packvol=0.00084,hulotsize=1500.00where flow='FG_W00' and item='9030021009373' and uc=1500.00     
update flowdet set packvol=0.00084,hulotsize=500.00  where flow='FG_W00' and item='9030011006193' and uc=500.00       
update flowdet set packvol=0.01932,hulotsize=200.00  where flow='FG_W00' and item='1190001006190' and uc=200.00       
update flowdet set packvol=0.01932,hulotsize=200.00  where flow='FG_W00' and item='1190001006191' and uc=200.00       
update flowdet set packvol=0.0672 ,hulotsize=24.00   where flow='FG_W00' and item='1190001006188' and uc=24.00        
update flowdet set packvol=0.096  ,hulotsize=6.00    where flow='FG_W00' and item='1252011005139' and uc=6.00         
update flowdet set packvol=0.08775,hulotsize=200.00  where flow='FG_W00' and item='1250401005445' and uc=200.00       
update flowdet set packvol=0.08775,hulotsize=200.00  where flow='FG_W00' and item='1250301005325' and uc=200.00              
update flowdet set packvol=0.0672 ,hulotsize=45.00   where flow='FG_W00' and item='2250401006178' and uc=45.00        
update flowdet set packvol=0.1236 ,hulotsize=16.00   where flow='FG_W00' and item='2250001005231' and uc=16.00             
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='FG_W00' and item='2250501006177' and uc=48.00        
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W00' and item='1476001012661' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W00' and item='1471001012661' and uc=5.00         
update flowdet set packvol=0.12   ,hulotsize=6.00    where flow='FG_W01' and item='1091011010035' and uc=6.00         
update flowdet set packvol=0.06541,hulotsize=24.00   where flow='FG_W01' and item='1090201009966' and uc=24.00        
update flowdet set packvol=0.1272 ,hulotsize=20.00   where flow='FG_W01' and item='2100001006238' and uc=20.00        
update flowdet set packvol=0.06018,hulotsize=16.00   where flow='FG_W01' and item='2510301010793' and uc=16.00        
update flowdet set packvol=0.06018,hulotsize=16.00   where flow='FG_W01' and item='2510301010794' and uc=16.00        
update flowdet set packvol=0.06103,hulotsize=12.00   where flow='FG_W01' and item='2511201010687' and uc=12.00        
update flowdet set packvol=0.06103,hulotsize=12.00   where flow='FG_W01' and item='2513201010687' and uc=12.00        
update flowdet set packvol=0.06103,hulotsize=12.00   where flow='FG_W01' and item='2511201010688' and uc=12.00        
update flowdet set packvol=0.06103,hulotsize=12.00   where flow='FG_W01' and item='2513201010688' and uc=12.00               
update flowdet set packvol=0.05184,hulotsize=45.00   where flow='FG_W01' and item='1240001007924' and uc=45.00        
update flowdet set packvol=0.1386 ,hulotsize=4.00    where flow='FG_W01' and item='1242051015310' and uc=4.00         
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='FG_W01' and item='2180001013647' and uc=48.00        
update flowdet set packvol=0.0864 ,hulotsize=12.00   where flow='FG_W01' and item='2180001011638' and uc=12.00        
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='FG_W01' and item='2180401020825' and uc=48.00        
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='FG_W01' and item='2240001007723' and uc=48.00        
update flowdet set packvol=0.05271,hulotsize=8.00    where flow='FG_W01' and item='2240001007480' and uc=8.00         
update flowdet set packvol=0.05271,hulotsize=8.00    where flow='FG_W01' and item='2241001007480' and uc=8.00         
update flowdet set packvol=0.05271,hulotsize=8.00    where flow='FG_W01' and item='2240001024025' and uc=8.00         
update flowdet set packvol=0.04255,hulotsize=12.00   where flow='FG_W01' and item='2570001026432' and uc=12.00        
update flowdet set packvol=0.084  ,hulotsize=12.00   where flow='FG_W01' and item='2570001011638' and uc=12.00        
update flowdet set packvol=0.0203 ,hulotsize=30.00   where flow='FG_W01' and item='2570001021429' and uc=30.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3181001020123' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3181001020124' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='FG_W01' and item='3181001013386' and uc=18.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='FG_W01' and item='3181001013387' and uc=18.00        
update flowdet set packvol=0.02789,hulotsize=72.00   where flow='FG_W01' and item='3181001013380' and uc=72.00        
update flowdet set packvol=0.02789,hulotsize=24.00   where flow='FG_W01' and item='3181001013383' and uc=24.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3181001027718' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3181001027719' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='FG_W01' and item='3181001027922' and uc=18.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='FG_W01' and item='3181001027923' and uc=18.00        
update flowdet set packvol=0.02789,hulotsize=24.00   where flow='FG_W01' and item='3181001027721' and uc=24.00        
update flowdet set packvol=0.02789,hulotsize=72.00   where flow='FG_W01' and item='3180001027720' and uc=72.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='FG_W01' and item='3183001013386' and uc=18.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='FG_W01' and item='3183001013387' and uc=18.00        
update flowdet set packvol=0.02789,hulotsize=72.00   where flow='FG_W01' and item='3183001013380' and uc=72.00        
update flowdet set packvol=0.02789,hulotsize=24.00   where flow='FG_W01' and item='3183001013383' and uc=24.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3183001020124' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3183001020123' and uc=16.00        
update flowdet set packvol=0.03878,hulotsize=1000.00 where flow='FG_W01' and item='3183001013382' and uc=1000.00     
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='FG_W01' and item='3240001007852' and uc=6.00         
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='FG_W01' and item='3240001007853' and uc=6.00         
update flowdet set packvol=0.12   ,hulotsize=6.00    where flow='FG_W01' and item='3240001007851' and uc=6.00         
update flowdet set packvol=0.06541,hulotsize=500.00  where flow='FG_W01' and item='3240001008180' and uc=500.00       
update flowdet set packvol=0.06541,hulotsize=500.00  where flow='FG_W01' and item='3240001008180' and uc=500.00       
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='FG_W01' and item='3240001007855' and uc=24.00        
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='FG_W01' and item='3240001007854' and uc=24.00        
update flowdet set packvol=0.0551 ,hulotsize=36.00   where flow='FG_W01' and item='3240001007847' and uc=36.00        
update flowdet set packvol=0.0551 ,hulotsize=36.00   where flow='FG_W01' and item='3240001007850' and uc=36.00        
update flowdet set packvol=0.0072 ,hulotsize=100.00  where flow='FG_W01' and item='3240001013432' and uc=100.00       
update flowdet set packvol=0.06541,hulotsize=2500.00 where flow='FG_W01' and item='3240001008188' and uc=2500.00     
update flowdet set packvol=0.06541,hulotsize=400.00  where flow='FG_W01' and item='3240001007856' and uc=400.00       
update flowdet set packvol=0.024  ,hulotsize=100.00  where flow='FG_W01' and item='9030011007736' and uc=100.00       
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3240001025014' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3240001025004' and uc=16.00        
update flowdet set packvol=0.0336 ,hulotsize=500.00  where flow='FG_W01' and item='3240401025025' and uc=500.00       
update flowdet set packvol=0.0336 ,hulotsize=500.00  where flow='FG_W01' and item='3240401025027' and uc=500.00       
update flowdet set packvol=0.0483 ,hulotsize=60.00   where flow='FG_W01' and item='3240001025046' and uc=60.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='FG_W01' and item='3240001025047' and uc=60.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='FG_W01' and item='3570001025003' and uc=18.00        
update flowdet set packvol=0.05958,hulotsize=18.00   where flow='FG_W01' and item='3570001025011' and uc=18.00              
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3240001025012' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3240001025013' and uc=16.00        
update flowdet set packvol=0.0336 ,hulotsize=500.00  where flow='FG_W01' and item='3730001025026' and uc=500.00       
update flowdet set packvol=0.0336 ,hulotsize=500.00  where flow='FG_W01' and item='3730001025028' and uc=500.00       
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='FG_W01' and item='3246001007852' and uc=6.00         
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='FG_W01' and item='3246001007853' and uc=6.00         
update flowdet set packvol=0.0072 ,hulotsize=200.00  where flow='FG_W01' and item='3246001013432' and uc=200.00       
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='FG_W01' and item='3246001007855' and uc=24.00        
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='FG_W01' and item='3246001007854' and uc=24.00        
update flowdet set packvol=0.0551 ,hulotsize=36.00   where flow='FG_W01' and item='3246001007850' and uc=36.00        
update flowdet set packvol=0.0551 ,hulotsize=36.00   where flow='FG_W01' and item='3246001007847' and uc=36.00        
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='FG_W01' and item='3244001007852' and uc=6.00         
update flowdet set packvol=0.05268,hulotsize=6.00    where flow='FG_W01' and item='3244001007853' and uc=6.00         
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='FG_W01' and item='3244001007855' and uc=24.00        
update flowdet set packvol=0.07715,hulotsize=24.00   where flow='FG_W01' and item='3244001007854' and uc=24.00        
update flowdet set packvol=0.02789,hulotsize=72.00   where flow='FG_W01' and item='3244001007847' and uc=72.00        
update flowdet set packvol=0.0551 ,hulotsize=36.00   where flow='FG_W01' and item='3244001007850' and uc=36.00        
update flowdet set packvol=0.0072 ,hulotsize=400.00  where flow='FG_W01' and item='3244001013432' and uc=400.00       
update flowdet set packvol=0.07998,hulotsize=45.00   where flow='FG_W01' and item='1180001013364' and uc=45.00        
update flowdet set packvol=0.07998,hulotsize=60.00   where flow='FG_W01' and item='1240001007479' and uc=60.00        
update flowdet set packvol=0.05184,hulotsize=45.00   where flow='FG_W01' and item='1240001007924' and uc=45.00        
update flowdet set packvol=0.23757,hulotsize=1500.00 where flow='FG_W01' and item='1240001007476' and uc=1500.00     
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='FG_W01' and item='2240001007723' and uc=48.00        
update flowdet set packvol=0.04101,hulotsize=6.00    where flow='FG_W01' and item='2240101007491' and uc=6.00         
update flowdet set packvol=0.05271,hulotsize=8.00    where flow='FG_W01' and item='2241001007480' and uc=8.00         
update flowdet set packvol=0.05271,hulotsize=8.00    where flow='FG_W01' and item='2240001007480' and uc=8.00               
update flowdet set packvol=0.04851,hulotsize=8.00    where flow='FG_W01' and item='2570101016789' and uc=8.00         
update flowdet set packvol=0.0203 ,hulotsize=30.00   where flow='FG_W01' and item='2570001021429' and uc=30.00        
update flowdet set packvol=0.01925,hulotsize=48.00   where flow='FG_W01' and item='2180001013647' and uc=48.00        
update flowdet set packvol=0.0864 ,hulotsize=12.00   where flow='FG_W01' and item='2180001011638' and uc=12.00        
update flowdet set packvol=0.1359 ,hulotsize=8.00    where flow='FG_W01' and item='2180101011192' and uc=8.00         
update flowdet set packvol=1.2    ,hulotsize=42.00   where flow='FG_W01' and item='1021011009942' and uc=42.00        
update flowdet set packvol=0.0672 ,hulotsize=15.00   where flow='FG_W01' and item='1020201009961' and uc=15.00        
update flowdet set packvol=1.2    ,hulotsize=42.00   where flow='FG_W01' and item='1010021023842' and uc=42.00        
update flowdet set packvol=0.1272 ,hulotsize=20.00   where flow='FG_W01' and item='2020001020804' and uc=20.00        
update flowdet set packvol=1.2    ,hulotsize=198.00  where flow='FG_W01' and item='2700101021883' and uc=198.00       
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='FG_W01' and item='2700301022493' and uc=12.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='FG_W01' and item='2700301022494' and uc=12.00        
update flowdet set packvol=0.10238,hulotsize=4.00    where flow='FG_W01' and item='1550001015812' and uc=4.00         
update flowdet set packvol=0.07998,hulotsize=60.00   where flow='FG_W01' and item='1550001018006' and uc=60.00        
update flowdet set packvol=0.117  ,hulotsize=12.00   where flow='FG_W01' and item='2550001016058' and uc=12.00        
update flowdet set packvol=0.117  ,hulotsize=12.00   where flow='FG_W01' and item='2550001027316' and uc=12.00        
update flowdet set packvol=0.11193,hulotsize=8.00    where flow='FG_W01' and item='2550101016063' and uc=8.00         
update flowdet set packvol=0.0203 ,hulotsize=30.00   where flow='FG_W01' and item='2550001018185' and uc=30.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3554001020856' and uc=16.00        
update flowdet set packvol=0.05958,hulotsize=16.00   where flow='FG_W01' and item='3554001020857' and uc=16.00            
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W01' and item='1580011018883' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W01' and item='1580011017074' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W01' and item='1580021018453' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W01' and item='1581011023865' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W01' and item='1581021023864' and uc=5.00         
update flowdet set packvol=0.00888,hulotsize=300.00  where flow='FG_W01' and item='1580301018465' and uc=300.00       
update flowdet set packvol=0.00888,hulotsize=300.00  where flow='FG_W01' and item='1580401018466' and uc=300.00       
update flowdet set packvol=0.0672 ,hulotsize=8.00    where flow='FG_W01' and item='2580001016389' and uc=8.00         
update flowdet set packvol=0.0672 ,hulotsize=6.00    where flow='FG_W01' and item='2580101016707' and uc=6.00         
update flowdet set packvol=0.00888,hulotsize=200.00  where flow='FG_W01' and item='2330301016749' and uc=200.00       
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='FG_W01' and item='2330301015353' and uc=12.00        
update flowdet set packvol=0.0672 ,hulotsize=12.00   where flow='FG_W01' and item='2330301015354' and uc=12.00        
update flowdet set packvol=0.08   ,hulotsize=22.00   where flow='FG_W02' and item='1630201022184' and uc=22.00        
update flowdet set packvol=0.0672 ,hulotsize=10.00   where flow='FG_W02' and item='1630201019601' and uc=10.00        
update flowdet set packvol=0.077  ,hulotsize=40.00   where flow='FG_W02' and item='1630001019596' and uc=40.00        
update flowdet set packvol=0.07866,hulotsize=2.00    where flow='FG_W02' and item='1630001019599' and uc=2.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W02' and item='1610001019271' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W02' and item='1610001021951' and uc=5.00         
update flowdet set packvol=0.12867,hulotsize=5.00    where flow='FG_W02' and item='1610001019270' and uc=5.00         
update flowdet set packvol=0.05436,hulotsize=20.00   where flow='FG_W02' and item='2280201018940' and uc=20.00        
update flowdet set packvol=0.05436,hulotsize=20.00   where flow='FG_W02' and item='2280201018946' and uc=20.00        
update flowdet set packvol=0.05436,hulotsize=20.00   where flow='FG_W02' and item='2070201019840' and uc=20.00        
update flowdet set packvol=0.05436,hulotsize=20.00   where flow='FG_W02' and item='2070201019841' and uc=20.00               
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YHJS  ' and item='3183001013378' and uc=60.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YHJS  ' and item='3181001013378' and uc=60.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YHJS  ' and item='3183001013379' and uc=60.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YHJS  ' and item='3181001013379' and uc=60.00            
update flowdet set packvol=0.03795,hulotsize=51.00   where flow='YHJS  ' and item='3244001007848' and uc=51.00        
update flowdet set packvol=0.02705,hulotsize=54.00   where flow='YHJS  ' and item='3244001007849' and uc=54.00        
update flowdet set packvol=0.03795,hulotsize=51.00   where flow='YHJS  ' and item='3244001009697' and uc=51.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YHJS  ' and item='3181001013378' and uc=60.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YHJS  ' and item='3183001013378' and uc=60.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YHJS  ' and item='3181001013379' and uc=60.00        
update flowdet set packvol=0.03795,hulotsize=60.00   where flow='YHJS  ' and item='3183001013379' and uc=60.00        



--更新asn
update ipmstr set isreferenced=0,flow =
(select distinct(om.flow) from ipdet d inner join orderloctrans l on d.orderloctransid=l.id 
inner join orderdet od on l.orderdetid=od.id
inner join ordermstr om on od.orderno=om.orderno
 where ipmstr.ipno=d.ipno)
where ipmstr.status in ('Create','In-Process') 
and ipmstr.partyfrom='YFK-FG'