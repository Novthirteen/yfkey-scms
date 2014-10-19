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
	begin try
		declare @Msg nvarchar(MAX)
		declare @MaxRowId int
		declare @ExpandLevel int

		set @Msg = ''
		set @ExpandLevel = 1

		create table #tempBomDetail
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

		insert into #tempBomDetail(BomDetId, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Location, BackFlushMethod)
		select Id, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Loc, BackFlushMethod from BomDet with(NOLOCK)
		where Bom = @BomCode and StartDate <= @EffDate and (EndDate >= @EffDate or EndDate is null)

		select @MaxRowId = MAX(RowId) from #tempBomDetail

		while exists(select top 1 1 from #tempBomDetail where StruType = 'X')
		begin
			insert into #tempBomDetail(BomDetId, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Location, BackFlushMethod)
			select Id, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Loc, BackFlushMethod from BomDet with(NOLOCK)
			where Bom in (select Item from #tempBomDetail where StruType = 'X') 
			and StartDate <= @EffDate and (EndDate >= @EffDate or EndDate is null)

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

		select BomDetId, Bom, Item, StruType, Uom, Op, Ref, RateQty, ScrapPct, Location, BackFlushMethod from #tempBomDetail

		drop table #tempBomDetail
	end try
	begin catch
		set @Msg = N'Bom�ֽ�����쳣��' + Error_Message()
		RAISERROR(@Msg, 16, 1) 
	end catch 
END 


