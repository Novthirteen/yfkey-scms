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
    public class NHOrderOperationBaseDao : NHDaoBase, IOrderOperationBaseDao
    {
        public NHOrderOperationBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateOrderOperation(OrderOperation entity)
        {
            Create(entity);
        }

        public virtual IList<OrderOperation> GetAllOrderOperation()
        {
            return FindAll<OrderOperation>();
        }

        public virtual OrderOperation LoadOrderOperation(Int32 id)
        {
            return FindById<OrderOperation>(id);
        }

        public virtual void UpdateOrderOperation(OrderOperation entity)
        {
            Update(entity);
        }

        public virtual void DeleteOrderOperation(Int32 id)
        {
            string hql = @"from OrderOperation entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteOrderOperation(OrderOperation entity)
        {
            Delete(entity);
        }

        public virtual void DeleteOrderOperation(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from OrderOperation entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteOrderOperation(IList<OrderOperation> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (OrderOperation entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteOrderOperation(pkList);
        }


        public virtual OrderOperation LoadOrderOperation(com.Sconit.Entity.MasterData.OrderHead orderHead, Int32 operation)
        {
            string hql = @"from OrderOperation entity where entity.OrderHead.OrderNo = ? and entity.Operation = ?";
            IList<OrderOperation> result = FindAllWithCustomQuery<OrderOperation>(hql, new object[] { orderHead.OrderNo, operation }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeleteOrderOperation(String orderHeadOrderNo, Int32 operation)
        {
            string hql = @"from OrderOperation entity where entity.OrderHead.OrderNo = ? and entity.Operation = ?";
            Delete(hql, new object[] { orderHeadOrderNo, operation }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 });
        }

        public virtual OrderOperation LoadOrderOperation(String orderHeadOrderNo, Int32 operation)
        {
            string hql = @"from OrderOperation entity where entity.OrderHead.OrderNo = ? and entity.Operation = ?";
            IList<OrderOperation> result = FindAllWithCustomQuery<OrderOperation>(hql, new object[] { orderHeadOrderNo, operation }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        #endregion Method Created By CodeSmith
    }
}
