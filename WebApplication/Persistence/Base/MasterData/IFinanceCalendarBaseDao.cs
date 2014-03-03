using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IFinanceCalendarBaseDao
    {
        #region Method Created By CodeSmith

        void CreateFinanceCalendar(FinanceCalendar entity);

        FinanceCalendar LoadFinanceCalendar(Int32 id);
  
        IList<FinanceCalendar> GetAllFinanceCalendar();
  
        void UpdateFinanceCalendar(FinanceCalendar entity);
        
        void DeleteFinanceCalendar(Int32 id);
    
        void DeleteFinanceCalendar(FinanceCalendar entity);
    
        void DeleteFinanceCalendar(IList<Int32> pkList);
    
        void DeleteFinanceCalendar(IList<FinanceCalendar> entityList);    
        
        FinanceCalendar LoadFinanceCalendar(Int32 financeYear, Int32 financeMonth);
    
        void DeleteFinanceCalendar(Int32 financeYear, Int32 financeMonth);
        #endregion Method Created By CodeSmith
    }
}
