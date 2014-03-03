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
using com.Sconit.Utility;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationBillMgr : TransportationBillBaseMgr, ITransportationBillMgr
    {
        private string[] TransportationBillCloneField = new string[] 
            { 
                "ExternalBillNo",
                "BillAddress",
                "Currency",
                "IsIncludeTax",
                "TaxCode"
            };

        private string[] TransportationBillDetailCloneField = new string[] 
            { 
                "ActBill",
                "UnitPrice",
                "Currency",
                "IsIncludeTax",
                "TaxCode",
                "TransType"
            };

        private ITransportationBillDetailMgr transportationBillDetailMgr;
        private ITransportationActBillMgr transportationActBillMgr;
        private INumberControlMgr numberControlMgr;
        private IUserMgr userMgr;

        public TransportationBillMgr(ITransportationBillDao entityDao,
            ITransportationBillDetailMgr transportationBillDetailMgr,
            ITransportationActBillMgr transportationActBillMgr,
            INumberControlMgr numberControlMgr,
            IUserMgr userMgr)
            : base(entityDao)
        {
            this.transportationBillDetailMgr = transportationBillDetailMgr;
            this.transportationActBillMgr = transportationActBillMgr;
            this.numberControlMgr = numberControlMgr;
            this.userMgr = userMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public TransportationBill CheckAndLoadTransportationBill(string billNo)
        {
            TransportationBill transportationBill = this.LoadTransportationBill(billNo);
            if (transportationBill != null)
            {
                return transportationBill;
            }
            else
            {
                throw new BusinessErrorException("TransportationBill.Error.BillNoNotExist", billNo);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public TransportationBill CheckAndLoadTransportationBill(string billNo, bool includeBillDetail)
        {
            TransportationBill transportationBill = this.CheckAndLoadTransportationBill(billNo);

            if (includeBillDetail)
            {
                if (transportationBill.TransportationBillDetails != null && transportationBill.TransportationBillDetails.Count > 0)
                {
                }
            }

            return transportationBill;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, User user)
        {
            return this.CreateTransportationBill(transportationActBillList, user, BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, 0);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, string userCode)
        {
            return this.CreateTransportationBill(transportationActBillList, userCode, BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, 0);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, User user, string status)
        {
            return this.CreateTransportationBill(transportationActBillList, user, status, 0);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, string userCode, string status)
        {
            return this.CreateTransportationBill(transportationActBillList, userCode, status, 0);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, User user, string status, decimal discount)
        {
            if (status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE
                && status != BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT
                && status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
            {
                throw new TechnicalException("status specified is not valided");
            }

            if (transportationActBillList == null || transportationActBillList.Count == 0)
            {
                throw new BusinessErrorException("TransportationBill.Error.EmptyBillDetails");
            }

            DateTime dateTimeNow = DateTime.Now;
            IList<TransportationBill> transportationBillList = new List<TransportationBill>();

            foreach (TransportationActBill transportationActBill in transportationActBillList)
            {
                TransportationActBill oldTransportationActBill = this.transportationActBillMgr.LoadTransportationActBill(transportationActBill.Id);
                oldTransportationActBill.CurrentBillQty = transportationActBill.CurrentBillQty;
                oldTransportationActBill.CurrentBillAmount = transportationActBill.CurrentBillAmount;
                oldTransportationActBill.CurrentDiscount = transportationActBill.CurrentDiscount;

                TransportationBill transportationBill = null;

                #region 查找和待开明细的billAddress、currency一致的TransportationBillMstr
                foreach (TransportationBill thisTransportationBill in transportationBillList)
                {
                    if (thisTransportationBill.BillAddress.Code == oldTransportationActBill.BillAddress.Code
                        && thisTransportationBill.Currency.Code == oldTransportationActBill.Currency.Code)
                    {
                        transportationBill = thisTransportationBill;
                        break;
                    }
                }
                #endregion

                #region 没有找到匹配的TransportationBill，新建
                if (transportationBill == null)
                {
                    #region 检查权限
                    bool hasPermission = false;
                    foreach (Permission permission in user.OrganizationPermission)
                    {
                        if (permission.Code == oldTransportationActBill.BillAddress.Party.Code)
                        {
                            hasPermission = true;
                            break;
                        }
                    }

                    if (!hasPermission)
                    {
                        throw new BusinessErrorException("TransportationBill.Create.Error.NoAuthrization", oldTransportationActBill.BillAddress.Party.Code);
                    }
                    #endregion

                    #region 创建TransportationBill
                    transportationBill = new TransportationBill();
                    transportationBill.BillNo = numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_TRANSPORTATIONBILL);
                    transportationBill.Status = status;
                    transportationBill.BillAddress = oldTransportationActBill.BillAddress;
                    transportationBill.Currency = oldTransportationActBill.Currency;
                    transportationBill.Discount = discount;                   //可能有问题
                    transportationBill.BillType = BusinessConstants.CODE_TRANSPORTATION_TRANSPORTATIONBILL_TYPE_VALUE_NORMAL;
                    transportationBill.CreateDate = dateTimeNow;
                    transportationBill.CreateUser = user;
                    transportationBill.LastModifyDate = dateTimeNow;
                    transportationBill.LastModifyUser = user;

                    this.CreateTransportationBill(transportationBill);
                    transportationBillList.Add(transportationBill);
                    #endregion
                }
                #endregion

                TransportationBillDetail transportationBillDetail = this.transportationBillDetailMgr.TransferTransportationActBill2TransportationBillDetail(oldTransportationActBill);
                transportationBillDetail.Bill = transportationBill;
                transportationBill.AddTransportationBillDetail(transportationBillDetail);

                this.transportationBillDetailMgr.CreateTransportationBillDetail(transportationBillDetail);
                //扣减TransportationActBill数量和金额
                this.transportationActBillMgr.ReverseUpdateTransportationActBill(null, transportationBillDetail, user);
            }

            return transportationBillList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<TransportationBill> CreateTransportationBill(IList<TransportationActBill> transportationActBillList, string userCode, string status, decimal discount)
        {
            return this.CreateTransportationBill(transportationActBillList, this.userMgr.CheckAndLoadUser(userCode), status, discount);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteTransportationBill(string billNo, string userCode)
        {
            this.DeleteTransportationBill(billNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteTransportationBill(string billNo, User user)
        {
            TransportationBill oldTransportationBill = this.CheckAndLoadTransportationBill(billNo, true);

            #region 检查状态
            if (oldTransportationBill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                throw new BusinessErrorException("TransportationBill.Error.StatusErrorWhenDelete", oldTransportationBill.Status, oldTransportationBill.BillNo);
            }
            #endregion

            if (oldTransportationBill.TransportationBillDetails != null && oldTransportationBill.TransportationBillDetails.Count > 0)
            {
                foreach (TransportationBillDetail transportationBillDetail in oldTransportationBill.TransportationBillDetails)
                {
                    TransportationBillDetail oldTransportationBillDetail = this.transportationBillDetailMgr.LoadTransportationBillDetail(transportationBillDetail.Id);
                    //扣减TransportationActBill数量和金额
                    this.transportationActBillMgr.ReverseUpdateTransportationActBill(oldTransportationBillDetail, null, user);

                    this.transportationBillDetailMgr.DeleteTransportationBillDetail(oldTransportationBillDetail);
                }
            }

            this.DeleteTransportationBill(oldTransportationBill);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteTransportationBill(TransportationBill transportationBill, string userCode)
        {
            this.DeleteTransportationBill(transportationBill.BillNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteTransportationBill(TransportationBill transportationBill, User user)
        {
            this.DeleteTransportationBill(transportationBill.BillNo, user);
        }

        [Transaction(TransactionMode.Unspecified)]
        public TransportationBill LoadTransportationBill(string billNo, bool includeBillDetail)
        {
            TransportationBill transportationBill = this.LoadTransportationBill(billNo);

            if (includeBillDetail)
            {
                if (transportationBill.TransportationBillDetails != null && transportationBill.TransportationBillDetails.Count > 0)
                {
                }
            }

            return transportationBill;
        }
        
        [Transaction(TransactionMode.Requires)]
        public void AddTransportationBillDetail(string billNo, IList<TransportationActBill> transportationActBillList, string userCode)
        {
            this.AddTransportationBillDetail(billNo, transportationActBillList, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void AddTransportationBillDetail(string billNo, IList<TransportationActBill> transportationActBillList, User user)
        {
            TransportationBill oldTransportationBill = this.CheckAndLoadTransportationBill(billNo, true);

            #region 检查状态
            if (oldTransportationBill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                throw new BusinessErrorException("TransportationBill.Error.StatusErrorWhenAddDetail", oldTransportationBill.Status, oldTransportationBill.BillNo);
            }
            #endregion

            if (transportationActBillList != null && transportationActBillList.Count > 0)
            {
                foreach (TransportationActBill transportationActBill in transportationActBillList)
                {
                    TransportationActBill oldTransportationActBill = this.transportationActBillMgr.LoadTransportationActBill(transportationActBill.Id);
                    oldTransportationActBill.CurrentBillQty = transportationActBill.CurrentBillQty;
                    oldTransportationActBill.CurrentDiscount = transportationActBill.CurrentDiscount;

                    TransportationBillDetail transportationBillDetail = this.transportationBillDetailMgr.TransferTransportationActBill2TransportationBillDetail(oldTransportationActBill);
                    transportationBillDetail.Bill = oldTransportationBill;
                    oldTransportationBill.AddTransportationBillDetail(transportationBillDetail);

                    this.transportationBillDetailMgr.CreateTransportationBillDetail(transportationBillDetail);
                    //扣减TransportationActBill数量和金额
                    this.transportationActBillMgr.ReverseUpdateTransportationActBill(null, transportationBillDetail, user);
                }

                oldTransportationBill.LastModifyDate = DateTime.Now;
                oldTransportationBill.LastModifyUser = user;

                this.UpdateTransportationBill(oldTransportationBill);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void AddTransportationBillDetail(TransportationBill transportationBill, IList<TransportationActBill> transportationActBillList, string userCode)
        {
            this.AddTransportationBillDetail(transportationBill.BillNo, transportationActBillList, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void AddTransportationBillDetail(TransportationBill transportationBill, IList<TransportationActBill> transportationActBillList, User user)
        {
            this.AddTransportationBillDetail(transportationBill.BillNo, transportationActBillList, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateTransportationBill(TransportationBill transportationBill, string userCode)
        {
            this.UpdateTransportationBill(transportationBill, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateTransportationBill(TransportationBill transportationBill, User user)
        {
            TransportationBill oldTransportationBill = this.CheckAndLoadTransportationBill(transportationBill.BillNo, true);
            oldTransportationBill.Discount = transportationBill.Discount;
            oldTransportationBill.ExternalBillNo = transportationBill.ExternalBillNo;

            #region 检查状态
            if (oldTransportationBill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                throw new BusinessErrorException("TransportationBill.Error.StatusErrorWhenUpdate", oldTransportationBill.Status, oldTransportationBill.BillNo);
            }
            #endregion

            #region 更新明细
            if (transportationBill.TransportationBillDetails != null && transportationBill.TransportationBillDetails.Count > 0)
            {
                foreach (TransportationBillDetail newTransportationBillDetail in transportationBill.TransportationBillDetails)
                {
                    TransportationBillDetail oldTransportationBillDetail = this.transportationBillDetailMgr.LoadTransportationBillDetail(newTransportationBillDetail.Id);
                    newTransportationBillDetail.ActBill = oldTransportationBillDetail.ActBill;
                    newTransportationBillDetail.UnitPrice = oldTransportationBillDetail.UnitPrice;
                    newTransportationBillDetail.Currency = oldTransportationBillDetail.Currency;
                    newTransportationBillDetail.IsIncludeTax = oldTransportationBillDetail.IsIncludeTax;
                    newTransportationBillDetail.TaxCode = oldTransportationBillDetail.TaxCode;

                    /*
                     * 
                     * 1.TransType=Transportation 价格单明细（承运商） 或  短拨费（区域）时
                     * a.PricingMethod=M3或KG  按数量
                     * b.SHIPT   按金额
                     * 2.TransType=WarehouseLease(固定费用) 按金额
                     * 3.TransType=Operation(操作费) 按数量
                     */
                    if (oldTransportationBillDetail.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION
                        ||
                        (oldTransportationBillDetail.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION
                        && (oldTransportationBillDetail.ActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3 || oldTransportationBillDetail.ActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_KG)
                        )
                        )
                    {
                        //反向更新TransportationActBill，会重新计算开票金额
                        if (oldTransportationBillDetail.BilledQty != newTransportationBillDetail.BilledQty)
                        {
                            this.transportationActBillMgr.ReverseUpdateTransportationActBill(
                                oldTransportationBillDetail, newTransportationBillDetail, user);
                        }

                        oldTransportationBillDetail.Amount = newTransportationBillDetail.Amount;
                        oldTransportationBillDetail.BilledQty = newTransportationBillDetail.BilledQty;
                        oldTransportationBillDetail.Discount = newTransportationBillDetail.Discount;
                    }
                    else
                    {
                        //反向更新TransportationActBill，会重新计算开票金额
                        if (oldTransportationBillDetail.Amount != newTransportationBillDetail.Amount)
                        {
                            this.transportationActBillMgr.ReverseUpdateTransportationActBill(
                                oldTransportationBillDetail, newTransportationBillDetail, user);
                        }

                        oldTransportationBillDetail.UnitPrice = newTransportationBillDetail.Amount;
                        oldTransportationBillDetail.Amount = newTransportationBillDetail.Amount;
                        oldTransportationBillDetail.BilledQty = 1;
                        oldTransportationBillDetail.Discount = newTransportationBillDetail.Discount;
                    }

                    this.transportationBillDetailMgr.UpdateTransportationBillDetail(oldTransportationBillDetail);
                }
            }
            #endregion

            oldTransportationBill.LastModifyUser = user;
            oldTransportationBill.LastModifyDate = DateTime.Now;

            this.UpdateTransportationBill(oldTransportationBill);
        }

        [Transaction(TransactionMode.Requires)]
        public void ReleaseTransportationBill(string billNo, string userCode)
        {
            this.ReleaseTransportationBill(billNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void ReleaseTransportationBill(string billNo, User user)
        {
            TransportationBill oldTransportationBill = this.CheckAndLoadTransportationBill(billNo);

            #region 检查状态
            if (oldTransportationBill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                throw new BusinessErrorException("TransportationBill.Error.StatusErrorWhenRelease", oldTransportationBill.Status, oldTransportationBill.BillNo);
            }
            #endregion

            #region 检查明细不能为空
            if (oldTransportationBill.TransportationBillDetails == null || oldTransportationBill.TransportationBillDetails.Count == 0)
            {
                throw new BusinessErrorException("TransportationBill.Error.EmptyBillDetail", oldTransportationBill.BillNo);
            }
            #endregion

            #region 记录开票事务
            /*20110422 Tag 先做标记，后期补充
            foreach (TransportationBillDetail transportationBillDetail in oldTransportationBill.TransportationBillDetails)
            {
                this.billTransactionMgr.RecordBillTransaction(transportationBillDetail, user, false);
            }
             */
            #endregion

            oldTransportationBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
            oldTransportationBill.LastModifyUser = user;
            oldTransportationBill.LastModifyDate = DateTime.Now;

            this.UpdateTransportationBill(oldTransportationBill);
        }

        [Transaction(TransactionMode.Requires)]
        public void ReleaseTransportationBill(TransportationBill transportationBill, string userCode)
        {
            this.ReleaseTransportationBill(transportationBill.BillNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void ReleaseTransportationBill(TransportationBill transportationBill, User user)
        {
            this.ReleaseTransportationBill(transportationBill.BillNo, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void CancelTransportationBill(string billNo, string userCode)
        {
            this.CancelTransportationBill(billNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CancelTransportationBill(string billNo, User user)
        {
            TransportationBill oldTransportationBill = this.CheckAndLoadTransportationBill(billNo);

            #region 检查状态
            if (oldTransportationBill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
            {
                throw new BusinessErrorException("TransportationBill.Error.StatusErrorWhenCancel", oldTransportationBill.Status, oldTransportationBill.BillNo);
            }
            #endregion

            if (oldTransportationBill.TransportationBillDetails != null && oldTransportationBill.TransportationBillDetails.Count > 0)
            {
                foreach (TransportationBillDetail newTransportationBillDetail in oldTransportationBill.TransportationBillDetails)
                {
                    TransportationBillDetail oldTransportationBillDetail = this.transportationBillDetailMgr.LoadTransportationBillDetail(newTransportationBillDetail.Id);

                    //反向更新TransportationActBill
                    this.transportationActBillMgr.ReverseUpdateTransportationActBill(oldTransportationBillDetail, null, user);

                    #region 记录开票事务
                    /*20110422 Tag 先做标记，后期补充
                    this.billTransactionMgr.RecordBillTransaction(oldTransportationBillDetail, user, true);
                     */
                    #endregion
                }
            }

            oldTransportationBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL;
            oldTransportationBill.LastModifyUser = user;
            oldTransportationBill.LastModifyDate = DateTime.Now;

            this.UpdateTransportationBill(oldTransportationBill);
        }

        [Transaction(TransactionMode.Requires)]
        public void CancelTransportationBill(TransportationBill transportationBill, string userCode)
        {
            this.CancelTransportationBill(transportationBill.BillNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CancelTransportationBill(TransportationBill transportationBill, User user)
        {
            this.CancelTransportationBill(transportationBill.BillNo, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void CloseTransportationBill(string billNo, string userCode)
        {
            this.CloseTransportationBill(billNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CloseTransportationBill(string billNo, User user)
        {
            TransportationBill oldTransportationBill = this.CheckAndLoadTransportationBill(billNo);

            #region 检查状态
            if (oldTransportationBill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
            {
                throw new BusinessErrorException("TransportationBill.Error.StatusErrorWhenClose", oldTransportationBill.Status, oldTransportationBill.BillNo);
            }
            #endregion

            oldTransportationBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
            oldTransportationBill.LastModifyUser = user;
            oldTransportationBill.LastModifyDate = DateTime.Now;

            this.UpdateTransportationBill(oldTransportationBill);
        }

        [Transaction(TransactionMode.Requires)]
        public void CloseTransportationBill(TransportationBill transportationBill, string userCode)
        {
            this.CloseTransportationBill(transportationBill.BillNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CloseTransportationBill(TransportationBill transportationBill, User user)
        {
            this.CloseTransportationBill(transportationBill.BillNo, user);
        }

        [Transaction(TransactionMode.Requires)]
        public TransportationBill VoidTransportationBill(string billNo, string userCode)
        {
            return this.VoidTransportationBill(billNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public TransportationBill VoidTransportationBill(string billNo, User user)
        {
            TransportationBill oldTransportationBill = this.CheckAndLoadTransportationBill(billNo, true);
            DateTime dateTimeNow = DateTime.Now;

            #region 检查状态
            if (oldTransportationBill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
            {
                throw new BusinessErrorException("TransportationBill.Error.StatusErrorWhenVoid", oldTransportationBill.Status, oldTransportationBill.BillNo);
            }
            #endregion

            #region 创建作废账单
            TransportationBill voidTransportationBill = new TransportationBill();
            CloneHelper.CopyProperty(oldTransportationBill, voidTransportationBill, this.TransportationBillCloneField);

            voidTransportationBill.BillNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_TRANSPORTATIONBILL_RED);
            voidTransportationBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
            if (oldTransportationBill.Discount.HasValue)
            {
                voidTransportationBill.Discount = 0 - oldTransportationBill.Discount.Value;
            }
            voidTransportationBill.ReferenceBillNo = oldTransportationBill.BillNo;
            voidTransportationBill.BillType = BusinessConstants.CODE_TRANSPORTATION_TRANSPORTATIONBILL_TYPE_VALUE_CANCEL;
            voidTransportationBill.CreateDate = dateTimeNow;
            voidTransportationBill.CreateUser = user;
            voidTransportationBill.LastModifyDate = dateTimeNow;
            voidTransportationBill.LastModifyUser = user;

            this.CreateTransportationBill(voidTransportationBill);
            #endregion

            #region 创建作废账单明细
            foreach (TransportationBillDetail oldTransportationBillDetail in oldTransportationBill.TransportationBillDetails)
            {
                TransportationBillDetail voidTransportationBillDetail = new TransportationBillDetail();
                CloneHelper.CopyProperty(oldTransportationBillDetail, voidTransportationBillDetail, this.TransportationBillDetailCloneField);
                voidTransportationBillDetail.BilledQty = 0 - oldTransportationBillDetail.BilledQty;
                voidTransportationBillDetail.Discount = 0 - oldTransportationBillDetail.Discount;
                voidTransportationBillDetail.Amount = 0 - oldTransportationBillDetail.Amount;
                voidTransportationBillDetail.Bill = voidTransportationBill;

                this.transportationBillDetailMgr.CreateTransportationBillDetail(voidTransportationBillDetail);
                voidTransportationBill.AddTransportationBillDetail(voidTransportationBillDetail);

                //反向更新TransportationActBill
                this.transportationActBillMgr.ReverseUpdateTransportationActBill(null, voidTransportationBillDetail, user);
            }
            #endregion

            #region 记录开票事务
            /*20110422 Tag 先做标记，后期补充
            foreach (TransportationBillDetail transportationBillDetail in oldTransportationBill.TransportationBillDetails)
            {
                this.billTransactionMgr.RecordBillTransaction(transportationBillDetail, user, true);
            }
             */
            #endregion

            #region 更新原账单
            oldTransportationBill.ReferenceBillNo = voidTransportationBill.BillNo;
            oldTransportationBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_VOID;
            oldTransportationBill.LastModifyDate = dateTimeNow;
            oldTransportationBill.LastModifyUser = user;

            this.UpdateTransportationBill(oldTransportationBill);
            #endregion

            return voidTransportationBill;
        }

        [Transaction(TransactionMode.Requires)]
        public TransportationBill VoidTransportationBill(TransportationBill transportationBill, string userCode)
        {
            return this.VoidTransportationBill(transportationBill.BillNo, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public TransportationBill VoidTransportationBill(TransportationBill transportationBill, User user)
        {
            return this.VoidTransportationBill(transportationBill.BillNo, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteTransportationBillDetail(IList<TransportationBillDetail> transportationBillDetailList, string userCode)
        {
            this.DeleteTransportationBillDetail(transportationBillDetailList, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteTransportationBillDetail(IList<TransportationBillDetail> transportationBillDetailList, User user)
        {
            if (transportationBillDetailList != null && transportationBillDetailList.Count > 0)
            {
                IDictionary<string, TransportationBill> cachedBillDic = new Dictionary<string, TransportationBill>();

                foreach (TransportationBillDetail transportationBillDetail in transportationBillDetailList)
                {
                    TransportationBillDetail oldTransportationBillDetail = this.transportationBillDetailMgr.LoadTransportationBillDetail(transportationBillDetail.Id);
                    TransportationBill transportationBill = oldTransportationBillDetail.Bill;

                    #region 缓存TransportationBill
                    if (!cachedBillDic.ContainsKey(transportationBill.BillNo))
                    {
                        cachedBillDic.Add(transportationBill.BillNo, transportationBill);

                        #region 检查状态
                        if (transportationBill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                        {
                            throw new BusinessErrorException("TransportationBill.Error.StatusErrorWhenDeleteDetail", transportationBill.Status, transportationBill.BillNo);
                        }
                        #endregion
                    }
                    #endregion

                    //扣减TransportationActBill数量和金额
                    this.transportationActBillMgr.ReverseUpdateTransportationActBill(oldTransportationBillDetail, null, user);

                    this.transportationBillDetailMgr.DeleteTransportationBillDetail(oldTransportationBillDetail);
                }

                #region 更新TransportationBill
                DateTime dateTimeNow = DateTime.Now;
                foreach (TransportationBill transportationBill in cachedBillDic.Values)
                {
                    transportationBill.LastModifyDate = dateTimeNow;
                    transportationBill.LastModifyUser = user;

                    this.UpdateTransportationBill(transportationBill);
                }
                #endregion
            }
        }

        #endregion Customized Methods
    }
}