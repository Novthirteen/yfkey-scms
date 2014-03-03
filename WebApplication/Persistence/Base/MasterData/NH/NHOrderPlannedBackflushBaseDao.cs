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
    public class NHOrderPlannedBackflushBaseDao : NHDaoBase, IOrderPlannedBackflushBaseDao
    {
        public NHOrderPlannedBackflushBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateOrderPlannedBackflush(OrderPlannedBackflush entity)
        {
            Create(entity);
        }

        public virtual IList<OrderPlannedBackflush> GetAllOrderPlannedBackflush()
        {
            return FindAll<OrderPlannedBackflush>();
        }

        public virtual OrderPlannedBackflush LoadOrderPlannedBackflush(Int32 id)
        {
            return FindById<OrderPlannedBackflush>(id);
        }

        public virtual void UpdateOrderPlannedBackflush(OrderPlannedBackflush entity)
        {
            Update(entity);
        }

        public virtual void DeleteOrderPlannedBackflush(Int32 id)
        {
            string hql = @"from OrderPlannedBackflush entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteOrderPlannedBackflush(OrderPlannedBackflush entity)
        {
            Delete(entity);
        }

        public virtual void DeleteOrderPlannedBackflush(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from OrderPlannedBackflush entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteOrderPlannedBackflush(IList<OrderPlannedBackflush> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (OrderPlannedBackflush entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteOrderPlannedBackflush(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
