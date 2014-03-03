using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IClientMonitorBaseDao
    {
        #region Method Created By CodeSmith

        void CreateClientMonitor(ClientMonitor entity);

        ClientMonitor LoadClientMonitor(Int32 id);
  
        IList<ClientMonitor> GetAllClientMonitor();
  
        void UpdateClientMonitor(ClientMonitor entity);
        
        void DeleteClientMonitor(Int32 id);
    
        void DeleteClientMonitor(ClientMonitor entity);
    
        void DeleteClientMonitor(IList<Int32> pkList);
    
        void DeleteClientMonitor(IList<ClientMonitor> entityList);    
        #endregion Method Created By CodeSmith
    }
}
