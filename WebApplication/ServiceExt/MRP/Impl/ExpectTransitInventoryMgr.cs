using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class ExpectTransitInventoryMgr : ExpectTransitInventoryBaseMgr, IExpectTransitInventoryMgr
    {
        public ExpectTransitInventoryMgr(IExpectTransitInventoryDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}