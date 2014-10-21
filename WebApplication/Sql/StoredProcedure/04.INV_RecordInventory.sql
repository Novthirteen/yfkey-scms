SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='INV_RecordInventory') 
     DROP PROCEDURE INV_RecordInventory 
GO

CREATE PROCEDURE [dbo].[INV_RecordInventory] 
(
	@NeedInspection bit,
	@CreateUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN
	set nocount on
	declare @DateTimeNow datetime
	declare @ErrorMsg nvarchar(MAX)

	set @DateTimeNow = GetDate()

	create table #tempQtyInventoryIO
	(
		RowId int Identity(1, 1) primary key,
		Item varchar(50),
		Location varchar(50),
		Qty decimal(18, 8),
		PlanBillId int
	)

	create table #tempHuInventoryIO
	(
		RowId int Identity(1, 1) primary key,
		HuId varchar(50),
		Location varchar(50),
		Qty decimal(18, 8)
	)

	create table #tempQtyInventoryTrans
	(
		RowId int Identity(1, 1) primary key,
		Location varchar(50),
		Item varchar(50),
		Qty decimal(18, 8),
		PlanBillId int
	)

	create table #tempHuInventoryTrans
	(
		RowId int Identity(1, 1) primary key,
		Location varchar(50),
		HuId varchar(50),
		LotNo varchar(50),
		Qty decimal(18, 8),
		PlanBillId int
	)

	begin try
		if not exists(select top 1 1 FROM tempdb.sys.objects WHERE type = 'U' AND name like '#tempInventoryIO%') 
		begin
			set @ErrorMsg = 'û�ж������������'
			RAISERROR(@ErrorMsg, 16, 1) 

			--���벻��ִ�е�����
			create table #tempInventoryIO
			(
				RowId int Identity(1, 1) primary key,
				Item varchar(50),
				Location varchar(50),
				Qty decimal(18, 8),
				HuId varchar(50),
				PlanBillId int
			)
		end

		if not exists(select top 1 1 FROM tempdb.sys.objects WHERE type = 'U' AND name like '#tempInventoryTrans%') 
		begin
			set @ErrorMsg = 'û�ж������������'
			RAISERROR(@ErrorMsg, 16, 1) 

			--���벻��ִ�е�����
			create table #tempInventoryTrans
			(
				RowId int Identity(1, 1) primary key,
				Location varchar(50),
				Item varchar(50),
				HuId varchar(50),
				LotNo varchar(50),
				Qty decimal(18, 8),
				PlanBillId int
			)
		end

		if (@NeedInspection = 1)
		begin  --����ֱ����Inspect�⣬������ջ�����Ӧ���ȵ���BIL_CreateActingBill4ReceiveOrder��ɽ���
			if exists(select top 1 1 from #tempInventoryIO where Qty <= 0)
			begin
				select top 1 @ErrorMsg = N'��������[' + Item + N']����������С�ڵ���0��' from #tempInventoryIO where Qty <= 0
				RAISERROR(@ErrorMsg, 16, 1) 
			end

			insert into LocationLotDet(Location, Item, HuId, LotNo, Qty, IsCS, PlanBillId, CreateDate, LastModifyDate, RefLoc)
			select 'Inspect', tmp.Item, tmp.HuId, hu.LotNo, tmp.Qty, CASE WHEN tmp.PlanBillId is null then 0 else 1 END, tmp.PlanBillId, @DateTimeNow, @DateTimeNow, tmp.Location 
			from #tempInventoryIO as tmp left join HuDet as hu on tmp.HuId = hu.HuId
		end
		else
		begin  --�Ǽ��鴦��
			insert into #tempQtyInventoryIO(Item, Location, Qty, PlanBillId)
			select Item, Location, Qty, PlanBillId from #tempInventoryIO where HuId is null

			insert into #tempHuInventoryIO(HuId, Location)
			select HuId, Location from #tempInventoryIO where HuId is not null

			if exists(select top 1 1 from #tempQtyInventoryIO)
			begin
				if exists(select top 1 1 from #tempQtyInventoryIO where Qty < 0 and PlanBillId is not null)
				begin
					select @ErrorMsg = N'��������[' + Item + N']����[' + CONVERT(varchar, -Qty) + N']��PlanBill[' + CONVERT(varchar, PlanBillId) + N']��Ϊ�ա�' from #tempQtyInventoryIO where Qty > 0 and PlanBillId is not null
					RAISERROR(@ErrorMsg, 16, 1) 
				end

				exec INV_RecordQtyInventory @CreateUser

				insert into #tempInventoryTrans(Location, Item, Qty, PlanBillId)
				select Location, Item, Qty, PlanBillId from #tempQtyInventoryTrans
				
			end

			if exists(select top 1 1 from #tempHuInventoryIO)
			begin
				exec INV_RecordHuInventory @CreateUser

				insert into #tempInventoryTrans(Location, HuId, LotNo, Qty, PlanBillId)
				select Location, HuId, LotNo, Qty, PlanBillId from #tempHuInventoryTrans

			end
		end
	end try
	begin catch
		set @ErrorMsg = N'���¿������쳣��' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempQtyInventoryIO
	drop table #tempHuInventoryIO
	drop table #tempQtyInventoryTrans
	drop table #tempHuInventoryTrans
END
GO