using System;
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
    public class PriceListDetailMgr : PriceListDetailBaseMgr, IPriceListDetailMgr
    {
        private ICriteriaMgr criteriaMgr;
        public PriceListDetailMgr(IPriceListDetailDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public PriceListDetail GetLastestPriceListDetail(string priceListCode, string itemCode, DateTime effectiveDate, string currencyCode)
        {
            //NullableDateTime nullableEffDate = new NullableDateTime(effectiveDate);

            DetachedCriteria detachedCriteria = DetachedCriteria.For<PriceListDetail>();
            detachedCriteria.Add(Expression.Eq("PriceList.Code", priceListCode));
            detachedCriteria.Add(Expression.Eq("Item.Code", itemCode));
            detachedCriteria.Add(Expression.Eq("Currency.Code", currencyCode));
            detachedCriteria.Add(Expression.Le("StartDate", effectiveDate));
            detachedCriteria.Add(Expression.Or(Expression.Ge("EndDate", effectiveDate), Expression.IsNull("EndDate")));
            detachedCriteria.AddOrder(Order.Desc("StartDate")); //按StartDate降序，取最新的价格

            IList<PriceListDetail> priceListDetailList = criteriaMgr.FindAll<PriceListDetail>(detachedCriteria);
            if (priceListDetailList != null && priceListDetailList.Count > 0)
            {
                return priceListDetailList[0];
            }
            else
            {
                return null;
            }
        }

        public PriceListDetail GetLastestPriceListDetail(PriceList priceList, Item item, DateTime effectiveDate, Currency currency)
        {
            return GetLastestPriceListDetail(priceList.Code, item.Code, effectiveDate, currency.Code);
        }

        public PriceListDetail GetLastestPriceListDetail(string priceListCode, string itemCode, DateTime effectiveDate, string currencyCode, string uomCode)
        {
            //NullableDateTime nullableEffDate = new NullableDateTime(effectiveDate);

            DetachedCriteria detachedCriteria = DetachedCriteria.For<PriceListDetail>();
            detachedCriteria.Add(Expression.Eq("PriceList.Code", priceListCode));
            detachedCriteria.Add(Expression.Eq("Item.Code", itemCode));
            detachedCriteria.Add(Expression.Eq("Currency.Code", currencyCode));
            detachedCriteria.Add(Expression.Eq("Uom.Code", uomCode));
            detachedCriteria.Add(Expression.Le("StartDate", effectiveDate));
            detachedCriteria.Add(Expression.Or(Expression.Ge("EndDate", effectiveDate.Date), Expression.IsNull("EndDate")));
            detachedCriteria.AddOrder(Order.Desc("StartDate")); //按StartDate降序，取最新的价格

            IList<PriceListDetail> priceListDetailList = criteriaMgr.FindAll<PriceListDetail>(detachedCriteria);
            if (priceListDetailList != null && priceListDetailList.Count > 0)
            {
                return priceListDetailList[0];
            }
            else
            {
                return null;
            }
        }

        public PriceListDetail GetLastestPriceListDetail(PriceList priceList, Item item, DateTime effectiveDate, Currency currency, Uom uom)
        {
            return GetLastestPriceListDetail(priceList.Code, item.Code, effectiveDate, currency.Code, uom.Code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public PriceListDetail LoadPriceListDetail(string priceListCode, string currencyCode, string itemCode, string uomCode, DateTime startDate)
        {
            DetachedCriteria detachedCriteria = DetachedCriteria.For<PriceListDetail>();
            detachedCriteria.Add(Expression.Eq("PriceList.Code", priceListCode));
            detachedCriteria.Add(Expression.Eq("Currency.Code", currencyCode));
            detachedCriteria.Add(Expression.Eq("Item.Code", itemCode));
            detachedCriteria.Add(Expression.Eq("Uom.Code", uomCode));
            detachedCriteria.Add(Expression.Eq("StartDate", startDate));

            IList<PriceListDetail> priceListDetailList = criteriaMgr.FindAll<PriceListDetail>(detachedCriteria);
            if (priceListDetailList != null && priceListDetailList.Count > 0)
            {
                return priceListDetailList[0];
            }
            else
            {
                return null;
            }
        }

        #endregion Customized Methods
    }
}