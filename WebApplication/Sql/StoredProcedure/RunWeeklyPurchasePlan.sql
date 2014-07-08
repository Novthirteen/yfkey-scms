SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunWeeklyPurchasePlan') 
     DROP PROCEDURE RunWeeklyPurchasePlan
GO

CREATE PROCEDURE [dbo].RunWeeklyPurchasePlan
(
	@BatchNo int,
	@RunUser varchar(50),
	@DateTimeNow datetime
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateNow datetime
	declare @Msg nvarchar(MAX)
	declare @MaxPlanDate datetime
	declare @RowId int
	declare @MaxRowId int
	declare @ProdItem varchar(50)
	declare @Bom varchar(50)
	declare @EffDate datetime
	declare @BomQty decimal(18, 8)
	declare @ReleaseNo varchar(50)
	declare @PurchasePlanId int

	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''

	begin try
		create table #tempMsg
		(
			Lvl varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Phase varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Msg varchar(500) COLLATE  Chinese_PRC_CI_AS
		)

		create table #tempProductionPlan
		(
			RowId int identity(1, 1) Primary Key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			RefItemCode varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			PlanDate datetime
		)

		create table #tempBomDetail
		(
			RowId int identity(1, 1) primary key,
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StruType varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			RateQty decimal(18, 8),
			ScrapPct decimal(18, 8),
			BackFlushMethod varchar(50) COLLATE  Chinese_PRC_CI_AS,
		)

		create table #tempCurrentMaterialPlanDet
		(
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			ReqQty decimal(18, 8),
			RateQty decimal(18, 8),
			ScrapPct decimal(18, 8),
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			ReqTime datetime
		)

		create table #tempMaterialPlanDet
		(
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			ReqQty decimal(18, 8),
			RateQty decimal(18, 8),
			ScrapPct decimal(18, 8),
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			ReqTime datetime
		)

		create table #tempMergeMaterialPlanDet
		(
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			BaseReqQty decimal(18, 8),
			BasePurchaseQty decimal(18, 8),
			BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			ReqTime datetime
		)

		create table #tempPurchasePlanDet
		(
			RowId int identity(1, 1) primary key,
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			PurchaseFlow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			RefItemCode varchar(50) COLLATE  Chinese_PRC_CI_AS,
			BaseReqQty decimal(18, 8),
			BasePurchaseQty decimal(18, 8),
			ReqQty decimal(18, 8),
			PurchaseQty decimal(18, 8),
			OrderQty decimal(18, 8),
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			UC decimal(18, 8),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempOpenOrder
		(
			RowId int identity(1, 1)  primary key,
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			OrderNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StartTime datetime,
			WindowTime datetime,
			EffDate datetime,
			OrderQty decimal(18, 8),
			ShipQty decimal(18, 8),
			RecQty decimal(18, 8),
		)

		-----------------------------↓获取生产计划-----------------------------
		--选取开始日期大于等于当天的所有客户日程
		insert into #tempProductionPlan(Item, ItemDesc, RefItemCode, Qty,
		Uom, BaseUom, PlanDate)
		select det.Item, det.ItemDesc, det.RefItemCode, det.Qty,
		det.Uom, i.Uom as BaseUom, det.StartTime
		from MRP_WeeklyProductionPlan as det
		inner join Item as i on det.Item = i.Code

		if not exists(select top 1 1 from #tempProductionPlan)
		begin
			RAISERROR(N'没有找到有效的生产计划。', 16, 1) 
			return			
		end

		--需求全部转换为周1开始
		set datefirst 1 --设置周1为1周开始时间
		update #tempProductionPlan set PlanDate = DATEADD(DAY, -DATEPART(WEEKDAY, PlanDate), PlanDate)

		--删除排产日期和班产计划重复的
		select @MaxPlanDate = MAX(PlanDate) from MRP_ShiftPlanDet 
		where PlanId in (select Max(mstr.Id) from MRP_ShiftPlanDet as det
		inner join MRP_ShiftPlanMstr as mstr on det.PlanId = mstr.Id
		where mstr.[Status] = 'Submit')
		delete from #tempProductionPlan where PlanDate <= @MaxPlanDate

		--计算单位换算
		update #tempProductionPlan set UnitQty = 1 where Uom = BaseUom
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempProductionPlan as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempProductionPlan as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempProductionPlan as det inner join UomConv as c on det.Uom = c.AltUom and det.BaseUom = c.BaseUom 
		where det.UnitQty is null and c.Item is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempProductionPlan as det inner join UomConv as c on det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
		where det.UnitQty is null and c.Item is null

		--删除没有维护单位换算的物料
		insert into #tempMsg(Lvl, Phase, Item, Msg) 
		select 'Error', 'Purchase', Item, N'成品[ ' + Item + N']没有维护单位[ ' + Uom + N' ]和基本单位[' + BaseUom + N']的换算率' 
		from #tempProductionPlan where UnitQty is null
		delete from #tempProductionPlan where UnitQty is null
		-----------------------------↑获取生产计划-----------------------------

	
		-----------------------------↓展开Bom-----------------------------
		--查找Bom，先从生产线上找，在从物料上找，最后取物料代码
		update t set Bom = ISNULL(i.Bom, t.Item)
		from #tempProductionPlan as t 
		inner join Item as i on t.Item = i.Code

		--记录日志没有Bom主数据
		insert into #tempMsg(Lvl, Phase, Item, Msg) 
		select 'Error', 'Production', t.Item,
		N'成品[ ' + t.Item + N']的Bom代码[ ' + t.Bom + N']不存在' 
		from #tempProductionPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where b.Code is null
		--删除班产计划
		delete t from #tempProductionPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where b.Code is null

		--记录日志没有Bom明细
		insert into #tempMsg(Lvl, Phase, Item, Msg) 
		select 'Error', 'Production', t.Item,
		N'成品[ ' + t.Item + N']的Bom代码[ ' + t.Bom + N']没有Bom明细' 
		from #tempProductionPlan as t
		left join BomDet as b on t.Bom = b.Bom
		where b.Bom is null
		--删除班产计划
		delete t from #tempProductionPlan as t
		left join BomDet as b on t.Bom = b.Bom
		where b.Bom is null

		--循环展开Bom
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempProductionPlan
		while @RowId <= @MaxRowId
		begin
			if exists(select top 1 1 from #tempProductionPlan where RowId = @RowId)
			begin
				select @ProdItem = Item, @Bom = Bom, @EffDate = PlanDate, @BomQty = Qty 
				from #tempProductionPlan where RowId = @RowId
				
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
				insert into #tempMsg(Lvl, Phase, Item, Msg) 
				select 'Error', 'Production', Item,
				N'物料[ ' + Item + N']没有维护单位[ ' + Uom + N' ]和基本单位[' + BaseUom + N']的换算率' 
				from #tempCurrentMaterialPlanDet where UnitQty is null
				delete from #tempCurrentMaterialPlanDet where UnitQty is null

				--记录物料计划临时表
				insert into #tempMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime)
				select UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime from #tempCurrentMaterialPlanDet
			end
			set @RowId = @RowId + 1
		end

		--删除不参与MRP运算的物料
		delete pl from #tempMaterialPlanDet as pl inner join Item as i on pl.Item = i.Code where i.IsMRP <> 1
		-----------------------------↑展开Bom-----------------------------



		-----------------------------↓合并采购需求-----------------------------
		--毛需求插入合并表
		insert into  #tempMergeMaterialPlanDet(UUID, Item, ItemDesc, BaseReqQty, BasePurchaseQty, BaseUom, ReqTime)
		select UUID, Item, ItemDesc, ReqQty * UnitQty, ReqQty * UnitQty, BaseUom, ReqTime from #tempMaterialPlanDet

		--需求全部转换为周1开始
		update #tempMergeMaterialPlanDet set ReqTime = DATEADD(DAY, -DATEPART(WEEKDAY, ReqTime), ReqTime)

		--合并毛需求至一行（最小UUID)
		update p set BaseReqQty = t.BaseReqQty, BasePurchaseQty = t.BaseReqQty
		from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(BaseReqQty) as BaseReqQty from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--删除重复毛需求
		delete p from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, ReqTime from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.Item = t.Item and p.ReqTime = t.ReqTime
		where p.UUID <> MinUUID
		-----------------------------↑合并采购需求-----------------------------



		-----------------------------↓查找采购路线-----------------------------
		insert into #tempPurchasePlanDet(UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, BaseReqQty, BasePurchaseQty, Uom, BaseUom, UC, StartTime, WindowTime)
		select t.UUID, d.Flow, t.Item, t.ItemDesc, d.RefItemCode, t.BaseReqQty, t.BasePurchaseQty, d.Uom, t.BaseUom, d.UC, DATEADD(day, -ISNULL(m.LeadTime, 0), t.ReqTime), t.ReqTime
		from #tempMergeMaterialPlanDet as t
		inner join FlowDet as d on t.Item = d.Item
		inner join FlowMstr as m on d.Flow = m.Code
		where m.[Type] = 'Procurement' and m.IsMRP = 1 and m.IsActive = 1

		--记录日志没有找到采购路线
		insert into #tempMsg(Lvl, Phase, Item, Msg) 
		select 'Error', 'Production', Item,
		N'物料[ ' + Item + N']没有找到采购路线' 
		from #tempPurchasePlanDet where PurchaseFlow is null
		--删除物料需求
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
	
		--删除没有维护单位换算的物料
		insert into #tempMsg(Lvl, Phase, Flow, Item, Msg) 
		select 'Error', 'Production', PurchaseFlow, Item,
		N'采购路线[' + PurchaseFlow + N']物料[ ' + Item + N']没有维护单位[ ' + Uom + N']和基本单位[' + BaseUom + N']的换算率' 
		from #tempPurchasePlanDet where UnitQty is null
		delete from #tempPurchasePlanDet where UnitQty is null

		--数量转为采购单位
		update #tempPurchasePlanDet set ReqQty = BaseReqQty * UnitQty, PurchaseQty = BasePurchaseQty * UnitQty
		-----------------------------↑查找采购路线-----------------------------


		-----------------------------↓更新订单数-----------------------------
		--查找订单数
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, DATEADD(DAY, -DATEPART(WEEKDAY, ord.WindowTime), ord.WindowTime), ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord
		inner join (select distinct PurchaseFlow, Item from #tempPurchasePlanDet) as pl on ord.Flow = pl.PurchaseFlow and ord.Item = pl.Item
		where ord.WindowTime > @MaxPlanDate

		--更新订单数
		update pl set OrderQty = ISNULL(ord.OrderQty, 0)
		from #tempPurchasePlanDet as pl
		left join (select Flow, Item, EffDate, SUM(ISNULL(OrderQty, 0)) as OrderQty from #tempOpenOrder group by Flow, Item, EffDate) as ord 
		on pl.PurchaseFlow = ord.Flow and pl.Item = ord.Item and pl.WindowTime = ord.EffDate
		insert into #tempPurchasePlanDet(UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, BaseReqQty, BasePurchaseQty, ReqQty, PurchaseQty, OrderQty, Uom, BaseUom, UC, StartTime, WindowTime)
		select NEWID(), d.Flow, ord.Item, i.Desc1, d.RefItemCode, 0, 0, 0, 0, ord.OrderQty, d.Uom, i.Uom, d.UC, DATEADD(day, -ISNULL(m.LeadTime, 0), @DateNow), @DateNow
		from (select Flow, Item, EffDate, SUM(ISNULL(OrderQty, 0)) as OrderQty from #tempOpenOrder group by Flow, Item, EffDate) as ord
		inner join Item as i on ord.Item = i.Code
		inner join FlowDet as d on ord.Item = d.Item
		inner join FlowMstr as m on d.Flow = m.Code
		left join #tempPurchasePlanDet as pl on pl.PurchaseFlow = ord.Flow and pl.Item = ord.Item and pl.WindowTime = ord.EffDate
		where m.[Type] = 'Procurement' and m.IsMRP = 1 and m.IsActive = 1 and pl.Item is null

		--更新订单表关联关系
		update ord set UUID = pl.UUID
		from #tempOpenOrder as ord inner join #tempPurchasePlanDet as pl on pl.PurchaseFlow = ord.Flow and pl.Item = ord.Item and pl.WindowTime = ord.EffDate
		-----------------------------↑更新订单数-----------------------------



		-----------------------------↓生成物料需求计划-----------------------------
		--删除未释放的物料需求计划
		delete from MRP_WeeklyPurchasePlanOpenOrder where PurchasePlanId in(select Id from MRP_WeeklyPurchasePlanMstr where Status = 'Create')
		delete from MRP_WeeklyPurchasePlanDet where PurchasePlanId in(select Id from MRP_WeeklyPurchasePlanMstr where Status = 'Create')
		delete from MRP_WeeklyPurchasePlanMstr where Status = 'Create'

		--获取ReleaseNo
		select @ReleaseNo = ISNULL(MAX(ReleaseNo), 0) + 1 from MRP_WeeklyPurchasePlanMstr

		--新增物料需求计划头
		insert into MRP_WeeklyPurchasePlanMstr(ReleaseNo, BatchNo, EffDate, [Status], CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		values(@ReleaseNo, @BatchNo, @DateNow, 'Create', @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1)

		--获取物料需求计划头Id
		set @PurchasePlanId = @@Identity
		
		--发货数按包装圆整
		update #tempPurchasePlanDet set PurchaseQty = ceiling(PurchaseQty / UC) * UC where PurchaseQty > 0 and UC > 0

		--新增物料需求计划明细
		insert into MRP_WeeklyPurchasePlanDet(PurchasePlanId, UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgPurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @PurchasePlanId, UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, 
		ReqQty, PurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, 
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempPurchasePlanDet

		--新增Open Order追溯表
		insert into MRP_WeeklyPurchasePlanOpenOrder(PurchasePlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser)
		select @PurchasePlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, @DateTimeNow, @RunUser from #tempOpenOrder
		-----------------------------↑生成物料需求计划-----------------------------

		--记录日志
		insert into MRP_RunWeeklyMRPLog(BatchNo, Lvl, Phase, Flow, Item, Msg, CreateDate, CreateUser)
		select @BatchNo, Lvl, Phase, Flow, Item, Msg, @DateTimeNow, @RunUser from #tempMsg


		drop table #tempMsg
		drop table #tempProductionPlan
		drop table #tempBomDetail
		drop table #tempCurrentMaterialPlanDet
		drop table #tempMaterialPlanDet
		drop table #tempMergeMaterialPlanDet
		drop table #tempPurchasePlanDet
		drop table #tempOpenOrder
	end try
	begin catch
		set @Msg = N'运行采购计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
		return
	end catch 
END 


