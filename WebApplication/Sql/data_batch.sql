--BatchJobDet
--BatchTrigger
--BatchTriggerParam
--BatchJobParam


set identity_insert BatchJobDet on;
INSERT INTO "BatchJobDet" (Id,Name,Desc1,ServiceName) VALUES (1,'LeanEngineJob','Job of Automatic Generate Orders','LeanEngineJob')
INSERT INTO "BatchJobDet" (Id,Name,Desc1,ServiceName) VALUES (2,'OrderCloseJob','Job of Automatic Close Orders','OrderCloseJob')
set identity_insert BatchJobDet off;

set identity_insert BatchTrigger on;
INSERT INTO "BatchTrigger" (Id,Name,Desc1,JobId,NextFireTime,PrevFireTime,RepeatCount,Interval,IntervalType,TimesTriggered,Status) VALUES (2,'LeanEngineTrigger','Trigger of Automatic Generate Orders',1,'2010-08-12 13:09:42','2010-07-23 13:40:00',0,10,'Minutes',2944,'Pause')
INSERT INTO "BatchTrigger" (Id,Name,Desc1,JobId,NextFireTime,PrevFireTime,RepeatCount,Interval,IntervalType,TimesTriggered,Status) VALUES (6,'OrderCloseTrigger','Trigger of Automatic Close Orders',2,'2010-08-12 13:09:42','2010-07-06 00:00:00',0,1,'Days',2,'Pause')
set identity_insert BatchTrigger off;


set identity_insert BatchTriggerParam on;

set identity_insert BatchTriggerParam off;