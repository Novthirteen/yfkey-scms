using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.Transportation;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportPriceListDetailMgr : TransportPriceListDetailBaseMgr, ITransportPriceListDetailMgr
    {
        private ICriteriaMgr criteriaMgr;

        public TransportPriceListDetailMgr(ITransportPriceListDetailDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<TransportPriceListDetail> GetAllTransportPriceListDetail(string transportPriceListCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TransportPriceListDetail>();
            criteria.Add(Expression.Eq("TransportPriceList.Code", transportPriceListCode));

            return criteriaMgr.FindAll<TransportPriceListDetail>(criteria);
        }

        public TransportPriceListDetail GetLastestTransportPriceListDetail(TransportPriceList priceList, Item item, DateTime effectiveDate, Currency currency, Uom uom, string priceListType, string billingMethod)
        {
            return GetLastestTransportPriceListDetail(priceList, item, effectiveDate, currency, uom, null, null, null, priceListType, billingMethod,null);

        }

        public TransportPriceListDetail GetLastestTransportPriceListDetail(TransportPriceList priceList, DateTime effectiveDate, Currency currency, string pricingMethod, TransportationAddress shipFrom, TransportationAddress shipTo, string priceListType, string vehicleType)
        {
            return GetLastestTransportPriceListDetail(priceList, null, effectiveDate, currency, null, pricingMethod, shipFrom, shipTo, priceListType, null, vehicleType);
        }

        public TransportPriceListDetail GetLastestTransportPriceListDetail(TransportPriceList priceList, Item item, DateTime effectiveDate, Currency currency, Uom uom, string pricingMethod, TransportationAddress shipFrom, TransportationAddress shipTo, string priceListType, string billingMethod, string vehicleType)
        {
          
            DetachedCriteria detachedCriteria = DetachedCriteria.For<TransportPriceListDetail>();
            detachedCriteria.Add(Expression.Eq("TransportPriceList.Code", priceList.Code));
            if (item != null )
            {
                detachedCriteria.Add(Expression.Eq("Item.Code", item.Code));
            }
            detachedCriteria.Add(Expression.Eq("Currency.Code", currency.Code));
            if (uom != null )
            {
                detachedCriteria.Add(Expression.Eq("Uom.Code", uom.Code));
            }
            detachedCriteria.Add(Expression.Le("StartDate", effectiveDate));
            detachedCriteria.Add(Expression.Or(Expression.Ge("EndDate", effectiveDate.Date), Expression.IsNull("EndDate")));

            if (pricingMethod != null && pricingMethod != string.Empty)
            {
                detachedCriteria.Add(Expression.Eq("PricingMethod", pricingMethod));
            }
            if (shipFrom != null)
            {
                detachedCriteria.Add(Expression.Eq("ShipFrom.Id", shipFrom.Id));
            }
            if (shipTo != null)
            {
                detachedCriteria.Add(Expression.Eq("ShipTo.Id", shipTo.Id));
            }

            if (billingMethod != null && billingMethod != string.Empty)
            {
                detachedCriteria.Add(Expression.Eq("BillingMethod", billingMethod));
            }

            if (vehicleType != null && vehicleType != string.Empty)
            {
                detachedCriteria.Add(Expression.Eq("VehicleType", vehicleType));
            }
            detachedCriteria.Add(Expression.Eq("Type", priceListType));

            detachedCriteria.AddOrder(Order.Desc("StartDate")); //按StartDate降序，取最新的价格
            IList<TransportPriceListDetail> priceListDetailList = criteriaMgr.FindAll<TransportPriceListDetail>(detachedCriteria);
            if (priceListDetailList != null && priceListDetailList.Count > 0)
            {
                return priceListDetailList[0];
            }
            else
            {
                return null;
            }
        }

        public IList<TransportPriceListDetail> CheckOperation(string transportPriceListCode, string startDate, string endDate, string item, string billingMethod)
        {
            DetachedCriteria detachedCriteria = DetachedCriteria.For<TransportPriceListDetail>();
            detachedCriteria.CreateAlias("TransportPriceList", "tp");
            detachedCriteria.Add(Expression.Eq("tp.Code", transportPriceListCode));

            if (endDate != null && endDate != string.Empty)
            {
                detachedCriteria.Add(
                    Expression.Or(
                    Expression.Or(
                    Expression.And(
                    Expression.Le("StartDate", DateTime.Parse(startDate)),
                    Expression.Or(
                    Expression.IsNull("EndDate"),
                    Expression.Ge("EndDate", DateTime.Parse(startDate))
                    )
                    ),
                    Expression.And(
                    Expression.Le("StartDate", DateTime.Parse(endDate)),
                    Expression.Or(
                    Expression.IsNull("EndDate"),
                    Expression.Ge("EndDate", DateTime.Parse(endDate))
                    )
                    )
                    ),
                    Expression.And(
                    Expression.Ge("StartDate", DateTime.Parse(startDate)),
                    Expression.Or(
                    Expression.IsNull("EndDate"),
                    Expression.Le("EndDate", DateTime.Parse(endDate))
                    )
                    )
                    )
                    );
            }
            else
            {
                detachedCriteria.Add(
                    Expression.Or(
                    Expression.And(
                    Expression.Le("StartDate", DateTime.Parse(startDate)),
                    Expression.Or(
                    Expression.IsNull("EndDate"),
                    Expression.Ge("EndDate", DateTime.Parse(startDate))
                    )
                    ),
                    Expression.Ge("StartDate", DateTime.Parse(startDate))
                    )
                    );
            }
            
            if (item != null && item != string.Empty)
            {
                detachedCriteria.Add(Expression.Eq("Item.Code", item));
            }

            detachedCriteria.Add(Expression.Eq("tp.IsActive", true));

            detachedCriteria.Add(Expression.Not(Expression.Eq("BillingMethod", billingMethod)));

            return criteriaMgr.FindAll<TransportPriceListDetail>(detachedCriteria);
        }
        #endregion Customized Methods
    }
}