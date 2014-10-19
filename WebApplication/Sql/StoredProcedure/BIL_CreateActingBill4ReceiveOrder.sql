SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='BIL_CreateActingBill4ReceiveOrder') 
     DROP PROCEDURE BIL_CreateActingBill4ReceiveOrder 
GO

CREATE PROCEDURE [dbo].[BIL_CreateActingBill4ReceiveOrder] 
(
	@RecNo varchar(50),
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
	
	begin try
		if @trancount = 0
		begin
            begin tran
        end

		create table #tempPlanBillId
		(
			PlanBillId int,
			ActQty decimal(18, 8),
			LastModifyDate datetime
		)
		
		declare @OrderType varchar(50)
		select @OrderType = OrderType from ReceiptMstr where RecNo = @RecNo
		
		if (@OrderType = 'Procurement' or @OrderType = 'Subconctracting')
		begin
			insert into #tempPlanBillId(PlanBillId, ActQty, LastModifyDate)
			select pb.Id, rd.RecQty, pb.LastModifyDate
			from ReceiptDet as rd with(NOLOCK)
			inner join OrderLocTrans as olt with(NOLOCK) on rd.OrderLocTransId = olt.Id
			inner join PlanBill as pb with(NOLOCK) on rd.Id = pb.RecDetId
			left join Location as l with(NOLOCK) on olt.Loc = l.Code
			where rd.RecNo = @RecNo and (l.Code is null or l.IsSetCS = 1 or pb.SettleTerm in ('BAR', 'BAC'))
		end
		else if (@OrderType = 'Transfer')			
		begin
			insert into #tempPlanBillId(PlanBillId, ActQty, LastModifyDate)
			select pb.Id, rd.RecQty, pb.LastModifyDate
			from ReceiptDet as rd with(NOLOCK)
			inner join OrderLocTrans as olt with(NOLOCK) on rd.OrderLocTransId = olt.Id
			inner join PlanBill as pb with(NOLOCK) on rd.PlanBillId = pb.Id
			left join Location as l with(NOLOCK) on olt.Loc = l.Code
			where rd.RecNo = @RecNo and (l.Code is null or l.IsSetCS = 1)
		end
		else if (@OrderType = 'Distribution')
		begin
			insert into #tempPlanBillId(PlanBillId, ActQty, LastModifyDate)
			select pb.Id, rd.RecQty, pb.LastModifyDate
			from ReceiptDet as rd with(NOLOCK)
			inner join OrderLocTrans as olt with(NOLOCK) on rd.OrderLocTransId = olt.Id
			inner join PlanBill as pb with(NOLOCK) on rd.Id = pb.RecDetId
			where rd.RecNo = @RecNo and pb.SettleTerm ='BAR'
		end

		if exists(select top 1 1 from #tempPlanBillId)
		begin
			create table #tempPlanBill
			(
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
				ActQty decimal(18, 8),
			)

			insert into #tempPlanBill(OrderNo, ExtRecNo, RecNo, IpNo, TransType, Item, RefItemCode, BillAddr, Uom, UC, PriceList, UnitPrice, Currency, IsIncludeTax, TaxCode, LocFrom, IsProvEst, EffDate, ActQty)
			select pb.OrderNo, pb.ExtRecNo, pb.RecNo, pb.IpNo, pb.TransType, pb.Item, pb.RefItemCode, pb.BillAddr, pb.Uom, pb.UC, pb.PriceList, pb.UnitPrice, pb.Currency, pb.IsIncludeTax, pb.TaxCode, pb.LocFrom, pb.IsProvEst, pb.EffDate, SUM(rd.RecQty * olt.UnitQty)
			from ReceiptDet as rd with(NOLOCK)
			inner join PlanBill as pb with(NOLOCK) on rd.Id = pb.RecDetId
			inner join #tempPlanBillId as tpb with(NOLOCK) on pb.Id = tpb.Id
			group by pb.OrderNo, pb.ExtRecNo, pb.RecNo, pb.IpNo, pb.TransType, pb.Item, pb.RefItemCode, pb.BillAddr, pb.Uom, pb.UC, pb.PriceList, pb.UnitPrice, pb.Currency, pb.IsIncludeTax, pb.TaxCode, pb.LocFrom, pb.IsProvEst, pb.EffDate
	
			update pb set PlanQty = pb.PlanQty - tpb.ActQty, ActQty = ISNULL(pb.ActQty, 0) + tpb.ActQty
			from PlanBill as pb with(NOLOCK) inner join #tempPlanBillId as tpb on pb.Id = tpb.PlanBillId and pb.LastModifyDate = tpb.LastModifyDate

			if exists(select top 1 1 from #tempPlanBillId having COUNT(1) <> @@ROWCOUNT)
			begin
				RAISERROR(N'数据已经被更新，请重试。', 16, 1)
			end

			update ab set BillQty = BillQty + tpb.ActQty, 
			Status = (CASE WHEN BilledQty <> (BillQty + tpb.ActQty) THEN 'Create' ELSE 'Close' END),
			LastModifyUser = @CreateUser,
			LastModifyDate = @DateTimeNow
			from #tempPlanBill as tpb inner join ActBill as ab 
			on tpb.OrderNo = ab.OrderNo
			and (tpb.ExtRecNo = ab.ExtRecNo or (tpb.ExtRecNo is null and ab.ExtRecNo is null))
			and tpb.RecNo = ab.RecNo
			and tpb.TransType = ab.TransType
			and tpb.Item = ab.Item
			and tpb.BillAddr = ab.BillAddr
			and tpb.Uom = ab.Uom
			and tpb.UC = ab.UC
			and tpb.PriceList = ab.PriceList
			and tpb.UnitPrice = ab.UnitPrice
			and tpb.Currency = ab.Currency
			and tpb.IsIncludeTax = ab.IsIncludeTax
			and (tpb.TaxCode = ab.TaxCode or (tpb.TaxCode is null and ab.TaxCode is null))
			and (tpb.LocFrom = ab.LocFrom or (tpb.LocFrom is null and ab.LocFrom is null))
			and tpb.IsProvEst = ab.IsProvEst
			and tpb.EffDate = ab.EffDate
		
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
			tpb.OrderNo,
			tpb.RecNo,
			tpb.ExtRecNo,
			tpb.TransType,
			tpb.Item,
			tpb.BillAddr,
			tpb.Uom,
			tpb.ActQty,
			0,
			tpb.UnitPrice,
			0,
			0,
			tpb.PriceList,
			tpb.Currency,
			tpb.IsIncludeTax,
			tpb.TaxCode,
			tpb.IsProvEst,
			@DateTimeNow,
			@CreateUser,
			@DateTimeNow,
			@CreateUser,
			'Create',
			tpb.EffDate,
			tpb.UC,
			tpb.LocFrom,
			tpb.IpNo,
			tpb.RefItemCode 
			from #tempPlanBill as tpb left join ActBill as ab 
			on tpb.OrderNo = ab.OrderNo
			and (tpb.ExtRecNo = ab.ExtRecNo or (tpb.ExtRecNo is null and ab.ExtRecNo is null))
			and tpb.RecNo = ab.RecNo
			and tpb.TransType = ab.TransType
			and tpb.Item = ab.Item
			and tpb.BillAddr = ab.BillAddr
			and tpb.Uom = ab.Uom
			and tpb.UC = ab.UC
			and tpb.PriceList = ab.PriceList
			and tpb.UnitPrice = ab.UnitPrice
			and tpb.Currency = ab.Currency
			and tpb.IsIncludeTax = ab.IsIncludeTax
			and (tpb.TaxCode = ab.TaxCode or (tpb.TaxCode is null and ab.TaxCode is null))
			and (tpb.LocFrom = ab.LocFrom or (tpb.LocFrom is null and ab.LocFrom is null))
			and tpb.IsProvEst = ab.IsProvEst
			and tpb.EffDate = ab.EffDate
			where ab.Id is null

			drop table #tempPlanBill
		end
		
		drop table #tempPlanBillId

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
		
		set @ErrorMsg = Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch
END
GO