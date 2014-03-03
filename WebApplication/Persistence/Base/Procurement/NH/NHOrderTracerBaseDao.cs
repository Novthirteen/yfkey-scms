using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Procurement.NH
{
    public class NHOrderTracerBaseDao : NHDaoBase, IOrderTracerBaseDao
    {
        public NHOrderTracerBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateOrderTracer(OrderTracer entity)
        {
            Create(entity);
        }

        public virtual IList<OrderTracer> GetAllOrderTracer()
        {
            return FindAll<OrderTracer>();
        }

        public virtual OrderTracer LoadOrderTracer(Int32 id)
        {
            return FindById<OrderTracer>(id);
        }

        public virtual void UpdateOrderTracer(OrderTracer entity)
        {
            Update(entity);
        }

        public virtual void DeleteOrderTracer(Int32 id)
        {
            string hql = @"from OrderTracer entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteOrderTracer(OrderTracer entity)
        {
            Delete(entity);
        }

        public virtual void DeleteOrderTracer(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from OrderTracer entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteOrderTracer(IList<OrderTracer> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (OrderTracer entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteOrderTracer(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
