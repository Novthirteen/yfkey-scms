using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IUomConversionBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateUomConversion(UomConversion entity);

        UomConversion LoadUomConversion(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Uom alterUom, com.Sconit.Entity.MasterData.Uom baseUom);

        IList<UomConversion> GetAllUomConversion();

        UomConversion LoadUomConversion(Int32 id);

        UomConversion LoadUomConversion(String itemCode, String alterUomCode, String baseUomCode);

        void UpdateUomConversion(UomConversion entity);

        void DeleteUomConversion(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Uom alterUom, com.Sconit.Entity.MasterData.Uom baseUom);
    
        void DeleteUomConversion(String itemCode, String alterUomCode, String baseUomCode);
    
        void DeleteUomConversion(UomConversion entity);

        void DeleteUomConversion(Int32 id);

        void DeleteUomConversion(IList<Int32> pkList);

        void DeleteUomConversion(IList<UomConversion> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
