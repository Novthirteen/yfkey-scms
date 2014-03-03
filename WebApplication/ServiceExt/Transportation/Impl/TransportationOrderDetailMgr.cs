using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationOrderDetailMgr : TransportationOrderDetailBaseMgr, ITransportationOrderDetailMgr
    {
        public TransportationOrderDetailMgr(ITransportationOrderDetailDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}