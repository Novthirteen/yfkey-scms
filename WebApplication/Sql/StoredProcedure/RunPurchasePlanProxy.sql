SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunPurchasePlanProxy') 
     DROP PROCEDURE RunPurchasePlanProxy
GO

CREATE PROCEDURE [dbo].RunPurchasePlanProxy
(
	@RunUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	begin try	
		exec SnapshotLocationDet
		exec SnapshotIpDet
		exec SnapshotOpenOrder
		exec RunPurchasePlan @RunUser
	end try
	begin catch
        if @trancount = 0
        begin
            rollback
        end 
       
		set @Msg = N'运行物料需求计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


