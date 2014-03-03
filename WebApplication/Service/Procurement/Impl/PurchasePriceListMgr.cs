using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.Procurement;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class PurchasePriceListMgr : PurchasePriceListBaseMgr, IPurchasePriceListMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IBillAddressMgr billAddressMgr;
        public PurchasePriceListMgr(IPurchasePriceListDao entityDao, ICriteriaMgr criteriaMgr, IBillAddressMgr billAddressMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.billAddressMgr = billAddressMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public PurchasePriceList LoadPurchasePriceList(string code, bool includeDetail)
        {
            PurchasePriceList purchasePriceList = this.LoadPurchasePriceList(code);
            if (purchasePriceList != null && includeDetail && purchasePriceList.PriceListDetails != null && purchasePriceList.PriceListDetails.Count > 0)
            {

            }
            return purchasePriceList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<PurchasePriceList> GetAllPurchasePriceList(string addressCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PurchasePriceList>();
            criteria.Add(Expression.Eq("IsActive", true));
            if (addressCode != null && addressCode != string.Empty)
            {
                BillAddress billAddress = billAddressMgr.LoadBillAddress(addressCode);
                criteria.Add(Expression.Or(Expression.Eq("Party.Code", billAddress.Party.Code), Expression.IsNull("Party")));
            }
            else
            {
                criteria.Add(Expression.IsNull("Party"));
            }
            return criteriaMgr.FindAll<PurchasePriceList>(criteria);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<PurchasePriceList> GetAllPurchasePriceList(BillAddress address)
        {
            return GetAllPurchasePriceList(address.Code);
        }


        #endregion Customized Methods
    }
}