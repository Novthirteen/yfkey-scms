SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='ORD_BatchBackflushWIP') 
     DROP PROCEDURE ORD_BatchBackflushWIP 
GO

CREATE PROCEDURE [dbo].[ORD_BatchBackflushWIP] 
(
	@CreateUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN
	set nocount on
	declare @DateTimeNow datetime
	declare @DateNow datetime
	declare @ErrorMsg nvarchar(MAX)
	declare @trancount int

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(varchar(10), @DateTimeNow, 120)
	set @trancount = @@trancount

	declare @RowId int
	declare @MaxRowId int
	declare @IpRowId int
	declare @MaxIpRowId int
	declare @ProdLineIpId int
	declare @ProdLine varchar(50)
	declare @Item varchar(50)
	declare @IpNo varchar(50)
	declare @LocFrom varchar(50)
	declare @LocFromName varchar(50)
	declare @HuId varchar(50)
	declare @PlanBillId int
	declare @Version int
	declare @RemainQty decimal(18, 8)
	declare @AvgBackflushQty decimal(18, 8)
	declare @TotalPlanQty decimal(18, 8)

	Create table #tempProdLineIp_14
	(
		Id int Primary Key,
		ProdLine varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Qty decimal(18, 8),
		BackflushQty decimal(18, 8),
		RemainQty decimal(18, 8),
		PlanBillId int,
		IpNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LocFrom varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LocFromName varchar(50) COLLATE  Chinese_PRC_CI_AS,
		HuId varchar(50) COLLATE  Chinese_PRC_CI_AS,
		[Version] int
	)

	
	Create table #tempThisProdLineIp_14
	(
		RowId int identity(1, 1),
		Id int Primary Key,
		ProdLine varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Qty decimal(18, 8),
		BackflushQty decimal(18, 8),
		RemainQty decimal(18, 8),
		PlanBillId int,
		IpNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LocFrom varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LocFromName varchar(50) COLLATE  Chinese_PRC_CI_AS,
		HuId varchar(50) COLLATE  Chinese_PRC_CI_AS,
		[Version] int
	)

	Create table #tempGroupedProdLineIp_14
	(
		RowId int identity(1, 1),
		ProdLine varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
	)

	Create table #tempOrderPlanBackflush_14
	(
		Id int Primary Key,
		ProdLine varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		OrderLocTransId int,
		PlanQty decimal(18, 8)
	)

	Create table #tempThisOrderPlanBackflush_14
	(
		Id int Primary Key,
		ProdLine varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		OrderLocTransId int,
		PlanQty decimal(18, 8),
		BackflushQty decimal(18, 8),
	)

	create table #tempLog_14
	(
		ProdLineIpId int,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		ProdLine varchar(50) COLLATE  Chinese_PRC_CI_AS,
		IpNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LocFrom varchar(50) COLLATE  Chinese_PRC_CI_AS,
		HuId varchar(50) COLLATE  Chinese_PRC_CI_AS,
		RemainQty decimal(18, 8),
		Lvl tinyint,
		Msg varchar(500) COLLATE  Chinese_PRC_CI_AS,
		CreateDate datetime
	)

	create table #tempSettlePlanBill
	(
		RowId int Identity(1, 1) primary key,
		PlanBillId int,
		SettleQty decimal(18, 8)
	)
	
	begin try
		begin try
			insert into #tempProdLineIp_14(Id, ProdLine, Item, Qty, BackflushQty, RemainQty, PlanBillId, LocFrom, LocFromName, HuId, [Version])
			select ip.Id, ip.ProdLine, ip.Item, ip.Qty, ip.BackflushQty, ip.Qty - ip.BackflushQty, CASE WHEN ip.IsCS = 1 THEN ip.PlanBillId ELSE NULL END, ip.LocFrom, l.Name, ip.HuId, ip.[Version] 
			from ProdLineIp as ip left join Location as l on ip.LocFrom = l.Code
			where ip.Qty > ip.BackflushQty and ip.[Status] = 'Create'
			
			insert into #tempOrderPlanBackflush_14(Id, ProdLine, Item, OrderLocTransId, PlanQty)
			select pl.Id, pl.Flow, trans.Item, pl.OrderLocTransId, pl.PlanQty
			from OrderPlanBackflush as pl 
			inner join OrderLocTrans as trans on pl.OrderLocTransId = trans.Id 
			where pl.IsActive = 1

			insert into #tempGroupedProdLineIp_14(ProdLine, Item)
			select ProdLine, Item from #tempProdLineIp_14 group by ProdLine, Item
		end try
		begin catch
			set @ErrorMsg = N'数据准备出现异常：' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch

		begin try
			select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempGroupedProdLineIp_14
			while (@RowId <= @MaxRowId)
			begin  --循环反冲Bom
				select @ProdLine = ProdLine, @Item = Item from #tempGroupedProdLineIp_14 where RowId = @RowId

				truncate table #tempThisProdLineIp_14
				insert into #tempThisProdLineIp_14(Id, ProdLine, Item, Qty, BackflushQty, RemainQty, PlanBillId, LocFrom, LocFromName, HuId, [Version])
				select Id, ProdLine, Item, Qty, BackflushQty, RemainQty, PlanBillId, LocFrom, LocFromName, HuId, [Version] 
				from #tempProdLineIp_14 where Item = @Item and ProdLine = @ProdLine

				truncate table #tempThisOrderPlanBackflush_14
				insert into #tempThisOrderPlanBackflush_14(Id, ProdLine, Item, OrderLocTransId, PlanQty)
				select Id, ProdLine, Item, OrderLocTransId, PlanQty from #tempOrderPlanBackflush_14 where Item = @Item and ProdLine = @ProdLine

				if exists(select top 1 1 from #tempThisOrderPlanBackflush_14)
				begin
					begin try
						if @trancount = 0
						begin
							begin tran
						end

						select @IpRowId = MIN(RowId), @MaxIpRowId = MAX(RowId) from #tempThisProdLineIp_14
						while (@IpRowId <= @MaxIpRowId)
						begin  --待反冲记录
							select @ProdLineIpId = Id, @IpNo = IpNo, @LocFrom = LocFrom, @LocFromName = LocFromName,
							@HuId = HuId, @PlanBillId = PlanBillId, @RemainQty = RemainQty, @Version = [Version]
							from #tempThisProdLineIp_14 where RowId = @IpRowId

							insert into #tempLog_14(ProdLineIpId, Item, ProdLine, IpNo, LocFrom, HuId, RemainQty, Lvl, Msg, CreateDate) 
							values(@ProdLineIpId, @Item, @ProdLine, @IpNo, @LocFrom, @HuId, @RemainQty, 0, N'开始投料反冲', GETDATE())
						
							--计算总的计划反冲数和实际平均反冲数
							select @TotalPlanQty = SUM(PlanQty), @AvgBackflushQty = FLOOR(@RemainQty / SUM(PlanQty) * 10000) / 10000 
							from #tempThisOrderPlanBackflush_14
						
							--计算实际反冲数
							update #tempThisOrderPlanBackflush_14 set BackflushQty = PlanQty * @AvgBackflushQty

							--实际反冲数的零头差加到一条待反冲记录里面
							update #tempThisOrderPlanBackflush_14 set BackflushQty = BackflushQty + (@RemainQty - @TotalPlanQty * @AvgBackflushQty)
							where Id = (select top 1 Id from #tempThisOrderPlanBackflush_14)

							--更新OrderLocTrans表
							update olt set AccumQty = ISNULL(olt.AccumQty, 0) + bf.BackflushQty
							from OrderLocTrans as olt inner join (select OrderLocTransId, SUM(BackflushQty) as BackflushQty 
																	from #tempThisOrderPlanBackflush_14
																	group by OrderLocTransId) as bf on bf.OrderLocTransId = olt.Id	

							--更新OrderPlanBackflush表
							update bf set IsActive = 0
							from OrderPlanBackflush as bf inner join #tempThisOrderPlanBackflush_14 as tmp on bf.Id = tmp.Id
							where bf.IsActive = 1

							--更新ProdLineIp表
							update ProdLineIp set BackflushQty = Qty, [Status] = 'Close', LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow, [Version] = [Version] + 1
							where Id = @ProdLineIpId and [Version] = @Version

							if (@@Identity <> 1)
							begin
								RAISERROR(N'数据已经被更新，请重试。', 16, 1)
							end

							if @PlanBillId is not null
							begin  --所有未结算的物料在反冲后全部结算
								truncate table #tempSettlePlanBill
								insert into #tempSettlePlanBill(PlanBillId, SettleQty) values(@PlanBillId, -@RemainQty)
								--把结算数量更新成订单单位
								update tmp set tmp.SettleQty = tmp.SettleQty / pb.UnitQty from #tempSettlePlanBill as tmp inner join PlanBill as pb on tmp.PlanBillId = pb.Id
								--执行结算程序
								exec BIL_SettlePlanBill @CreateUser
							end

							--记录库存事务
							insert into LocTrans(OrderNo, OrderDetId, OrderLocTransId, RefOrderNo, ExtOrderNo, IpNo, TransType, Item, ItemDesc, Uom, Qty, PartyFrom, PartyFromName, PartyTo, PartyToName, Loc, LocName, RefLoc, RefLocName, EffDate, CreateDate, CreateUser, IsSubContract, HuId)
							select det.OrderNo, det.Id, trans.Id, @ProdLineIpId, bf.Id, @IpNo, 'ISS-WO-BF', trans.Item, i.Desc1, i.Uom, -bf.BackflushQty, mstr.PartyFrom, pf.Name, mstr.PartyFrom, pf.Name, bf.ProdLine, bf.ProdLine, @LocFrom, @LocFromName, @DateNow, @DateTimeNow, @CreateUser, 0, @HuId
							from #tempThisOrderPlanBackflush_14 as bf 
							inner join OrderLocTrans as trans on bf.OrderLocTransId = trans.Id
							inner join OrderDet as det on trans.OrderDetId = det.Id
							inner join OrderMstr as mstr on det.OrderNo = mstr.OrderNo
							inner join Party as pf on mstr.PartyFrom = pf.Code
							inner join Item as i on trans.Item = i.Code
						
							insert into #tempLog_14(ProdLineIpId, Item, ProdLine, IpNo, LocFrom, HuId, RemainQty, Lvl, Msg, CreateDate) 
							values(@ProdLineIpId, @Item, @ProdLine, @IpNo, @LocFrom, @HuId, @RemainQty, 0, N'投料反冲成功', GETDATE())

							set @IpRowId = @IpRowId + 1
						end

						if @trancount = 0 
						begin  
							commit
						end
					end try
					begin catch
						if @Trancount = 0
						begin
							rollback
						end 

						set @ErrorMsg = N'投料反冲出现异常：' + Error_Message()
						set @ErrorMsg = SUBSTRING(@ErrorMsg, 1, 500)

						truncate table #tempLog_14
						insert into #tempLog_14(Item, ProdLine, Lvl, Msg, CreateDate) 
						values(@Item, @ProdLine, 1, @ErrorMsg, GETDATE())
					end catch
				end
				else
				begin
					insert into #tempLog_14(Item, ProdLine, Lvl, Msg, CreateDate) 
					values(@Item, @ProdLine, 2, N'没有找到投料反冲的生产单', GETDATE())
				end
			
				--记录反冲日志
				insert into ProdLineIpBackflushLog(ProdLineIpId, Item, ProdLine, IpNo, LocFrom, HuId, RemainQty, Lvl, Msg, CreateDate) 
				select ProdLineIpId, Item, ProdLine, IpNo, LocFrom, HuId, RemainQty, Lvl, Msg, CreateDate from #tempLog_14
				truncate table #tempLog_14

				set @RowId = @RowId + 1
			end
		end try
		begin catch
			set @ErrorMsg = N'数据更新出现异常：' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch
	end try
	begin catch
		set @ErrorMsg = N'投料反冲出现异常：' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch
END
GO