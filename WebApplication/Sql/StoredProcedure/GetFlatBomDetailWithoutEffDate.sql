SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='GetFlatBomDetailWithoutEffDate') 
     DROP PROCEDURE GetFlatBomDetailWithoutEffDate
GO

CREATE PROCEDURE [dbo].GetFlatBomDetailWithoutEffDate
(
	@BomCode varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on
	begin try
		declare @Msg nvarchar(MAX)
		declare @MaxRowId int
		declare @ExpandLevel int

		set @Msg = ''
		set @ExpandLevel = 1

		create table #tempBomDetail
		(
			RowId int identity(1, 1) primary key,
			Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StruType varchar(50) COLLATE  Chinese_PRC_CI_AS,
			Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
			RateQty decimal(18, 8),
			ScrapPct decimal(18, 8),
			BackFlushMethod varchar(50) COLLATE  Chinese_PRC_CI_AS,
			StartDate datetime,
			EndDate datetime
		)

		insert into #tempBomDetail(Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod, StartDate, EndDate)
		select Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod, StartDate, EndDate from BomDet with(NOLOCK)
		where Bom = @BomCode

		select @MaxRowId = MAX(RowId) from #tempBomDetail

		while exists(select top 1 1 from #tempBomDetail where StruType = 'X')
		begin
			insert into #tempBomDetail(Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod, StartDate, EndDate)
			select Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod, StartDate, EndDate from BomDet with(NOLOCK)
			where Bom in (select Item from #tempBomDetail where StruType = 'X') 

			delete from #tempBomDetail where RowId <= @MaxRowId and StruType = 'X'
			select @MaxRowId = MAX(RowId) from #tempBomDetail
			if (@ExpandLevel >= 99)
			begin
				RAISERROR(N'Bom�ֽⳬ��99�㣬������ѭ����', 16, 1) 
			end
			else
			begin
				set @ExpandLevel = @ExpandLevel + 1
			end
		end

		select Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod, StartDate, EndDate from #tempBomDetail

		drop table #tempBomDetail
	end try
	begin catch
		set @Msg = N'Bom�ֽ�����쳣��' + N'Bom����[' + @BomCode + ']��' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


