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
    public class NHPlannedBillBaseDao : NHDaoBase, IPlannedBillBaseDao
    {
        public NHPlannedBillBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePlannedBill(PlannedBill entity)
        {
            Create(entity);
        }

        public virtual IList<PlannedBill> GetAllPlannedBill()
        {
            return FindAll<PlannedBill>();
        }

        public virtual PlannedBill LoadPlannedBill(Int32 id)
        {
            return FindById<PlannedBill>(id);
        }

        public virtual void UpdatePlannedBill(PlannedBill entity)
        {
            Update(entity);
        }

        public virtual void DeletePlannedBill(Int32 id)
        {
            string hql = @"from PlannedBill entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeletePlannedBill(PlannedBill entity)
        {
            Delete(entity);
        }

        public virtual void DeletePlannedBill(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PlannedBill entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePlannedBill(IList<PlannedBill> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (PlannedBill entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeletePlannedBill(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
