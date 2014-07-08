SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunWeeklyProductionPlan') 
     DROP PROCEDURE RunWeeklyProductionPlan
GO

CREATE PROCEDURE [dbo].RunWeeklyProductionPlan
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
	declare @GroupId int
	declare @MaxGroupId int
	declare @GroupSeq int
	declare @MaxGroupSeq int
	declare @Item varchar(50)
	declare @Bom varchar(50)
	declare @Effdate datetime
	declare @BomQty decimal(18, 8)
	declare @MaxRowId int

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
			Qty decimal(18, 8),
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StartTime datetime,
			WindowTime datetime
		)

		-----------------------------↓获取顶层毛需求-----------------------------
		insert into #tempCurrentLevlProductPlan(UUID, GroupId, GroupSeq, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
		select NEWID(), DENSE_RANK() over(Order by det.Item), DENSE_RANK() over(Partition by Item order by det.StartTime),
		det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, SUM(ISNULL(det.ReqQty, 0) * (1 + ISNULL(i.ScrapPct, 0) / 100) * ISNULL(det.UnitQty, 0)) as Qty, ISNULL(i.Bom, det.Item),
		DATEADD(day, -ISNULL(i.LeadTime, 0), det.StartTime) as StartTime, det.StartTime as WindowTime
		from MRP_WeeklyShipPlan as det 
		inner join Item as i on det.Item = i.Code
		group by det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, det.StartTime, i.Bom, i.LeadTime

		
		if not exists(select top 1 1 from #tempCurrentLevlProductPlan)
		begin
			RAISERROR(N'没有找到有效的发运计划。', 16, 1) 
			return			
		end

		--删除开始日期小于今天的需求
		delete from #tempCurrentLevlProductPlan where StartTime < @DateNow
		-----------------------------↑获取顶层毛需求-----------------------------



		-----------------------------↓循环分解Bom，添加下层生产计划-----------------------------
		declare @ExpandLevel int
		set @ExpandLevel = 1
		while exists(select top 1 1 from #tempCurrentLevlProductPlan)
		begin
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
					insert into #tempMsg(Lvl, Phase, Item, Msg) values('Error', 'Production', @Item, N'物料[' + @Item + N']的Bom[' + @Bom + N']主数据不存在或已停用')
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
							insert into #tempMsg(Lvl, Phase, Item, Msg) values('Error', 'Production', @Item, N'物料[' + @Item + N']Bom[' + @Bom + N']上线日期[' + convert(varchar(10), @EffDate, 121) + ']的Bom明细不存在')
							delete from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq
							set @GroupSeq = @GroupSeq + 1
							continue
						end

						--如果下层是半成品，插入下层生产计划
						insert into #tempNextLevlProductPlan(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, 
						RateQty, ScrapPct, StartTime, WindowTime)
						select NEWID(), bom.Item, i.Desc1, null, bom.Uom, @BomQty * bom.RateQty * (1 + bom.ScrapPct / 100), ISNULL(i.Bom, bom.Item),
						bom.RateQty, bom.ScrapPct, DATEADD(day, -ISNULL(i.LeadTime, 0), @EffDate), @EffDate
						from #tempBomDetail as bom 
						inner join Item as i on bom.Item = i.Code
						inner join BomMstr as bm on (i.Bom is not null and bm.Code = i.Bom) or (i.Bom is null and bm.Code = bom.Item)
						where bm.IsActive = 1

						--删除开始日期小于今天的需求
						delete from #tempNextLevlProductPlan where StartTime < @DateNow
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
		--删除重复毛需求
		delete p from #tempProductPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, StartTime from #tempProductPlanDet group by Item, StartTime having count(1) > 1) as t
		on p.Item = t.Item and p.StartTime = t.StartTime
		where p.UUID <> MinUUID
		-----------------------------↑合并生产计划-----------------------------

		--生成生产计划
		insert into MRP_WeeklyProductionPlan(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
		select UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime from #tempProductPlanDet

		--记录日志
		insert into MRP_RunWeeklyMRPLog(BatchNo, Lvl, Phase, Flow, Item, Msg, CreateDate, CreateUser)
		select @BatchNo, Lvl, Phase, Flow, Item, Msg, @DateTimeNow, @RunUser from #tempMsg

		drop table #tempMsg
		drop table #tempCurrentLevlProductPlan
		drop table #tempBomDetail
		drop table #tempNextLevlProductPlan
		drop table #tempProductPlanDet
	end try
	begin catch
		set @Msg = N'运行生产计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
		return
	end catch 
END 


