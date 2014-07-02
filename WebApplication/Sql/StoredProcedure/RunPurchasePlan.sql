SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunPurchasePlan') 
     DROP PROCEDURE RunPurchasePlan
GO

CREATE PROCEDURE [dbo].RunPurchasePlan
(
	@Plant varchar(50),
	@RunUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateTimeNow datetime = GetDate()
	declare @DateNow datetime = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow))
	declare @Msg nvarchar(MAX) = ''
	declare @trancount int = @@trancount
	declare @BatchNo int
	declare @RowId int
	declare @MaxRowId int
	declare @ProdLine varchar(50)
	declare @MaxMstrId int
	declare @MinPlandate datetime

	exec GetNextSequence 'RunPurchasePlan', @BatchNo output
	begin try
		create table #tempMsg
		(
			Lvl varchar(50),
			ProdLine varchar(50),
			Item varchar(50),
			Uom varchar(50),
			Qty decimal(18, 8),
			PlanDate datetime,
			Msg varchar(500)
		)

		create table #tempEffShiftPlan
		(
			Id int Primary Key,
			MstrId int,
			ProdLine varchar(50),
			Item varchar(50),
			ItemDesc varchar(100),
			RefItemCode varchar(50),
			Qty decimal(18, 8),
			Uom varchar(5),
			BaseUom varchar(5),
			UnitQty decimal(18, 8),   --Qty * UnitQty = 基本单位数量
			Location varchar(50),
			PlanDate datetime
		)

		create table #tempProdLine
		(
			RowId int Identity(1, 1),
			ProdLine varchar(50),
		)

		-----------------------------↓获取班产计划-----------------------------
		--选取开始日期大于等于当天的所有客户日程
		insert into #tempEffShiftPlan(Id, MstrId, ProdLine, Item, ItemDesc, RefItemCode, Qty,
		Uom, BaseUom, Location, PlanDate)
		select det.Id, mstr.Id as MstrId, mstr.ProdLine, det.Item, det.ItemDesc, det.RefItemCode, det.Qty,
		det.Uom, i.Uom as BaseUom, fMstr.LocTo, det.PlanDate
		from MRP_ShiftPlanDet as det inner join MRP_ShiftPlanMstr as mstr on det.PlanId = mstr.Id
		inner join FlowMstr as fMstr on mstr.ProdLine = fMstr.Code
		inner join Item as i on det.Item = i.Code
		where mstr.[Status] = 'Submit' and PlanDate >= @DateNow 

		if not exists(select top 1 1 from #tempEffShiftPlan)
		begin
			insert into #tempMsg(Lvl, Msg) values('Info', N'没有找到有效的班产计划')
			return			
		end

		--取最新班产计划的所有明细，旧班产计划的明细要删除和最新班产计划重复的明细，依次循环得到有效班产计划
		insert into #tempProdLine(ProdLine) select distinct ProdLine from #tempEffShiftPlan
		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempProdLine
		while @RowId <= @MaxRowId
		begin
			select @ProdLine = ProdLine from #tempProdLine where RowId = @RowId

			select @MaxMstrId = MAX(MstrId) from #tempEffShiftPlan where ProdLine = @ProdLine
			select @MinPlandate = MIN(Plandate) from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId = @MaxMstrId
			while exists(select top 1 1 from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId < @MaxMstrId and Plandate > @MinPlandate)
			begin
				delete from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId < @MaxMstrId and Plandate > @MinPlandate
				if exists(select top 1 1 from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId < @MaxMstrId)
				begin --没有更小版本的客户日程，跳出循环
					break;
				end
				else
				begin --取下一个版本的客户日程
					select @MaxMstrId = MAX(MstrId) from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId < @MaxMstrId
					select @MinPlandate = MIN(Plandate) from #tempEffShiftPlan where ProdLine = @ProdLine and MstrId = @MaxMstrId
				end
			end

			set @RowId = @RowId + 1
		end

		--计算单位换算
		update #tempEffShiftPlan set UnitQty = 1 where Uom = BaseUom
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempEffShiftPlan as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.AltUom and det.BaseUom = c.BaseUom
		where det.UnitQty is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempEffShiftPlan as det inner join UomConv as c on det.Item = c.Item and det.Uom = c.BaseUom and det.BaseUom = c.AltUom
		where det.UnitQty is null
		update det set UnitQty = c.BaseQty / c.AltQty
		from #tempEffShiftPlan as det inner join UomConv as c on det.Uom = c.AltUom and det.BaseUom = c.BaseUom 
		where det.UnitQty is null and c.Item is null
		update det set UnitQty =  c.AltQty / c.BaseQty
		from #tempEffShiftPlan as det inner join UomConv as c on det.Uom = c.BaseUom  and det.BaseUom = c.AltUom 
		where det.UnitQty is null and c.Item is null

		--删除没有维护单位换算的物料
		insert into #tempMsg(Lvl, ProdLine, Item, Qty, Uom, PlanDate, Msg) 
		select 'Error', ProdLine, Item, Qty, Uom, Location, PlanDate, N'产品没有维护单位换算' from #tempEffShiftPlan where UnitQty is null
		delete from #tempEffShiftPlan where UnitQty is null
		-----------------------------↑获取班产计划-----------------------------


		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempProdLine
		while @RowId <= @MaxRowId
		begin
			
			set @RowId = @RowId + 1
		end

		if @trancount = 0
		begin
			begin tran
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
       
		set @Msg = N'运行主生产计划异常' + Error_Message()
		insert into MRP_RunShipPlanLog(BatchNo, EffDate, Lvl, Msg, CreateDate, CreateUser) values(@BatchNo, @DateNow, 'Error', @Msg, @DateTimeNow, @RunUser)
		RAISERROR(@Msg, 16, 1) 

	end catch 
END 


