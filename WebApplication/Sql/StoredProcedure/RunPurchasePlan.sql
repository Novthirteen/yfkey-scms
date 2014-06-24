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
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
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
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
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
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
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
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
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

		-----------------------------����ȡ����ƻ�-----------------------------
		--ѡȡ��ʼ���ڴ��ڵ��ڵ�������пͻ��ճ�
		insert into #tempEffShiftPlan(DetId, MstrId, RefPlanNo, ProdLine, Item, ItemDesc, RefItemCode, Qty,
		Uom, BaseUom, PlanDate)
		select det.Id, mstr.Id as MstrId, mstr.RefPlanNo, mstr.ProdLine, det.Item, det.ItemDesc, det.RefItemCode, det.Qty,
		det.Uom, i.Uom as BaseUom, det.PlanDate
		from MRP_ShiftPlanDet as det inner join MRP_ShiftPlanMstr as mstr on det.PlanId = mstr.Id
		inner join Item as i on det.Item = i.Code
		where mstr.[Status] = 'Submit' and det.PlanDate >= @DateNow

		if not exists(select top 1 1 from #tempEffShiftPlan)
		begin
			RAISERROR(N'û���ҵ���Ч�İ���ƻ���', 16, 1) 
			return			
		end

		--ȡ���°���ƻ���������ϸ���ɰ���ƻ�����ϸҪɾ�������°���ƻ��ظ�����ϸ������ѭ���õ���Ч����ƻ�
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
				begin --û�и�С�汾�Ŀͻ��ճ̣�����ѭ��
					break;
				end
				else
				begin --ȡ��һ���汾�Ŀͻ��ճ�
					select @MaxMstrId = MAX(MstrId) from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId < @MaxMstrId
					select @MinPlandate = MIN(Plandate) from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId = @MaxMstrId
				end
			end

			set @RowId = @RowId + 1
		end

		--���㵥λ����
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

		--ɾ��û��ά����λ���������
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', ProdLine, Item, Qty, Uom, PlanDate, 
		N'������[' + ProdLine + N']��Ʒ[ ' + Item + N']û��ά����λ[ ' + Uom + N' ]�ͻ�����λ[' + BaseUom + N']�Ļ�����' 
		from #tempEffShiftPlan where UnitQty is null
		delete from #tempEffShiftPlan where UnitQty is null
		-----------------------------����ȡ����ƻ�-----------------------------
		


		-----------------------------��չ��Bom-----------------------------
		--����Bom���ȴ����������ң��ڴ��������ң����ȡ���ϴ���
		update t set Bom = ISNULL(d.Bom, ISNULL(i.Bom, t.Item))
		from #tempEffShiftPlan as t 
		left join FlowDet as d on t.ProdLine = d.Flow and t.Item = d.Item
		inner join Item as i on t.Item = i.Code

		--��¼��־������û��ά����Ʒ
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Msg) 
		select 'Warning', t.ProdLine, t.Item, t.Qty, t.Uom, t.PlanDate, 
		N'������[' + t.ProdLine + N']û��ά����Ʒ[ ' + t.Item + N']' 
		from #tempEffShiftPlan as t
		left join FlowDet as d on t.ProdLine = d.Flow and t.Item = d.Item
		where d.Id is null

		--��¼��־û��Bom������
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Bom, Msg) 
		select 'Error', t.ProdLine, t.Item, t.Qty, t.Uom, t.PlanDate, t.Bom, 
		N'������[' + t.ProdLine + N']��Ʒ[ ' + t.Item + N']��Bom����[ ' + t.Bom + N']������' 
		from #tempEffShiftPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where t.ProdLine = @ProdLine and b.Code is null
		--ɾ������ƻ�
		delete t from #tempEffShiftPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where b.Code is null

		--��¼��־û��Bom��ϸ
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, PlanDate, Bom, Msg) 
		select 'Error', t.ProdLine, t.Item, t.Qty, t.Uom, t.PlanDate, t.Bom, 
		N'������[' + t.ProdLine + N']��Ʒ[ ' + t.Item + N']��Bom����[ ' + t.Bom + N']û��Bom��ϸ' 
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

				--��¼���ϼƻ���ʱ��
				insert into #tempMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime)
				select UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime from #tempCurrentMaterialPlanDet
				--��¼׷�ݹ�ϵ
				insert into #tempMaterialPlanDetTrace(UUID, DetId, MstrId, RefPlanNo, ProdLine, ProdItem, ProdQty, RateQty, ScrapPct, BomUnitQty, BomUom, PlanDate)
				select UUID, @DetId, @MstrId, @RefPlanNo, @ProdLine, @ProdItem, @BomQty, RateQty, ScrapPct, UnitQty, Uom, @EffDate 
				from #tempCurrentMaterialPlanDet 
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



		-----------------------------����ȡ���ÿ��-----------------------------
		insert into #tempLocatoinDet(Item, SafeStock, MaxStock, Qty, InTransitQty, InSpectQty, ActiveQty)
		select Item, ISNULL(i.SafeStock, 0), ISNULL(i.MaxStock, 0), Qty, PurchaseInTransitQty, InSpectQty, Qty + InspectQty - ISNULL(i.SafeStock, 0)
		from 
		(
		select loc.Item, SUM(loc.Qty) as Qty, SUM(loc.InTransitQty) as InTransitQty, SUM(loc.PurchaseInTransitQty) as PurchaseInTransitQty, SUM(loc.InspectQty) as InspectQty
		from MRP_LocationDetSnapShot as loc
		inner join (select distinct Item from #tempMaterialPlanDet) as p on loc.Item = p.Item
		group by loc.Item
		) as a inner join Item as i on a.Item = i.Code 
		-----------------------------����ȡ���ÿ��-----------------------------



		-----------------------------���ϲ��ɹ�����-----------------------------
		--ë�������ϲ���
		insert into  #tempMergeMaterialPlanDet(UUID, Item, ItemDesc, BaseReqQty, BasePurchaseQty, BaseUom, ReqTime)
		select UUID, Item, ItemDesc, ReqQty * UnitQty, ReqQty * UnitQty, BaseUom, ReqTime from #tempMaterialPlanDet
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
				from #tempMergeMaterialPlanDet as det with(INDEX(IX_WindowTime))
				where det.Item = @Item
			end
			
			set @RowId = @RowId + 1
		end		
		-----------------------------�����㾻����-----------------------------



		-----------------------------�����Ҳɹ�·��-----------------------------
		insert into #tempPurchasePlanDet(UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, BaseReqQty, BasePurchaseQty, Uom, BaseUom, UC, StartTime, WindowTime)
		select t.UUID, d.Flow, t.Item, t.ItemDesc, d.RefItemCode, t.BaseReqQty, t.BasePurchaseQty, d.Uom, t.BaseUom, d.UC, DATEADD(day, -ISNULL(m.LeadTime, 0), t.ReqTime), t.ReqTime
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


		-----------------------------�����¶�����-----------------------------
		--���Ҷ�����
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, CASE WHEN ord.WindowTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.WindowTime, 121)) END, ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord
		inner join (select distinct PurchaseFlow, Item from #tempPurchasePlanDet) as pl on ord.Flow = pl.PurchaseFlow and ord.Item = pl.Item

		--���¶�����
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

		--���¶����������ϵ
		update ord set UUID = pl.UUID
		from #tempOpenOrder as ord inner join #tempPurchasePlanDet as pl on pl.PurchaseFlow = ord.Flow and pl.Item = ord.Item and pl.WindowTime = ord.EffDate
		-----------------------------�����¶�����-----------------------------

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

		-----------------------------��������������ƻ�-----------------------------
		--ɾ��δ�ͷŵ���������ƻ�
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
		insert into MRP_PurchasePlanInitLocationDet(PurchasePlanId, Item, InitStock, SafeStock, MaxStock, InTransitQty, InspectQty, CreateDate, CreateUser)
		select @PurchasePlanId, Item, Qty, SafeStock, MaxStock, InTransitQty, InspectQty, @DateTimeNow, @RunUser from #tempLocatoinDet

		--����������װԲ��
		update #tempPurchasePlanDet set PurchaseQty = ceiling(PurchaseQty / UC) * UC where PurchaseQty > 0 and UC > 0

		--������������ƻ���ϸ
		insert into MRP_PurchasePlanDet(PurchasePlanId, UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgPurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @PurchasePlanId, UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, 
		ReqQty, PurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, 
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempPurchasePlanDet

		--������������ƻ���ϸ׷�ݱ�
		insert into MRP_PurchasePlanDetTrace(PurchasePlanId, UUID, ShiftPlanDetId, ShiftPlanMstrId, RefPlanNo, ProdLine, ProdItem, ProdQty, RateQty, ScrapPct, 
		BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, CreateDate, CreateUser)
		select @PurchasePlanId, UUID, DetId, MstrId, RefPlanNo, ProdLine, ProdItem, ProdQty, RateQty, ScrapPct,
		BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, @DateTimeNow, @RunUser from #tempMaterialPlanDetTrace

		--����Open Order׷�ݱ�
		insert into MRP_PurchasePlanOpenOrder(PurchasePlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser)
		select @PurchasePlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, @DateTimeNow, @RunUser from #tempOpenOrder
		-----------------------------��������������ƻ�-----------------------------

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


