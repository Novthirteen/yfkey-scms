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

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''
	set @trancount = @@trancount

	exec GetNextSequence 'RunProductionPlan', @BatchNo output
	begin try
		create table #tempMsg
		(
			Lvl varchar(50),
			Item varchar(50),
			Bom varchar(50),
			EffDate datetime,
			Msg varchar(500)
		)

		create table #tempCurrentLevlProductPlan
		(
			UUID varchar(50) primary key,
			GroupId int,
			GroupSeq int,
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			Uom varchar(5),
			Qty decimal(18, 8),
			Bom varchar(50),
			StartTime datetime,
			WindowTime datetime
		)

		create index IX_WindowTime on #tempCurrentLevlProductPlan(WindowTime asc)

		create table #tempProductPlanDetTrace
		(
			UUID varchar(50),
			Flow varchar(50),
			Item varchar(50),
			Bom varchar(50),
			ReqDate datetime,
			ReqQty decimal(18, 8),
			RateQty decimal(18, 8), 
			ScrapPct decimal(18, 8),
			Uom varchar(5),
			UnitQty decimal(18, 8)
		)

		create table #tempNextLevlProductPlan
		(
			UUID varchar(50) primary key,
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			Uom varchar(5),
			Qty decimal(18, 8),
			Bom varchar(50),
			RateQty decimal(18, 8), 
			ScrapPct decimal(18, 8),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempProductPlanDet
		(
			UUID varchar(50) primary key,
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			Uom varchar(5),
			Qty decimal(18, 8),
			Bom varchar(50),
			OrderQty decimal(18, 8),
			StartTime datetime,
			WindowTime datetime
		)

		create table #tempLocatoinDet
		(
			RowId int identity(1, 1) primary key,
			Item varchar(50),
			Qty decimal(18, 8),
			InspectQty decimal(18, 8), 
			InTransitQty decimal(18, 8), 
			SafeStock decimal(18, 8),
			RemainQty decimal(18, 8)
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

		select @ShipPlanReleaseNo = MAX(ReleaseNo) from MRP_ShipPlanMstr where [Status] = 'Submit'

		if (@ShipPlanReleaseNo is null)
		begin
			RAISERROR(N'û���ҵ��ͷŵķ��˼ƻ���', 16, 1)
		end

		if not exists(select top 1 1 from Location where IsFG = 1)
		begin
			RAISERROR(N'û���ҵ���Ʒ��λ��', 16, 1) 
		end


		-----------------------------����ȡ����ë����-----------------------------
		insert into #tempCurrentLevlProductPlan(UUID, GroupId, GroupSeq, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
		select NEWID(), DENSE_RANK() over(Order by det.Item), DENSE_RANK() over(Partition by Item order by det.StartTime),
		det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, SUM(det.ShipQty * (1 + i.ScrapPct / 100) * det.UnitQty) as Qty, ISNULL(i.Bom, det.Item),
		DATEADD(day, -ISNULL(i.LeadTime, 0), det.StartTime) as StartTime, det.StartTime as WindowTime
		from MRP_ShipPlanDet as det 
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Item as i on det.Item = i.Code
		where mstr.ReleaseNo = @ShipPlanReleaseNo
		group by det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, det.StartTime, i.Bom, i.LeadTime

		--ɾ����ʼʱ��С�ڽ���ļƻ�
		delete from #tempCurrentLevlProductPlan where StartTime < @DateNow

		--��¼����׷�ݱ�
		insert into #tempProductPlanDetTrace(UUID, Flow, Item, ReqDate, ReqQty, ScrapPct, Uom, UnitQty)
		select t.UUID, det.Flow, det.Item, det.StartTime, det.ShipQty, i.ScrapPct, det.Uom, det.UnitQty
		from MRP_ShipPlanDet as det
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Item as i on det.Item = i.Code
		inner join #tempCurrentLevlProductPlan as t on t.Item = det.Item and t.WindowTime = det.StartTime
		where mstr.ReleaseNo = @ShipPlanReleaseNo

		--ɾ����ʼ����С�ڽ��������
		delete from #tempCurrentLevlProductPlan where StartTime < @DateNow
		-----------------------------����ȡ����ë����-----------------------------



		-----------------------------��ѭ���ֽ�Bom������²������ƻ�-----------------------------
		declare @ExpandLevel int
		set @ExpandLevel = 1
		while exists(select top 1 1 from #tempCurrentLevlProductPlan)
		begin
			-----------------------------��������ÿ��-----------------------------
			insert into #tempLocatoinDet(Item, Qty, InspectQty, InTransitQty, SafeStock, RemainQty)
			select p.Item, ISNULL(a.Qty, 0), ISNULL(a.InspectQty, 0), ISNULL(a.InTransitQty, 0), ISNULL(i.SafeStock, 0),
			ISNULL(a.Qty, 0) + ISNULL(a.InspectQty, 0) + ISNULL(a.InTransitQty, 0) - ISNULL(i.SafeStock, 0)
			from (select distinct Item from #tempCurrentLevlProductPlan) as p
			inner join Item as i on p.Item = i.Code 
			left join (select loc.Item, SUM(loc.Qty) as Qty, SUM(loc.InspectQty) as InspectQty, SUM(loc.InTransitQty) as InTransitQty
						from MRP_LocationDetSnapShot as loc
						inner join Location as l on loc.Location = l.Code
						inner join (select distinct Item from #tempCurrentLevlProductPlan) as p on loc.Item = p.Item
						where l.IsFG = 1  --ȡ��Ʒ��λ���
						group by loc.Item
						) as a on p.Item = a.Item
			left join #tempLocatoinDet as t on p.Item = t.Item
			where t.Item is null  --�����Ѿ���ӵ��б��еĿ��
			-----------------------------��������ÿ��-----------------------------



			-----------------------------�����㾻����-----------------------------
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
			-----------------------------�����㾻����-----------------------------



			-----------------------------���ֽ�Bom�������²�Bom�Ƿ��ǰ��Ʒ-----------------------------
			set @GroupId = null
			set @MaxGroupId = null
			select @GroupId = MIN(GroupId), @MaxGroupId = MAX(GroupId) from #tempCurrentLevlProductPlan
			while (@GroupId <= @MaxGroupId)
			begin  --���������ѭ��
				set @Item = null
				set @Bom = null
				select top 1 @Item = Item, @Bom = Bom from #tempCurrentLevlProductPlan where GroupId = @GroupId

				if not exists(select top 1 1 from BomMstr where Code = @Bom and IsActive = 1)
				begin   --û���ҵ�Bom������
					insert into #tempMsg(Lvl, Item, Bom, Msg) values('Error', @Item, @Bom, N'����[' + @Item + N']��Bom[' + @Bom + N']�����ݲ����ڻ���ͣ��')
					delete from #tempCurrentLevlProductPlan where Item = @Bom
					set @GroupId = @GroupId + 1
					continue
				end

				set @GroupSeq = null
				set @MaxGroupSeq = null
				select @GroupSeq = MIN(GroupSeq), @MaxGroupSeq = MAX(GroupSeq) from #tempCurrentLevlProductPlan where GroupId = @GroupId
				while @GroupSeq <= @MaxGroupSeq
				begin  --����������ѭ��
					if exists(select top 1 1 from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq and Qty > 0)
					begin
						set @EffDate = null
						set @BomQty = null
						select @EffDate = StartTime, @BomQty = Qty from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq

						truncate table #tempBomDetail
						insert into #tempBomDetail exec GetFlatBomDetail @Bom, @EffDate

						if not exists(select top 1 1 from #tempBomDetail)
						begin --չ����û��Bom��ϸ
							insert into #tempMsg(Lvl, Item, Bom, EffDate, Msg) values('Error', @Item, @Bom, @EffDate, N'����[' + @Item + N']Bom[' + @Bom + N']��������[' + convert(varchar(10), @EffDate, 121) + ']��Bom��ϸ������')
							delete from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq
							set @GroupSeq = @GroupSeq + 1
							continue
						end

						--����²��ǰ��Ʒ�������²������ƻ�
						insert into #tempNextLevlProductPlan(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, 
						RateQty, ScrapPct, StartTime, WindowTime)
						select NEWID(), bom.Item, i.Desc1, null, bom.Uom, @BomQty * bom.RateQty * (1 + bom.ScrapPct / 100), 
						bom.RateQty, bom.ScrapPct, ISNULL(i.Bom, bom.Item), DATEADD(day, -ISNULL(i.LeadTime, 0), @EffDate), @EffDate
						from #tempBomDetail as bom 
						inner join Item as i on bom.Item = i.Code
						inner join BomMstr as bm on (i.Bom is not null and bm.Code = i.Bom) or (i.Bom is null and bm.Code = bom.Item)
						where bm.IsActive = 1

						--ɾ����ʼ����С�ڽ��������
						delete from #tempNextLevlProductPlan where StartTime < @DateNow

						--��������׷�ݱ�
						insert into #tempProductPlanDetTrace(UUID, Item, ReqDate, ReqQty, Bom, RateQty, ScrapPct, Uom)
						select UUID, @Item, @Effdate, @BomQty, @Bom, RateQty, ScrapPct, Uom from #tempNextLevlProductPlan
					end

					set @GroupSeq = @GroupSeq + 1 
				end
			
				set @GroupId = @GroupId + 1
			end
			-----------------------------���ֽ�Bom�������²�Bom�Ƿ��ǰ��Ʒ-----------------------------


			--��ӱ��������ƻ�
			insert into #tempProductPlanDet(UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
			select UUID, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime from #tempCurrentLevlProductPlan

			--ɾ�����������ƻ�
			truncate table #tempCurrentLevlProductPlan

			--����²��а��Ʒ�����ƻ�����ӵ����������ƻ��н����´�ѭ��
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
				RAISERROR(N'��Ʒ�ֽⳬ��99�Σ�������ѭ����', 16, 1) 
			end
			else
			begin
				set @ExpandLevel = @ExpandLevel + 1
			end
		end
		-----------------------------��ѭ���ֽ�Bom������²������ƻ�-----------------------------



		-----------------------------���ϲ������ƻ�-----------------------------
		--�ϲ������ƻ���һ�У���СUUID)
		update p set Qty = t.Qty
		from #tempProductPlanDet as p inner join
		(select MIN(UUID) as MinUUID, SUM(Qty) as Qty from #tempProductPlanDet group by Item, StartTime having count(1) > 1) as t
		on p.UUID = t.MinUUID
		--�ϲ������м���������м��UUIDȫ������Ϊ��СUUID
		update dt set UUID = t.MinUUID
		from #tempProductPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, StartTime from #tempProductPlanDet group by Item, StartTime having count(1) > 1) as t
		on p.Item = t.Item and p.StartTime = t.StartTime
		inner join #tempProductPlanDetTrace as dt on p.UUID = dt.UUID
		--ɾ���ظ�ë����
		delete p from #tempProductPlanDet as p inner join
		(select MIN(UUID) as MinUUID, Item, StartTime from #tempProductPlanDet group by Item, StartTime having count(1) > 1) as t
		on p.Item = t.Item and p.StartTime = t.StartTime
		where p.UUID <> MinUUID
		-----------------------------���ϲ������ƻ�-----------------------------



		-----------------------------������Open Order-----------------------------
		--���뷢��·�ߵ�Open Order
		insert into #tempOpenOrder(Flow, OrderNo, Item, StartTime, WindowTime, EffDate, OrderQty, ShipQty, RecQty)
		select ord.Flow, ord.OrderNo, ord.Item, ord.StartTime, ord.WindowTime, CASE WHEN ord.StartTime < @DateNow THEN @DateNow ELSE CONVERT(datetime, CONVERT(varchar(10), ord.StartTime, 121)) END, ord.OrderQty, ord.ShipQty, ord.RecQty
		from MRP_OpenOrderSnapShot as ord
		inner join (select distinct Item from #tempProductPlanDet) as pl on ord.Item = pl.Item
		-----------------------------������Open Order-----------------------------
	end try
	begin catch
		set @Msg = N'�����������ƻ��쳣��' + Error_Message()
		RAISERROR(@Msg, 16, 1)
		return
	end catch 

	begin try
		if @trancount = 0
		begin
			begin tran
		end

		-----------------------------�������������ƻ�-----------------------------
		--ɾ��δ�ͷŵķ��˼ƻ�
		delete from MRP_ProductionPlanInitLocationDet where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo)
		delete from MRP_ProductionPlanDetTrace where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo)
		delete from MRP_ProductionPlanDet where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo)
		delete from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo

		--�����������ƻ�ͷ
		insert into MRP_ProductionPlanMstr(ReleaseNo, BatchNo, EffDate, CreateDate, CreateUser)
		values(@ShipPlanReleaseNo, @BatchNo, @DateNow, @DateTimeNow, @RunUser)

		--�����������ƻ���ϸ
		insert into MRP_ProductionPlanDet(ProductionPlanId, Item, ItemDesc, RefItemCode, OrgQty, Qty, Uom, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @@Identity, Item, ItemDesc, RefItemCode, Qty, Qty, Uom, StartTime, WindowTime, @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempProductPlanDet
		-----------------------------�������������ƻ�-----------------------------

		--��¼��־
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
       
		set @Msg = N'�����������ƻ��쳣' + Error_Message()
		insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, @DateNow, 'Error', @Msg, @DateTimeNow, @RunUser)
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


