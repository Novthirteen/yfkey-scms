using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface INamedQueryBaseDao
    {
        #region Method Created By CodeSmith

        void CreateNamedQuery(NamedQuery entity);

        NamedQuery LoadNamedQuery(com.Sconit.Entity.MasterData.User user, String queryName);
        NamedQuery LoadNamedQuery(String userCode, String queryName);
  
        IList<NamedQuery> GetAllNamedQuery();
  
        void UpdateNamedQuery(NamedQuery entity);
        
        void DeleteNamedQuery(com.Sconit.Entity.MasterData.User user, String queryName);
    
        void DeleteNamedQuery(String userCode, String queryName);
    
        void DeleteNamedQuery(NamedQuery entity);
    
        void DeleteNamedQuery(IList<NamedQuery> entityList);    
        #endregion Method Created By CodeSmith
    }
}
