SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='SplitWeeklyCustSchedule') 
DROP PROCEDURE SplitWeeklyCustSchedule
GO
CREATE PROCEDURE [dbo].SplitWeeklyCustSchedule
--WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateTimeNow datetime
	declare @DateNow datetime
	declare @Msg nvarchar(MAX)
	declare @trancount int
	declare @RowId int
	declare @MaxRowId int
	declare @SplitSymbol char(1)

	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), @DateTimeNow, 121))
	set @Msg = ''
	set @trancount = @@trancount
	set @SplitSymbol = ','
	
	begin try
		if @trancount = 0
		begin
            begin tran
        end

		create table #tempFlow
		(
			RowId int identity(1, 1),
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			MaxMstrId int
		)

		create table #tempWeekDay
		(
			RowId int identity(1, 1),
			[DateDiff] int
		)

		create table #tempCustomerScheduleDet
		(
			RowId int identity(1, 1) Primary Key,
			DetId int,
			MstrId int,
			Flow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ShipFlow varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
			ItemRef varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Qty int,
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			UC decimal(18, 8),
			Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StartTime datetime,
			WindowTime datetime
		)
	
		insert into #tempFlow(Flow, MaxMstrId)
		select Flow, MAX(Id) from CustScheduleMstr where [Type] = 'Weekly' and [Status] = 'Submit' group by Flow

		select @RowId = MIN(RowId), @MaxRowId = MAX(RowId) from #tempFlow
		while (@RowId <= @MaxRowId)
		begin
			declare @Flow varchar(50)
			declare @MaxMstrId int
			declare @DateFirst int
			declare @WorkDate varchar(50)
			declare @Count int

			select @Flow = Flow, @MaxMstrId = MaxMstrId from #tempFlow where RowId = @RowId
			select @DateFirst = ISNULL(DateFst, 1), @WorkDate = ISNULL(WorkDate, '1,2,3,4,5,6,7') from FlowMstr where Code = @Flow
			 
			truncate table #tempWeekDay
			if (charindex(@SplitSymbol, @WorkDate) <> 0)
			begin
				while(charindex(@SplitSymbol, @WorkDate) <> 0)
				begin
					insert #tempWeekDay([DateDiff]) values (substring(@WorkDate, 1, charindex(@SplitSymbol, @WorkDate) - 1))
					set @WorkDate = stuff(@WorkDate, 1, charindex(@SplitSymbol, @WorkDate), ' ')
				end
			end
	
			insert #tempWeekDay([DateDiff]) values (Ltrim(@WorkDate))
			update #tempWeekDay set [DateDiff] = LTRIM(RTRIM([DateDiff]))
			select @Count = COUNT(1) from #tempWeekDay

			insert into #tempCustomerScheduleDet(DetId, MstrId, Flow, ShipFlow, Item, ItemDesc, ItemRef, Qty, Uom, UC, Location, WindowTime)
			select det.Id, @MaxMstrId, det.Flow, det.ShipFlow, det.Item, det.ItemDesc, det.ItemRef, det.Qty / @Count, det.Uom, det.UC, det.Loc,
			DATEADD(DAY, CASE WHEN (tmp.[DateDiff] - @DateFirst + 1) <= 0 THEN (tmp.[DateDiff] - @DateFirst + 1) + 7 ELSE (tmp.[DateDiff] - @DateFirst + 1) END - 1, det.DateFrom) 
			from CustScheduleDet as det, #tempWeekDay as tmp
			where ScheduleId = @MaxMstrId
			order by det.Id, tmp.RowId

			--数量更新为相同日期的日计划
			update tmp set Qty = det.Qty
			from #tempCustomerScheduleDet as tmp 
			inner join CustScheduleDet as det on tmp.Flow = det.Flow and tmp.Item = det.Item and tmp.WindowTime = det.DateFrom
			where det.ScheduleId in (select MAX(Id) from CustScheduleMstr where Flow = @Flow and Type = 'Daily' and [Status] = 'Submit')

			set @RowId = @RowId + 1
		end

		update det set StartTime = DATEADD(day, -ISNULL(fMstr.MRPLeadTime, 0), det.WindowTime)
		from #tempCustomerScheduleDet as det inner join FlowMstr as fMstr on det.Flow = fMstr.Code

		--update #tempCustomerScheduleDet set Qty = ceiling(Qty / UC) * UC
		--where Qty > 0 and UC > 0
			
		update det set Qty = CASE WHEN Qty + tmp2.DiffQty > 0 THEN Qty + tmp2.DiffQty ELSE 0 END
		from #tempCustomerScheduleDet as det
		inner join (select DetId, Max(RowId) as RowId from #tempCustomerScheduleDet group by DetId) as tmp1 on det.Rowid = tmp1.RowId
		inner join  (select det.Id, (det.Qty - tmp.Qty) as DiffQty from CustScheduleDet as det 
					inner join (select DetId, SUM(Qty) as Qty from #tempCustomerScheduleDet group by DetId) as tmp on det.Id = tmp.DetId)
					as tmp2 on tmp2.Id = tmp1.DetId

		truncate table MRP_SplitWeeklyCustScheduleDet
		insert into MRP_SplitWeeklyCustScheduleDet(DetId, MstrId, Flow, ShipFlow, Item, ItemDesc, ItemRef, Qty, Uom, UC, Location, StartTime, WindowTime)
		select DetId, MstrId, Flow, ShipFlow, Item, ItemDesc, ItemRef, Qty, Uom, UC, Location, StartTime, WindowTime from #tempCustomerScheduleDet
		
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
       
		set @Msg = N'运行发运计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


