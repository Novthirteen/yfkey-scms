SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='SnapshotLocationDet') 
DROP PROCEDURE SnapshotLocationDet
GO
CREATE PROCEDURE [dbo].SnapshotLocationDet
WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateNow datetime
	declare @Msg nvarchar(MAX)
	declare @trancount int
	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), GetDate(), 121))
	set @Msg = ''
	set @trancount = @@trancount
	
	begin try
		if @trancount = 0
		begin
            begin tran
        end
		
		if not exists(select top 1 1 from MRP_LocationDetSnapShot) or
			exists(select top 1 1 from MRP_LocationDetSnapShot where EffDate <> @DateNow)
		begin
			truncate table MRP_LocationDetSnapShot
			insert into MRP_LocationDetSnapShot(Item, Location, Plant, Qty, InTransitQty, PurchaseInTransitQty, InspectQty, EffDate)
			select loc.Item, loc.Location, r.Plant, SUM(Qty) as Qty, SUM(InTransitQty) as InTransitQty, SUM(PurchaseInTransitQty) as PurchaseInTransitQty, SUM(InspectQty) as InspectQty, @DateNow
			from (
			--获取库存
			select det.Item, det.Location, SUM(Qty) as Qty, 0 as InTransitQty, 0 as PurchaseInTransitQty, 0 as InspectQty
			from LocationDet as det
			inner join Location as l on det.Location = l.Code
			where l.IsMRP = 1 and l.Code not in ('Inspect', 'Reject')
			group by det.Item, det.Location
			union all
			--获取在途（全部）
			select oDet.Item, oDet.LocTo, 0 as Qty, SUM(iDet.Qty - ISNULL(iDet.RecQty, 0)) as InTransitQty, 0 as PurchaseInTransitQty, 0 as InspectQty
			from IpDet as iDet
			inner join IpMstr as iMstr on iDet.IpNo = iMstr.IpNo
			inner join OrderLocTrans as oTrans on iDet.OrderLocTransId = oTrans.Id
			inner join OrderDet as oDet on oTrans.OrderDetId = oDet.Id
			inner join OrderMstr as oMstr on oDet.OrderNo = oMstr.OrderNo
			where oMstr.[Type] = 'Transfer' and iMstr.[Status] = 'Create' and iMstr.[Type] = 'Nml'
			group by oDet.Item, oDet.LocTo
			union all
			--获取在途（采购）
			select oDet.Item, oDet.LocTo, 0 as Qty, 0 as InTransitQty, SUM(iDet.Qty - ISNULL(iDet.RecQty, 0)) as PurchaseInTransitQty, 0 as InspectQty
			from IpDet as iDet
			inner join IpMstr as iMstr on iDet.IpNo = iMstr.IpNo
			inner join OrderLocTrans as oTrans on iDet.OrderLocTransId = oTrans.Id
			inner join OrderDet as oDet on oTrans.OrderDetId = oDet.Id
			inner join OrderMstr as oMstr on oDet.OrderNo = oMstr.OrderNo
			where oMstr.[Type] in ('Procurement', 'Subconctracting') and iMstr.[Status] = 'Create' and oMstr.SubType = 'Nml'
			group by oDet.Item, oDet.LocTo
			union all
			--获取检验库存
			select loc.Item, det.LocTo, 0 as Qty, 0 as InTransitQty, 0 as PurchaseInTransitQty, SUM(loc.Qty) as InspectQty 
			from InspectDet as det 
			inner join LocationLotDet as loc on det.LocLotDetId = loc.Id
			inner join InspectMstr as mstr on det.InspNo = mstr.InspNo
			inner join Location as l on det.LocTo = l.Code
			where mstr.IsSeperated = 0 and mstr.[Status] = 'Create'
			and l.IsMRP = 1
			group by loc.Item, det.LocTo
			) as loc inner join Location as l on l.Code = loc.Location
			inner join Region as r on l.Region = r.Code
			group by loc.Item, loc.Location, r.Plant
		end

		if @trancount = 0 
		begin  
            commit
        end
	end try
	begin catch
        if @trancount = 0
        begin
            rollback
        end 
		set @Msg = N'快照库存异常' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


