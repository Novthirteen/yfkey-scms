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
	declare @MaxMstrId int
	declare @MinPlandate datetime
	declare @Bom varchar(50)
	declare @EffDate datetime
	declare @BomQty decimal(18, 8)
	declare @ProdItem varchar(50)
	declare @Flow varchar(50)
	declare @Item varchar(50)
	declare @ActiveQty decimal(18, 8)
	declare @LastActiveQty decimal(18, 8)
	declare @ReleaseNo varchar(50)
	declare @PurchasePlanId int
	declare @LastOverflowCount int
	declare @CurrentOverflowCount int
	declare @WindowTime datetime
	declare @ShipPlanReleaseNo int
	declare @MaxWindowTime datetime

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''
	set @trancount = @@trancount

	exec GetNextSequence 'RunPurchasePlan', @BatchNo output
	begin try
		create table #tempMsg
		(
			Lvl varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Uom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			PlanDate datetime,
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Msg varchar(500) COLLATE  Chinese_PRC_CI_AS
		)

		create table #tempEffShiftPlan
		(
			RowId int identity(1, 1) Primary Key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			RefItemCode varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
			PlanDate datetime
		)

		create index IX_tempEffShiftPlan_Item on #tempEffShiftPlan(Item asc)

		create table #tempProdLine
		(
			RowId int Identity(1, 1) Primary Key,
			ProdLine varchar(50) COLLATE  Chinese_PRC_CI_AS,
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
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
			ReqTime datetime
		)

		create index IX_tempCurrentMaterialPlanDet_Item on #tempCurrentMaterialPlanDet(Item asc)

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
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
			ReqTime datetime
		)

		create index IX_tempMaterialPlanDet_Item_ReqTime on #tempMaterialPlanDet(Item asc, ReqTime asc)

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

		create index IX_tempMergeMaterialPlanDet_Item_ReqTime on #tempMergeMaterialPlanDet(Item asc, ReqTime asc)

		create table #tempPurchasePlanDet
		(
			RowId int identity(1, 1) primary key,
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			PurchaseFlow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			LeadTime int,
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
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
			UC decimal(18, 8),
			OrgPurchaseQty decimal(18, 8),
			OverflowQty decimal(18, 8),
			MinLotSize decimal(18, 8),
			StartTime datetime,
			WindowTime datetime,
		)

		create index IX_tempPurchasePlanDet_UUID on #tempPurchasePlanDet(UUID asc)
		create index IX_tempPurchasePlanDet_Flow_Item_WindowTime on #tempPurchasePlanDet(Item asc, PurchaseFlow asc, WindowTime asc)

		create table #tempMaterialPlanDetTrace
		(
			RowId int identity(1, 1) primary key,
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			ProdItem varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ProdQty decimal(18, 8),
			RateQty decimal(18, 8),
			ScrapPct decimal(18, 8),
			BomUnitQty decimal(18, 8),
			PurchaseUnitQty decimal(18, 8),
			BomUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			PurchaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			PlanDate datetime
		)

		create index IX_tempMaterialPlanDetTrace_UUID on #tempMaterialPlanDetTrace(UUID asc)

		create table #tempLocatoinDet
		(
			RowId int identity(1, 1) primary key,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			SafeStock decimal(18, 8),
			MaxStock decimal(18, 8),
			Qty decimal(18, 8),
			InTransitQty decimal(18, 8),
			InSpectQty decimal(18, 8),
			ActiveQty decimal(18, 8),
		)

		create index IX_tempLocatoinDet_Item on #tempLocatoinDet(Item asc)

		create table #tempIpDet
		(
			RowId int identity(1, 1)  primary key,
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			IpNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StartTime datetime,
			WindowTime datetime,
			Qty decimal(18, 8),
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

		create index IX_tempOpenOrder_Flow_Item_EffDate on #tempOpenOrder(Item asc, Flow asc, EffDate asc)

		create table #tempWeeklyPurchasePlanDetMap
		(
			RowId int identity(1, 1) primary key,
			DailyRowId int, 
			DailyUUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			WeeklyUUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			WeeklyStartTime datetime,
			WeeklyWindowTime datetime,
			DailyWindowTime datetime
		)

		create index IX_tempWeeklyPurchasePlanDetMap_DailyRowId on #tempWeeklyPurchasePlanDetMap(DailyRowId asc)

		create table #tempWindowTime
		(
			WindowTime datetime primary key
		)

		-----------------------------����ȡ�����ƻ�-----------------------------
		insert into #tempEffShiftPlan(Item, ItemDesc, RefItemCode, Qty, Uom, BaseUom, UnitQty, PlanDate)
		select det.Item, det.ItemDesc, det.RefItemCode, det.Qty, det.Uom, det.Uom, 1, det.StartTime
		from MRP_ProductionPlanDet as det inner join MRP_ProductionPlanMstr as mstr on det.ProductionPlanId = mstr.Id
		where mstr.Id in (select MAX(Id) from MRP_ProductionPlanMstr) and det.[Type] = 'Daily'
		-----------------------------����ȡ�����ƻ�-----------------------------



		-----------------------------��չ��Bom-----------------------------
		--����Bom���ȴ��������ң����ȡ���ϴ���
		update t set Bom = ISNULL(i.Bom, t.Item)
		from #tempEffShiftPlan as t 
		inner join Item as i on t.Item = i.Code

		--��¼��־û��Bom������
		insert into #tempMsg(Lvl, Item, Qty, Uom, PlanDate, Bom, Msg) 
		select 'Error', t.Item, t.Qty, t.Uom, t.PlanDate, t.Bom, 
		N'��Ʒ[ ' + t.Item + N']��Bom����[ ' + t.Bom + N']������' 
		from #tempEffShiftPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where b.Code is null
		--ɾ������ƻ�
		delete t from #tempEffShiftPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where b.Code is null

		--��¼��־û��Bom��ϸ
		insert into #tempMsg(Lvl, Item, Qty, Uom, PlanDate, Bom, Msg) 
		select 'Error', t.Item, t.Qty, t.Uom, t.PlanDate, t.Bom, 
		N'��Ʒ[ ' + t.Item + N']��Bom����[ ' + t.Bom + N']û��Bom��ϸ' 
		from #tempEffShiftPlan as t
		left join BomDet as b on t.Bom = b.Bom
		where b.Bom is null
		--ɾ������ƻ�
		delete t from #tempEffShiftPlan as t
		left join BomDet as b on t.Bom = b.Bom
		where b.Bom is null

		--ѭ��չ��Bom
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempEffShiftPlan
		while @RowId <= @MaxRowId
		begin
			if exists(select top 1 1 from #tempEffShiftPlan where RowId = @RowId)
			begin
				select @ProdItem = Item, @Bom = Bom, @EffDate = PlanDate, @BomQty = Qty 
				from #tempEffShiftPlan where RowId = @RowId
				
				truncate table #tempBomDetail
				insert into #tempBomDetail exec GetFlatBomDetail @Bom, @EffDate

				truncate table #tempCurrentMaterialPlanDet
				insert into #tempCurrentMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct,
				Uom, BaseUom, ReqTime)
				select NEWID(), t.Item, i.Desc1, @BomQty * (t.RateQty + t.ScrapPct / 100), t.RateQty, t.ScrapPct,
				t.Uom, i.Uom, @EffDate
				from #tempBomDetail as t inner join Item as i on t.Item = i.Code

				--���㵥λ���㣨Bom��λתΪ������λ��
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

				--ɾ��û��ά����λ���������
				insert into #tempMsg(Lvl, Item, Qty, Uom, PlanDate, Msg) 
				select 'Error', Item, ReqQty, Uom, ReqTime, 
				N'����[ ' + Item + N']û��ά����λ[ ' + Uom + N' ]�ͻ�����λ[' + BaseUom + N']�Ļ�����' 
				from #tempCurrentMaterialPlanDet where UnitQty is null
				delete from #tempCurrentMaterialPlanDet where UnitQty is null

				--��¼׷�ݹ�ϵ
				insert into #tempMaterialPlanDetTrace(UUID, ProdItem, ProdQty, RateQty, ScrapPct, BomUnitQty, BomUom, PlanDate)
				select UUID, @ProdItem, @BomQty, RateQty, ScrapPct, UnitQty, Uom, @EffDate 
				from #tempCurrentMaterialPlanDet 

				--��¼���ϼƻ���ʱ��
				insert into #tempMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime)
				select UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime from #tempCurrentMaterialPlanDet
			end
			set @RowId = @RowId + 1
		end

		--ɾ��������MRP���������
		delete tr from #tempMaterialPlanDetTrace as tr 
		inner join #tempMaterialPlanDet as pl on tr.UUID = pl.UUID
		inner join Item as i on pl.Item = i.Code
		where i.IsMRP <> 1
		delete pl from #tempMaterialPlanDet as pl inner join Item as i on pl.Item = i.Code where i.IsMRP <> 1
		-----------------------------��չ��Bom-----------------------------



		-----------------------------����ȡת��ó�׷����ƻ�-----------------------------
		select @ShipPlanReleaseNo = MAX(ReleaseNo) from MRP_ShipPlanMstr where [Status] = 'Submit'
		truncate table #tempCurrentMaterialPlanDet
		insert into #tempCurrentMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime)
		select NEWID(), det.Item, det.ItemDesc, SUM(det.ShipQty), 1, 0, det.Uom, det.BaseUom, det.UnitQty, det.StartTime
		from MRP_ShipPlanDet as det 
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Item as i on det.Item = i.Code
		left join BomDet as bd on ((i.Bom is not null and bd.Bom = i.Bom) or (i.Bom is null and bd.Bom = det.Item))
					and bd.StartDate <= det.StartTime and (bd.EndDate >= det.StartTime or bd.EndDate is null)
		where mstr.ReleaseNo = @ShipPlanReleaseNo and det.[Type] = 'Daily' and bd.Id is null
		group by det.Item, det.ItemDesc, det.Uom, det.BaseUom, det.UnitQty, det.StartTime

		--��¼׷�ݹ�ϵ
		insert into #tempMaterialPlanDetTrace(UUID, ProdItem, ProdQty, RateQty, ScrapPct, BomUnitQty, BomUom, PlanDate)
		select UUID, Item, ReqQty, 1, 0, 1, Uom, ReqTime 
		from #tempCurrentMaterialPlanDet 

		--��¼���ϼƻ���ʱ��
		insert into #tempMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime)
		select UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime from #tempCurrentMaterialPlanDet
		-----------------------------����ȡת��ó�׷����ƻ�-----------------------------



		-----------------------------����ȡ���ÿ��-----------------------------
		insert into #tempLocatoinDet(Item, SafeStock, MaxStock, Qty, InTransitQty, InSpectQty, ActiveQty)
		select Item, ISNULL(i.SafeStock, 0), ISNULL(i.MaxStock, 0), Qty, PurchaseInTransitQty, InSpectQty, Qty + InspectQty - ISNULL(i.SafeStock, 0)
		from 
		(
		select p.Item, SUM(ISNULL(loc.Qty, 0)) as Qty, SUM(ISNULL(loc.InTransitQty, 0)) as InTransitQty, SUM(ISNULL(loc.PurchaseInTransitQty, 0)) as PurchaseInTransitQty, SUM(ISNULL(loc.InspectQty, 0)) as InspectQty
		from MRP_LocationDetSnapShot as loc
		right join (select distinct Item from #tempMaterialPlanDet) as p on loc.Item = p.Item
		group by p.Item
		) as a inner join Item as i on a.Item = i.Code 
		-----------------------------����ȡ���ÿ��-----------------------------



		-----------------------------���ϲ��ɹ�����-----------------------------
		--ë�������ϲ���
		insert into  #tempMergeMaterialPlanDet(UUID, Item, ItemDesc, BaseReqQty, BasePurchaseQty, BaseUom, ReqTime)
		select UUID, Item, ItemDesc, ReqQty * UnitQty, ReqQty * UnitQty, BaseUom, ReqTime from #tempMaterialPlanDet order by Item, ReqTime
		--�ϲ�ë������һ�У���СUUID)
		update p set BaseReqQty = t.BaseReqQty, BasePurchaseQty = t.BaseReqQty
		from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(BaseReqQty) as BaseReqQty from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--�ϲ������м���������м��UUIDȫ������Ϊ��СUUID
		update dt set UUID = t.MinUUID
		from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, ReqTime from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.Item = t.Item and p.ReqTime = t.ReqTime
		inner join #tempMaterialPlanDetTrace as dt on p.UUID = dt.UUID
		--ɾ���ظ�ë����
		delete p from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, ReqTime from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.Item = t.Item and p.ReqTime = t.ReqTime
		where p.UUID <> MinUUID
		-----------------------------���ϲ��ɹ�����-----------------------------



		-----------------------------�����ڰ�ȫ����תΪ�������������ƻ�-----------------------------
		update p set BasePurchaseQty = BasePurchaseQty - d.ActiveQty
		from #tempMergeMaterialPlanDet as p inner join #tempLocatoinDet as d on p.Item = d.Item and p.ReqTime = @DateNow
		where d.ActiveQty < 0
		insert into #tempMergeMaterialPlanDet(UUID, Item, ItemDesc, BaseReqQty, BasePurchaseQty, BaseUom, ReqTime)
		select NEWID(), d.Item, i.Desc1, 0, -d.ActiveQty, i.Uom, @DateNow 
		from #tempLocatoinDet as d left join #tempMergeMaterialPlanDet as p on p.Item = d.Item and p.ReqTime = @DateNow
		inner join Item as i on d.Item = i.Code
		where d.ActiveQty < 0 and p.Item is null
		-----------------------------�����ڰ�ȫ����תΪ�������������ƻ�-----------------------------



		-----------------------------�������ռƻ�-----------------------------
		select @WindowTime = @DateNow, @MaxWindowTime = MAX(ReqTime) from #tempMergeMaterialPlanDet

		while (@WindowTime <= @MaxWindowTime)
		begin
			insert into #tempWindowTime(WindowTime) values (@WindowTime)
			set @WindowTime = DATEADD(day, 1, @WindowTime)
		end

		insert into #tempMergeMaterialPlanDet(UUID, Item, ItemDesc, BaseReqQty, BasePurchaseQty, BaseUom, ReqTime)
		select NEWID(), tmp.Item, i.Desc1, 0, 0, i.Uom, tmp.WindowTime
		from (select a.WindowTime, b.Item from #tempWindowTime as a, (select distinct Item from #tempMergeMaterialPlanDet) as b ) as tmp 
		inner join Item as i on tmp.Item = i.Code
		left join #tempMergeMaterialPlanDet as pl on tmp.Item = pl.Item and tmp.WindowTime = pl.ReqTime
		where pl.UUID is null
		-----------------------------�������ռƻ�-----------------------------


		
		-----------------------------�����㾻����-----------------------------
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
				from #tempMergeMaterialPlanDet as det with(INDEX(IX_tempMergeMaterialPlanDet_Item_ReqTime))
				where det.Item = @Item
			end

			set @RowId = @RowId + 1
		end		
		-----------------------------�����㾻����-----------------------------



		-----------------------------�����Ҳɹ�·��-----------------------------
		insert into #tempPurchasePlanDet(UUID, PurchaseFlow, LeadTime, Item, ItemDesc, RefItemCode, BaseReqQty, BasePurchaseQty, Uom, BaseUom, UC, MinLotSize, StartTime, WindowTime)
		select t.UUID, d.Flow, ISNULL(m.MRPLeadTime, 0), t.Item, t.ItemDesc, d.RefItemCode, t.BaseReqQty, t.BasePurchaseQty, d.Uom, t.BaseUom, ISNULL(d.HuLotSize, 0), ISNULL(d.MinLotSize, 0), DATEADD(day, -ISNULL(m.LeadTime, 0), t.ReqTime), t.ReqTime
		from #tempMergeMaterialPlanDet as t
		inner join FlowDet as d on t.Item = d.Item
		inner join FlowMstr as m on d.Flow = m.Code
		where m.[Type] = 'Procurement' and m.IsMRP = 1 and m.IsActive = 1

		--��¼��־û���ҵ��ɹ�·��
		insert into #tempMsg(Lvl, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', Item, ReqQty, BaseUom, WindowTime, 
		N'����[ ' + Item + N']û���ҵ��ɹ�·��' 
		from #tempPurchasePlanDet where PurchaseFlow is null
		--ɾ����������
		delete from #tempMaterialPlanDetTrace where UUID in (select UUID from #tempPurchasePlanDet where PurchaseFlow is null)
		delete from #tempPurchasePlanDet where PurchaseFlow is null

		--���㵥λ���㣨������λתΪ�ɹ���λ��
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
		
		--����׷�ݱ�
		update t set PurchaseUnitQty = m.UnitQty, PurchaseUom = m.Uom
		from #tempMaterialPlanDetTrace as t inner join #tempPurchasePlanDet as m on t.UUID = m.UUID

		--ɾ��û��ά����λ���������
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', PurchaseFlow, Item, ReqQty, Uom, WindowTime, 
		N'�ɹ�·��[' + PurchaseFlow + N']����[ ' + Item + N']û��ά����λ[ ' + Uom + N']�ͻ�����λ[' + BaseUom + N']�Ļ�����' 
		from #tempPurchasePlanDet where UnitQty is null
		delete from #tempPurchasePlanDet where UnitQty is null

		--����תΪ�ɹ���λ
		update #tempPurchasePlanDet set ReqQty = BaseReqQty * UnitQty, PurchaseQty = BasePurchaseQty * UnitQty
		-----------------------------�����Ҳɹ�·��-----------------------------


		-----------------------------��������;ASN-----------------------------
		insert into #tempIpDet(IpNo, Flow, Item, StartTime, WindowTime, Qty)
		select det.IpNo, det.Flow, det.Item, det.StartTime, DATEADD(day, ISNULL(fmstr.MRPLeadTime, 0), det.StartTime), SUM(det.Qty) as Qty
		from MRP_IpDetSnapShot as det inner join (select distinct Item from #tempMergeMaterialPlanDet) as mstr on det.Item = mstr.Item
		inner join FlowMstr as fmstr on fmstr.Code = det.Flow
		group by det.IpNo, det.Flow, det.Item, det.StartTime, fmstr.MRPLeadTime, det.WindowTime
		-----------------------------��������;-----------------------------



		-----------------------------��������ʱ��ۼ���;-----------------------------
		set @RowId = null
		set @MaxRowId = null
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempIpDet
		while (@RowId <= @MaxRowId)
		begin
			set @Item = null
			set @Flow = null
			set @ActiveQty = null
			set @WindowTime = null
			set @LastActiveQty = 0

			select @ActiveQty = Qty, @Item = Item, @Flow = Flow, @WindowTime = WindowTime from #tempIpDet where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set PurchaseQty = CASE WHEN @ActiveQty >= PurchaseQty THEN 0 WHEN @ActiveQty < PurchaseQty and @ActiveQty > 0 THEN PurchaseQty - @ActiveQty ELSE PurchaseQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = PurchaseQty
				from #tempPurchasePlanDet as det with(INDEX(IX_tempPurchasePlanDet_Flow_Item_WindowTime))
				where det.Item = @Item and det.PurchaseFlow = @Flow and det.WindowTime >= @WindowTime
			end

			set @RowId = @RowId + 1
		end		
		-----------------------------��������ʱ��ۼ���;-----------------------------



		-----------------------------�����¶�����-----------------------------
		--���Ҷ�����
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, DATEADD(day, pl.LeadTime, ord.WindowTime), ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord
		inner join (select distinct Item, PurchaseFlow, LeadTime from #tempPurchasePlanDet) as pl on ord.Item = pl.Item and ord.Flow = pl.PurchaseFlow

		--������Ч����
		update #tempOpenOrder set EffDate = CASE WHEN WindowTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), WindowTime, 121)) END

		--���¶�����
		update pl set OrderQty = ISNULL(ord.OrderQty, 0)
		from #tempPurchasePlanDet as pl
		left join (select Item, Flow, EffDate, SUM(ISNULL(OrderQty, 0) - ISNULL(ShipQty, 0)) as OrderQty from #tempOpenOrder group by Item, Flow, EffDate) as ord 
		on pl.Item = ord.Item and pl.PurchaseFlow = ord.Flow and pl.WindowTime = ord.EffDate
		insert into #tempPurchasePlanDet(UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, BaseReqQty, BasePurchaseQty, ReqQty, PurchaseQty, OrderQty, Uom, BaseUom, UC, MinLotSize, StartTime, WindowTime)
		select NEWID(), d.Flow, ord.Item, i.Desc1, d.RefItemCode, 0, 0, 0, 0, ord.OrderQty, d.Uom, i.Uom, ISNULL(d.HuLotSize, 0), ISNULL(d.MinLotSize, 0), DATEADD(day, -ISNULL(m.LeadTime, 0), @DateNow), @DateNow
		from (select Item, Flow, EffDate, SUM(ISNULL(OrderQty, 0) - ISNULL(ShipQty, 0)) as OrderQty from #tempOpenOrder group by Item, Flow, EffDate) as ord
		inner join Item as i on ord.Item = i.Code
		inner join FlowDet as d on ord.Item = d.Item
		inner join FlowMstr as m on d.Flow = m.Code
		left join #tempPurchasePlanDet as pl on pl.Item = ord.Item and pl.PurchaseFlow = ord.Flow and pl.WindowTime = ord.EffDate
		where m.[Type] = 'Procurement' and m.IsMRP = 1 and m.IsActive = 1 and pl.Item is null

		--���¶����������ϵ
		update ord set UUID = pl.UUID
		from #tempOpenOrder as ord inner join #tempPurchasePlanDet as pl on pl.Item = ord.Item and pl.PurchaseFlow = ord.Flow and pl.WindowTime = ord.EffDate
		-----------------------------�����¶�����-----------------------------



		-----------------------------��������ʱ��ۼ�����-----------------------------
		set @RowId = null
		set @MaxRowId = null
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempOpenOrder
		while (@RowId <= @MaxRowId)
		begin
			set @Item = null
			set @Flow = null
			set @ActiveQty = null
			set @WindowTime = null
			set @LastActiveQty = 0

			select @ActiveQty = OrderQty - ShipQty, @Item = Item, @Flow = Flow, @WindowTime = CONVERT(varchar(10), WindowTime, 121) from #tempOpenOrder where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set PurchaseQty = CASE WHEN @ActiveQty >= PurchaseQty THEN 0 WHEN @ActiveQty < PurchaseQty and @ActiveQty > 0 THEN PurchaseQty - @ActiveQty ELSE PurchaseQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = PurchaseQty
				from #tempPurchasePlanDet as det with(INDEX(IX_tempPurchasePlanDet_Flow_Item_WindowTime))
				where det.Item = @Item and det.PurchaseFlow = @Flow and det.WindowTime >= @WindowTime
			end

			set @RowId = @RowId + 1
		end		
		-----------------------------��������ʱ��ۼ�����-----------------------------



		-----------------------------��������ʱ��ϲ�����-----------------------------

		-----------------------------��������ʱ��ϲ�����-----------------------------



		-----------------------------������������װԲ��-----------------------------
		--��¼ԭ�ɹ�����
		update #tempPurchasePlanDet set OrgPurchaseQty = PurchaseQty
		 
		--��������װԲ��
		update #tempPurchasePlanDet set PurchaseQty = ceiling(PurchaseQty / UC) * UC where UC > 0

		--��������
		update #tempPurchasePlanDet set PurchaseQty = CASE WHEN PurchaseQty < MinLotSize THEN MinLotSize ELSE PurchaseQty END where MinLotSize > 0

		--��¼�����
		update det set OverflowQty = tmp.OverflowQty
		from #tempPurchasePlanDet as det inner join
		(select det2.Item, det2.PurchaseFlow, det2.WindowTime, SUM(ISNULL(det1.PurchaseQty, 0) - ISNULL(det1.OrgPurchaseQty, 0)) as OverflowQty
		from #tempPurchasePlanDet as det1 inner join #tempPurchasePlanDet as det2 on det1.Item = det2.Item and det1.PurchaseFlow = det2.PurchaseFlow 
		where det1.WindowTime <= det2.WindowTime
		group by det2.Item, det2.PurchaseFlow, det2.WindowTime) as tmp on det.Item = tmp.Item and det.PurchaseFlow = tmp.PurchaseFlow and det.WindowTime = tmp.WindowTime
		--�ȿۼ���������
		set @LastOverflowCount = 0
		select @CurrentOverflowCount = COUNT(1) from #tempPurchasePlanDet 
		where OverflowQty >= MinLotSize and MinLotSize > 0 and (MinLotSize = PurchaseQty or MinLotSize >= PurchaseQty * 2)
		while @LastOverflowCount <> @CurrentOverflowCount
		begin
			update det set PurchaseQty = PurchaseQty - CASE WHEN det.WindowTime = tmp.WindowTime THEN MinLotSize ELSE 0 END, OverflowQty = OverflowQty - MinLotSize
			from #tempPurchasePlanDet as det inner join (select Item, PurchaseFlow, MIN(WindowTime) as WindowTime from #tempPurchasePlanDet 
													where OverflowQty >= MinLotSize and MinLotSize > 0 and (MinLotSize = PurchaseQty or MinLotSize >= PurchaseQty * 2)
													group by Item, PurchaseFlow) as tmp 
													on det.Item = tmp.Item and det.PurchaseFlow = tmp.PurchaseFlow and det.WindowTime >= tmp.WindowTime

			set @LastOverflowCount = @CurrentOverflowCount
			select @CurrentOverflowCount = COUNT(1) from #tempPurchasePlanDet 
			where OverflowQty >= MinLotSize and MinLotSize > 0 and (MinLotSize = PurchaseQty or MinLotSize >= PurchaseQty * 2)
		end

		--�ڿۼ���װ
		set @LastOverflowCount = 0
		select @CurrentOverflowCount = COUNT(1) from #tempPurchasePlanDet 
		where OverflowQty >= UC and UC > 0 and PurchaseQty >= UC and ((MinLotSize > 0 and PurchaseQty >= (MinLotSize + UC)) or (MinLotSize is null) or (MinLotSize = 0))
		while @LastOverflowCount <> @CurrentOverflowCount
		begin
			update det set PurchaseQty = PurchaseQty - CASE WHEN det.WindowTime = tmp.WindowTime THEN UC ELSE 0 END, OverflowQty = OverflowQty - UC
			from #tempPurchasePlanDet as det inner join (select Item, PurchaseFlow, MIN(WindowTime) as WindowTime from #tempPurchasePlanDet 
													where OverflowQty >= UC and UC > 0 and PurchaseQty >= UC
													and ((MinLotSize > 0 and PurchaseQty >= (MinLotSize + UC)) or (MinLotSize is null) or (MinLotSize = 0)) 
													group by Item, PurchaseFlow) as tmp
													on det.Item = tmp.Item and det.PurchaseFlow = tmp.PurchaseFlow and det.WindowTime >= tmp.WindowTime

			set @LastOverflowCount = @CurrentOverflowCount
			select @CurrentOverflowCount = COUNT(1) from #tempPurchasePlanDet 
			where OverflowQty >= UC and UC > 0 and PurchaseQty >= UC
			and ((MinLotSize > 0 and PurchaseQty >= (MinLotSize + UC)) or (MinLotSize is null) or (MinLotSize = 0))
		end
		-----------------------------������������װԲ��-----------------------------

	end try
	begin catch
		set @Msg = N'������������ƻ��쳣��' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
		return
	end catch 

	begin try	
		if @trancount = 0
		begin
			begin tran
		end




		-----------------------------��������������ƻ�(�գ�-----------------------------
		--ɾ��δ�ͷŵ���������ƻ�
		delete from MRP_PurchasePlanIpDet where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanOpenOrder where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanDetTrace where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanInitLocationDet where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanDet where PurchasePlanId in(select Id from MRP_PurchasePlanMstr where Status = 'Create')
		delete from MRP_PurchasePlanMstr where Status = 'Create'

		--��ȡReleaseNo
		select @ReleaseNo = ISNULL(MAX(ReleaseNo), 0) + 1 from MRP_PurchasePlanMstr

		--������������ƻ�ͷ
		insert into MRP_PurchasePlanMstr(ReleaseNo, BatchNo, EffDate, [Status], CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		values(@ReleaseNo, @BatchNo, @DateNow, 'Create', @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1)

		--��ȡ��������ƻ�ͷId
		set @PurchasePlanId = @@Identity
		
		--������������ƻ��ڳ����
		insert into MRP_PurchasePlanInitLocationDet(PurchasePlanId, [Type], Item, InitStock, SafeStock, MaxStock, InTransitQty, InspectQty, CreateDate, CreateUser)
		select @PurchasePlanId, 'Daily', Item, Qty, SafeStock, MaxStock, InTransitQty, InspectQty, @DateTimeNow, @RunUser from #tempLocatoinDet	

		--�������˼ƻ���;���
		insert into MRP_PurchasePlanIpDet(PurchasePlanId, [Type], IpNo, Flow, Item, StartTime, WindowTime, Qty, CreateDate, CreateUser)
		select @PurchasePlanId, 'Daily', IpNo, Flow, Item, StartTime, WindowTime, Qty, @DateTimeNow, @RunUser from #tempIpDet

		--������������ƻ���ϸ
		insert into MRP_PurchasePlanDet(PurchasePlanId, [Type], UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgPurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom, UnitQty, UC, MinLotSize, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @PurchasePlanId, 'Daily', UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, 
		ReqQty, PurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom, UnitQty, UC, MinLotSize, StartTime, WindowTime, 
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempPurchasePlanDet

		--������������ƻ���ϸ׷�ݱ�
		insert into MRP_PurchasePlanDetTrace(PurchasePlanId, [Type], UUID, ProdItem, ProdQty, RateQty, ScrapPct, 
		BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, CreateDate, CreateUser)
		select @PurchasePlanId, 'Daily', UUID, ProdItem, ProdQty, RateQty, ScrapPct,
		BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, @DateTimeNow, @RunUser from #tempMaterialPlanDetTrace

		--����Open Order׷�ݱ�
		insert into MRP_PurchasePlanOpenOrder(PurchasePlanId, [Type], UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser)
		select @PurchasePlanId, 'Daily', UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, @DateTimeNow, @RunUser from #tempOpenOrder
		-----------------------------��������������ƻ�(�գ�-----------------------------



		-----------------------------��������������ƻ�(�ܣ�-----------------------------
		set datefirst 1  --������һΪһ�ܿ�ʼʱ��

		--�����ռƻ����ܼƻ���ӳ���
		insert into #tempWeeklyPurchasePlanDetMap(DailyRowId, DailyUUID, WeeklyUUID, WeeklyStartTime, WeeklyWindowTime, DailyWindowTime)
		select tmp.RowId, tmp.UUID, pl.UUID, DATEADD(DAY, -pl.LeadTime, tmp.WindowTime), tmp.WindowTime, tmp.OldWindowTime
		from #tempPurchasePlanDet as pl inner join (
		select DATEADD(DAY, -datepart(WEEKDAY, WindowTime) + 1, WindowTime) as WindowTime, PurchaseFlow, Item, RowId, UUID, WindowTime as OldWindowTime
		from #tempPurchasePlanDet) as tmp on 
		pl.PurchaseFlow = tmp.PurchaseFlow and pl.Item = tmp.Item and pl.WindowTime = tmp.WindowTime

		--������������ƻ���ϸ
		insert into MRP_PurchasePlanDet(PurchasePlanId, [Type], UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgPurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom, UnitQty, UC, MinLotSize, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @PurchasePlanId, 'Weekly', map.WeeklyUUID, pl.PurchaseFlow, pl.Item, pl.ItemDesc, pl.RefItemCode, 
		SUM(ISNULL(ReqQty, 0)), SUM(ISNULL(PurchaseQty, 0)), SUM(ISNULL(PurchaseQty, 0)), SUM(ISNULL(OrderQty, 0)), pl.Uom, pl.BaseUom, pl.UnitQty, pl.UC, pl.MinLotSize, map.WeeklyStartTime, map.WeeklyWindowTime,
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempPurchasePlanDet as pl inner join #tempWeeklyPurchasePlanDetMap as map on pl.RowId = map.DailyRowID
		where map.DailyWindowTime >= DATEADD(DAY, 14, @DateNow)
		group by map.WeeklyUUID, pl.PurchaseFlow, pl.Item, pl.ItemDesc, pl.RefItemCode, pl.Uom, pl.BaseUom, pl.UnitQty, pl.UC, pl.MinLotSize, map.WeeklyStartTime, map.WeeklyWindowTime

		--������������ƻ���ϸ׷�ݱ�
		insert into MRP_PurchasePlanDetTrace(PurchasePlanId, [Type], UUID, ProdItem, ProdQty, RateQty, ScrapPct, 
		BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, CreateDate, CreateUser)
		select @PurchasePlanId, 'Weekly', map.WeeklyUUID, tr.ProdItem, tr.ProdQty, tr.RateQty, tr.ScrapPct,
		tr.BomUnitQty, tr.PurchaseUnitQty, tr.BomUom, tr.PurchaseUom, tr.PlanDate, @DateTimeNow, @RunUser 
		from #tempMaterialPlanDetTrace as tr inner join #tempWeeklyPurchasePlanDetMap as map on tr.UUID = map.DailyUUID 
		where map.DailyWindowTime >= DATEADD(DAY, 14, @DateNow)

		--����Open Order׷�ݱ�
		insert into MRP_PurchasePlanOpenOrder(PurchasePlanId, [Type], UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser)
		select @PurchasePlanId, 'Weekly', map.WeeklyUUID, oo.Flow, oo.OrderNo, oo.Item, oo.StartTime, oo.WindowTime, oo.OrderQty, oo.ShipQty, oo.RecQty, @DateTimeNow, @RunUser 
		from #tempOpenOrder as oo inner join #tempWeeklyPurchasePlanDetMap as map on oo.UUID = map.DailyUUID
		where map.DailyWindowTime >= DATEADD(DAY, 14, @DateNow)
		-----------------------------��������������ƻ�(�ܣ�-----------------------------



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
       
		set @Msg = N'������������ƻ��쳣��' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


