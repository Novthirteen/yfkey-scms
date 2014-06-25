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
	declare @RowId int
	declare @MaxRowId int
	declare @DistributionFlow varchar(50)
	declare @ShipPlanId int

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''
	set @trancount = @@trancount
	
	exec GetNextSequence 'RunShipPlan', @BatchNo output
	begin try
		create table #tempMsg
		(
			Lvl varchar(50),
			Flow varchar(50),
			Item varchar(50),
			Qty decimal(18, 8),
			Uom varchar(5),
			LocFrom varchar(50),
			LocTo varchar(50),
			StartTime datetime,
			WindowTime datetime,
			Msg varchar(500)
		)

		create table #tempEffCustScheduleDet
		(
			Id int Primary Key,
			MstrId int,
			Flow varchar(50),
			ShipFlow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			ItemRef varchar(50),
			Qty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
			UC decimal(18, 8),
			Location varchar(50),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempDistributionFlow
		(
			RowId int Identity(1, 1),
			Flow varchar(50),
		)

		create table #tempShipFlowDet
		(
			RowId int Identity(1, 1),
			Flow varchar(50),
			LeadTime decimal(18 ,8),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),
			UC decimal(18, 8),
			LocFrom varchar(50),
			LocTo varchar(50),
			LocQty decimal(18, 8),
			InTransitQty decimal(18, 8),
			SafeStock decimal(18, 8),
			MaxStock decimal(18, 8),
			ActiveQty decimal(18, 8)
		)
		
		create table #tempShipPlanDet
		(
			UUID varchar(50) primary key, 
			DistributionFlow varchar(50),
			Flow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			ReqQty decimal(18, 8),
			ShipQty decimal(18, 8),
			OrderQty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
			UC decimal(18, 8),
			LocFrom varchar(50),
			LocTo varchar(50),
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_WindowTime on #tempShipPlanDet(WindowTime asc)

		create table #tempShipPlanDetTrace
		(
			RowId int identity(1, 1)  primary key,
			UUID varchar(50), 
			DistributionFlow varchar(50),
			Item varchar(50),
			ReqDate datetime,
			ReqQty decimal(18, 8)
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

		--���¸��ؼƻ��ķ�����
		--update det set ShipQty = p.CurrenCumQty
		--from CustScheduleDet as det inner join EDI_FordPlan as p on det.FordPlanId = p.Id

		--���¿ͻ��ճ̿�ʼʱ��
		update CustScheduleDet set StartTime = DATEADD(day, -ISNULL(fMstr.MRPLeadTime, 0), det.DateFrom)
		from CustScheduleDet as det 
		inner join CustScheduleMstr as mstr on det.ScheduleId = mstr.Id
		inner join FlowMstr as fMstr on det.Flow = fMstr.Code
		where  mstr.[Type] = 'Daily' and mstr.[Status] = 'Submit' and det.DateFrom >= @DateNow 


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

		--ȡ�����ճ̵�������ϸ�����ճ̵���ϸҪɾ���������ճ��ظ�����ϸ������ѭ���õ���Ч�ճ�
		--ԭ����ֹ�ͻ��������ճ�û�а�����������������������ֻ��ȡ���ճ��ϵ���ϸ������Ҫ��֤�����µ��ճ̲������ظ�
		insert into #tempDistributionFlow(Flow) select distinct Flow from #tempEffCustScheduleDet
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempDistributionFlow
		while @RowId <= @MaxRowId
		begin
			select @DistributionFlow = Flow from #tempDistributionFlow where RowId = @RowId

			select @MaxMstrId = MAX(MstrId) from #tempEffCustScheduleDet where Flow = @DistributionFlow
			select @MinWindowTime = MIN(WindowTime) from #tempEffCustScheduleDet where Flow = @DistributionFlow and MstrId = @MaxMstrId
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

			set @RowId = @RowId + 1
		end

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
		ISNULL(loc.Qty, 0), ISNULL(loc.InTransitQty, 0), ISNULL(det.SafeStock, 0), ISNULL(det.MaxStock, 0), (ISNULL(loc.Qty, 0) - ISNULL(det.SafeStock, 0))
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
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord
		inner join #tempShipFlowDet as fDet on ord.Flow = fDet.Flow and ord.Item = fDet.Item

		--��������·�ߵ�Order Order
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord 
		inner join (select distinct Flow, Item from #tempEffCustScheduleDet where ShipFlow is null) as dFlow on ord.Flow = dFlow.Flow and ord.Item = dFlow.Item
		-----------------------------������Open Order-----------------------------

		

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
		from #tempEffCustScheduleDet where ShipFlow is null

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
		from #tempShipPlanDet as p inner join #tempShipFlowDet as d on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0
		insert into #tempShipPlanDet(UUID, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), d.Flow, d.Item, d.ItemDesc, d.RefItemCode, -d.ActiveQty, d.Uom, d.BaseUom, d.UnitQty, d.UC, d.LocFrom, d.LocTo, @DateNow, DATEADD(day, d.LeadTime, @DateNow) 
		from #tempShipFlowDet as d left join #tempShipPlanDet as p on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0 and p.Flow is null

		--���ݿ�ʼʱ�����οۼ���棨������;��棩
		set @RowId = null
		set @MaxRowId = null
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempShipFlowDet
		while (@RowId <= @MaxRowId)
		begin
			declare @ActiveQty decimal(18, 8)
			declare @LastActiveQty decimal(18, 8)
			declare @Flow varchar(50)
			declare @Item varchar(50)
			set @ActiveQty = 0
			set @LastActiveQty = 0
			set @Flow = null
			set @Item = null

			select @ActiveQty = ActiveQty, @Flow = Flow, @Item = Item from #tempShipFlowDet where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set ShipQty = CASE WHEN @ActiveQty >= ShipQty THEN 0 WHEN @ActiveQty < ShipQty and @ActiveQty>0 THEN ShipQty - @ActiveQty ELSE ShipQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = ShipQty
				from #tempShipPlanDet as det with(INDEX(IX_WindowTime))
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

		--���ܷ�������
		update d set ReqQty = ISNULL(dt.ReqQty, 0) from #tempShipPlanDet as d 
		left join (select UUID, SUM(ISNULL(ReqQty, 0)) as ReqQty from #tempShipPlanDetTrace group by UUID) as dt on d.UUID = dt.UUID
		-----------------------------�����㷢�˼ƻ�-----------------------------
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

		-----------------------------�����ɷ��˼ƻ�-----------------------------
		--ɾ��δ�ͷŵķ��˼ƻ�
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
		insert into MRP_ShipPlanInitLocationDet(ShipPlanId, Location, Item, InitStock, SafeStock, MaxStock, InTransitQty, CreateDate, CreateUser)
		select @ShipPlanId, LocTo, Item, LocQty, SafeStock, MaxStock, InTransitQty, @DateTimeNow, @RunUser from #tempShipFlowDet

		--����������װԲ��
		update #tempShipPlanDet set ShipQty = ceiling(ShipQty / UC) * UC where ShipQty > 0 and UC > 0

		--�������˼ƻ���ϸ
		insert into MRP_ShipPlanDet(ShipPlanId, UUID, Flow, Item, ItemDesc, RefItemCode, 
		ReqQty, OrgShipQty, ShipQty, OrderQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, 
		CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @ShipPlanId, UUID, Flow, Item, ItemDesc, RefItemCode, 
		ISNULL(ReqQty, 0), ISNULL(ShipQty, 0), ISNULL(ShipQty, 0), ISNULL(OrderQty, 0), Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, 
		@DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempShipPlanDet

		--�������˼ƻ���ϸ׷�ݱ�
		insert into MRP_ShipPlanDetTrace(ShipPlanId, UUID, DistributionFlow, Item, ReqDate, ReqQty, CreateDate, CreateUser)
		select @ShipPlanId, UUID, DistributionFlow, Item, ReqDate, ReqQty, @DateTimeNow, @RunUser from #tempShipPlanDetTrace

		--����Open Order׷�ݱ�
		insert into MRP_ShipPlanOpenOrder(ShipPlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser)
		select @ShipPlanId, UUID, Flow, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, @DateTimeNow, @RunUser from #tempOpenOrder
		-----------------------------�����ɷ��˼ƻ�-----------------------------

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


