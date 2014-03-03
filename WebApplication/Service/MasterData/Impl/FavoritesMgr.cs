using System.Collections;
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
    public class FavoritesMgr : FavoritesBaseMgr, IFavoritesMgr
    {
        private ICriteriaMgr criteriaMgr;
        public FavoritesMgr(IFavoritesDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<Favorites> GetFavorites(string userCode, string type)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Favorites>();
            criteria.Add(Expression.Eq("User.Code", userCode));
            criteria.Add(Expression.Eq("Type", type));
            criteria.AddOrder(Order.Desc("Id"));
            return criteriaMgr.FindAll<Favorites>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public bool CheckFavoritesUniqueExist(string userCode, string type, string pageName)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Favorites>();
            criteria.Add(Expression.Eq("User.Code", userCode));
            criteria.Add(Expression.Eq("Type", type));
            criteria.Add(Expression.Eq("PageName", pageName));
            IList<Favorites> temp = criteriaMgr.FindAll<Favorites>(criteria);

            if (temp.Count > 0)
            {
                return true;                
            }
            else
            {
                return false;
            }
        }

        #endregion Customized Methods
    }
}