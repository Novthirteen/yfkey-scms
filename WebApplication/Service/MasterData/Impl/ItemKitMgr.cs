using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ItemKitMgr : ItemKitBaseMgr, IItemKitMgr
    {
        private ICriteriaMgr criteriaMgr;
        public ItemKitMgr(IItemKitDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemKit> GetChildItemKit(string parentItemCode)
        {
            return GetChildItemKit(parentItemCode, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemKit> GetChildItemKit(string parentItemCode, bool includeInActive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ItemKit>();
            criteria.Add(Expression.Eq("ParentItem.Code", parentItemCode));

            if (!includeInActive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            return this.criteriaMgr.FindAll<ItemKit>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemKit> GetChildItemKit(Item parentItem)
        {
            return GetChildItemKit(parentItem.Code, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemKit> GetChildItemKit(Item parentItem, bool includeInActive)
        {
            return GetChildItemKit(parentItem.Code, includeInActive);
        }

        #endregion Customized Methods
    }
}