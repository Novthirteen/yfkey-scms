using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface INumberControlBaseDao
    {
        #region Method Created By CodeSmith

        void CreateNumberControl(NumberControl entity);

        NumberControl LoadNumberControl(String code);
  
        IList<NumberControl> GetAllNumberControl();
  
        void UpdateNumberControl(NumberControl entity);
        
        void DeleteNumberControl(String code);
    
        void DeleteNumberControl(NumberControl entity);
    
        void DeleteNumberControl(IList<String> pkList);
    
        void DeleteNumberControl(IList<NumberControl> entityList);    
        #endregion Method Created By CodeSmith
    }
}
