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
    public class NHOrderDetailBaseDao : NHDaoBase, IOrderDetailBaseDao
    {
        public NHOrderDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateOrderDetail(OrderDetail entity)
        {
            Create(entity);
        }

        public virtual IList<OrderDetail> GetAllOrderDetail()
        {
            return FindAll<OrderDetail>();
        }

        public virtual OrderDetail LoadOrderDetail(Int32 id)
        {
            return FindById<OrderDetail>(id);
        }

        public virtual void UpdateOrderDetail(OrderDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteOrderDetail(Int32 id)
        {
            string hql = @"from OrderDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteOrderDetail(OrderDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteOrderDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from OrderDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteOrderDetail(IList<OrderDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (OrderDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteOrderDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
