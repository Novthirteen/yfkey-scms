using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class OrderBindingMgr : OrderBindingBaseMgr, IOrderBindingMgr
    {
        private ICriteriaMgr criteriaMgr;
        public OrderBindingMgr(IOrderBindingDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public OrderBinding CreateOrderBinding(OrderHead order, Flow bindedFlow, string bindingType)
        {
            OrderBinding orderBinding = new OrderBinding();

            orderBinding.OrderHead = order;
            orderBinding.BindedFlow = bindedFlow;
            orderBinding.BindingType = bindingType;

            this.CreateOrderBinding(orderBinding);

            return orderBinding;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderBinding> GetOrderBinding(string orderNo, params string[] bindingTypes)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderBinding));
            criteria.Add(Expression.Eq("OrderHead.OrderNo", orderNo));
            if (bindingTypes.Length > 0)
            {
                if (bindingTypes.Length == 1)
                {
                    criteria.Add(Expression.Eq("BindingType", bindingTypes[0]));
                }
                else
                {
                    criteria.Add(Expression.In("BindingType", bindingTypes));
                }
            }
            return criteriaMgr.FindAll<OrderBinding>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderBinding> GetOrderBinding(OrderHead orderHead, params string[] bindingTypes)
        {
            return GetOrderBinding(orderHead.OrderNo, bindingTypes);
        }

        #endregion Customized Methods
    }
}