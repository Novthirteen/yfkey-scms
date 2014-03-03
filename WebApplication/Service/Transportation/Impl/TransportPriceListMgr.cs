using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Transportation;
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportPriceListMgr : TransportPriceListBaseMgr, ITransportPriceListMgr
    {
        private ITransportPriceListDetailMgr transportPriceListDetailMgr;
        private ICriteriaMgr criteriaMgr;

        public TransportPriceListMgr(ITransportPriceListDao entityDao,
            ITransportPriceListDetailMgr transportPriceListDetailMgr, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.transportPriceListDetailMgr = transportPriceListDetailMgr;
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public override void DeleteTransportPriceList(String code)
        {
            IList<TransportPriceListDetail> transportPriceListDetails = this.transportPriceListDetailMgr.GetAllTransportPriceListDetail(code);

            if (transportPriceListDetails != null)
                this.transportPriceListDetailMgr.DeleteTransportPriceListDetail(transportPriceListDetails);

            base.DeleteTransportPriceList(code);
        }

        [Transaction(TransactionMode.Requires)]
        public override void DeleteTransportPriceList(TransportPriceList entity)
        {
            DeleteTransportPriceList(entity.Code);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<TransportPriceList> GetTransportPriceList(string partyCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(TransportPriceList));
            criteria.Add(Expression.Eq("Party.Code", partyCode));
            criteria.Add(Expression.Eq("IsActive", true));
            return criteriaMgr.FindAll<TransportPriceList>(criteria);
        }

        #endregion Customized Methods
    }
}