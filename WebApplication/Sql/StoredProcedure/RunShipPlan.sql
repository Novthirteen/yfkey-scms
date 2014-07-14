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
	declare @MaxWindowTime datetime
	declare @RowId int
	declare @MaxRowId int
	declare @DistributionFlow varchar(50)
	declare @ShipPlanId int
	declare @ActiveQty decimal(18, 8)
	declare @LastActiveQty decimal(18, 8)
	declare @Flow varchar(50)
	declare @Item varchar(50)
	declare @StartTime datetime
	declare @LastOverflowCount int
	declare @CurrentOverflowCount int

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''
	set @trancount = @@trancount
	
	exec GetNextSequence 'RunShipPlan', @BatchNo output
	begin try
		create table #tempMsg
		(
			Lvl varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			LocFrom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			LocTo varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StartTime datetime,
			WindowTime datetime,
			Msg varchar(500) COLLATE  Chinese_PRC_CI_AS
		)

		create table #tempEffCustScheduleDet
		(
			RowId int identity(1, 1) Primary Key,
			Id int,
			MstrId int,
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ShipFlow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			ItemRef varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Qty decimal(18, 8),
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
			UC decimal(18, 8),
			Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempDistributionFlow
		(
			RowId int Identity(1, 1),
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
		)

		create table #tempShipFlowDet
		(
			RowId int Identity(1, 1),
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			LeadTime decimal(18 ,8),
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			RefItemCode varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UnitQty decimal(18, 8),
			UC decimal(18, 8),
			LocFrom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			LocTo varchar(50) COLLATE  Chinese_PRC_CI_AS,
			LocQty decimal(18, 8),
			InTransitQty decimal(18, 8),
			SafeStock decimal(18, 8),
			MaxStock decimal(18, 8),
			ActiveQty decimal(18, 8)
		)

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
		
		create table #tempShipPlanDet
		(
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS primary key, 
			DistributionFlow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			RefItemCode varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ReqQty decimal(18, 8),
			OrgShipQty decimal(18, 8),
			ShipQty decimal(18, 8),
			OverflowQty decimal(18, 8),
			OrderQty decimal(18, 8),
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
			UC decimal(18, 8),
			LocFrom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			LocTo varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_StartTime on #tempShipPlanDet(StartTime asc)

		create table #tempShipPlanDetTrace
		(
			RowId int identity(1, 1)  primary key,
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			DistributionFlow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ReqDate datetime,
			ReqQty decimal(18, 8)
		)

		create table #tempOpenOrder
		(
			RowId int identity(1, 1)  primary key,
			UUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			OrderNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			OrgStartTime datetime,
			OrgWindowTime datetime,
			StartTime datetime,
			WindowTime datetime,
			EffDate datetime,
			OrderQty decimal(18, 8),
			ShipQty decimal(18, 8),
			RecQty decimal(18, 8),
		)

		create table #tempWeeklyShipPlanDetMap
		(
			RowId int identity(1, 1) primary key,
			DailyUUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			WeeklyUUID varchar(50) COLLATE  Chinese_PRC_CI_AS, 
			WeeklyStartTime datetime,
			WeeklyWindowTime datetime,
			DailyStartTime datetime
		)

		--���¸��ؼƻ��ķ�����
		--update det set ShipQty = p.CurrenCumQty
		--from CustScheduleDet as det inner join EDI_FordPlan as p on det.FordPlanId = p.Id

		--���¿ͻ��ճ̿�ʼʱ��(�ռƻ���
		update CustScheduleDet set StartTime = DATEADD(day, -ISNULL(fMstr.MRPLeadTime, 0), det.DateFrom)
		from CustScheduleDet as det 
		inner join CustScheduleMstr as mstr on det.ScheduleId = mstr.Id
		inner join FlowMstr as fMstr on det.Flow = fMstr.Code
		where mstr.[Type] = 'Daily' and mstr.[Status] = 'Submit' and det.DateFrom >= @DateNow 

		-----------------------------����ȡ�ͻ��ճ�-----------------------------
		--ѡȡ��ʼ���ڴ��ڵ��ڵ�������пͻ��ճ�
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
			RAISERROR(N'û���ҵ���Ч�Ŀͻ��ճ̡�', 16, 1)
		end

		--�����ܼƻ���ֵ��ռƻ�
		exec SplitWeeklyCustSchedule

		--ȡ�����ճ̵�������ϸ�����ճ̵���ϸҪɾ���������ճ��ظ�����ϸ������ѭ���õ���Ч�ճ�
		--ԭ����ֹ�ͻ��������ճ�û�а�����������������������ֻ��ȡ���ճ��ϵ���ϸ������Ҫ��֤�����µ��ճ̲������ظ�
		insert into #tempDistributionFlow(Flow) select distinct Flow from #tempEffCustScheduleDet
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempDistributionFlow
		while @RowId <= @MaxRowId
		begin
			set @MaxMstrId = null
			set @MinWindowTime = null
			set @MaxWindowTime = null

			select @DistributionFlow = Flow from #tempDistributionFlow where RowId = @RowId
				
			select @MaxMstrId = MAX(MstrId) from #tempEffCustScheduleDet where Flow = @DistributionFlow
			select @MinWindowTime = MIN(WindowTime), @MaxWindowTime = MAX(WindowTime) from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId = @MaxMstrId
	
			while exists(select top 1 1 from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId < @MaxMstrId and WindowTime > @MinWindowTime)
			begin
				delete from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId < @MaxMstrId and WindowTime > @MinWindowTime
				if exists(select top 1 1 from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId < @MaxMstrId)
				begin --û�и�С�汾�Ŀͻ��ճ̣�����ѭ��
					break;
				end
				else
				begin --ȡ��һ���汾�Ŀͻ��ճ�
					select @MaxMstrId = MAX(MstrId) from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId < @MaxMstrId
					select @MinWindowTime = MIN(WindowTime) from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId = @MaxMstrId
				end
			end

			--����ܼƻ���ֵ��ռƻ�
			insert into #tempEffCustScheduleDet(Id, MstrId, Flow, ShipFlow, Item, ItemDesc, ItemRef, Qty, Uom, BaseUom, UC, Location, StartTime, WindowTime)
			select det.DetId, det.MstrId, det.Flow, det.ShipFlow, det.Item, det.ItemDesc, det.ItemRef, det.Qty, det.Uom, i.Uom, det.UC, det.Location, det.StartTime, det.WindowTime 
			from MRP_SplitWeeklyCustScheduleDet as det inner join Item as i on det.Item = i.Code
			where det.Flow = @DistributionFlow and det.WindowTime > @MaxWindowTime
			
			set @RowId = @RowId + 1
		end

		--���ܼƻ�����ռƻ���·�ߣ�û���ռƻ���
		insert into #tempEffCustScheduleDet(Id, MstrId, Flow, ShipFlow, Item, ItemDesc, ItemRef, Qty, Uom, BaseUom, UC, Location, StartTime, WindowTime)
		select det.DetId, det.MstrId, det.Flow, det.ShipFlow, det.Item, det.ItemDesc, det.ItemRef, det.Qty, det.Uom, i.Uom, det.UC, det.Location, det.StartTime, det.WindowTime 
		from MRP_SplitWeeklyCustScheduleDet as det inner join Item as i on det.Item = i.Code
		where det.WindowTime > @DateNow and det.Flow not in (select Flow from #tempDistributionFlow)

		--���㵥λ����
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

		--ɾ��û��ά����λ���������
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, LocFrom, StartTime, WindowTime, Msg)
		select 'Error', Flow, Item, Qty, Uom, Location, StartTime, WindowTime, 
		N'����·��[' + Flow + N']����[ ' + Item + N']û��ά����λ[ ' + Uom + N' ]�ͻ�����λ[' + BaseUom + N']�Ļ�����' 
		from #tempEffCustScheduleDet where UnitQty is null
		delete from #tempEffCustScheduleDet where UnitQty is null
		-----------------------------����ȡ�ͻ��ճ�-----------------------------



		-----------------------------����ȡ����·��-----------------------------
		--��ȡ����·�ߺͷ��˿�λ�Ŀ�棨�м�ֿ�棩
		insert into #tempShipFlowDet(Flow, LeadTime, Item, ItemDesc, RefItemCode, Uom, BaseUom, UC, LocFrom, LocTo, 
		LocQty, InTransitQty, SafeStock, MaxStock, ActiveQty)
		select det.Flow, ISNULL(mstr.MRPLeadTime, 0) as LeadTime, det.Item, i.Desc1, det.RefItemCode, det.Uom, i.Uom as BaseUom, det.UC, mstr.LocFrom, mstr.LocTo, 
		ISNULL(loc.Qty, 0), ISNULL(loc.InTransitQty, 0), ISNULL(det.SafeStock, 0), ISNULL(det.MaxStock, 0), (ISNULL(loc.Qty, 0) + ISNULL(loc.InTransitQty, 0) - ISNULL(det.SafeStock, 0))
		from FlowDet as det
		inner join FlowMstr as mstr on det.Flow = mstr.Code
		inner join Item as i on det.Item = i.Code
		left join MRP_LocationDetSnapShot as loc on det.Item = loc.Item and mstr.LocTo = loc.Location
		where Flow in (select ShipFlow from FlowMstr where [Type] = 'Distribution' and IsActive = 1 and ShipFlow is not null)

		--���㵥λ����
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
		
		--ɾ��û��ά����λ���������
		insert into #tempMsg(Lvl, Flow, Item, Uom, LocFrom, LocTo, Msg) 
		select 'Error', Flow, Item, Uom, LocFrom, LocTo, 
		N'����·��[' + Flow + N']����[ ' + Item + N']û��ά����λ[ ' + Uom + N']�ͻ�����λ[' + BaseUom + N']�Ļ�����' 
		from #tempShipFlowDet where UnitQty is null
		delete from #tempShipFlowDet where UnitQty is null

		--����·��û��ά������
		insert into #tempMsg(Lvl, Flow, Item, Uom, LocFrom, LocTo, Msg) 
		select 'Error', c.ShipFlow, c.Item, c.Uom, c.Location, null, 
		N'����[ ' + c.Item + N']�ڷ���·��[' + c.ShipFlow + N']�в�����' 
		from #tempEffCustScheduleDet as c left join #tempShipFlowDet as f on c.ShipFlow = f.Flow and c.Item = f.Item
		where f.RowId is null
		-----------------------------����ȡ����·��-----------------------------



		-----------------------------������Open Order-----------------------------
		--���뷢��·�ߵ�Open Order
		insert into #tempOpenOrder(Flow, OrderNo, Item, OrgStartTime, OrgWindowTime, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, DATEADD(day, fDet.LeadTime, ord.WindowTime), 
		CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, 
		CASE WHEN ord.StartTime < @DateNow THEN DATEADD(day, fdet.LeadTime, @DateNow) ELSE DATEADD(day, fdet.LeadTime, CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121))) END, 
		CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, 
		ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord
		inner join #tempShipFlowDet as fDet on ord.Flow = fDet.Flow and ord.Item = fDet.Item
		where ord.OrderQty > ord.ShipQty

		--��������·�ߵ�Order Order
		insert into #tempOpenOrder(Flow, OrderNo, Item, OrgStartTime, OrgWindowTime, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, 
		CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, 
		CASE WHEN ord.StartTime < @DateNow THEN DATEADD(day, ISNULL(fMstr.LeadTime, 0), @DateNow) ELSE DATEADD(day, ISNULL(fMstr.LeadTime, 0), CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121))) END, 
		CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, 
		ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord 
		inner join (select distinct Flow, Item from #tempEffCustScheduleDet where ShipFlow is null) as dFlow on ord.Flow = dFlow.Flow and ord.Item = dFlow.Item
		inner join FlowMstr as fMstr on ord.Flow = fMstr.Code
		-----------------------------������Open Order-----------------------------



		-----------------------------��������;ASN-----------------------------
		insert into #tempIpDet(IpNo, Flow, Item, StartTime, WindowTime, Qty)
		select det.IpNo, det.Flow, det.Item, det.StartTime, DATEADD(day, mstr.LeadTime, det.StartTime), SUM(det.Qty) as Qty
		from MRP_IpDetSnapShot as det inner join (select distinct Flow, LeadTime from #tempShipFlowDet) as mstr on det.Flow = mstr.Flow
		group by det.IpNo, det.Flow, det.Item, det.StartTime, mstr.LeadTime, det.WindowTime
		-----------------------------��������;-----------------------------




		-----------------------------�����㷢�˼ƻ�-----------------------------
		--ת�з���·�ߵģ�ë����
		insert into #tempShipPlanDet(UUID, DistributionFlow, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), req.Flow, flow.Flow, flow.Item, req.ItemDesc, req.ItemRef, req.Qty * req.UnitQty / flow.UnitQty, /*�Ȱѿͻ��ճ̵ĵ�λתΪ������λ����תΪ���˼ƻ��ĵ�λ*/
		req.Uom, req.BaseUom, req.UnitQty, flow.UC, flow.LocFrom, flow.LocTo, DATEADD(day, -ISNULL(flow.LeadTime, 0), StartTime), StartTime  --�ͻ��ճ̵Ŀ�ʼʱ����Ƿ��˼ƻ��Ĵ���ʱ��
		from #tempEffCustScheduleDet as req 
		inner join #tempShipFlowDet as flow on req.ShipFlow = flow.Flow and req.Item = flow.Item

		--ɾ����ʼʱ��С�ڽ���ļƻ�
		delete from #tempShipPlanDet where StartTime < @DateNow

		--תû�з���·�ߵģ�����·��ֱ�Ӿ��Ƿ���·��
		insert into #tempShipPlanDet(UUID, DistributionFlow, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), Flow, Flow, Item, ItemDesc, ItemRef, Qty, Uom, BaseUom, UnitQty, UC, Location, null, StartTime, WindowTime
		from #tempEffCustScheduleDet where ShipFlow is null or ShipFlow = ''

		--��¼�����м��
		insert into #tempShipPlanDetTrace(UUID, DistributionFlow, Item, ReqDate, ReqQty) 
		select UUID, DistributionFlow, Item, WindowTime, ShipQty from #tempShipPlanDet

		--�ϲ���ͬ����·��+���ϵ�����
		--�ϲ�ë������һ�У���СUUID)
		update p set ShipQty = t.ShipQty
		from #tempShipPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(ShipQty) as ShipQty from #tempShipPlanDet group by Flow, Item, StartTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--�ϲ������м���������м��UUIDȫ������Ϊ��СUUID
		update dt set UUID = t.MinUUID
		from #tempShipPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Flow, Item, StartTime from #tempShipPlanDet group by Flow, Item, StartTime having count(1) > 1) as t
		on p.Flow = t.Flow and p.Item = t.Item and p.StartTime = t.StartTime
		inner join #tempShipPlanDetTrace as dt on p.UUID = dt.UUID
		--ɾ���ظ�ë���� q
		delete p from #tempShipPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Flow, Item, StartTime from #tempShipPlanDet group by Flow, Item, StartTime having count(1) > 1) as t
		on p.Flow = t.Flow and p.Item = t.Item and p.StartTime = t.StartTime
		where p.UUID <> MinUUID

		--���ڰ�ȫ����תΪ����ķ��˼ƻ�
		update p set ShipQty = ShipQty - d.ActiveQty
		from #tempShipPlanDet as p 
		inner join #tempShipFlowDet as d on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0
		insert into #tempShipPlanDet(UUID, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), d.Flow, d.Item, d.ItemDesc, d.RefItemCode, -d.ActiveQty, d.Uom, d.BaseUom, d.UnitQty, d.UC, d.LocFrom, d.LocTo, @DateNow, DATEADD(day, d.LeadTime, @DateNow) 
		from #tempShipFlowDet as d 
		left join #tempShipPlanDet as p on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0 and p.Flow is null

		--�������οۼ���棨������;��棩
		set @RowId = null
		set @MaxRowId = null
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempShipFlowDet
		while (@RowId <= @MaxRowId)
		begin
			set @ActiveQty = 0
			set @LastActiveQty = 0
			set @Flow = null
			set @Item = null

			select @ActiveQty = ActiveQty, @Flow = Flow, @Item = Item from #tempShipFlowDet where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set ShipQty = CASE WHEN @ActiveQty >= ShipQty THEN 0 WHEN @ActiveQty < ShipQty and @ActiveQty>0 THEN ShipQty - @ActiveQty ELSE ShipQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = ShipQty
				from #tempShipPlanDet as det with(INDEX(IX_StartTime))
				where det.Flow = @Flow and det.Item = @Item
			end

			set @RowId = @RowId + 1
		end

		--���¶�����
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

		--���ݿ�ʼʱ�����οۼ�������
		set @RowId = null
		set @MaxRowId = null
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempOpenOrder
		while (@RowId <= @MaxRowId)
		begin
			set @ActiveQty = 0
			set @LastActiveQty = 0
			set @Flow = null
			set @Item = null
			set @StartTime = null

			select @ActiveQty = (OrderQty - ShipQty), @Flow = Flow, @Item = Item, @StartTime = StartTime from #tempOpenOrder where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set ShipQty = CASE WHEN @ActiveQty >= ShipQty THEN 0 WHEN @ActiveQty < ShipQty and @ActiveQty>0 THEN ShipQty - @ActiveQty ELSE ShipQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = ShipQty
				from #tempShipPlanDet as det with(INDEX(IX_StartTime))
				where det.Flow = @Flow and det.Item = @Item and det.StartTime >= @StartTime
			end

			set @RowId = @RowId + 1
		end

		--���ܷ�������
		update d set ReqQty = ISNULL(dt.ReqQty, 0) from #tempShipPlanDet as d 
		left join (select UUID, SUM(ISNULL(ReqQty, 0)) as ReqQty from #tempShipPlanDetTrace group by UUID) as dt on d.UUID = dt.UUID
		-----------------------------�����㷢�˼ƻ�-----------------------------




		-----------------------------������������װԲ��-----------------------------
		update #tempShipPlanDet set ShipQty = ceiling(ShipQty / UC) * UC, OrgShipQty = ShipQty where ShipQty > 0 and UC > 0

		update det set OverflowQty = tmp.OverflowQty
		from #tempShipPlanDet as det inner join
		(select det2.Flow, det2.Item, det2.StartTime, SUM(ISNULL(det1.ShipQty, 0) - ISNULL(det1.OrgShipQty, 0)) as OverflowQty
		from #tempShipPlanDet as det1 inner join #tempShipPlanDet as det2 on det1.Flow = det2.Flow and det1.Item = det2.Item
		where det1.StartTime <= det2.StartTime
		group by det2.Flow, det2.Item, det2.StartTime) as tmp on det.Flow = tmp.Flow and det.Item = tmp.Item and det.StartTime = tmp.StartTime
	
		
		set @LastOverflowCount = 0
		select @CurrentOverflowCount = COUNT(1) from #tempShipPlanDet where OverflowQty >= UC and UC > 0 and ShipQty >= UC
		while @LastOverflowCount <> @CurrentOverflowCount
		begin
			update det set ShipQty = ShipQty - CASE WHEN det.StartTime = tmp.StartTime THEN UC ELSE 0 END, OverflowQty = OverflowQty - UC
			from #tempShipPlanDet as det inner join (select Flow, Item, MIN(StartTime) as StartTime from #tempShipPlanDet 
													where OverflowQty >= UC and UC > 0 and ShipQty >= UC
													group by Flow, Item) as tmp 
													on det.Flow = tmp.Flow and det.Item = tmp.Item and det.StartTime >= tmp.StartTime

			set @LastOverflowCount = @CurrentOverflowCount
			select @CurrentOverflowCount = COUNT(1) from #tempShipPlanDet where OverflowQty >= UC and UC > 0 and ShipQty >= UC
		end
		-----------------------------������������װԲ��-----------------------------
	end try
	begin catch
		set @Msg = N'���з��˼ƻ��쳣��' + Error_Message()
		RAISERROR(@Msg, 16, 1)
		return
	end catch 

	begin try
		if @trancount = 0
		begin
            begin tran
        end

		-----------------------------�����ɷ��˼ƻ����գ�-----------------------------
		--ɾ��δ�ͷŵķ��˼ƻ�
		delete from MRP_ShipPlanIpDet where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanOpenOrder where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanInitLocationDet where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanDetTrace where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanDet where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanMstr where Status = 'Create'

		--��ȡReleaseNo
		select @ReleaseNo = ISNULL(MAX(ReleaseNo), 0) + 1 from MRP_ShipPlanMstr

		--�������˼ƻ�ͷ
		insert into MRP_ShipPlanMstr(ReleaseNo, BatchNo, EffDate, [Status], CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		values(@ReleaseNo, @BatchNo, @DateNow, 'Create', @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1)

		--��ȡ���˼ƻ�ͷId
		set @ShipPlanId = @@Identity
		
		--�������˼ƻ��ڳ����
		insert into MRP_ShipPlanInitLocationDet(ShipPlanId, [Type], Location, Item, InitStock, SafeStock, MaxStock, InTransitQty, CreateDate, CreateUser)
		select @ShipPlanId, 'Daily', LocTo, Item, LocQty, SafeStock, MaxStock, InTransitQty, @DateTimeNow, @RunUser from #tempShipFlowDet

		--�������˼ƻ���;���
		insert into MRP_ShipPlanIpDet(ShipPlanId, [Type], IpNo, Flow, Item, StartTime, WindowTime, Qty, CreateDate, CreateUser)
		select @ShipPlanId, 'Daily', IpNo, Flow, Item, StartTime, WindowTime, Qty, @DateTimeNow, @RunUser from #tempIpDet

		--�������˼ƻ���ϸ
		insert into MRP_ShipPlanDet(ShipPlanId, [Type], UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgShipQty, ShipQty, OrderQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @ShipPlanId, 'Daily', UUID, Flow, Item, ItemDesc, RefItemCode, 
		ISNULL(ReqQty, 0), ISNULL(ShipQty, 0), ISNULL(ShipQty, 0), ISNULL(OrderQty, 0), Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, 
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempShipPlanDet

		--�������˼ƻ���ϸ׷�ݱ�
		insert into MRP_ShipPlanDetTrace(ShipPlanId, [Type], UUID, DistributionFlow, Item, ReqDate, ReqQty, CreateDate, CreateUser)
		select @ShipPlanId, 'Daily', UUID, DistributionFlow, Item, ReqDate, ReqQty, @DateTimeNow, @RunUser from #tempShipPlanDetTrace

		--����Open Order׷�ݱ�
		insert into MRP_ShipPlanOpenOrder(ShipPlanId, [Type], UUID, Flow, OrderNo, Item, OrgStartTime, OrgWindowTime, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser)
		select @ShipPlanId, 'Daily', UUID, Flow, OrderNo, Item, OrgStartTime, OrgWindowTime, StartTime, WindowTime, OrderQty, ShipQty, RecQty, @DateTimeNow, @RunUser from #tempOpenOrder
		-----------------------------�����ɷ��˼ƻ����գ�-----------------------------



		-----------------------------�����ɷ��˼ƻ����ܣ�-----------------------------
		set datefirst 1  --������һΪһ�ܿ�ʼʱ��


		--�����ռƻ����ܼƻ���ӳ���
		insert into #tempWeeklyShipPlanDetMap(DailyUUID, WeeklyUUID, WeeklyStartTime, WeeklyWindowTime, DailyStartTime)
		select tmp.UUID, pl.UUID, tmp.StartTime, DATEADD(DAY, ISNULL(fd.LeadTime, 0), tmp.StartTime), tmp.OldStartTime
		from #tempShipPlanDet as pl 
		inner join (select DATEADD(DAY, -datepart(WEEKDAY, StartTime) + 1, StartTime) as StartTime, Flow, Item, UUID, StartTime as OldStartTime
					from #tempShipPlanDet) as tmp on 
					pl.Flow = tmp.Flow and pl.Item = tmp.Item and pl.StartTime = tmp.StartTime
		inner join #tempShipFlowDet as fd on pl.Flow = fd.Flow and pl.Item = fd.Item

		--�������˼ƻ���ϸ
		insert into MRP_ShipPlanDet(ShipPlanId, [Type], UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgShipQty, ShipQty, OrderQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @ShipPlanId, 'Weekly', map.WeeklyUUID, pl.Flow, pl.Item, pl.ItemDesc, pl.RefItemCode, 
		SUM(ISNULL(ReqQty, 0)), SUM(ISNULL(ShipQty, 0)), SUM(ISNULL(ShipQty, 0)), SUM(ISNULL(OrderQty, 0)), pl.Uom, pl.BaseUom, pl.UnitQty, pl.UC, pl.LocFrom, pl.LocTo, map.WeeklyStartTime,  map.WeeklyWindowTime, 
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempShipPlanDet as pl inner join #tempWeeklyShipPlanDetMap as map on pl.UUID = map.DailyUUID
		where map.DailyStartTime >= DATEADD(DAY, 14, @DateNow)
		group by map.WeeklyUUID, pl.Flow, pl.Item, pl.ItemDesc, pl.RefItemCode,  pl.Uom, pl.BaseUom, pl.UnitQty, pl.UC, pl.LocFrom, pl.LocTo, map.WeeklyStartTime,  map.WeeklyWindowTime

		--�������˼ƻ���ϸ׷�ݱ�
		insert into MRP_ShipPlanDetTrace(ShipPlanId, [Type], UUID, DistributionFlow, Item, ReqDate, ReqQty, CreateDate, CreateUser)
		select @ShipPlanId, 'Weekly', map.WeeklyUUID, tr.DistributionFlow, tr.Item, tr.ReqDate, tr.ReqQty, @DateTimeNow, @RunUser 
		from #tempShipPlanDetTrace as tr inner join #tempWeeklyShipPlanDetMap as map on tr.UUID = map.DailyUUID
		where map.DailyStartTime >= DATEADD(DAY, 14, @DateNow)

		--����Open Order׷�ݱ�
		insert into MRP_ShipPlanOpenOrder(ShipPlanId, [Type], UUID, Flow, OrderNo, Item, OrgStartTime, OrgWindowTime, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser)
		select @ShipPlanId, 'Weekly', map.WeeklyUUID, oo.Flow, oo.OrderNo, oo.Item, oo.OrgStartTime, oo.OrgWindowTime, oo.StartTime, oo.WindowTime, oo.OrderQty, oo.ShipQty, oo.RecQty, @DateTimeNow, @RunUser 
		from #tempOpenOrder as oo inner join #tempWeeklyShipPlanDetMap as map on oo.UUID = map.DailyUUID
		where map.DailyStartTime >= DATEADD(DAY, 14, @DateNow)
		-----------------------------�����ɷ��˼ƻ����ܣ�-----------------------------




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
       
		set @Msg = N'���з��˼ƻ��쳣��' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


