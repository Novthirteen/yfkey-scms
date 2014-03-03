using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface INamedQueryBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateNamedQuery(NamedQuery entity);

        NamedQuery LoadNamedQuery(com.Sconit.Entity.MasterData.User user, String queryName);

        IList<NamedQuery> GetAllNamedQuery();
    
    
        NamedQuery LoadNamedQuery(String userCode, String queryName);
        void UpdateNamedQuery(NamedQuery entity);

        void DeleteNamedQuery(com.Sconit.Entity.MasterData.User user, String queryName);
    
        void DeleteNamedQuery(String userCode, String queryName);
    
        void DeleteNamedQuery(NamedQuery entity);
    
        void DeleteNamedQuery(IList<NamedQuery> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
