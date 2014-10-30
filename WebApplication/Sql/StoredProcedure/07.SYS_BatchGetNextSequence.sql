SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='SYS_BatchGetNextSequence') 
     DROP PROCEDURE SYS_BatchGetNextSequence 
GO

CREATE PROCEDURE [dbo].[SYS_BatchGetNextSequence] 
(
	@CodePrefix varchar(50),
	@BatchSize int, 
	@NextSequence int OUTPUT
) --WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON 
	declare @trancount int 
	set @trancount = @@trancount
	
	begin try
		if @trancount = 0
		begin
			begin tran
		end
        
		IF EXISTS (SELECT * FROM NumCtrl WITH (UPDLOCK,SERIALIZABLE) WHERE Code = @CodePrefix) 
		BEGIN 
			SELECT @NextSequence=IntValue + @BatchSize FROM NumCtrl WHERE Code = @CodePrefix 
			UPDATE NumCtrl SET IntValue = IntValue + @BatchSize WHERE Code = @CodePrefix 
		END 
		ELSE 
		BEGIN 
			INSERT INTO NumCtrl(Code,IntValue) 
			VALUES(@CodePrefix,@BatchSize) 
			SET @NextSequence = @BatchSize 
		END 
	
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
        
		declare @ErrorMsg nvarchar(MAX)
		set @ErrorMsg = N'批量获取序号出现异常：' + Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
    end catch
END
