SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='GetFlatBomDetail') 
     DROP PROCEDURE GetFlatBomDetail
GO

CREATE PROCEDURE [dbo].GetFlatBomDetail
(
	@BomCode varchar(50),
	@EffDate datetime
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
			Bom varchar(50),
			Item varchar(50),
			StruType varchar(50),
			Uom varchar(5),
			RateQty decimal(18, 8),
			ScrapPct decimal(18, 8),
			BackFlushMethod varchar(50)
		)

		insert into #tempBomDetail(Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod)
		select Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod from BomDet with(NOLOCK)
		where Bom = @BomCode and StartDate <= @EffDate and (EndDate >= @EffDate or EndDate is null)

		select @MaxRowId = MAX(RowId) from #tempBomDetail

		while exists(select top 1 1 from #tempBomDetail where StruType = 'X')
		begin
			insert into #tempBomDetail(Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod)
			select Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod from BomDet with(NOLOCK)
			where Bom in (select Item from #tempBomDetail where StruType = 'X') 
			and StartDate <= @EffDate and (EndDate >= @EffDate or EndDate is null)

			delete from #tempBomDetail where RowId <= @MaxRowId and StruType = 'X'
			select @MaxRowId = MAX(RowId) from #tempBomDetail
			if (@ExpandLevel >= 99)
			begin
				RAISERROR(N'Bom分解超过99层，可能有循环。', 16, 1) 
			end
			else
			begin
				set @ExpandLevel = @ExpandLevel + 1
			end
		end

		select Bom, Item, StruType, Uom, RateQty, ScrapPct, BackFlushMethod from #tempBomDetail
		drop table #tempBomDetail
	end try
	begin catch
		set @Msg = N'Bom分解出现异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


