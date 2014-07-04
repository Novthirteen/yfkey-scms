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
	@Plant varchar(50),
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
	declare @Qty decimal(18, 8)
	declare @LastQty decimal(18, 8)
	declare @Effdate datetime

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

		create table #tempNextLevlProductPlan
		(
			RowId int identity(1, 1) primary key,
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

		create table #tempProductPlan
		(
			RowId int identity(1, 1) primary key,
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			Uom varchar(5),
			Qty decimal(18, 8),
			Bom varchar(50),
			StartTime datetime,
			WindowTime datetime
		)


		create table #tempLocatoinDet
		(
			RowId int identity(1, 1) primary key,
			Item varchar(50),
			Qty decimal(18, 8)
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

		select @ShipPlanReleaseNo = MAX(ReleaseNo) from MRP_ShipPlanMstr where [Status] = 'Submit'

		if (@ShipPlanReleaseNo is null)
		begin
			set @Msg = N'û���ҵ��ͷŵķ��˼ƻ�'
			--insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, @DateNow, 'Error', @Msg, @DateTimeNow, @RunUser)
			RAISERROR(@Msg, 16, 1) 
		end


		-----------------------------����ȡ����ë����-----------------------------
		insert into #tempCurrentLevlProductPlan(GroupId, GroupSeq, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
		select DENSE_RANK() over(Order by det.Item), DENSE_RANK() over(Partition by Item order by det.StartTime),
		det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, SUM(det.ShipQty * det.UnitQty) as Qty, ISNULL(i.Bom, det.Item),
		DATEADD(day, -ISNULL(i.LeadTime, 0), det.StartTime) as StartTime, det.StartTime as WindowTime
		from MRP_ShipPlanDet as det 
		inner join MRP_ShipPlanMstr as mstr on det.ShipPlanId = mstr.Id
		inner join Location as l on det.LocFrom = l.Code
		inner join Region as r on l.Region = r.Code
		inner join Item as i on det.Item = i.Code
		where mstr.ReleaseNo = @ShipPlanReleaseNo and r.Plant = @Plant
		group by det.Item, det.ItemDesc, det.RefItemCode, det.BaseUom, det.StartTime, i.Bom, i.LeadTime

		--ɾ����ʼ����С�ڽ��������
		delete from #tempCurrentLevlProductPlan where StartTime < @DateNow
		-----------------------------����ȡ����ë����-----------------------------



		-----------------------------��ѭ���ֽ�Bom������²������ƻ�-----------------------------
		declare @ExpandLevel int
		set @ExpandLevel = 1
		while exists(select top 1 1 from #tempCurrentLevlProductPlan)
		begin
			-----------------------------��������ÿ��-----------------------------
			insert into #tempLocatoinDet(Item, Qty)
			select Item, SUM(Qty) - MAX(ISNULL(i.SafeStock, 0)) as ActiveQty
			from 
			(
			select loc.Item, (loc.Qty + loc.InspectQty + loc.InTransitQty) as Qty  
			from MRP_LocationDetSnapShot as loc
			inner join (select distinct Item from #tempCurrentLevlProductPlan) as p on loc.Item = p.Item
			where loc.Plant = @Plant
			) as a inner join Item as i on a.Item = i.Code 
			group by Item
			-----------------------------��������ÿ��-----------------------------



			-----------------------------�����㾻����-----------------------------
			set @RowId = null
			set @MaxRowId = null
			select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempLocatoinDet
			while (@RowId <= @MaxRowId)
			begin
				set @Item = null
				set @Qty = null
				set @LastQty = null

				select @Qty = Qty, @Item = Item from #tempLocatoinDet where RowId = @RowId
				if (@Qty > 0)
				begin
					update det set Qty = CASE WHEN @Qty >= Qty THEN 0 WHEN @Qty < Qty and @Qty > 0 THEN Qty - @Qty ELSE Qty END,
					@Qty = @Qty - @LastQty, @LastQty = Qty
					from #tempCurrentLevlProductPlan as det with(INDEX(IX_WindowTime))
					where det.Item = @Item
				end
			
				set @RowId = @RowId + 1
			end

			--����С�ڽ������ȫ��תΪ����
			--�н��������
			update b set Qty = b.Qty + a.Qty
			from #tempCurrentLevlProductPlan as a inner join #tempCurrentLevlProductPlan as b on a.Item = b.Item
			where b.StartTime = @DateNow and a.StartTime < @DateNow	
			update a set Qty = 0
			from #tempCurrentLevlProductPlan as a inner join #tempCurrentLevlProductPlan as b on a.Item = b.Item
			where b.StartTime = @DateNow and a.StartTime < @DateNow	
			--û�н��������
			update a set StartTime = @DateNow, WindowTime = DATEADD(day, ISNULL(i.LeadTime, 0), @DateNow) 
			from #tempCurrentLevlProductPlan as a left join #tempCurrentLevlProductPlan as b on a.ITem = b.Item and b.StartTime = @DateNow
			inner join Item as i on a.Item = i.Code
			where a.StartTime < @DateNow and b.Item is null
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
					insert into #tempMsg(Lvl, Item, Bom, Msg) values('Error', @Item, @Bom, N'û��Bom������')
					delete from #tempCurrentLevlProductPlan where Item = @Bom
					set @GroupId = @GroupId + 1
					continue
				end

				set @GroupSeq = null
				set @MaxGroupSeq = null
				select @GroupSeq = MIN(GroupSeq), @MaxGroupSeq = MAX(GroupSeq) from #tempCurrentLevlProductPlan where GroupId = @GroupId
				while @GroupSeq <= @MaxGroupSeq
				begin  --ѭ��
					if exists(select top 1 1 from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq and Qty > 0)
					begin
						set @EffDate = null
						set @Qty = null
						select @EffDate = StartTime, @Qty = Qty from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq

						truncate table #tempBomDetail
						insert into #tempBomDetail exec GetFlatBomDetail @Bom, @EffDate

						if not exists(select top 1 1 from #tempBomDetail)
						begin --չ����û��Bom��ϸ
							insert into #tempMsg(Lvl, Item, Bom, EffDate, Msg) values('Error', @Item, @Bom, @EffDate, N'û��Bom��ϸ')
							delete from #tempCurrentLevlProductPlan where GroupId = @GroupId and GroupSeq = @GroupSeq
							set @GroupSeq = @GroupSeq + 1
							continue
						end

						--����²��ǰ��Ʒ�������²������ƻ�
						insert into #tempNextLevlProductPlan(Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
						select bom.Item, i.Desc1, null, bom.Uom, @Qty * (bom.RateQty + bom.ScrapPct / 100), ISNULL(i.Bom, bom.Item), DATEADD(day, -ISNULL(i.LeadTime, 0), @EffDate), @EffDate
						from #tempBomDetail as bom 
						inner join Item as i on bom.Item = i.Code
						inner join BomMstr as bm on (i.Bom is not null and bm.Code = i.Bom) or (i.Bom is null and bm.Code = bom.Item)
						where bm.IsActive = 1

						--ɾ����ʼ����С�ڽ��������
						delete from #tempNextLevlProductPlan where StartTime < @DateNow
					end

					set @GroupSeq = @GroupSeq + 1 
				end
			
				set @GroupId = @GroupId + 1
			end
			-----------------------------���ֽ�Bom�������²�Bom�Ƿ��ǰ��Ʒ-----------------------------


			--��ӱ��������ƻ�
			insert into #tempProductPlan(Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
			select Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime from #tempCurrentLevlProductPlan
			where Qty > 0

			--ɾ�����������ƻ�
			truncate table #tempCurrentLevlProductPlan

			--����²��а��Ʒ�����ƻ�����ӵ����������ƻ��н����´�ѭ��
			if exists(select top 1 1 from #tempNextLevlProductPlan)
			begin
				insert into #tempCurrentLevlProductPlan(GroupId, GroupSeq, Item, ItemDesc, RefItemCode, Uom, Qty, Bom, StartTime, WindowTime)
				select DENSE_RANK() over(Order by Item), DENSE_RANK() over(Partition by Item order by StartTime),
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
	end try
	begin catch
		set @Msg = N'�����������ƻ��쳣��' + Error_Message()
		insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, @DateNow, 'Error', @Msg, @DateTimeNow, @RunUser)
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
		delete from MRP_ProductionPlanDet where ProductionPlanId in(select Id from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo)
		delete from MRP_ProductionPlanMstr where ReleaseNo = @ShipPlanReleaseNo

		--�����������ƻ�ͷ
		insert into MRP_ProductionPlanMstr(ReleaseNo, BatchNo, CreateDate, CreateUser)
		values(@ShipPlanReleaseNo, @BatchNo, @DateTimeNow, @RunUser)

		--�����������ƻ���ϸ
		insert into MRP_ProductionPlanDet(ProductionPlanId, Item, ItemDesc, RefItemCode, OrgQty, Qty, Uom, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, [Version])
		select @@Identity, Item, ItemDesc, RefItemCode, Qty, Qty, Uom, StartTime, WindowTime, @DateTimeNow, @RunUser, @DateTimeNow, @RunUser, 1
		from #tempProductPlan where Qty > 0
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


