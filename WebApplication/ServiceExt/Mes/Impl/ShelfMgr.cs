using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Mes;

using com.Sconit.Service.MasterData;


using com.Sconit.Entity.Mes;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class ShelfMgr : ShelfBaseMgr, IShelfMgr
    {

        public IFlowMgr flowMgr { get; set; }
        
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.MesDssIn");


        public ShelfMgr(IShelfDao entityDao)
            : base(entityDao)
        {

        }

        #region Customized Methods

        #endregion Customized Methods
    }
}