set identity_insert BatchJobDet on;
INSERT INTO "BatchJobDet" (Id,Name,Desc1,ServiceName) VALUES (53,'TransportationOrderCompleteJob','Job of Automatic Complete Transportation Orders','TransportationOrderCompleteJob')
set identity_insert BatchJobDet off;

set identity_insert BatchTrigger on;
INSERT INTO "BatchTrigger" (Id,Name,Desc1,JobId,NextFireTime,PrevFireTime,RepeatCount,Interval,IntervalType,TimesTriggered,Status) VALUES (53,'TransportationOrderCompleteTrigger','Trigger of Automatic Complete Transportation Orders',53,'2011-04-18 13:09:42','2011-04-18 00:00:00',0,1,'Days',0,'Pause')
set identity_insert BatchTrigger off;