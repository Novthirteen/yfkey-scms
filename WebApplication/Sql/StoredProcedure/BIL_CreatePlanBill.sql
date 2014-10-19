SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='BIL_CreatePlanBill') 
     DROP PROCEDURE BIL_CreatePlanBill 
GO

CREATE PROCEDURE [dbo].[BIL_CreatePlanBill] 
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
		
		if exists(select top 1 1 from ReceiptMstr as rm with(NOLOCK)
			inner join ReceiptDet as rd with(NOLOCK) on rm.RecNo = rd.RecNo	
			inner join OrderLocTrans as olt with(NOLOCK) on rd.OrderLocTransId = olt.Id
			inner join OrderDet as od with(NOLOCK) on olt.OrderDetId = od.Id
			inner join OrderMstr as om with(NOLOCK) on od.OrderNo = od.OrderNo 
			where rm.RecNo = @RecNo and om.[Type] in ('Procurement', 'Subconctracting'))
		begin
			insert into PlanBill(
			OrderNo,
			RecNo,
			ExtRecNo,
			TransType,
			Item,
			BillAddr,
			SettleTerm,
			Uom,
			PlanQty,
			ActQty,
			UnitQty,
			UnitPrice,
			PriceList,
			Currency,
			IsIncludeTax,
			TaxCode,
			IsProvEst,
			PlanAmount,
			ActAmount,
			CreateDate,
			CreateUser,
			LastModifyDate,
			LastModifyUser,
			IsAutoBill,
			UC,
			HuId,
			LotNo,
			LocFrom,
			IpNo,
			RefItemCode,
			PartyFrom,
			EffDate,
			RecDetId)
			select 
			om.OrderNo,
			rd.RecNo,
			rm.ExtRecNo,
			'PO',
			od.Item,
			ISNULL(od.BillFrom, om.BillFrom),
			ISNULL(od.BillSettleTerm, om.BillSettleTerm),
			od.Uom,
			ISNULL(rd.RecQty, 0),
			0,
			olt.UnitQty,
			ISNULL(pld.UnitPrice, 0),
			ISNULL(od.PriceListFrom, om.PriceListFrom),
			ISNULL(pld.Currency, om.Currency),
			ISNULL(pld.IsIncludeTax, 0), 
			pld.TaxCode, 
			ISNULL(pld.IsProvEst, 1), 
			ISNULL(pld.UnitPrice, 0) * rd.RecQty,
			0,
			@DateTimeNow,
			@CreateUser,
			@DateTimeNow,
			@CreateUser,
			om.IsAutoBill,
			od.UC,
			rd.HuId,
			rd.LotNo,
			olt.Loc,
			rm.RefIpNo,
			od.RefItemCode,
			CASE WHEN olt.Loc = 'Reject' THEN om.PartyTo ELSE NULL END,
			rm.CreateDate,
			rd.Id
			from ReceiptMstr as rm with(NOLOCK)
			inner join ReceiptDet as rd with(NOLOCK) on rm.RecNo = rd.RecNo
			inner join OrderLocTrans as olt with(NOLOCK) on rd.OrderLocTransId = olt.Id
			inner join OrderDet as od with(NOLOCK) on olt.OrderDetId = od.Id
			inner join OrderMstr as om with(NOLOCK) on od.OrderNo = od.OrderNo
			left join PriceListDet as pld with(NOLOCK) on od.PriceListDetFrom = pld.PriceList
			where rm.RecNo = @RecNo
		end
		else
		begin
			insert into PlanBill(
			OrderNo,
			RecNo,
			ExtRecNo,
			TransType,
			Item,
			BillAddr,
			SettleTerm,
			Uom,
			PlanQty,
			ActQty,
			UnitQty,
			UnitPrice,
			PriceList,
			Currency,
			IsIncludeTax,
			TaxCode,
			IsProvEst,
			PlanAmount,
			ActAmount,
			CreateDate,
			CreateUser,
			LastModifyDate,
			LastModifyUser,
			IsAutoBill,
			UC,
			HuId,
			LotNo,
			LocFrom,
			IpNo,
			RefItemCode,
			PartyFrom,
			EffDate,
			RecDetId)
			select 
			om.OrderNo,
			rd.RecNo,
			rm.ExtRecNo,
			'SO',
			od.Item,
			ISNULL(od.BillTo, om.BillTo),
			ISNULL(od.BillSettleTerm, om.BillSettleTerm),
			od.Uom,
			ISNULL(rd.RecQty, 0),
			0,
			olt.UnitQty,
			ISNULL(pld.UnitPrice, 0),
			ISNULL(od.PriceListTo, om.PriceListTo),
			ISNULL(pld.Currency, om.Currency),
			ISNULL(pld.IsIncludeTax, 0), 
			pld.TaxCode, 
			ISNULL(pld.IsProvEst, 1), 
			ISNULL(pld.UnitPrice, 0) * rd.RecQty,
			0,
			@DateTimeNow,
			@CreateUser,
			@DateTimeNow,
			@CreateUser,
			om.IsAutoBill,
			od.UC,
			rd.HuId,
			rd.LotNo,
			olt.Loc,
			rm.RefIpNo,
			od.RefItemCode,
			CASE WHEN olt.Loc = 'Reject' THEN om.PartyTo ELSE NULL END,
			ISNULL(im.CreateDate, @DateTimeNow),  --销售单的生效日期取asn创建的日期
			rd.Id
			from ReceiptMstr as rm 
			inner join ReceiptDet as rd with(NOLOCK) on rm.RecNo = rd.RecNo	
			inner join OrderLocTrans as olt with(NOLOCK) on rd.OrderLocTransId = olt.Id
			inner join OrderDet as od with(NOLOCK) on olt.OrderDetId = od.Id
			inner join OrderMstr as om with(NOLOCK) on od.OrderNo = od.OrderNo
			left join PriceListDet as pld with(NOLOCK) on od.PriceListDetTo = pld.PriceList
			left join ReceiptIp as ri with(NOLOCK) on rm.RecNo = ri.RecNo
			left join IpMstr as im with(NOLOCK) on ri.IpNo = im.IpNo
			where rm.RecNo = @RecNo
		end
		
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