using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IShiftDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateShiftDetail(ShiftDetail entity);

        ShiftDetail LoadShiftDetail(Int32 id);
  
        IList<ShiftDetail> GetAllShiftDetail();
  
        void UpdateShiftDetail(ShiftDetail entity);
        
        void DeleteShiftDetail(Int32 id);
    
        void DeleteShiftDetail(ShiftDetail entity);
    
        void DeleteShiftDetail(IList<Int32> pkList);
    
        void DeleteShiftDetail(IList<ShiftDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
