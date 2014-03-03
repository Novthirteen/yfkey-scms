using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IActingBillBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateActingBill(ActingBill entity);

        ActingBill LoadActingBill(Int32 id);

        IList<ActingBill> GetAllActingBill();
    
        void UpdateActingBill(ActingBill entity);

        void DeleteActingBill(Int32 id);
    
        void DeleteActingBill(ActingBill entity);
    
        void DeleteActingBill(IList<Int32> pkList);
    
        void DeleteActingBill(IList<ActingBill> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
