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
	declare @ProductionPlanId int
	declare @StartTime datetime
	declare @LastOverflowCount int
	declare @CurrentOverflowCount int
	declare @ReleaseNo int

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
			ShipQty decimal(18, 8),
			OrderQty decimal(18, 8),
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			EffDate datetime,
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_tempCurrentLevlProductPlan_Item_WindowTime on #tempCurrentLevlProductPlan(Item asc, WindowTime asc)

		create table #tempProductPlanDetTrace
		(
			RowId int Identity(1, 1) primary key,
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

		create index IX_tempProductPlanDetTrace_UUID on #tempProductPlanDetTrace(UUID asc)

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
			WindowTime datetime,
			ReqQty decimal(18, 8)
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
			InventoryCountDownQty decimal(18, 8),
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			OrderQty decimal(18, 8),
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_tempProductPlanDet_Item_StartTime on #tempProductPlanDet(Item asc, StartTime asc)

		create table #tempLocationDet
		(
			RowId int identity(1, 1) primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			InspectQty decimal(18, 8), 
			InTransitQty decimal(18, 8), 
			SafeStock decimal(18, 8),
			MaxStock decimal(18, 8),
			RemainQty decimal(18, 8),
			InventoryCountDown decimal(18, 8),
			NetInventoryCountDown decimal(18, 8)
		)

		create index IX_tempLocatoinDet_Item on #tempLocationDet(Item asc)

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
			StartDate datetime,
			EndDate datetime
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

		create table #tempWeeklyProductionPlanDetMap
		(
			RowId int identity(1, 1) primary key,
			DailyUUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			WeeklyUUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			WeeklyStartTime datetime,
			WeeklyWindowTime datetime,
			DailyStartTime datetime
		)

		create table #tempBomCode
		(
			Code varchar(50) COLLATE  Chinese_PRC_CI_AS primary key 
		)

		create index IX_tempWeeklyProductionPlanDetMap_DailyUUID on #tempWeeklyProductionPlanDetMap(DailyUUID asc)

		create table #tempInventoryCountDown
		(
			RowId int identity(1, 1) primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			InventoryCountDown decimal(18, 8)
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
		--缓存BomCode
		insert into #tempBomCode select mstr.Code from BomMstr as mstr inner join BomDet as det on mstr.Code = det.Bom group by mstr.Code

		--记录计划数
		insert into #tempCurrentLevlProductPlan(UUID, GroupId, GroupSeq, Item, ItemDesc, RefItemCode, Uom, ShipQty, OrderQty, Bom, EffDate, StartTime, WindowTime)
		select NEWID(), DENSE_RANK() over(Order by det.Item), DENSE_RANK() over(Partition by Item order by det.StartTime),
		det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, 
		SUM((ISNULL(det.ShipQty, 0)) * (1 + ISNULL(i.ScrapPct, 0) / 100) * ISNULL(det.UnitQty, 0)) as Qty,
		SUM((ISNULL(det.OrderQty, 0)) * (1 + ISNULL(i.ScrapPct, 0) / 100) * ISNULL(det.UnitQty, 0)) as Qty, ISNULL(i.Bom, det.Item),
		det.StartTime as EffDate, DATEADD(day, -ISNULL(i.LeadTime, 0), det.StartTime) as StartTime, det.StartTime as WindowTime
		from MRP_ShipPlanDet as det 
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Item as i on det.Item = i.Code
		inner join #tempBomCode as pBom on ((i.Bom is not null and pBom.Code = i.Bom) or (i.Bom is null and pBom.Code = det.Item))
		where mstr.ReleaseNo = @ShipPlanReleaseNo and det.[Type] = 'Daily'
		group by det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, det.StartTime, i.Bom, i.LeadTime

		--如果生效时间小于今天的把开始时间设置为今天，不考虑过期的计划数，但是要考虑过期的订单数
		update tmp set StartTime = @DateNow, WindowTime = DATEADD(day, ISNULL(i.LeadTime, 0), @DateNow), ShipQty = 0
		from #tempCurrentLevlProductPlan as tmp inner join Item as i on tmp.Item = i.Code
		where tmp.StartTime < @DateNow
		update #tempCurrentLevlProductPlan set Qty = ShipQty + OrderQty

		--记录物料追溯表
		insert into #tempProductPlanDetTrace(UUID, Flow, Item, ReqDate, ReqQty, ScrapPct, Uom, UnitQty)
		select t.UUID, det.Flow, det.Item, det.StartTime, ISNULL(det.ShipQty, 0), i.ScrapPct, det.Uom, det.UnitQty
		from MRP_ShipPlanDet as det
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Item as i WITH(NOLOCK) on det.Item = i.Code
		inner join #tempCurrentLevlProductPlan as t on t.Item = det.Item and t.EffDate = det.StartTime
		where mstr.ReleaseNo = @ShipPlanReleaseNo and det.[Type] = 'Daily' and ISNULL(det.ShipQty, 0) <> 0
		and DATEADD(day, -ISNULL(i.LeadTime, 0), det.StartTime) >= @DateNow
		insert into #tempProductPlanDetTrace(UUID, Flow, Item, ReqDate, ReqQty, ScrapPct, Uom, UnitQty)
		select t.UUID, det.Flow, det.Item, det.StartTime, ISNULL(det.OrderQty, 0), i.ScrapPct, det.Uom, det.UnitQty
		from MRP_ShipPlanDet as det
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Item as i WITH(NOLOCK) on det.Item = i.Code
		inner join #tempCurrentLevlProductPlan as t on t.Item = det.Item and t.EffDate = det.StartTime
		where mstr.ReleaseNo = @ShipPlanReleaseNo and det.[Type] = 'Daily' and ISNULL(det.OrderQty, 0) <> 0
		-----------------------------↑获取顶层毛需求-----------------------------



		-----------------------------↓循环分解Bom，添加下层生产计划-----------------------------
		declare @ExpandLevel int
		set @ExpandLevel = 1
		while exists(select top 1 1 from #tempCurrentLevlProductPlan)
		begin
			-----------------------------↓计算可用库存-----------------------------
			insert into #tempLocationDet(Item, Qty, InspectQty, InTransitQty, SafeStock, MaxStock, RemainQty, InventoryCountDown, NetInventoryCountDown)
			select p.Item, ISNULL(a.Qty, 0), ISNULL(a.InspectQty, 0), ISNULL(a.InTransitQty, 0), ISNULL(i.SafeStock, 0), ISNULL(i.MaxStock, 0),
			ISNULL(a.Qty, 0) + ISNULL(a.InspectQty, 0) + ISNULL(a.InTransitQty, 0) - ISNULL(i.SafeStock, 0), i.InvCountDown, 
			(i.InvCountDown - ISNULL(a.Qty, 0) - ISNULL(a.InspectQty, 0)) as NetInventoryCountDown
			from (select distinct Item from #tempCurrentLevlProductPlan) as p
			inner join Item as i WITH(NOLOCK) on p.Item = i.Code 
			left join (select loc.Item, SUM(loc.Qty) as Qty, SUM(loc.InspectQty) as InspectQty, SUM(loc.InTransitQty) as InTransitQty
						from MRP_LocationDetSnapShot as loc
						inner join Location as l WITH(NOLOCK) on loc.Location = l.Code
						inner join (select distinct Item from #tempCurrentLevlProductPlan) as p on loc.Item = p.Item
						where l.IsFG = 1  --取成品库位库存
						group by loc.Item
						) as a on p.Item = a.Item
			left join #tempLocationDet as t on p.Item = t.Item
			where t.Item is null  --过滤已经添加到列表中的库存
			-----------------------------↑计算可用库存-----------------------------



			-----------------------------↓计算净需求-----------------------------
			set @RowId = null
			set @MaxRowId = null
			select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempLocationDet
			while (@RowId <= @MaxRowId)
			begin
				set @Item = null
				set @ActiveQty = null
				set @LastActiveQty = 0

				select @ActiveQty = RemainQty, @Item = Item from #tempLocationDet where RowId = @RowId
				if (@ActiveQty > 0)
				begin
					update det set Qty = CASE WHEN @ActiveQty >= Qty THEN 0 WHEN @ActiveQty < Qty and @ActiveQty > 0 THEN Qty - @ActiveQty ELSE Qty END,
					@ActiveQty = CASE WHEN @ActiveQty >= @LastActiveQty THEN @ActiveQty - @LastActiveQty ELSE 0 END, 
					@LastActiveQty = Qty
					from #tempCurrentLevlProductPlan as det with(INDEX(IX_tempCurrentLevlProductPlan_Item_WindowTime))
					where det.Item = @Item
				end

				update #tempLocationDet set RemainQty = @ActiveQty where RowId = @RowId
			
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

				if not exists(select top 1 1 from BomMstr WITH(NOLOCK) where Code = @Bom and IsActive = 1)
				begin   --没有找到Bom主数据
					insert into #tempMsg(Lvl, Item, Bom, Msg) values('Error', @Item, @Bom, N'物料[' + @Item + N']的Bom[' + @Bom + N']主数据不存在或已停用')
					delete from #tempCurrentLevlProductPlan where Item = @Bom
					set @GroupId = @GroupId + 1
					continue
				end

				truncate table #tempBomDetail
				--查找物料Bom，不按生效日期循环查找，所有BOM全部查出来
				insert into #tempBomDetail exec GetFlatBomDetailWithoutEffDate @Bom

				if not exists(select top 1 1 from #tempBomDetail)
				begin --展开后没有Bom明细
					insert into #tempMsg(Lvl, Item, Bom, EffDate, Msg) values('Error', @Item, @Bom, @EffDate, N'物料[' + @Item + N']Bom[' + @Bom + N']的Bom明细不存在')
					delete from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq
					set @GroupId = @GroupId + 1
					continue
				end

				truncate table #tempTempNextLevlProductPlan
				insert into #tempTempNextLevlProductPlan(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, 
				RateQty, ScrapPct, StartTime, WindowTime, ReqQty)
				select NEWID(), bom.Item, i.Desc1, null, bom.Uom, pl.Qty * bom.RateQty * (1 + bom.ScrapPct / 100), ISNULL(i.Bom, bom.Item),
				bom.RateQty, bom.ScrapPct, DATEADD(day, -ISNULL(i.LeadTime, 0), EffDate), EffDate, pl.Qty
				from #tempBomDetail as bom 
				inner join #tempCurrentLevlProductPlan as pl on bom.StartDate <= pl.EffDate and (bom.EndDate >= pl.EffDate or bom.EndDate is null) 
				inner join Item as i WITH(NOLOCK) on bom.Item = i.Code
				inner join #tempBomCode as pBom on ((i.Bom is not null and pBom.Code = i.Bom) or (i.Bom is null and pBom.Code = bom.Item))
				where  pl.GroupId = @GroupId

				--把开始时间小于今天把开始时间更新为今天
				update tmp set StartTime = @DateNow, WindowTime = DATEADD(day, ISNULL(i.LeadTime, 0), @DateNow)
				from #tempTempNextLevlProductPlan as tmp inner join Item as i on tmp.Item = i.Code
				where StartTime < @DateNow

				--插入物料追溯表
				insert into #tempProductPlanDetTrace(UUID, Item, ReqDate, ReqQty, Bom, RateQty, ScrapPct, Uom)
				select pl.UUID, @Item, pl.StartTime, pl.ReqQty, @Bom, pl.RateQty, pl.ScrapPct, pl.Uom 
				from #tempTempNextLevlProductPlan as pl
				inner join Item as i on pl.Item = i.Code

				--插入下层生产计划
				insert into #tempNextLevlProductPlan(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, RateQty, ScrapPct, StartTime, WindowTime)
				select UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, RateQty, ScrapPct, StartTime, WindowTime from #tempTempNextLevlProductPlan

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
		from #tempProductPlanDet as p inner join #tempLocationDet as d on p.Item = d.Item and p.StartTime = @DateNow
		where d.RemainQty < 0
		insert into #tempProductPlanDet(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
		select NEWID(), d.Item, i.Desc1, null, i.Uom, -d.RemainQty, ISNULL(i.Bom, d.Item), @DateNow, DATEADD(day, ISNULL(i.LeadTime, 0), @DateNow) 
		from  #tempLocationDet as d left join #tempProductPlanDet as p on p.Item = d.Item and p.StartTime = @DateNow
		inner join Item as i on d.Item = i.Code
		where d.RemainQty < 0 and p.Item is null
		-----------------------------↑低于安全库存的转为当天的生产计划-----------------------------



		-----------------------------↓补齐日计划-----------------------------
		--select @StartTime = @DateNow, @MaxStartTime = MAX(StartTime) from #tempProductPlanDet

		--while (@StartTime <= @MaxStartTime)
		--begin
		--	insert into #tempStartTime(StartTime) values (@StartTime)
		--	set @StartTime = DATEADD(day, 1, @StartTime)
		--end

		--insert into #tempProductPlanDet(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
		--select NEWID(), tmp.Item, i.Desc1, null, i.Uom, 0, ISNULL(i.Bom, tmp.Item), tmp.StartTime, DATEADD(day, ISNULL(i.LeadTime, 0), tmp.StartTime) 
		--from (select a.StartTime, b.Item from #tempStartTime as a, (select distinct Item from #tempProductPlanDet) as b ) as tmp 
		--inner join Item as i on tmp.Item = i.Code
		--left join #tempProductPlanDet as p on p.Item = tmp.Item and p.StartTime = tmp.StartTime
		--where p.Item is null
		-----------------------------↑补齐日计划-----------------------------



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
			set @ActiveQty = null
			set @LastActiveQty = 0
			set @Item = null
			set @StartTime = null

			select @ActiveQty = (OrderQty - RecQty), @Item = Item, @StartTime = CONVERT(varchar(10), StartTime, 121) from #tempOpenOrder where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set Qty = CASE WHEN @ActiveQty >= Qty THEN 0 WHEN @ActiveQty < Qty and @ActiveQty>0 THEN Qty - @ActiveQty ELSE Qty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = Qty
				from #tempProductPlanDet as det with(INDEX(IX_tempProductPlanDet_Item_StartTime))
				where det.Item = @Item and det.StartTime >= @StartTime
			end

			set @RowId = @RowId + 1
		end

		--汇总生产需求
		update d set ReqQty = ISNULL(dt.ReqQty, 0) from #tempProductPlanDet as d
		left join (select UUID, SUM(ISNULL(ReqQty, 0)) as ReqQty from #tempProductPlanDetTrace group by UUID) as dt on d.UUID = dt.UUID
		-----------------------------↑更新订单数-----------------------------



		-----------------------------↓生产数按包装圆整-----------------------------
		----取包装量、经济批量
		update pl set UC = i.UC, MinLotSize = i.MinLotSize, OrgQty = Qty
		from #tempProductPlanDet as pl inner join Item as i on pl.Item = i.Code

		--数量按包装圆整
		update #tempProductPlanDet set Qty = ceiling(Qty / UC) * UC where UC > 0

		--经济批量
		update #tempProductPlanDet set Qty = CASE WHEN Qty < MinLotSize THEN MinLotSize ELSE Qty END where MinLotSize > 0

		--记录溢出量
		update det set OverflowQty = tmp.OverflowQty
		from #tempProductPlanDet as det inner join
		(select det2.Item, det2.StartTime, SUM(ISNULL(det1.Qty, 0) - ISNULL(det1.OrgQty, 0)) as OverflowQty
		from #tempProductPlanDet as det1 inner join #tempProductPlanDet as det2 on det1.Item = det2.Item
		where det1.StartTime <= det2.StartTime
		group by det2.Item, det2.StartTime) as tmp on det.Item = tmp.Item and det.StartTime = tmp.StartTime

		--先扣减经济批量
		set @LastOverflowCount = 0
		select @CurrentOverflowCount = COUNT(1) from #tempProductPlanDet 
		where OverflowQty >= MinLotSize and MinLotSize > 0 and (MinLotSize = Qty or MinLotSize >= Qty * 2)
		while @LastOverflowCount <> @CurrentOverflowCount
		begin
			update det set Qty = Qty - CASE WHEN det.StartTime = tmp.StartTime THEN MinLotSize ELSE 0 END, OverflowQty = OverflowQty - MinLotSize
			from #tempProductPlanDet as det inner join (select Item, MIN(StartTime) as StartTime from #tempProductPlanDet 
													where OverflowQty >= MinLotSize and MinLotSize > 0 and (MinLotSize = Qty or MinLotSize >= Qty * 2)
													group by Item) as tmp 
													on det.Item = tmp.Item and det.StartTime >= tmp.StartTime

			set @LastOverflowCount = @CurrentOverflowCount
			select @CurrentOverflowCount = COUNT(1) from #tempProductPlanDet 
			where OverflowQty >= MinLotSize and MinLotSize > 0 and (MinLotSize = Qty or MinLotSize >= Qty * 2)
		end
	
		--在扣减包装
		set @LastOverflowCount = 0
		select @CurrentOverflowCount = COUNT(1) from #tempProductPlanDet 
		where OverflowQty >= UC and UC > 0 and Qty >= UC and ((MinLotSize > 0 and Qty >= (MinLotSize + UC)) or (MinLotSize is null) or (MinLotSize = 0))
		while @LastOverflowCount <> @CurrentOverflowCount
		begin
			update det set Qty = Qty - CASE WHEN det.StartTime = tmp.StartTime THEN UC ELSE 0 END, OverflowQty = OverflowQty - UC
			from #tempProductPlanDet as det inner join (select Item, MIN(StartTime) as StartTime from #tempProductPlanDet 
													where OverflowQty >= UC and UC > 0 and Qty >= UC
													and ((MinLotSize > 0 and Qty >= (MinLotSize + UC)) or (MinLotSize is null) or (MinLotSize = 0)) 
													group by Item) as tmp 
													on det.Item = tmp.Item and det.StartTime >= tmp.StartTime

			set @LastOverflowCount = @CurrentOverflowCount
			select @CurrentOverflowCount = COUNT(1) from #tempProductPlanDet 
			where OverflowQty >= UC and UC > 0 and Qty >= UC
			and ((MinLotSize > 0 and Qty >= (MinLotSize + UC)) or (MinLotSize is null) or (MinLotSize = 0))
		end
		-----------------------------↑生产数按包装圆整-----------------------------



		-----------------------------↓考虑库存倒数-----------------------------		
		--库存净需求减去已下达的订单数（库存净需求 = 库存需求 - 库存数 - 已下达的订单数）
		update loc set NetInventoryCountDown = loc.NetInventoryCountDown - ord.OrderQty
		from #tempLocationDet as loc 
		inner join (select Item, SUM(OrderQty) as OrderQty from #tempProductPlanDet group by Item) as ord on loc.Item = ord.Item
		where loc.NetInventoryCountDown > 0

		--库存净需求小于等于0的把生产计划数全部更新为0
		update pl set Qty = 0
		from #tempProductPlanDet as pl inner join #tempLocationDet as loc on pl.Item = loc.Item
		where loc.NetInventoryCountDown <= 0
			
		if exists(select top 1 1 from #tempLocationDet where NetInventoryCountDown > 0)
		begin
			insert into #tempInventoryCountDown(Item, InventoryCountDown) 
			select Item, NetInventoryCountDown from #tempLocationDet where NetInventoryCountDown > 0

			set @RowId = null
			set @MaxRowId = null
			select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempInventoryCountDown
			while (@RowId <= @MaxRowId)
			begin
				set @ActiveQty = null
				set @LastActiveQty = 0
				set @Item = null

				select @ActiveQty = InventoryCountDown, @Item = Item from #tempInventoryCountDown where RowId = @RowId

				update #tempProductPlanDet set InventoryCountDownQty = Qty where Item = @Item
				update det set InventoryCountDownQty = CASE WHEN @ActiveQty >= InventoryCountDownQty THEN 0 WHEN @ActiveQty < InventoryCountDownQty and @ActiveQty>0 THEN InventoryCountDownQty - @ActiveQty ELSE InventoryCountDownQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = InventoryCountDownQty
				from #tempProductPlanDet as det with(INDEX(IX_tempProductPlanDet_Item_StartTime))
				where det.Item = @Item
				update #tempProductPlanDet set Qty = Qty - InventoryCountDownQty where Item = @Item

				set @RowId = @RowId + 1
			end
		end
		-----------------------------↑考虑库存倒数-----------------------------



		delete from #tempProductPlanDet where Item not like '3%'
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

		-----------------------------↓生成生产计划（日）-----------------------------
		--删除未释放的发运计划
		--delete from MRP_ProductionPlanOpenOrder where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where [Status] = 'Create')
		--delete from MRP_ProductionPlanInitLocationDet where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where [Status] = 'Create')
		--delete from MRP_ProductionPlanDetTrace where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where [Status] = 'Create')
		--delete from MRP_ProductionPlanDet where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where [Status] = 'Create')
		--delete from MRP_ProductionPlanMstr where  [Status] = 'Create'
		truncate table  MRP_ProductionPlanOpenOrder 
		truncate table  MRP_ProductionPlanInitLocationDet 
		truncate table  MRP_ProductionPlanDetTrace 
		truncate table  MRP_ProductionPlanDet 
		truncate table  MRP_ProductionPlanMstr

		--获取ReleaseNo
		select @ReleaseNo = ISNULL(MAX(ReleaseNo), 0) + 1 from MRP_ProductionPlanMstr

		--新增主生产计划头
		insert into MRP_ProductionPlanMstr(ReleaseNo, BatchNo, EffDate, [Status], CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		values(@ReleaseNo, @BatchNo, @DateNow, 'Create', @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1)

		--获取主生产计划头Id
		set @ProductionPlanId = @@Identity

		--新增主生产计划期初库存
		insert into MRP_ProductionPlanInitLocationDet(ProductionPlanId, [Type], Item, InitStock, SafeStock, MaxStock, InTransitQty, InventoryCountDown, CreateDate, CreateUser)
		select @ProductionPlanId, 'Daily', Item, Qty, SafeStock, MaxStock, InTransitQty, InventoryCountDown, @DateTimeNow, @RunUser from #tempLocationDet

		--新增主生产计划明细
		insert into MRP_ProductionPlanDet(ProductionPlanId, [Type], UUID, Item, ItemDesc, RefItemCode, ReqQty, OrgQty, Qty, OrderQty, Uom, UC, MinLotSize, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @ProductionPlanId, 'Daily', UUID, Item, ItemDesc, RefItemCode, ISNULL(ReqQty, 0), ISNULL(Qty, 0), ISNULL(Qty, 0), ISNULL(OrderQty, 0), Uom, UC, MinLotSize, StartTime, WindowTime, @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempProductPlanDet

		--新增主生产计划明细追溯
		insert into MRP_ProductionPlanDetTrace(ProductionPlanId, [Type], UUID, Flow, Item, Bom, ReqDate, ReqQty, RateQty, ScrapPct, Uom, UnitQty, CreateDate, CreateUser)
		select @ProductionPlanId, 'Daily', UUID, Flow, Item, Bom, ReqDate, ReqQty, RateQty, ScrapPct, Uom, UnitQty, @DateTimeNow, @RunUser from #tempProductPlanDetTrace
		
		--新增Open Order追溯表
		insert into MRP_ProductionPlanOpenOrder(ProductionPlanId, [Type], UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, RecQty, CreateDate, CreateUser)
		select @ProductionPlanId, 'Daily', UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, RecQty, @DateTimeNow, @RunUser from #tempOpenOrder
		-----------------------------↑生成主生产计划（日）-----------------------------



		-----------------------------↓生成主生产计划（周）-----------------------------
		set datefirst 1  --设置周一为一周开始时间

		--新增日计划和周计划的映射表
		insert into #tempWeeklyProductionPlanDetMap(DailyUUID, WeeklyUUID, WeeklyStartTime, WeeklyWindowTime, DailyStartTime)
		select tmp.UUID, pl.UUID, tmp.StartTime, DATEADD(DAY, ISNULL(i.LeadTime, 0), tmp.StartTime), tmp.OldStartTime
		from #tempProductPlanDet as pl 
		inner join (select DATEADD(DAY, -datepart(WEEKDAY, StartTime) + 1, StartTime) as StartTime, Item, UUID, StartTime as OldStartTime
					from #tempProductPlanDet) as tmp on 
					pl.Item = tmp.Item and pl.StartTime = tmp.StartTime
		inner join Item as i on pl.Item = i.Code

		--新增主生产计划明细
		insert into MRP_ProductionPlanDet(ProductionPlanId, [Type], UUID, Item, ItemDesc, RefItemCode, ReqQty, OrgQty, Qty, OrderQty, Uom, UC, MinLotSize, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @ProductionPlanId, 'Weekly', map.WeeklyUUID, pl.Item, pl.ItemDesc, pl.RefItemCode, SUM(ISNULL(ReqQty, 0)), SUM(ISNULL(Qty, 0)), SUM(ISNULL(Qty, 0)), SUM(ISNULL(OrderQty, 0)), pl.Uom, pl.UC, pl.MinLotSize, map.WeeklyStartTime, map.WeeklyWindowTime, @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempProductPlanDet as pl inner join #tempWeeklyProductionPlanDetMap as map on pl.UUID = map.DailyUUID
		where map.DailyStartTime >= DATEADD(DAY, 14, @DateNow)
		group by map.WeeklyUUID, pl.Item, pl.ItemDesc, pl.RefItemCode, pl.Uom, pl.UC, pl.MinLotSize, map.WeeklyStartTime, map.WeeklyWindowTime

		--新增主生产计划明细追溯
		insert into MRP_ProductionPlanDetTrace(ProductionPlanId, [Type], UUID, Flow, Item, Bom, ReqDate, ReqQty, RateQty, ScrapPct, Uom, UnitQty, CreateDate, CreateUser)
		select @ProductionPlanId, 'Weekly', map.WeeklyUUID, tr.Flow, tr.Item, tr.Bom, tr.ReqDate, tr.ReqQty, tr.RateQty, tr.ScrapPct, tr.Uom, tr.UnitQty, @DateTimeNow, @RunUser 
		from #tempProductPlanDetTrace as tr inner join #tempWeeklyProductionPlanDetMap as map on tr.UUID = map.DailyUUID
		where map.DailyStartTime >= DATEADD(DAY, 14, @DateNow)

		--新增Open Order追溯表
		insert into MRP_ProductionPlanOpenOrder(ProductionPlanId, [Type], UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, RecQty, CreateDate, CreateUser)
		select @ProductionPlanId, 'Weekly', map.WeeklyUUID, oo.Flow, oo.OrderNo, oo.Item, oo.StartTime, oo.WindowTime, oo.OrderQty, oo.RecQty, @DateTimeNow, @RunUser 
		from #tempOpenOrder as oo inner join #tempWeeklyProductionPlanDetMap as map on oo.UUID = map.DailyUUID
		where map.DailyStartTime >= DATEADD(DAY, 14, @DateNow)
		-----------------------------↑生成主生产计划（周）-----------------------------



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
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


