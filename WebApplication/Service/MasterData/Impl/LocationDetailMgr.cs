using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.Distribution;
using NHibernate.Expression;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.View;
using com.Sconit.Utility;
using NHibernate.Transform;
using System.Linq;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class LocationDetailMgr : LocationDetailBaseMgr, ILocationDetailMgr
    {
        private ICriteriaMgr CriteriaMgr;
        private IRegionMgr RegionMgr;
        private ILocationMgr LocationMgr;
        private IItemMgr ItemMgr;
        private IOrderLocationTransactionMgr OrderLocTransMgr;
        private ILocationTransactionMgr LocTransMgr;
        private IInProcessLocationDetailMgr IPLocDetMgr;
        private IUomConversionMgr UomConversionMgr;
        private IPickListResultMgr PickListResultMgr;
        public LocationDetailMgr(ILocationDetailDao entityDao,
            ICriteriaMgr CriteriaMgr,
            IRegionMgr RegionMgr,
            ILocationMgr LocationMgr,
            IItemMgr ItemMgr,
            IOrderLocationTransactionMgr OrderLocTransMgr,
            ILocationTransactionMgr LocTransMgr,
            IInProcessLocationDetailMgr IPLocDetMgr,
            IUomConversionMgr UomConversionMgr,
            IPickListResultMgr PickListResultMgr)
            : base(entityDao)
        {
            this.CriteriaMgr = CriteriaMgr;
            this.RegionMgr = RegionMgr;
            this.LocationMgr = LocationMgr;
            this.ItemMgr = ItemMgr;
            this.OrderLocTransMgr = OrderLocTransMgr;
            this.LocTransMgr = LocTransMgr;
            this.IPLocDetMgr = IPLocDetMgr;
            this.UomConversionMgr = UomConversionMgr;
            this.PickListResultMgr = PickListResultMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public LocationDetail GetLocationDetail(string location, string item)
        {
            IList<LocationDetail> locationDetailList = this.GetLocationDetailList(location, item);
            if (locationDetailList != null && locationDetailList.Count > 0)
                return locationDetailList[0];
            else
                return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        public decimal GetCurrentInv(string location, string item)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationDetail));
            if (location != null && location.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Location.Code", location));
            if (item != null && item.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Item.Code", item));

            criteria.SetProjection(Projections.Sum("Qty"));
            IList result = CriteriaMgr.FindAll(criteria);
            if (result[0] != null)
            {
                return (decimal)result[0];
            }
            else
            {
                return 0;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationDetail> GetLocationDetailList(string location, string item)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationDetail));
            if (location != null && location.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Location.Code", location));
            if (item != null && item.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Item.Code", item));
            return CriteriaMgr.FindAll<LocationDetail>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationDetail> FindLocationDetail(IList<string> locationCode, IList<string> itemCode, DateTime? effectiveDate, string userCode)
        {
            return this.FindLocationDetail(locationCode, itemCode, effectiveDate, userCode, false, 0, 0);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationDetail> FindLocationDetail(IList<string> locationCode, IList<string> itemCode, DateTime? effectiveDate, string userCode, bool includeActiveOrder)
        {
            return this.FindLocationDetail(locationCode, itemCode, effectiveDate, userCode, false, 0, 0);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationDetail> FindLocationDetail(IList<string> locationCode, IList<string> itemCode, DateTime? effectiveDate, string userCode, bool includeActiveOrder, int pageSize, int pageIndex)
        {
            IList<LocationDetail> ldList = new List<LocationDetail>();
            IList<Region> regionList = RegionMgr.GetRegion(userCode);

            IList<Location> locList = new List<Location>();
            IList<Location> inputLocList = this.LocationMgr.GetLocation(locationCode);
            if (inputLocList != null && inputLocList.Count > 0)
            {
                foreach (Location location in inputLocList)
                {
                    if (regionList != null && regionList.Count > 0 && regionList.IndexOf(location.Region) < 0)
                        continue;

                    locList.Add(location);
                }
            }
            else
            {
                locList = LocationMgr.GetLocationByUserCode(userCode);
            }

            if (pageSize > 0 && pageIndex < 1)
            {
                throw new TechnicalException("CurrentPage can't less than 1");
            }

            int totalCount = 0;
            int fromCount = (pageIndex - 1) * pageSize;
            int toCount = pageIndex * pageSize;

            if (locList != null && locList.Count > 0)
            {
                IList<Item> itemList = this.ItemMgr.GetItem(itemCode);
                IList<Item> newItemList = new List<Item>();
                foreach (Location location in locList)
                {
                    if (pageSize > 0 && totalCount >= toCount)
                    {
                        break;
                    }

                    newItemList = new List<Item>();
                    IList<LocationDetail> preldList = this.GetLocationDetailList(location.Code, null);
                    if (preldList != null && preldList.Count > 0)
                    {

                        foreach (LocationDetail preld in preldList)
                        {
                            if (itemList == null || itemList.Count == 0 || itemList.Contains(preld.Item))
                            {
                                if (preld.ConsignmentQty != 0 || preld.NormalQty != 0 || preld.InvQty != 0 ||
                                   preld.QtyToBeIn != 0 || preld.InTransitQty != 0 || preld.QtyToBeOut != 0 || preld.PAB != 0)
                                {
                                    newItemList.Add(preld.Item);
                                }
                            }
                        }
                    }


                    if (newItemList != null && newItemList.Count > 0)
                    {
                        foreach (Item item in newItemList)
                        {
                            if (pageSize > 0 && totalCount >= toCount)
                            {
                                break;
                            }

                            totalCount++;

                            if (pageSize == 0 || totalCount > fromCount)
                            {
                                LocationDetail ld = this.FindLocationDetail(location, item, effectiveDate, includeActiveOrder);
                                if (ld != null)
                                {
                                    ldList.Add(ld);
                                }
                            }
                        }
                    }
                }
            }

            return ldList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public int FindLocationDetailCount(IList<string> locationCode, IList<string> itemCode, DateTime? effectiveDate, string userCode, bool includeActiveOrder)
        {
            int count = 0;
            IList<Region> regionList = RegionMgr.GetRegion(userCode);

            IList<Location> locList = new List<Location>();
            IList<Location> inputLocList = this.LocationMgr.GetLocation(locationCode);
            if (inputLocList != null && inputLocList.Count > 0)
            {
                foreach (Location location in inputLocList)
                {
                    if (regionList != null && regionList.Count > 0 && regionList.IndexOf(location.Region) < 0)
                        continue;

                    locList.Add(location);
                }
            }
            else
            {
                locList = LocationMgr.GetLocationByUserCode(userCode);
            }

            if (locList != null && locList.Count > 0)
            {
                IList<Item> itemList = this.ItemMgr.GetItem(itemCode);
                IList<Item> newItemList = new List<Item>();
                foreach (Location location in locList)
                {

                    newItemList = new List<Item>();
                    IList<LocationDetail> preldList = this.GetLocationDetailList(location.Code, null);
                    if (preldList != null && preldList.Count > 0)
                    {

                        foreach (LocationDetail preld in preldList)
                        {
                            if (itemList == null || itemList.Count == 0 || itemList.Contains(preld.Item))
                            {
                                if (preld.ConsignmentQty != 0 || preld.NormalQty != 0 || preld.InvQty != 0 ||
                                    preld.QtyToBeIn != 0 || preld.InTransitQty != 0 || preld.QtyToBeOut != 0 || preld.PAB != 0)
                                {

                                    newItemList.Add(preld.Item);
                                }
                            }
                        }
                    }


                    if (newItemList != null && newItemList.Count > 0)
                    {
                        foreach (Item item in newItemList)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        [Transaction(TransactionMode.Unspecified)]
        public LocationDetail FindLocationDetail(string loc, string itemCode, DateTime? effectiveDate)
        {
            return this.FindLocationDetail(loc, itemCode, effectiveDate, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public LocationDetail FindLocationDetail(string loc, string itemCode, DateTime? effectiveDate, bool includeActiveOrder)
        {
            Item item = ItemMgr.LoadItem(itemCode);
            Location location = LocationMgr.LoadLocation(loc);
            if (location == null)
                return null;
            else
                return this.FindLocationDetail(location, item, effectiveDate);
        }

        [Transaction(TransactionMode.Unspecified)]
        public LocationDetail FindLocationDetail(Location location, Item item, DateTime? effectiveDate)
        {
            return this.FindLocationDetail(location, item, effectiveDate, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public LocationDetail FindLocationDetail(Location location, Item item, DateTime? effectiveDate, bool includeActiveOrder)
        {
            //精确到日期
            DateTime effDate = effectiveDate.HasValue ? ((DateTime)effectiveDate).Date : DateTime.MaxValue;

            LocationDetail ld = this.GetLocationDetail(location.Code, item.Code);
            if (ld == null)
            {
                if (!includeActiveOrder)
                    return null;

                ld = new LocationDetail();
                ld.Location = location;
                ld.Item = item;
                ld.InvQty = 0;
            }
            ld.InvQty = ld.Qty;

            //History inventory
            IList<LocationTransaction> locTransList = LocTransMgr.GetLocationTransactionAfterEffDate(item.Code, location.Code, effDate);
            decimal transQty = 0;
            if (locTransList != null && locTransList.Count > 0)
            {
                foreach (LocationTransaction locTrans in locTransList)
                {
                    transQty += locTrans.Qty;
                }
            }
            ld.InvQty = ld.InvQty - transQty;

            if (includeActiveOrder)
            {
                IList<OrderLocationTransaction> outOrderLocTransList = OrderLocTransMgr.GetOpenOrderLocTransOut(item.Code, location.Code, BusinessConstants.IO_TYPE_OUT, effDate);
                decimal qtyOut = 0;
                if (outOrderLocTransList != null && outOrderLocTransList.Count > 0)
                {
                    foreach (OrderLocationTransaction orderLocTrans in outOrderLocTransList)
                    {
                        //待发
                        qtyOut += orderLocTrans.RemainQty;
                    }
                }

                IList<OrderLocationTransaction> inOrderLocTransList = OrderLocTransMgr.GetOpenOrderLocTransIn(item.Code, location.Code, BusinessConstants.IO_TYPE_IN, effDate);
                decimal qtyIn = 0;
                decimal qtyInTransit = 0;
                if (inOrderLocTransList != null && inOrderLocTransList.Count > 0)
                {
                    foreach (OrderLocationTransaction orderLocTrans in inOrderLocTransList)
                    {
                        IList<InProcessLocationDetail> inTransitList = IPLocDetMgr.GetInProcessLocationDetail(orderLocTrans.OrderDetail);
                        decimal totalQtyInTransit = 0;
                        if (inTransitList != null && inTransitList.Count > 0)
                        {
                            foreach (InProcessLocationDetail ipld in inTransitList)
                            {
                                if (ipld.InProcessLocation.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                                {
                                    //只有未收货的才计入在途
                                    totalQtyInTransit += ipld.Qty * ipld.OrderLocationTransaction.UnitQty;
                                }
                                else if (ipld.InProcessLocation.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                                {
                                    if (ipld.ReceivedQty.HasValue)
                                    {
                                        totalQtyInTransit += (ipld.Qty - ipld.ReceivedQty.Value) * ipld.OrderLocationTransaction.UnitQty;
                                    }
                                    else
                                    {
                                        totalQtyInTransit += ipld.Qty * ipld.OrderLocationTransaction.UnitQty;
                                    }
                                }
                            }
                        }

                        //在途
                        qtyInTransit += totalQtyInTransit;

                        //待收
                        qtyIn += orderLocTrans.RemainQty - totalQtyInTransit;
                    }
                }

                ld.QtyToBeOut = qtyOut;
                ld.QtyToBeIn = qtyIn;
                ld.InTransitQty = qtyInTransit;

                //预计可用库存 = 当前库存 + 待收(剩余) + 在途 - 待发
                ld.PAB = ld.InvQty + qtyIn + qtyInTransit - qtyOut;
            }

            return ld;
        }

        #region Old method
        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationDetail> GetInvIOB(IList<string> locationCode, IList<string> itemCode, DateTime startDate, DateTime endDate, string userCode)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            IList<LocationDetail> locDetList = this.FindLocationDetail(locationCode, itemCode, endDate, userCode);
            if (locDetList != null && locDetList.Count > 0)
            {
                foreach (LocationDetail ld in locDetList)
                {
                    //采购收货、退货、调整
                    ld.RCTPO = this.SumTransQty(LocTransMgr.GetPeriodLocationTransaction(ld.Item.Code, ld.Location.Code, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PO, startDate, endDate));

                    //移库入库、退货、调整
                    ld.RCTTR = this.SumTransQty(LocTransMgr.GetPeriodLocationTransaction(ld.Item.Code, ld.Location.Code, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_TR, startDate, endDate));

                    //生产入库、退货、调整
                    ld.RCTWO = this.SumTransQty(LocTransMgr.GetPeriodLocationTransaction(ld.Item.Code, ld.Location.Code, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO, startDate, endDate));

                    //计划外入库
                    ld.RCTUNP = this.SumTransQty(LocTransMgr.GetPeriodLocationTransaction(ld.Item.Code, ld.Location.Code, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_UNP, startDate, endDate));

                    //入库合计
                    ld.TotalInQty = ld.RCTPO + ld.RCTTR + ld.RCTWO + ld.RCTUNP;


                    //销售出库、退货、调整
                    ld.ISSSO = -1 * this.SumTransQty(LocTransMgr.GetPeriodLocationTransaction(ld.Item.Code, ld.Location.Code, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO, startDate, endDate));

                    //移库出库、退货、调整
                    ld.ISSTR = -1 * this.SumTransQty(LocTransMgr.GetPeriodLocationTransaction(ld.Item.Code, ld.Location.Code, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR, startDate, endDate));

                    //生产消耗、退货、调整
                    ld.ISSWO = -1 * this.SumTransQty(LocTransMgr.GetPeriodLocationTransaction(ld.Item.Code, ld.Location.Code, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO, startDate, endDate));

                    //计划外出库
                    ld.ISSUNP = -1 * this.SumTransQty(LocTransMgr.GetPeriodLocationTransaction(ld.Item.Code, ld.Location.Code, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP, startDate, endDate));

                    //出库合计
                    ld.TotalOutQty = ld.ISSSO + ld.ISSTR + ld.ISSWO + ld.ISSUNP;

                    //计划外出库
                    ld.CYCCNT = -1 * this.SumTransQty(LocTransMgr.GetPeriodLocationTransaction(ld.Item.Code, ld.Location.Code, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT, startDate, endDate));

                    //期初库存
                    ld.StartInvQty = ld.InvQty - ld.TotalInQty + ld.TotalOutQty - ld.CYCCNT;
                }
            }

            return locDetList;
        }
        #endregion

        [Transaction(TransactionMode.Unspecified)]
        public void PostProcessInvIOB(IList list, DateTime? startEffDate, DateTime? endEffDate)
        {
            if (list == null)
                throw new BusinessErrorException("Common.Business.Warn.DetailEmpty");

            DateTime startDate = startEffDate.HasValue ? startEffDate.Value : DateTime.MinValue;
            DateTime endDate = endEffDate.HasValue ? endEffDate.Value : DateTime.MaxValue;

            IList<LocationDetail> locationDetailList = IListHelper.ConvertToList<LocationDetail>(list);
            IList<string> itemList = locationDetailList.Select(l => l.Item.Code).Distinct().ToList<string>();
            IList<string> locList = locationDetailList.Select(l => l.Location.Code).Distinct().ToList<string>();
            IList<LocationTransaction> locationTransactionList = LocTransMgr.GetLocationTransaction(itemList, locList, startEffDate);

            foreach (var ld in locationDetailList)
            {
                decimal afterTransQty = (from l in locationTransactionList
                                         where l.Item == ld.Item.Code && l.Location == ld.Location.Code && l.EffectiveDate >= endDate
                                         select l.Qty).Sum();

                decimal allTransQty = (from l in locationTransactionList
                                       where l.Item == ld.Item.Code && l.Location == ld.Location.Code && l.EffectiveDate >= startDate
                                       select l.Qty).Sum();

                //期初库存
                ld.StartInvQty = ld.Qty - allTransQty;
                //期末库存
                ld.InvQty = ld.Qty - afterTransQty;

                //采购收货、退货、调整
                ld.RCTPO = (from l in locationTransactionList
                            where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                            && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                            && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PO)
                            select l.Qty).Sum();


                //移库入库、退货、调整 
                ld.RCTTR = (from l in locationTransactionList
                            where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                            && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                            && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_TR)
                            select l.Qty).Sum();


                //检验入库
                ld.RCTINP = (from l in locationTransactionList
                             where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                             && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                             && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP)
                             select l.Qty).Sum();


                //生产入库、退货、调整
                ld.RCTWO = (from l in locationTransactionList
                            where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                            && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                            && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO)
                            select l.Qty).Sum();


                //计划外入库CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_UNP
                ld.RCTUNP = (from l in locationTransactionList
                             where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                             && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                             && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_UNP)
                             select l.Qty).Sum();
                //入库合计
                ld.TotalInQty = ld.RCTPO + ld.RCTTR + ld.RCTINP + ld.RCTWO + ld.RCTUNP;




                //销售出库、退货、调整
                ld.ISSSO = -(from l in locationTransactionList
                             where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                             && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                             && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO)
                             select l.Qty).Sum();

                //移库出库、退货、调整
                ld.ISSTR = -(from l in locationTransactionList
                             where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                             && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                             && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR)
                             select l.Qty).Sum();


                //检验出库
                ld.ISSINP = -(from l in locationTransactionList
                              where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                              && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                              && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP)
                              select l.Qty).Sum();


                //生产消耗、退货、调整
                ld.ISSWO = -(from l in locationTransactionList
                             where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                             && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                             && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO)
                             select l.Qty).Sum();

                //计划外出库
                ld.ISSUNP = -(from l in locationTransactionList
                              where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                              && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                              && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP)
                              select l.Qty).Sum();
                //出库合计
                ld.TotalOutQty = ld.ISSSO + ld.ISSTR + ld.ISSINP + ld.ISSWO + ld.ISSUNP;

                //盘点
                ld.CYCCNT = (from l in locationTransactionList
                             where l.Item == ld.Item.Code && l.Location == ld.Location.Code
                             && l.EffectiveDate >= startDate && l.EffectiveDate < endDate
                             && l.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT)
                             select l.Qty).Sum();

                //未统计
                ld.NoStatsQty = ld.InvQty - ld.StartInvQty - ld.TotalInQty + ld.TotalOutQty - ld.CYCCNT;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationDetail> GetInvVisualBoard(string locCode, string itemCode, string flow, DateTime? date, User user)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowDetail));
            criteria.CreateAlias("Flow", "f");
            if (flow != null && flow.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Flow.Code", flow));
            if (itemCode != null && itemCode.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Item.Code", itemCode));
            if (locCode != null && locCode != string.Empty)
            {
                criteria.Add(Expression.Or(Expression.And(Expression.IsNull("LocationTo"), Expression.Eq("f.LocationTo.Code", locCode)),
                    Expression.Eq("LocationTo.Code", locCode)));
            }
            IList<FlowDetail> flowDetailList = CriteriaMgr.FindAll<FlowDetail>(criteria);

            //过滤权限
            if (locCode == null || locCode == string.Empty)
                flowDetailList = this.FilterFlowDetail(flowDetailList, user);

            IList<LocationDetail> ldList = new List<LocationDetail>();
            if (flowDetailList != null && flowDetailList.Count > 0)
            {
                foreach (FlowDetail fd in flowDetailList)
                {
                    LocationDetail ld = new LocationDetail();
                    if (fd.DefaultLocationTo == null)
                        continue;

                    ld = this.FindLocationDetail(fd.DefaultLocationTo, fd.Item, date, true);
                    if (ld != null)
                    {
                        ld.FlowDetail = fd;

                        //单位换算
                        if (!ld.FlowDetail.Item.Uom.Equals(ld.FlowDetail.Uom))
                        {
                            ld.InTransitQty = UomConversionMgr.ConvertUomQtyInvToOrder(ld.FlowDetail, ld.InTransitQty);
                            ld.QtyToBeIn = UomConversionMgr.ConvertUomQtyInvToOrder(ld.FlowDetail, ld.QtyToBeIn);
                            ld.QtyToBeOut = UomConversionMgr.ConvertUomQtyInvToOrder(ld.FlowDetail, ld.QtyToBeOut);
                            ld.InvQty = UomConversionMgr.ConvertUomQtyInvToOrder(ld.FlowDetail, ld.InvQty);
                            ld.PAB = UomConversionMgr.ConvertUomQtyInvToOrder(ld.FlowDetail, ld.PAB);
                        }

                        ldList.Add(ld);
                    }
                }
            }

            return ldList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public decimal GetDateInventory(string item, string loc, DateTime date)
        {
            decimal currentInv = this.GetCurrentInv(loc, item);
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationTransaction));

            #region Customize
            criteria.Add(Expression.Gt("EffectiveDate", date));//选定日期期末库存
            #endregion

            #region Select Parameters
            if (loc != null && loc.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Location", loc));
            if (item != null && item.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Item", item));
            #endregion

            criteria.SetProjection(Projections.Sum("Qty"));

            IList result = CriteriaMgr.FindAll(criteria);
            decimal transQty = result[0] != null ? (decimal)result[0] : 0;

            return currentInv - transQty;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void PostProcessInvDetail(IList list)
        {
            if (list == null)
                throw new BusinessErrorException("Common.Business.Warn.DetailEmpty");

            IList<LocationDetail> locationDetailList = IListHelper.ConvertToList<LocationDetail>(list);
            IList<string> itemList = locationDetailList.Select(l => l.Item.Code).Distinct().ToList<string>();
            string[] statusArray = new string[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS };

            IList<string> locList = locationDetailList.Select(l => l.Location.Code).Distinct().ToList<string>();
            IList<OrderLocationTransaction> orderLocationTransactionList = OrderLocTransMgr.GetOpenOrderLocationTransaction(itemList, locList);
            IList<InProcessLocationDetail> ipDetOut = this.IPLocDetMgr.GetInProcessLocationDetailOut(itemList, locList);
            IList<InProcessLocationDetail> ipDetIn = this.IPLocDetMgr.GetInProcessLocationDetailIn(itemList, locList);
            IList<PickListResult> pickListResults = this.PickListResultMgr.GetPickListResult(locList.ToArray(), itemList.ToArray(), null, null, statusArray, true);

            foreach (var ld in locationDetailList)
            {
                //订单待收
                ld.QtyToBeIn = (from o in orderLocationTransactionList
                                where o.Item.Equals(ld.Item) && o.Location.Equals(ld.Location) && o.IOType == BusinessConstants.IO_TYPE_IN
                                select o.RemainQty).Sum();

                //订单待发
                ld.QtyToBeOut = (from o in orderLocationTransactionList
                                 where o.Item.Equals(ld.Item) && o.Location.Equals(ld.Location) && o.IOType == BusinessConstants.IO_TYPE_OUT
                                 select o.RemainQty).Sum();

                //出在途
                ld.InTransitQtyOut = (from i in ipDetOut
                                      where StringHelper.Eq(ld.Item.Code, i.ItemCode) && StringHelper.Eq(ld.Location.Code, i.LocationCode)
                                      select i.Qty).Sum();

                //入在途
                ld.InTransitQty = (from i in ipDetIn
                                   where StringHelper.Eq(ld.Item.Code, i.ItemCode)
                                   select i.Qty).Sum();

                //已拣

                ld.PickedQty = (from pickListResult in pickListResults
                                where StringHelper.Eq(ld.Item.Code, pickListResult.ItemCode)
                                && StringHelper.Eq(ld.Location.Code, pickListResult.LocationCode)
                                select pickListResult.Qty).Sum();

                //可用库存
                ld.PAB = ld.Qty + ld.QtyToBeIn - ld.QtyToBeOut;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void PostProcessInvHistory(IList list, DateTime? date)
        {
            if (list == null)
                throw new BusinessErrorException("Common.Business.Warn.DetailEmpty");

            IList<LocationDetail> locationDetailList = IListHelper.ConvertToList<LocationDetail>(list);
            IList<string> itemList = locationDetailList.Select(l => l.Item.Code).Distinct().ToList<string>();
            IList<string> locList = locationDetailList.Select(l => l.Location.Code).Distinct().ToList<string>();
            IList<LocationTransaction> locationTransactionList = LocTransMgr.GetProjectionLocationTransaction(itemList, locList, date);

            foreach (var ld in locationDetailList)
            {
                decimal transQty = (from l in locationTransactionList
                                    where StringHelper.Eq(l.Item, ld.Item.Code) && StringHelper.Eq(l.Location, ld.Location.Code)
                                    select l.Qty).Sum();

                //历史库存
                ld.InvQty = ld.Qty - transQty;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void PostProcessInvVisualBoard(IList list, DateTime? date)
        {
            if (list == null)
                throw new BusinessErrorException("Common.Business.Warn.DetailEmpty");

            IList<FlowView> flowViewList = IListHelper.ConvertToList<FlowView>(list);
            IList<string> itemList = flowViewList.Select(f => f.FlowDetail.Item.Code).Distinct().ToList<string>();
            IList<string> locList = flowViewList.Where(f => f.LocationTo != null).Select(f => f.LocationTo.Code).Distinct().ToList<string>();
            IList<LocationDetail> locationDetailList = this.GetLocationDetail(itemList, locList);
            IList<OrderLocationTransaction> orderLocationTransactionList = OrderLocTransMgr.GetOpenOrderLocationTransaction(itemList, locList);
            IList<InProcessLocationDetail> ipDetOut = this.IPLocDetMgr.GetInProcessLocationDetailOut(itemList, locList);
            IList<InProcessLocationDetail> ipDetIn = this.IPLocDetMgr.GetInProcessLocationDetailIn(itemList, locList);

            IList<LocationDetail> result = new List<LocationDetail>();
            if (list != null && list.Count > 0)
            {
                foreach (var flowView in flowViewList)
                {
                    flowView.LocationDetail = locationDetailList.Where(l => l.Item.Equals(flowView.FlowDetail.Item)
                        && l.Location.Equals(flowView.LocationTo)).SingleOrDefault();

                    if (flowView.LocationDetail == null)
                    {
                        flowView.LocationDetail = new LocationDetail();
                        flowView.LocationDetail.Location = flowView.LocationTo;
                        flowView.LocationDetail.Item = flowView.FlowDetail.Item;
                    }
                    LocationDetail locationDetail = flowView.LocationDetail;

                    //订单待收
                    locationDetail.QtyToBeIn = (from o in orderLocationTransactionList
                                                where o.Item.Equals(locationDetail.Item) && o.Location.Equals(locationDetail.Location)
                                                && o.IOType == BusinessConstants.IO_TYPE_IN
                                                //&& (!date.HasValue || o.OrderDetail.OrderHead.WindowTime >= date.Value)
                                                select o.RemainQty).Sum();

                    //订单待发
                    locationDetail.QtyToBeOut = (from o in orderLocationTransactionList
                                                 where o.Item.Equals(locationDetail.Item) && o.Location.Equals(locationDetail.Location)
                                                 && o.IOType == BusinessConstants.IO_TYPE_OUT
                                                 //&& (!date.HasValue || o.OrderDetail.OrderHead.StartTime > date.Value)
                                                 select o.RemainQty).Sum();

                    //入在途
                    locationDetail.InTransitQty = (from i in ipDetIn
                                                   where locationDetail.Item.Code.Equals(i.ItemCode)
                                                   //&& (!date.HasValue || i.OrderLocationTransaction.OrderDetail.OrderHead.WindowTime >= date.Value)
                                                   select i.Qty).Sum();

                    //可用库存
                    if (StringHelper.Eq(flowView.Flow.FlowStrategy, BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_KB))
                        locationDetail.PAB = locationDetail.Qty + locationDetail.QtyToBeIn;
                    else
                        locationDetail.PAB = locationDetail.Qty + locationDetail.QtyToBeIn - locationDetail.QtyToBeOut;

                    //单位换算
                    if (!flowView.FlowDetail.Item.Uom.Equals(flowView.FlowDetail.Uom))
                    {
                        locationDetail.Qty = UomConversionMgr.ConvertUomQtyInvToOrder(flowView.FlowDetail, locationDetail.InvQty);
                        locationDetail.QtyToBeIn = UomConversionMgr.ConvertUomQtyInvToOrder(flowView.FlowDetail, locationDetail.QtyToBeIn);
                        locationDetail.InTransitQty = UomConversionMgr.ConvertUomQtyInvToOrder(flowView.FlowDetail, locationDetail.InTransitQty);
                        locationDetail.QtyToBeOut = UomConversionMgr.ConvertUomQtyInvToOrder(flowView.FlowDetail, locationDetail.QtyToBeOut);
                        locationDetail.PAB = UomConversionMgr.ConvertUomQtyInvToOrder(flowView.FlowDetail, locationDetail.PAB);
                    }
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationDetail> GetLocationDetail(IList<string> itemList, IList<string> locList)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationDetail));
            if (itemList.Count == 1)
            {
                criteria.Add(Expression.Eq("Item.Code", itemList[0]));
            }
            else
            {
                criteria.Add(Expression.InG<string>("Item.Code", itemList));
            }
            if (locList.Count == 1)
            {
                criteria.Add(Expression.Eq("Location.Code", locList[0]));
            }
            else
            {
                criteria.Add(Expression.InG<string>("Location.Code", locList));
            }
            return CriteriaMgr.FindAll<LocationDetail>(criteria);
        }

        #endregion Customized Methods


        #region Private Methods

        private decimal SumTransQty(IList<LocationTransaction> locTransList)
        {
            decimal totalQty = 0;
            if (locTransList != null && locTransList.Count > 0)
            {
                foreach (LocationTransaction locTrans in locTransList)
                {
                    totalQty += locTrans.Qty;
                }
            }

            return totalQty;
        }

        private IList<FlowDetail> FilterFlowDetail(IList<FlowDetail> flowDetailList, User user)
        {
            IList<Location> locList = LocationMgr.GetLocationByUserCode(user.Code);
            if (locList == null || locList.Count == 0)
                return null;

            IList<FlowDetail> fdList = new List<FlowDetail>();
            if (flowDetailList != null && flowDetailList.Count > 0)
            {
                foreach (FlowDetail fd in flowDetailList)
                {
                    bool isExist = false;
                    foreach (Location loc in locList)
                    {
                        if (fd.DefaultLocationTo != null && fd.DefaultLocationTo.Code == loc.Code)
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (isExist)
                    {
                        fdList.Add(fd);
                    }
                }
            }

            return fdList;
        }

        #endregion
    }
}