SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='ORD_GetFlatBomDetail') 
     DROP PROCEDURE ORD_GetFlatBomDetail
GO

CREATE PROCEDURE [dbo].ORD_GetFlatBomDetail
(
	@BomCode varchar(50),
	@EffDate datetime
) --WITH ENCRYPTION
AS 
BEGIN 
	set nocount on

	declare @Msg nvarchar(MAX)
	declare @MaxRowId int
	declare @ExpandLevel int

	set @Msg = ''
	set @ExpandLevel = 1

	create table #tempBomDetail_06
	(
		RowId int identity(1, 1) primary key,
		BomDetId int,
		Bom varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		StruType varchar(50) COLLATE  Chinese_PRC_CI_AS,
		Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
		Op int,
		Ref varchar(50),
		RateQty decimal(18, 8),
		ScrapPct decimal(18, 8),
		Location varchar(50) COLLATE  Chinese_PRC_CI_AS,
		BackFlushMethod varchar(50) COLLATE  Chinese_PRC_CI_AS
	)

	begin try
		insert into #tempBomDetail_06(BomDetId, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Location, BackFlushMethod)
		select Id, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Loc, ISNULL(BackFlushMethod, 'GoodsReceive') from BomDet with(NOLOCK)
		where Bom = @BomCode and StartDate <= @EffDate and (EndDate >= @EffDate or EndDate is null)

		select @MaxRowId = MAX(RowId) from #tempBomDetail_06

		while exists(select top 1 1 from #tempBomDetail_06 where StruType = 'X')
		begin
			insert into #tempBomDetail_06(BomDetId, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Location, BackFlushMethod)
			select Id, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Loc, ISNULL(BackFlushMethod, 'GoodsReceive') from BomDet with(NOLOCK)
			where Bom in (select Item from #tempBomDetail_06 where StruType = 'X') 
			and StartDate <= @EffDate and (EndDate >= @EffDate or EndDate is null)

			delete from #tempBomDetail_06 where RowId <= @MaxRowId and StruType = 'X'
			select @MaxRowId = MAX(RowId) from #tempBomDetail_06
			if (@ExpandLevel >= 99)
			begin
				RAISERROR(N'Bom分解超过99层，可能有循环。', 16, 1) 
			end
			else
			begin
				set @ExpandLevel = @ExpandLevel + 1
			end
		end

		select BomDetId, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Location, BackFlushMethod from #tempBomDetail_06
	end try
	begin catch
		set @Msg = N'Bom分解出现异常：' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 

	drop table #tempBomDetail_06
END 


