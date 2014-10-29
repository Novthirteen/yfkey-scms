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
			set @ErrorMsg = '没有定义出入库参数表。'
			RAISERROR(@ErrorMsg, 16, 1) 

			--代码不会执行到这里
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
			set @ErrorMsg = '没有定义出入库返回表。'
			RAISERROR(@ErrorMsg, 16, 1) 

			--代码不会执行到这里
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
			--记录入库临时表
			insert into #tempInventoryQtyIn_08(Item, Location, Qty, PlanBillId)
			select Item, Location, SUM(Qty), PlanBillId from #tempQtyInventoryIO where Qty > 0
			group by Item, Location, PlanBillId

			--记录出库临时表
			insert into #tempInventoryQtyOut_08(Item, Location, AllowNegativeInventory, Qty)
			select tmp.Item, tmp.Location, l.AllowNegaInv, SUM(tmp.Qty)
			from #tempQtyInventoryIO as tmp left join Location as l on tmp.Location = l.Code 
			where tmp.Qty < 0
			group by tmp.Item, tmp.Location, l.AllowNegaInv

			--if exists(select top 1 1 from #tempInventoryQtyOut_08 where AllowNegativeInventory is null)
			--begin
			--	select @ErrorMsg = N'库位[' + Location + N']不存在。' from #tempInventoryQtyOut_08 where AllowNegativeInventory is null
			--	RAISERROR(@ErrorMsg, 16, 1) 
			--end

			if exists(select top 1 1 from #tempInventoryQtyIn_08)
			begin  --数量入库
				insert into #tempLoadedLocationLotDet_08(Id, Location, Item, OrgQty, Qty, [Version])
				select det.Id, det.Location, det.Item, det.Qty, det.Qty, det.[Version] from #tempInventoryQtyIn_08 as tmp 
				inner join LocationLotDet as det on tmp.Item = det.Item and tmp.Location = det.Location
				where det.Qty < 0 and HuId is null

				select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempInventoryQtyIn_08
				while (@RowId <= @MaxRowId)
				begin  --循环冲抵负数库存
					set @Item = null
					set @Location = null
					set @PlanBillId = null
					set @OrgQty = null
					set @Qty = null
					set @LastQty = 0

					select @Item = Item, @Location = Location, @PlanBillId = PlanBillId, @OrgQty = Qty, @Qty = Qty 
					from #tempInventoryQtyIn_08 where RowId = @RowId
				
					--按负数库存入库的先后顺序依次冲抵
					update det set Qty = CASE WHEN @Qty >= -Qty THEN 0 WHEN @Qty < -Qty and @Qty > 0 THEN Qty + @Qty ELSE Qty END,
					@Qty = @Qty + @LastQty, @LastQty = Qty
					from #tempLoadedLocationLotDet_08 as det with(INDEX(#tempLoadedLocationLotDet_08_Item_Location_Id))
					where det.Item = @Item and det.Location = @Location
					set @Qty = @Qty + @LastQty
				
					if (@PlanBillId is not null and @OrgQty <> @Qty)
					begin  --入库的寄售库存被负数库存冲抵，要结算冲抵部分的PlanBill
						insert into #tempSettlePlanBill(PlanBillId, SettleQty) values(@PlanBillId, @OrgQty - @Qty)
					end

					if (@Qty > 0)
					begin  --未冲抵完的部分作为新记录插入库存明细表
						insert into #tempInsertLocationLotDet_08(Location, Item, Qty, IsCS, PlanBillId)
						values(@Location, @Item, @Qty, CASE WHEN @PlanBillId is null then 0 else 1 end, @PlanBillId)
					end

					set @RowId = @RowId + 1
				end
			end

			if exists(select top 1 1 from #tempInventoryQtyOut_08)
			begin  --数量出库，出库PlanBillId必定为空
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
				
					--按正数库存入库的先后顺序依次冲抵
					update det set Qty = CASE WHEN -@Qty >= Qty THEN 0 WHEN -@Qty < Qty and -@Qty > 0 THEN Qty + @Qty ELSE Qty END,
					@Qty = @Qty + @LastQty, @LastQty = Qty
					from #tempLoadedLocationLotDet_08 as det with(INDEX(#tempLoadedLocationLotDet_08_Item_Location_Id))
					where det.Item = @Item and det.Location = @Location and det.Qty > 0
					set @Qty = @Qty + @LastQty

					if (@Qty < 0)
					begin  --未冲抵完的部分作为新记录插入库存明细表
						if (@AllowNegativeInventory = 0)
						begin
							set @ErrorMsg = N'物料[' + @Item + N']在库位[' + @Location + N']不允许负库存。'
							RAISERROR(@ErrorMsg, 16, 1) 
						end
						else
						begin
							if exists(select top 1 1 from #tempLoadedLocationLotDet_08 where Item = @Item and Location = @Location and Qty < 0)
							begin  --如果找到负数记录直接更新负数记录的数量
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

			--记录返回的出入库事务
			insert into #tempQtyInventoryTrans(Location, Item, Qty, PlanBillId)
			select Location, Item, Qty - OrgQty, PlanBillId from #tempLoadedLocationLotDet_08 where OrgQty <> Qty

			--记录返回的出入库事务
			insert into #tempQtyInventoryTrans(Location, Item, Qty, PlanBillId)
			select Location, Item, Qty, PlanBillId from #tempInsertLocationLotDet_08

			--把结算数量更新成订单单位
			update tmp set tmp.SettleQty = tmp.SettleQty / pb.UnitQty
			from #tempSettlePlanBill as tmp inner join PlanBill as pb on tmp.PlanBillId = pb.Id
		end try
		begin catch
			set @ErrorMsg = N'数据准备出现异常：' + Error_Message()
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
					RAISERROR(N'数据已经被更新，请重试。', 16, 1)
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

			set @ErrorMsg = N'数据更新出现异常：' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch
	end try
	begin catch
		set @ErrorMsg = N'更新数量库存出现异常：' + Error_Message()
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempInventoryQtyIn_08
	drop table #tempInventoryQtyOut_08
	drop table #tempLoadedLocationLotDet_08
	drop table #tempInsertLocationLotDet_08
	drop table #tempSettlePlanBill
END
GO