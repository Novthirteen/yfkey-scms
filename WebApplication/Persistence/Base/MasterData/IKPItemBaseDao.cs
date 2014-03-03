using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IKPItemBaseDao
    {
        #region Method Created By CodeSmith

        void CreateKPItem(KPItem entity);

        KPItem LoadKPItem(Decimal iTEM_SEQ_ID);
  
        IList<KPItem> GetAllKPItem();
  
        void UpdateKPItem(KPItem entity);
        
        void DeleteKPItem(Decimal iTEM_SEQ_ID);
    
        void DeleteKPItem(KPItem entity);
    
        void DeleteKPItem(IList<Decimal> pkList);
    
        void DeleteKPItem(IList<KPItem> entityList);    
        #endregion Method Created By CodeSmith
    }
}
