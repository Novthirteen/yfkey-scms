using com.Sconit.Service.Ext.MasterData;


using System;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Utility;
using System.Collections.Generic;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ActingBillMgr : ActingBillBaseMgr, IActingBillMgr
    {

        private IEntityPreferenceMgr entityPreferenceMgr;
        //private IBillTransactionMgr billTransactionMgr;
        private ICriteriaMgr criteriaMgr;
        private IPriceListDetailMgr priceListDetailMgr;

        public ActingBillMgr(IActingBillDao entityDao,
            IEntityPreferenceMgr entityPreferenceMgr,
            //IBillTransactionMgr billTransactionMgr,
            ICriteriaMgr criteriaMgr,
            IPriceListDetailMgr priceListDetailMgr)
            : base(entityDao)
        {
            this.entityPreferenceMgr = entityPreferenceMgr;
            //this.billTransactionMgr = billTransactionMgr;
            this.criteriaMgr = criteriaMgr;
            this.priceListDetailMgr = priceListDetailMgr;
        }

        #region Customized Methods
       
        [Transaction(TransactionMode.Requires)]
        public void ReverseUpdateActingBill(BillDetail oldBillDetail, BillDetail newBillDetail, User user)
        {
            if (oldBillDetail != null && newBillDetail != null
                && oldBillDetail.ActingBill.Id != newBillDetail.ActingBill.Id)
            {
                throw new TechnicalException("oldBillDetail.ActingBill.Id != newBillDetail.ActingBill.Id when ReverseUpdateActingBill");
            }

            DateTime dateTimeNow = DateTime.Now;
            #region 扣减旧BillDetail的数量和金额
            if (oldBillDetail != null)
            {
                //todo 校验数量、金额
                ActingBill actingBill = this.LoadActingBill(oldBillDetail.ActingBill.Id);
                actingBill.BilledQty -= oldBillDetail.BilledQty;
                actingBill.BilledAmount -= oldBillDetail.OrderAmount;
                actingBill.LastModifyDate = dateTimeNow;
                actingBill.LastModifyUser = user;
                if (actingBill.BillQty == actingBill.BilledQty)
                {
                    actingBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                }
                else
                {
                    actingBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                }
                this.UpdateActingBill(actingBill);
            }
            #endregion

            #region 增加新BillDetail的数量和金额
            if (newBillDetail != null)
            {
                //todo 校验数量、金额
                ActingBill actingBill = this.LoadActingBill(newBillDetail.ActingBill.Id);

                #region 根据新开票数量计算开票金额
                EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(
                                BusinessConstants.ENTITY_PREFERENCE_CODE_AMOUNT_DECIMAL_LENGTH);
                int amountDecimalLength = int.Parse(entityPreference.Value);
                if (newBillDetail.BilledQty != (actingBill.BillQty - actingBill.BilledQty))
                {
                    //本次开票数量大于剩余数量
                    if ((actingBill.BillQty > 0 && actingBill.BillQty - actingBill.BilledQty - newBillDetail.BilledQty < 0)
                        || (actingBill.BillQty < 0 && actingBill.BillQty - actingBill.BilledQty - newBillDetail.BilledQty > 0))
                    {
                        throw new BusinessErrorException("ActingBill.Error.CurrentBillQtyGeRemainQty");
                    }

                    //本次开票数量小于剩余数量
                    newBillDetail.OrderAmount = Math.Round((actingBill.BillAmount / actingBill.BillQty * newBillDetail.BilledQty), amountDecimalLength, MidpointRounding.AwayFromZero);
                }
                else
                {
                    //本次开票数量等于剩余数量
                    newBillDetail.OrderAmount = actingBill.BillAmount - actingBill.BilledAmount;
                }
                #endregion

                actingBill.BilledQty += newBillDetail.BilledQty;
                actingBill.BilledAmount += newBillDetail.OrderAmount;
                actingBill.LastModifyDate = dateTimeNow;
                actingBill.LastModifyUser = user;
                if (actingBill.BillQty == actingBill.BilledQty)
                {
                    actingBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                }
                else
                {
                    actingBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                }
                this.UpdateActingBill(actingBill);
            }
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public void RecalculatePrice(IList<ActingBill> actingBillList, User user)
        {
            this.RecalculatePrice(actingBillList, user, null);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecalculatePrice(IList<ActingBill> actingBillList, User user, DateTime? efftiveDate)
        {
            if (actingBillList != null && actingBillList.Count > 0)
            {
                DateTime dateTimeNow = DateTime.Now;

                //if (!efftiveDate.HasValue)
                //{
                //    #region 查找结算日期
                //    //DetachedCriteria criteria = DetachedCriteria.For<BillTransaction>();
                //    //criteria.Add(Expression.Eq("ActingBill", actingBill.Id));

                //    //IList<BillTransaction> billTransactionList = billTransactionMgrE.GetAllBillTransaction();
                //    //if (billTransactionList != null && billTransactionList.Count > 0)
                //    //{
                //    //    efftiveDate = billTransactionList[0].EffectiveDate;
                //    //}
                //    //else
                //    //{
                //        //没有找到结算事务，用当前时间去找价格
                //        //efftiveDate = DateTime.Now;
                //    //}
                //    #endregion
                //}

                foreach (ActingBill actingBill in actingBillList)
                {
                    PriceListDetail priceListDetail = null;
                    if (efftiveDate.HasValue)
                    {
                        priceListDetail = this.priceListDetailMgr.GetLastestPriceListDetail(actingBill.PriceList, actingBill.Item, efftiveDate.Value, actingBill.Currency, actingBill.Uom);
                    }
                    else
                    {
                        priceListDetail = this.priceListDetailMgr.GetLastestPriceListDetail(actingBill.PriceList, actingBill.Item, actingBill.EffectiveDate, actingBill.Currency, actingBill.Uom);
                    }

                    if (priceListDetail != null)
                    {
                        actingBill.UnitPrice = priceListDetail.UnitPrice;
                        actingBill.IsProvisionalEstimate = priceListDetail.IsProvisionalEstimate;
                        actingBill.LastModifyDate = dateTimeNow;
                        actingBill.LastModifyUser = user;
                        //不计折扣
                        actingBill.BillAmount = actingBill.UnitPrice * actingBill.BillQty;
                        this.UpdateActingBill(actingBill);
                    }
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ActingBill> GetUnBilledActingBill(OrderHead orderHead)
        {
            return GetUnBilledActingBill(orderHead.OrderNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ActingBill> GetUnBilledActingBill(string orderNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ActingBill>();
            criteria.Add(Expression.Eq("OrderNo", orderNo));
            criteria.Add(Expression.NotEqProperty("BillQty", "BilledQty"));
            return this.criteriaMgr.FindAll<ActingBill>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ActingBill> GetActingBill(string partyCode, string receiver, DateTime? effDateFrom, DateTime? effDateTo, string itemCode, string currency, string transType, string exceptBillNo)
        {
            return GetActingBill(partyCode, receiver, effDateFrom, effDateTo, itemCode, currency, transType, exceptBillNo, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ActingBill> GetActingBill(string partyCode, string receiver, DateTime? effDateFrom, DateTime? effDateTo, string itemCode, string currency, string transType, string exceptBillNo, bool? isProvisionalEstimate)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ActingBill>();

            criteria.CreateAlias("BillAddress", "ba");

            if (partyCode != null && partyCode != string.Empty)
            {
                criteria.Add(Expression.Eq("ba.Party.Code", partyCode));
            }

            if (receiver != null && receiver != string.Empty)
            {
                if (transType == BusinessConstants.BILL_TRANS_TYPE_PO)
                {
                    //采购查询收货单号
                    criteria.Add(Expression.Like("ReceiptNo", receiver, MatchMode.Start));
                }
                else
                {
                    //销售查询客户回单号
                    criteria.Add(Expression.Like("ExternalReceiptNo", receiver, MatchMode.Start));
                }
            }

            if (effDateFrom.HasValue)
            {
                criteria.Add(Expression.Ge("EffectiveDate", effDateFrom.Value));
            }

            if (effDateTo.HasValue)
            {
                criteria.Add(Expression.Le("EffectiveDate", effDateTo.Value));
            }

            if (itemCode != null && itemCode != string.Empty)
            {
                criteria.Add(Expression.Eq("Item.Code", itemCode));
            }

            if (currency != null && currency != string.Empty)
            {
                criteria.Add(Expression.Eq("Currency.Code", currency));
            }

            if (exceptBillNo != null && exceptBillNo != string.Empty)
            {
                DetachedCriteria bCriteria = DetachedCriteria.For<BillDetail>();
                bCriteria.Add(Expression.Eq("Bill.BillNo", exceptBillNo));

                IList<BillDetail> billDetailList = this.criteriaMgr.FindAll<BillDetail>(bCriteria);

                if (billDetailList != null && billDetailList.Count > 0)
                {
                    List<int> idList = new List<int>();

                    foreach (BillDetail billDetail in billDetailList)
                    {
                        idList.Add(billDetail.ActingBill.Id);
                    }

                    criteria.Add(Expression.Not(Expression.In("Id", idList)));
                }
            }

            criteria.Add(Expression.Eq("TransactionType", transType));

            if (isProvisionalEstimate.HasValue)
            {
                criteria.Add(Expression.Eq("IsProvisionalEstimate", isProvisionalEstimate));   //非暂估价
            }

            criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));

            return this.criteriaMgr.FindAll<ActingBill>(criteria);
        }

        #endregion Customized Methods
    }
}
