SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunPurchasePlan') 
     DROP PROCEDURE RunPurchasePlan
GO

CREATE PROCEDURE [dbo].RunPurchasePlan
(
	@Plant varchar(50),
	@RunUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateTimeNow datetime
	declare @DateNow datetime
	declare @Msg nvarchar(MAX)
	declare @trancount int
	declare @BatchNo int
	declare @RowId int
	declare @MaxRowId int
	declare @ProdLine varchar(50)
	declare @MaxMstrId int
	declare @MinPlandate datetime
	declare @Bom varchar(50)
	declare @EffDate datetime
	declare @BomQty decimal(18, 8)
	declare @DetId int
	declare @MstrId int
	declare @RefPlanNo varchar(50)
	declare @ProdItem varchar(50)
	declare @Item varchar(50)
	declare @Qty decimal(18, 8)
	declare @LastQty decimal(18, 8)
	declare @ReleaseNo varchar(50)
	declare @PurchasePlanId int

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''
	set @trancount = @@trancount

	exec GetNextSequence 'RunPurchasePlan', @BatchNo output
	begin try
		create table #tempMsg
		(
			Lvl varchar(50),
			ProdLine varchar(50),
			Item varchar(50),
			Uom varchar(50),
			Qty decimal(18, 8),
			PlanDate datetime,
			Bom varchar(50),
			Msg varchar(500)
		)

		create table #tempEffShiftPlan
		(
			RowId int Primary Key,
			DetId int,
			MstrId int,
			RefPlanNo varchar(50),
			ProdLine varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			Bom varchar(50),
			Qty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			Location varchar(50),
			PlanDate datetime
		)

		create table #tempProdLine
		(
			RowId int Identity(1, 1),
			ProdLine varchar(50),
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
			BackFlushMethod varchar(50),
		)

		create table #tempMaterialPlanDet
		(
			UUID varchar(50) primary key,
			Item varchar(50),
			ItemDesc varchar(100),
			ReqQty decimal(18, 8),
			RateQty decimal(18, 8),
			ScrapPct decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempMergeMaterialPlanDet
		(
			UUID varchar(50) primary key,
			Item varchar(50),
			ItemDesc varchar(100),
			BaseReqQty decimal(18, 8),
			BasePurchaseQty decimal(18, 8),
			BaseUom varchar(5),
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_WindowTime on #tempMergeMaterialPlanDet(WindowTime asc)

		create table #tempPurchasePlanDet
		(
			UUID varchar(50) primary key,
			PurchaseFlow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			BaseReqQty decimal(18, 8),
			BasePurchaseQty decimal(18, 8),
			ReqQty decimal(18, 8),
			PurchaseQty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			UC decimal(18, 8),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempMaterialPlanDetTrace
		(
			UUID varchar(50) primary key, 
			DetId int,
			MstrId int,
			RefPlanNo varchar(50),
			ProdLine varchar(50),
			ProdItem varchar(50),
			ProdQty decimal(18, 8),
			RateQty decimal(18, 8),
			ScrapPct decimal(18, 8),
			BomUnitQty decimal(18, 8),
			PurchaseUnitQty decimal(18, 8),
			BomUom varchar(5),
			PurchaseUom varchar(5),
			PlanDate datetime
		)

		create table #tempLocatoinDet
		(
			RowId int identity(1, 1) primary key,
			Item varchar(50),
			SafeStock decimal(18, 8),
			Qty decimal(18, 8),
			InTransitQty decimal(18, 8),
			InSpectQty decimal(18, 8),
			ActiveQty decimal(18, 8),
		)

		-----------------------------↓获取班产计划-----------------------------
		--选取开始日期大于等于当天的所有客户日程
		insert into #tempEffShiftPlan(DetId, MstrId, RefPlanNo, ProdLine, Item, ItemDesc, RefItemCode, Qty,
		Uom, BaseUom, Location, PlanDate)
		select det.Id, mstr.Id as MstrId, mstr.RefPlanNo, mstr.ProdLine, det.Item, det.ItemDesc, det.RefItemCode, det.Qty,
		det.Uom, i.Uom as BaseUom, fMstr.LocTo, det.PlanDate
		from MRP_ShiftPlanDet as det inner join MRP_ShiftPlanMstr as mstr on det.PlanId = mstr.Id
		inner join FlowMstr as fMstr on mstr.ProdLine = fMstr.Code
		inner join Item as i on det.Item = i.Code
		inner join Region as r on fMstr.PartyFrom = r.Code
		where mstr.[Status] = 'Submit' and PlanDate >= @DateNow and r.Plant = @Plant

		if not exists(select top 1 1 from #tempEffShiftPlan)
		begin
			insert into #tempMsg(Lvl, Msg) values('Info', N'没有找到有效的班产计划')
			return			
		end

		--取最新班产计划的所有明细，旧班产计划的明细要删除和最新班产计划重复的明细，依次循环得到有效班产计划
		insert into #tempProdLine(ProdLine) select distinct ProdLine from #tempEffShiftPlan
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempProdLine
		while @RowId <= @MaxRowId
		begin
			select @ProdLine = ProdLine from #tempProdLine where RowId = @RowId

			select @MaxMstrId = MAX(MstrId) from #tempEffShiftPlan where ProdLine = @ProdLine
			select @MinPlandate = MIN(Plandate) from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId = @MaxMstrId
			while exists(select top 1 1 from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId < @MaxMstrId and Plandate > @MinPlandate)
			begin
				delete from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId < @MaxMstrId and Plandate > @MinPlandate
				if exists(select top 1 1 from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId < @MaxMstrId)
				begin --没有更小版本的客户日程，跳出循环
					break;
				end
				else
				begin --取下一个版本的客户日程
					select @MaxMstrId = MAX(MstrId) from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId < @MaxMstrId
					select @MinPlandate = MIN(Plandate) from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId = @MaxMstrId
				end
			end

			set @RowId = @RowId + 1
		end

		--计算单位换算
		update #tempEffShiftPlan set UnitQty = 1 where Uom = BaseUom
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempEffShiftPlan as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempEffShiftPlan as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempEffShiftPlan as det inner join UomConv as c on det.Uom = c.AltUom and det.BaseUom = c.BaseUom 
		where det.UnitQty is null and c.Item is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempEffShiftPlan as det inner join UomConv as c on det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
		where det.UnitQty is null and c.Item is null

		--删除没有维护单位换算的物料
		insert into #tempMsg(Lvl, ProdLine, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', ProdLine, Item, Qty, Uom, PlanDate, N'成品没有维护单位换算' from #tempEffShiftPlan where UnitQty is null
		delete from #tempEffShiftPlan where UnitQty is null
		-----------------------------↑获取班产计划-----------------------------
		


		-----------------------------↓展开Bom-----------------------------
		--查找Bom，先从生产线上找，在从物料上找，最后取物料代码
		update t set Bom = ISNULL(d.Bom, ISNULL(i.Bom, t.Item))
		from #tempEffShiftPlan as t 
		left join FlowDet as d on t.ProdLine = d.Flow and t.Item = d.Item
		inner join Item as i on t.Item = i.Code

		--记录日志生产线没有维护成品
		insert into #tempMsg(Lvl, ProdLine, Item, Qty, Uom, PlanDate, Msg) 
		select 'Warning', t.ProdLine, t.Item, t.Qty, t.Uom, t.PlanDate, N'生产线没有维护成品' 
		from #tempEffShiftPlan as t
		left join FlowDet as d on t.ProdLine = d.Flow and t.Item = d.Item
		where d.Id is null

		--记录日志没有Bom主数据
		insert into #tempMsg(Lvl, ProdLine, Item, Qty, Uom, PlanDate, Bom, Msg) 
		select 'Error', t.ProdLine, t.Item, t.Qty, t.Uom, t.PlanDate, t.Bom, N'没有Bom主数据' 
		from #tempEffShiftPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where t.ProdLine = @ProdLine and b.Code is null
		--删除班产计划
		delete t from #tempEffShiftPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where b.Code is null

		--记录日志没有Bom明细
		insert into #tempMsg(Lvl, ProdLine, Item, Qty, Uom, PlanDate, Bom, Msg) 
		select 'Error', t.ProdLine, t.Item, t.Qty, t.Uom, t.PlanDate, t.Bom, N'没有Bom明细' 
		from #tempEffShiftPlan as t
		left join BomDet as b on t.Bom = b.Bom
		where b.Bom is null
		--删除班产计划
		delete t from #tempEffShiftPlan as t
		left join BomDet as b on t.Bom = b.Bom
		where b.Bom is null

		--循环展开Bom
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempEffShiftPlan
		while @RowId <= @MaxRowId
		begin
			if exists(select top 1 1 from #tempEffShiftPlan where RowId = @RowId)
			begin
				select @DetId = DetId, @MstrId = MstrId, @RefPlanNo = RefPlanNo, @ProdLine = ProdLine, 
				@ProdItem = Item, @Bom = Bom, @EffDate = PlanDate, @BomQty = Qty 
				from #tempEffShiftPlan where RowId = @RowId
				
				truncate table #tempBomDetail
				insert into #tempBomDetail exec GetFlatBomDetail @Bom, @EffDate

				insert into #tempMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct,
				Uom, BaseUom, StartTime, WindowTime)
				select NEWID(), t.Item, i.Desc1, @BomQty * (t.RateQty + t.ScrapPct), t.RateQty, t.ScrapPct,
				t.Uom, i.Uom, DATEADD(day, -ISNULL(i.LeadTime, 0), @EffDate), @EffDate
				from #tempBomDetail as t inner join Item as i on t.Item = i.Code

				--计算单位换算（Bom单位转为基本单位）
				update #tempMaterialPlanDet set UnitQty = 1 where Uom = BaseUom
				update det set UnitQty = c.BaseQty / c.AltQty
				from #tempMaterialPlanDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.AltUom and det.BaseUom = c.BaseUom
				where det.UnitQty is null
				update det set UnitQty =  c.AltQty / c.BaseQty
				from #tempMaterialPlanDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom and det.BaseUom = c.AltUom
				where det.UnitQty is null
				update det set UnitQty = c.BaseQty / c.AltQty
				from #tempMaterialPlanDet as det inner join UomConv as c on det.Uom = c.AltUom and det.BaseUom = c.BaseUom 
				where det.UnitQty is null and c.Item is null
				update det set UnitQty =  c.AltQty / c.BaseQty
				from #tempMaterialPlanDet as det inner join UomConv as c on det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
				where det.UnitQty is null and c.Item is null

				--删除没有维护单位换算的物料
				insert into #tempMsg(Lvl, Item, Qty, Uom, PlanDate, Msg) 
				select 'Error', Item, ReqQty, Uom, WindowTime, N'物料没有维护单位换算' from #tempMaterialPlanDet where UnitQty is null
				delete from #tempMaterialPlanDet where UnitQty is null

				--记录追溯关系
				insert into #tempMaterialPlanDetTrace(UUID, DetId, MstrId, RefPlanNo, ProdLine, ProdItem, ProdQty, RateQty, ScrapPct, BomUnitQty, BomUom, PlanDate)
				select UUID, @DetId, @MstrId, @RefPlanNo, @ProdLine, @ProdItem, @BomQty, RateQty, ScrapPct, UnitQty, Uom, @EffDate 
				from #tempMaterialPlanDet
			end
			set @RowId = @RowId + 1
		end
		-----------------------------↑展开Bom-----------------------------



		-----------------------------↓合并采购需求-----------------------------
		--毛需求插入合并表
		insert into  #tempMergeMaterialPlanDet(UUID, Item, ItemDesc, BaseReqQty, BasePurchaseQty, BaseUom, StartTime, WindowTime)
		select UUID, Item, ItemDesc, ReqQty * UnitQty, ReqQty * UnitQty, BaseUom, StartTime, WindowTime from #tempMaterialPlanDet
		--合并毛需求至一行（最小UUID)
		update p set BaseReqQty = t.BaseReqQty
		from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(BaseReqQty) as BaseReqQty from #tempMergeMaterialPlanDet group by Item, WindowTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--合并需求中间表，把需求中间表UUID全部更新为最小UUID
		update dt set UUID = t.MinUUID
		from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, WindowTime from #tempMergeMaterialPlanDet group by Item, WindowTime having count(1) > 1) as t
		on p.Item = t.Item and p.WindowTime = t.WindowTime
		inner join #tempMaterialPlanDetTrace as dt on p.UUID = dt.UUID
		--删除重复毛需求
		delete p from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, WindowTime from #tempMergeMaterialPlanDet group by Item, WindowTime having count(1) > 1) as t
		on p.Item = t.Item and p.WindowTime = t.WindowTime
		where p.UUID <> MinUUID
		-----------------------------↑合并采购需求-----------------------------
		
		

		-----------------------------↓获取可用库存-----------------------------
		insert into #tempLocatoinDet(Item, SafeStock, Qty, InTransitQty, InSpectQty, ActiveQty)
		select Item, i.SafeStock, Qty, InTransitQty, InSpectQty, SUM(Qty + InTransitQty + InspectQty) - MAX(ISNULL(i.SafeStock, 0))
		from 
		(
		select loc.Item, SUM(loc.Qty) as Qty, SUM(loc.InTransitQty) as InTransitQty, SUM(loc.InspectQty) as InspectQty
		from MRP_LocationDetSnapShot as loc
		inner join (select distinct Item from #tempMaterialPlanDet) as p on loc.Item = p.Item
		where loc.Plant = @Plant
		group by loc.Item
		) as a inner join Item as i on a.Item = i.Code 
		group by Item
		-----------------------------↑获取可用库存-----------------------------


		
		-----------------------------↓计算净需求-----------------------------
		set @RowId = null
		set @MaxRowId = null
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempLocatoinDet
		while (@RowId <= @MaxRowId)
		begin
			set @Item = null
			set @Qty = null
			set @LastQty = null

			select @Qty = ActiveQty, @Item = Item from #tempLocatoinDet where RowId = @RowId
			if (@Qty > 0)
			begin
				update det set BasePurchaseQty = CASE WHEN @Qty >= BasePurchaseQty THEN 0 WHEN @Qty < BasePurchaseQty and @Qty > 0 THEN BasePurchaseQty - @Qty ELSE BasePurchaseQty END,
				@Qty = @Qty - @LastQty, @LastQty = BasePurchaseQty
				from #tempMergeMaterialPlanDet as det with(INDEX(IX_WindowTime))
				where det.Item = @Item
			end
			
			set @RowId = @RowId + 1
		end

		--日期小于今天的量全部转为今天
		--有今天的数据
		update at set UUID = bt.UUID
		from #tempMergeMaterialPlanDet as a
		inner join #tempMergeMaterialPlanDet as b on a.Item = b.Item
		inner join #tempMaterialPlanDetTrace as at on a.UUID = at.UUID
		inner join #tempMaterialPlanDetTrace as bt on b.UUID = bt.UUID
		where b.StartTime = @DateNow and a.StartTime < @DateNow
		update b set BasePurchaseQty = b.BasePurchaseQty + a.BasePurchaseQty, BaseReqQty = b.BaseReqQty + a.BaseReqQty
		from #tempMergeMaterialPlanDet as a inner join #tempMergeMaterialPlanDet as b on a.Item = b.Item
		where b.StartTime = @DateNow and a.StartTime < @DateNow
		update a set BasePurchaseQty = 0
		from #tempMergeMaterialPlanDet as a inner join #tempMergeMaterialPlanDet as b on a.Item = b.Item
		where b.StartTime = @DateNow and a.StartTime < @DateNow
		--没有今天的数据
		update a set StartTime = @DateNow, WindowTime = DATEADD(day, ISNULL(i.LeadTime, 0), @DateNow) 
		from #tempMergeMaterialPlanDet as a left join #tempMergeMaterialPlanDet as b on a.ITem = b.Item and b.StartTime = @DateNow
		inner join Item as i on a.Item = i.Code
		where a.StartTime < @DateNow and b.Item is null
		-----------------------------↑计算净需求-----------------------------



		-----------------------------↓查找采购路线-----------------------------
		insert into #tempPurchasePlanDet(UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, BaseReqQty, BasePurchaseQty, Uom, BaseUom, UC, StartTime, WindowTime)
		select t.UUID, d.Flow, t.Item, t.ItemDesc, d.RefItemCode, t.BaseReqQty, t.BasePurchaseQty, d.Uom, t.BaseUom, d.UC, t.StartTime, t.WindowTime
		from #tempMergeMaterialPlanDet as t
		inner join FlowDet as d on t.Item = d.Item
		inner join FlowMstr as m on d.Flow = m.Code
		inner join Region as r on m.PartyFrom = r.Code
		where r.Plant = @Plant
		

		--记录日志没有找到采购路线
		insert into #tempMsg(Lvl, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', Item, ReqQty, BaseUom, WindowTime, N'没有找到采购路线' from #tempPurchasePlanDet where PurchaseFlow is null
		--删除物料需求
		delete from #tempMaterialPlanDetTrace where UUID in (select UUID from #tempPurchasePlanDet where PurchaseFlow is null)
		delete from #tempPurchasePlanDet where PurchaseFlow is null

		--计算单位换算（基本单位转为采购单位）
		update #tempPurchasePlanDet set UnitQty = 1 where Uom = BaseUom
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempPurchasePlanDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempPurchasePlanDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempPurchasePlanDet as det inner join UomConv as c on det.Uom = c.AltUom and det.BaseUom = c.BaseUom 
		where det.UnitQty is null and c.Item is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempPurchasePlanDet as det inner join UomConv as c on det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
		where det.UnitQty is null and c.Item is null
		
		--更新追溯表
		update t set PurchaseUnitQty = m.UnitQty, PurchaseUom = m.Uom
		from #tempMaterialPlanDetTrace as t inner join #tempPurchasePlanDet as m on t.UUID = m.UUID

		--删除没有维护单位换算的物料
		insert into #tempMsg(Lvl, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', Item, ReqQty, Uom, WindowTime, N'物料没有维护单位换算' from #tempPurchasePlanDet where UnitQty is null
		delete from #tempPurchasePlanDet where UnitQty is null

		--数量转为采购单位
		update #tempPurchasePlanDet set ReqQty = BaseReqQty * UnitQty, PurchaseQty = BasePurchaseQty * UnitQty
		-----------------------------↑查找采购路线-----------------------------
		

		if @trancount = 0
		begin
			begin tran
		end


		-----------------------------↓生成物料需求计划-----------------------------
		--删除未释放的物料需求计划
		delete from MRP_PurchasePlanDetTrace where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanInitLocationDet where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanDet where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanMstr where Status = 'Create'

		--获取ReleaseNo
		select @ReleaseNo = ISNULL(MAX(ReleaseNo), 0) + 1 from MRP_PurchasePlanMstr

		--新增物料需求计划头
		insert into MRP_PurchasePlanMstr(ReleaseNo, BatchNo, EffDate, [Status], CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		values(@ReleaseNo, @BatchNo, @DateNow, 'Create', @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1)

		--获取发运计划头Id
		set @PurchasePlanId = @@Identity
		
		--新增发运计划期初库存
		insert into MRP_PurchasePlanInitLocationDet(PurchasePlanId, Item, InitStock, SafeStock, InTransitQty, InspectQty, CreateDate, CreateUser)
		select @PurchasePlanId, Item, Qty, SafeStock, InTransitQty, InspectQty, @DateTimeNow, @RunUser from #tempLocatoinDet

		--新增发运计划明细
		insert into MRP_PurchasePlanDet(PurchasePlanId, UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgPurchaseQty, PurchaseQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @PurchasePlanId, UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, 
		ReqQty, PurchaseQty, PurchaseQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, 
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempPurchasePlanDet

		--新增发运计划明细追溯表
		insert into MRP_PurchasePlanDetTrace(PurchasePlanId, UUID, ShiftPlanDetId, ShiftPlanMstrId, RefPlanNo, ProdLine, ProdItem, RateQty, ScrapPct, 
		BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, CreateDate, CreateUser)
		select @PurchasePlanId, UUID, DetId, MstrId, RefPlanNo, ProdLine, ProdItem, RateQty, ScrapPct,
		BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, @DateTimeNow, @RunUser from #tempMaterialPlanDetTrace
		-----------------------------↑生成物料需求计划-----------------------------

		insert into MRP_RunPurchasePlanLog(BatchNo, Lvl, Item, Uom, Qty, PlanDate, Bom, Msg, CreateDate, CreateUser)
		select @BatchNo, Lvl, Item, Uom, Qty, PlanDate, Bom, Msg, @DateTimeNow, @RunUser from #tempMsg

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
       
		set @Msg = N'运行物料需求计划异常' + Error_Message()
		insert into MRP_RunPurchasePlanLog(BatchNo, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, 'Error', @Msg, @DateTimeNow, @RunUser)
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


