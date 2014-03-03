using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IReceiptDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateReceiptDetail(ReceiptDetail entity);

        ReceiptDetail LoadReceiptDetail(Int32 id);
  
        IList<ReceiptDetail> GetAllReceiptDetail();
  
        void UpdateReceiptDetail(ReceiptDetail entity);
        
        void DeleteReceiptDetail(Int32 id);
    
        void DeleteReceiptDetail(ReceiptDetail entity);
    
        void DeleteReceiptDetail(IList<Int32> pkList);
    
        void DeleteReceiptDetail(IList<ReceiptDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
