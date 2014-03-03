using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MasterData.NH
{
    public class NHOrderBindingBaseDao : NHDaoBase, IOrderBindingBaseDao
    {
        public NHOrderBindingBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateOrderBinding(OrderBinding entity)
        {
            Create(entity);
        }

        public virtual IList<OrderBinding> GetAllOrderBinding()
        {
            return FindAll<OrderBinding>();
        }

        public virtual OrderBinding LoadOrderBinding(Int32 id)
        {
            return FindById<OrderBinding>(id);
        }

        public virtual void UpdateOrderBinding(OrderBinding entity)
        {
            Update(entity);
        }

        public virtual void DeleteOrderBinding(Int32 id)
        {
            string hql = @"from OrderBinding entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteOrderBinding(OrderBinding entity)
        {
            Delete(entity);
        }

        public virtual void DeleteOrderBinding(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from OrderBinding entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteOrderBinding(IList<OrderBinding> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (OrderBinding entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteOrderBinding(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
