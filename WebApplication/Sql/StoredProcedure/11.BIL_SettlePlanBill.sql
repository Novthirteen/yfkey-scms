SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='BIL_SettlePlanBill') 
     DROP PROCEDURE BIL_SettlePlanBill 
GO

CREATE PROCEDURE [dbo].[BIL_SettlePlanBill] 
(
	@CreateUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN
	set nocount on
	declare @DateTimeNow datetime
	declare @ErrorMsg nvarchar(MAX)
	declare @trancount int

	set @DateTimeNow = GetDate()
	set @trancount = @@trancount
	
	create table #tempPlanBill_11
	(
		Id int primary key,
		OrderNo varchar(50),
		ExtRecNo varchar(50),
		RecNo varchar(50),
		IpNo varchar(50),
		TransType varchar(50),
		Item varchar(50),
		RefItemCode varchar(50),
		BillAddr varchar(50),
		Uom varchar(50),
		UC decimal(18, 8),
		PriceList varchar(50),
		UnitPrice decimal(18, 8),
		Currency varchar(50),
		IsIncludeTax bit,
		TaxCode varchar(50),
		LocFrom varchar(50),
		IsProvEst bit,
		EffDate DateTime,
		PlanQty decimal(18, 8),
		ActQty decimal(18, 8),
		ThisActQty decimal(18, 8),
		LastModifyDate datetime,
	)

	begin try
		if not exists(select top 1 1 FROM tempdb.sys.objects WHERE type = 'U' AND name like '#tempSettlePlanBill%') 
		begin
			set @ErrorMsg = '没有定义寄售结算参数表。'
			RAISERROR(@ErrorMsg, 16, 1) 

			--代码不会执行到这里
			create table #tempSettlePlanBill
			(
				RowId int Identity(1, 1) primary key,
				PlanBillId int,
				SettleQty decimal(18, 8)
			)
		end

		begin try
			insert into #tempPlanBill_11(Id, OrderNo, ExtRecNo, RecNo, IpNo, TransType, Item, RefItemCode, BillAddr, Uom, UC, PriceList, UnitPrice, Currency, IsIncludeTax, TaxCode, LocFrom, IsProvEst, EffDate, PlanQty, ActQty, ThisActQty, LastModifyDate)
			select pb.Id, pb.OrderNo, pb.ExtRecNo, pb.RecNo, pb.IpNo, pb.TransType, pb.Item, pb.RefItemCode, pb.BillAddr, pb.Uom, pb.UC, pb.PriceList, pb.UnitPrice, pb.Currency, pb.IsIncludeTax, pb.TaxCode, pb.LocFrom, pb.IsProvEst, pb.EffDate, pb.PlanQty, ISNULL(pb.ActQty, 0), tmp.SettleQty, pb.LastModifyDate
			from PlanBill as pb
			inner join (select PlanBillId, SUM(SettleQty) as SettleQty from #tempSettlePlanBill group by PlanBillId) as tmp on pb.Id = tmp.PlanBillId

			if exists(select top 1 1 from #tempPlanBill_11 where PlanQty <= ActQty or ABS(PlanQty - ActQty) < ABS(ThisActQty))
			begin
				select top 1 @ErrorMsg = N'要货单[' + OrderNo + N']的结算数量大于收货数量。' from #tempPlanBill_11 where PlanQty <= ActQty or ABS(PlanQty + ActQty) < ABS(ThisActQty)
				RAISERROR(@ErrorMsg, 16, 1) 
			end
		end try
		begin catch
			set @ErrorMsg = N'数据准备出现异常：' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch

		begin try
			if @trancount = 0
			begin
				begin tran
			end

			update pb set ActQty = tmp.ActQty + tmp.ThisActQty
			from PlanBill as pb inner join #tempPlanBill_11 as tmp on pb.Id = tmp.Id and pb.LastModifyDate = tmp.LastModifyDate

			if exists(select top 1 1 from #tempPlanBill_11 having COUNT(1) <> @@ROWCOUNT)
			begin
				RAISERROR(N'数据已经被更新，请重试。', 16, 1)
			end

			update ab set BillQty = BillQty + tmp.ThisActQty, 
			Status = (CASE WHEN BilledQty <> (BillQty + tmp.ThisActQty) THEN 'Create' ELSE 'Close' END),
			LastModifyUser = @CreateUser,
			LastModifyDate = @DateTimeNow
			from #tempPlanBill_11 as tmp inner join ActBill as ab 
			on tmp.OrderNo = ab.OrderNo
			and (tmp.ExtRecNo = ab.ExtRecNo or (tmp.ExtRecNo is null and ab.ExtRecNo is null))
			and tmp.RecNo = ab.RecNo
			and tmp.TransType = ab.TransType
			and tmp.Item = ab.Item
			and tmp.BillAddr = ab.BillAddr
			and tmp.Uom = ab.Uom
			and tmp.UC = ab.UC
			and tmp.PriceList = ab.PriceList
			and tmp.UnitPrice = ab.UnitPrice
			and tmp.Currency = ab.Currency
			and tmp.IsIncludeTax = ab.IsIncludeTax
			and (tmp.TaxCode = ab.TaxCode or (tmp.TaxCode is null and ab.TaxCode is null))
			and (tmp.LocFrom = ab.LocFrom or (tmp.LocFrom is null and ab.LocFrom is null))
			and tmp.IsProvEst = ab.IsProvEst
			and tmp.EffDate = ab.EffDate
		
			insert into ActBill (
			OrderNo,
			RecNo,
			ExtRecNo,
			TransType,
			Item,
			BillAddr,
			Uom,
			BillQty,
			BilledQty,
			UnitPrice,
			BillAmount,
			BilledAmount,
			PriceList,
			Currency,
			IsIncludeTax,
			TaxCode,
			IsProvEst,
			CreateDate,
			CreateUser,
			LastModifyDate,
			LastModifyUser,
			Status,
			EffDate,
			UC,
			LocFrom,
			IpNo,
			RefItemCode)
			select
			tmp.OrderNo,
			tmp.RecNo,
			tmp.ExtRecNo,
			tmp.TransType,
			tmp.Item,
			tmp.BillAddr,
			tmp.Uom,
			tmp.ThisActQty,
			0,
			tmp.UnitPrice,
			0,
			0,
			tmp.PriceList,
			tmp.Currency,
			tmp.IsIncludeTax,
			tmp.TaxCode,
			tmp.IsProvEst,
			@DateTimeNow,
			@CreateUser,
			@DateTimeNow,
			@CreateUser,
			'Create',
			tmp.EffDate,
			tmp.UC,
			tmp.LocFrom,
			tmp.IpNo,
			tmp.RefItemCode 
			from #tempPlanBill_11 as tmp left join ActBill as ab 
			on tmp.OrderNo = ab.OrderNo
			and (tmp.ExtRecNo = ab.ExtRecNo or (tmp.ExtRecNo is null and ab.ExtRecNo is null))
			and tmp.RecNo = ab.RecNo
			and tmp.TransType = ab.TransType
			and tmp.Item = ab.Item
			and tmp.BillAddr = ab.BillAddr
			and tmp.Uom = ab.Uom
			and tmp.UC = ab.UC
			and tmp.PriceList = ab.PriceList
			and tmp.UnitPrice = ab.UnitPrice
			and tmp.Currency = ab.Currency
			and tmp.IsIncludeTax = ab.IsIncludeTax
			and (tmp.TaxCode = ab.TaxCode or (tmp.TaxCode is null and ab.TaxCode is null))
			and (tmp.LocFrom = ab.LocFrom or (tmp.LocFrom is null and ab.LocFrom is null))
			and tmp.IsProvEst = ab.IsProvEst
			and tmp.EffDate = ab.EffDate
			where ab.Id is null
		
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
		
			set @ErrorMsg = N'数据更新出现异常：' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch
	end try
	begin catch
		set @ErrorMsg = N'结算寄售库存出现异常：' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempPlanBill_11
END
GO