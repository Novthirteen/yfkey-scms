SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunWeeklyShipPlan') 
     DROP PROCEDURE RunWeeklyShipPlan
GO

CREATE PROCEDURE [dbo].RunWeeklyShipPlan
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
	declare @MaxMstrId int
	declare @MinWindowTime datetime
	declare @RowId int
	declare @MaxRowId int
	declare @DistributionFlow varchar(50)

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

		create table #tempShipPlanDet
		(
			UUID varchar(50) primary key, 
			DistributionFlow varchar(50),
			Flow varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			BaseReqQty decimal(18, 8),
			ReqQty decimal(18, 8),
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

		--���¿ͻ��ճ̿�ʼʱ��
		update CustScheduleDet set StartTime = DATEADD(day, -ISNULL(fMstr.MRPLeadTime, 0), det.DateFrom)
		from CustScheduleDet as det 
		inner join CustScheduleMstr as mstr on det.ScheduleId = mstr.Id
		inner join FlowMstr as fMstr on det.Flow = fMstr.Code
		where  mstr.[Type] = 'Weekly' and mstr.[Status] = 'Submit' and det.DateFrom >= @DateNow 

		-----------------------------����ȡ�ͻ��ճ�-----------------------------
		--ѡȡ��ʼ���ڴ��ڵ��ڵ�������пͻ��ճ�
		insert into #tempEffCustScheduleDet(Id, MstrId, Flow, ShipFlow, Item, ItemDesc, ItemRef, Qty,
		Uom, BaseUom, UC, Location, StartTime, WindowTime)
		select det.Id, mstr.Id as MstrId, mstr.Flow, mstr.ShipFlow, det.Item, det.ItemDesc, det.ItemRef, det.Qty,
		det.Uom, i.Uom as BaseUom, det.UC, det.Loc as Location, det.StartTime, det.DateFrom as WindowTime
		from CustScheduleDet as det 
		inner join CustScheduleMstr as mstr on det.ScheduleId = mstr.Id
		inner join Item as i on det.Item = i.Code
		where mstr.[Type] = 'Weekly' and mstr.[Status] = 'Submit' and det.StartTime >= @DateNow 

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
		insert into #tempMsg(Lvl, Phase, Flow, Item, Msg)
		select 'Error', 'ShipPlan', Flow, Item, N'����·��[' + Flow + N']����[ ' + Item + N']û��ά����λ[ ' + Uom + N' ]�ͻ�����λ[' + BaseUom + N']�Ļ�����' 
		from #tempEffCustScheduleDet where UnitQty is null
		delete from #tempEffCustScheduleDet where UnitQty is null
		-----------------------------����ȡ�ͻ��ճ�-----------------------------



		-----------------------------�����㷢�˼ƻ�-----------------------------
		--��ȡ����·�ߺͷ��˿�λ�Ŀ�棨�м�ֿ�棩
		insert into #tempShipPlanDet(UUID, DistributionFlow, Flow, Item, ItemDesc, RefItemCode, BaseReqQty, Uom, BaseUom, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), req.Flow, mstr.Code, req.Item, req.ItemDesc, req.ItemRef, req.Qty * req.UnitQty, --������λ
		det.Uom, i.Uom, det.UC, mstr.LocFrom, mstr.LocTo, DATEADD(day, -ISNULL(mstr.MRPLeadTime, 0), req.StartTime), req.StartTime  --�ͻ��ճ̵Ŀ�ʼʱ����Ƿ��˼ƻ��Ĵ���ʱ��
		from #tempEffCustScheduleDet as req 
		inner join FlowDet as det on req.ShipFlow = det.Flow and req.Item = det.Item
		inner join FlowMstr as mstr on det.Flow = mstr.Code
		inner join Item as i on det.Item = i.Code
		where mstr.Code in (select ShipFlow from FlowMstr where [Type] = 'Distribution' and IsActive = 1 and ShipFlow is not null)

		--ɾ����ʼʱ��С�ڽ���ļƻ�
		delete from #tempShipPlanDet where StartTime < @DateNow

		--תû�з���·�ߵģ�����·��ֱ�Ӿ��Ƿ���·��
		insert into #tempShipPlanDet(UUID, DistributionFlow, Flow, Item, ItemDesc, RefItemCode, BaseReqQty, ReqQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select NEWID(), Flow, Flow, Item, ItemDesc, ItemRef, Qty * UnitQty, Qty, Uom, BaseUom, UnitQty, UC, Location, null, StartTime, WindowTime
		from #tempEffCustScheduleDet
		where ShipFlow is null

		--���㵥λ����
		update #tempShipPlanDet set UnitQty = 1, ReqQty = BaseReqQty where Uom = BaseUom 
		update det set UnitQty = c.BaseQty / c.AltQty, ReqQty = BaseReqQty / (c.BaseQty / c.AltQty)
		from #tempShipPlanDet as det inner join UomConv as c on det.Item = c.Item  and det.Uom = c.AltUom  and det.BaseUom = c.BaseUom 
		where det.UnitQty is null
		update det set UnitQty =  c.AltQty / c.BaseQty, ReqQty = BaseReqQty / (c.AltQty / c.BaseQty)
		from #tempShipPlanDet as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
		where det.UnitQty is null
		update det set UnitQty = c.BaseQty / c.AltQty, ReqQty = BaseReqQty / (c.BaseQty / c.AltQty)
		from #tempShipPlanDet as det inner join UomConv as c on det.Uom = c.AltUom  and det.BaseUom = c.BaseUom 
		where det.UnitQty is null and c.Item is null
		update det set UnitQty =  c.AltQty / c.BaseQty, ReqQty = BaseReqQty / (c.AltQty / c.BaseQty)
		from #tempShipPlanDet as det inner join UomConv as c on det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
		where det.UnitQty is null and c.Item is null

		--ɾ��û��ά����λ���������
		insert into #tempMsg(Lvl, Phase, Flow, Item, Msg) 
		select 'Error', 'ShipPlan', Flow, Item, N'����·��[' + Flow + N']����[ ' + Item + N']û��ά����λ[ ' + Uom + N']�ͻ�����λ[' + BaseUom + N']�Ļ�����' 
		from #tempShipPlanDet where UnitQty is null
		delete from #tempShipPlanDet where UnitQty is null

		--����·��û��ά������
		insert into #tempMsg(Lvl, Phase, Flow, Item, Msg) 
		select 'Error', 'ShipPlan', c.ShipFlow, c.Item, N'����[ ' + c.Item + N']�ڷ���·��[' + c.ShipFlow + N']�в�����' 
		from #tempEffCustScheduleDet as c left join #tempShipPlanDet as f on c.ShipFlow = f.Flow and c.Item = f.Item
		where f.UUID is null

		--�ϲ���ͬ����·��+���ϵ�����
		--�ϲ�ë������һ�У���СUUID)
		update p set BaseReqQty = t.BaseReqQty, ReqQty = t.ReqQty
		from #tempShipPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(BaseReqQty) as BaseReqQty, SUM(ReqQty) as ReqQty from #tempShipPlanDet group by Flow, Item, StartTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--ɾ���ظ�ë���� q
		delete p from #tempShipPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Flow, Item, StartTime from #tempShipPlanDet group by Flow, Item, StartTime having count(1) > 1) as t
		on p.Flow = t.Flow and p.Item = t.Item and p.StartTime = t.StartTime
		where p.UUID <> MinUUID

		insert into MRP_WeeklyShipPlan(UUID, Flow, Item, ItemDesc, RefItemCode, ReqQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime)
		select UUID, Flow, Item, ItemDesc, RefItemCode, ReqQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo, StartTime, WindowTime from #tempShipPlanDet
		-----------------------------�����㷢�˼ƻ�-----------------------------


		--��¼��־
		insert into MRP_RunWeeklyMRPLog(BatchNo, Lvl, Phase, Flow, Item, Msg, CreateDate, CreateUser)
		select @BatchNo, Lvl, Phase, Flow, Item, Msg, @DateTimeNow, @RunUser from #tempMsg

		drop table #tempMsg
		drop table #tempEffCustScheduleDet
		drop table #tempDistributionFlow
		drop table #tempShipPlanDet
	end try
	begin catch
		set @Msg = N'���з��˼ƻ��쳣��' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
		return
	end catch 
END 


