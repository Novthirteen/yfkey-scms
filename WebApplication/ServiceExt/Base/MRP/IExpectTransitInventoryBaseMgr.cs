using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface IExpectTransitInventoryBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateExpectTransitInventory(ExpectTransitInventory entity);

        ExpectTransitInventory LoadExpectTransitInventory(Int32 id);

        IList<ExpectTransitInventory> GetAllExpectTransitInventory();
    
        void UpdateExpectTransitInventory(ExpectTransitInventory entity);

        void DeleteExpectTransitInventory(Int32 id);
    
        void DeleteExpectTransitInventory(ExpectTransitInventory entity);
    
        void DeleteExpectTransitInventory(IList<Int32> pkList);
    
        void DeleteExpectTransitInventory(IList<ExpectTransitInventory> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
