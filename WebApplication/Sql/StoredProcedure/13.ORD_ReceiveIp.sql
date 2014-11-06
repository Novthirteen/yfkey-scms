SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='ORD_ReceiveIp') 
     DROP PROCEDURE ORD_ReceiveIp 
GO

CREATE PROCEDURE [dbo].[ORD_ReceiveIp] 
(
	@ReceiveIpTable ReceiveIpType readonly,
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
	
	create table #tempOrder_13
	(
	)

	begin try
		begin try
			if not exists(select top 1 1 from @ReceiveIpTable where RecQty > 0)
			begin
				RAISERROR(N'收货明细不能为空。', 16, 1) 
			end

			select om.OrderNo, om.Type, om.Status, om.[Version], od.Id, od.Item, od.ItemDesc, od.OrderQty, od.RecQty, olt.Id, im.IpNo, im.Status, id.Id, id.Qty
			from @ReceiveIpTable as tmp 
			inner join IpDet as id on tmp.IpDetId = id.Id
			inner join IpMstr as im on id.IpNo = im.IpNo
			inner join OrderLocTrans as olt on id.OrderLocTransId = olt.Id
			inner join OrderDet as od on olt.OrderDetId = od.Id
			inner join OrderMstr as om on om.OrderNo = om.OrderNo
			

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
		set @ErrorMsg = N'ASN收货出现异常：' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempPlanBill_11
END
GO