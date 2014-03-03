using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IUomConversionBaseDao
    {
        #region Method Created By CodeSmith

        void CreateUomConversion(UomConversion entity);

        UomConversion LoadUomConversion(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Uom alterUom, com.Sconit.Entity.MasterData.Uom baseUom);
        
        UomConversion LoadUomConversion(String itemCode, String alterUomCode, String baseUomCode);
  
        IList<UomConversion> GetAllUomConversion();
  
        void UpdateUomConversion(UomConversion entity);
        
        void DeleteUomConversion(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Uom alterUom, com.Sconit.Entity.MasterData.Uom baseUom);
    
        void DeleteUomConversion(String itemCode, String alterUomCode, String baseUomCode);
    
        void DeleteUomConversion(UomConversion entity);
    
        void DeleteUomConversion(IList<UomConversion> entityList);
       
        UomConversion LoadUomConversion(Int32 id);

        void DeleteUomConversion(Int32 id);

        void DeleteUomConversion(IList<Int32> pkList);
   
        #endregion Method Created By CodeSmith
    }
}
