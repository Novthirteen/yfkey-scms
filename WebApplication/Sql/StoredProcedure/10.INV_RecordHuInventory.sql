SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='INV_RecordHuInventory') 
     DROP PROCEDURE INV_RecordHuInventory 
GO

CREATE PROCEDURE [dbo].[INV_RecordHuInventory] 
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

	set @DateTimeNow = GetDate()
	set @trancount = @@trancount

	create table #tempInventoryHuIn_04
	(
		RowId int Identity(1, 1) primary key,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		HuId varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LotNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Qty decimal(18, 8),
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Bin varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LocationLotDetId int,
		PlanBillId int,
		HuQty decimal(18, 8)
	)

	create table #tempInventoryHuOut_04
	(
		RowId int Identity(1, 1) primary key,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		HuId varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LotNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Qty decimal(18, 8),
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LocationQty decimal(18, 8),
		LocationLotDetId int,
		PlanBillId int,
		[Version] int,
		HuQty decimal(18, 8)
	)

	begin try
		if not exists(select top 1 1 FROM tempdb.sys.objects WHERE type = 'U' AND name like '#tempHuInventoryIO%') 
		begin
			set @ErrorMsg = 'û�ж������������'
			RAISERROR(@ErrorMsg, 16, 1) 

			--���벻��ִ�е�����
			create table #tempHuInventoryIO
			(
				RowId int Identity(1, 1) primary key,
				HuId varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Bin varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Qty decimal(18, 8)
			)
		end

		if not exists(select top 1 1 FROM tempdb.sys.objects WHERE type = 'U' AND name like '#tempHuInventoryTrans%') 
		begin
			set @ErrorMsg = 'û�ж������ⷵ�ر�'
			RAISERROR(@ErrorMsg, 16, 1) 

			--���벻��ִ�е�����
			create table #tempHuInventoryTrans
			(
				RowId int Identity(1, 1) primary key,
				Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
				HuId varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Bin varchar(50) COLLATE  Chinese_PRC_CI_AS,
				LotNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
				Qty decimal(18, 8),
				PlanBillId int
			)
		end

		begin try
			--��¼�����ʱ��
			insert into #tempInventoryHuIn_04(Item, HuId, LotNo, Qty, Location, Bin, LocationLotDetId, HuQty, PlanBillId)
			select hu.Item, tmp.HuId, hu.LotNo, tmp.Qty, tmp.Location, tmp.Bin, det.Id, hu.Qty, pb.Id
			from #tempHuInventoryIO as tmp
			left join LocationLotDet as det with(NOLOCK) on tmp.HuId = det.HuId and det.Qty > 0
			left join HuDet as hu with(NOLOCK) on tmp.HuId = hu.HuId
			left join PlanBill as pb with(NOLOCK) on tmp.HuId = pb.HuId and pb.PlanQty > 0 and pb.ActQty = 0 and pb.TransType = 'PO'  --PlanQty > 0����ΪС��0�Ļ���������
			where tmp.Qty > 0

			--��¼������ʱ��
			insert into #tempInventoryHuOut_04(Item, HuId, LotNo, Qty, Location, LocationQty, LocationLotDetId, PlanBillId, [Version], HuQty)
			select hu.Item, tmp.HuId, hu.LotNo, tmp.Qty, tmp.Location, det.Qty, det.Id, CASE WHEN det.IsCS = 1 THEN det.PlanBillId ELSE null end, det.[Version], hu.Qty
			from #tempHuInventoryIO as tmp 
			left join LocationLotDet as det with(NOLOCK) on tmp.HuId = det.HuId and tmp.Location = det.Location and det.Qty > 0
			left join HuDet as hu with(NOLOCK) on tmp.HuId = hu.HuId
			where tmp.Qty < 0

			if exists(select top 1 1 from #tempInventoryHuIn_04)
			begin  --�������У��
				if exists(select top 1 1 from #tempInventoryHuIn_04 where Item is null)
				begin
					select @ErrorMsg = N'����[' + HuId + N']�����ڡ�' from #tempInventoryHuIn_04 where Item is null
					RAISERROR(@ErrorMsg, 16, 1) 
				end

				if exists(select top 1 1 from #tempInventoryHuIn_04 where HuQty <> Qty)
				begin
					select @ErrorMsg = N'����[' + HuId + N']���������[' + CONVERT(varchar, Qty) + N']��������������[' + CONVERT(varchar, HuQty) + N']��' from #tempInventoryHuIn_04 where HuQty <> Qty
					RAISERROR(@ErrorMsg, 16, 1) 
				end

				if exists(select top 1 1 from #tempInventoryHuIn_04 where LocationLotDetId is not null)
				begin
					select top 1 @ErrorMsg = N'�������[' + tmp.HuId + N']�Ѿ��ڿ�λ[' + det.Location + N']�С�' 
					from #tempInventoryHuIn_04 as tmp inner join LocationLotDet as det on tmp.LocationLotDetId = det.Id
					where tmp.LocationLotDetId is not null
					RAISERROR(@ErrorMsg, 16, 1) 
				end

				--��¼���صĳ��������
				insert into #tempHuInventoryTrans(Item, HuId, LotNo, Location, Bin, Qty, PlanBillId)
				select Item, HuId, LotNo, Location, Bin, Qty, PlanBillId from #tempInventoryHuIn_04
			end

			if exists(select top 1 1 from #tempInventoryHuOut_04)
			begin  --��������У��
				if exists(select top 1 1 from #tempInventoryHuOut_04 where Item is null)
				begin
					select @ErrorMsg = N'����[' + HuId + N']�����ڡ�' from #tempInventoryHuOut_04 where Item is null
					RAISERROR(@ErrorMsg, 16, 1) 
				end

				if exists(select top 1 1 from #tempInventoryHuOut_04 where HuQty <> Qty)
				begin
					select @ErrorMsg = N'����[' + HuId + N']�ĳ�������[' + CONVERT(varchar, Qty) + N']��������������[' + CONVERT(varchar, HuQty) + N']��' from #tempInventoryHuOut_04 where HuQty <> Qty
					RAISERROR(@ErrorMsg, 16, 1) 
				end
			
				if exists(select top 1 1 from #tempInventoryHuOut_04 where LocationLotDetId is null)
				begin
					select top 1 @ErrorMsg = N'��������[' + HuId + N']�ڿ�λ[' + Location + N']�в����ڡ�' 
					from #tempInventoryHuOut_04 where LocationLotDetId is null
					RAISERROR(@ErrorMsg, 16, 1) 
				end

				if exists(select top 1 1 from #tempInventoryHuOut_04 where LocationQty <> Qty)
				begin
					select @ErrorMsg = N'����[' + HuId + N']�ĳ�������[' + CONVERT(varchar, Qty) + N']�����ڿ������[' + CONVERT(varchar, LocationQty) + N']��' from #tempInventoryHuOut_04 where LocationQty <> Qty
					RAISERROR(@ErrorMsg, 16, 1) 
				end
				
				--��¼���صĳ��������
				insert into #tempHuInventoryTrans(Item, HuId, LotNo, Location, Qty, PlanBillId)
				select Item, HuId, LotNo, Location, Qty, PlanBillId from #tempInventoryHuOut_04
			end
		end try
		begin catch
			set @ErrorMsg = N'����׼�������쳣��' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch

		begin try
			if @Trancount = 0
			begin
				begin tran
			end

			if exists(select top 1 1 from #tempInventoryHuIn_04)
			begin  --����������
				insert into LocationLotDet(Location, Bin, Item, HuId, LotNo, Qty, IsCS, PlanBillId, CreateDate, LastModifyDate, [Version])
				select Location, Bin, Item, HuId, LotNo, Qty, CASE WHEN PlanBillId is null THEN 0 ELSE 1 END, PlanBillId, @DateTimeNow, @DateTimeNow, 1 from #tempInventoryHuIn_04
			end

			if exists(select top 1 1 from #tempInventoryHuOut_04)
			begin  --�����������
				update LocationLotDet set Qty = 0, LastModifyDate = @DateTimeNow, [Version] = det.[Version] + 1
				from LocationLotDet as det inner join #tempInventoryHuOut_04 as tmp on det.Id = tmp.LocationLotDetId and det.[Version] = tmp.[Version]

				if (@@ROWCOUnt <> (select COUNT(1) from #tempInventoryHuOut_04))
				begin
					RAISERROR(N'�����Ѿ������£������ԡ�', 16, 1)
				end
			end

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
		set @ErrorMsg = N'��������������쳣��' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempInventoryHuIn_04
	drop table #tempInventoryHuOut_04
END
GO