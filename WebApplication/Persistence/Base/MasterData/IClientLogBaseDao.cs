using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IClientLogBaseDao
    {
        #region Method Created By CodeSmith

        void CreateClientLog(ClientLog entity);

        ClientLog LoadClientLog(Int32 id);
  
        IList<ClientLog> GetAllClientLog();
  
        void UpdateClientLog(ClientLog entity);
        
        void DeleteClientLog(Int32 id);
    
        void DeleteClientLog(ClientLog entity);
    
        void DeleteClientLog(IList<Int32> pkList);
    
        void DeleteClientLog(IList<ClientLog> entityList);    
        #endregion Method Created By CodeSmith
    }
}
