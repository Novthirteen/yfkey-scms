/****** Object:  StoredProcedure [dbo].[USP_Busi_ImportKPOrder]    Script Date: 05/14/2014 11:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter procedure [dbo].[USP_Busi_ImportKPOrder]  
	 @StartDate datetime 

as     
begin       
   declare @DateTimeNow datetime   
   declare @LastItemSeqId int       
 
    ----1设置传的当前时间
 	set @DateTimeNow = getdate()
 	
 	----2插入sv的order表
 	Insert into isv.sv.order_header(PARTY_FROM_ID,PARTY_TO_ID,ORDER_TYPE_ID,ORDER_TYPE_NAME,ORDER_PUB_DATE,ORDER_PRINT,QAD_ORDER_ID,SYS_CODE)
    select h.xrcd_vend,'','KP','开票通知单',@DateTimeNow,'N',h.xrcd_no,'YK'  from svp.dbo.xrcd_hist h
    where h.xrcd_char2 >= @StartDate 
    and not exists (select 1 from isv.sv.order_header o where o.PARTY_FROM_ID = h.xrcd_vend and o.QAD_ORDER_ID = h.xrcd_no) 
    group by xrcd_vend,xrcd_no
    
    ----3插入sv的kp_order表,用pubdate
    insert into isv.sv.kp_order (ORDER_ID)
    select order_id from  isv.sv.order_header where order_pub_date=@DateTimeNow
    
    
    ----4先取出sv的kp_item表的最大id,传数据用
    select @LastItemSeqId =  max(SEQ_ID) from isv.sv.kp_item
    
    ----5插入sv的kp_item表，只取价格price1大于0的，防止没价格的进来
    insert into isv.sv.kp_item (SEQ_ID,PART_CODE,Part_Name,UM,INCOMING_QTY,PURCHASE_ORDER_ID,INCOMING_ORDER_ID,DELIVER_ORDER_ID,ORDER_ID,PRICE,PRICE1,PRICE2,INCOMING_DATE,LOCATION_ID)
    select xrcd_line,xrcd_part,xrcd_desc1,xrcd_um,xrcd_rcvd,xrcd_nbr,xrcd_receiver,xrcd_ps_nbr,v.order_id,xrcd_pcprice,xrcd_pcprice1,xrcd_pcprice2,CONVERT(varchar(100), xrcd_rcp_date, 23),xrcd_loc
    from svp.dbo.xrcd_hist h 
    inner join isv.sv.order_header v on h.xrcd_vend = v.PARTY_FROM_ID and h.xrcd_no=v.QAD_ORDER_ID
    where v.ORDER_TYPE_ID='KP' and h.xrcd_char2 >= @StartDate and h.xrcd_pcprice1 > 0
    and v.order_id in (select order_id from  isv.sv.order_header where order_pub_date=@DateTimeNow)
    group by xrcd_line,xrcd_part,xrcd_desc1,xrcd_um,xrcd_rcvd,xrcd_nbr,xrcd_receiver,xrcd_ps_nbr,
    v.order_id,xrcd_pcprice,xrcd_pcprice1,xrcd_pcprice2,CONVERT(varchar(100), xrcd_rcp_date, 23),xrcd_loc
   
    --6插入scms的kp_order,用的pubdate
    insert into scms.dbo.kp_order
    select *,null,null,null,null,null,null,null,null,null,null from isv.sv.order_header
    where order_pub_date=@DateTimeNow

    --7插入scms的kp_item,用的id
    insert into scms.dbo.kp_item
    select part_code,purchase_order_id,incoming_order_id,seq_id,incoming_qty,
    price,um,price1,price2,part_name,deliver_order_id,incoming_date,location_id,order_id,
    item_seq_id+1400 from isv.sv.kp_item where item_seq_id > @LastItemSeqId
     
    --8更新kp_order的sysCode,最后几位有效数字是3位数的更新成北京'BJ'
    update scms.dbo.kp_order set sys_code='BJ' 
    where qad_order_id like 'RC%' and right(qad_order_id,8)<1000 and order_id in (select order_id from  isv.sv.order_header where order_pub_date=@DateTimeNow)
    

    
    --9错误数据查到表里，暂时没有什么情况有错误，这个先预留
end