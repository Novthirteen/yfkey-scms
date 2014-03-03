using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.View;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.View.NH
{
    public class NHOrderLocTransViewBaseDao : NHDaoBase, IOrderLocTransViewBaseDao
    {
        public NHOrderLocTransViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateOrderLocTransView(OrderLocTransView entity)
        {
            Create(entity);
        }

        public virtual IList<OrderLocTransView> GetAllOrderLocTransView()
        {
            return FindAll<OrderLocTransView>();
        }

        public virtual OrderLocTransView LoadOrderLocTransView(Int32 id)
        {
            return FindById<OrderLocTransView>(id);
        }

        public virtual void UpdateOrderLocTransView(OrderLocTransView entity)
        {
            Update(entity);
        }

        public virtual void DeleteOrderLocTransView(Int32 id)
        {
            string hql = @"from OrderLocTransView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteOrderLocTransView(OrderLocTransView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteOrderLocTransView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from OrderLocTransView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteOrderLocTransView(IList<OrderLocTransView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (OrderLocTransView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteOrderLocTransView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
