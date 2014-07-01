SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunShipPlan') 
     DROP PROCEDURE RunShipPlan
GO

CREATE PROCEDURE [dbo].RunShipPlan
(
	@RunUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateTimeNow datetime = GetDate()
	declare @DateNow datetime = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow))
	declare @Msg nvarchar(MAX) = ''
	declare @trancount int = @@trancount
	declare @ReleaseNo int
	declare @BatchNo int
	declare @MaxMstrId int
	declare @MinWindowTime datetime
	declare @RowId int
	declare @MaxRowId int
	declare @DistributionFlow varchar(50)
	
	exec GetNextSequence 'RunShipPlan', @BatchNo output
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

		create table #tempEffCustScheduleDet
		(
			Id int Primary Key,
			MstrId int,
			Flow varchar(50),
			ShipFlow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			ItemRef varchar(50),
			Qty decimal(18, 8),
			ShipQty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			UC decimal(18, 8),
			Location varchar(50),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempDistributionFlow
		(
			RowId int Identity(1, 1),
			Flow varchar(50),
		)

		create table #tempShipFlowDet
		(
			RowId int Identity(1, 1),
			Flow varchar(50),
			LeadTime decimal(18 ,8),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),
			UC decimal(18, 8),
			LocFrom varchar(50),
			LocTo varchar(50),
			LocQty decimal(18, 8),
			InTransitQty decimal(18, 8),
			SafeStock decimal(18, 8),
			ActiveQty decimal(18, 8)
		)

		create table #tempShipPlan
		(
			Flow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			ShipQty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			UC decimal(18, 8),
			LocFrom varchar(50),
			LocTo varchar(50),
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_WindowTime on #tempShipPlan(WindowTime asc)

		--更新福特计划的发货数
		update det set ShipQty = p.CurrenCumQty
		from CustScheduleDet as det inner join EDI_FordPlan as p on det.FordPlanId = p.Id



		-----------------------------↓获取客户日程-----------------------------
		--选取开始日期大于等于当天的所有客户日程
		insert into #tempEffCustScheduleDet(Id, MstrId, Flow, ShipFlow, Item, ItemDesc, ItemRef, Qty, ShipQty,
		Uom, BaseUom, UC, Location, StartTime, WindowTime)
		select det.Id, mstr.Id as MstrId, mstr.Flow, mstr.ShipFlow, det.Item, det.ItemDesc, det.ItemRef, det.Qty, ISNULL(det.ShipQty, 0) as ShipQty,
		det.Uom, i.Uom as BaseUom, det.UC, det.Loc as Location, det.StartTime, det.DateFrom as WindowTime
		from CustScheduleDet as det inner join CustScheduleMstr as mstr on det.ScheduleId = mstr.Id
		inner join Item as i on det.Item = i.Code
		where mstr.[Type] = 'Daily' and mstr.[Status] = 'Submit' and det.StartTime >= @DateNow 
		and (det.Qty > det.ShipQty or (det.ShipQty is null and det.Qty > 0))

		if not exists(select top 1 1 from #tempEffCustScheduleDet)
		begin
			insert into #tempMsg(Lvl, Msg) values('Info', N'没有找到有效的客户日程')
			return			
		end

		--取最新日程的所有明细，旧日程的明细要删除和最新日程重复的明细，依次循环得到有效日程
		--原理：防止客户给出的日程没有包含最近几天的需求，最近几天的只能取旧日程上的明细，但是要保证和最新的日程不出现重复
		insert into #tempDistributionFlow(Flow) select distinct Flow from #tempEffCustScheduleDet
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempDistributionFlow
		while @RowId <= @MaxRowId
		begin
			select @DistributionFlow = Flow from #tempDistributionFlow where RowId = @RowId

			select @MaxMstrId = MAX(MstrId) from #tempEffCustScheduleDet where Flow = @DistributionFlow
			select @MinWindowTime = MIN(WindowTime) from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId = @MaxMstrId
			while exists(select top 1 1 from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId < @MaxMstrId and WindowTime > @MinWindowTime)
			begin
				delete from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId < @MaxMstrId and WindowTime > @MinWindowTime
				if exists(select top 1 1 from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId < @MaxMstrId)
				begin --没有更小版本的客户日程，跳出循环
					break;
				end
				else
				begin --取下一个版本的客户日程
					select @MaxMstrId = MAX(MstrId) from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId < @MaxMstrId
					select @MinWindowTime = MIN(WindowTime) from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId = @MaxMstrId
				end
			end

			set @RowId = @RowId + 1
		end

		--计算单位换算
		update #tempEffCustScheduleDet set UnitQty = 1 where Uom = BaseUom
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempEffCustScheduleDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempEffCustScheduleDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempEffCustScheduleDet as det inner join UomConv as c on det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null and c.Item is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempEffCustScheduleDet as det inner join UomConv as c on det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null and c.Item is null

		--删除没有维护单位换算的物料
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, LocFrom, StartTime, WindowTime, Msg) 
		select 'Error', Flow, Item, Qty, Uom, Location, StartTime, WindowTime, N'销售路线的物料没有维护单位换算' from #tempEffCustScheduleDet where UnitQty is null
		delete from #tempEffCustScheduleDet where UnitQty is null
		-----------------------------↑获取客户日程-----------------------------



		-----------------------------↓获取发运路线-----------------------------
		--获取发运路线和发运库位的库存（中间仓库存）
		insert into #tempShipFlowDet(Flow, LeadTime, Item, ItemDesc, RefItemCode, Uom, BaseUom, UC, LocFrom, LocTo, LocQty, SafeStock)
		select det.Flow, ISNULL(mstr.MRPLeadTime, 0) as LeadTime, det.Item, i.Desc1, det.RefItemCode, det.Uom, i.Uom as BaseUom, det.UC, mstr.LocFrom, mstr.LocTo, ISNULL(loc.Qty, 0), ISNULL(det.SafeStock, 0) 
		from FlowDet as det
		inner join FlowMstr as mstr on det.Flow = mstr.Code
		inner join Item as i on det.Item = i.Code
		left join LocationDet as loc on det.Item = loc.Item and mstr.LocTo = loc.Location
		where Flow in (select ShipFlow from FlowMstr where [Type] = 'Distribution' and IsActive = 1 and ShipFlow is not null)

		--更新在途数量
		update #tempShipFlowDet set InTransitQty = ISNULL(t.InTransitQty, 0), ActiveQty = LocQty + ISNULL(t.InTransitQty, 0) - SafeStock
		from #tempShipFlowDet as det left join (select oMstr.Flow, oDet.Item, SUM(iDet.Qty - ISNULL(iDet.RecQty, 0)) as InTransitQty from IpDet as iDet
												inner join IpMstr as iMstr on iDet.IpNo = iMstr.IpNo
												inner join OrderLocTrans as oTrans on iDet.OrderLocTransId = oTrans.Id
												inner join OrderDet as oDet on oTrans.OrderDetId = oDet.Id
												inner join OrderMstr as oMstr on oDet.OrderNo = oMstr.OrderNo
												where oMstr.flow in (select distinct Flow from #tempShipFlowDet)
												and iMstr.[Status] = 'Create' and oMstr.SubType = 'Nml'
												group by oMstr.Flow, oDet.Item) as t on det.Flow = t.Flow and det.Item = t.Item

		--计算单位换算
		update #tempShipFlowDet set UnitQty = 1 where Uom = BaseUom
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null and c.Item is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null and c.Item is null
		
		--删除没有维护单位换算的物料
		insert into #tempMsg(Lvl, Flow, Item, Uom, LocFrom, LocTo, Msg) 
		select 'Error', Flow, Item, Uom, LocFrom, LocTo, N'发运路线的物料没有维护单位换算' from #tempShipFlowDet where UnitQty is null
		delete from #tempShipFlowDet where UnitQty is null

		--发运路线没有维护物料
		insert into #tempMsg(Lvl, Flow, Item, Uom, LocFrom, LocTo, Msg) 
		select 'Error', c.ShipFlow, c.Item, c.Uom, c.Location, null, N'物料在发运路线中不存在' 
		from #tempEffCustScheduleDet as c left join #tempShipFlowDet as f on c.ShipFlow = f.Flow and c.Item = f.Item
		where f.RowId is null
		-----------------------------↑获取发运路线-----------------------------



		-----------------------------↓计算发运计划-----------------------------
		--转有发运路线的（毛需求）
		insert into #tempShipPlan(Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select flow.Flow, flow.Item, req.ItemDesc, flow.RefItemCode, (req.Qty - req.ShipQty) * req.UnitQty / flow.UnitQty, --先把客户日程的单位转为基本单位，在转为发运计划的单位
		req.Uom, req.BaseUom, req.UnitQty, flow.UC, flow.LocFrom, flow.LocTo, DATEADD(day, -flow.LeadTime, StartTime), StartTime  --客户日程的开始时间就是发运计划的窗口时间
		from #tempEffCustScheduleDet as req inner join #tempShipFlowDet as flow on req.ShipFlow = flow.Flow and req.Item = flow.Item

		--根据开始时间依次扣减库存（含在途库存，不考虑在途库存的到货时间）
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempShipFlowDet
		while (@RowId <= @MaxRowId)
		begin
			declare @ActiveQty decimal(18, 8)
			declare @LastActiveQty decimal(18, 8)
			declare @Flow varchar(50)
			declare @Item varchar(50)

			select @ActiveQty = ActiveQty, @Flow = Flow, @Item = Item from #tempShipFlowDet where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set ShipQty = CASE WHEN @ActiveQty >= ShipQty THEN 0 WHEN @ActiveQty < ShipQty and @ActiveQty>0 THEN ShipQty - @ActiveQty ELSE ShipQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = ShipQty
				from #tempShipPlan as det with(INDEX(IX_WindowTime))
				where det.Flow = @Flow and det.Item = @Item
			end

			set @RowId = @RowId + 1
		end

		--转没有发运路线的，销售路线直接就是发运路线
		insert into #tempShipPlan(Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select Flow, Item, ItemDesc, ItemRef, (Qty - ShipQty), Uom, BaseUom, UnitQty, UC, Location, null, StartTime, WindowTime
		from #tempEffCustScheduleDet where ShipFlow is null

		--低于安全库存的转为当天的发运计划
		update #tempShipPlan set ShipQty -= d.ActiveQty
		from #tempShipPlan as p inner join #tempShipFlowDet as d on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0
		insert into #tempShipPlan(Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select d.Flow, d.Item, d.ItemDesc, d.RefItemCode, -d.ActiveQty, d.Uom, d.BaseUom, d.UnitQty, d.UC, d.LocFrom, d.LocTo, @DateNow, DATEADD(day, d.LeadTime, @DateNow) 
		from #tempShipFlowDet as d left join #tempShipPlan as p on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0 and p.Flow is null

		--日期小于今天的量全部转为今天
		update b set ShipQty = b.ShipQty + a.ShipQty
		from #tempShipPlan as a inner join #tempShipPlan as b on a.Flow = b.Flow and a.ITem = b.Item
		where b.StartTime = @DateNow and a.StartTime < @DateNow
		insert into #tempShipPlan(Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select a.Flow, a.Item, a.ItemDesc, a.RefItemCode, a.ShipQty, a.Uom, a.BaseUom, a.UnitQty, a.UC, a.LocFrom, a.LocTo, @DateNow, DATEADD(day, d.LeadTime, @DateNow) 
		from #tempShipPlan as a left join #tempShipPlan as b on a.Flow = b.Flow and a.ITem = b.Item and b.StartTime = @DateNow
		inner join #tempShipFlowDet as d on a.Flow = d.Flow and a.Item = d.Item
		where a.StartTime < @DateNow and b.Flow is null
		-----------------------------↑计算发运计划-----------------------------

		if @trancount = 0
		begin
            begin tran
        end

		-----------------------------↓生成发运计划-----------------------------
		--删除未释放的发运计划
		delete from MRP_ShipPlanDet where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanMstr where Status = 'Create'

		--获取ReleaseNo
		select @ReleaseNo = ISNULL(MAX(ReleaseNo), 0) + 1 from MRP_ShipPlanMstr

		--新增发运计划头
		insert into MRP_ShipPlanMstr(ReleaseNo, BatchNo, EffDate, [Status], CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		values(@ReleaseNo, @BatchNo, @DateNow, 'Create', @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1)

		--新增发运计划明细
		insert into MRP_ShipPlanDet(ShipPlanId, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @@Identity, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempShipPlan where ShipQty > 0
		-----------------------------↑生成发运计划-----------------------------

		insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Flow, Item, Qty, Uom, LocFrom, LocTo, StartTime, WindowTime, Msg, CreateDate, CreateUser) 
		select @BatchNo, @DateNow, Lvl, Flow, Item, Qty, Uom, LocFrom, LocTo, StartTime, WindowTime, Msg, @DateTimeNow, @RunUser from #tempMsg

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
       
		set @Msg = N'运行发运计划异常' + Error_Message()
		insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, @DateNow, 'Error', @Msg, @DateTimeNow, @RunUser)
		RAISERROR(@Msg, 16, 1) 

	end catch 
END 


