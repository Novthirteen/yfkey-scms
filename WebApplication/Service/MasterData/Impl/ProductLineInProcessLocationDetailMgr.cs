using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.Production;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Distribution;
using System.Linq;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ProductLineInProcessLocationDetailMgr : ProductLineInProcessLocationDetailBaseMgr, IProductLineInProcessLocationDetailMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IFlowMgr flowMgr;
        private ILocationMgr locationMgr;
        private ILocationTransactionMgr locationTransactionMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IOrderPlannedBackflushMgr orderPlannedBackflushMgr;
        private IInProcessLocationDetailMgr inProcessLocationDetailMgr;
        private IUserMgr userMgr;
        private IInProcessLocationMgr inProcessLocationMgr;

        public ProductLineInProcessLocationDetailMgr(IProductLineInProcessLocationDetailDao entityDao,
            ICriteriaMgr criteriaMgr,
            IFlowMgr flowMgr,
            ILocationMgr locationMgr,
            ILocationTransactionMgr locationTransactionMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IEntityPreferenceMgr entityPreferenceMgr,
            IOrderPlannedBackflushMgr orderPlannedBackflushMgr,
            IInProcessLocationDetailMgr inProcessLocationDetailMgr,
            IUserMgr userMgr,
            IInProcessLocationMgr inProcessLocationMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.flowMgr = flowMgr;
            this.locationMgr = locationMgr;
            this.locationTransactionMgr = locationTransactionMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.orderPlannedBackflushMgr = orderPlannedBackflushMgr;
            this.inProcessLocationDetailMgr = inProcessLocationDetailMgr;
            this.userMgr = userMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public IList<ProductLineInProcessLocationDetail> GetProductLineInProcessLocationDetail(string prodLineCode, string status)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ProductLineInProcessLocationDetail>();

            criteria.Add(Expression.Eq("ProductLine.Code", prodLineCode));
            criteria.Add(Expression.Eq("Status", status));

            criteria.AddOrder(Order.Asc("Id"));

            return this.criteriaMgr.FindAll<ProductLineInProcessLocationDetail>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ProductLineInProcessLocationDetail> GetProductLineInProcessLocationDetail(string prodLineCode, string status, string[] items)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ProductLineInProcessLocationDetail>();

            criteria.Add(Expression.Eq("ProductLine.Code", prodLineCode));
            criteria.Add(Expression.Eq("Status", status));

            if (items != null && items.Length > 0)
            {
                criteria.CreateAlias("Item", "item");
                criteria.Add(Expression.In("item.Code", items));
            }

            criteria.AddOrder(Order.Asc("Id"));

            return this.criteriaMgr.FindAll<ProductLineInProcessLocationDetail>(criteria);
        }

        public IList<ProductLineInProcessLocationDetail> GetProductLineInProcessLocationDetailGroupByItem(string prodLineCode, string status)
        {
            IList<ProductLineInProcessLocationDetail> plIpGroupList = new List<ProductLineInProcessLocationDetail>();
            IList<ProductLineInProcessLocationDetail> plIpList = GetProductLineInProcessLocationDetail(prodLineCode, status);
            foreach (ProductLineInProcessLocationDetail plIpDetail in plIpList)
            {
                bool isExist = false;
                foreach (ProductLineInProcessLocationDetail plIpGroupDetail in plIpGroupList)
                {
                    if (plIpGroupDetail.Item.Code.Trim() == plIpDetail.Item.Code.Trim())
                    {
                        isExist = true;
                        plIpGroupDetail.Qty += plIpDetail.Qty;
                        plIpGroupDetail.BackflushQty += plIpGroupDetail.BackflushQty;
                        break;
                    }
                }
                if (!isExist)
                {
                    ProductLineInProcessLocationDetail newPlIpDetail = new ProductLineInProcessLocationDetail();
                    newPlIpDetail.Item = plIpDetail.Item;
                    newPlIpDetail.Qty = plIpDetail.Qty;
                    newPlIpDetail.BackflushQty = plIpDetail.BackflushQty;
                    plIpGroupList.Add(newPlIpDetail);
                }

            }

            return plIpGroupList;
        }

        [Transaction(TransactionMode.Requires)]
        public void RawMaterialIn(string prodLineCode, IList<MaterialIn> materialInList, User user)
        {
            Flow flow = this.flowMgr.CheckAndLoadFlow(prodLineCode);
            IList<BomDetail> bomDetailList = this.flowMgr.GetBatchFeedBomDetail(flow);

            IList<MaterialIn> noneZeroMaterialInList = new List<MaterialIn>();
            DateTime dateTimeNow = DateTime.Now;

            if (materialInList != null && materialInList.Count > 0)
            {
                foreach (MaterialIn materialIn in materialInList)
                {
                    if (materialIn.Qty != 0)
                    {
                        noneZeroMaterialInList.Add(materialIn);
                    }

                    #region 查找物料是否是生产线上投料的
                    if (bomDetailList != null && bomDetailList.Count > 0)
                    {
                        bool findMatch = false;
                        foreach (BomDetail bomDetail in bomDetailList)
                        {
                            if (bomDetail.Item.Code == materialIn.RawMaterial.Code)
                            {
                                findMatch = true;
                                break;
                            }
                        }

                        if (!findMatch)
                        {
                            throw new BusinessErrorException("MasterData.Production.Feed.Error.NotContainMaterial", materialIn.RawMaterial.Code, prodLineCode);
                        }
                    }
                    else
                    {
                        throw new BusinessErrorException("MasterData.Production.Feed.Error.NoFeedMaterial", prodLineCode);
                    }
                    #endregion
                }
            }

            if (noneZeroMaterialInList.Count == 0)
            {
                throw new BusinessErrorException("Order.Error.ProductLineInProcessLocationDetailEmpty");
            }

            foreach (MaterialIn materialIn in noneZeroMaterialInList)
            {
                #region 出库
                IList<InventoryTransaction> inventoryTransactionList = this.locationMgr.InventoryOut(materialIn, user, flow);
                #endregion

                #region 入生产线物料
                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                {
                    ProductLineInProcessLocationDetail productLineInProcessLocationDetail = new ProductLineInProcessLocationDetail();
                    productLineInProcessLocationDetail.ProductLine = flow;
                    productLineInProcessLocationDetail.Operation = materialIn.Operation;
                    productLineInProcessLocationDetail.Item = inventoryTransaction.Item;
                    productLineInProcessLocationDetail.HuId = inventoryTransaction.Hu != null ? inventoryTransaction.Hu.HuId : null;
                    productLineInProcessLocationDetail.LotNo = inventoryTransaction.Hu != null ? inventoryTransaction.Hu.LotNo : null;
                    productLineInProcessLocationDetail.Qty = 0 - inventoryTransaction.Qty;
                    productLineInProcessLocationDetail.IsConsignment = inventoryTransaction.IsConsignment;
                    productLineInProcessLocationDetail.PlannedBill = inventoryTransaction.PlannedBill;
                    productLineInProcessLocationDetail.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                    productLineInProcessLocationDetail.LocationFrom = inventoryTransaction.Location;
                    productLineInProcessLocationDetail.CreateDate = dateTimeNow;
                    productLineInProcessLocationDetail.CreateUser = user;
                    productLineInProcessLocationDetail.LastModifyDate = dateTimeNow;
                    productLineInProcessLocationDetail.LastModifyUser = user;

                    this.CreateProductLineInProcessLocationDetail(productLineInProcessLocationDetail);

                    //记录库存事务
                    this.locationTransactionMgr.RecordLocationTransaction(productLineInProcessLocationDetail, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_MATERIAL_IN, user, BusinessConstants.IO_TYPE_IN);
                }
                #endregion
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void RawMaterialIn(Flow prodLine, IList<MaterialIn> materialInList, User user)
        {
            this.RawMaterialIn(prodLine.Code, materialInList, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void RawMaterialBackflush(string prodLineCode, User user)
        {
            this.RawMaterialBackflush(prodLineCode, null, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void RawMaterialBackflush(string prodLineCode, IDictionary<string, decimal> itemQtydic, User user)
        {

            if (itemQtydic == null || itemQtydic.Count == 0)
            {
                throw new BusinessErrorException("MasterData.Production.Feed.Error.NoSelectFeed");
            }

            Flow flow = this.flowMgr.CheckAndLoadFlow(prodLineCode);
            DateTime dateTimeNow = DateTime.Now;

            IList<ProductLineInProcessLocationDetail> productLineInProcessLocationDetailList =
                this.GetProductLineInProcessLocationDetail(prodLineCode, BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, itemQtydic.Keys.ToArray<string>());

            IList<ProductLineInProcessLocationDetail> targetProductLineInProcessLocationDetailList = new List<ProductLineInProcessLocationDetail>();

            #region 根据剩余数量计算回冲零件数量，添加到待处理列表
            if (itemQtydic != null && itemQtydic.Count > 0)
            {
                foreach (string itemCode in itemQtydic.Keys)
                {
                    decimal remainQty = itemQtydic[itemCode];   //剩余投料量
                    decimal inQty = 0;                     //总投料量
                    IList<ProductLineInProcessLocationDetail> currentProductLineInProcessLocationDetailList = new List<ProductLineInProcessLocationDetail>();
                    foreach (ProductLineInProcessLocationDetail productLineInProcessLocationDetail in productLineInProcessLocationDetailList)
                    {
                        if (productLineInProcessLocationDetail.Item.Code == itemCode)
                        {
                            inQty += (productLineInProcessLocationDetail.Qty - productLineInProcessLocationDetail.BackflushQty);
                            currentProductLineInProcessLocationDetailList.Add(productLineInProcessLocationDetail);
                        }
                    }

                    if (remainQty > inQty)
                    {
                        throw new BusinessErrorException("MasterData.Production.Feed.Error.RemainQtyGtFeedQty", itemCode);
                    }

                    decimal backflushQty = inQty - remainQty;  //本次回冲量

                    #region 设定本次回冲数量
                    if (backflushQty > 0)
                    {
                        foreach (ProductLineInProcessLocationDetail productLineInProcessLocationDetail in currentProductLineInProcessLocationDetailList)
                        {
                            if (backflushQty - (productLineInProcessLocationDetail.Qty - productLineInProcessLocationDetail.BackflushQty) > 0)
                            {
                                productLineInProcessLocationDetail.CurrentBackflushQty = productLineInProcessLocationDetail.Qty - productLineInProcessLocationDetail.BackflushQty;
                                backflushQty -= productLineInProcessLocationDetail.Qty - productLineInProcessLocationDetail.BackflushQty;
                                productLineInProcessLocationDetail.BackflushQty = productLineInProcessLocationDetail.Qty;
                                targetProductLineInProcessLocationDetailList.Add(productLineInProcessLocationDetail);
                            }
                            else
                            {
                                productLineInProcessLocationDetail.CurrentBackflushQty = backflushQty;
                                productLineInProcessLocationDetail.BackflushQty += backflushQty;
                                backflushQty = 0;
                                targetProductLineInProcessLocationDetailList.Add(productLineInProcessLocationDetail);
                                break;
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion


            if (targetProductLineInProcessLocationDetailList != null && targetProductLineInProcessLocationDetailList.Count > 0)
            {
                #region 更新生产线上的物料
                foreach (ProductLineInProcessLocationDetail productLineInProcessLocationDetail in targetProductLineInProcessLocationDetailList)
                {
                    if (productLineInProcessLocationDetail.Qty == productLineInProcessLocationDetail.BackflushQty)
                    {
                        productLineInProcessLocationDetail.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                    }
                    productLineInProcessLocationDetail.LastModifyDate = dateTimeNow;
                    productLineInProcessLocationDetail.LastModifyUser = user;

                    this.UpdateProductLineInProcessLocationDetail(productLineInProcessLocationDetail);

                    //记录库存事务
                    //this.locationTransactionMgrE.RecordLocationTransaction(productLineInProcessLocationDetail, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR, user, BusinessConstants.IO_TYPE_OUT);
                    //this.locationTransactionMgrE.RecordLocationTransaction(productLineInProcessLocationDetail, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_TR, user, BusinessConstants.IO_TYPE_OUT);
                }
                #endregion
                IList<object[]> orderPlannedBackflushList = this.orderPlannedBackflushMgr.GetActiveOrderPlannedBackflush(prodLineCode);

                if (orderPlannedBackflushList == null || orderPlannedBackflushList.Count == 0)
                {
                    throw new BusinessErrorException("MasterData.Production.Feed.Error.NoWO", prodLineCode);
                }

                var productLineInProcessLocationDetailDic = from plIp in targetProductLineInProcessLocationDetailList
                                                            group plIp by new
                                                            {
                                                                Item = plIp.Item.Code,
                                                                Operation = plIp.Operation,
                                                                HuId = plIp.HuId,
                                                                LotNo = plIp.LotNo,
                                                                LocationFrom = plIp.LocationFrom,
                                                                IsConsignment = plIp.IsConsignment,
                                                                PlannedBill = plIp.PlannedBill
                                                            } into result
                                                            select new
                                                            {
                                                                Item = result.Key.Item,
                                                                Operation = result.Key.Operation,
                                                                HuId = result.Key.HuId,
                                                                LotNo = result.Key.LotNo,
                                                                LocationFrom = result.Key.LocationFrom,
                                                                IsConsignment = result.Key.IsConsignment,
                                                                PlannedBill = result.Key.PlannedBill,
                                                                BackflushQty = result.Sum(plIp => plIp.CurrentBackflushQty)
                                                            };

                foreach (var productLineInProcessLocationDetail in productLineInProcessLocationDetailDic)
                {
                   
                    var planList = orderPlannedBackflushList.Where(p => (string)p[3] == productLineInProcessLocationDetail.Item
                       && (!productLineInProcessLocationDetail.Operation.HasValue || productLineInProcessLocationDetail.Operation == (int?)p[1])).ToList();

                    var totalBaseQty = planList.Sum(p => (decimal)p[2]); //回冲分配基数

                    if (planList.Count > 0)
                    {
                        EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_AMOUNT_DECIMAL_LENGTH);
                        int amountDecimalLength = int.Parse(entityPreference.Value);

                        decimal remainTobeBackflushQty = productLineInProcessLocationDetail.BackflushQty;  //剩余待回冲数量
                        decimal unitQty = Math.Round(remainTobeBackflushQty / totalBaseQty, amountDecimalLength);  //单位基数的回冲数量

                        for (int i = 0; i < planList.Count; i++)
                        {
                            #region 物料回冲
                            #region 更新匹配的OrderLocationTransaction
                            OrderPlannedBackflush matchedOrderPlannedBackflush = orderPlannedBackflushMgr.LoadOrderPlannedBackflush((int)planList[i][0]);
                            OrderLocationTransaction matchedOrderLocationTransaction = matchedOrderPlannedBackflush.OrderLocationTransaction;
                            //InProcessLocation ip = inProcessLocationMgr.LoadInProcessLocation(matchedOrderPlannedBackflush.InProcessLocation);

                            bool isLastestRecord = (i == (planList.Count - 1));
                            decimal currentTotalBackflushQty = 0;

                            if (!matchedOrderLocationTransaction.AccumulateQty.HasValue)
                            {
                                matchedOrderLocationTransaction.AccumulateQty = 0;
                            }

                            if (!isLastestRecord)
                            {
                                decimal currentBackflushQty = matchedOrderPlannedBackflush.PlannedQty * unitQty;
                                currentTotalBackflushQty += currentBackflushQty;
                                matchedOrderLocationTransaction.AccumulateQty += currentBackflushQty;
                                remainTobeBackflushQty -= currentBackflushQty;
                            }
                            else
                            {
                                currentTotalBackflushQty += remainTobeBackflushQty;
                                matchedOrderLocationTransaction.AccumulateQty += remainTobeBackflushQty;
                                remainTobeBackflushQty = 0;
                            }

                            this.orderLocationTransactionMgr.UpdateOrderLocationTransaction(matchedOrderLocationTransaction);
                            #endregion

                            #region 新增/更新AsnDetail,暂时不更新了
                            //InProcessLocationDetail inProcessLocationDetail = null;
                         
                            //if (productLineInProcessLocationDetail.HuId == null || productLineInProcessLocationDetail.HuId.Trim() == string.Empty)
                            //{
                            //    inProcessLocationDetail = this.inProcessLocationDetailMgr.GetNoneHuAndIsConsignmentInProcessLocationDetail(ip, matchedOrderPlannedBackflush.OrderLocationTransaction);
                            //    if (inProcessLocationDetail != null)
                            //    {
                            //        inProcessLocationDetail.Qty += currentTotalBackflushQty;

                            //        this.inProcessLocationDetailMgr.UpdateInProcessLocationDetail(inProcessLocationDetail);
                            //    }
                            //}

                            //if (inProcessLocationDetail == null)
                            //{
                            //    inProcessLocationDetail = new InProcessLocationDetail();
                            //    inProcessLocationDetail.InProcessLocation = ip;
                            //    inProcessLocationDetail.OrderLocationTransaction = matchedOrderPlannedBackflush.OrderLocationTransaction;
                            //    inProcessLocationDetail.HuId = productLineInProcessLocationDetail.HuId;
                            //    inProcessLocationDetail.LotNo = productLineInProcessLocationDetail.LotNo;
                            //    inProcessLocationDetail.IsConsignment = productLineInProcessLocationDetail.IsConsignment;
                            //    inProcessLocationDetail.PlannedBill = productLineInProcessLocationDetail.PlannedBill;
                            //    inProcessLocationDetail.Qty = currentTotalBackflushQty;

                            //    this.inProcessLocationDetailMgr.CreateInProcessLocationDetail(inProcessLocationDetail);

                            //    ip.AddInProcessLocationDetail(inProcessLocationDetail);
                            //}

                            #endregion

                            #region 新增库存事务
                            this.locationTransactionMgr.RecordWOBackflushLocationTransaction(
                                matchedOrderPlannedBackflush.OrderLocationTransaction, productLineInProcessLocationDetail.HuId,
                                productLineInProcessLocationDetail.LotNo, currentTotalBackflushQty,
                                matchedOrderPlannedBackflush.InProcessLocation, user, productLineInProcessLocationDetail.LocationFrom);
                            #endregion
                            #endregion

                            #region 关闭OrderPlannedBackflush
                            if (matchedOrderPlannedBackflush.IsActive)
                            {
                                matchedOrderPlannedBackflush.IsActive = false;
                                this.orderPlannedBackflushMgr.UpdateOrderPlannedBackflush(matchedOrderPlannedBackflush);
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        #region 没有匹配的OrderLocationTransaction
                        //退回原库位
                        throw new BusinessErrorException("MasterData.BackFlush.NotFoundResources", productLineInProcessLocationDetail.Item);
                        //this.locationMgrE.InventoryIn(productLineInProcessLocationDetail, user);
                        #endregion
                    }
                }
            }
            else
            {
                throw new BusinessErrorException("MasterData.Production.Feed.Error.NoFeed", prodLineCode);
            }
        }


        [Transaction(TransactionMode.Requires)]
        public void RawMaterialBackflush(Flow prodLine, User user)
        {
            this.RawMaterialBackflush(prodLine.Code, null, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void RawMaterialBackflush(Flow prodLine, IDictionary<string, decimal> itemQtydic, User user)
        {
            this.RawMaterialBackflush(prodLine.Code, itemQtydic, user);
        }       

        #endregion Customized Methods

        #region Private Methods

        #endregion
    }
}