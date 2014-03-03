using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IReceiptInProcessLocationBaseDao
    {
        #region Method Created By CodeSmith

        void CreateReceiptInProcessLocation(ReceiptInProcessLocation entity);

        ReceiptInProcessLocation LoadReceiptInProcessLocation(Int32 id);
  
        IList<ReceiptInProcessLocation> GetAllReceiptInProcessLocation();
  
        void UpdateReceiptInProcessLocation(ReceiptInProcessLocation entity);
        
        void DeleteReceiptInProcessLocation(Int32 id);
    
        void DeleteReceiptInProcessLocation(ReceiptInProcessLocation entity);
    
        void DeleteReceiptInProcessLocation(IList<Int32> pkList);
    
        void DeleteReceiptInProcessLocation(IList<ReceiptInProcessLocation> entityList);    
        #endregion Method Created By CodeSmith
    }
}
