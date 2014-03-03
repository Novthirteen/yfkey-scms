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
    public class NHPlannedBillViewBaseDao : NHDaoBase, IPlannedBillViewBaseDao
    {
        public NHPlannedBillViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePlannedBillView(PlannedBillView entity)
        {
            Create(entity);
        }

        public virtual IList<PlannedBillView> GetAllPlannedBillView()
        {
            return FindAll<PlannedBillView>();
        }

        public virtual PlannedBillView LoadPlannedBillView(Int32 id)
        {
            return FindById<PlannedBillView>(id);
        }

        public virtual void UpdatePlannedBillView(PlannedBillView entity)
        {
            Update(entity);
        }

        public virtual void DeletePlannedBillView(Int32 id)
        {
            string hql = @"from PlannedBillView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeletePlannedBillView(PlannedBillView entity)
        {
            Delete(entity);
        }

        public virtual void DeletePlannedBillView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PlannedBillView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePlannedBillView(IList<PlannedBillView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (PlannedBillView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeletePlannedBillView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
