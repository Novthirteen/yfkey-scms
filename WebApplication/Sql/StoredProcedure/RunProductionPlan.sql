SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunProductionPlan') 
     DROP PROCEDURE RunProductionPlan
GO

CREATE PROCEDURE [dbo].RunProductionPlan
(
	@Plant varchar(50),
	@RunUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateTimeNow datetime = GetDate()
	declare @DateNow datetime = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow))
	declare @Msg nvarchar(MAX) = ''
	declare @trancount int = @@trancount
	declare @BatchNo int
	declare @ShipPlanReleaseNo int
	
	exec GetNextSequence 'RunProductionPlan', @BatchNo output
	begin try
		create table #tempMsg
		(
			Lvl varchar(50),
			Flow varchar(50),
			Item varchar(50),
			Qty decimal(18, 8),
			Uom varchar(5),
			LocFrom varchar(50),
			LocTo varchar(50),
			StartTime datetime,
			WindowTime datetime,
			Msg varchar(500)
		)

		create table #tempProductPlan
		(
			Flow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			Uom varchar(5),
			Qty decimal(18, 8),
			Location varchar(50),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempBomDetail
		(
			RowId int identity(1, 1) primary key,
			Bom varchar(50),
			Item varchar(50),
			StruType varchar(50),
			Uom varchar(5),
			RateQty decimal(18, 8),
			ScrapPct decimal(18, 8),
			BackFlushMethod varchar(50)
		)

		select @ShipPlanReleaseNo = MAX(ReleaseNo) from MRP_ShipPlanMstr where [Status] = 'Submit'

		if (@ShipPlanReleaseNo is null)
		begin
			set @Msg = N'没有找到释放的发运计划' + Error_Message()
			--insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, @DateNow, 'Error', @Msg, @DateTimeNow, @RunUser)
			RAISERROR(@Msg, 16, 1) 
		end

		--获取毛需求
		insert into #tempProductPlan(Flow, Item, ItemDesc, RefItemCode, Uom, Qty,
		Location, WindowTime)
		select det.Flow, det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, det.ShipQty * det.UnitQty,
		det.LocTo, det.StartTime as WindowTime
		from MRP_ShipPlanDet as det 
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Location as l on det.LocFrom = l.Code
		inner join Region as r on l.Region = r.Code
		where mstr.ReleaseNo = @ShipPlanReleaseNo and r.Plant = @Plant

		--计算可用库存
		select Item, SUM(Qty) - ISNULL(MAX(i.SafeStock), 0) as ActiveQty
		from (
		--获取库存
		select det.Item, SUM(Qty) as Qty
		from LocationDet as det
		inner join Location as l on det.Location = l.Code
		inner join Region as r on l.Region = r.Code
		inner join (select distinct Item, Location from #tempProductPlan) as p on det.Item = p.Item and det.Location <> p.Location
		where l.IsMRP = 1 and r.Plant = @Plant and l.Code not in ('Inspect', 'Reject')
		group by det.Item
		union all
		--获取检验库存
		select loc.Item, SUM(loc.Qty) as InspectQty 
		from InspectDet as det 
		inner join LocationLotDet as loc on det.LocLotDetId = loc.Id
		inner join InspectMstr as mstr on det.InspNo = mstr.InspNo
		inner join Location as l on det.LocTo = l.Code
		inner join Region as r on l.Region = r.Code
		inner join (select distinct Item, Location from #tempProductPlan) as p on loc.Item = p.Item and det.LocTo <> p.Location
		where mstr.IsSeperated = 0 and mstr.[Status] = 'Create'
		and l.IsMRP = 1 and r.Plant = @Plant
		group by loc.Item
		union all
		--获取在途
		select oDet.Item, SUM(iDet.Qty - ISNULL(iDet.RecQty, 0)) as InTransitQty 
		from IpDet as iDet
		inner join IpMstr as iMstr on iDet.IpNo = iMstr.IpNo
		inner join OrderLocTrans as oTrans on iDet.OrderLocTransId = oTrans.Id
		inner join Location as l on oTrans.Loc = l.Code
		inner join Region as r on l.Region = r.Code
		inner join OrderDet as oDet on oTrans.OrderDetId = oDet.Id
		inner join OrderMstr as oMstr on oDet.OrderNo = oMstr.OrderNo
		inner join (select distinct Item, Flow from #tempProductPlan) as p on oDet.Item = p.Item and oMstr.Flow <> p.Flow
		where oMstr.[Type] <> 'Distribution' and iMstr.[Status] = 'Create' and oMstr.SubType = 'Nml' and r.Plant = @Plant
		group by oDet.Item) as a inner join Item as i on a.Item = i.Code 
		group by Item




		--if @trancount = 0
		--begin
		--	begin tran
		--end

		--if @trancount = 0 
		--begin  
		--	commit
		--end
	end try
	begin catch
        if @trancount = 0
        begin
            rollback
        end 
       
		set @Msg = N'运行主生产计划异常' + Error_Message()
		insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, @DateNow, 'Error', @Msg, @DateTimeNow, @RunUser)
		RAISERROR(@Msg, 16, 1) 

	end catch 
END 


