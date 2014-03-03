using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Procurement;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.Distribution;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class PlannedBillMgr : PlannedBillBaseMgr, IPlannedBillMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IUomConversionMgr uomConversionMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IReceiptInProcessLocationMgr receiptInProcessLocationMgr;

        public PlannedBillMgr(IPlannedBillDao entityDao,
            IUomConversionMgr uomConversionMgr,
            IEntityPreferenceMgr entityPreferenceMgr,
            ICriteriaMgr criteriaMgr,
            IReceiptInProcessLocationMgr receiptInProcessLocationMgr)
            : base(entityDao)
        {
            this.uomConversionMgr = uomConversionMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.criteriaMgr = criteriaMgr;
            this.receiptInProcessLocationMgr = receiptInProcessLocationMgr;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<PlannedBill> GetUnSettledPlannedBill(OrderHead orderHead)
        {
            return this.GetUnSettledPlannedBill(orderHead.OrderNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<PlannedBill> GetUnSettledPlannedBill(string orderNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PlannedBill>();
            criteria.Add(Expression.Eq("OrderNo", orderNo));
            criteria.Add(Expression.NotEqProperty("PlannedQty", "ActingQty"));
            return this.criteriaMgr.FindAll<PlannedBill>(criteria);
        }

        #region Customized Methods
        [Transaction(TransactionMode.Requires)]
        public PlannedBill CreatePlannedBill(ReceiptDetail receiptDetail, User user)
        {
            Receipt receipt = receiptDetail.Receipt;

            OrderLocationTransaction orderLocationTransaction = receiptDetail.OrderLocationTransaction;
            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;

            //DateTime dateTimeNow = DateTime.Now;
            //decimal plannedAmount = 0;

            PlannedBill plannedBill = new PlannedBill();
            plannedBill.OrderNo = orderHead.OrderNo;
            plannedBill.ExternalReceiptNo = receipt.ExternalReceiptNo;        //��¼�ͻ��ص���
            plannedBill.ReceiptNo = receipt.ReceiptNo;
            plannedBill.Item = orderDetail.Item;
            plannedBill.SettleTerm = orderDetail.DefaultBillSettleTerm;
            plannedBill.PlannedQty =
                receiptDetail.ReceivedQty.HasValue ? receiptDetail.ReceivedQty.Value : 0;         //���ô���������Ĭ��ֵ
            plannedBill.Uom = orderDetail.Uom;                                                  //��λΪ������λ
            plannedBill.UnitCount = orderDetail.UnitCount;
            plannedBill.UnitQty = orderLocationTransaction.UnitQty;                                 //UnitQty����OrderLocationTransaction
            plannedBill.CreateDate = receipt.CreateDate;
            plannedBill.CreateUser = user;
            plannedBill.LastModifyDate = receipt.CreateDate;
            plannedBill.LastModifyUser = user;
            plannedBill.EffectiveDate = receipt.CreateDate;
            plannedBill.PlannedAmount = receiptDetail.PlannedAmount;
            plannedBill.IsAutoBill = orderHead.IsAutoBill;
            plannedBill.HuId = receiptDetail.HuId;
            plannedBill.LotNo = receiptDetail.LotNo;
            if (orderLocationTransaction.Location != null)
            {
                plannedBill.LocationFrom = orderLocationTransaction.Location.Code;
                //Ϊ��֧�ֶ�site
                //����ǲ��ϸ�Ʒ��λ����Ҫ�ڼ�¼PartyFrom���Ӷ�ͨ��PartyFrom��QAD���ҵ���Ӧ��QC��λ
                if (plannedBill.LocationFrom == BusinessConstants.SYSTEM_LOCATION_REJECT)
                {
                    plannedBill.PartyFrom = orderHead.PartyTo.Code;
                }
            }
            plannedBill.IpNo = receipt.ReferenceIpNo;
            plannedBill.ReferenceItemCode = orderDetail.ReferenceItemCode;

            if (orderDetail.OrderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                || orderDetail.OrderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING)
            {
                plannedBill.TransactionType = BusinessConstants.BILL_TRANS_TYPE_PO;
                plannedBill.BillAddress = orderDetail.DefaultBillFrom;
                plannedBill.PriceList = orderDetail.DefaultPriceListFrom;
                plannedBill.IsProvisionalEstimate =      //�ݹ��۸���û���ҵ��۸�Ҳ��Ϊ���ݹ��۸�
                         orderDetail.PriceListDetailFrom != null ? orderDetail.PriceListDetailFrom.IsProvisionalEstimate : true;

                //plannedAmount = orderDetail.TotalAmountFrom.HasValue ? orderDetail.TotalAmountFrom.Value : 0;
                if (orderDetail.PriceListDetailFrom != null)
                {
                    //�ҵ��۸�
                    plannedBill.UnitPrice = orderDetail.PriceListDetailFrom.UnitPrice;
                    plannedBill.Currency = orderDetail.PriceListDetailFrom.Currency;
                    plannedBill.IsIncludeTax = orderDetail.PriceListDetailFrom.IsIncludeTax;
                    plannedBill.TaxCode = orderDetail.PriceListDetailFrom.TaxCode;
                }
                else
                {
                    //û���ҵ��۸�
                    plannedBill.UnitPrice = 0;
                    plannedBill.Currency = orderDetail.OrderHead.Currency;
                }
            }
            else if (orderDetail.OrderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                plannedBill.TransactionType = BusinessConstants.BILL_TRANS_TYPE_SO;
                plannedBill.BillAddress = orderDetail.DefaultBillTo;
                plannedBill.PriceList = orderDetail.DefaultPriceListTo;
                plannedBill.IsProvisionalEstimate =     //�ݹ��۸���û���ҵ��۸�Ҳ��Ϊ���ݹ��۸�
                orderDetail.PriceListDetailTo != null ? orderDetail.PriceListDetailTo.IsProvisionalEstimate : true;
                #region ��Ч����ȡasn����������
                if (receipt.InProcessLocations != null && receipt.InProcessLocations.Count > 0)
                {
                    plannedBill.EffectiveDate = DateTime.Parse(receipt.InProcessLocations[0].CreateDate.ToShortDateString());
                }

                #endregion

                //plannedAmount = orderDetail.TotalAmountTo.HasValue ? orderDetail.TotalAmountTo.Value : 0;
                if (orderDetail.PriceListDetailTo != null)
                {
                    //�ҵ��۸�
                    plannedBill.UnitPrice = orderDetail.PriceListDetailTo.UnitPrice;
                    plannedBill.Currency = orderDetail.PriceListDetailTo.Currency;
                    plannedBill.IsIncludeTax = orderDetail.PriceListDetailTo.IsIncludeTax;
                    plannedBill.TaxCode = orderDetail.PriceListDetailTo.TaxCode;
                }
                else
                {
                    //û���ҵ��۸�
                    plannedBill.UnitPrice = 0;
                    plannedBill.Currency = orderDetail.OrderHead.Currency;
                }
            }
            else
            {
                throw new TechnicalException("Only SO and PO/SubContract can create planned bill.");
            }

            //if (orderDetail.Uom.Code != plannedBill.Uom.Code)
            //{
            //    //������λ�Ͳɹ���λ��һ�£���Ҫ����UnitQty��PlannedQtyֵ
            //    plannedBill.UnitQty = this.uomConversionMgr.ConvertUomQty(orderDetail.Item, orderDetail.Uom, plannedBill.UnitQty, plannedBill.Uom);
            //    plannedBill.PlannedQty = plannedBill.PlannedQty * plannedBill.UnitQty;
            //}

            this.CreatePlannedBill(plannedBill);

            return plannedBill;
        }

        #endregion Customized Methods
    }
}