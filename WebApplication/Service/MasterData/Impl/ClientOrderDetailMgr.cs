using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ClientOrderDetailMgr : ClientOrderDetailBaseMgr, IClientOrderDetailMgr
    {
        private ICriteriaMgr criteriaMgr;
        public ClientOrderDetailMgr(IClientOrderDetailDao entityDao,ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public IList<ClientOrderDetail> GetAllClientOrderDetail(string OrderHeadId) 
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ClientOrderDetail));
            criteria.Add(Expression.Eq("ClientOrderHead.Id", OrderHeadId));
            IList<ClientOrderDetail> clientOrderDetailList = criteriaMgr.FindAll<ClientOrderDetail>(criteria);
            if (clientOrderDetailList.Count > 0)
            {
                return clientOrderDetailList;
            }
            return null;
        }

        #endregion Customized Methods
    }
}