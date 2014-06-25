SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunShipPlan') 
DROP PROCEDURE RunShipPlan
GO
CREATE PROCEDURE [dbo].RunShipPlan --exec RunShipPlan 'su'
(
	@RunUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateTimeNow datetime
	declare @DateNow datetime
	declare @Msg nvarchar(MAX)
	declare @trancount int
	declare @ReleaseNo int
	declare @BatchNo int
	declare @MaxMstrId int
	declare @MinWindowTime datetime
	declare @RowId int
	declare @MaxRowId int
	declare @DistributionFlow varchar(50)
	declare @ShipPlanId int

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''
	set @trancount = @@trancount
	
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
			MaxStock decimal(18, 8),
			ActiveQty decimal(18, 8)
		)
		
		create table #tempShipPlanDet
		(
			UUID varchar(50) primary key, 
			DistributionFlow varchar(50),
			Flow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			ReqQty decimal(18, 8),
			ShipQty decimal(18, 8),
			OrderQty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			UC decimal(18, 8),
			LocFrom varchar(50),
			LocTo varchar(50),
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_WindowTime on #tempShipPlanDet(WindowTime asc)

		create table #tempShipPlanDetTrace
		(
			RowId int identity(1, 1)  primary key,
			UUID varchar(50), 
			DistributionFlow varchar(50),
			Item varchar(50),
			ReqDate datetime,
			ReqQty decimal(18, 8)
		)

		create table #tempOpenOrder
		(
			RowId int identity(1, 1)  primary key,
			UUID varchar(50), 
			Flow varchar(50),
			OrderNo varchar(50),
			Item varchar(50),
			StartTime datetime,
			WindowTime datetime,
			EffDate datetime,
			OrderQty decimal(18, 8),
			ShipQty decimal(18, 8),
			RecQty decimal(18, 8),
		)

		--更新福特计划的发货数
		--update det set ShipQty = p.CurrenCumQty
		--from CustScheduleDet as det inner join EDI_FordPlan as p on det.FordPlanId = p.Id

		--更新客户日程开始时间
		update CustScheduleDet set StartTime = DATEADD(day, -ISNULL(fMstr.MRPLeadTime, 0), det.DateFrom)
		from CustScheduleDet as det 
		inner join CustScheduleMstr as mstr on det.ScheduleId = mstr.Id
		inner join FlowMstr as fMstr on det.Flow = fMstr.Code
		where  mstr.[Type] = 'Daily' and mstr.[Status] = 'Submit' and det.DateFrom >= @DateNow 


		-----------------------------↓获取客户日程-----------------------------
		--选取开始日期大于等于当天的所有客户日程
		insert into #tempEffCustScheduleDet(Id, MstrId, Flow, ShipFlow, Item, ItemDesc, ItemRef, Qty,
		Uom, BaseUom, UC, Location, StartTime, WindowTime)
		select det.Id, mstr.Id as MstrId, mstr.Flow, mstr.ShipFlow, det.Item, det.ItemDesc, det.ItemRef, det.Qty,
		det.Uom, i.Uom as BaseUom, det.UC, det.Loc as Location, det.StartTime, det.DateFrom as WindowTime
		from CustScheduleDet as det 
		inner join CustScheduleMstr as mstr on det.ScheduleId = mstr.Id
		inner join Item as i on det.Item = i.Code
		where mstr.[Type] = 'Daily' and mstr.[Status] = 'Submit' and det.StartTime >= @DateNow 
		--and (det.Qty > det.ShipQty or (det.ShipQty is null and det.Qty > 0))

		if not exists(select top 1 1 from #tempEffCustScheduleDet)
		begin
			RAISERROR(N'没有找到有效的客户日程。', 16, 1)
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
		from #tempEffCustScheduleDet as det inner join UomConv as c on det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
		where det.UnitQty is null and c.Item is null

		--删除没有维护单位换算的物料
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, LocFrom, StartTime, WindowTime, Msg)
		select 'Error', Flow, Item, Qty, Uom, Location, StartTime, WindowTime, 
		N'销售路线[' + Flow + N']物料[ ' + Item + N']没有维护单位[ ' + Uom + N' ]和基本单位[' + BaseUom + N']的换算率' 
		from #tempEffCustScheduleDet where UnitQty is null
		delete from #tempEffCustScheduleDet where UnitQty is null
		-----------------------------↑获取客户日程-----------------------------



		-----------------------------↓获取发运路线-----------------------------
		--获取发运路线和发运库位的库存（中间仓库存）
		insert into #tempShipFlowDet(Flow, LeadTime, Item, ItemDesc, RefItemCode, Uom, BaseUom, UC, LocFrom, LocTo, 
		LocQty, InTransitQty, SafeStock, MaxStock, ActiveQty)
		select det.Flow, ISNULL(mstr.MRPLeadTime, 0) as LeadTime, det.Item, i.Desc1, det.RefItemCode, det.Uom, i.Uom as BaseUom, det.UC, mstr.LocFrom, mstr.LocTo, 
		ISNULL(loc.Qty, 0), ISNULL(loc.InTransitQty, 0), ISNULL(det.SafeStock, 0), ISNULL(det.MaxStock, 0), (ISNULL(loc.Qty, 0) - ISNULL(det.SafeStock, 0))
		from FlowDet as det
		inner join FlowMstr as mstr on det.Flow = mstr.Code
		inner join Item as i on det.Item = i.Code
		left join MRP_LocationDetSnapShot as loc on det.Item = loc.Item and mstr.LocTo = loc.Location
		where Flow in (select ShipFlow from FlowMstr where [Type] = 'Distribution' and IsActive = 1 and ShipFlow is not null)

		--计算单位换算
		update #tempShipFlowDet set UnitQty = 1 where Uom = BaseUom 
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Item = c.Item  and det.Uom = c.AltUom  and det.BaseUom = c.BaseUom 
		where det.UnitQty is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
		where det.UnitQty is null
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Uom = c.AltUom  and det.BaseUom = c.BaseUom 
		where det.UnitQty is null and c.Item is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
		where det.UnitQty is null and c.Item is null
		
		--删除没有维护单位换算的物料
		insert into #tempMsg(Lvl, Flow, Item, Uom, LocFrom, LocTo, Msg) 
		select 'Error', Flow, Item, Uom, LocFrom, LocTo, 
		N'发运路线[' + Flow + N']物料[ ' + Item + N']没有维护单位[ ' + Uom + N']和基本单位[' + BaseUom + N']的换算率' 
		from #tempShipFlowDet where UnitQty is null
		delete from #tempShipFlowDet where UnitQty is null

		--发运路线没有维护物料
		insert into #tempMsg(Lvl, Flow, Item, Uom, LocFrom, LocTo, Msg) 
		select 'Error', c.ShipFlow, c.Item, c.Uom, c.Location, null, 
		N'物料[ ' + c.Item + N']在发运路线[' + c.ShipFlow + N']中不存在' 
		from #tempEffCustScheduleDet as c left join #tempShipFlowDet as f on c.ShipFlow = f.Flow and c.Item = f.Item
		where f.RowId is null
		-----------------------------↑获取发运路线-----------------------------



		-----------------------------↓缓存Open Order-----------------------------
		--插入发运路线的Open Order
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord
		inner join #tempShipFlowDet as fDet on ord.Flow = fDet.Flow and ord.Item = fDet.Item

		--插入销售路线的Order Order
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord 
		inner join (select distinct Flow, Item from #tempEffCustScheduleDet where ShipFlow is null) as dFlow on ord.Flow = dFlow.Flow and ord.Item = dFlow.Item
		-----------------------------↑缓存Open Order-----------------------------

		

		-----------------------------↓计算发运计划-----------------------------
		--转有发运路线的（毛需求）
		insert into #tempShipPlanDet(UUID, DistributionFlow, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), req.Flow, flow.Flow, flow.Item, req.ItemDesc, req.ItemRef, req.Qty * req.UnitQty / flow.UnitQty, /*先把客户日程的单位转为基本单位，在转为发运计划的单位*/
		req.Uom, req.BaseUom, req.UnitQty, flow.UC, flow.LocFrom, flow.LocTo, DATEADD(day, -ISNULL(flow.LeadTime, 0), StartTime), StartTime  --客户日程的开始时间就是发运计划的窗口时间
		from #tempEffCustScheduleDet as req 
		inner join #tempShipFlowDet as flow on req.ShipFlow = flow.Flow and req.Item = flow.Item

		--删除开始时间小于今天的计划
		delete from #tempShipPlanDet where StartTime < @DateNow

		--转没有发运路线的，销售路线直接就是发运路线
		insert into #tempShipPlanDet(UUID, DistributionFlow, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), Flow, Flow, Item, ItemDesc, ItemRef, Qty, Uom, BaseUom, UnitQty, UC, Location, null, StartTime, WindowTime
		from #tempEffCustScheduleDet where ShipFlow is null

		--记录需求中间表
		insert into #tempShipPlanDetTrace(UUID, DistributionFlow, Item, ReqDate, ReqQty) 
		select UUID, DistributionFlow, Item, WindowTime, ShipQty from #tempShipPlanDet

		--合并相同发运路线+物料的需求
		--合并毛需求至一行（最小UUID)
		update p set ShipQty = t.ShipQty
		from #tempShipPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(ShipQty) as ShipQty from #tempShipPlanDet group by Flow, Item, StartTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--合并需求中间表，把需求中间表UUID全部更新为最小UUID
		update dt set UUID = t.MinUUID
		from #tempShipPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Flow, Item, StartTime from #tempShipPlanDet group by Flow, Item, StartTime having count(1) > 1) as t
		on p.Flow = t.Flow and p.Item = t.Item and p.StartTime = t.StartTime
		inner join #tempShipPlanDetTrace as dt on p.UUID = dt.UUID
		--删除重复毛需求 q
		delete p from #tempShipPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Flow, Item, StartTime from #tempShipPlanDet group by Flow, Item, StartTime having count(1) > 1) as t
		on p.Flow = t.Flow and p.Item = t.Item and p.StartTime = t.StartTime
		where p.UUID <> MinUUID

		--低于安全库存的转为当天的发运计划
		update p set ShipQty = ShipQty - d.ActiveQty
		from #tempShipPlanDet as p inner join #tempShipFlowDet as d on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0
		insert into #tempShipPlanDet(UUID, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), d.Flow, d.Item, d.ItemDesc, d.RefItemCode, -d.ActiveQty, d.Uom, d.BaseUom, d.UnitQty, d.UC, d.LocFrom, d.LocTo, @DateNow, DATEADD(day, d.LeadTime, @DateNow) 
		from #tempShipFlowDet as d left join #tempShipPlanDet as p on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0 and p.Flow is null

		--根据开始时间依次扣减库存（不含在途库存）
		set @RowId = null
		set @MaxRowId = null
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempShipFlowDet
		while (@RowId <= @MaxRowId)
		begin
			declare @ActiveQty decimal(18, 8)
			declare @LastActiveQty decimal(18, 8)
			declare @Flow varchar(50)
			declare @Item varchar(50)
			set @ActiveQty = 0
			set @LastActiveQty = 0
			set @Flow = null
			set @Item = null

			select @ActiveQty = ActiveQty, @Flow = Flow, @Item = Item from #tempShipFlowDet where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set ShipQty = CASE WHEN @ActiveQty >= ShipQty THEN 0 WHEN @ActiveQty < ShipQty and @ActiveQty>0 THEN ShipQty - @ActiveQty ELSE ShipQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = ShipQty
				from #tempShipPlanDet as det with(INDEX(IX_WindowTime))
				where det.Flow = @Flow and det.Item = @Item
			end

			set @RowId = @RowId + 1
		end

		--更新订单数
		update pd set OrderQty = ord.OrderQty
		from #tempShipPlanDet as pd
		inner join (select Flow, Item, EffDate, SUM(ISNULL(OrderQty, 0) - ISNULL(ShipQty, 0)) as OrderQty from #tempOpenOrder group by Flow, Item, EffDate) as ord 
		on pd.Flow = ord.Flow and pd.Item = ord.Item and pd.StartTime = ord.EffDate
		insert into #tempShipPlanDet(UUID, Flow, Item, ItemDesc, RefItemCode, ShipQty, OrderQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), d.Flow, d.Item, d.ItemDesc, d.RefItemCode, 0, ord.OrderQty, d.Uom, d.BaseUom, d.UnitQty, d.UC, d.LocFrom, d.LocTo, ord.EffDate, DATEADD(day, d.LeadTime, ord.EffDate) 
		from (select Flow, Item, EffDate, SUM(ISNULL(OrderQty, 0) - ISNULL(ShipQty, 0)) as OrderQty from #tempOpenOrder group by Flow, Item, EffDate) as ord 
		left join #tempShipPlanDet as p on p.Flow = ord.Flow and p.Item = ord.Item and p.StartTime = ord.EffDate
		inner join #tempShipFlowDet as d on d.Flow = ord.Flow and d.Item = ord.Item
		where p.Flow is null

		update ord set UUID = pl.UUID
		from #tempOpenOrder as ord inner join #tempShipPlanDet as pl on pl.Flow = ord.Flow and pl.Item = ord.Item and pl.StartTime = ord.EffDate

		--汇总发运需求
		update d set ReqQty = ISNULL(dt.ReqQty, 0) from #tempShipPlanDet as d 
		left join (select UUID, SUM(ISNULL(ReqQty, 0)) as ReqQty from #tempShipPlanDetTrace group by UUID) as dt on d.UUID = dt.UUID
		-----------------------------↑计算发运计划-----------------------------
	end try
	begin catch
		set @Msg = N'运行发运计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1)
		return
	end catch 

	begin try
		if @trancount = 0
		begin
            begin tran
        end

		-----------------------------↓生成发运计划-----------------------------
		--删除未释放的发运计划
		delete from MRP_ShipPlanOpenOrder where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanInitLocationDet where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanDetTrace where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanDet where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanMstr where Status = 'Create'

		--获取ReleaseNo
		select @ReleaseNo = ISNULL(MAX(ReleaseNo), 0) + 1 from MRP_ShipPlanMstr

		--新增发运计划头
		insert into MRP_ShipPlanMstr(ReleaseNo, BatchNo, EffDate, [Status], CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		values(@ReleaseNo, @BatchNo, @DateNow, 'Create', @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1)

		--获取发运计划头Id
		set @ShipPlanId = @@Identity
		
		--新增发运计划期初库存
		insert into MRP_ShipPlanInitLocationDet(ShipPlanId, Location, Item, InitStock, SafeStock, MaxStock, InTransitQty, CreateDate, CreateUser)
		select @ShipPlanId, LocTo, Item, LocQty, SafeStock, MaxStock, InTransitQty, @DateTimeNow, @RunUser from #tempShipFlowDet

		--发货数按包装圆整
		update #tempShipPlanDet set ShipQty = ceiling(ShipQty / UC) * UC where ShipQty > 0 and UC > 0

		--新增发运计划明细
		insert into MRP_ShipPlanDet(ShipPlanId, UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgShipQty, ShipQty, OrderQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @ShipPlanId, UUID, Flow, Item, ItemDesc, RefItemCode, 
		ISNULL(ReqQty, 0), ISNULL(ShipQty, 0), ISNULL(ShipQty, 0), ISNULL(OrderQty, 0), Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, 
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempShipPlanDet

		--新增发运计划明细追溯表
		insert into MRP_ShipPlanDetTrace(ShipPlanId, UUID, DistributionFlow, Item, ReqDate, ReqQty, CreateDate, CreateUser)
		select @ShipPlanId, UUID, DistributionFlow, Item, ReqDate, ReqQty, @DateTimeNow, @RunUser from #tempShipPlanDetTrace

		--新增Open Order追溯表
		insert into MRP_ShipPlanOpenOrder(ShipPlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser)
		select @ShipPlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, @DateTimeNow, @RunUser from #tempOpenOrder
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
       
		set @Msg = N'运行发运计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


