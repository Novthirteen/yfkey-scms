SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='ORD_BatchBackflushWO') 
     DROP PROCEDURE ORD_BatchBackflushWO
GO

CREATE PROCEDURE [dbo].ORD_BatchBackflushWO
(
	@CreateUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateTimeNow datetime
	declare @DateNow datetime
	declare @ErrorMsg nvarchar(MAX)
	declare @Trancount int
	declare @RowId int
	declare @MaxRowId int
	declare @Item varchar(50)
	declare @Location varchar(50)
	declare @BackflushQty decimal(18, 8)
	declare @DeleteCycleCount int

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(varchar(10), @DateTimeNow, 120)
	set @Trancount = @@Trancount

	create table #tempWOBomBackflush_09
	(
		Id int Primary Key,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		BackflushQty decimal(18, 8), 
	)

	create index IX_tempWOBomBackflush_09_Item_Location on #tempWOBomBackflush_09(Item asc, Location asc)

	create table #tempGroupWOBomBackflush_09
	(
		RowId int identity(1, 1) Primary Key,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		BackflushQty decimal(18, 8),
	)

	create table #tempLog_09
	(
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		BackflushQty decimal(18, 8),
		Lvl tinyint,
		Msg varchar(500) COLLATE  Chinese_PRC_CI_AS,
		CreateDate datetime
	)

	create table #tempInventoryIO
	(
		RowId int Identity(1, 1) primary key,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		HuId varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Bin varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Qty decimal(18, 8),
		PlanBillId int
	)

	create table #tempInventoryTrans
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

	create table #tempSettlePlanBill
	(
		RowId int Identity(1, 1) primary key,
		PlanBillId int,
		SettleQty decimal(18, 8)
	)

	begin try
		begin try
			insert into #tempWOBomBackflush_09(Id, Item, Location, BackflushQty)
			select Id, Item, Location, BackflushQty from WOBomBackflush
	
			insert into #tempGroupWOBomBackflush_09(Item, Location, BackflushQty)
			select Item, Location, SUM(BackflushQty) from #tempWOBomBackflush_09 group by Item, Location
		end try
		begin catch
			set @ErrorMsg = N'数据准备出现异常：' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch

		begin try
			select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempGroupWOBomBackflush_09
			while (@RowId <= @MaxRowId)
			begin  --循环反冲Bom
				select @Item = Item, @Location = Location, @BackflushQty = -BackflushQty from #tempGroupWOBomBackflush_09 where RowId = @RowId
				
				begin try
					insert into #tempLog_09(Item, Location, BackflushQty, Lvl, Msg, CreateDate) values(@Item, @Location, @BackflushQty, 0, N'开始反冲', GETDATE())
					if @Trancount = 0
					begin
						begin tran
					end

					--更新OrderLocTrans表
					update olt set AccumQty = ISNULL(olt.AccumQty, 0) + bf.BackflushQty
					from  OrderLocTrans as olt inner join (select bf.OrderLocTransId, SUM(bf.BackflushQty) as BackflushQty 
															from WOBomBackflush as bf 
															inner join #tempWOBomBackflush_09 as tmp on tmp.Id = bf.Id
															where tmp.Item = @Item and tmp.Location = @Location
															group by bf.OrderLocTransId) as bf on bf.OrderLocTransId = olt.Id

					--反冲物料
					truncate table #tempInventoryIO
					insert into #tempInventoryIO(Item, Location, Qty) values(@Item, @Location, @BackflushQty)
					exec INV_RecordInventory 0, @CreateUser

					if exists(select top 1 1 from #tempInventoryTrans where PlanBillId is not null and Qty <> 0)
					begin  --所有未结算的物料在反冲后全部结算
						truncate table #tempSettlePlanBill
						insert into #tempSettlePlanBill(PlanBillId, SettleQty) select PlanBillId, SUM(-Qty) from #tempInventoryTrans where PlanBillId is not null and Qty <> 0 group by PlanBillId
						--把结算数量更新成订单单位
						update tmp set tmp.SettleQty = tmp.SettleQty / pb.UnitQty from #tempSettlePlanBill as tmp inner join PlanBill as pb on tmp.PlanBillId = pb.Id
						--执行结算程序
						exec BIL_SettlePlanBill @CreateUser
					end

					--记录库存事务
					insert into LocTrans(OrderNo, RecNo, TransType, Item, ItemDesc, Uom, Qty, PartyFrom, PartyFromName, PartyTo, PartyToName, Loc, LocName, EffDate, CreateDate, CreateUser, OrderDetId, OrderLocTransId, IsSubContract)
					select bf.OrderNo, bf.RecNo, 'ISS-WO', bf.Item, i.Desc1, i.Uom, -bf.BackflushQty, om.PartyFrom, pf.Name, om.PartyFrom, pf.Name, bf.Location, l.Name, bf.EffDate, @DateTimeNow, @CreateUser, bf.OrderDetId, bf.OrderLocTransId, 0 
					from WOBomBackflush as bf inner join #tempWOBomBackflush_09 as tmp on bf.Id = tmp.Id
					inner join OrderMstr as om on bf.OrderNo = om.OrderNo
					inner join Party as pf on om.PartyFrom = pf.Code
					inner join Item as i on bf.Item = i.Code
					inner join Location as l on bf.Location = l.Code
					where tmp.Item = @Item and tmp.Location = @Location

					--备份待反冲记录
					set @DeleteCycleCount = 1
					while exists(select top 1 1 from WOBomBackflush as bf  
													inner join #tempWOBomBackflush_09 as tmp on tmp.Id = bf.Id
													where bf.Item = @Item)
					begin
						delete top(10000) bf output 
						deleted.Id, 
						deleted.Flow,
						deleted.OrderNo,
						deleted.OrderDetId,
						deleted.OrderLocTransId,
						deleted.RecNo,
						deleted.Item,
						deleted.HuId,
						deleted.BackflushQty,
						deleted.Location,
						deleted.DssImpHisId,
						deleted.EffDate,
						deleted.CreateDate,
						deleted.CreateUser,
						@DateTimeNow,
						@CreateUser
						into WOBomBackflushArch(
						Id,
						Flow,
						OrderNo,
						OrderDetId,
						OrderLocTransId,
						RecNo,
						Item,
						HuId,
						BackflushQty,
						Location,
						DssImpHisId,
						EffDate,
						CreateDate,
						CreateUser,
						BackflushDate,
						BackflushUser) 
						from WOBomBackflush as bf
						inner join #tempWOBomBackflush_09 as tmp on tmp.Id = bf.Id
						where bf.Item = @Item

						set @DeleteCycleCount = @DeleteCycleCount + 1

						if (@DeleteCycleCount > 999)
						begin
							set @ErrorMsg = N'备份反冲物料[' + @Item + N']的循环次数大于999次，可能出现死循环。'
							RAISERROR(@ErrorMsg, 16, 1) 
						end
					end

					if @Trancount = 0 
					begin  
						commit
					end

					insert into #tempLog_09(Item, Location, BackflushQty, Lvl, Msg, CreateDate) values(@Item, @Location, @BackflushQty, 0, N'反冲成功', GETDATE())
				end try
				begin catch
					if @Trancount = 0
					begin
						rollback
					end 

					set @ErrorMsg = N'反冲出现异常：' + Error_Message()
					set @ErrorMsg = SUBSTRING(@ErrorMsg, 1, 500)
					insert into #tempLog_09(Item, Location, BackflushQty, Lvl, Msg, CreateDate) values(@Item, @Location, @BackflushQty, 1, @ErrorMsg, GETDATE())
				end catch

				--记录反冲日志
				insert into WOBomBackflushLog(Item, Location, BackflushQty, Lvl, Msg, CreateUser, CreateDate) 
				select Item, Location, BackflushQty, Lvl, Msg, @CreateUser, CreateDate from #tempLog_09
				truncate table #tempLog_09

				set @RowId = @RowId + 1
			end
		end try
		begin catch
			set @ErrorMsg = N'数据更新出现异常：' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch
	end try
	begin catch
		set @ErrorMsg = N'批量反冲Bom出现异常：' + Error_Message()
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempWOBomBackflush_09
	drop table #tempGroupWOBomBackflush_09
	drop table #tempLog_09
	drop table #tempInventoryIO
	drop table #tempInventoryTrans
	drop table #tempSettlePlanBill
END 


