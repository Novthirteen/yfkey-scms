using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IClientWorkingHoursBaseDao
    {
        #region Method Created By CodeSmith

        void CreateClientWorkingHours(ClientWorkingHours entity);

        ClientWorkingHours LoadClientWorkingHours(Int32 id);
  
        IList<ClientWorkingHours> GetAllClientWorkingHours();
  
        void UpdateClientWorkingHours(ClientWorkingHours entity);
        
        void DeleteClientWorkingHours(Int32 id);
    
        void DeleteClientWorkingHours(ClientWorkingHours entity);
    
        void DeleteClientWorkingHours(IList<Int32> pkList);
    
        void DeleteClientWorkingHours(IList<ClientWorkingHours> entityList);    
        #endregion Method Created By CodeSmith
    }
}
