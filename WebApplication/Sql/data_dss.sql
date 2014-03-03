--DssFtpCtrl
--DssInboundCtrl
--DssObjectMapping
--DssOutboundCtrl
--DssSysMstr


INSERT INTO "DssSysMstr" (Code,SysAlias,Desc1,Type,Flag,UndefStr1,UndefStr2,UndefStr3,UndefStr4,UndefStr5,Prefix1,Prefix2,Prefix3,Prefix4,Prefix5) VALUES ('QAD',null,'QAD','ERP',null,'1000',null,null,null,null,null,null,null,null,null)




set IDENTITY_INSERT DssFtpCtrl on;

INSERT INTO "DssFtpCtrl" (Id,FtpServer,FtpPort,FtpUser,FtpPass,FtpTempFolder,FtpFolder,FilePattern,LocalTempFolder,LocalFolder,IOType) VALUES (1,'192.168.210.110',21,'dlc','cfgui24','/usr/dss/messages/prod/intemp','/usr/dss/messages/prod/in','*.REQ','D:\Dss\outtemp','D:\Dss\out','Out')


INSERT INTO "DssFtpCtrl" (Id,FtpServer,FtpPort,FtpUser,FtpPass,FtpTempFolder,FtpFolder,FilePattern,LocalTempFolder,LocalFolder,IOType) VALUES (2,'192.168.210.110',21,'dlc','cfgui24','','/usr/dss/messages/prod/out','(.REQ)$','D:\Dss\intemp','D:\Dss\in','In')


INSERT INTO "DssFtpCtrl" (Id,FtpServer,FtpPort,FtpUser,FtpPass,FtpTempFolder,FtpFolder,FilePattern,LocalTempFolder,LocalFolder,IOType) VALUES (3,'192.168.210.100',21,'mes2scms','sconit','','','([^5](\w*)\.txt)$','D:\Dss\wotemp','D:\sconitInboundTest\ABImport','In')

set IDENTITY_INSERT DssFtpCtrl off;



INSERT INTO "DssInboundCtrl" (Id,InFloder,FilePattern,ServiceName,ArchiveFloder,ErrorFloder,SeqNo,FileEncoding) VALUES (1,'D:\Dss\in','*ptmstr.REQ','ItemInboundMgr.Service','D:\Dss\inbak','D:\Dss\inerror',3,'gb2312')


INSERT INTO "DssInboundCtrl" (Id,InFloder,FilePattern,ServiceName,ArchiveFloder,ErrorFloder,SeqNo,FileEncoding) VALUES (2,'D:\Dss\in','*cmmstr.REQ','CustomerInboundMgr.Service','D:\Dss\inbak','D:\Dss\inerror',5,'gb2312')


INSERT INTO "DssInboundCtrl" (Id,InFloder,FilePattern,ServiceName,ArchiveFloder,ErrorFloder,SeqNo,FileEncoding) VALUES (3,'D:\Dss\in','*vdmstr.REQ','SupplierInboundMgr.Service','D:\Dss\inbak','D:\Dss\inerror',4,'gb2312')


INSERT INTO "DssInboundCtrl" (Id,InFloder,FilePattern,ServiceName,ArchiveFloder,ErrorFloder,SeqNo,FileEncoding) VALUES (4,'D:\Dss\in','*pcmstr.REQ','PriceListInboundMgr.Service','D:\Dss\inbak','D:\Dss\inerror',8,'gb2312')


INSERT INTO "DssInboundCtrl" (Id,InFloder,FilePattern,ServiceName,ArchiveFloder,ErrorFloder,SeqNo,FileEncoding) VALUES (5,'D:\Dss\in','*bommstr.REQ','BomInboundMgr.Service','D:\Dss\inbak','D:\Dss\inerror',6,'gb2312')


INSERT INTO "DssInboundCtrl" (Id,InFloder,FilePattern,ServiceName,ArchiveFloder,ErrorFloder,SeqNo,FileEncoding) VALUES (6,'D:\Dss\in','*psmstr.REQ','BomDetailInboundMgr.Service','D:\Dss\inbak','D:\Dss\inerror',7,'gb2312')


