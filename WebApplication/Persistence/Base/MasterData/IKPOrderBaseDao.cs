using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IKPOrderBaseDao
    {
        #region Method Created By CodeSmith

        void CreateKPOrder(KPOrder entity);

        KPOrder LoadKPOrder(Decimal oRDER_ID);
  
        IList<KPOrder> GetAllKPOrder();
  
        void UpdateKPOrder(KPOrder entity);
        
        void DeleteKPOrder(Decimal oRDER_ID);
    
        void DeleteKPOrder(KPOrder entity);
    
        void DeleteKPOrder(IList<Decimal> pkList);
    
        void DeleteKPOrder(IList<KPOrder> entityList);    
        #endregion Method Created By CodeSmith
    }
}
