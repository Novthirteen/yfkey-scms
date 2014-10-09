USE [scms]
GO

/****** Object:  StoredProcedure [dbo].[RunPurchasePlanProxy2]    Script Date: 10/09/2014 14:18:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[RunPurchasePlanProxy2]
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
		exec RunPurchasePlan2 @RunUser
	end try
	begin catch
		declare @Msg varchar(max)
		set @Msg = N'运行物料需求计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 




GO


