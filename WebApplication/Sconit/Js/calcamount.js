function CalCulateRowAmount(obj, objName, adjustOption, unitPriceField, qtyField, discountField, discountRateField, amountField, totalDiscountField, totalDiscountRateField, totalAmountField,totalAmountAfterDiscountField,totalQtyField,isBlank) 
    {
		    var objId = $(obj).attr("id");
		    var parentId = objId.substring(0, objId.length - objName.length);
	        var qtyId = "#" + parentId + qtyField;
		    var amountId = "#" + parentId + amountField;
		    var unitPriceId = "#" + parentId + unitPriceField;
		    var discountId = "#" + parentId + discountField;
		    var discountRateId = "#" + parentId + discountRateField;
		    
		    if (adjustOption == "BaseOnDiscountRate") 
		    {
		    	//根据折扣率调整
	            if (!isNaN($(qtyId).val()) && !isNaN($(discountRateId).val())) 
	            {
			        var orgAmount = ($(unitPriceId).val() * $(qtyId).val()).toFixed(4);
		    	    var discount = (orgAmount * $(discountRateId).val() / 100).toFixed(4);
                    var amount = (orgAmount - discount).toFixed(4);
                    $(discountId).attr('value', discount);
			        $(amountId).attr('value', amount);
                }
		    }else if (adjustOption == "BaseOnDiscount") 
		    {
	            //根据折扣金额调整
			    if (!isNaN($(qtyId).val()) && !isNaN($(discountId).val())) 
			    {
				    var orgAmount = ($(unitPriceId).val() * $(qtyId).val()).toFixed(4);
				    var amount = (orgAmount - $(discountId).val()).toFixed(4);
			    	var discountRate = ((1 - (amount / orgAmount)) * 100).toFixed(4);
			    	$(discountRateId).attr('value', discountRate);
			    	$(amountId).attr('value', amount);
			    }
		    }
		    //行金额的新值赋给新值
		    var newAmount = $(amountId).attr('value');
	    	var oldAmount = $(amountId).attr('oldValue');
	    	$(amountId).attr('oldValue', newAmount);

	    	var newQty = $(qtyId).attr('value');
	    	var oldQty = $(qtyId).attr('oldValue');
	    	$(qtyId).attr('oldValue', newQty);
	    	if (totalDiscountField != null && totalDiscountRateField != null && totalAmountField != null) 
	    	{
			    //调整总金额
			    if (isNaN(newAmount)) {
		    	    newAmount = 0;
		    	}
		    	if (isNaN(oldAmount)) {
		    	    oldAmount = 0;
		    	}
		    	//调整总数量
		    	if (isNaN(newQty)) {
		    	    newQty = 0;
		    	}
		    	if (isNaN(oldQty)) {
		    	    oldQty = 0;
		    	}
		    	if(!isBlank)
		    	{
		    	    CalCulateTotalAmount("BaseOnRowAmount", totalDiscountField, totalDiscountRateField, totalAmountField, totalAmountAfterDiscountField, totalQtyField,(newAmount - oldAmount), (newQty - oldQty))
		        }
		    }
	   
     }

     function CalCulateTotalAmount(adjustOption, totalDiscountField, totalDiscountRateField, totalAmountField, totalAmountAfterDiscountField, totalQtyField, rowAmountDiff, rowQtyDiff) 
    {
	    if (adjustOption == "BaseOnRowAmount")
	    {
		    //根据行金额调整
		    var discountRate = $(totalDiscountRateField).val();
		    
		    if (isNaN(discountRate) || discountRate==null || discountRate=="")
		    {
			    discountRate = 0;
			}
			var totalAmount = $(totalAmountField).val();
			if (isNaN(totalAmount) || totalAmount==null || totalAmount=="") 
			{
			    totalAmount = 0;
			}
			
	        totalAmount = parseFloat(totalAmount) + parseFloat(rowAmountDiff);
		    var discount = (totalAmount * discountRate/100).toFixed(4);
		    $(totalAmountField).attr('value', totalAmount.toFixed(4));
			$(totalAmountAfterDiscountField).attr('value', (totalAmount-discount).toFixed(4));
			$(totalDiscountField).attr('value', discount);
			if (totalQtyField != null) {
			    var totalQty = $(totalQtyField).val();
			    totalQty = parseFloat(totalQty) + parseFloat(rowQtyDiff);
			    $(totalQtyField).attr('value', totalQty);
			}
		   
		}else if (adjustOption == "BaseOnDiscountRate") 
		{
			//根据折扣率调整
			if (!isNaN($(totalDiscountRateField).val()) && !isNaN($(totalAmountField).val())) 
			{
				var totalAmount = $(totalAmountField).val();
				var discount = (totalAmount * $(totalDiscountRateField).val()/100).toFixed(4);
				totalAmount -= discount;
				$(totalDiscountField).attr('value', discount);
				$(totalAmountAfterDiscountField).attr('value', totalAmount.toFixed(4));
			}
		} else if (adjustOption == "BaseOnDiscount")
		{
			//根据折扣金额调整
			if (!isNaN($(totalDiscountField).val())) 
			{
				var discount = $(totalDiscountField).val();
			    var totalAmount = $(totalAmountField).val();
			    if (isNaN(totalAmount))
			    {	
			        totalAmount = 0;
			    }
			    var discountRate = (100 * discount / totalAmount).toFixed(4);
			    totalAmount = totalAmount - discount;
			    $(totalAmountAfterDiscountField).attr('value', totalAmount.toFixed(4));
		    	$(totalDiscountRateField).attr('value', discountRate);
			}
		 }
	}