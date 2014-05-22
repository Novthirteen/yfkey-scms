Alter PROCEDURE [dbo].[GetPartyFrom]
	@Item varchar(50),
	@OrderNo varchar(50),
	@PartyFrom varchar(50) OUTPUT
AS
begin
	if exists( select 1 from scms_archive2.dbo.OrderLocTrans where  IOType='Out' and orderdetid=(select Id from scms_archive2.dbo.orderdet where item=@Item and orderno=@OrderNo))
	begin
		select @PartyFrom=PartyFrom from scms_archive2.dbo.OrderMstr where orderno=@OrderNo
	end
	if(isnull(@PartyFrom,'')<>'') 
	begin
		if exists( select 1 from scms_archive.dbo.OrderLocTrans where  IOType='Out' and orderdetid=(select Id from scms_archive.dbo.orderdet where item=@Item and orderno=@OrderNo))
		begin
			select @PartyFrom=PartyFrom from scms_archive.dbo.OrderMstr where orderno=@OrderNo
		end
	end

end