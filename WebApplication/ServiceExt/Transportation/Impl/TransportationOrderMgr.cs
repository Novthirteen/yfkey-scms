using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Transportation;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Transportation;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.MasterData.Impl;
using com.Sconit.Service.Distribution;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using System.Linq;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationOrderMgr : TransportationOrderBaseMgr, ITransportationOrderMgr
    {
        private INumberControlMgr numberControlMgr;
        private ITransportationRouteMgr transportationRouteMgr;
        private IUserMgr userMgr;
        private ITransportationOrderDetailMgr transportationOrderDetailMgr;
        private IInProcessLocationMgr inProcessLocationMgr;
        private IExpenseMgr expenseMgr;
        private ICriteriaMgr criteriaMgr;
        private ITransportationActBillMgr transportationActBillMgr;
        private IBillAddressMgr billAddressMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;

        public TransportationOrderMgr(ITransportationOrderDao entityDao, ITransportationRouteMgr transportationRouteMgr,
            IUserMgr userMgr, ITransportationOrderDetailMgr transportationOrderDetailMgr, INumberControlMgr numberControlMgr,
            IInProcessLocationMgr inProcessLocationMgr, IExpenseMgr expenseMgr, ICriteriaMgr criteriaMgr,
            ITransportationActBillMgr transportationActBillMgr, IBillAddressMgr billAddressMgr, IEntityPreferenceMgr entityPreferenceMgr)
            : base(entityDao)
        {
            this.transportationRouteMgr = transportationRouteMgr;
            this.userMgr = userMgr;
            this.transportationOrderDetailMgr = transportationOrderDetailMgr;
            this.numberControlMgr = numberControlMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.expenseMgr = expenseMgr;
            this.criteriaMgr = criteriaMgr;
            this.transportationActBillMgr = transportationActBillMgr;
            this.billAddressMgr = billAddressMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public TransportationOrder CreateTransportationOrder(string route, IList<InProcessLocation> ipList, string userCode)
        {
            return CreateTransportationOrder(route, ipList, this.userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public TransportationOrder CreateTransportationOrder(string route, IList<InProcessLocation> ipList, User user)
        {
            if (ipList == null || ipList.Count == 0)
            {
                throw new BusinessErrorException("TransportationOrder.Error.DetailEmpty");
            }

            #region 初始化订单头
            TransportationOrder orderHead = new TransportationOrder();
            orderHead.OrderNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_TRANSPORTATION_ORDER);
            orderHead.TransportationRoute = transportationRouteMgr.LoadTransportationRoute(route);
            orderHead.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
            orderHead.CreateDate = DateTime.Now;
            orderHead.CreateUser = user;
            orderHead.LastModifyUser = user;
            orderHead.LastModifyDate = DateTime.Now;
            #endregion

            #region 创建订单
            CreateTransportationOrder(orderHead);
            #endregion

            #region 创建OrderDetail
            foreach (InProcessLocation ip in ipList)
            {
                TransportationOrderDetail orderDetail = new TransportationOrderDetail();
                orderDetail.InProcessLocation = ip;
                orderDetail.TransportationOrder = orderHead;
                transportationOrderDetailMgr.CreateTransportationOrderDetail(orderDetail);

                //更新ip
                ip.IsReferenced = true;
                inProcessLocationMgr.UpdateInProcessLocation(ip);
            }

            #endregion

            return orderHead;
        }


        [Transaction(TransactionMode.Unspecified)]
        public TransportationOrder LoadTransportationOrder(String orderNo, bool includeDetail)
        {
            TransportationOrder orderHead = this.LoadTransportationOrder(orderNo);

            if (includeDetail && orderHead != null && orderHead.OrderDetails != null && orderHead.OrderDetails.Count > 0)
            {
                foreach (TransportationOrderDetail orderDetail in orderHead.OrderDetails)
                {

                }
            }

            return orderHead;
        }



        [Transaction(TransactionMode.Requires)]
        public TransportationOrder CreateTransportationOrder(string expenseCode, string userCode)
        {
            return CreateTransportationOrder(expenseMgr.LoadExpense(expenseCode), userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public TransportationOrder CreateTransportationOrder(Expense expense, string userCode)
        {
            return CreateTransportationOrder(expense, userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public TransportationOrder CreateTransportationOrder(string expenseCode, User user)
        {
            return CreateTransportationOrder(expenseMgr.LoadExpense(expenseCode), user);
        }

        [Transaction(TransactionMode.Requires)]
        public TransportationOrder CreateTransportationOrder(Expense expense, User user)
        {
            #region 初始化订单头
            TransportationOrder orderHead = new TransportationOrder();
            orderHead.OrderNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_TRANSPORTATION_ORDER);
            orderHead.Carrier = expense.Carrier;
            orderHead.Expense = expense;
            orderHead.Remark = expense.Remark;
            orderHead.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE;
            orderHead.CreateDate = DateTime.Now;
            orderHead.CreateUser = user;
            orderHead.StartDate = DateTime.Now;
            orderHead.StartUser = user;
            orderHead.CompleteDate = DateTime.Now;
            orderHead.CompleteUser = user;
            orderHead.LastModifyUser = user;
            orderHead.LastModifyDate = DateTime.Now;
            orderHead.IsValuated = false;
            orderHead.CarrierBillAddress = billAddressMgr.GetDefaultBillAddress(expense.Carrier.Code);
            if (orderHead.CarrierBillAddress == null)
            {
                throw new BusinessErrorException("Transportation.Error.CarrierPrimaryBillAddressEmpty");
            }
            #endregion

            #region 创建订单
            CreateTransportationOrder(orderHead);
            #endregion

            #region 计费
            ValuateTransportationOrder(orderHead, user);
            #endregion

            return orderHead;
        }



        [Transaction(TransactionMode.Requires)]
        public void StartTransportationOrder(string orderNo, string userCode)
        {
            StartTransportationOrder(this.LoadTransportationOrder(orderNo), userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void StartTransportationOrder(string orderNo, User user)
        {
            StartTransportationOrder(this.LoadTransportationOrder(orderNo), user);
        }


        [Transaction(TransactionMode.Requires)]
        public void StartTransportationOrder(TransportationOrder order, string userCode)
        {
            StartTransportationOrder(order, userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void StartTransportationOrder(TransportationOrder order, User user)
        {
            if (order.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                throw new BusinessErrorException("TransportationOrder.Error.StatusErrorWhenStart", order.Status, order.OrderNo);
            }
            order.StartDate = DateTime.Now;
            order.StartUser = user;
            order.LastModifyDate = DateTime.Now;
            order.LastModifyUser = user;
            order.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS;

            this.UpdateTransportationOrder(order);
        }

        [Transaction(TransactionMode.Requires)]
        public void CompleteTransportationOrder(string orderNo, string userCode)
        {
            CompleteTransportationOrder(this.LoadTransportationOrder(orderNo), userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CompleteTransportationOrder(string orderNo, User user)
        {
            CompleteTransportationOrder(this.LoadTransportationOrder(orderNo), user);
        }


        [Transaction(TransactionMode.Requires)]
        public void CompleteTransportationOrder(TransportationOrder order, string userCode)
        {
            CompleteTransportationOrder(order, userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CompleteTransportationOrder(TransportationOrder order, User user)
        {
            if (order.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
            {
                throw new BusinessErrorException("TransportationOrder.Error.StatusErrorWhenComplete", order.Status, order.OrderNo);
            }
            order.CompleteDate = DateTime.Now;
            order.CompleteUser = user;
            order.LastModifyDate = DateTime.Now;
            order.LastModifyUser = user;
            order.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE;

            this.UpdateTransportationOrder(order);

            #region 完成时自动计价
            bool isAutoValuate = bool.Parse(entityPreferenceMgr.LoadEntityPreference(
                               BusinessConstants.ENTITY_PREFERENCE_CODE_VALUATE_WHEN_COMPLETE).Value);
            if (isAutoValuate && !order.IsValuated)
            {
                ValuateTransportationOrder(order, user);
            }
            #endregion

        }

        [Transaction(TransactionMode.Requires)]
        public void CancelTransportationOrder(string orderNo, string userCode)
        {
            CancelTransportationOrder(this.LoadTransportationOrder(orderNo), userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CancelTransportationOrder(string orderNo, User user)
        {
            CancelTransportationOrder(this.LoadTransportationOrder(orderNo), user);
        }


        [Transaction(TransactionMode.Requires)]
        public void CancelTransportationOrder(TransportationOrder order, string userCode)
        {
            CancelTransportationOrder(order, userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CancelTransportationOrder(TransportationOrder order, User user)
        {
            if (order.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE || order.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
            {
                throw new BusinessErrorException("TransportationOrder.Error.StatusErrorWhenCancel", order.Status, order.OrderNo);
            }

            #region 检查开过票的不能取消
            IList<TransportationActBill> actBillList = transportationActBillMgr.GetTransportationActBill(order);
            var i = (from c in actBillList
                     where c.BilledAmount > 0
                     select c).Count();
            #endregion
            if (i > 0)
            {
                throw new BusinessErrorException("TransportationOrder.Error.BillAmountErrorWhenCancel", order.OrderNo);
            }

            #region 关闭actbill
            if (actBillList != null && actBillList.Count > 0)
            {
                foreach (TransportationActBill actBill in actBillList)
                {
                    actBill.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                    actBill.LastModifyDate = DateTime.Now;
                    actBill.LastModifyUser = user;
                    transportationActBillMgr.UpdateTransportationActBill(actBill);
                }
            }
            #endregion
            #region 删除与asn的关联
            foreach (TransportationOrderDetail orderDetail in order.OrderDetails)
            {
                InProcessLocation ip = orderDetail.InProcessLocation;
                ip.IsReferenced = false;
                inProcessLocationMgr.UpdateInProcessLocation(ip);
            }

            #endregion

            #region 更新运单头
            order.CancelDate = DateTime.Now;
            order.CancelUser = user;
            order.LastModifyDate = DateTime.Now;
            order.LastModifyUser = user;
            order.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL;

            this.UpdateTransportationOrder(order);
            #endregion
        }


        [Transaction(TransactionMode.Requires)]
        public void CloseTransportationOrder(string orderNo, string userCode)
        {
            CloseTransportationOrder(this.LoadTransportationOrder(orderNo), userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CloseTransportationOrder(string orderNo, User user)
        {
            CloseTransportationOrder(this.LoadTransportationOrder(orderNo), user);
        }


        [Transaction(TransactionMode.Requires)]
        public void CloseTransportationOrder(TransportationOrder order, string userCode)
        {
            CloseTransportationOrder(order, userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void CloseTransportationOrder(TransportationOrder order, User user)
        {
            if (order.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
            {
                throw new BusinessErrorException("TransportationOrder.Error.StatusErrorWhenClose", order.Status, order.OrderNo);
            }
            if (!order.IsValuated)
            {
                throw new BusinessErrorException("TransportationOrder.Error.IsNotValuatedErrorWhenClose", order.OrderNo);
            }

            order.CloseDate = DateTime.Now;
            order.CloseUser = user;
            order.LastModifyDate = DateTime.Now;
            order.LastModifyUser = user;
            order.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;

            this.UpdateTransportationOrder(order);
        }

        [Transaction(TransactionMode.Requires)]
        public void TryCloseTransportationOrder()
        {
            IList<TransportationOrder> transportationOrderList = this.GetTransportationOrder(BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE, true);
            foreach (TransportationOrder transportationOrder in transportationOrderList)
            {
                CloseTransportationOrder(transportationOrder, userMgr.LoadUser(BusinessConstants.SYSTEM_USER_MONITOR));
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<TransportationOrder> GetTransportationOrder(string status, bool isValuated)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(TransportationOrder));
            criteria.AddOrder(Order.Desc("CreateDate"));
            if (status != null && status != string.Empty)
            {
                criteria.Add(Expression.Eq("Status", status));
            }
            criteria.Add(Expression.Eq("IsValuated", isValuated));

            return criteriaMgr.FindAll<TransportationOrder>(criteria);
        }


        [Transaction(TransactionMode.Requires)]
        public void ValuateTransportationOrder(string orderNo, string userCode)
        {
            ValuateTransportationOrder(this.LoadTransportationOrder(orderNo), userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void ValuateTransportationOrder(string orderNo, User user)
        {
            ValuateTransportationOrder(this.LoadTransportationOrder(orderNo), user);
        }


        [Transaction(TransactionMode.Requires)]
        public void ValuateTransportationOrder(TransportationOrder order, string userCode)
        {
            ValuateTransportationOrder(order, userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void ValuateTransportationOrder(TransportationOrder order, User user)
        {
            order = this.LoadTransportationOrder(order.OrderNo, true);

            if (order.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                throw new BusinessErrorException("TransportationOrder.Error.StatusErrorWhenValuate", order.Status, order.OrderNo);
            }
            if (order.IsValuated)
            {
                return;
            }

            #region 计费
            transportationActBillMgr.CreateTransportationActBill(order, user);
            #endregion

            #region 更新费用单
            if (order.Expense != null)
            {
                Expense expense = order.Expense;
                expense.IsReferenced = true;
                expense.LastModifyDate = DateTime.Now;
                expense.LastModifyUser = user;
                expenseMgr.UpdateExpense(expense);
            }
            #endregion


            #region 更新运单
            order.LastModifyDate = DateTime.Now;
            order.LastModifyUser = user;
            order.IsValuated = true;
            this.UpdateTransportationOrder(order);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public void ValuateTransportationOrder(IList<TransportationOrder> transportationOrderList, User user)
        {
            foreach (TransportationOrder transportationOrder in transportationOrderList)
            {
                ValuateTransportationOrder(transportationOrder, user);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void TryCompleteTransportationOrder(InProcessLocation ip, User user)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(TransportationOrderDetail));
            criteria.Add(Expression.Eq("InProcessLocation.IpNo", ip.IpNo));
            IList<TransportationOrderDetail> orderDetailList = criteriaMgr.FindAll<TransportationOrderDetail>(criteria);
            if (orderDetailList != null && orderDetailList.Count > 0)
            {
                bool canComplete = true;
                TransportationOrder order = this.LoadTransportationOrder(orderDetailList[0].TransportationOrder.OrderNo);
                foreach (TransportationOrderDetail orderDetail in order.OrderDetails)
                {
                    if (orderDetail.InProcessLocation.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                    {
                        canComplete = false;
                        break;
                    }
                }
                if (order.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS && canComplete)
                {
                    CompleteTransportationOrder(order, user);
                }
            }
        }

        #endregion Customized Methods
    }
}