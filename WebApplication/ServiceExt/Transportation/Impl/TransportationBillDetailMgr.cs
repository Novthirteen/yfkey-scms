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
using com.Sconit.Service.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationBillDetailMgr : TransportationBillDetailBaseMgr, ITransportationBillDetailMgr
    {
        private IEntityPreferenceMgr entityPreferenceMgr;

        public TransportationBillDetailMgr(ITransportationBillDetailDao entityDao,
            IEntityPreferenceMgr entityPreferenceMgr)
            : base(entityDao)
        {
            this.entityPreferenceMgr = entityPreferenceMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public TransportationBillDetail TransferTransportationActBill2TransportationBillDetail(TransportationActBill transportationActBill)
        {
            EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(
                BusinessConstants.ENTITY_PREFERENCE_CODE_AMOUNT_DECIMAL_LENGTH);
            int amountDecimalLength = int.Parse(entityPreference.Value);
            TransportationBillDetail transportationBillDetail = new TransportationBillDetail();
            transportationBillDetail.ActBill = transportationActBill;

            transportationBillDetail.TransType = transportationActBill.TransType;
            transportationBillDetail.Currency = transportationActBill.Currency;
            transportationBillDetail.IsIncludeTax = transportationActBill.IsIncludeTax;
            transportationBillDetail.TaxCode = transportationActBill.TaxCode;

            /*
             * 
             * 1.TransType=Transportation �۸���ϸ�������̣� ��  �̲��ѣ�����ʱ
             * a.PricingMethod=M3��KG  ������
             * b.SHIPT   �����
             * 2.TransType=WarehouseLease(�̶�����) �����
             * 3.TransType=Operation(������) ������
             */
            if (transportationActBill.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION
                ||
                (transportationActBill.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION
                && (transportationActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3 || transportationActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_KG)
                )
                )
            {
                transportationBillDetail.UnitPrice = transportationActBill.UnitPrice;
                transportationBillDetail.BilledQty = transportationActBill.CurrentBillQty;
                transportationBillDetail.Discount = transportationActBill.CurrentDiscount;
                if (transportationActBill.CurrentBillQty != (transportationActBill.BillQty - transportationActBill.BilledQty))
                {
                    //���ο�Ʊ��������ʣ������
                    if (transportationActBill.CurrentBillQty > (transportationActBill.BillQty - transportationActBill.BilledQty))
                    {
                        throw new BusinessErrorException("TransportationActBill.Error.CurrentBillQtyGeRemainQty");
                    }

                    //���ο�Ʊ����С��ʣ������
                    transportationBillDetail.Amount = Math.Round((transportationActBill.BillAmount / transportationActBill.BillQty * transportationActBill.CurrentBillQty), amountDecimalLength, MidpointRounding.AwayFromZero);
                }
                else
                {
                    //���ο�Ʊ��������ʣ������
                    transportationBillDetail.Amount = transportationActBill.BillAmount - transportationActBill.BilledAmount;
                }
            }
            else
            {
                transportationBillDetail.UnitPrice = transportationActBill.CurrentBillAmount;
                transportationBillDetail.BilledQty = 1;
                transportationBillDetail.Discount = transportationActBill.CurrentDiscount;
                if (transportationActBill.CurrentBillAmount != (transportationActBill.BillAmount - transportationActBill.BilledAmount))
                {
                    //���ο�Ʊ������ʣ����
                    if (transportationActBill.CurrentBillAmount > (transportationActBill.BillAmount - transportationActBill.BilledAmount))
                    {
                        throw new BusinessErrorException("TransportationActBill.Error.CurrentBillAmountGeRemainAmount");
                    }

                    //���ο�Ʊ���С��ʣ����
                    transportationBillDetail.Amount = transportationActBill.CurrentBillAmount;
                }
                else
                {
                    //���ο�Ʊ������ʣ����
                    transportationBillDetail.Amount = transportationActBill.BillAmount - transportationActBill.BilledAmount;
                }
            }

            return transportationBillDetail;
        }

        #endregion Customized Methods
    }
}