using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IReceiptBaseDao
    {
        #region Method Created By CodeSmith

        void CreateReceipt(Receipt entity);

        Receipt LoadReceipt(String receiptNo);
  
        IList<Receipt> GetAllReceipt();
  
        void UpdateReceipt(Receipt entity);
        
        void DeleteReceipt(String receiptNo);
    
        void DeleteReceipt(Receipt entity);
    
        void DeleteReceipt(IList<String> pkList);
    
        void DeleteReceipt(IList<Receipt> entityList);    
        #endregion Method Created By CodeSmith
    }
}
