using System;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class BillDetailMgr : BillDetailBaseMgr, IBillDetailMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;

        public BillDetailMgr(IBillDetailDao entityDao, 
            ICriteriaMgr criteriaMgr,
            IEntityPreferenceMgr entityPreferenceMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public IList<BillDetail> GetBillDetail(string billNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BillDetail>();
            criteria.Add(Expression.Eq("Bill.BillNo", billNo));

            return this.criteriaMgr.FindAll<BillDetail>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BillDetail TransferAtingBill2BillDetail(ActingBill actingBill)
        {
            EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(
                BusinessConstants.ENTITY_PREFERENCE_CODE_AMOUNT_DECIMAL_LENGTH);
            int amountDecimalLength = int.Parse(entityPreference.Value);
            BillDetail billDetail = new BillDetail();
            billDetail.ActingBill = actingBill;

            billDetail.Currency = actingBill.Currency;
            billDetail.IsIncludeTax = actingBill.IsIncludeTax;
            billDetail.TaxCode = actingBill.TaxCode;
            billDetail.UnitPrice = actingBill.UnitPrice;
            billDetail.BilledQty = actingBill.CurrentBillQty;
            billDetail.Discount = actingBill.CurrentDiscount;
            billDetail.LocationFrom = actingBill.LocationFrom;
            billDetail.IpNo = actingBill.IpNo;
            billDetail.ReferenceItemCode = actingBill.ReferenceItemCode;
            billDetail.IsProvEst = actingBill.IsProvisionalEstimate;
            if (actingBill.CurrentBillQty != (actingBill.BillQty - actingBill.BilledQty))
            {
                //本次开票数量大于剩余数量
                if (Math.Abs(actingBill.CurrentBillQty) > Math.Abs(actingBill.BillQty - actingBill.BilledQty))
                {
                    throw new BusinessErrorException("ActingBill.Error.CurrentBillQtyGeRemainQty");
                }

                //本次开票数量小于剩余数量
                billDetail.OrderAmount = Math.Round((actingBill.BillAmount / actingBill.BillQty * actingBill.CurrentBillQty), amountDecimalLength, MidpointRounding.AwayFromZero);
            }
            else
            {
                //本次开票数量等于剩余数量
                billDetail.OrderAmount = actingBill.BillAmount - actingBill.BilledAmount;
            }           

            return billDetail;
        }

        #endregion Customized Methods       
    }    
}