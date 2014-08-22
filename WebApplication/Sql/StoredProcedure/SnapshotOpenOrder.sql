SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='SnapshotOpenOrder') 
DROP PROCEDURE SnapshotOpenOrder
GO
CREATE PROCEDURE [dbo].SnapshotOpenOrder
WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateNow datetime
	declare @Msg nvarchar(MAX)
	declare @trancount int
	set @DateNow = CONVERT(datetime, CONVERT(varchar(10), DATEADD(hour, -6, GetDate()), 121))
	set @Msg = ''
	set @trancount = @@trancount
	
	begin try
		if @trancount = 0
		begin
            begin tran
        end
		
		--if not exists(select top 1 1 from MRP_OpenOrderSnapShot) or
		--	exists(select top 1 1 from MRP_OpenOrderSnapShot where EffDate <> @DateNow)
		--begin
			truncate table MRP_OpenOrderSnapShot
			insert into MRP_OpenOrderSnapShot(OrderNo, Item, Flow, OrderType, StartTime, WindowTime, OrderQty, ShipQty, RecQty, EffDate)
			select det.OrderNo, det.Item, mstr.Flow, mstr.[Type], mstr.StartTime, mstr.WindowTime, SUM(ISNULL(det.OrderQty, 0)), SUM(ISNULL(det.ShipQty, 0)), SUM(ISNULL(det.RecQty, 0)), @DateNow
			from OrderMstr as mstr inner join OrderDet as det on mstr.OrderNo = det.OrderNo
			where mstr.[Status] in ('Submit', 'In-Process') and mstr.SubType in ('Nml') and ((det.OrderQty > det.RecQty) or (det.OrderQty > 0 and det.RecQty is null))
			group by det.OrderNo, det.Item, mstr.Flow, mstr.[Type], mstr.StartTime, mstr.WindowTime
		--end

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
		set @Msg = N'øÏ’’Open Order“Ï≥£' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


