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
	@ReceiveIpInputTable ReceiveIpInputType readonly,
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
		OrderNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		OrderType varchar(50) COLLATE  Chinese_PRC_CI_AS,
		OrderSubType varchar(50) COLLATE  Chinese_PRC_CI_AS,
		OrderStatus varchar(50) COLLATE  Chinese_PRC_CI_AS,
		StartTime datetime,
		Currency varchar(50) COLLATE  Chinese_PRC_CI_AS,
		IsAutoRelease bit,
		IsAutoStart bit,
		FulfillUC bit,
		OrderVersion int,
		OrderDetId int,
		Item varchar(50) COLLATE  Chinese_PRC_CI_AS,
		ItemDesc varchar(100) COLLATE  Chinese_PRC_CI_AS,
		Uom varchar(5) COLLATE  Chinese_PRC_CI_AS,
		UC decimal(18, 8),
		OrderQty decimal(18, 8),
		RecQty decimal(18, 8),
		PriceList varchar(50) COLLATE  Chinese_PRC_CI_AS,
		PriceListDetId int,
		OrderLocTransId decimal(18, 8),
		UnitQty decimal(18, 8),
		IpNo varchar(50) COLLATE  Chinese_PRC_CI_AS,
		IpStatus varchar(50) COLLATE  Chinese_PRC_CI_AS,
		IpDetId int,
		IpQty decimal(18, 8)
	)

	begin try
		begin try
			if not exists(select top 1 1 from @ReceiveIpInputTable where RecQty > 0)
			begin
				RAISERROR(N'�ջ���ϸ����Ϊ�ա�', 16, 1) 
			end

			insert into #tempOrder_13(OrderNo, OrderType, OrderSubType, OrderStatus, StartTime, Currency, IsAutoRelease, IsAutoStart, FulfillUC, OrderVersion, 
			OrderDetId, Item, ItemDesc, Uom, UC, OrderQty, RecQty, PriceList, PriceListDetId, OrderLocTransId, UnitQty, IpNo, IpStatus, IpDetId, IpQty)
			select om.OrderNo, om.[Type], om.SubType, om.[Status], om.StartTime, om.Currency, om.IsAutoRelease, om.IsAutoStart, om.FulfillUC, om.[Version], 
			od.Id, od.Item, od.ItemDesc, od.Uom, od.UC, od.OrderQty, od.RecQty, ISNULL(od.PriceListFrom, om.PriceListFrom), od.PriceListDetFrom, olt.Id, olt.UnitQty, im.IpNo, im.[Status], id.Id, id.Qty
			from (select distinct IpDetId from @ReceiveIpInputTable) as tmp
			inner join IpDet as id on tmp.IpDetId = id.Id
			inner join IpMstr as im on id.IpNo = im.IpNo
			inner join OrderLocTrans as olt on id.OrderLocTransId = olt.Id
			inner join OrderDet as od on olt.OrderDetId = od.Id
			inner join OrderMstr as om on om.OrderNo = om.OrderNo
			
			--���ж�OrderHead״̬��ֻҪ��ASN�Ͷ������ջ�
			--���ж��Ƿ�����ջ��жϣ�ֻҪ��ASN�Ͷ������ջ�
			
			--�ж�IpMstr״̬
			if exists(select top 1 1 from #tempOrder_13 where IpStatus = 'Close')
			begin
				select top 1 @ErrorMsg = N'ASN[' + IpNo + N']�Ѿ��رա�' from #tempOrder_13 where IpStatus = 'Close'
				RAISERROR(@ErrorMsg, 16, 1) 
			end

			--����װ�ջ��жϣ����ٵĲ�����
			if exists(select top 1 1 from #tempOrder_13 as ord inner join @ReceiveIpInputTable as rec on ord.IpDetId = rec.IpDetId 
						where ord.OrderSubType = 'Nml' and not(ord.IsAutoRelease = 1 and ord.IsAutoStart = 1) and rec.RecQty % ord.UC <> 0)
			begin
				select top 1 @ErrorMsg = N'ASN[' + ord.IpNo + N']�����[' + ord.Item + N']û�а�����װ�ջ���' from #tempOrder_13 as ord 
						inner join @ReceiveIpInputTable as rec on ord.IpDetId = rec.IpDetId 
						where ord.OrderSubType = 'Nml' and not(ord.IsAutoRelease = 1 and ord.IsAutoStart = 1) and rec.RecQty % ord.UC <> 0
				RAISERROR(@ErrorMsg, 16, 1) 
			end

			--�ɹ��ջ��Ƿ��м۸��жϣ�û�м۸����²���һ�μ۸�
			if exists(select top 1 1 from EntityOpt where PreCode = 'NoPriceListReceipt' and PreValue = 'False')
			begin
				update ord set PriceListDetId = pld.Id
				from #tempOrder_13 as ord 
				inner join PriceListDet as pld on ord.PriceList = pld.PriceList and ord.Item = pld.Item 
				and ord.Currency = pld.Currency and ord.Uom = pld.Uom and pld.StartDate <= ord.StartTime and (pld.EndDate > ord.StartTime or pld.EndDate is null)
				where ord.OrderType = 'Procuement'

				if exists(select top 1 1 from #tempOrder_13 where OrderType = 'Procuement' and PriceListDetId is null)
				begin
					
				end
			end
		end try
		begin catch
			set @ErrorMsg = N'����׼�������쳣��' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch

		begin try
			if @trancount = 0
			begin
				begin tran
			end

			--��زֿ������
			--

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
		
			set @ErrorMsg = N'���ݸ��³����쳣��' + Error_Message()
			RAISERROR(@ErrorMsg, 16, 1) 
		end catch
	end try
	begin catch
		set @ErrorMsg = N'ASN�ջ������쳣��' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempPlanBill_11
END
GO