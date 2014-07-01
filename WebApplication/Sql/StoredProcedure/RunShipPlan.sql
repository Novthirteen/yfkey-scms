SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunShipPlan') 
     DROP PROCEDURE RunShipPlan
GO

CREATE PROCEDURE [dbo].RunShipPlan
(
	@RunUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateTimeNow datetime = GetDate()
	declare @DateNow datetime = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow))
	declare @Msg nvarchar(MAX) = ''
	declare @trancount int = @@trancount
	declare @ReleaseNo int
	declare @BatchNo int
	declare @MaxMstrId int
	declare @MinWindowTime datetime
	declare @RowId int
	declare @MaxRowId int
	declare @DistributionFlow varchar(50)
	
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
			ShipQty decimal(18, 8),
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
			ActiveQty decimal(18, 8)
		)

		create table #tempShipPlan
		(
			Flow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			ShipQty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = ������λ����
			UC decimal(18, 8),
			LocFrom varchar(50),
			LocTo varchar(50),
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_WindowTime on #tempShipPlan(WindowTime asc)

		--���¸��ؼƻ��ķ�����
		update det set ShipQty = p.CurrenCumQty
		from CustScheduleDet as det inner join EDI_FordPlan as p on det.FordPlanId = p.Id



		-----------------------------����ȡ�ͻ��ճ�-----------------------------
		--ѡȡ��ʼ���ڴ��ڵ��ڵ�������пͻ��ճ�
		insert into #tempEffCustScheduleDet(Id, MstrId, Flow, ShipFlow, Item, ItemDesc, ItemRef, Qty, ShipQty,
		Uom, BaseUom, UC, Location, StartTime, WindowTime)
		select det.Id, mstr.Id as MstrId, mstr.Flow, mstr.ShipFlow, det.Item, det.ItemDesc, det.ItemRef, det.Qty, ISNULL(det.ShipQty, 0) as ShipQty,
		det.Uom, i.Uom as BaseUom, det.UC, det.Loc as Location, det.StartTime, det.DateFrom as WindowTime
		from CustScheduleDet as det inner join CustScheduleMstr as mstr on det.ScheduleId = mstr.Id
		inner join Item as i on det.Item = i.Code
		where mstr.[Type] = 'Daily' and mstr.[Status] = 'Submit' and det.StartTime >= @DateNow 
		and (det.Qty > det.ShipQty or (det.ShipQty is null and det.Qty > 0))

		if not exists(select top 1 1 from #tempEffCustScheduleDet)
		begin
			insert into #tempMsg(Lvl, Msg) values('Info', N'û���ҵ���Ч�Ŀͻ��ճ�')
			return			
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
		from #tempEffCustScheduleDet as det inner join UomConv as c on det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null and c.Item is null

		--ɾ��û��ά����λ���������
		insert into #tempMsg(Lvl, Flow, Item, Qty, Uom, LocFrom, StartTime, WindowTime, Msg) 
		select 'Error', Flow, Item, Qty, Uom, Location, StartTime, WindowTime, N'����·�ߵ�����û��ά����λ����' from #tempEffCustScheduleDet where UnitQty is null
		delete from #tempEffCustScheduleDet where UnitQty is null
		-----------------------------����ȡ�ͻ��ճ�-----------------------------



		-----------------------------����ȡ����·��-----------------------------
		--��ȡ����·�ߺͷ��˿�λ�Ŀ�棨�м�ֿ�棩
		insert into #tempShipFlowDet(Flow, LeadTime, Item, ItemDesc, RefItemCode, Uom, BaseUom, UC, LocFrom, LocTo, LocQty, SafeStock)
		select det.Flow, ISNULL(mstr.MRPLeadTime, 0) as LeadTime, det.Item, i.Desc1, det.RefItemCode, det.Uom, i.Uom as BaseUom, det.UC, mstr.LocFrom, mstr.LocTo, ISNULL(loc.Qty, 0), ISNULL(det.SafeStock, 0) 
		from FlowDet as det
		inner join FlowMstr as mstr on det.Flow = mstr.Code
		inner join Item as i on det.Item = i.Code
		left join LocationDet as loc on det.Item = loc.Item and mstr.LocTo = loc.Location
		where Flow in (select ShipFlow from FlowMstr where [Type] = 'Distribution' and IsActive = 1 and ShipFlow is not null)

		--������;����
		update #tempShipFlowDet set InTransitQty = ISNULL(t.InTransitQty, 0), ActiveQty = LocQty + ISNULL(t.InTransitQty, 0) - SafeStock
		from #tempShipFlowDet as det left join (select oMstr.Flow, oDet.Item, SUM(iDet.Qty - ISNULL(iDet.RecQty, 0)) as InTransitQty from IpDet as iDet
												inner join IpMstr as iMstr on iDet.IpNo = iMstr.IpNo
												inner join OrderLocTrans as oTrans on iDet.OrderLocTransId = oTrans.Id
												inner join OrderDet as oDet on oTrans.OrderDetId = oDet.Id
												inner join OrderMstr as oMstr on oDet.OrderNo = oMstr.OrderNo
												where oMstr.flow in (select distinct Flow from #tempShipFlowDet)
												and iMstr.[Status] = 'Create' and oMstr.SubType = 'Nml'
												group by oMstr.Flow, oDet.Item) as t on det.Flow = t.Flow and det.Item = t.Item

		--���㵥λ����
		update #tempShipFlowDet set UnitQty = 1 where Uom = BaseUom
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null and c.Item is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempShipFlowDet as det inner join UomConv as c on det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null and c.Item is null
		
		--ɾ��û��ά����λ���������
		insert into #tempMsg(Lvl, Flow, Item, Uom, LocFrom, LocTo, Msg) 
		select 'Error', Flow, Item, Uom, LocFrom, LocTo, N'����·�ߵ�����û��ά����λ����' from #tempShipFlowDet where UnitQty is null
		delete from #tempShipFlowDet where UnitQty is null

		--����·��û��ά������
		insert into #tempMsg(Lvl, Flow, Item, Uom, LocFrom, LocTo, Msg) 
		select 'Error', c.ShipFlow, c.Item, c.Uom, c.Location, null, N'�����ڷ���·���в�����' 
		from #tempEffCustScheduleDet as c left join #tempShipFlowDet as f on c.ShipFlow = f.Flow and c.Item = f.Item
		where f.RowId is null
		-----------------------------����ȡ����·��-----------------------------



		-----------------------------�����㷢�˼ƻ�-----------------------------
		--ת�з���·�ߵģ�ë����
		insert into #tempShipPlan(Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select flow.Flow, flow.Item, req.ItemDesc, flow.RefItemCode, (req.Qty - req.ShipQty) * req.UnitQty / flow.UnitQty, --�Ȱѿͻ��ճ̵ĵ�λתΪ������λ����תΪ���˼ƻ��ĵ�λ
		req.Uom, req.BaseUom, req.UnitQty, flow.UC, flow.LocFrom, flow.LocTo, DATEADD(day, -flow.LeadTime, StartTime), StartTime  --�ͻ��ճ̵Ŀ�ʼʱ����Ƿ��˼ƻ��Ĵ���ʱ��
		from #tempEffCustScheduleDet as req inner join #tempShipFlowDet as flow on req.ShipFlow = flow.Flow and req.Item = flow.Item

		--���ݿ�ʼʱ�����οۼ���棨����;��棬��������;���ĵ���ʱ�䣩
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempShipFlowDet
		while (@RowId <= @MaxRowId)
		begin
			declare @ActiveQty decimal(18, 8)
			declare @LastActiveQty decimal(18, 8)
			declare @Flow varchar(50)
			declare @Item varchar(50)

			select @ActiveQty = ActiveQty, @Flow = Flow, @Item = Item from #tempShipFlowDet where RowId = @RowId
			if (@ActiveQty > 0)
			begin
				update det set ShipQty = CASE WHEN @ActiveQty >= ShipQty THEN 0 WHEN @ActiveQty < ShipQty and @ActiveQty>0 THEN ShipQty - @ActiveQty ELSE ShipQty END,
				@ActiveQty = @ActiveQty - @LastActiveQty, @LastActiveQty = ShipQty
				from #tempShipPlan as det with(INDEX(IX_WindowTime))
				where det.Flow = @Flow and det.Item = @Item
			end

			set @RowId = @RowId + 1
		end

		--תû�з���·�ߵģ�����·��ֱ�Ӿ��Ƿ���·��
		insert into #tempShipPlan(Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select Flow, Item, ItemDesc, ItemRef, (Qty - ShipQty), Uom, BaseUom, UnitQty, UC, Location, null, StartTime, WindowTime
		from #tempEffCustScheduleDet where ShipFlow is null

		--���ڰ�ȫ����תΪ����ķ��˼ƻ�
		update #tempShipPlan set ShipQty -= d.ActiveQty
		from #tempShipPlan as p inner join #tempShipFlowDet as d on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0
		insert into #tempShipPlan(Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select d.Flow, d.Item, d.ItemDesc, d.RefItemCode, -d.ActiveQty, d.Uom, d.BaseUom, d.UnitQty, d.UC, d.LocFrom, d.LocTo, @DateNow, DATEADD(day, d.LeadTime, @DateNow) 
		from #tempShipFlowDet as d left join #tempShipPlan as p on p.Flow = d.Flow and p.Item = d.Item and p.StartTime = @DateNow
		where d.ActiveQty < 0 and p.Flow is null

		--����С�ڽ������ȫ��תΪ����
		update b set ShipQty = b.ShipQty + a.ShipQty
		from #tempShipPlan as a inner join #tempShipPlan as b on a.Flow = b.Flow and a.ITem = b.Item
		where b.StartTime = @DateNow and a.StartTime < @DateNow
		insert into #tempShipPlan(Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select a.Flow, a.Item, a.ItemDesc, a.RefItemCode, a.ShipQty, a.Uom, a.BaseUom, a.UnitQty, a.UC, a.LocFrom, a.LocTo, @DateNow, DATEADD(day, d.LeadTime, @DateNow) 
		from #tempShipPlan as a left join #tempShipPlan as b on a.Flow = b.Flow and a.ITem = b.Item and b.StartTime = @DateNow
		inner join #tempShipFlowDet as d on a.Flow = d.Flow and a.Item = d.Item
		where a.StartTime < @DateNow and b.Flow is null
		-----------------------------�����㷢�˼ƻ�-----------------------------

		if @trancount = 0
		begin
            begin tran
        end

		-----------------------------�����ɷ��˼ƻ�-----------------------------
		--ɾ��δ�ͷŵķ��˼ƻ�
		delete from MRP_ShipPlanDet where ShipPlanId in(select Id from MRP_ShipPlanMstr where Status = 'Create')
		delete from MRP_ShipPlanMstr where Status = 'Create'

		--��ȡReleaseNo
		select @ReleaseNo = ISNULL(MAX(ReleaseNo), 0) + 1 from MRP_ShipPlanMstr

		--�������˼ƻ�ͷ
		insert into MRP_ShipPlanMstr(ReleaseNo, BatchNo, EffDate, [Status], CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		values(@ReleaseNo, @BatchNo, @DateNow, 'Create', @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1)

		--�������˼ƻ���ϸ
		insert into MRP_ShipPlanDet(ShipPlanId, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @@Identity, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime, @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempShipPlan where ShipQty > 0
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
       
		set @Msg = N'���з��˼ƻ��쳣' + Error_Message()
		insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, @DateNow, 'Error', @Msg, @DateTimeNow, @RunUser)
		RAISERROR(@Msg, 16, 1) 

	end catch 
END 


