SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='INV_RecordQtyInventory') 
     DROP PROCEDURE INV_RecordQtyInventory 
GO

CREATE PROCEDURE [dbo].[INV_RecordQtyInventory] 
(
	@CreateUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN
	set nocount on
	declare @DateTimeNow datetime
	declare @ErrorMsg nvarchar(MAX)
	declare @trancount int
	declare @RowId int
	declare @MaxRowId int
	declare @Item varchar(50)
	declare @Location varchar(50)
	declare @AllowNegativeInventory bit
	declare @PlanBillId int
	declare @OrgQty decimal(18, 8)
	declare @Qty decimal(18, 8)
	declare @LastQty decimal(18, 8)

	set @DateTimeNow = GetDate()
	set @trancount = @@trancount

	create table #tempInventoryQtyIn_08
	(
		RowId int Identity(1, 1) primary key,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Qty decimal(18, 8),
		PlanBillId int
	)

	create table #tempInventoryQtyOut_08
	(
		RowId int Identity(1, 1) primary key,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		AllowNegativeInventory bit,
		Qty decimal(18, 8)
	)

	create table #tempLoadedLocationLotDet_08
	(
		Id int primary key,
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		OrgQty decimal(18, 8),
		Qty decimal(18, 8),
		IsCS bit,
		PlanBillId int,
		[Version] int,
	)
	create index #tempLoadedLocationLotDet_08_Item_Location_Id on #tempLoadedLocationLotDet_08(Item asc, Location asc, Id asc)

	create table #tempInsertLocationLotDet_08
	(
		RowId int Identity(1, 1) primary key,
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Qty decimal(18, 8),
		IsCS bit,
		PlanBillId int
	)

	create table #tempSettlePlanBill
	(
		RowId int Identity(1, 1) primary key,
		PlanBillId int,
		SettleQty decimal(18, 8)
	)

	begin try
		if not exists(select top 1 1 FROM tempdb.sys.objects WHERE type = 'U' AND name like '#tempQtyInventoryIO%') 
		begin
			set @ErrorMsg = 'û�ж������������'
			RAISERROR(@ErrorMsg, 16, 1) 

			--���벻��ִ�е�����
			create table #tempQtyInventoryIO
			(
				RowId int Identity(1, 1) primary key,
				Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Qty decimal(18, 8),
				PlanBillId int
			)
		end

		if not exists(select top 1 1 FROM tempdb.sys.objects WHERE type = 'U' AND name like '#tempQtyInventoryTrans%') 
		begin
			set @ErrorMsg = 'û�ж������ⷵ�ر�'
			RAISERROR(@ErrorMsg, 16, 1) 

			--���벻��ִ�е�����
			create table #tempQtyInventoryTrans
			(
				RowId int Identity(1, 1) primary key,
				Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Qty decimal(18, 8),
				PlanBillId int
			)
		end

		begin try
			--��¼�����ʱ��
			insert into #tempInventoryQtyIn_08(Item, Location, Qty, PlanBillId)
			select Item, Location, SUM(Qty), PlanBillId from #tempQtyInventoryIO where Qty > 0
			group by Item, Location, PlanBillId

			--��¼������ʱ��
			insert into #tempInventoryQtyOut_08(Item, Location, AllowNegativeInventory, Qty)
			select tmp.Item, tmp.Location, l.AllowNegaInv, SUM(tmp.Qty)
			from #tempQtyInventoryIO as tmp left join Location as l on tmp.Location = l.Code 
			where tmp.Qty < 0
			group by tmp.Item, tmp.Location, l.AllowNegaInv

			--if exists(select top 1 1 from #tempInventoryQtyOut_08 where AllowNegativeInventory is null)
			--begin
			--	select @ErrorMsg = N'��λ[' + Location + N']�����ڡ�' from #tempInventoryQtyOut_08 where AllowNegativeInventory is null
			--	RAISERROR(@ErrorMsg, 16, 1) 
			--end

			if exists(select top 1 1 from #tempInventoryQtyIn_08)
			begin  --�������
				insert into #tempLoadedLocationLotDet_08(Id, Location, Item, OrgQty, Qty, [Version])
				select det.Id, det.Location, det.Item, det.Qty, det.Qty, det.[Version] from #tempInventoryQtyIn_08 as tmp 
				inner join LocationLotDet as det on tmp.Item = det.Item and tmp.Location = det.Location
				where det.Qty < 0 and HuId is null

				select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempInventoryQtyIn_08
				while (@RowId <= @MaxRowId)
				begin  --ѭ����ָ������
					set @Item = null
					set @Location = null
					set @PlanBillId = null
					set @OrgQty = null
					set @Qty = null
					set @LastQty = 0

					select @Item = Item, @Location = Location, @PlanBillId = PlanBillId, @OrgQty = Qty, @Qty = Qty 
					from #tempInventoryQtyIn_08 where RowId = @RowId
				
					--��������������Ⱥ�˳�����γ��
					update det set Qty = CASE WHEN @Qty >= -Qty THEN 0 WHEN @Qty < -Qty and @Qty > 0 THEN Qty + @Qty ELSE Qty END,
					@Qty = @Qty + @LastQty, @LastQty = Qty
					from #tempLoadedLocationLotDet_08 as det with(INDEX(#tempLoadedLocationLotDet_08_Item_Location_Id))
					where det.Item = @Item and det.Location = @Location
					set @Qty = @Qty + @LastQty
				
					if (@PlanBillId is not null and @OrgQty <> @Qty)
					begin  --���ļ��ۿ�汻��������֣�Ҫ�����ֲ��ֵ�PlanBill
						insert into #tempSettlePlanBill(PlanBillId, SettleQty) values(@PlanBillId, @OrgQty - @Qty)
					end

					if (@Qty > 0)
					begin  --δ�����Ĳ�����Ϊ�¼�¼��������ϸ��
						insert into #tempInsertLocationLotDet_08(Location, Item, Qty, IsCS, PlanBillId)
						values(@Location, @Item, @Qty, CASE WHEN @PlanBillId is null then 0 else 1 end, @PlanBillId)
					end

					set @RowId = @RowId + 1
				end
			end

			if exists(select top 1 1 from #tempInventoryQtyOut_08)
			begin  --�������⣬����PlanBillId�ض�Ϊ��
				insert into #tempLoadedLocationLotDet_08(Id, Location, Item, OrgQty, Qty, PlanBillId, [Version])
				select det.Id, det.Location, det.Item, det.Qty, det.Qty, det.PlanBillId, det.[Version] from #tempInventoryQtyOut_08 as tmp 
				inner join LocationLotDet as det on tmp.Item = det.Item and tmp.Location = det.Location
				where det.Qty <> 0 and HuId is null

				select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempInventoryQtyOut_08
				while (@RowId <= @MaxRowId)
				begin
					set @Item = null
					set @Location = null
					set @AllowNegativeInventory = null
					set @OrgQty = null
					set @Qty = null
					set @LastQty = 0

					select @Item = Item, @Location = Location, @AllowNegativeInventory = AllowNegativeInventory, @OrgQty = Qty, @Qty = Qty 
					from #tempInventoryQtyOut_08 where RowId = @RowId
				
					--��������������Ⱥ�˳�����γ��
					update det set Qty = CASE WHEN -@Qty >= Qty THEN 0 WHEN -@Qty < Qty and -@Qty > 0 THEN Qty + @Qty ELSE Qty END,
					@Qty = @Qty + @LastQty, @LastQty = Qty
					from #tempLoadedLocationLotDet_08 as det with(INDEX(#tempLoadedLocationLotDet_08_Item_Location_Id))
					where det.Item = @Item and det.Location = @Location and det.Qty > 0
					set @Qty = @Qty + @LastQty

					if (@Qty < 0)
					begin  --δ�����Ĳ�����Ϊ�¼�¼��������ϸ��
						if (@AllowNegativeInventory = 0)
						begin
							set @ErrorMsg = N'����[' + @Item + N']�ڿ�λ[' + @Location + N']��������档'
							RAISERROR(@ErrorMsg, 16, 1) 
						end
						else
						begin
							if exists(select top 1 1 from #tempLoadedLocationLotDet_08 where Item = @Item and Location = @Location and Qty < 0)
							begin  --����ҵ�������¼ֱ�Ӹ��¸�����¼������
								update tmp set Qty = Qty + @Qty
								from #tempLoadedLocationLotDet_08 as tmp 
								inner join (select top 1 Id from #tempLoadedLocationLotDet_08 where Item = @Item and Location = @Location and Qty < 0 order by Id) as tar on tmp.Id = tar.Id
							end
							else
							begin
								insert into #tempInsertLocationLotDet_08(Location, Item, Qty, IsCS) values(@Location, @Item, @Qty, 0)
							end
						end
					end

					set @RowId = @RowId + 1
				end
			end

			--��¼���صĳ��������
			insert into #tempQtyInventoryTrans(Location, Item, Qty, PlanBillId)
			select Location, Item, Qty - OrgQty, PlanBillId from #tempLoadedLocationLotDet_08 where OrgQty <> Qty

			--��¼���صĳ��������
			insert into #tempQtyInventoryTrans(Location, Item, Qty, PlanBillId)
			select Location, Item, Qty, PlanBillId from #tempInsertLocationLotDet_08

			--�ѽ����������³ɶ�����λ
			update tmp set tmp.SettleQty = tmp.SettleQty / pb.UnitQty
			from #tempSettlePlanBill as tmp inner join PlanBill as pb on tmp.PlanBillId = pb.Id
		end try
		begin catch
			set @ErrorMsg = N'����׼�������쳣��' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch

		begin try
			declare @UpdateCount int
			if @Trancount = 0
			begin
				begin tran
			end

			if exists(select top 1 1 from #tempSettlePlanBill)
			begin 
				exec BIL_SettlePlanBill @CreateUser
			end

			select @UpdateCount = COUNT(1) from #tempLoadedLocationLotDet_08 where OrgQty <> Qty

			if @UpdateCount > 0
			begin 
				update LocationLotDet set Qty = tmp.Qty, LastModifyDate = GETDATE(), [Version] = det.[Version] + 1
				from LocationLotDet as det inner join #tempLoadedLocationLotDet_08 as tmp on det.Id = tmp.Id and det.[Version] = tmp.[Version]
				where tmp.OrgQty <> tmp.Qty

				if (@@ROWCOUNT <> @UpdateCount)
				begin
					RAISERROR(N'�����Ѿ������£������ԡ�', 16, 1)
				end
			end

			insert into LocationLotDet(Location, Item, Qty, IsCS, PlanBillId, CreateDate, LastModifyDate, [Version])
			select Location, Item, Qty, IsCS, PlanBillId, @DateTimeNow, @DateTimeNow, 1 from #tempInsertLocationLotDet_08
			
			if @Trancount = 0 
			begin  
				commit
			end
		end try
		begin catch
			if @Trancount = 0
			begin
				rollback
			end 

			set @ErrorMsg = N'���ݸ��³����쳣��' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch
	end try
	begin catch
		set @ErrorMsg = N'���������������쳣��' + Error_Message()
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempInventoryQtyIn_08
	drop table #tempInventoryQtyOut_08
	drop table #tempLoadedLocationLotDet_08
	drop table #tempInsertLocationLotDet_08
	drop table #tempSettlePlanBill
END
GO