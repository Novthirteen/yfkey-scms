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
    public class NHOrderHeadBaseDao : NHDaoBase, IOrderHeadBaseDao
    {
        public NHOrderHeadBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateOrderHead(OrderHead entity)
        {
            Create(entity);
        }

        public virtual IList<OrderHead> GetAllOrderHead()
        {
            return FindAll<OrderHead>();
        }

        public virtual OrderHead LoadOrderHead(String orderNo)
        {
            return FindById<OrderHead>(orderNo);
        }

        public virtual void UpdateOrderHead(OrderHead entity)
        {
            Update(entity);
        }

        public virtual void DeleteOrderHead(String orderNo)
        {
            string hql = @"from OrderHead entity where entity.OrderNo = ?";
            Delete(hql, new object[] { orderNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteOrderHead(OrderHead entity)
        {
            Delete(entity);
        }

        public virtual void DeleteOrderHead(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from OrderHead entity where entity.OrderNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteOrderHead(IList<OrderHead> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (OrderHead entity in entityList)
            {
                pkList.Add(entity.OrderNo);
            }

            DeleteOrderHead(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
