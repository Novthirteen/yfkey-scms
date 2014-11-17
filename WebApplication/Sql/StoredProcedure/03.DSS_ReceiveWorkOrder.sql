SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='DSS_ReceiveWorkOrder') 
     DROP PROCEDURE DSS_ReceiveWorkOrder 
GO

CREATE PROCEDURE [dbo].[DSS_ReceiveWorkOrder] 
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

	set @DateTimeNow = GetDate()
	set @DateNow = CONVERT(varchar(10), @DateTimeNow, 120)

	Create table #tempWOReceipt_03
	(
		Id int primary key,
		ProdLine varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		ItemDesc varchar(50) COLLATE  Chinese_PRC_CI_AS,
		HuId varchar(50) COLLATE  Chinese_PRC_CI_AS,
		QtyStr varchar(50) COLLATE  Chinese_PRC_CI_AS,
		IsQtyNumeric bit,
		OfflineDateStr varchar(50) COLLATE  Chinese_PRC_CI_AS,
		OfflineTimeStr varchar(50) COLLATE  Chinese_PRC_CI_AS,
		IsOfflineDateTime bit,
		Qty decimal(18, 8),
		OffLineDateTime datetime,
		OrderNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		OrderDetId int,
		OrderLocTransId int,
		LotNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LotNoYear varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LotNoMonth varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LotNoDay varchar(50) COLLATE  Chinese_PRC_CI_AS,
		UC decimal(18, 8),
		Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
		UnitQty decimal(18, 8),
		ManufactureDate DateTime,
		ManufactureParty varchar(50) COLLATE  Chinese_PRC_CI_AS,
		ManufacturePartyName varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		LocationName varchar(50) COLLATE  Chinese_PRC_CI_AS,
		RecNo varchar(50) COLLATE  Chinese_PRC_CI_AS
	)

	Create table #tempNoWO_03
	(
		RowId int identity(1, 1) primary key,
		ProdLine varchar(50),
		Item varchar(50),
		Qty decimal(18, 8),
		StartTime datetime,
		WindowTime datetime,
	)

	Create table #tempBomTobeBackflush_03
	(
		Flow varchar(50), 
		OrderNo varchar(50), 
		OrderDetId int, 
		OrderLocTransId int,
		RecNo varchar(50),
		Item varchar(50),
		HuId varchar(50), 
		BackflushQty decimal(18, 8), 
		Location varchar(50), 
		BackFlushMethod varchar(50),
		DssImpHisId int,
		EffDate datetime
	)

	begin try
		begin try
			insert into #tempWOReceipt_03(Id, ProdLine, Item, ItemDesc, HuId, LotNo, LotNoYear, LotNoMonth, LotNoDay, QtyStr, IsQtyNumeric, OfflineDateStr, OfflineTimeStr, IsOfflineDateTime, OrderNo)
			select dih.Id, dih.data0, dih.data1, i.Desc1, dih.data2, SUBSTRING(data2, LEN(data2) - 7, 4), SUBSTRING(data2, LEN(data2) - 7, 1), SUBSTRING(data2, LEN(data2) - 6, 1), SUBSTRING(data2, LEN(data2) - 5, 2), dih.data3, ISNUMERIC(dih.data3), dih.data7, dih.data8, ISDATE(dih.data7 + ' ' + dih.data8), dih.data12
			from DssImpHis as dih left join Item as i on dih.data1 = i.Code
			where dih.IsActive = 1 and dih.ErrCount < 2 and dih.DssInboundCtrl = 9 and dih.EventCode = 'CREATE'

			if not exists(select top 1 1 from #tempWOReceipt_03)
			begin
				return
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where (ProdLine is null or ProdLine = '') and (OrderNo is null or OrderNo = ''))
			begin
				update dih set Memo = '生产线不能为空。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where (tmp.ProdLine is null or tmp.ProdLine = '') and (tmp.OrderNo is null or tmp.OrderNo = '')

				delete from #tempWOReceipt_03 where (ProdLine is null or ProdLine = '') and (OrderNo is null or OrderNo = '')
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where Item is null or Item = '')
			begin
				update dih set Memo = '成品代码不能为空。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.Item is null or tmp.Item = ''

				delete from #tempWOReceipt_03 where Item is null or Item = ''
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where ItemDesc is null)
			begin
				update dih set Memo = '成品代码不存在。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.ItemDesc is null

				delete from #tempWOReceipt_03 where ItemDesc is null
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where HuId is null or HuId = '')
			begin
				update dih set Memo = '条码不能为空。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.HuId is null or tmp.HuId = ''

				delete from #tempWOReceipt_03 where HuId is null or HuId = ''
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where LEN(HuId) < 9)
			begin
				update dih set Memo = '条码长度不能小于9。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where LEN(tmp.HuId) < 9

				delete from #tempWOReceipt_03 where  LEN(HuId) < 9
			end

			if exists (select top 1 1 from #tempWOReceipt_03 where LotNoYear not in ('1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'S', 'T', 'V', 'W', 'X', 'Y'))
			begin
				update dih set Memo = '批号的年份格式不正确。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.LotNoYear not in ('1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'S', 'T', 'V', 'W', 'X', 'Y')

				delete from #tempWOReceipt_03 where LotNoYear not in ('1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'S', 'T', 'V', 'W', 'X', 'Y')
			end

			if exists (select top 1 1 from #tempWOReceipt_03 where LotNoMonth not in ('1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C'))
			begin
				update dih set Memo = '批号的月份格式不正确。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.LotNoMonth not in ('1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C')

				delete from #tempWOReceipt_03 where LotNoMonth not in ('1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C')
			end

			if exists (select top 1 1 from #tempWOReceipt_03 where LotNoDay > 31 or LotNoDay < 1)
			begin
				update dih set Memo = '批号的日期格式不正确。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.LotNoDay > 31 or tmp.LotNoDay < 1

				delete from #tempWOReceipt_03 where LotNoDay > 31 or LotNoDay < 1
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where QtyStr is null or QtyStr = '')
			begin
				update dih set Memo = '数量不能为空。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.QtyStr is null or tmp.QtyStr = ''

				delete from #tempWOReceipt_03 where QtyStr is null or QtyStr = ''
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where QtyStr = 0)
			begin
				update dih set Memo = '数量不能为0。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where QtyStr = 0

				delete from #tempWOReceipt_03 where QtyStr = 0
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where IsQtyNumeric = 0)
			begin
				update dih set Memo = '数量不正确。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where IsQtyNumeric = 0

				delete from #tempWOReceipt_03 where IsQtyNumeric = 0
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where OfflineDateStr is null or OfflineDateStr = '')
			begin
				update dih set Memo = '下线日期不能为空。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.OfflineDateStr is null or tmp.OfflineDateStr = ''

				delete from #tempWOReceipt_03 where OfflineDateStr is null or OfflineDateStr = ''
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where OfflineTimeStr is null or OfflineTimeStr = '')
			begin
				update dih set Memo = '下线时间不能为空。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.OfflineTimeStr is null or tmp.OfflineTimeStr = ''

				delete from #tempWOReceipt_03 where OfflineTimeStr is null or OfflineTimeStr = ''
			end

			if exists(select top 1 1 from #tempWOReceipt_03 where IsOfflineDateTime = 0)
			begin
				update dih set Memo = '下线日期或下线时间不正确。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where IsOfflineDateTime = 0

				delete from #tempWOReceipt_03 where IsOfflineDateTime = 0
			end

			if exists(select top 1 1 from #tempWOReceipt_03 as tmp inner join HuDet as hu on tmp.HuId = hu.HuId)
			begin
				update dih set Memo = '条码已经存在。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				inner join HuDet as hu on tmp.HuId = hu.HuId

				delete tmp from #tempWOReceipt_03 as tmp inner join HuDet as hu on tmp.HuId = hu.HuId
			end

			if exists(select top 1 1 from #tempWOReceipt_03 group by HuId having count(1) > 1)
			begin
				update dih set Memo = '条码已经存在。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih 
				inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				inner join (select HuId, Min(Id) as Id from #tempWOReceipt_03 group by HuId having count(1) > 1) as uni on uni.HuId = tmp.HuId and uni.Id <> tmp.Id

				delete tmp from #tempWOReceipt_03 as tmp
				inner join (select HuId, Min(Id) as Id from #tempWOReceipt_03 group by HuId having count(1) > 1) as uni on uni.HuId = tmp.HuId and uni.Id <> tmp.Id
			end

			--为已经有生产单号的找生产单相关信息
			update #tempWOReceipt_03 set ProdLine = mstr.Flow, OrderDetId = det.Id, OrderLocTransId = trans.Id, UC = det.UC, 
			Uom = det.Uom, UnitQty = trans.UnitQty, ManufactureParty = mstr.PartyFrom, ManufacturePartyName = p.Name,  Location = trans.Loc, LocationName = l.Name
			from #tempWOReceipt_03 as tmp inner join OrderMstr as mstr on mstr.OrderNo = tmp.OrderNo
			inner join OrderDet as det on mstr.OrderNo = det.OrderNo
			inner join OrderLocTrans as trans on det.Id = trans.OrderDetId and trans.TransType = 'RCT-WO'
			inner join Location as l on trans.Loc = l.Code
			inner join Party as p on mstr.PartyFrom = p.Code
			where tmp.OrderNo is not null and tmp.OrderNo <> ''

			if exists(select top 1 1 from #tempWOReceipt_03 where OrderNo is not null and OrderNo <> '' and OrderDetId is null)
			begin
				update dih set Memo = '生产单号不存在。', ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
				where tmp.OrderNo is not null and tmp.OrderNo <> '' and tmp.OrderDetId is null
				
				delete from #tempWOReceipt_03 where OrderNo is not null and OrderNo <> '' and OrderDetId is null
			end

			if not exists(select top 1 1 from #tempWOReceipt_03)
			begin
				return
			end

			declare @RowCount int
			declare @BaseRecNoSeq int
			select @RowCount = COUNT(1) from #tempWOReceipt_03
			exec SYS_BatchGetNextSequence 'REC', @RowCount, @BaseRecNoSeq output

			update tmp set RecNo = rec.RecNo
			from #tempWOReceipt_03 as tmp inner join (select Id, ('REC' + replicate('0', 9 - len(@BaseRecNoSeq - ROW_NUMBER() over (order by Id) + 1)) + convert(varchar(50), @BaseRecNoSeq - ROW_NUMBER() over (order by Id) + 1)) as RecNo 
													from #tempWOReceipt_03 as tmp) as rec on tmp.Id = rec.Id

			update #tempWOReceipt_03 set Qty = CONVERT(decimal(18, 8), QtyStr), OffLineDateTime = CONVERT(DateTime, OfflineDateStr + ' ' + OfflineTimeStr), 
			ManufactureDate = CONVERT(DateTime, OfflineDateStr)

			update #tempWOReceipt_03 set ProdLine = mstr.Flow, OrderNo = mstr.OrderNo, OrderDetId = det.Id, OrderLocTransId = trans.Id, UC = det.UC, 
			Uom = det.Uom, UnitQty = trans.UnitQty, ManufactureParty = mstr.PartyFrom, ManufacturePartyName = p.Name,  Location = trans.Loc, LocationName = l.Name
			from #tempWOReceipt_03 as tmp inner join OrderDet as det on tmp.Item = det.Item
			inner join OrderMstr as mstr on mstr.OrderNo = det.OrderNo and mstr.StartTime <= tmp.OffLineDateTime and mstr.WindowTime >= tmp.OffLineDateTime and mstr.Flow like '%' + tmp.ProdLine
			inner join OrderLocTrans as trans on det.Id = trans.OrderDetId and trans.TransType = 'RCT-WO'
			inner join Location as l on trans.Loc = l.Code
			inner join Party as p on mstr.PartyFrom = p.Code
			where mstr.[Type] = 'Production' and mstr.[Status] = 'In-Process' and (tmp.OrderNo is not null or tmp.OrderNo <> '')

			if exists(select top 1 1 from #tempWOReceipt_03 where OrderNo is null)
			begin  --为没有找到工单的收货创建工单
				
				declare @RowId int
				declare @MaxRowId int
				declare @ProdLine varchar(50)
				declare @Item varchar(50)
				declare @Qty decimal(18, 8)
				declare @StartTime datetime
				declare @WindowTime datetime
				declare @OrderNo varchar(50)

				insert into #tempNoWO_03(ProdLine, Item, Qty, StartTime, WindowTime)
				select ProdLine, Item, Sum(Qty), DATEADD(HOUR, -12, MIN(OffLineDateTime)), DATEADD(HOUR, 12, MAX(OffLineDateTime))
				from #tempWOReceipt_03 where OrderNo is null
				group by ProdLine, Item

				select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempNoWO_03

				while @RowId <= @MaxRowId
				begin
					begin try
						select @ProdLine = ProdLine, @Item = Item, @Qty = Qty, @StartTime = StartTime, @WindowTime = WindowTime
						from #tempNoWO_03 where RowId = @RowId
						exec ORD_CreateWorkOrder @ProdLine, 0, null, @Item, @Qty, @StartTime, @WindowTime, 'Normal', 'Nml', @CreateUser, 1, 1, @OrderNo output

						--新增工单回写工单收货表
						update tmp set ProdLine = mstr.Flow, OrderNo = @OrderNo, OrderDetId = det.Id, OrderLocTransId = trans.Id, UC = det.UC, 
						Uom = det.Uom, UnitQty = trans.UnitQty, ManufactureParty = mstr.PartyFrom, Location = trans.Loc, LocationName = l.Name
						from #tempWOReceipt_03 as tmp 
						inner join OrderMstr as mstr on mstr.OrderNo = @OrderNo and tmp.ProdLine = @ProdLine and tmp.Item = @Item
						inner join OrderDet as det on det.OrderNo = mstr.OrderNo
						inner join OrderLocTrans as trans on det.Id = trans.OrderDetId and trans.TransType = 'RCT-WO'
						inner join Location as l on trans.Loc = l.Code
					end try
					begin catch
						update dih set Memo = Error_Message() , ErrCount = 10, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
						from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id
						where tmp.ProdLine = @ProdLine and tmp.Item = @Item

						delete from #tempWOReceipt_03 where ProdLine = @ProdLine and Item = @Item
					end catch

					set @RowId = @RowId + 1
				end
			end
			
			insert into #tempBomTobeBackflush_03(Flow, OrderNo, OrderDetId, OrderLocTransId, RecNo, Item, HuId, BackflushQty, Location, BackFlushMethod, DssImpHisId, EffDate)
			select tmp.ProdLine, tmp.OrderNo, tmp.OrderDetId, trans.Id, tmp.RecNo, trans.Item, tmp.HuId, trans.UnitQty * tmp.Qty, trans.Loc, ISNULL(trans.BackFlushMethod, 'GoodsReceive'), tmp.Id, CONVERT(varchar(10), tmp.OffLineDateTime, 120)
			from #tempWOReceipt_03 as tmp inner join OrderLocTrans as trans on tmp.OrderDetId = trans.OrderDetId
			where trans.TransType = 'ISS-WO'
		end try
		begin catch
			set @ErrorMsg = N'数据准备出现异常：' +  Error_Message() 
			RAISERROR(@ErrorMsg, 16, 1)
		end catch

		set @Trancount = @@Trancount

		begin try
			if @Trancount = 0
			begin
				begin tran
			end

			declare @LocationLotDetId int
			insert into WOBomBackflush(Flow, OrderNo, OrderDetId, OrderLocTransId, RecNo, Item, HuId, BackflushQty, Location, DssImpHisId, EffDate, CreateDate, CreateUser)
			select Flow, OrderNo, OrderDetId, OrderLocTransId, RecNo, Item, HuId, BackflushQty, Location, DssImpHisId, @DateNow, @DateTimeNow, @CreateUser 
			from #tempBomTobeBackflush_03 where BackFlushMethod = 'GoodsReceive' and BackflushQty <> 0

			insert into OrderPlanBackflush(OrderLocTransId, PlanQty, IsActive, Flow) select OrderLocTransId, BackflushQty, 1, Flow from #tempBomTobeBackflush_03 
			where BackFlushMethod = 'BatchFeed' and BackflushQty <> 0

			update dih set IsActive = 0, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow, data12 = tmp.OrderNo, data13 = tmp.OrderDetId, data14 = tmp.OrderLocTransId
			from DssImpHis as dih inner join #tempWOReceipt_03 as tmp on dih.Id = tmp.Id

			insert into HuDet(HuId, LotNo, Item, QualityLevel, Uom, UC, UnitQty, Qty, OrderNo, RecNo, ManufactureDate, ManufactureParty, PrintCount, CreateDate, CreateUser, LotSize, Location, Status)
			select HuId, LotNo, Item, 'Level1', Uom, UC, UnitQty, Qty, OrderNo, RecNo, ManufactureDate, ManufactureParty, 0, @DateTimeNow, @CreateUser, Qty, Location, 'Inventory' from #tempWOReceipt_03
		
			update mstr set Status = 'Complete', CompleteDate = @DateTimeNow, CompleteUser = @CreateUser, [Version] = mstr.[Version] + 1
			from OrderDet as det inner join (select OrderDetId, SUM(Qty) as Qty from #tempWOReceipt_03 group by OrderDetId) as tmp on det.Id = tmp.OrderDetId
			inner join OrderMstr as mstr on det.OrderNo = mstr.OrderNo
			where det.OrderQty <= ISNULL(det.RecQty, 0) + tmp.Qty

			update det set RecQty = ISNULL(det.RecQty, 0) + tmp.Qty
			from OrderDet as det inner join (select OrderDetId, SUM(Qty) as Qty from #tempWOReceipt_03 group by OrderDetId) as tmp on det.Id = tmp.OrderDetId

			update olt set AccumQty = ISNULL(olt.AccumQty, 0) + tmp.Qty * olt.UnitQty
			from OrderLocTrans as olt inner join (select OrderLocTransId, SUM(Qty) as Qty from #tempWOReceipt_03 group by OrderLocTransId) as tmp on olt.Id = tmp.OrderLocTransId

			insert into ReceiptMstr(RecNo, OrderType, CreateDate, CreateUser, PartyFrom, PartyTo, IsPrinted, NeedPrint)
			select RecNo, 'Production', @DateTimeNow, @CreateUser, ManufactureParty, ManufactureParty, 0, 0 from #tempWOReceipt_03
			
			insert into ReceiptDet(RecNo, OrderNo, OrderDetId, OrderLocTransId, Item, ItemDesc, HuId, LotNo, IsCS, ShipQty, RecQty, RejQty, ScrapQty)
			select RecNo, OrderNo, OrderDetId, OrderLocTransId, Item, ItemDesc, HuId, LotNo, 0, 0, Qty, 0, 0 from #tempWOReceipt_03
		
			insert into LocationLotDet(Location, Item, HuId, LotNo, Qty, IsCS, CreateDate, LastModifyDate)
			select Location, Item, HuId, LotNo, Qty, 0, @DateTimeNow, @DateTimeNow from #tempWOReceipt_03 order by Id

			set @LocationLotDetId = @@IDENTITY
			insert into LocTrans(OrderNo, RecNo, HuId, LotNo, BatchNo, TransType, Item, ItemDesc, Uom, Qty, PartyFrom, PartyFromName, PartyTo, PartyToName, Loc, LocName, EffDate, CreateDate, CreateUser, OrderDetId, OrderLocTransId, IsSubContract)
			select OrderNo, RecNo, HuId, LotNo, @LocationLotDetId - (ROW_NUMBER() over (order by Id desc)) + 1, 'RCT-WO', Item, ItemDesc, Uom, Qty, ManufactureParty, ManufacturePartyName, ManufactureParty, ManufacturePartyName, Location, LocationName, @DateNow, @DateTimeNow, @CreateUser, OrderDetId, OrderLocTransId, 0 from #tempWOReceipt_03 order by Id

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
		
			set @ErrorMsg = N'数据更新出现异常：' +  Error_Message() 
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch
	end try
	begin catch
			set @ErrorMsg = N'DSS工单入库出现异常：' +  Error_Message() 
			RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempBomTobeBackflush_03
	drop table #tempWOReceipt_03
	drop table #tempNoWO_03
END
GO