SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='RunWeeklyMRP') 
     DROP PROCEDURE RunWeeklyMRP
GO

CREATE PROCEDURE [dbo].RunWeeklyMRP
(
	@RunUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	declare @DateTimeNow datetime
	declare @Msg nvarchar(MAX)
	declare @trancount int
	declare @BatchNo int

	set @DateTimeNow = GETDATE()
	set @Msg = ''
	set @trancount = @@trancount

	begin try
		exec GetNextSequence 'RunWeeklyMRP', @BatchNo output

		truncate table MRP_WeeklyShipPlan
		truncate table MRP_WeeklyProductionPlan

		exec RunWeeklyShipPlan @BatchNo, @RunUser, @DateTimeNow
		exec RunWeeklyProductionPlan @BatchNo, @RunUser, @DateTimeNow
	end try
	begin catch
		set @Msg = N'运行周物料需求计划异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
		return
	end catch 
END 


