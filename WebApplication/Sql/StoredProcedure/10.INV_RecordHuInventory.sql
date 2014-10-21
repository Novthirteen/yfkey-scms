SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='INV_RecordHuInventory') 
     DROP PROCEDURE INV_RecordHuInventory 
GO

CREATE PROCEDURE [dbo].[INV_RecordHuInventory] 
(
	@CreateUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN
	set nocount on
	declare @DateTimeNow datetime
	declare @ErrorMsg nvarchar(MAX)
	declare @trancount int
	declare @RowId int
	declare @MaxRowId int

	set @DateTimeNow = GetDate()
	set @trancount = @@trancount

	create table #tempInventoryHuIdIn_04
	(
		RowId int Identity(1, 1) primary key,
		HuId varchar(50),
		Location varchar(50)
	)

	create table #tempInventoryHuIdOut_04
	(
		RowId int Identity(1, 1) primary key,
		HuId varchar(50),
		Location varchar(50)
	)

	begin try
		if not exists(select top 1 1 FROM tempdb.sys.objects WHERE type = 'U' AND name like '#tempHuInventoryIO%') 
		begin
			set @ErrorMsg = '没有定义出入库参数表。'
			RAISERROR(@ErrorMsg, 16, 1) 

			--代码不会执行到这里
			create table #tempHuInventoryIO
			(
				RowId int Identity(1, 1) primary key,
				HuId varchar(50),
				Location varchar(50),
				Qty decimal(18, 8)
			)
		end
	end try
	begin catch
		set @ErrorMsg = N'更新条码库存出现异常：' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempInventoryHuIdIn_04
	drop table #tempInventoryHuIdOut_04
END
GO