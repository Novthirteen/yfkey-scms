using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using System.Collections;
using System;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ReceiptDetailMgr : ReceiptDetailBaseMgr, IReceiptDetailMgr
    {

        private ICriteriaMgr criteriaMgr;
        private IInProcessLocationDetailMgr inProcessLocationDetailMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        public ReceiptDetailMgr(IReceiptDetailDao entityDao,
            IInProcessLocationDetailMgr inProcessLocationDetailMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.inProcessLocationDetailMgr = inProcessLocationDetailMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Requires)]
        public IList<ReceiptDetail> CreateReceiptDetail(Receipt receipt, OrderLocationTransaction inOrderLocationTransaction, IList<Hu> huList)
        {
            IList<ReceiptDetail> receiptDetailList = new List<ReceiptDetail>();

            foreach (Hu hu in huList)
            {
                ReceiptDetail receiptDetail = new ReceiptDetail();
                receiptDetail.Receipt = receipt;
                receiptDetail.OrderLocationTransaction = inOrderLocationTransaction;
                receiptDetail.HuId = hu.HuId;
                receiptDetail.LotNo = hu.LotNo;

                //根据hu的质量标记判断是正品还是次品
                if (hu.QualityLevel == BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1)
                {
                    //先乘Hu.UnitQty转为基本单位，在除outOrderLocationTransaction.UnitQty转为订单单位。
                    receiptDetail.ReceivedQty = hu.Qty * hu.UnitQty / inOrderLocationTransaction.UnitQty;
                }
                else if (hu.QualityLevel == BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_2)
                {
                    receiptDetail.RejectedQty = hu.Qty * hu.UnitQty / inOrderLocationTransaction.UnitQty;
                }
                else
                {
                    throw new TechnicalException("hu quality level:" + hu.QualityLevel + " not valided");
                }

                this.CreateReceiptDetail(receiptDetail);

                receiptDetailList.Add(receiptDetail);
                receipt.AddReceiptDetail(receiptDetail);
            }

            return receiptDetailList;
        }

        [Transaction(TransactionMode.Requires)]
        public override void CreateReceiptDetail(ReceiptDetail receiptDetail)
        {
            #region 记录发货明细上的收货数量
            OrderLocationTransaction inOrderLocationTransaction = receiptDetail.OrderLocationTransaction;
            OrderDetail orderDetail = inOrderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;
            if (receiptDetail.ReceivedInProcessLocationDetail != null)
            {
                //如果已经在收货时做过匹配，直接把收货数记录到匹配的InProcessLocationDetail记录上
                InProcessLocationDetail inProcessLocationDetail = this.inProcessLocationDetailMgr.LoadInProcessLocationDetail(receiptDetail.ReceivedInProcessLocationDetail.Id);
                if (inProcessLocationDetail.ReceivedQty.HasValue)
                {
                    inProcessLocationDetail.ReceivedQty += receiptDetail.ReceivedQty;
                }
                else
                {
                    inProcessLocationDetail.ReceivedQty = receiptDetail.ReceivedQty;
                }
                this.inProcessLocationDetailMgr.UpdateInProcessLocationDetail(inProcessLocationDetail);
            }
            else if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION) //生产不需要记录收货数量
            {
                #region 找到对应的发货单IpNo
                OrderLocationTransaction outOrderLocationTransaction = this.orderLocationTransactionMgr.GetOrderLocationTransaction(orderDetail.Id, BusinessConstants.IO_TYPE_OUT)[0];

                DetachedCriteria criteria = DetachedCriteria.For<ReceiptInProcessLocation>();
                criteria.SetProjection(Projections.Property("InProcessLocation.IpNo"));
                criteria.Add(Expression.Eq("Receipt.ReceiptNo", receiptDetail.Receipt.ReceiptNo));

                IList list = this.criteriaMgr.FindAll(criteria);
                string ipNo = (string)list[0];
                #endregion

                IList<InProcessLocationDetail> inProcessLocationDetailList = this.inProcessLocationDetailMgr.GetInProcessLocationDetail(ipNo, outOrderLocationTransaction.Id);
                if (inProcessLocationDetailList == null || inProcessLocationDetailList.Count == 0)
                {
                    throw new TechnicalException("can't find InProcessLocationDetail by ipno and outOrderLocationTransactionId");
                }
                else if (inProcessLocationDetailList.Count == 1)
                {
                    if (inProcessLocationDetailList[0].ReceivedQty.HasValue)
                    {
                        inProcessLocationDetailList[0].ReceivedQty += receiptDetail.ReceivedQty;
                    }
                    else
                    {
                        inProcessLocationDetailList[0].ReceivedQty = receiptDetail.ReceivedQty;
                    }
                    this.inProcessLocationDetailMgr.UpdateInProcessLocationDetail(inProcessLocationDetailList[0]);
                }
                else
                {
                    throw new NotImplementedException("Find serverl InProcessLocationDetails by ipno and outOrderLocationTransactionId, not impl yet.");
                }
            }
            #endregion

            this.entityDao.CreateReceiptDetail(receiptDetail);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ReceiptDetail> SummarizeReceiptDetails(IList<ReceiptDetail> receiptDetailList)
        {
            IList<ReceiptDetail> recDetList = new List<ReceiptDetail>();
            if (receiptDetailList != null && receiptDetailList.Count > 0)
            {
                foreach (ReceiptDetail recDetail in receiptDetailList)
                {
                    //if (recDetail.HuId == null)
                    //{
                    //    //不支持Hu,不需要汇总
                    //    return receiptDetailList;
                    //}

                    bool isExist = false;
                    foreach (ReceiptDetail recDet in recDetList)
                    {
                        //OrderLocationTransaction相同的汇总
                        if (recDetail.OrderLocationTransaction.Id == recDet.OrderLocationTransaction.Id)
                        {
                            if (recDetail.HuId == null || recDetail.HuId == string.Empty)
                            {
                                recDet.ShippedQty += recDetail.ShippedQty;
                            }
                            else
                            {
                                recDet.ShippedQty = recDetail.ShippedQty;
                            }
                            recDet.ReceivedQty += recDetail.ReceivedQty;
                            recDet.AddHuReceiptDetails(recDetail);
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        ReceiptDetail receiptDetail = new ReceiptDetail();
                        CloneHelper.CopyProperty(recDetail, receiptDetail, new string[] { "Id", "HuId" }, true);
                        receiptDetail.AddHuReceiptDetails(recDetail);
                        recDetList.Add(receiptDetail);
                    }
                }
            }

            return recDetList;
        }
        #endregion Customized Methods
    }
}