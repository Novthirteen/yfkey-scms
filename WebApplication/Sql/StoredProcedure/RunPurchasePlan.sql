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
	declare @ActiveQty decimal(18, 8)
	declare @LastActiveQty decimal(18, 8)
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
			Flow varchar(50),
			Item varchar(50),
			Uom varchar(50),
			Qty decimal(18, 8),
			PlanDate datetime,
			Bom varchar(50),
			Msg varchar(500)
		)

		create table #tempEffShiftPlan
		(
			RowId int identity(1, 1) Primary Key,
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

		create table #tempCurrentMaterialPlanDet
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
			ReqTime datetime
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
			ReqTime datetime
		)

		create table #tempMergeMaterialPlanDet
		(
			UUID varchar(50) primary key,
			Item varchar(50),
			ItemDesc varchar(100),
			BaseReqQty decimal(18, 8),
			BasePurchaseQty decimal(18, 8),
			BaseUom varchar(5),
			ReqTime datetime
		)

		create index IX_WindowTime on #tempMergeMaterialPlanDet(ReqTime asc)

		create table #tempPurchasePlanDet
		(
			RowId int identity(1, 1) primary key,
			UUID varchar(50), 
			PurchaseFlow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			BaseReqQty decimal(18, 8),
			BasePurchaseQty decimal(18, 8),
			ReqQty decimal(18, 8),
			PurchaseQty decimal(18, 8),
			OrderQty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			UC decimal(18, 8),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempMaterialPlanDetTrace
		(
			RowId int identity(1, 1) primary key,
			UUID varchar(50), 
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
			MaxStock decimal(18, 8),
			Qty decimal(18, 8),
			InTransitQty decimal(18, 8),
			InSpectQty decimal(18, 8),
			ActiveQty decimal(18, 8),
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

		-----------------------------↓获取班产计划-----------------------------
		--选取开始日期大于等于当天的所有客户日程
		insert into #tempEffShiftPlan(DetId, MstrId, RefPlanNo, ProdLine, Item, ItemDesc, RefItemCode, Qty,
		Uom, BaseUom, PlanDate)
		select det.Id, mstr.Id as MstrId, mstr.RefPlanNo, mstr.ProdLine, det.Item, det.ItemDesc, det.RefItemCode, det.Qty,
		det.Uom, i.Uom as BaseUom, det.PlanDate
		from MRP_ShiftPlanDet as det inner join MRP_ShiftPlanMstr as mstr on det.PlanId = mstr.Id
		inner join Item as i on det.Item = i.Code
		where mstr.[Status] = 'Submit' and det.PlanDate >= @DateNow

		if not exists(select top 1 1 from #tempEffShiftPlan)
		begin
			RAISERROR(N'没有找到有效的班产计划。', 16, 1) 
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
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', ProdLine, Item, Qty, Uom, PlanDate, 
		N'生产线[' + ProdLine + N']成品[ ' + Item + N']没有维护单位[ ' + Uom + N' ]和基本单位[' + BaseUom + N']的换算率' 
		from #tempEffShiftPlan where UnitQty is null
		delete from #tempEffShiftPlan where UnitQty is null
		-----------------------------↑获取班产计划-----------------------------
		


		-----------------------------↓展开Bom-----------------------------
		--查找Bom，先从生产线上找，在从物料上找，最后取物料代码
		update t set Bom = ISNULL(d.Bom, ISNULL(i.Bom, t.Item))
		from #tempEffShiftPlan as t 
		left join FlowDet as d on t.ProdLine = d.Flow and t.Item = d.Item
		inner join Item as i on t.Item = i.Code

		--记录日志生产线没有维护成品
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Msg) 
		select 'Warning', t.ProdLine, t.Item, t.Qty, t.Uom, t.PlanDate, 
		N'生产线[' + t.ProdLine + N']没有维护成品[ ' + t.Item + N']' 
		from #tempEffShiftPlan as t
		left join FlowDet as d on t.ProdLine = d.Flow and t.Item = d.Item
		where d.Id is null

		--记录日志没有Bom主数据
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Bom, Msg) 
		select 'Error', t.ProdLine, t.Item, t.Qty, t.Uom, t.PlanDate, t.Bom, 
		N'生产线[' + t.ProdLine + N']成品[ ' + t.Item + N']的Bom代码[ ' + t.Bom + N']不存在' 
		from #tempEffShiftPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where t.ProdLine = @ProdLine and b.Code is null
		--删除班产计划
		delete t from #tempEffShiftPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where b.Code is null

		--记录日志没有Bom明细
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Bom, Msg) 
		select 'Error', t.ProdLine, t.Item, t.Qty, t.Uom, t.PlanDate, t.Bom, 
		N'生产线[' + t.ProdLine + N']成品[ ' + t.Item + N']的Bom代码[ ' + t.Bom + N']没有Bom明细' 
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

				truncate table #tempCurrentMaterialPlanDet
				insert into #tempCurrentMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct,
				Uom, BaseUom, ReqTime)
				select NEWID(), t.Item, i.Desc1, @BomQty * (t.RateQty + t.ScrapPct / 100), t.RateQty, t.ScrapPct,
				t.Uom, i.Uom, @EffDate
				from #tempBomDetail as t inner join Item as i on t.Item = i.Code

				--计算单位换算（Bom单位转为基本单位）
				update #tempCurrentMaterialPlanDet set UnitQty = 1 where Uom = BaseUom
				update det set UnitQty = c.BaseQty / c.AltQty
				from #tempCurrentMaterialPlanDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.AltUom and det.BaseUom = c.BaseUom
				where det.UnitQty is null
				update det set UnitQty =  c.AltQty / c.BaseQty
				from #tempCurrentMaterialPlanDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom and det.BaseUom = c.AltUom
				where det.UnitQty is null
				update det set UnitQty = c.BaseQty / c.AltQty
				from #tempCurrentMaterialPlanDet as det inner join UomConv as c on det.Uom = c.AltUom and det.BaseUom = c.BaseUom 
				where det.UnitQty is null and c.Item is null
				update det set UnitQty =  c.AltQty / c.BaseQty
				from #tempCurrentMaterialPlanDet as det inner join UomConv as c on det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
				where det.UnitQty is null and c.Item is null

				--删除没有维护单位换算的物料
				insert into #tempMsg(Lvl, Item, Qty, Uom, PlanDate, Msg) 
				select 'Error', Item, ReqQty, Uom, ReqTime, 
				N'物料[ ' + Item + N']没有维护单位[ ' + Uom + N' ]和基本单位[' + BaseUom + N']的换算率' 
				from #tempCurrentMaterialPlanDet where UnitQty is null
				delete from #tempCurrentMaterialPlanDet where UnitQty is null

				--记录物料计划临时表
				insert into #tempMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime)
				select UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime from #tempCurrentMaterialPlanDet
				--记录追溯关系
				insert into #tempMaterialPlanDetTrace(UUID, DetId, MstrId, RefPlanNo, ProdLine, ProdItem, ProdQty, RateQty, ScrapPct, BomUnitQty, BomUom, PlanDate)
				select UUID, @DetId, @MstrId, @RefPlanNo, @ProdLine, @ProdItem, @BomQty, RateQty, ScrapPct, UnitQty, Uom, @EffDate 
				from #tempCurrentMaterialPlanDet 
			end
			set @RowId = @RowId + 1
		end

		--删除不参与MRP运算的物料
		delete tr from #tempMaterialPlanDetTrace as tr 
		inner join #tempMaterialPlanDet as pl on tr.UUID = pl.UUID
		inner join Item as i on pl.Item = i.Code
		where i.IsMRP <> 1
		delete pl from #tempMaterialPlanDet as pl inner join Item as i on pl.Item = i.Code where i.IsMRP <> 1
		-----------------------------↑展开Bom-----------------------------



		-----------------------------↓获取可用库存-----------------------------
		insert into #tempLocatoinDet(Item, SafeStock, MaxStock, Qty, InTransitQty, InSpectQty, ActiveQty)
		select Item, ISNULL(i.SafeStock, 0), ISNULL(i.MaxStock, 0), Qty, PurchaseInTransitQty, InSpectQty, Qty + InspectQty - ISNULL(i.SafeStock, 0)
		from 
		(
		select loc.Item, SUM(loc.Qty) as Qty, SUM(loc.InTransitQty) as InTransitQty, SUM(loc.PurchaseInTransitQty) as PurchaseInTransitQty, SUM(loc.InspectQty) as InspectQty
		from MRP_LocationDetSnapShot as loc
		inner join (select distinct Item from #tempMaterialPlanDet) as p on loc.Item = p.Item
		group by loc.Item
		) as a inner join Item as i on a.Item = i.Code 
		-----------------------------↑获取可用库存-----------------------------



		-----------------------------↓合并采购需求-----------------------------
		--毛需求插入合并表
		insert into  #tempMergeMaterialPlanDet(UUID, Item, ItemDesc, BaseReqQty, BasePurchaseQty, BaseUom, ReqTime)
		select UUID, Item, ItemDesc, ReqQty * UnitQty, ReqQty * UnitQty, BaseUom, ReqTime from #tempMaterialPlanDet
		--合并毛需求至一行（最小UUID)
		update p set BaseReqQty = t.BaseReqQty, BasePurchaseQty = t.BaseReqQty
		from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(BaseReqQty) as BaseReqQty from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--合并需求中间表，把需求中间表UUID全部更新为最小UUID
		update dt set UUID = t.MinUUID
		from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, ReqTime from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.Item = t.Item and p.ReqTime = t.ReqTime
		inner join #tempMaterialPlanDetTrace as dt on p.UUID = dt.UUID
		--删除重复毛需求
		delete p from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, ReqTime from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.Item = t.Item and p.ReqTime = t.ReqTime
		where p.UUID <> MinUUID
		-----------------------------↑合并采购需求-----------------------------



		-----------------------------↓低于安全库存的转为当天的物料需求计划-----------------------------
		update p set BasePurchaseQty = BasePurchaseQty - d.ActiveQty
		from #tempMergeMaterialPlanDet as p inner join #tempLocatoinDet as d on p.Item = d.Item and p.ReqTime = @DateNow
		where d.ActiveQty < 0
		insert into #tempMergeMaterialPlanDet(UUID, Item, ItemDesc, BaseReqQty, BasePurchaseQty, BaseUom, ReqTime)
		select NEWID(), d.Item, i.Desc1, 0, -d.ActiveQty, i.Uom, @DateNow 
		from #tempLocatoinDet as d left join #tempMergeMaterialPlanDet as p on p.Item = d.Item and p.ReqTime = @DateNow
		inner join Item as i on d.Item = i.Code
		where d.ActiveQty < 0 and p.Item is null
		-----------------------------↑低于安全库存的转为当天的物料需求计划-----------------------------


		
		-----------------------------↓计算净需求-----------------------------
		set @RowId = null
		set @MaxRowId = null
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempLocatoinDet
		while (@RowId <= @MaxRowId)
		begin
			set @Item = null
			set @ActiveQty = null
			set @LastActiveQty = 0

			select @ActiveQty = ActiveQty, @Item = Item from #tempLocatoinDet where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set BasePurchaseQty = CASE WHEN @ActiveQty >= BasePurchaseQty THEN 0 WHEN @ActiveQty < BasePurchaseQty and @ActiveQty > 0 THEN BasePurchaseQty - @ActiveQty ELSE BasePurchaseQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = BasePurchaseQty
				from #tempMergeMaterialPlanDet as det with(INDEX(IX_WindowTime))
				where det.Item = @Item
			end
			
			set @RowId = @RowId + 1
		end		
		-----------------------------↑计算净需求-----------------------------



		-----------------------------↓查找采购路线-----------------------------
		insert into #tempPurchasePlanDet(UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, BaseReqQty, BasePurchaseQty, Uom, BaseUom, UC, StartTime, WindowTime)
		select t.UUID, d.Flow, t.Item, t.ItemDesc, d.RefItemCode, t.BaseReqQty, t.BasePurchaseQty, d.Uom, t.BaseUom, d.UC, DATEADD(day, -ISNULL(m.LeadTime, 0), t.ReqTime), t.ReqTime
		from #tempMergeMaterialPlanDet as t
		inner join FlowDet as d on t.Item = d.Item
		inner join FlowMstr as m on d.Flow = m.Code
		where m.[Type] = 'Procurement' and m.IsMRP = 1 and m.IsActive = 1

		--记录日志没有找到采购路线
		insert into #tempMsg(Lvl, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', Item, ReqQty, BaseUom, WindowTime, 
		N'物料[ ' + Item + N']没有找到采购路线' 
		from #tempPurchasePlanDet where PurchaseFlow is null
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
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', PurchaseFlow, Item, ReqQty, Uom, WindowTime, 
		N'采购路线[' + PurchaseFlow + N']物料[ ' + Item + N']没有维护单位[ ' + Uom + N']和基本单位[' + BaseUom + N']的换算率' 
		from #tempPurchasePlanDet where UnitQty is null
		delete from #tempPurchasePlanDet where UnitQty is null

		--数量转为采购单位
		update #tempPurchasePlanDet set ReqQty = BaseReqQty * UnitQty, PurchaseQty = BasePurchaseQty * UnitQty
		-----------------------------↑查找采购路线-----------------------------


		-----------------------------↓更新订单数-----------------------------
		--查找订单数
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, CASE WHEN ord.WindowTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.WindowTime, 121)) END, ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord
		inner join (select distinct PurchaseFlow, Item from #tempPurchasePlanDet) as pl on ord.Flow = pl.PurchaseFlow and ord.Item = pl.Item

		--更新订单数
		update pl set OrderQty = ISNULL(ord.OrderQty, 0)
		from #tempPurchasePlanDet as pl
		left join (select Flow, Item, EffDate, SUM(ISNULL(OrderQty, 0) - ISNULL(ShipQty, 0)) as OrderQty from #tempOpenOrder group by Flow, Item, EffDate) as ord 
		on pl.PurchaseFlow = ord.Flow and pl.Item = ord.Item and pl.WindowTime = ord.EffDate
		insert into #tempPurchasePlanDet(UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, BaseReqQty, BasePurchaseQty, ReqQty, PurchaseQty, OrderQty, Uom, BaseUom, UC, StartTime, WindowTime)
		select NEWID(), d.Flow, ord.Item, i.Desc1, d.RefItemCode, 0, 0, 0, 0, ord.OrderQty, d.Uom, i.Uom, d.UC, DATEADD(day, -ISNULL(m.LeadTime, 0), @DateNow), @DateNow
		from (select Flow, Item, EffDate, SUM(ISNULL(OrderQty, 0) - ISNULL(ShipQty, 0)) as OrderQty from #tempOpenOrder group by Flow, Item, EffDate) as ord
		inner join Item as i on ord.Item = i.Code
		inner join FlowDet as d on ord.Item = d.Item
		inner join FlowMstr as m on d.Flow = m.Code
		left join #tempPurchasePlanDet as pl on pl.PurchaseFlow = ord.Flow and pl.Item = ord.Item and pl.WindowTime = ord.EffDate
		where m.[Type] = 'Procurement' and m.IsMRP = 1 and m.IsActive = 1 and pl.Item is null

		--更新订单表关联关系
		update ord set UUID = pl.UUID
		from #tempOpenOrder as ord inner join #tempPurchasePlanDet as pl on pl.PurchaseFlow = ord.Flow and pl.Item = ord.Item and pl.WindowTime = ord.EffDate
		-----------------------------↑更新订单数-----------------------------

	end try
	begin catch
		set @Msg = N'运行物料需求计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
		return
	end catch 

	begin try	
		if @trancount = 0
		begin
			begin tran
		end

		-----------------------------↓生成物料需求计划-----------------------------
		--删除未释放的物料需求计划
		delete from MRP_PurchasePlanOpenOrder where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanDetTrace where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanInitLocationDet where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanDet where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanMstr where Status = 'Create'

		--获取ReleaseNo
		select @ReleaseNo = ISNULL(MAX(ReleaseNo), 0) + 1 from MRP_PurchasePlanMstr

		--新增物料需求计划头
		insert into MRP_PurchasePlanMstr(ReleaseNo, BatchNo, EffDate, [Status], CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		values(@ReleaseNo, @BatchNo, @DateNow, 'Create', @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1)

		--获取物料需求计划头Id
		set @PurchasePlanId = @@Identity
		
		--新增物料需求计划期初库存
		insert into MRP_PurchasePlanInitLocationDet(PurchasePlanId, Item, InitStock, SafeStock, MaxStock, InTransitQty, InspectQty, CreateDate, CreateUser)
		select @PurchasePlanId, Item, Qty, SafeStock, MaxStock, InTransitQty, InspectQty, @DateTimeNow, @RunUser from #tempLocatoinDet

		--发货数按包装圆整
		update #tempPurchasePlanDet set PurchaseQty = ceiling(PurchaseQty / UC) * UC where PurchaseQty > 0 and UC > 0

		--新增物料需求计划明细
		insert into MRP_PurchasePlanDet(PurchasePlanId, UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgPurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @PurchasePlanId, UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, 
		ReqQty, PurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, 
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempPurchasePlanDet

		--新增物料需求计划明细追溯表
		insert into MRP_PurchasePlanDetTrace(PurchasePlanId, UUID, ShiftPlanDetId, ShiftPlanMstrId, RefPlanNo, ProdLine, ProdItem, ProdQty, RateQty, ScrapPct, 
		BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, CreateDate, CreateUser)
		select @PurchasePlanId, UUID, DetId, MstrId, RefPlanNo, ProdLine, ProdItem, ProdQty, RateQty, ScrapPct,
		BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, @DateTimeNow, @RunUser from #tempMaterialPlanDetTrace

		--新增Open Order追溯表
		insert into MRP_PurchasePlanOpenOrder(PurchasePlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser)
		select @PurchasePlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, @DateTimeNow, @RunUser from #tempOpenOrder
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
       
		set @Msg = N'运行物料需求计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


