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
	declare @RowId int
	declare @MaxRowId int
	declare @ProdItem varchar(50)
	declare @Bom varchar(50)
	declare @EffDate datetime
	declare @BomQty decimal(18, 8)

	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''

	begin try
		create table #tempMsg
		(
			Lvl varchar(50),
			Phase varchar(50),
			Flow varchar(50),
			Item varchar(50),
			Msg varchar(500)
		)

		create table #tempProductionPlan
		(
			RowId int identity(1, 1) Primary Key,
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

		-----------------------------����ȡ�����ƻ�-----------------------------
		--ѡȡ��ʼ���ڴ��ڵ��ڵ�������пͻ��ճ�
		insert into #tempProductionPlan(Item, ItemDesc, RefItemCode, Qty,
		Uom, BaseUom, PlanDate)
		select det.Item, det.ItemDesc, det.RefItemCode, det.Qty,
		det.Uom, i.Uom as BaseUom, det.StartTime
		from MRP_WeeklyProductionPlan as det
		inner join Item as i on det.Item = i.Code

		if not exists(select top 1 1 from #tempProductionPlan)
		begin
			RAISERROR(N'û���ҵ���Ч�������ƻ���', 16, 1) 
			return			
		end

		--����ȫ��ת��Ϊ��1��ʼ
		set datefirst 1 --������1Ϊ1�ܿ�ʼʱ��
		update #tempProductionPlan set PlanDate = DATEADD(DAY, -DATEPART(WEEKDAY, PlanDate), PlanDate)

		--ɾ���Ų����ںͰ���ƻ��ظ���
		delete from #tempProductionPlan where PlanDate <= (select MAX(PlanDate) from MRP_ShiftPlanDet 
															where PlanId in (select Max(mstr.Id) from MRP_ShiftPlanDet as det
															inner join MRP_ShiftPlanMstr as mstr on det.PlanId = mstr.Id
															where mstr.[Status] = 'Submit'))

		--���㵥λ����
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

		--ɾ��û��ά����λ���������
		insert into #tempMsg(Lvl, Phase, Item, Msg) 
		select 'Error', 'Purchase', Item, N'��Ʒ[ ' + Item + N']û��ά����λ[ ' + Uom + N' ]�ͻ�����λ[' + BaseUom + N']�Ļ�����' 
		from #tempProductionPlan where UnitQty is null
		delete from #tempProductionPlan where UnitQty is null
		-----------------------------����ȡ�����ƻ�-----------------------------

	
		-----------------------------��չ��Bom-----------------------------
		--����Bom���ȴ����������ң��ڴ��������ң����ȡ���ϴ���
		update t set Bom = ISNULL(i.Bom, t.Item)
		from #tempProductionPlan as t 
		inner join Item as i on t.Item = i.Code

		--��¼��־û��Bom������
		insert into #tempMsg(Lvl, Phase, Item, Msg) 
		select 'Error', 'Production', t.Item,
		N'��Ʒ[ ' + t.Item + N']��Bom����[ ' + t.Bom + N']������' 
		from #tempProductionPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where b.Code is null
		--ɾ������ƻ�
		delete t from #tempProductionPlan as t
		left join BomMstr as b on t.Bom = b.Code
		where b.Code is null

		--��¼��־û��Bom��ϸ
		insert into #tempMsg(Lvl, Phase, Item, Msg) 
		select 'Error', 'Production', t.Item,
		N'��Ʒ[ ' + t.Item + N']��Bom����[ ' + t.Bom + N']û��Bom��ϸ' 
		from #tempProductionPlan as t
		left join BomDet as b on t.Bom = b.Bom
		where b.Bom is null
		--ɾ������ƻ�
		delete t from #tempProductionPlan as t
		left join BomDet as b on t.Bom = b.Bom
		where b.Bom is null

		--ѭ��չ��Bom
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
				insert into #tempMsg(Lvl, Phase, Item, Msg) 
				select 'Error', 'Production', Item,
				N'����[ ' + Item + N']û��ά����λ[ ' + Uom + N' ]�ͻ�����λ[' + BaseUom + N']�Ļ�����' 
				from #tempCurrentMaterialPlanDet where UnitQty is null
				delete from #tempCurrentMaterialPlanDet where UnitQty is null

				--��¼���ϼƻ���ʱ��
				insert into #tempMaterialPlanDet(UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime)
				select UUID, Item, ItemDesc, ReqQty, RateQty, ScrapPct, Uom, BaseUom, UnitQty, ReqTime from #tempCurrentMaterialPlanDet
			end
			set @RowId = @RowId + 1
		end

		--ɾ��������MRP���������
		delete pl from #tempMaterialPlanDet as pl inner join Item as i on pl.Item = i.Code where i.IsMRP <> 1
		-----------------------------��չ��Bom-----------------------------



		-----------------------------���ϲ��ɹ�����-----------------------------
		--ë�������ϲ���
		insert into  #tempMergeMaterialPlanDet(UUID, Item, ItemDesc, BaseReqQty, BasePurchaseQty, BaseUom, ReqTime)
		select UUID, Item, ItemDesc, ReqQty * UnitQty, ReqQty * UnitQty, BaseUom, ReqTime from #tempMaterialPlanDet

		--����ȫ��ת��Ϊ��1��ʼ
		set datefirst 1 --������1Ϊ1�ܿ�ʼʱ��
		update #tempMergeMaterialPlanDet set ReqTime = DATEADD(DAY, -DATEPART(WEEKDAY, ReqTime), ReqTime)

		--�ϲ�ë������һ�У���СUUID)
		update p set BaseReqQty = t.BaseReqQty, BasePurchaseQty = t.BaseReqQty
		from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(BaseReqQty) as BaseReqQty from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--ɾ���ظ�ë����
		delete p from #tempMergeMaterialPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, ReqTime from #tempMergeMaterialPlanDet group by Item, ReqTime having count(1) > 1) as t
		on p.Item = t.Item and p.ReqTime = t.ReqTime
		where p.UUID <> MinUUID
		-----------------------------���ϲ��ɹ�����-----------------------------



		-----------------------------�����Ҳɹ�·��-----------------------------
		insert into #tempPurchasePlanDet(UUID, PurchaseFlow, Item, ItemDesc, RefItemCode, BaseReqQty, BasePurchaseQty, Uom, BaseUom, UC, StartTime, WindowTime)
		select t.UUID, d.Flow, t.Item, t.ItemDesc, d.RefItemCode, t.BaseReqQty, t.BasePurchaseQty, d.Uom, t.BaseUom, d.UC, DATEADD(day, -ISNULL(m.LeadTime, 0), t.ReqTime), t.ReqTime
		from #tempMergeMaterialPlanDet as t
		inner join FlowDet as d on t.Item = d.Item
		inner join FlowMstr as m on d.Flow = m.Code
		where m.[Type] = 'Procurement' and m.IsMRP = 1 and m.IsActive = 1

		--��¼��־û���ҵ��ɹ�·��
		insert into #tempMsg(Lvl, Phase, Item, Msg) 
		select 'Error', 'Production', Item,
		N'����[ ' + Item + N']û���ҵ��ɹ�·��' 
		from #tempPurchasePlanDet where PurchaseFlow is null
		--ɾ����������
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
	
		--ɾ��û��ά����λ���������
		insert into #tempMsg(Lvl, Phase, Flow, Item, Msg) 
		select 'Error', 'Production', PurchaseFlow, Item,
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

		drop table #tempMsg
	end try
	begin catch
		set @Msg = N'���вɹ��ƻ��쳣��' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
		return
	end catch 
END 


