using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Production;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Entity.Distribution;
using com.Sconit.Utility;
using com.Sconit.Entity;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Production.Impl
{
    public class MaterialFlushBackMgr : IMaterialFlushBackMgr
    {
        #region 构造函数
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IHuMgr huMgr;
        private IGenericMgr genericMgr;
        public MaterialFlushBackMgr(
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IEntityPreferenceMgr entityPreferenceMgr,
            IHuMgr huMgr,
            IGenericMgr genericMgr)
        {
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.huMgr = huMgr;
            this.genericMgr = genericMgr;
        }
        #endregion

        //[Transaction(TransactionMode.Unspecified)]
        //public IList<MaterialFlushBack> FindMatchMaterialFlushBack(OrderLocationTransaction orderLocationTransaction, string ipNo)
        //{
        //    OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
        //    IList<MaterialFlushBack> materialFlushBackList = new List<MaterialFlushBack>();

        //    IList<InProcessLocationDetail> inProcessLocationDetailList = this.inProcessLocationDetailMgr.GetInProcessLocationDetail(ipNo);
        //    foreach (InProcessLocationDetail inProcessLocationDetail in inProcessLocationDetailList)
        //    {
        //        if (inProcessLocationDetail.OrderLocationTransaction.Id == orderLocationTransaction.Id)
        //        {
        //            MaterialFlushBack materialFlushBack = new MaterialFlushBack();
        //            materialFlushBack.OrderDetail = orderDetail;
        //            materialFlushBack.Operation = orderLocationTransaction.Operation;
        //            materialFlushBack.RawMaterial = orderLocationTransaction.Item;
        //            materialFlushBack.Uom = orderLocationTransaction.Uom;
        //            materialFlushBack.InProcessLocationDetail = inProcessLocationDetail;
        //            materialFlushBack.HuId = inProcessLocationDetail.HuId;
        //            materialFlushBack.LotNo = inProcessLocationDetail.LotNo;
        //            if (orderDetail.OrderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        //            {
        //                materialFlushBack.Qty = orderDetail.CurrentReceiveQty * orderLocationTransaction.UnitQty;
        //            }
        //            else
        //            {
        //                //生产，废品和次品都消耗原材料
        //                materialFlushBack.Qty =
        //                    (orderDetail.CurrentReceiveQty + orderDetail.CurrentRejectQty + orderDetail.CurrentScrapQty)
        //                     * orderLocationTransaction.UnitQty;
        //            }

        //            materialFlushBackList.Add(materialFlushBack);
        //        }
        //    }

        //    return materialFlushBackList;
        //}

        //[Transaction(TransactionMode.Unspecified)]
        //public IList<MaterialFlushBack> FindMatchMaterialFlushBack(OrderLocationTransaction orderLocationTransaction, IList<string> ipNoList)
        //{
        //    IList<MaterialFlushBack> materialFlushBackList = new List<MaterialFlushBack>();

        //    foreach (string ipNo in ipNoList)
        //    {
        //        IList<MaterialFlushBack> result = FindMatchMaterialFlushBack(orderLocationTransaction, ipNo);
        //        IListHelper.AddRange(materialFlushBackList, result);
        //    }

        //    return materialFlushBackList;
        //}

        //[Transaction(TransactionMode.Unspecified)]
        //public IList<MaterialFlushBack> FindMatchMaterialFlushBack(OrderLocationTransaction orderLocationTransaction, IList<MaterialFlushBack> materialFlushBackList)
        //{
        //    IList<MaterialFlushBack> returnMaterialFlushBackList = new List<MaterialFlushBack>();
        //    foreach (MaterialFlushBack materialFlushBack in materialFlushBackList)
        //    {
        //        if (orderLocationTransaction.OrderDetail.Id == materialFlushBack.OrderDetail.Id
        //            && orderLocationTransaction.Item.Code == materialFlushBack.RawMaterial.Code
        //            && orderLocationTransaction.Operation == materialFlushBack.Operation)
        //        {
        //            returnMaterialFlushBackList.Add(materialFlushBack);
        //        }
        //    }
        //    return returnMaterialFlushBackList;
        //}

        [Transaction(TransactionMode.Unspecified)]
        public IList<MaterialFlushBack> AssignMaterialFlushBack(MaterialFlushBack sourceMaterialFlushBack, IList<OrderDetail> orderDetailList)
        {
            if (orderDetailList != null && orderDetailList.Count > 0)
            {
                IList<OrderLocationTransaction> targetInOrderLocationTransactionList = new List<OrderLocationTransaction>();

                foreach (OrderDetail orderDetail in orderDetailList)
                {
                    IList<OrderLocationTransaction> inOrderLocationTransactionList = this.orderLocationTransactionMgr.GetOrderLocationTransaction(orderDetail.Id, BusinessConstants.IO_TYPE_IN);

                    //入库(in)的OrderLocationTransaction只可能有一个
                    inOrderLocationTransactionList[0].CurrentReceiveQty = orderDetail.CurrentReceiveQty;
                    inOrderLocationTransactionList[0].CurrentRejectQty = orderDetail.CurrentRejectQty;
                    inOrderLocationTransactionList[0].CurrentScrapQty = orderDetail.CurrentScrapQty;

                    targetInOrderLocationTransactionList.Add(inOrderLocationTransactionList[0]);
                }

                return AssignMaterialFlushBack(sourceMaterialFlushBack, targetInOrderLocationTransactionList);
            }

            return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<MaterialFlushBack> AssignMaterialFlushBack(MaterialFlushBack sourceMaterialFlushBack, IList<OrderLocationTransaction> inOrderLocationTransactionList)
        {
            Hu hu = null;
            if (sourceMaterialFlushBack.HuId != null && sourceMaterialFlushBack.HuId.Trim() != string.Empty)
            {
                hu = this.huMgr.CheckAndLoadHu(sourceMaterialFlushBack.HuId);
            }

            IList<MaterialFlushBack> materialFlushBackList = new List<MaterialFlushBack>();
            decimal theoryTotalQty = 0;  //理论消耗量
            if (inOrderLocationTransactionList != null && inOrderLocationTransactionList.Count > 0)
            {
                foreach (OrderLocationTransaction inOrderLocationTransaction in inOrderLocationTransactionList)
                {
                    IList<OrderLocationTransaction> orderLocationTransactionList = this.orderLocationTransactionMgr.GetOrderLocationTransaction(inOrderLocationTransaction.OrderDetail.Id, BusinessConstants.IO_TYPE_OUT);
                    if (orderLocationTransactionList != null && orderLocationTransactionList.Count > 0)
                    {
                        foreach (OrderLocationTransaction orderLocationTransaction in orderLocationTransactionList)
                        {
                            if (orderLocationTransaction.Item.Code == sourceMaterialFlushBack.RawMaterial.Code)
                            {
                                if (sourceMaterialFlushBack.Operation == 0
                                    || orderLocationTransaction.Operation == sourceMaterialFlushBack.Operation)
                                {
                                    if ((hu == null)   //按数量分配
                                        || (hu != null && (hu.Version == null || hu.Version.Trim() == string.Empty)) //按条码分配
                                        || (hu != null && hu.Version != null && hu.Version.Trim() != string.Empty && hu.Version == orderLocationTransaction.ItemVersion))  //按条码和工程状态分配
                                    {
                                        MaterialFlushBack materialFlushBack = new MaterialFlushBack();
                                        materialFlushBack.OrderDetail = inOrderLocationTransaction.OrderDetail;
                                        materialFlushBack.OrderLocationTransaction = orderLocationTransaction;
                                        materialFlushBack.Operation = orderLocationTransaction.Operation;
                                        materialFlushBack.RawMaterial = orderLocationTransaction.Item;
                                        materialFlushBack.Uom = orderLocationTransaction.OrderDetail.Uom;
                                        materialFlushBack.HuId = sourceMaterialFlushBack.HuId;
                                        if (inOrderLocationTransaction.OrderDetail.OrderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                                        {
                                            materialFlushBack.Qty = inOrderLocationTransaction.CurrentReceiveQty * orderLocationTransaction.UnitQty;
                                        }
                                        else
                                        {
                                            //生产，废品和次品都消耗原材料
                                            materialFlushBack.Qty =
                                                (inOrderLocationTransaction.CurrentReceiveQty + inOrderLocationTransaction.CurrentRejectQty + inOrderLocationTransaction.CurrentScrapQty)
                                                    * orderLocationTransaction.UnitQty;
                                        }
                                        theoryTotalQty += materialFlushBack.Qty;

                                        materialFlushBackList.Add(materialFlushBack);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int FlushBackListCount = materialFlushBackList.Count;

            if (FlushBackListCount == 0)
            {
                throw new BusinessErrorException("Order.Error.ReceiveOrder.AssignMaterial", sourceMaterialFlushBack.RawMaterial.Code);
            }

            EntityPreference entityPreference = entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_AMOUNT_DECIMAL_LENGTH);
            int decimalLength = int.Parse(entityPreference.Value);

            decimal remainFlushBack = sourceMaterialFlushBack.Qty;
            for (int i = 0; i < FlushBackListCount; i++)
            {
                //分配的消耗量等于实际的总消耗量 / 理论总消耗量 * 单个物料的消耗量，最后一条用减法处理
                if (i < FlushBackListCount - 1)
                {
                    materialFlushBackList[i].Qty = Math.Round(sourceMaterialFlushBack.Qty / theoryTotalQty * materialFlushBackList[i].Qty, decimalLength, MidpointRounding.AwayFromZero); ;
                    remainFlushBack = remainFlushBack - materialFlushBackList[i].Qty;
                }
                else
                {
                    materialFlushBackList[i].Qty = remainFlushBack;
                }
            }
            //foreach(MaterialFlushBack materialFlushBack in materialFlushBackList)
            //{




            //    materialFlushBack.Qty = sourceMaterialFlushBack.Qty / theoryTotalQty * materialFlushBack.Qty;
            //}

            return materialFlushBackList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void ImportProdItemHuId(IList<DssImportHistory> dssImportHistoryList)
        {
            foreach (var dssImpHis in dssImportHistoryList)
            {
                this.genericMgr.Create(dssImpHis);
            }
        }
    }
}