INSERT INTO "DssInboundCtrl" (Id,InFloder,FilePattern,ServiceName,ArchiveFloder,ErrorFloder,SeqNo,FileEncoding) VALUES (7,'D:\Dss\in','*codemstr.REQ','CodeMasterInboundMgr.Service','D:\Dss\inbak','D:\Dss\inerror',1,'gb2312')


INSERT INTO "DssInboundCtrl" (Id,InFloder,FilePattern,ServiceName,ArchiveFloder,ErrorFloder,SeqNo,FileEncoding) VALUES (8,'D:\Dss\in','*ummstr.REQ','UomConversionInboundMgr.Service','D:\Dss\inbak','D:\Dss\inerror',2,'gb2312')


INSERT INTO "DssInboundCtrl" (Id,InFloder,FilePattern,ServiceName,ArchiveFloder,ErrorFloder,SeqNo,FileEncoding) VALUES (9,'D:\sconitInboundTest\ABImport\','*.txt','WoReceiptInboundMgr.Service','D:\sconitInboundTest\ABArchive\','D:\sconitInboundTest\ABError\',0,'gb2312')






set IDENTITY_INSERT DssObjectMapping on;


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (2,'Party','YFK-CU','QAD','Site','3000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (3,'Party','YFK-AB','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (4,'Party','YFK-FG','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (5,'Party','YFK-RWH','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (6,'Party','YFK-SB','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (7,'Party','YFK-SW','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (8,'Party','YFK-WH','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (9,'Location','Inspect','QAD','Location','QC')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (10,'Location','Reject','QAD','Location','QC')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (11,'Location','M444A','QAD','Location','F300')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (12,'Location','subc','QAD','Location','XTYQC')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (13,'Location','W001A','QAD','Location','W100')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (14,'Party','YFK_SUB','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (16,'Location','STC','QAD','Location','R001A')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (17,'Party','YFK-RW','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (18,'Party','YFK-W012','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (19,'Party','YFK-R-PY','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (20,'Party','YFK-R-XS','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (21,'Party','YFK-STY','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (22,'Party','YFK-W001','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (23,'Party','YFK-W002','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (24,'Party','YFK-W003','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (25,'Party','YFK-W004','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (26,'Party','YFK-W005','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (27,'Party','YFK-W006','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (28,'Party','YFK-W007','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (29,'Party','YFK-W008','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (30,'Party','YFK-W009','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (31,'Party','YFK-W010','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (32,'Party','YFK-W011','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (33,'Party','YFK-W013','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (34,'Party','YFK-W014','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (35,'Party','YFK-W015','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (36,'Party','YFK-W016','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (37,'Party','YFK-W017','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (38,'Party','YFK-W018','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (39,'Party','YFK-W019','QAD','Site','1000')


INSERT INTO "DssObjectMapping" (Id,Entity,Code,ExtSys,ExtEntity,ExtCode) VALUES (40,'Party','YFK-W020','QAD','Site','1000')


set IDENTITY_INSERT DssObjectMapping off;

set IDENTITY_INSERT DssOutboundCtrl on;


INSERT INTO "DssOutboundCtrl" (Id,ExtSysCode,ExtObjectCode,OutFolder,ServiceName,ArchiveFolder,SeqNo,TempFolder,FileEncoding,SysAlias,FilePrefix,FileSuffix,IsActive,Mark,UndefStr1,UndefStr2,UndefStr3,UndefStr4,UndefStr5) VALUES (2,'QAD','rctpo','D:\Dss\out','PoBillTransactionOutboundMgr.Service','D:\Dss\outbak',1,'D:\Dss\outtemp','gb2312',null,null,null,1,52349,null,null,null,null,null)


INSERT INTO "DssOutboundCtrl" (Id,ExtSysCode,ExtObjectCode,OutFolder,ServiceName,ArchiveFolder,SeqNo,TempFolder,FileEncoding,SysAlias,FilePrefix,FileSuffix,IsActive,Mark,UndefStr1,UndefStr2,UndefStr3,UndefStr4,UndefStr5) VALUES (3,'QAD','isstr','D:\Dss\out','RcttrOutboundMgr.Service','D:\Dss\outbak',2,'D:\Dss\outtemp','gb2312',null,null,null,1,1658538,null,null,null,null,null)


INSERT INTO "DssOutboundCtrl" (Id,ExtSysCode,ExtObjectCode,OutFolder,ServiceName,ArchiveFolder,SeqNo,TempFolder,FileEncoding,SysAlias,FilePrefix,FileSuffix,IsActive,Mark,UndefStr1,UndefStr2,UndefStr3,UndefStr4,UndefStr5) VALUES (4,'QAD','isstr','D:\Dss\out','IsssoOutboundMgr.Service','D:\Dss\outbak',4,'D:\Dss\outtemp','gb2312',null,null,null,1,1658727,'Cust',null,null,null,null)


INSERT INTO "DssOutboundCtrl" (Id,ExtSysCode,ExtObjectCode,OutFolder,ServiceName,ArchiveFolder,SeqNo,TempFolder,FileEncoding,SysAlias,FilePrefix,FileSuffix,IsActive,Mark,UndefStr1,UndefStr2,UndefStr3,UndefStr4,UndefStr5) VALUES (6,'QAD','rctisswo1','D:\Dss\out','RctwoOutboundMgr.Service','D:\Dss\outbak',3,'D:\Dss\outtemp','gb2312',null,null,null,1,1658657,'Norm','10',null,null,null)


INSERT INTO "DssOutboundCtrl" (Id,ExtSysCode,ExtObjectCode,OutFolder,ServiceName,ArchiveFolder,SeqNo,TempFolder,FileEncoding,SysAlias,FilePrefix,FileSuffix,IsActive,Mark,UndefStr1,UndefStr2,UndefStr3,UndefStr4,UndefStr5) VALUES (7,'QAD','issso1','D:\Dss\out','SoBill1OutboundMgr.Service','D:\Dss\outbak',6,'D:\Dss\outtemp','gb2312',null,null,null,1,5784,'Cust',null,null,null,null)


INSERT INTO "DssOutboundCtrl" (Id,ExtSysCode,ExtObjectCode,OutFolder,ServiceName,ArchiveFolder,SeqNo,TempFolder,FileEncoding,SysAlias,FilePrefix,FileSuffix,IsActive,Mark,UndefStr1,UndefStr2,UndefStr3,UndefStr4,UndefStr5) VALUES (8,'QAD','pobill','D:\Dss\out','PoBillOutboundMgr.Service','D:\Dss\outbak',7,'D:\Dss\outtemp','gb2312',null,null,null,0,0,null,null,null,null,null)


INSERT INTO "DssOutboundCtrl" (Id,ExtSysCode,ExtObjectCode,OutFolder,ServiceName,ArchiveFolder,SeqNo,TempFolder,FileEncoding,SysAlias,FilePrefix,FileSuffix,IsActive,Mark,UndefStr1,UndefStr2,UndefStr3,UndefStr4,UndefStr5) VALUES (9,'QAD','issunp','D:\Dss\out','IssunpOutboundMgr.Service','D:\Dss\outbak',8,'D:\Dss\outtemp','gb2312',null,null,null,1,1649853,null,null,null,null,null)


INSERT INTO "DssOutboundCtrl" (Id,ExtSysCode,ExtObjectCode,OutFolder,ServiceName,ArchiveFolder,SeqNo,TempFolder,FileEncoding,SysAlias,FilePrefix,FileSuffix,IsActive,Mark,UndefStr1,UndefStr2,UndefStr3,UndefStr4,UndefStr5) VALUES (10,'QAD','rctunp','D:\Dss\out','RctunpOutboundMgr.Service','D:\Dss\outbak',9,'D:\Dss\outtemp','gb2312',null,null,null,1,1561824,null,null,null,null,null)

set IDENTITY_INSERT DssOutboundCtrl off;
GO