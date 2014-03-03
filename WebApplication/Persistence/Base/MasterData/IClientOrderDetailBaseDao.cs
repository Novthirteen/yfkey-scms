using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IClientOrderDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateClientOrderDetail(ClientOrderDetail entity);

        ClientOrderDetail LoadClientOrderDetail(Int32 id);
  
        IList<ClientOrderDetail> GetAllClientOrderDetail();
  
        void UpdateClientOrderDetail(ClientOrderDetail entity);
        
        void DeleteClientOrderDetail(Int32 id);
    
        void DeleteClientOrderDetail(ClientOrderDetail entity);
    
        void DeleteClientOrderDetail(IList<Int32> pkList);
    
        void DeleteClientOrderDetail(IList<ClientOrderDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
