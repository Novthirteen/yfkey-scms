using com.Sconit.Service.Ext.MasterData;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IUomConversionMgr : IUomConversionBaseMgr
    {
        #region Customized Methods

        decimal ConvertUomQty(string itemCode, Uom sourceUom, decimal sourceQty, Uom targetUom);

        decimal ConvertUomQty(string itemCode, string sourceUomCode, decimal sourceQty, string targetUomCode);

        decimal ConvertUomQty(Item item, Uom sourceUom, decimal sourceQty, Uom targetUom);

        IList GetUomConversion(string itemCode, string altUom, string baseUom);

        IList<UomConversion> GetUomConversion(string itemCode);

        decimal ConvertUomQtyInvToOrder(FlowDetail flowDetail, decimal invQty);

        #endregion Customized Methods
    }
}





#region Extend Interface






namespace com.Sconit.Service.Ext.MasterData
{
    public partial interface IUomConversionMgrE : com.Sconit.Service.MasterData.IUomConversionMgr
    {
        
    }
}

#endregion
