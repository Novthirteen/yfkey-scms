SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='DSS_ReceiveWorkOrder') 
     DROP PROCEDURE DSS_ReceiveWorkOrder 
GO

CREATE PROCEDURE [dbo].[DSS_ReceiveWorkOrder] 
(
	@CreateUser varchar(50)
) --WITH ENCRYPTION
AS 
BEGIN
	set nocount on
	declare @DateTimeNow datetime
	declare @ErrorMsg nvarchar(MAX)
	declare @Trancount int

	set @DateTimeNow = GetDate()

	begin try
		
	end try
	begin catch
		set @ErrorMsg = Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch

	set @Trancount = @@Trancount

	begin try
		if @Trancount = 0
		begin
            begin tran
        end

		
		if @Trancount = 0 
		begin  
            commit
        end
	end try
	begin catch
        if @Trancount = 0
        begin
            rollback
        end 
		
		set @ErrorMsg = Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
	end catch
END
GO