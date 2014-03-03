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
    public class NHActingBillViewBaseDao : NHDaoBase, IActingBillViewBaseDao
    {
        public NHActingBillViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateActingBillView(ActingBillView entity)
        {
            Create(entity);
        }

        public virtual IList<ActingBillView> GetAllActingBillView()
        {
            return FindAll<ActingBillView>();
        }

        public virtual ActingBillView LoadActingBillView(Int32 id)
        {
            return FindById<ActingBillView>(id);
        }

        public virtual void UpdateActingBillView(ActingBillView entity)
        {
            Update(entity);
        }

        public virtual void DeleteActingBillView(Int32 id)
        {
            string hql = @"from ActingBillView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteActingBillView(ActingBillView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteActingBillView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ActingBillView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteActingBillView(IList<ActingBillView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ActingBillView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteActingBillView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
