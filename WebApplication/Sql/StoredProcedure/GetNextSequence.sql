SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS(SELECT * FROM sys.objects WHERE type='P' AND name='GetNextSequence') 
     DROP PROCEDURE GetNextSequence 
GO

CREATE PROCEDURE [dbo].[GetNextSequence] 
(
	@CodePrefix varchar(50),
	@NextSequence int OUTPUT
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
		
		IF EXISTS (SELECT * FROM NumCtrl WITH (UPDLOCK, SERIALIZABLE) WHERE Code = @CodePrefix) 
		BEGIN 
			SELECT @NextSequence = IntValue + 1 FROM NumCtrl WHERE Code = @CodePrefix 
			UPDATE NumCtrl SET IntValue=IntValue + 1 WHERE Code = @CodePrefix 
		END 
		ELSE 
		BEGIN 
			INSERT INTO NumCtrl(Code,IntValue) VALUES(@CodePrefix, 1) 
			SET @NextSequence = 1 
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
		set @ErrorMsg = Error_Message() 
		RAISERROR(@ErrorMsg, 16, 1) 
    end catch
END
