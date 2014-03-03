using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IClientOrderHeadBaseDao
    {
        #region Method Created By CodeSmith

        void CreateClientOrderHead(ClientOrderHead entity);

        ClientOrderHead LoadClientOrderHead(String id);
  
        IList<ClientOrderHead> GetAllClientOrderHead();
  
        void UpdateClientOrderHead(ClientOrderHead entity);
        
        void DeleteClientOrderHead(String id);
    
        void DeleteClientOrderHead(ClientOrderHead entity);
    
        void DeleteClientOrderHead(IList<String> pkList);
    
        void DeleteClientOrderHead(IList<ClientOrderHead> entityList);    
        #endregion Method Created By CodeSmith
    }
}
