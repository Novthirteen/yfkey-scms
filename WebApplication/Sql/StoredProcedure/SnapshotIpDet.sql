SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='SnapshotIpDet') 
DROP PROCEDURE SnapshotIpDet
GO
CREATE PROCEDURE [dbo].SnapshotIpDet
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
		
		--if not exists(select top 1 1 from MRP_IpDetSnapShot) or
		--	exists(select top 1 1 from MRP_IpDetSnapShot where EffDate <> @DateNow)
		--begin
			truncate table MRP_IpDetSnapShot
			insert into MRP_IpDetSnapShot(IpDetId, IpNo, Flow, Item, StartTime, WindowTime, Qty, EffDate)
			select iDet.Id, imstr.IpNo, oMstr.Flow, oDet.Item, convert(varchar(10), oMstr.StartTime, 121), convert(varchar(10), oMstr.WindowTime, 121), iDet.Qty - ISNULL(iDet.RecQty, 0), @DateNow
			from IpDet as iDet
			inner join IpMstr as iMstr on iDet.IpNo = iMstr.IpNo
			inner join OrderLocTrans as oTrans on iDet.OrderLocTransId = oTrans.Id
			inner join OrderDet as oDet on oTrans.OrderDetId = oDet.Id
			inner join OrderMstr as oMstr on oDet.OrderNo = oMstr.OrderNo
			where oMstr.[Type] in ('Procurement', 'Transfer') and iMstr.[Status] = 'Create' and iMstr.[Type] = 'Nml'
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
		set @Msg = N'øÏ’’‘⁄Õæ“Ï≥£' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


