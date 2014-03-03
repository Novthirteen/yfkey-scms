using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Mes;
using com.Sconit.Entity.Mes;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class ProductLineUserMgr : ProductLineUserBaseMgr, IProductLineUserMgr
    {
        private ICriteriaMgr criteriaMgr;
        public ProductLineUserMgr(IProductLineUserDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public ProductLineUser LoadProductLineUser(string userCode, string flowCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ProductLineUser>();
            criteria.Add(Expression.Eq("ProductLine.Code", flowCode));
            criteria.Add(Expression.Eq("DeliveryUser.Code", userCode));
            IList<ProductLineUser> productLineUserList = criteriaMgr.FindAll<ProductLineUser>(criteria);
            return productLineUserList.Count > 0 ? productLineUserList[0] : null;
        }

        #endregion Customized Methods
    }
}