SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='DSS_ProcessItemInbound') 
     DROP PROCEDURE DSS_ProcessItemInbound 
GO

CREATE PROCEDURE [dbo].[DSS_ProcessItemInbound] 
(
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
	
	create table #tempItem
	(
		Id int Primary Key,
		Code varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
		Desc1 varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Desc2 varchar(50) COLLATE  Chinese_PRC_CI_AS,
		[Type] varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Routing varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Plant varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Category varchar(50) COLLATE  Chinese_PRC_CI_AS,
		IsActive bit,
		BackFlushMethod varchar(50) COLLATE  Chinese_PRC_CI_AS,
	)

	begin try
		begin try
			insert into #tempItem(Id, Code, Uom, Desc1, Desc2, [Type], Bom, Routing, Plant, Category, IsActive, BackFlushMethod)
			select Id, data1, UPPER(data2), data3, data4, CASE WHEN data5 = 'P' THEN 'P' ELSE 'M' END, CASE WHEN data6 <> '' THEN data6 ELSE null end, CASE WHEN data7 <> '' THEN data7 ELSE null end, SUBSTRING(data8, 2, 1), SUBSTRING(data8, 1, 1), CASE WHEN data9 = 'A' THEN 1 ELSE 0 END, CASE WHEN data12 = 'TL' THEN 'BatchFeed' ELSE 'GoodsReceive' END
			from DssImpHis where IsActive = 1 and ErrCount < 10 and DssInboundCtrl = 1 and EventCode = 'CREATE'

			if exists(select top 1 1 from #tempItem as tmp left join BomMstr as bm on tmp.Bom = bm.Code 
						where tmp.Bom is not null and tmp.Bom <> '' and bm.Code is null)
			begin
				update dih set Memo = 'Bom不存在。', ErrCount = ISNULL(ErrCount, 0) + 1, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempItem as tmp on dih.Id = tmp.Id
				left join BomMstr as bm on tmp.Bom = bm.Code 
				where tmp.Bom is not null and tmp.Bom <> '' and bm.Code is null

				delete tmp from #tempItem as tmp left join BomMstr as bm on tmp.Bom = bm.Code 
				where tmp.Bom is not null and tmp.Bom <> '' and bm.Code is null
			end

			if exists(select top 1 1 from #tempItem as tmp left join RoutingMstr as rm on tmp.Bom = rm.Code 
						where tmp.Bom is not null and tmp.Bom <> '' and rm.Code is null)
			begin
				update dih set Memo = 'Routing不存在。', ErrCount = ISNULL(ErrCount, 0) + 1, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
				from DssImpHis as dih inner join #tempItem as tmp on dih.Id = tmp.Id
				left join RoutingMstr as rm on tmp.Bom = rm.Code 
				where tmp.Bom is not null and tmp.Bom <> '' and rm.Code is null

				delete tmp from #tempItem as tmp left join RoutingMstr as rm on tmp.Bom = rm.Code 
				where tmp.Bom is not null and tmp.Bom <> '' and rm.Code is null
			end

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

			update dih set IsActive = 0, LastModifyUser = @CreateUser, LastModifyDate = @DateTimeNow
			from DssImpHis as dih inner join #tempItem as tmp on dih.Id = tmp.Id

			update i set Uom = tmp.Uom, i.Desc1 = tmp.Desc1, i.Desc2 = tmp.Desc2, i.[Type] = tmp.[Type], i.Bom = tmp.Bom, i.Routing = tmp.Routing, 
			i.Category = tmp.Category, i.IsActive = tmp.IsActive, i.BackflushMethod = tmp.BackflushMethod, i.LastModifyUser = @CreateUser, i.LastModifyDate = @DateTimeNow
			from Item as i inner join #tempItem as tmp on i.Code = tmp.Code
		
			insert into Item(Code, [Type], Desc1, Desc2, Uom, UC, Bom, Routing, IsActive, LastModifyDate, LastModifyUser, Category, BackflushMethod, Plant)
			select tmp.Code, tmp.[Type], tmp.Desc1, tmp.Desc2, tmp.Uom, 1, tmp.Bom, tmp.Routing, tmp.IsActive, @DateTimeNow, @CreateUser, tmp.Category, tmp.BackflushMethod, tmp.Plant
			from #tempItem as tmp left join Item as i on i.Code = tmp.Code
			where i.Code is null

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
		set @ErrorMsg = N'导入零件主数据出现异常：' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempItem
END
GO