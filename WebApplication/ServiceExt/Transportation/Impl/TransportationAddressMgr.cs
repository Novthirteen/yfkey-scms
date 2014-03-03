using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Transportation;
using com.Sconit.Service.Transportation;
using com.Sconit.Entity.Transportation;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Persistence;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity;
using System.Data.SqlClient;
using System.Data;
using NHibernate.Expression;
using com.Sconit.Utility;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationAddressMgr : TransportationAddressBaseMgr, ITransportationAddressMgr
    {
        public TransportationAddressMgr(ITransportationAddressDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        #endregion Customized Methods
    }
}