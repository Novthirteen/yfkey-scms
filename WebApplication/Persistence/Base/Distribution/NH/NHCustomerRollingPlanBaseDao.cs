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
    public class NHCustomerRollingPlanBaseDao : NHDaoBase, ICustomerRollingPlanBaseDao
    {
        public NHCustomerRollingPlanBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCustomerRollingPlan(CustomerRollingPlan entity)
        {
            Create(entity);
        }

        public virtual IList<CustomerRollingPlan> GetAllCustomerRollingPlan()
        {
            return FindAll<CustomerRollingPlan>();
        }

        public virtual CustomerRollingPlan LoadCustomerRollingPlan(Int32 id)
        {
            return FindById<CustomerRollingPlan>(id);
        }

        public virtual void UpdateCustomerRollingPlan(CustomerRollingPlan entity)
        {
            Update(entity);
        }

        public virtual void DeleteCustomerRollingPlan(Int32 id)
        {
            string hql = @"from CustomerRollingPlan entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteCustomerRollingPlan(CustomerRollingPlan entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCustomerRollingPlan(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CustomerRollingPlan entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCustomerRollingPlan(IList<CustomerRollingPlan> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (CustomerRollingPlan entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteCustomerRollingPlan(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
