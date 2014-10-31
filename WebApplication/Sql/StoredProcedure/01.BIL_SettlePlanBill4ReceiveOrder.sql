SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='BIL_SettlePlanBill4ReceiveOrder') 
     DROP PROCEDURE BIL_SettlePlanBill4ReceiveOrder 
GO

CREATE PROCEDURE [dbo].[BIL_SettlePlanBill4ReceiveOrder] 
(
	@RecNo varchar(50),
	@CreateUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN
	set nocount on
	declare @DateTimeNow datetime
	declare @ErrorMsg nvarchar(MAX)
	declare @trancount int
	declare @OrderType varchar(50)

	set @DateTimeNow = GetDate()
	set @trancount = @@trancount

	create table #tempSettlePlanBill
	(
		RowId int Identity(1, 1) primary key,
		PlanBillId int,
		SettleQty decimal(18, 8)
	)

	begin try
		begin try
			select @OrderType = OrderType from ReceiptMstr where RecNo = @RecNo
		
			if (@OrderType = 'Procurement' or @OrderType = 'Subconctracting')
			begin  --�ɹ�����
				insert into #tempSettlePlanBill(PlanBillId, SettleQty)
				select pb.Id, rd.RecQty
				from ReceiptDet as rd with(NOLOCK)
				inner join OrderLocTrans as olt with(NOLOCK) on rd.OrderLocTransId = olt.Id
				inner join OrderDet as od with(NOLOCK) on olt.OrderDetId = od.Id
				inner join OrderMstr as om with(NOLOCK) on od.OrderNo = om.OrderNo
				inner join PlanBill as pb with(NOLOCK) on rd.PlanBillId = pb.Id and rd.IsCS = 1
				left join Location as l with(NOLOCK) on olt.Loc = l.Code
				where rd.RecNo = @RecNo 
				and ((pb.SettleTerm = 'BAR' or pb.SettleTerm = '' or pb.SettleTerm is null)   --�ջ�����
					or (l.Code is null)        --�ջ���λΪ��
					or (l.IsSetCS = 1 and pb.SettleTerm = 'BAC')   --���߽���
					or ((od.NeedInspect = 0 or om.NeedInspect = 0) and pb.SettleTerm = 'BAI')   --�������û�����ü���
					or (om.SubType = 'Rtn'))  --�˿���������
			end
			else if (@OrderType = 'Transfer')			
			begin
				insert into #tempSettlePlanBill(PlanBillId, SettleQty)
				select pb.Id, rd.RecQty
				from ReceiptDet as rd with(NOLOCK)
				inner join OrderLocTrans as olt with(NOLOCK) on rd.OrderLocTransId = olt.Id
				inner join PlanBill as pb with(NOLOCK) on rd.PlanBillId = pb.Id and rd.IsCS = 1
				left join Location as l with(NOLOCK) on olt.Loc = l.Code
				where rd.RecNo = @RecNo and (l.Code is null or (l.IsSetCS = 1 and pb.SettleTerm = 'BAC'))  --�ջ�����
			end
			else if (@OrderType = 'Distribution')
			begin
				insert into #tempSettlePlanBill(PlanBillId, SettleQty)
				select pb.Id, rd.RecQty
				from ReceiptDet as rd with(NOLOCK)
				inner join OrderLocTrans as olt with(NOLOCK) on rd.OrderLocTransId = olt.Id
				inner join PlanBill as pb with(NOLOCK) on rd.PlanBillId = pb.Id and rd.IsCS = 1
				where rd.RecNo = @RecNo 
				and (pb.SettleTerm ='BAR' or pb.SettleTerm = '' or pb.SettleTerm is null)  --�ջ�����
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
		select * from #tempSettlePlanBill
			exec BIL_SettlePlanBill @CreateUser

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
		set @ErrorMsg = N'�ջ��ж��Ƿ��������쳣��' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	drop table #tempSettlePlanBill
END
GO