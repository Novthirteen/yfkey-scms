using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IClientBaseDao
    {
        #region Method Created By CodeSmith

        void CreateClient(Client entity);

        Client LoadClient(String clientId);
  
        IList<Client> GetAllClient();
  
        IList<Client> GetAllClient(bool includeInactive);
  
        void UpdateClient(Client entity);
        
        void DeleteClient(String clientId);
    
        void DeleteClient(Client entity);
    
        void DeleteClient(IList<String> pkList);
    
        void DeleteClient(IList<Client> entityList);    
        #endregion Method Created By CodeSmith
    }
}
