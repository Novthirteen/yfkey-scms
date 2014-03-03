using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface IExpenseBaseDao
    {
        #region Method Created By CodeSmith

        void CreateExpense(Expense entity);

        Expense LoadExpense(String code);
  
        IList<Expense> GetAllExpense();
  
        void UpdateExpense(Expense entity);
        
        void DeleteExpense(String code);
    
        void DeleteExpense(Expense entity);
    
        void DeleteExpense(IList<String> pkList);
    
        void DeleteExpense(IList<Expense> entityList);    
        #endregion Method Created By CodeSmith
    }
}
