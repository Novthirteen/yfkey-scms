using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.View;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.View;
using com.Sconit.Utility;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View.Impl
{
    [Transactional]
    public class OrderDetailViewMgr : OrderDetailViewBaseMgr, IOrderDetailViewMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IOrderLocTransViewMgr orderLocTransViewMgr;
        public OrderDetailViewMgr(IOrderDetailViewDao entityDao,
            ICriteriaMgr criteriaMgr,
            IOrderLocTransViewMgr orderLocTransViewMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.orderLocTransViewMgr = orderLocTransViewMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderDetailView> GetProdIO(string flow, string region, string startDate, string endDate, string item, string userCode, int pageSize, int pageIndex)
        {
            IList<OrderDetailView> baseList = this.GetProdIOBaseList(flow, region, startDate, endDate, item, userCode);

            IList<OrderDetailView> list = new List<OrderDetailView>();
            if (baseList != null && baseList.Count > 0)
            {
                int startRow = GridViewHelper.GetStartRow(pageSize, pageIndex);
                int endRow = GridViewHelper.GetEndRow(pageSize, pageIndex, baseList.Count);
                for (int i = startRow; i < endRow; i++)
                {
                    list.Add(baseList[i]);
                }
            }

            foreach (OrderDetailView orderDetailView in list)
            {
                orderDetailView.OutList = orderLocTransViewMgr.GetProdIODataList(flow, region, startDate, endDate, orderDetailView.Item.Code, userCode, BusinessConstants.IO_TYPE_OUT);
            }

            return list;
        }

        [Transaction(TransactionMode.Unspecified)]
        public int GetProdIOCount(string flow, string region, string startDate, string endDate, string item, string userCode)
        {
            IList<OrderDetailView> result = this.GetProdIOBaseList(flow, region, startDate, endDate, item, userCode);

            return result.Count;
        }

        #endregion Customized Methods

        #region Private Method
        [Transaction(TransactionMode.Unspecified)]
        private IList<OrderDetailView> GetProdIOBaseList(string flow, string region, string startDate, string endDate, string item, string userCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderDetailView));
            criteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
            //区域权限
            SecurityHelper.SetRegionSearchCriteria(criteria, "PartyTo.Code", userCode);
            //订单状态
            OrderHelper.SetActiveOrderStatusCriteria(criteria, "Status");

            if (flow != string.Empty)
            {
                criteria.Add(Expression.Eq("Flow", flow));
            }
            if (region != string.Empty)
            {
                criteria.Add(Expression.Eq("PartyTo.Code", region));
            }
            if (startDate != string.Empty)
            {
                criteria.Add(Expression.Ge("EffDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                criteria.Add(Expression.Lt("EffDate", DateTime.Parse(endDate).AddDays(1)));
            }
            if (item != string.Empty)
            {
                criteria.Add(Expression.Like("Item.Code", item, MatchMode.Start));
            }

            criteria.AddOrder(Order.Asc("Item.Code"));
            criteria.SetProjection(Projections.ProjectionList()
                .Add(Projections.GroupProperty("Item"))
                .Add(Projections.GroupProperty("Uom"))
                .Add(Projections.Sum("ReceivedQty")));
            IList result = criteriaMgr.FindAll(criteria);

            return this.ConvertToList(result);
        }

        private IList<OrderDetailView> ConvertToList(IList list)
        {
            IList<OrderDetailView> orderDetailViewList = new List<OrderDetailView>();
            if (list != null && list.Count > 0)
            {
                foreach (object obj in list)
                {
                    OrderDetailView orderDetailView = new OrderDetailView();
                    orderDetailView.Item = (Item)((object[])obj)[0];
                    orderDetailView.Uom = (string)((object[])obj)[1];
                    orderDetailView.ReceivedQty = (decimal)((object[])obj)[2];
                    orderDetailViewList.Add(orderDetailView);
                }
            }

            return orderDetailViewList;
        }

        #endregion
    }
}