using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Transportation;
using com.Sconit.Persistence.Transportation;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity.Distribution;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationActBillMgr : TransportationActBillBaseMgr, ITransportationActBillMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private ITransportPriceListMgr transportPriceListMgr;
        private ITransportPriceListDetailMgr transportPriceListDetailMgr;
        private ICurrencyMgr currencyMgr;
        private IBillAddressMgr billAddressMgr;
        private IUserMgr userMgr;

        public TransportationActBillMgr(ITransportationActBillDao entityDao,
            ICriteriaMgr criteriaMgr,
            IEntityPreferenceMgr entityPreferenceMgr,
            ITransportPriceListMgr transportPriceListMgr,
            ITransportPriceListDetailMgr transportPriceListDetailMgr,
            ICurrencyMgr currencyMgr,
            IBillAddressMgr billAddressMgr,
            IUserMgr userMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.transportPriceListMgr = transportPriceListMgr;
            this.transportPriceListDetailMgr = transportPriceListDetailMgr;
            this.currencyMgr = currencyMgr;
            this.billAddressMgr = billAddressMgr;
            this.userMgr = userMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public void ReverseUpdateTransportationActBill(TransportationBillDetail oldBillDetail, TransportationBillDetail newBillDetail, User user)
        {
            if (oldBillDetail != null && newBillDetail != null
                && oldBillDetail.ActBill.Id != newBillDetail.ActBill.Id)
            {
                throw new TechnicalException("oldBillDetail.ActingBill.Id != newBillDetail.ActingBill.Id when ReverseUpdateActingBill");
            }

            DateTime dateTimeNow = DateTime.Now;
            #region 扣减旧TransportationBillDetail的数量和金额
            if (oldBillDetail != null)
            {
                //todo 校验数量、金额
                TransportationActBill transportationActBill = this.LoadTransportationActBill(oldBillDetail.ActBill.Id);

                /*
                 * 
                 * 1.TransType=Transportation 价格单明细（承运商） 或  短拨费（区域）时
                 * a.PricingMethod=M3或KG  按数量
                 * b.SHIPT   按金额
                 * 2.TransType=WarehouseLease(固定费用) 按金额
                 * 3.TransType=Operation(操作费) 按数量
                 */
                if (transportationActBill.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION
                    ||
                    (transportationActBill.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION
                    && (transportationActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3 || transportationActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_KG)
                    )
                    )
                {
                    transportationActBill.BilledQty -= oldBillDetail.BilledQty;
                    transportationActBill.BilledAmount -= oldBillDetail.Amount;
                    if (transportationActBill.BillQty == transportationActBill.BilledQty)
                    {
                        transportationActBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                    }
                    else
                    {
                        transportationActBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                    }
                }
                else
                {
                    transportationActBill.BilledAmount -= oldBillDetail.Amount;

                    if (transportationActBill.BilledAmount == 0)
                    {
                        transportationActBill.BilledQty = 0;
                    }

                    if (transportationActBill.BillAmount == transportationActBill.BilledAmount)
                    {
                        transportationActBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                    }
                    else
                    {
                        transportationActBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                    }
                }

                transportationActBill.LastModifyDate = dateTimeNow;
                transportationActBill.LastModifyUser = user;

                this.UpdateTransportationActBill(transportationActBill);
            }
            #endregion

            #region 增加新BillDetail的数量和金额
            if (newBillDetail != null)
            {
                //todo 校验数量、金额
                TransportationActBill transportationActBill = this.LoadTransportationActBill(newBillDetail.ActBill.Id);

                EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(
                                BusinessConstants.ENTITY_PREFERENCE_CODE_AMOUNT_DECIMAL_LENGTH);
                int amountDecimalLength = int.Parse(entityPreference.Value);

                /*
                 * 
                 * 1.TransType=Transportation 价格单明细（承运商） 或  短拨费（区域）时
                 * a.PricingMethod=M3或KG  按数量
                 * b.SHIPT   按金额
                 * 2.TransType=WarehouseLease(固定费用) 按金额
                 * 3.TransType=Operation(操作费) 按数量
                 */
                if (transportationActBill.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION
                    ||
                    (transportationActBill.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION
                    && (transportationActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3 || transportationActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_KG)
                    )
                    )
                {
                    #region 根据新开票数量计算开票金额
                    if (newBillDetail.BilledQty != (transportationActBill.BillQty - transportationActBill.BilledQty))
                    {
                        //本次开票数量大于剩余数量
                        if ((transportationActBill.BillQty > 0 &&
                             transportationActBill.BillQty - transportationActBill.BilledQty - newBillDetail.BilledQty <
                             0)
                            ||
                            (transportationActBill.BillQty < 0 &&
                             transportationActBill.BillQty - transportationActBill.BilledQty - newBillDetail.BilledQty >
                             0))
                        {
                            throw new BusinessErrorException("TransportationActBill.Error.CurrentBillQtyGeRemainQty");
                        }

                        //本次开票数量小于剩余数量
                        newBillDetail.Amount =
                            Math.Round(
                                (transportationActBill.BillAmount / transportationActBill.BillQty * newBillDetail.BilledQty),
                                amountDecimalLength, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        //本次开票数量等于剩余数量
                        newBillDetail.Amount = transportationActBill.BillAmount - transportationActBill.BilledAmount;
                    }
                    #endregion

                    transportationActBill.BilledQty += newBillDetail.BilledQty;
                    transportationActBill.BilledAmount += newBillDetail.Amount;
                    if (transportationActBill.BillQty == transportationActBill.BilledQty)
                    {
                        transportationActBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                    }
                    else
                    {
                        transportationActBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                    }
                }
                else
                {
                    if (newBillDetail.Amount != (transportationActBill.BillAmount - transportationActBill.BilledAmount))
                    {
                        //本次开票金额大于剩余金额
                        if ((transportationActBill.BillAmount > 0 &&
                             transportationActBill.BillAmount - transportationActBill.BilledAmount - newBillDetail.Amount <
                             0)
                            ||
                            (transportationActBill.BillAmount < 0 &&
                             transportationActBill.BillAmount - transportationActBill.BilledAmount - newBillDetail.Amount >
                             0))
                        {
                            throw new BusinessErrorException("TransportationActBill.Error.CurrentBillAmountGeRemainAmount");
                        }
                    }
                    else
                    {
                        //本次开票金额等于剩余金额
                        newBillDetail.Amount = transportationActBill.BillAmount - transportationActBill.BilledAmount;
                    }

                    transportationActBill.BilledQty = 1;
                    transportationActBill.BilledAmount += newBillDetail.Amount;
                    if (transportationActBill.BillAmount == transportationActBill.BilledAmount)
                    {
                        transportationActBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                    }
                    else
                    {
                        transportationActBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                    }
                }

                transportationActBill.LastModifyDate = dateTimeNow;
                transportationActBill.LastModifyUser = user;
                this.UpdateTransportationActBill(transportationActBill);
            }
            #endregion
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<TransportationActBill> GetTransportationActBill(string partyCode, string expenseNo, DateTime? effDateFrom, DateTime? effDateTo, string itemCode, string currency, string exceptBillNo)
        {
            return GetTransportationActBill(partyCode, expenseNo, effDateFrom, effDateTo, itemCode, currency, exceptBillNo, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<TransportationActBill> GetTransportationActBill(string partyCode, string expenseNo, DateTime? effDateFrom, DateTime? effDateTo, string itemCode, string currency, string exceptBillNo, bool? isProvisionalEstimate)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TransportationActBill>();

            criteria.CreateAlias("BillAddress", "ba");

            if (partyCode != null && partyCode != string.Empty)
            {
                criteria.Add(Expression.Eq("ba.Party.Code", partyCode));
            }

            if (expenseNo != null && expenseNo != string.Empty)
            {
                criteria.Add(Expression.Like("ExpenseNo", expenseNo, MatchMode.Start));
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
                DetachedCriteria bCriteria = DetachedCriteria.For<TransportationBillDetail>();
                bCriteria.Add(Expression.Eq("Bill.BillNo", exceptBillNo));

                IList<TransportationBillDetail> transportationBillDetailList = this.criteriaMgr.FindAll<TransportationBillDetail>(bCriteria);

                if (transportationBillDetailList != null && transportationBillDetailList.Count > 0)
                {
                    List<int> idList = new List<int>();

                    foreach (TransportationBillDetail transportationBillDetail in transportationBillDetailList)
                    {
                        idList.Add(transportationBillDetail.ActBill.Id);
                    }

                    criteria.Add(Expression.Not(Expression.In("Id", idList)));
                }
            }

            if (isProvisionalEstimate.HasValue)
            {
                criteria.Add(Expression.Eq("IsProvisionalEstimate", isProvisionalEstimate));   //非暂估价
            }

            criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));

            return this.criteriaMgr.FindAll<TransportationActBill>(criteria);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecalculatePrice(IList<TransportationActBill> transportationActBillList, User user)
        {
            this.RecalculatePrice(transportationActBillList, user, null);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecalculatePrice(IList<TransportationActBill> transportationActBillList, User user, DateTime? efftiveDate)
        {
            if (transportationActBillList != null && transportationActBillList.Count > 0)
            {
                DateTime dateTimeNow = DateTime.Now;

                foreach (TransportationActBill transportationActBill in transportationActBillList)
                {
                    TransportPriceListDetail transportPriceListDetail = null;
                    if (efftiveDate.HasValue)
                    {
                        if (transportationActBill.PriceList != null)
                        {
                            transportPriceListDetail = this.transportPriceListDetailMgr.GetLastestTransportPriceListDetail(transportationActBill.PriceList, efftiveDate.Value, transportationActBill.Currency, transportationActBill.PricingMethod, transportationActBill.ShipFrom, transportationActBill.ShipTo, BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION, transportationActBill.VehicleType, transportationActBill.TransportMethod);
                        }
                    }
                    else
                    {
                        if (transportationActBill.PriceList != null)
                        {
                            transportPriceListDetail = this.transportPriceListDetailMgr.GetLastestTransportPriceListDetail(transportationActBill.PriceList, transportationActBill.EffectiveDate, transportationActBill.Currency, transportationActBill.PricingMethod, transportationActBill.ShipFrom, transportationActBill.ShipTo, BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION, transportationActBill.VehicleType, transportationActBill.TransportMethod);
                        }
                    }

                    if (transportPriceListDetail != null)
                    {
                        transportationActBill.UnitPrice = transportPriceListDetail.UnitPrice;
                        transportationActBill.IsProvisionalEstimate = transportPriceListDetail.IsProvisionalEstimate;
                        transportationActBill.LastModifyDate = dateTimeNow;
                        transportationActBill.LastModifyUser = user;
                        //不计折扣
                        transportationActBill.BillAmount = transportationActBill.UnitPrice * transportationActBill.BillQty;
                        this.UpdateTransportationActBill(transportationActBill);
                    }
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public TransportationActBill CreateTransportationActBill(TransportationOrder order, User user)
        {

            TransportationActBill actBill = new TransportationActBill();
            if (order.Expense != null)
            {

                actBill.BillAmount = order.Expense.Amount;
                actBill.UnitPrice = order.Expense.Amount;
                actBill.BillQty = 1;
                actBill.Currency = order.Expense.Currency;
                actBill.IsIncludeTax = order.Expense.IsIncludeTax;
                actBill.Currency.Code = order.Expense.Currency.Code;
                actBill.IsProvisionalEstimate = false;

            }
            else
            {
                string currency = null;
                foreach (TransportationOrderDetail orderDetail in order.OrderDetails)
                {
                    #region currency
                    if (orderDetail.InProcessLocation.Flow.Currency == null)
                    {
                        throw new BusinessErrorException("Transportation.Flow.CurrencyEmpty", orderDetail.InProcessLocation.Flow.Code);
                    }
                    if (currency == null)
                    {
                        currency = orderDetail.InProcessLocation.Flow.Currency.Code;
                    }
                    else if (currency != orderDetail.InProcessLocation.Flow.Currency.Code)
                    {
                        throw new BusinessErrorException("Transportation.OrderDetail.CurrencyNotEqual");
                    }
                    #endregion
                }

                IList<TransportPriceList> transportPriceList = transportPriceListMgr.GetTransportPriceList(order.Carrier.Code);
                if (transportPriceList == null || transportPriceList.Count == 0)
                {
                    throw new BusinessErrorException("Transportation.PriceList.Empty", order.Carrier.Code);
                }
                if (transportPriceList.Count > 1)
                {
                    throw new BusinessErrorException("Transportation.PriceList.MoreThanOne", order.Carrier.Code);
                }

                TransportPriceListDetail priceListDetail = null;
                if (order.PricingMethod != BusinessConstants.TRANSPORTATION_PRICING_METHOD_LADDERSTERE)
                {
                    priceListDetail = this.transportPriceListDetailMgr.GetLastestTransportPriceListDetail(transportPriceList[0]
                       , order.CreateDate, currencyMgr.LoadCurrency(currency), order.PricingMethod, order.TransportationRoute.ShipFrom, order.TransportationRoute.ShipTo, BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION, order.VehicleType,order.TransportMethod);

                }

                #region 包车
                if (order.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_SHIPT)
                {
                    actBill.BillQty = 1;
                    if (priceListDetail != null && actBill.UnitPrice == 0)
                    {
                        actBill.UnitPrice = priceListDetail.UnitPrice;
                        actBill.BillAmount = actBill.UnitPrice * actBill.BillQty;
                    }
                }
                #endregion

                #region 体积和阶梯
                else if (order.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3 || order.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_LADDERSTERE)
                {
                    decimal totalVolume = 0;
                    foreach (TransportationOrderDetail orderDetail in order.OrderDetails)
                    {
                        foreach (InProcessLocationDetail ipDet in orderDetail.InProcessLocation.InProcessLocationDetails)
                        {
                            if (!ipDet.OrderLocationTransaction.OrderDetail.PackageVolumn.HasValue || ipDet.OrderLocationTransaction.OrderDetail.PackageVolumn.Value == 0)
                            {
                                throw new BusinessErrorException("Transportation.PackageVolumn.Empty", ipDet.InProcessLocation.IpNo, ipDet.OrderLocationTransaction.Item.Code);
                            }
                            else
                            {
                                if (ipDet.OrderLocationTransaction.OrderDetail.HuLotSize == null || ipDet.OrderLocationTransaction.OrderDetail.HuLotSize.Value == 0)
                                {
                                    throw new BusinessErrorException("Transportation.HuLotSize.Empty", ipDet.InProcessLocation.IpNo, ipDet.OrderLocationTransaction.Item.Code);
                                }
                                int box = Convert.ToInt32(Math.Ceiling(ipDet.Qty / (decimal)(ipDet.OrderLocationTransaction.OrderDetail.HuLotSize.Value)));
                                totalVolume += ipDet.OrderLocationTransaction.OrderDetail.PackageVolumn.Value * box;
                            }
                        }
                    }

                    #region 托盘数
                    if (order.PallentCount != 0)
                    {

                        decimal pallentVolume = decimal.Parse(entityPreferenceMgr.LoadEntityPreference(
                                BusinessConstants.ENTITY_PREFERENCE_CODE_PALLENTVOLUME).Value);
                        totalVolume += pallentVolume * order.PallentCount;
                    }
                    #endregion

                    if (order.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3)
                    {
                        #region 最小起运量
                        if (totalVolume < priceListDetail.MinVolume)
                        {
                            totalVolume = priceListDetail.MinVolume;
                        }
                        #endregion

                        actBill.BillQty = totalVolume;
                        if (priceListDetail != null && actBill.UnitPrice == 0)
                        {
                            actBill.UnitPrice = priceListDetail.UnitPrice;
                        }
                        actBill.BillAmount = actBill.UnitPrice * actBill.BillQty;
                    }
                    #region 阶梯
                    else if (order.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_LADDERSTERE)
                    {
                        priceListDetail = this.transportPriceListDetailMgr.GetLastestLadderStereTransportPriceListDetail(transportPriceList[0], null, order.CreateDate, currencyMgr.LoadCurrency(currency), null, order.PricingMethod, order.TransportationRoute.ShipFrom, order.TransportationRoute.ShipTo, BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION, null, order.VehicleType, totalVolume);
                        if (priceListDetail == null)
                        {
                            throw new BusinessErrorException("Transportation.PriceListDetail.Empty", order.PricingMethod);
                        }

                        #region 最小起运量
                        if (totalVolume < priceListDetail.MinVolume)
                        {
                            totalVolume = priceListDetail.MinVolume;
                        }
                        #endregion
                        actBill.UnitPrice = priceListDetail.UnitPrice;
                        actBill.BillQty = totalVolume;
                        decimal minPrice = priceListDetail.MinPrice.HasValue ? priceListDetail.MinPrice.Value : 0;
                        actBill.BillAmount = minPrice + actBill.UnitPrice * actBill.BillQty;
                    }
                    #endregion
                }
                #endregion

                #region 重量
                else if (order.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_KG)
                {

                }
                #endregion



                else
                {
                    throw new BusinessErrorException("Transportation.PricingMethod.Empty");
                }

                actBill.UnitPrice = priceListDetail.UnitPrice;
                // actBill.BillAmount = actBill.UnitPrice * actBill.BillQty;
                actBill.Currency = priceListDetail.Currency;
                actBill.IsIncludeTax = priceListDetail.IsIncludeTax;
                actBill.Currency = priceListDetail.Currency;
                actBill.IsProvisionalEstimate = priceListDetail.IsProvisionalEstimate;
                actBill.PricingMethod = order.PricingMethod;
                actBill.PriceList = priceListDetail.TransportPriceList;
                actBill.PriceListDetail = priceListDetail;
                actBill.VehicleType = priceListDetail.VehicleType;
                if (order.TransportationRoute != null)
                {
                    actBill.ShipFrom = order.TransportationRoute.ShipFrom;
                    actBill.ShipTo = order.TransportationRoute.ShipTo;
                }
            }

            actBill.OrderNo = order.OrderNo;
            actBill.BillAddress = order.CarrierBillAddress;
            actBill.EffectiveDate = DateTime.Parse(order.CreateDate.ToString("yyyy-MM-dd"));
            actBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
            actBill.TransType = BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION;
            actBill.CreateDate = DateTime.Now;
            actBill.CreateUser = user;
            actBill.LastModifyDate = DateTime.Now;
            actBill.LastModifyUser = user;
            actBill.TransportMethod = order.TransportMethod;
            this.CreateTransportationActBill(actBill);
            return actBill;
        }


        [Transaction(TransactionMode.Unspecified)]
        public IList<TransportationActBill> GetTransportationActBill(string orderNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TransportationActBill>();
            criteria.Add(Expression.Eq("OrderNo", orderNo));

            IList<TransportationActBill> actBillList = this.criteriaMgr.FindAll<TransportationActBill>(criteria);
            return actBillList;
        }


        [Transaction(TransactionMode.Unspecified)]
        public IList<TransportationActBill> GetTransportationActBill(TransportationOrder order)
        {
            return GetTransportationActBill(order.OrderNo);
        }


        [Transaction(TransactionMode.Unspecified)]
        public void CalculateFixPriceList()
        {
            DetachedCriteria criteria = DetachedCriteria.For<TransportPriceListDetail>();
            criteria.CreateAlias("TransportPriceList", "tpl");
            criteria.Add(Expression.Eq("tpl.IsActive", true));
            criteria.Add(Expression.Eq("Type", BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_WAREHOUSELEASE));
            criteria.Add(Expression.Le("StartDate", DateTime.Now));
            criteria.Add(Expression.Or(Expression.Ge("EndDate", DateTime.Now), Expression.IsNull("EndDate")));

            IList<TransportPriceListDetail> priceListDetailList = this.criteriaMgr.FindAll<TransportPriceListDetail>(criteria);
            foreach (TransportPriceListDetail priceListDetail in priceListDetailList)
            {
                CreateTransportationActBill(priceListDetail);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void CreateTransportationActBill(TransportPriceListDetail priceListDetail)
        {

            TransportationActBill actBill = new TransportationActBill();
            actBill.BillAddress = billAddressMgr.GetDefaultBillAddress(priceListDetail.TransportPriceList.Party.Code);

            if (actBill.BillAddress == null)
            {
                throw new BusinessErrorException("Transportation.Error.CarrierPrimaryBillAddressEmpty");
            }

            actBill.PriceListDetail = priceListDetail;
            actBill.PriceList = priceListDetail.TransportPriceList;
            actBill.UnitPrice = priceListDetail.UnitPrice * (1 + priceListDetail.ServiceCharge);
            actBill.TransType = BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_WAREHOUSELEASE;

            actBill.BillQty = 1;
            actBill.BillAmount = actBill.BillQty * actBill.UnitPrice;
            actBill.Currency = priceListDetail.Currency;
            actBill.EffectiveDate = DateTime.Now.Date;
            actBill.CreateDate = DateTime.Now;
            actBill.CreateUser = userMgr.GetMonitorUser();
            actBill.IsIncludeTax = priceListDetail.IsIncludeTax;
            actBill.IsProvisionalEstimate = priceListDetail.IsProvisionalEstimate;
            actBill.LastModifyDate = DateTime.Now;
            actBill.LastModifyUser = userMgr.GetMonitorUser();
            actBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
            actBill.TaxCode = priceListDetail.TaxCode;

            CreateTransportationActBill(actBill);
        }

        [Transaction(TransactionMode.Unspecified)]
        public TransportationActBill CreateTransportationItemActBill(Receipt receipt, string billingMethod)
        {
            TransportationActBill actBill = new TransportationActBill();
            TransportPriceList transportPriceList = null;
            if (billingMethod == BusinessConstants.TRANSPORTATION_BILLING_METHOD_OUT)
            {
                transportPriceList = transportPriceListMgr.LoadTransportPriceList(receipt.PartyFrom.Code);
            }
            if (transportPriceList != null)
            {
                Currency currency = receipt.ReceiptDetails[0].OrderLocationTransaction.OrderDetail.OrderHead.Currency;

                foreach (ReceiptDetail receiptDetail in receipt.ReceiptDetails)
                {
                    TransportPriceListDetail priceListDetail = transportPriceListDetailMgr.GetLastestTransportPriceListDetail(transportPriceList, receiptDetail.OrderLocationTransaction.Item,
                          receipt.CreateDate, currency, receiptDetail.OrderLocationTransaction.OrderDetail.Item.Uom, BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION, billingMethod);

                    if (priceListDetail != null)
                    {
                        priceListDetail = transportPriceListDetailMgr.GetLastestTransportPriceListDetail(transportPriceList, null,
                          receipt.CreateDate, currency, null, BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION, billingMethod);
                    }

                    actBill.BillAddress = billAddressMgr.GetDefaultBillAddress(priceListDetail.TransportPriceList.Party.Code);
                    actBill.PriceListDetail = priceListDetail;
                    actBill.PriceList = priceListDetail.TransportPriceList;
                    actBill.UnitPrice = priceListDetail.UnitPrice * (1 + priceListDetail.ServiceCharge);
                    actBill.TransType = BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION;

                    actBill.BillQty = receiptDetail.ReceivedQty.Value;
                    actBill.BillAmount = actBill.BillQty * actBill.UnitPrice;
                    actBill.Currency = priceListDetail.Currency;
                    actBill.EffectiveDate = DateTime.Now.Date;
                    actBill.CreateDate = DateTime.Now;
                    actBill.CreateUser = userMgr.GetMonitorUser();
                    actBill.IsIncludeTax = priceListDetail.IsIncludeTax;
                    actBill.IsProvisionalEstimate = priceListDetail.IsProvisionalEstimate;
                    actBill.LastModifyDate = DateTime.Now;
                    actBill.LastModifyUser = userMgr.GetMonitorUser();
                    actBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                    actBill.TaxCode = priceListDetail.TaxCode;

                }
            }
            return actBill;
        }

        #endregion Customized Methods
    }
}