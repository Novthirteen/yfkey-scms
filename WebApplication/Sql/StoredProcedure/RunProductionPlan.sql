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
	declare @ShipPlanReleaseNo int
	declare @RowId int
	declare @MaxRowId int
	declare @GroupId int
	declare @MaxGroupId int
	declare @GroupSeq int
	declare @MaxGroupSeq int
	declare @Item varchar(50)
	declare @Bom varchar(50)
	declare @ActiveQty decimal(18, 8)
	declare @LastActiveQty decimal(18, 8)
	declare @Effdate datetime
	declare @BomQty decimal(18, 8)
	declare @ProductionPlanId int
	declare @StartTime datetime
	declare @LastOverflowCount int
	declare @CurrentOverflowCount int

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''
	set @trancount = @@trancount

	exec GetNextSequence 'RunProductionPlan', @BatchNo output
	begin try
		create table #tempMsg
		(
			Lvl varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			EffDate datetime,
			Msg varchar(500) COLLATE  Chinese_PRC_CI_AS
		)

		create table #tempCurrentLevlProductPlan
		(
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS primary key,
			GroupId int,
			GroupSeq int,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			RefItemCode varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_WindowTime on #tempCurrentLevlProductPlan(WindowTime asc)

		create table #tempProductPlanDetTrace
		(
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ReqDate datetime,
			ReqQty decimal(18, 8),
			RateQty decimal(18, 8), 
			ScrapPct decimal(18, 8),
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UnitQty decimal(18, 8)
		)

		create table #tempTempNextLevlProductPlan
		(
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			RefItemCode varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			RateQty decimal(18, 8), 
			ScrapPct decimal(18, 8),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempNextLevlProductPlan
		(
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			RefItemCode varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			RateQty decimal(18, 8), 
			ScrapPct decimal(18, 8),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempProductPlanDet
		(
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			RefItemCode varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UC decimal(18, 8),
			MinLotSize decimal(18, 8),
			ReqQty decimal(18, 8),
			OrgQty decimal(18, 8),
			Qty decimal(18, 8),
			OverflowQty decimal(18, 8),
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			OrderQty decimal(18, 8),
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_StartTime on #tempProductPlanDet(StartTime asc)

		create table #tempLocatoinDet
		(
			RowId int identity(1, 1) primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			InspectQty decimal(18, 8), 
			InTransitQty decimal(18, 8), 
			SafeStock decimal(18, 8),
			MaxStock decimal(18, 8),
			RemainQty decimal(18, 8)
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
			RecQty decimal(18, 8),
		)

		select @ShipPlanReleaseNo = MAX(ReleaseNo) from MRP_ShipPlanMstr where [Status] = 'Submit'

		if (@ShipPlanReleaseNo is null)
		begin
			RAISERROR(N'没有找到释放的发运计划。', 16, 1)
		end

		if not exists(select top 1 1 from Location where IsFG = 1)
		begin
			RAISERROR(N'没有找到成品库位。', 16, 1) 
		end

		-----------------------------↓获取顶层毛需求-----------------------------
		insert into #tempCurrentLevlProductPlan(UUID, GroupId, GroupSeq, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
		select NEWID(), DENSE_RANK() over(Order by det.Item), DENSE_RANK() over(Partition by Item order by det.StartTime),
		det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, SUM((ISNULL(det.ShipQty, 0) + ISNULL(det.OrderQty, 0)) * (1 + ISNULL(i.ScrapPct, 0) / 100) * ISNULL(det.UnitQty, 0)) as Qty, ISNULL(i.Bom, det.Item),
		DATEADD(day, -ISNULL(i.LeadTime, 0), det.StartTime) as StartTime, det.StartTime as WindowTime
		from MRP_ShipPlanDet as det 
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Item as i on det.Item = i.Code
		where mstr.ReleaseNo = @ShipPlanReleaseNo
		group by det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, det.StartTime, i.Bom, i.LeadTime

		--删除开始日期小于今天的需求
		delete from #tempCurrentLevlProductPlan where StartTime < @DateNow

		--记录物料追溯表
		insert into #tempProductPlanDetTrace(UUID, Flow, Item, ReqDate, ReqQty, ScrapPct, Uom, UnitQty)
		select t.UUID, det.Flow, det.Item, det.StartTime, ISNULL(det.ShipQty, 0), i.ScrapPct, det.Uom, det.UnitQty
		from MRP_ShipPlanDet as det
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Item as i on det.Item = i.Code
		inner join #tempCurrentLevlProductPlan as t on t.Item = det.Item and t.WindowTime = det.StartTime
		where mstr.ReleaseNo = @ShipPlanReleaseNo and ISNULL(det.ShipQty, 0) <> 0
		insert into #tempProductPlanDetTrace(UUID, Flow, Item, ReqDate, ReqQty, ScrapPct, Uom, UnitQty)
		select t.UUID, det.Flow, det.Item, det.StartTime, ISNULL(det.OrderQty, 0), i.ScrapPct, det.Uom, det.UnitQty
		from MRP_ShipPlanDet as det
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Item as i on det.Item = i.Code
		inner join #tempCurrentLevlProductPlan as t on t.Item = det.Item and t.WindowTime = det.StartTime
		where mstr.ReleaseNo = @ShipPlanReleaseNo and ISNULL(det.OrderQty, 0) <> 0
		-----------------------------↑获取顶层毛需求-----------------------------



		-----------------------------↓循环分解Bom，添加下层生产计划-----------------------------
		declare @ExpandLevel int
		set @ExpandLevel = 1
		while exists(select top 1 1 from #tempCurrentLevlProductPlan)
		begin
			-----------------------------↓计算可用库存-----------------------------
			insert into #tempLocatoinDet(Item, Qty, InspectQty, InTransitQty, SafeStock, MaxStock, RemainQty)
			select p.Item, ISNULL(a.Qty, 0), ISNULL(a.InspectQty, 0), ISNULL(a.InTransitQty, 0), ISNULL(i.SafeStock, 0), ISNULL(i.MaxStock, 0),
			ISNULL(a.Qty, 0) + ISNULL(a.InspectQty, 0) + ISNULL(a.InTransitQty, 0) - ISNULL(i.SafeStock, 0)
			from (select distinct Item from #tempCurrentLevlProductPlan) as p
			inner join Item as i on p.Item = i.Code 
			left join (select loc.Item, SUM(loc.Qty) as Qty, SUM(loc.InspectQty) as InspectQty, SUM(loc.InTransitQty) as InTransitQty
						from MRP_LocationDetSnapShot as loc
						inner join Location as l on loc.Location = l.Code
						inner join (select distinct Item from #tempCurrentLevlProductPlan) as p on loc.Item = p.Item
						where l.IsFG = 1  --取成品库位库存
						group by loc.Item
						) as a on p.Item = a.Item
			left join #tempLocatoinDet as t on p.Item = t.Item
			where t.Item is null  --过滤已经添加到列表中的库存
			-----------------------------↑计算可用库存-----------------------------



			-----------------------------↓计算净需求-----------------------------
			set @RowId = null
			set @MaxRowId = null
			select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempLocatoinDet
			while (@RowId <= @MaxRowId)
			begin
				set @Item = null
				set @ActiveQty = null
				set @LastActiveQty = null

				select @ActiveQty = RemainQty, @Item = Item from #tempLocatoinDet where RowId = @RowId
				if (@ActiveQty > 0)
				begin
					update det set Qty = CASE WHEN @ActiveQty >= Qty THEN 0 WHEN @ActiveQty < Qty and @ActiveQty > 0 THEN Qty - @ActiveQty ELSE Qty END,
					@ActiveQty = CASE WHEN @ActiveQty >= @LastActiveQty THEN @ActiveQty - @LastActiveQty ELSE 0 END, 
					@LastActiveQty = Qty
					from #tempCurrentLevlProductPlan as det with(INDEX(IX_WindowTime))
					where det.Item = @Item
				end

				update #tempLocatoinDet set RemainQty = @ActiveQty where RowId = @RowId
			
				set @RowId = @RowId + 1
			end
			-----------------------------↑计算净需求-----------------------------



			-----------------------------↓分解Bom，查找下层Bom是否是半成品-----------------------------
			set @GroupId = null
			set @MaxGroupId = null
			select @GroupId = MIN(GroupId), @MaxGroupId = MAX(GroupId) from #tempCurrentLevlProductPlan
			while (@GroupId <= @MaxGroupId)
			begin  --按零件分组循环
				set @Item = null
				set @Bom = null
				select top 1 @Item = Item, @Bom = Bom from #tempCurrentLevlProductPlan where GroupId = @GroupId

				if not exists(select top 1 1 from BomMstr where Code = @Bom and IsActive = 1)
				begin   --没有找到Bom主数据
					insert into #tempMsg(Lvl, Item, Bom, Msg) values('Error', @Item, @Bom, N'物料[' + @Item + N']的Bom[' + @Bom + N']主数据不存在或已停用')
					delete from #tempCurrentLevlProductPlan where Item = @Bom
					set @GroupId = @GroupId + 1
					continue
				end

				set @GroupSeq = null
				set @MaxGroupSeq = null
				select @GroupSeq = MIN(GroupSeq), @MaxGroupSeq = MAX(GroupSeq) from #tempCurrentLevlProductPlan where GroupId = @GroupId
				while @GroupSeq <= @MaxGroupSeq
				begin  --按上线日期循环
					if exists(select top 1 1 from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq and Qty > 0)
					begin
						set @EffDate = null
						set @BomQty = null
						select @EffDate = StartTime, @BomQty = Qty from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq

						truncate table #tempBomDetail
						insert into #tempBomDetail exec GetFlatBomDetail @Bom, @EffDate

						if not exists(select top 1 1 from #tempBomDetail)
						begin --展开后没有Bom明细
							insert into #tempMsg(Lvl, Item, Bom, EffDate, Msg) values('Error', @Item, @Bom, @EffDate, N'物料[' + @Item + N']Bom[' + @Bom + N']上线日期[' + convert(varchar(10), @EffDate, 121) + ']的Bom明细不存在')
							delete from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq
							set @GroupSeq = @GroupSeq + 1
							continue
						end

						--如果下层是半成品，插入临时下层生产计划
						truncate table #tempTempNextLevlProductPlan
						insert into #tempTempNextLevlProductPlan(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, 
						RateQty, ScrapPct, StartTime, WindowTime)
						select NEWID(), bom.Item, i.Desc1, null, bom.Uom, @BomQty * bom.RateQty * (1 + bom.ScrapPct / 100), ISNULL(i.Bom, bom.Item),
						bom.RateQty, bom.ScrapPct, DATEADD(day, -ISNULL(i.LeadTime, 0), @EffDate), @EffDate
						from #tempBomDetail as bom 
						inner join Item as i on bom.Item = i.Code
						inner join BomMstr as bm on (i.Bom is not null and bm.Code = i.Bom) or (i.Bom is null and bm.Code = bom.Item)
						where bm.IsActive = 1

						--删除开始日期小于今天的需求
						delete from #tempTempNextLevlProductPlan where StartTime < @DateNow

						--插入物料追溯表
						insert into #tempProductPlanDetTrace(UUID, Item, ReqDate, ReqQty, Bom, RateQty, ScrapPct, Uom)
						select pl.UUID, @Item, DATEADD(day, -ISNULL(i.LeadTime, 0), @EffDate), @BomQty, @Bom, pl.RateQty, pl.ScrapPct, pl.Uom 
						from #tempTempNextLevlProductPlan as pl
						inner join Item as i on pl.Item = i.Code

						--
						insert into #tempNextLevlProductPlan(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, RateQty, ScrapPct, StartTime, WindowTime)
						select UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, RateQty, ScrapPct, StartTime, WindowTime from #tempTempNextLevlProductPlan
					end

					set @GroupSeq = @GroupSeq + 1 
				end
			
				set @GroupId = @GroupId + 1
			end
			-----------------------------↑分解Bom，查找下层Bom是否是半成品-----------------------------


			--添加本层生产计划
			insert into #tempProductPlanDet(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
			select UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime from #tempCurrentLevlProductPlan

			--删除本层生产计划
			truncate table #tempCurrentLevlProductPlan

			--如果下层有半成品生产计划，添加到本层生产计划中进行下次循环
			if exists(select top 1 1 from #tempNextLevlProductPlan)
			begin
				insert into #tempCurrentLevlProductPlan(UUID, GroupId, GroupSeq, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
				select UUID, DENSE_RANK() over(Order by Item), DENSE_RANK() over(Partition by Item order by StartTime),
				Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime from #tempNextLevlProductPlan

				truncate table #tempNextLevlProductPlan
			end

			select @MaxRowId = MAX(RowId) from #tempBomDetail
			if (@ExpandLevel >= 99)
			begin
				RAISERROR(N'成品分解超过99次，可能有循环。', 16, 1) 
			end
			else
			begin
				set @ExpandLevel = @ExpandLevel + 1
			end
		end
		-----------------------------↑循环分解Bom，添加下层生产计划-----------------------------



		-----------------------------↓合并生产计划-----------------------------
		--合并生产计划至一行（最小UUID)
		update p set Qty = t.Qty
		from #tempProductPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(Qty) as Qty from #tempProductPlanDet group by Item, StartTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--合并需求中间表，把需求中间表UUID全部更新为最小UUID
		update dt set UUID = t.MinUUID
		from #tempProductPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, StartTime from #tempProductPlanDet group by Item, StartTime having count(1) > 1) as t
		on p.Item = t.Item and p.StartTime = t.StartTime
		inner join #tempProductPlanDetTrace as dt on p.UUID = dt.UUID
		--删除重复毛需求
		delete p from #tempProductPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, StartTime from #tempProductPlanDet group by Item, StartTime having count(1) > 1) as t
		on p.Item = t.Item and p.StartTime = t.StartTime
		where p.UUID <> MinUUID
		-----------------------------↑合并生产计划-----------------------------



		-----------------------------↓低于安全库存的转为当天的生产计划-----------------------------
		update p set Qty = p.Qty - d.RemainQty
		from #tempProductPlanDet as p inner join #tempLocatoinDet as d on p.Item = d.Item and p.StartTime = @DateNow
		where d.RemainQty < 0
		insert into #tempProductPlanDet(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
		select NEWID(), d.Item, i.Desc1, null, i.Uom, -d.RemainQty, ISNULL(i.Bom, d.Item), @DateNow, DATEADD(day, ISNULL(i.LeadTime, 0), @DateNow) 
		from  #tempLocatoinDet as d left join #tempProductPlanDet as p on p.Item = d.Item and p.StartTime = @DateNow
		inner join Item as i on d.Item = i.Code
		where d.RemainQty < 0 and p.Item is null
		-----------------------------↑低于安全库存的转为当天的生产计划-----------------------------



		-----------------------------↓更新订单数-----------------------------
		--查找订单数
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, EffDate, OrderQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, ord.OrderQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord
		inner join (select distinct Item from #tempProductPlanDet) as pl on ord.Item = pl.Item
		where ord.OrderType = 'Production' and ord.StartTime >= @DateNow

		--更新订单数
		update pl set OrderQty = ISNULL(ord.OrderQty, 0)
		from #tempProductPlanDet as pl
		left join (select Item, EffDate, SUM(ISNULL(OrderQty, 0) - ISNULL(RecQty, 0)) as OrderQty from #tempOpenOrder group by Item, EffDate) as ord 
		on pl.Item = ord.Item and pl.StartTime = ord.EffDate
		insert into #tempProductPlanDet(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, OrderQty, Bom, StartTime, WindowTime)
		select NEWID(), ord.Item, i.Desc1, null, i.Uom, 0, ord.OrderQty, ISNULL(i.Bom, ord.Item), ord.EffDate, DATEADD(day, ISNULL(i.LeadTime, 0), ord.EffDate) 
		from (select Item, EffDate, SUM(ISNULL(OrderQty, 0) - ISNULL(RecQty, 0)) as OrderQty from #tempOpenOrder group by Item, EffDate) as ord
		inner join Item as i on ord.Item = i.Code
		left join #tempProductPlanDet as pl on pl.Item = ord.Item and pl.StartTime = ord.EffDate
		where pl.Item is null

		--更新订单表关联关系
		update ord set UUID = pl.UUID
		from #tempOpenOrder as ord inner join #tempProductPlanDet as pl on pl.Item = ord.Item and pl.StartTime = ord.EffDate

		--根据开始时间依次扣减订单数
		set @RowId = null
		set @MaxRowId = null
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempOpenOrder
		while (@RowId <= @MaxRowId)
		begin
			set @ActiveQty = 0
			set @LastActiveQty = 0
			set @Item = null
			set @StartTime = null

			select @ActiveQty = (OrderQty - RecQty), @Item = Item, @StartTime = StartTime from #tempOpenOrder where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set Qty = CASE WHEN @ActiveQty >= Qty THEN 0 WHEN @ActiveQty < Qty and @ActiveQty>0 THEN Qty - @ActiveQty ELSE Qty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = Qty
				from #tempProductPlanDet as det with(INDEX(IX_StartTime))
				where det.Item = @Item and det.StartTime >= @StartTime
			end

			set @RowId = @RowId + 1
		end

		--汇总生产需求
		update d set ReqQty = ISNULL(dt.ReqQty, 0) from #tempProductPlanDet as d
		left join (select UUID, SUM(ISNULL(ReqQty, 0)) as ReqQty from #tempProductPlanDetTrace group by UUID) as dt on d.UUID = dt.UUID
		-----------------------------↑更新订单数-----------------------------



		-----------------------------↓生产数按包装圆整-----------------------------
		--数量按包装圆整
		update pl set Qty = ceiling(pl.Qty / i.UC) * i.UC, UC = i.UC, OrgQty = Qty
		from #tempProductPlanDet as pl inner join Item as i on pl.Item = i.Code
		where pl.Qty > 0 and i.UC > 0

		--经济批量
		update pl set Qty = CASE WHEN Qty < i.MinLotSize THEN i.MinLotSize ELSE Qty END , MinLotSize = i.MinLotSize
		from #tempProductPlanDet as pl inner join Item as i on pl.Item = i.Code
		where pl.Qty > 0 and i.MinLotSize > 0

		update det set OverflowQty = tmp.OverflowQty
		from #tempProductPlanDet as det inner join
		(select det2.Item, det2.StartTime, SUM(ISNULL(det1.Qty, 0) - ISNULL(det1.OrgQty, 0)) as OverflowQty
		from #tempProductPlanDet as det1 inner join #tempProductPlanDet as det2 on det1.Item = det2.Item
		where det1.StartTime <= det2.StartTime
		group by det2.Item, det2.StartTime) as tmp on det.Item = tmp.Item and det.StartTime = tmp.StartTime
	
		set @LastOverflowCount = 0
		select @CurrentOverflowCount = COUNT(1) from #tempProductPlanDet 
		where OverflowQty >= UC and UC > 0 and Qty >= UC and ((MinLotSize > 0 and Qty >= (MinLotSize + UC)) or (MinLotSize is null))
		while @LastOverflowCount <> @CurrentOverflowCount
		begin
			update det set Qty = Qty - CASE WHEN det.StartTime = tmp.StartTime THEN UC ELSE 0 END, OverflowQty = OverflowQty - UC
			from #tempProductPlanDet as det inner join (select Item, MIN(StartTime) as StartTime from #tempProductPlanDet 
													where OverflowQty >= UC and UC > 0 and Qty >= UC
													and ((MinLotSize > 0 and Qty >= (MinLotSize + UC)) or (MinLotSize is null)) 
													group by Item) as tmp 
													on det.Item = tmp.Item and det.StartTime >= tmp.StartTime

			set @LastOverflowCount = @CurrentOverflowCount
			select @CurrentOverflowCount = COUNT(1) from #tempProductPlanDet 
			where OverflowQty >= UC and UC > 0 and Qty >= UC
			and ((MinLotSize > 0 and Qty >= (MinLotSize + UC)) or (MinLotSize is null))
		end
		-----------------------------↑生产数按包装圆整-----------------------------

	end try
	begin catch
		set @Msg = N'运行主生产计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1)
		return
	end catch 

	begin try
		if @trancount = 0
		begin
			begin tran
		end

		-----------------------------↓生成主生产计划-----------------------------
		--删除未释放的发运计划
		delete from MRP_ProductionPlanOpenOrder where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo)
		delete from MRP_ProductionPlanInitLocationDet where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo)
		delete from MRP_ProductionPlanDetTrace where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo)
		delete from MRP_ProductionPlanDet where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo)
		delete from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo

		--新增主生产计划头
		insert into MRP_ProductionPlanMstr(ReleaseNo, BatchNo, EffDate, CreateDate, CreateUser)
		values(@ShipPlanReleaseNo, @BatchNo, @DateNow, @DateTimeNow, @RunUser)

		--获取主生产计划头Id
		set @ProductionPlanId = @@Identity

		--新增主生产计划期初库存
		insert into MRP_ProductionPlanInitLocationDet(ProductionPlanId, Item, InitStock, SafeStock, MaxStock, InTransitQty, CreateDate, CreateUser)
		select @ProductionPlanId, Item, Qty, SafeStock, MaxStock, InTransitQty, @DateTimeNow, @RunUser from #tempLocatoinDet

		--新增主生产计划明细
		insert into MRP_ProductionPlanDet(ProductionPlanId, UUID, Item, ItemDesc, RefItemCode, ReqQty, OrgQty, Qty, OrderQty, Uom, UC, MinLotSize, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @ProductionPlanId, UUID, Item, ItemDesc, RefItemCode, ISNULL(ReqQty, 0), ISNULL(Qty, 0), ISNULL(Qty, 0), ISNULL(OrderQty, 0), Uom, UC, MinLotSize, StartTime, WindowTime, @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempProductPlanDet

		--新增主生产计划明细追溯
		insert into MRP_ProductionPlanDetTrace(ProductionPlanId, UUID, Flow, Item, Bom, ReqDate, ReqQty, RateQty, ScrapPct, Uom, UnitQty, CreateDate, CreateUser)
		select @ProductionPlanId, UUID, Flow, Item, Bom, ReqDate, ReqQty, RateQty, ScrapPct, Uom, UnitQty, @DateTimeNow, @RunUser from #tempProductPlanDetTrace
		
		--新增Open Order追溯表
		insert into MRP_ProductionPlanOpenOrder(ProductionPlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, RecQty, CreateDate, CreateUser)
		select @ProductionPlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, RecQty, @DateTimeNow, @RunUser from #tempOpenOrder
		-----------------------------↑生成主生产计划-----------------------------

		--记录日志
		insert into MRP_RunProductionPlanLog(BatchNo, Lvl, Item, Bom, EffDate, Msg, CreateDate, CreateUser)
		select @BatchNo, Lvl, Item, Bom, EffDate, Msg, @DateTimeNow, @RunUser from #tempMsg

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
       
		set @Msg = N'运行主生产计划异常' + Error_Message()
		insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, @DateNow, 'Error', @Msg, @DateTimeNow, @RunUser)
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


