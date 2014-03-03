using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IShiftBaseDao
    {
        #region Method Created By CodeSmith

        void CreateShift(Shift entity);

        Shift LoadShift(String code);
  
        IList<Shift> GetAllShift();
  
        void UpdateShift(Shift entity);
        
        void DeleteShift(String code);
    
        void DeleteShift(Shift entity);
    
        void DeleteShift(IList<String> pkList);
    
        void DeleteShift(IList<Shift> entityList);    
        #endregion Method Created By CodeSmith
    }
}
