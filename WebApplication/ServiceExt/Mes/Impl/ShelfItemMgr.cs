using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Mes;

using com.Sconit.Entity.Mes;

using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;


//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class ShelfItemMgr : ShelfItemBaseMgr, IShelfItemMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.MesDssIn");
        public ICriteriaMgr criteriaMgr { get; set; }
        public IShelfMgr shelfMgr { get; set; }
        public IItemMgr itemMgr { get; set; }

        public ShelfItemMgr(IShelfItemDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public ShelfItem LoadShelfItem(string shelfCode, string itemCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ShelfItem>();
            criteria.Add(Expression.Eq("Shelf.Code", shelfCode));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            IList<ShelfItem> shelfItemList = criteriaMgr.FindAll<ShelfItem>(criteria);
            return shelfItemList.Count > 0 ? shelfItemList[0] : null;
        }

        #endregion Customized Methods
    }
}