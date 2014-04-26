using System;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using com.Sconit.Persistence;
using System.Data;
using System.Data.SqlClient;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class NumberControlMgr : NumberControlBaseMgr, INumberControlMgr
    {
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IUomConversionMgr uomConversionMgr;
        private ISqlHelperDao sqlHelperDao;

        public NumberControlMgr(INumberControlDao entityDao,
            IEntityPreferenceMgr entityPreferenceMgr,
            IUomConversionMgr uomConversionMgr,
            ISqlHelperDao sqlHelperDao)
            : base(entityDao)
        {
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.sqlHelperDao = sqlHelperDao;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Requires)]
        public int GenerateNumberNextSequence(string code)
        {
            string numberSuffix = GetNextSequence(code);
            return Convert.ToInt32(numberSuffix);
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateNumber(string code)
        {
            string numberSuffix = GetNextSequence(code);

            EntityPreference entityPreference = entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_ORDER_LENGTH);
            int orderLength = int.Parse(entityPreference.Value);
            numberSuffix = numberSuffix.PadLeft(orderLength - code.Length, '0');


            return (code + numberSuffix);
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateNumber(string code, int numberSuffixLength)
        {
            string numberSuffix = GetNextSequence(code);

            if (numberSuffix.Length > numberSuffixLength)
            {
                throw new TechnicalException("numberSuffix.length > numberSuffixLength");
            }

            numberSuffix = numberSuffix.PadLeft(numberSuffixLength, '0');

            return (code + numberSuffix);
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateRMHuId(FlowDetail flowDetail, string lotNo, decimal qty)
        {
            return GenerateRMHuId(flowDetail, lotNo, qty, null);
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateRMHuId(FlowDetail flowDetail, string lotNo, decimal qty, string idMark)
        {
            #region ��������
            //if (flowDetail.BarCodeType == BusinessConstants.CODE_MASTER_RAW_MATERIAL_BAR_CODE_TYPE_VALUE_DEFAULT)
            //{

            //}
            //else
            //{

            //}
            //ItemCode����Ӧ�̱�ʶ��1λ������ �� ��4λ + ������4λ������4λ��0���룩��������ţ�3λ������3λ��0���룩
            string itemCode = flowDetail.Item.Code;
            string supIdMark = idMark != null && idMark.Trim() != string.Empty ? idMark :
                (flowDetail.IdMark != null && flowDetail.IdMark.Trim() != string.Empty
                ? flowDetail.IdMark.Trim() : BusinessConstants.DEFAULT_SUPPLIER_ID_MARK);
            if (lotNo != null && lotNo.Trim().Length != 0)
            {
                LotNoHelper.validateLotNo(lotNo);
            }
            else
            {
                lotNo = LotNoHelper.GenerateLotNo();
            }
            if (flowDetail.Item.Uom.Code != flowDetail.Uom.Code)
            {
                qty = qty * this.uomConversionMgr.ConvertUomQty(flowDetail.Item, flowDetail.Uom, 1, flowDetail.Item.Uom);   //��λ����
            }
            string qtyStr = qty.ToString().PadLeft(4, '0');

            string barCodePrefix = itemCode + supIdMark + lotNo + qtyStr;
            return GenerateNumber(barCodePrefix, 3);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateFGHuId(FlowDetail flowDetail, string shiftCode, decimal qty)
        {
            return GenerateFGHuId(flowDetail, shiftCode, qty, null);
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateFGHuId(FlowDetail flowDetail, string shiftCode, decimal qty, string idMark)
        {
            #region ��Ʒ����
            //if (flowDetail.BarCodeType == BusinessConstants.CODE_MASTER_FINISH_GOODS_BAR_CODE_TYPE_VALUE_DEFAULT)
            //{

            //}
            //else
            //{

            //}
            //������ʶ����2λ����Ʒʶ����(1λ) ����������4λ (����ͬ5.5.1.2�еĹ������ڱ���) ������1λ (�Դ�д��ĸ��ʾ) ��3λ˳���
            string pl = flowDetail.Flow.Code;
            if (pl.Length > 2)
            {
                //��������ߴ��볤�ȴ���2��ȡ����λ
                pl = pl.Substring(pl.Length - 2);
            }

            string pIdMark = idMark != null && idMark.Trim() != string.Empty ? idMark :
                (flowDetail.IdMark != null && flowDetail.IdMark.Trim() != string.Empty
                ? flowDetail.IdMark.Trim() : BusinessConstants.DEFAULT_FINISHI_GOODS_ID_MARK);
            string lotNo = flowDetail.HuLotNo;
            if (lotNo != null && lotNo.Trim().Length != 0)
            {
                LotNoHelper.validateLotNo(lotNo);
            }
            else
            {
                lotNo = LotNoHelper.GenerateLotNo();
            }
            string shift = shiftCode;

            string barCodePrefix = pl + pIdMark + lotNo + shift;
            return GenerateNumber(barCodePrefix, 3);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateRMHuId(OrderDetail orderDetail, string lotNo, decimal qty)
        {
            return GenerateRMHuId(orderDetail, lotNo, qty, null);
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateRMHuId(OrderDetail orderDetail, string lotNo, decimal qty, string idMark)
        {
            #region ��������
            //if (orderDetail.BarCodeType == BusinessConstants.CODE_MASTER_RAW_MATERIAL_BAR_CODE_TYPE_VALUE_DEFAULT)
            //{

            //}
            //else
            //{

            //}
            //ItemCode����Ӧ�̱�ʶ��1λ������ �� ��4λ + ������4λ������4λ��0���룩��������ţ�3λ������3λ��0���룩
            string itemCode = orderDetail.Item.Code;
            string supIdMark = idMark != null && idMark.Trim() != string.Empty ? idMark :
                (orderDetail.IdMark != null && orderDetail.IdMark.Trim() != string.Empty
                ? orderDetail.IdMark.Trim() : BusinessConstants.DEFAULT_SUPPLIER_ID_MARK);
            if (lotNo != null && lotNo.Trim().Length != 0)
            {
                LotNoHelper.validateLotNo(lotNo);
            }
            else
            {
                lotNo = LotNoHelper.GenerateLotNo();
            }

            //��λ���㣬ת��Ϊ������λ
            //������Ӧ��qty * inOrderLocationTransaction.UnitQty��������ʱֱ���õ�λ����
            if (orderDetail.Item.Uom.Code != orderDetail.Uom.Code)
            {
                qty = qty * uomConversionMgr.ConvertUomQty(orderDetail.Item, orderDetail.Uom, qty, orderDetail.Item.Uom);
            }

            string qtyStr = qty.ToString("0.########").PadLeft(4, '0');

            string barCodePrefix = itemCode + supIdMark + lotNo + qtyStr;
            return GenerateNumber(barCodePrefix, 3);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateFGHuId(OrderDetail orderDetail, decimal qty)
        {
            return GenerateFGHuId(orderDetail, qty, null);
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateFGHuId(OrderDetail orderDetail, decimal qty, string idMark)
        {
            #region ��Ʒ����
            //if (orderDetail.BarCodeType == BusinessConstants.CODE_MASTER_FINISH_GOODS_BAR_CODE_TYPE_VALUE_DEFAULT)
            //{

            //}
            //else
            //{

            //}
            //������ʶ����2λ����Ʒʶ����(1λ) ����������4λ (����ͬ5.5.1.2�еĹ������ڱ���) ������1λ (�Դ�д��ĸ��ʾ) ��3λ˳���
            string pl = orderDetail.OrderHead.Flow;
            if (pl.Length > 2)
            {
                //��������ߴ��볤�ȴ���2��ȡ����λ
                pl = pl.Substring(pl.Length - 2);
            }

            string pIdMark = idMark != null && idMark.Trim() != string.Empty ? idMark :
                (orderDetail.IdMark != null && orderDetail.IdMark.Trim() != string.Empty
                ? orderDetail.IdMark.Trim() : BusinessConstants.DEFAULT_FINISHI_GOODS_ID_MARK);
            string lotNo = LotNoHelper.GenerateLotNo();
            string shift = orderDetail.OrderHead.Shift.Code;

            string barCodePrefix = pl + pIdMark + lotNo + shift;
            return GenerateNumber(barCodePrefix, 3);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateRMHuId(InProcessLocationDetail inProcessLocationDetail, string lotNo, decimal qty)
        {
            return GenerateRMHuId(inProcessLocationDetail.OrderLocationTransaction.OrderDetail, lotNo, qty);
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateRMHuId(ReceiptDetail receiptDetail, string lotNo, decimal qty)
        {
            return GenerateRMHuId(receiptDetail.OrderLocationTransaction.OrderDetail, lotNo, qty);
        }

        [Transaction(TransactionMode.Requires)]
        public string GenerateFGHuId(ReceiptDetail receiptDetail, decimal qty)
        {
            return GenerateFGHuId(receiptDetail.OrderLocationTransaction.OrderDetail, qty);
        }

        [Transaction(TransactionMode.Requires)]
        public string CloneRMHuId(string huId, decimal qty)
        {
            #region ��������
            string prefix = huId.Substring(0, huId.Length - 7);
            string qtyStr = qty.ToString("0.########").PadLeft(4, '0');

            string barCodePrefix = prefix + qtyStr;
            return GenerateNumber(barCodePrefix, 3);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public string CloneFGHuId(string huId)
        {
            #region ��Ʒ����
            string barCodePrefix = huId.Substring(0, huId.Length - 3);
            return GenerateNumber(barCodePrefix, 3);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public void ReverseUpdateHuId(string huId)
        {
            string barCodePrefix = BarcodeHelper.GetBarcodePrefix(huId);
            int seq = BarcodeHelper.GetBarcodeSeq(huId);
            ReverseUpdateSequence(BarcodeHelper.GetBarcodePrefix(huId), seq);
        }

        #endregion Customized Methods

        #region Private Methods
        private String GetNextSequence(string code)
        {
            SqlParameter[] parm = new SqlParameter[2];

            parm[0] = new SqlParameter("@CodePrefix", SqlDbType.VarChar, 50);
            parm[0].Value = code;

            parm[1] = new SqlParameter("@NextSequence", SqlDbType.Int, 50);
            parm[1].Direction = ParameterDirection.InputOutput;

            sqlHelperDao.ExecuteStoredProcedure("GetNextSequence", parm);

            return parm[1].Value.ToString();
        }

        private void ReverseUpdateSequence(string code, int seq)
        {
            SqlParameter[] parm = new SqlParameter[2];

            parm[0] = new SqlParameter("@CodePrefix", SqlDbType.VarChar, 50);
            parm[0].Value = code;

            parm[1] = new SqlParameter("@NextSequence", SqlDbType.Int, 50);
            parm[1].Value = seq;

            sqlHelperDao.ExecuteStoredProcedure("GetNextSequence", parm);
        }
        #endregion
    }
}