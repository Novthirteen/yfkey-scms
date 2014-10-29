SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='ORD_CreateWorkOrder') 
     DROP PROCEDURE ORD_CreateWorkOrder 
GO

CREATE PROCEDURE [dbo].[ORD_CreateWorkOrder] 
(
	@ProdLine varchar(50),
	@ForceMatchProdLine bit,
	@RefOrderNo varchar(50),
	@FGItem varchar(50),
	@OrderQty decimal(18, 8),
	@StartTime datetime,
	@WindowTime datetime,
	@Priority varchar(50),
	@OrderSubType varchar(50),
	@CreateUser varchar(50),
	@IsAutoRelease bit,
	@IsAutoStart bit,
	@WorkOrderNo varchar(50) output
) --WITH ENCRYPTION
AS 
BEGIN
	set nocount on
	declare @DateTimeNow datetime
	declare @ErrorMsg nvarchar(MAX)
	declare @Trancount int
	declare @OrderNo varchar(50)
	declare @OrderNoSeq int
	declare @OrderDetId int
	declare @Flow varchar(50)
	declare @FlowDetId int
	declare @Bom varchar(50)
	declare @Routing varchar(50)
	declare @DefaultLocFrom varchar(50)
	declare @DefaultLocTo varchar(50)
	declare @FGIsMes bit
	declare @FGItemDesc varchar(100)
	declare @FGUC decimal(18, 8)
	declare @FGFullfillUC bit
	declare @FGUom varchar(5)   --路线成品的单位
	declare @FGBomUom varchar(5)   --成品Bom的单位
	declare @FGBaseUom varchar(5)   --成品基本单位
	declare @FGBomUnitQty decimal(18, 8)  --路线成品的单位转换为成品Bom的单位转换率
	declare @FGUnitQty decimal(18, 8)  --成品Bom的单位转换为成品基本单位转换率
	declare @DefaultOp int
	declare @MinOp int
	declare @MaxOp int

	set @DateTimeNow = GetDate()

	create table #tempBomDetail_04
	(
		BomDetId int primary key,
		Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		StruType varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
		Op int,
		Ref varchar(50) COLLATE  Chinese_PRC_CI_AS,
		RateQty decimal(18, 8),
		ScrapPct decimal(18, 8),
		Location varchar(50),
		BackFlushMethod varchar(50) COLLATE  Chinese_PRC_CI_AS
	)

	create table #tempOrderLocTrans_04
	(
		RowId int identity(1, 1) primary key,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
		BomDet int,
		Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
		BaseUom varchar(5) COLLATE  Chinese_PRC_CI_AS,
		UC decimal(18, 8),
		Op int,
		IOType varchar(50) COLLATE  Chinese_PRC_CI_AS,
		TransType varchar(50) COLLATE  Chinese_PRC_CI_AS,
		BomUnitQty decimal(18, 8),   --Bom用量
		UnitQty decimal(18, 8),      --和基本单位的转换率
		OrderQty decimal(18, 8),
		Loc varchar(50) COLLATE  Chinese_PRC_CI_AS,
		RejLoc varchar(50) COLLATE  Chinese_PRC_CI_AS,
		HuLotSize decimal(18, 8),
		BackFlushMethod varchar(50) COLLATE  Chinese_PRC_CI_AS,
		TagNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Shelf varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Cartons int
	)
	
	begin try
		begin try
			if (@ProdLine is null)
			begin
				RAISERROR(N'生产线不能为空。', 16, 1) 			
			end
		
			if (@FGItem is null)
			begin
				RAISERROR(N'成品不能为空。', 16, 1) 			
			end

			if (@OrderQty <= 0)
			begin
				RAISERROR(N'工单数量不能小于等于0。', 16, 1) 			
			end

			if (@StartTime is null)
			begin
				set @StartTime = DATEADD(HOUR, -12, @DateTimeNow)
			end

			if (@WindowTime is null)
			begin
				set @WindowTime = DATEADD(HOUR, 12, @DateTimeNow)
			end

			if (@Priority is null)
			begin
				set @Priority = 'Normal'
			end
			else if @Priority not in ('Normal', 'Urgent')
			begin
				set @ErrorMsg = N'优先级[' + @Priority + N']不正确。'
				RAISERROR(@ErrorMsg, 16, 1)
			end

			if (@OrderSubType is null)
			begin
				set @OrderSubType = 'Nml'
			end
			else if @OrderSubType not in ('Nml', 'Rwo')
			begin
				set @ErrorMsg = N'生产单子类型[' + @OrderSubType + N']不正确。'
				RAISERROR(@ErrorMsg, 16, 1)
			end

			select @Bom = ISNULL(Bom, Code), @FGBaseUom = Uom, @FGIsMes = IsMes from Item where Code = @FGItem

			if (@Bom is null)
			begin
				set @ErrorMsg = N'成品物料代码[' + @FGItem + N']不存在。'
				RAISERROR(@ErrorMsg, 16, 1)
			end

			select @FGBomUom = Uom from BomMstr where Code = @Bom
	
			if (@FGBomUom is null)
			begin
				set @ErrorMsg = N'成品[' + @FGItem + N']的Bom[' + @Bom + N']不存在。'
				RAISERROR(@ErrorMsg, 16, 1)
			end

			select @Flow = mstr.Code, @FlowDetId = det.Id, @FGItemDesc = i.Desc1, @FGUC = HuLotSize, @FGUom = det.Uom, @Routing = mstr.Routing,
			@DefaultLocFrom = ISNULL(det.LocFrom, mstr.LocFrom), @DefaultLocTo = ISNULL(det.LocTo, mstr.LocTo), @FGFullfillUC = mstr.FulfillUC
			from FlowMstr as mstr inner join FlowDet as det on mstr.Code = det.Flow
			inner join Item as i on det.Item = i.Code
			where mstr.Code = @ProdLine and det.Item = @FGItem and mstr.[Type] = 'Production'

			if (@Flow is null and @ForceMatchProdLine = 0)
			begin
				--没有找到完全匹配的生产线，用左匹配找一条
				select top 1 @Flow = mstr.Code, @FlowDetId = det.Id, @FGItemDesc = i.Desc1, @FGUC = HuLotSize, @FGUom = det.Uom, @Routing = mstr.Routing,
				@DefaultLocFrom = ISNULL(det.LocFrom, mstr.LocFrom), @DefaultLocTo = ISNULL(det.LocTo, mstr.LocTo), @FGFullfillUC = mstr.FulfillUC
				from FlowMstr as mstr inner join FlowDet as det on mstr.Code = det.Flow
				inner join Item as i on det.Item = i.Code
				where mstr.Code like '%'+ @ProdLine and det.Item = @FGItem and mstr.[Type] = 'Production'
			end

			if (@Flow is null)
			begin
				set @ErrorMsg = N'生产线[' + @ProdLine + N']不存在或生产线[' + @ProdLine + N']没有维护成品[' + @FGItem + N']。'
				RAISERROR(@ErrorMsg, 16, 1) 			
			end

			--整包校验,快速生产单、返工单不考虑
			if  (@FGFullfillUC = 1 and not(@IsAutoStart = 1 and @IsAutoRelease = 1) and @OrderSubType <> 'Rwo' and @OrderQty % @FGUC > 0)
			begin
				set @ErrorMsg = N'成品[' + @FGItem + N']的订单数[' + @OrderQty + N']不是包装数[' + @FGUC + N']的整数倍。'
				RAISERROR(@ErrorMsg, 16, 1) 	
			end

			--单位转换，从生产线单位转为Bom单位，在计算Bom单位用量时使用
			if (@FGBomUom <> @FGUom)
			begin
				select @FGBomUnitQty = BaseQty / AltQty from UomConv where Item = @FGItem and AltUom = @FGUom and BaseUom = @FGBomUom

				if (@FGBomUnitQty is null)
				begin
					select @FGBomUnitQty = AltQty / BaseQty from UomConv where Item = @FGItem and AltUom = @FGBomUom and BaseUom = @FGUom
				end

				if (@FGBomUnitQty is null)
				begin
					select @FGBomUnitQty = BaseQty / AltQty from UomConv where Item is null and AltUom = @FGUom and BaseUom = @FGBomUom
				end

				if (@FGBomUnitQty is null)
				begin
					select @FGBomUnitQty = AltQty / BaseQty from UomConv where Item is null and AltUom = @FGBomUom and BaseUom = @FGUom
				end

				if (@FGBomUnitQty is null)
				begin
					set @ErrorMsg = N'没有维护成品[' + @FGItem + N']从计量单位[' + @FGUom + N']转换为计量单位[' + @FGBomUom + N']的换算关系。'
					RAISERROR(@ErrorMsg, 16, 1)
				end
			end
			else
			begin
				set @FGBomUnitQty = 1
			end

			--单位转换，从生产线单位转为基本单位
			if (@FGBomUom <> @FGBaseUom)
			begin
				select @FGUnitQty = BaseQty / AltQty from UomConv where Item = @FGItem and AltUom = @FGBomUom and BaseUom = @FGBaseUom

				if (@FGUnitQty is null)
				begin
					select @FGUnitQty = AltQty / BaseQty from UomConv where Item = @FGItem and AltUom = @FGBaseUom and BaseUom = @FGBomUom
				end

				if (@FGUnitQty is null)
				begin
					select @FGUnitQty = BaseQty / AltQty from UomConv where Item is null and AltUom = @FGBomUom and BaseUom = @FGBaseUom
				end

				if (@FGUnitQty is null)
				begin
					select @FGUnitQty = AltQty / BaseQty from UomConv where Item is null and AltUom = @FGBaseUom and BaseUom = @FGBomUom
				end

				if (@FGUnitQty is null)
				begin
					set @ErrorMsg = N'没有维护成品[' + @FGItem + N']从计量单位[' + @FGBomUom + N']转换为计量单位[' + @FGBaseUom + N']的换算关系。'
					RAISERROR(@ErrorMsg, 16, 1)
				end
			end
			else
			begin
				set @FGUnitQty = 1
			end

			--分解Bom
			insert into #tempBomDetail_04 exec ORD_GetFlatBomDetail @Bom, @DateTimeNow
		
			if not exists(select top 1 1 from #tempBomDetail_04)
			begin
				set @ErrorMsg = N'成品[' + @FGItem + N']的Bom[' + @Bom + N']没有Bom明细。'
				RAISERROR(@ErrorMsg, 16, 1)
			end

			--取默认工序
			begin try
			select @DefaultOp = PreValue from EntityOpt where PreCode = 'SeqInterval'
			end try
			begin catch
				set @DefaultOp = 1
			end catch
			--没有工序号赋值默认工序
			update #tempBomDetail_04 set Op = @DefaultOp where Op is null
			select @MinOp = MIN(OP), @MaxOp = MAX(Op) from #tempBomDetail_04

			--记录工单产出
			insert into #tempOrderLocTrans_04 
			(
			Item,
			ItemDesc,
			Uom,
			BaseUom,
			UC,
			Op,
			IOType,
			TransType,
			BomUnitQty,
			OrderQty,
			Loc,
			RejLoc,
			HuLotSize
			)
			values 
			(
			@FGITem,
			@FGItemDesc,
			@FGUom,
			@FGBaseUom,
			@FGUC,
			@MaxOp,
			'In',
			'RCT-WO',
			@FGUnitQty,
			@OrderQty,
			@DefaultLocTo,
			'Reject',
			@FGUC
			)

			--返工，把自己添加到物料中
			if (@OrderSubType = 'Rwo')
			begin 
				insert into #tempOrderLocTrans_04 
				(
				Item,
				ItemDesc,
				Uom,
				BaseUom,
				UC,
				Op,
				IOType,
				TransType,
				BomUnitQty,
				OrderQty,
				Loc,
				RejLoc,
				HuLotSize
				)
				values 
				(
				@FGITem,
				@FGItemDesc,
				@FGUom,
				@FGBaseUom,
				@FGUC,
				@MinOp,
				'OUT',
				'RCT-WO',
				@FGUnitQty,
				@OrderQty,
				'Reject',
				'Reject',
				@FGUC
				)
			end

			--记录工单投入
			insert into #tempOrderLocTrans_04 
			(
			Item,
			ItemDesc,
			BomDet,
			Uom,
			BaseUom,
			UC,
			Op,
			IOType,
			TransType,
			BomUnitQty,
			OrderQty,
			Loc,
			RejLoc,
			BackFlushMethod
			)
			select
			Item,
			i.Desc1,
			bom.BomDetId,
			bom.Uom,
			i.Uom,
			i.UC,
			bom.Op,
			'Out',
			'ISS-WO',
			bom.RateQty,
			bom.RateQty * @OrderQty * @FGBomUnitQty,
			ISNULL(bom.Location, ISNULL(rd.LocFrom, @DefaultLocFrom)),
			'Reject',
			bom.BackFlushMethod
			from #tempBomDetail_04 as bom 
			inner join Item as i on bom.Item = i.Code
			left join RoutingDet as rd on rd.Routing = @Routing and rd.Op = bom.Op and ISNULL(rd.Ref, '') = ISNULL(bom.Ref, '')

			drop table #tempBomDetail_04

			--取工位和货架
			update olt set TagNo = s.TagNo, Shelf = s.Code
			from #tempOrderLocTrans_04 as olt inner join RoutingDet as rd on rd.Routing = @Routing and olt.Op = rd.Op
			inner join Mes_ShelfItem as si on si.Item = olt.Item and si.IsActive = 1
			inner join Mes_Shelf as s on si.Shelf = s.Code and s.TagNo = rd.tagNo

			--计算单位换算（Bom单位转为基本单位）
			update #tempOrderLocTrans_04 set UnitQty = 1 where Uom = BaseUom and UnitQty is null
			update olt set UnitQty = c.BaseQty / c.AltQty
			from #tempOrderLocTrans_04 as olt inner join UomConv as c on olt.Item = c.Item and olt.Uom = c.AltUom and olt.BaseUom = c.BaseUom
			where olt.UnitQty is null
			update olt set UnitQty =  c.AltQty / c.BaseQty
			from #tempOrderLocTrans_04 as olt inner join UomConv as c on olt.Item = c.Item and olt.Uom = c.BaseUom and olt.BaseUom = c.AltUom
			where olt.UnitQty is null
			update olt set UnitQty = c.BaseQty / c.AltQty
			from #tempOrderLocTrans_04 as olt inner join UomConv as c on olt.Uom = c.AltUom and olt.BaseUom = c.BaseUom 
			where olt.UnitQty is null and c.Item is null
			update olt set UnitQty =  c.AltQty / c.BaseQty
			from #tempOrderLocTrans_04 as olt inner join UomConv as c on olt.Uom = c.BaseUom  and olt.BaseUom = c.AltUom 
			where olt.UnitQty is null and c.Item is null
		
			if exists(select top 1 1 from #tempOrderLocTrans_04 where UnitQty is null)
			begin
				select top 1 @ErrorMsg = N'没有维护原材料[' + Item + N']从计量单位[' + Uom + N']转换为计量单位[' + BaseUom + N']的换算关系。' from #tempOrderLocTrans_04 where UnitQty is null
				RAISERROR(@ErrorMsg, 16, 1)
			end

			--Bom总用量、单位用量、箱数转为基本单位的用量
			update #tempOrderLocTrans_04 set OrderQty = OrderQty * UnitQty, BomUnitQty = BomUnitQty * UnitQty, Cartons = ROUND(OrderQty / UC, 0)

			exec GetNextSequence 'ORD', @OrderNoSeq output

			set @OrderNo = 'ORD' + replicate('0', 9 - len(@OrderNoSeq)) + convert(varchar(50), @OrderNoSeq)

		end try
		begin catch
			set @ErrorMsg = N'数据准备出现异常：' + Error_Message() 
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch

		set @Trancount = @@Trancount
		begin try
			if @Trancount = 0
			begin
				begin tran
			end

			insert into OrderMstr
			(
			OrderNo,
			RefOrderNo,
			Flow, 
			Routing, 
			StartTime, 
			WindowTime, 
			[Status], 
			[Priority], 
			[Type], 
			SubType,
			PartyFrom,
			PartyTo,
			LocFrom,
			LocTo,
			IsAutoRelease, 
			IsAutoStart,
			IsAutoShip,
			IsAutoReceive,
			IsAutoBill,
			IsShowPrice,
			NeedPrintOrder,
			NeedPrintAsn,
			NeedPrintRcpt,
			AllowExceed, 
			FulfillUC,
			IsPrinted,
			RecTemplate,
			OrderTemplate,
			CreateDate,
			CreateUser,
			LastModifyDate,
			LastModifyUser,
			ReleaseDate,
			ReleaseUser,
			StartDate,
			StartUser,
			CompleteDate,
			CompleteUser,
			CheckDetOpt,
			AllowCreateDetail,
			IsShipScan,
			IsRecScan,
			CreateHuOpt,
			AutoPrintHu,
			IsOddCreateHu,
			IsAutoCreatePL,
			NeedInspect,
			IsGrFifo,
			AntiResolveHu,
			MaxOnlineQty, 
			IsNewItem,
			HuTemplate,
			AllowRepeatlyExceed,
			IsPickFromBin,
			IsShipByOrder,
			IsAsnUniqueReceipt,
			IsSubcontract,
			IsPLCreate,
			IsAdditional,
			IsMes,
			[Version]
			)
			select 
			@OrderNo,
			@RefOrderNo,
			@Flow, 
			@Routing, 
			@StartTime, 
			@WindowTime, 
			CASE WHEN (@IsAutoRelease = 1 or (@IsAutoRelease is null and IsAutoRelease = 1)) and (@IsAutoStart = 1 or IsAutoStart = 1) THEN 'In-Process' WHEN @IsAutoRelease = 1 or (@IsAutoRelease is null and IsAutoRelease = 1) THEN 'Submit'  ELSE 'Create' END, 
			@Priority, 
			'Production', 
			@OrderSubType,
			PartyFrom,
			PartyTo,
			LocFrom,
			LocTo,
			CASE WHEN @IsAutoRelease is null THEN IsAutoRelease ELSE @IsAutoRelease END, 
			CASE WHEN @IsAutoStart is null THEN IsAutoStart ELSE @IsAutoStart END, 
			IsAutoShip,
			IsAutoReceive,
			IsAutoBill,
			IsShowPrice,
			NeedPrintOrder,
			NeedPrintAsn,
			NeedPrintRcpt,
			AllowExceed, 
			FulfillUC,
			0,
			RecTemplate,
			OrderTemplate,
			@DateTimeNow,
			@CreateUser,
			@DateTimeNow,
			@CreateUser,
			CASE WHEN @IsAutoRelease = 1 or (@IsAutoRelease is null and IsAutoRelease = 1) THEN @DateTimeNow  ELSE null END, 
			CASE WHEN @IsAutoRelease = 1 or (@IsAutoRelease is null and IsAutoRelease = 1) THEN @CreateUser  ELSE null END, 
			CASE WHEN (@IsAutoRelease = 1 or (@IsAutoRelease is null and IsAutoRelease = 1)) and (@IsAutoStart = 1 or IsAutoStart = 1) THEN @DateTimeNow  ELSE null END,
			CASE WHEN (@IsAutoRelease = 1 or (@IsAutoRelease is null and IsAutoRelease = 1)) and (@IsAutoStart = 1 or IsAutoStart = 1) THEN @CreateUser  ELSE null END,
			null,
			null,
			CheckDetOpt,
			AllowCreateDetail,
			IsShipScan,
			IsRecScan,
			CreateHuOpt,
			AutoPrintHu,
			IsOddCreateHu,
			IsAutoCreatePL,
			NeedInspect,
			IsGrFifo,
			AntiResolveHu,
			MaxOnlineQty, 
			0,
			HuTemplate,
			AllowRepeatlyExceed,
			IsPickFromBin,
			IsShipByOrder,
			IsAsnUniqueReceipt,
			0,
			0,
			CASE WHEN ISNULL(@RefOrderNo, '') = '' THEN 0 ELSE 1 END,
			IsMes,
			1
			from FlowMstr where Code = @Flow

			insert into OrderDet
			(OrderNo,
			Item,
			RefItemCode,
			ItemDesc,
			Seq,
			Uom,
			UC,
			ReqQty,
			OrderQty,
			RecQty,
			RejQty,
			ScrapQty,
			LocFrom,
			LocTo,
			Bom,
			HuLotSize,
			NeedInspect,
			CustomerItemCode,
			TFlag
			)
			select
			@OrderNo,
			@FGItem,
			RefItemCode,
			@FGItemDesc,
			1,
			@FGUom,
			UC,
			@OrderQty,
			@OrderQty,
			0,
			0,
			0,
			LocFrom,
			LocTo,
			@Bom,
			HuLotSize,
			NeedInspect,
			CustomerItemCode,
			@FGIsMes
			from FlowDet
			where Id = @FlowDetId

			set @OrderDetId = @@IDENTITY

			insert into OrderLoctrans
			(
			OrderNo,
			OrderDetId,
			Item,
			ItemDesc,
			BomDet,
			IsAssemble,
			Uom,
			Op,
			IOType,
			TransType,
			UnitQty,
			OrderQty,
			AccumQty,
			AccumRejQty,
			AccumScrapQty,
			Loc,
			RejLoc,
			HuLotSize,
			NeedPrint,
			IsShipScan,
			BackFlushMethod,
			TagNo,
			Shelf,
			Cartons
			)
			select
			@OrderNo,
			@OrderDetId,
			Item,
			ItemDesc,
			BomDet,
			1,
			Uom,
			Op,
			IOType,
			TransType,
			BomUnitQty,
			OrderQty,
			0,
			0,
			0,
			Loc,
			RejLoc,
			HuLotSize,
			1,
			0,
			BackFlushMethod,
			TagNo,
			Shelf,
			Cartons
			from #tempOrderLocTrans_04

			if @Trancount = 0 
			begin  
				commit
			end

			drop table #tempOrderLocTrans_04

			set @WorkOrderNo = @OrderNo
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
		set @ErrorMsg = N'创建工单出现异常：' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch
END
GO