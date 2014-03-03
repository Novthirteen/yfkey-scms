using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IPickListBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePickList(PickList entity);

        PickList LoadPickList(String pickListNo);
  
        IList<PickList> GetAllPickList();
  
        void UpdatePickList(PickList entity);
        
        void DeletePickList(String pickListNo);
    
        void DeletePickList(PickList entity);
    
        void DeletePickList(IList<String> pkList);
    
        void DeletePickList(IList<PickList> entityList);    
        #endregion Method Created By CodeSmith
    }
}
