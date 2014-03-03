using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Distribution;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Distribution.NH
{
    public class NHCustomerRollingPlanDetailBaseDao : NHDaoBase, ICustomerRollingPlanDetailBaseDao
    {
        public NHCustomerRollingPlanDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCustomerRollingPlanDetail(CustomerRollingPlanDetail entity)
        {
            Create(entity);
        }

        public virtual IList<CustomerRollingPlanDetail> GetAllCustomerRollingPlanDetail()
        {
            return FindAll<CustomerRollingPlanDetail>();
        }

        public virtual CustomerRollingPlanDetail LoadCustomerRollingPlanDetail(Int32 id)
        {
            return FindById<CustomerRollingPlanDetail>(id);
        }

        public virtual void UpdateCustomerRollingPlanDetail(CustomerRollingPlanDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteCustomerRollingPlanDetail(Int32 id)
        {
            string hql = @"from CustomerRollingPlanDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteCustomerRollingPlanDetail(CustomerRollingPlanDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCustomerRollingPlanDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CustomerRollingPlanDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCustomerRollingPlanDetail(IList<CustomerRollingPlanDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (CustomerRollingPlanDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteCustomerRollingPlanDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
