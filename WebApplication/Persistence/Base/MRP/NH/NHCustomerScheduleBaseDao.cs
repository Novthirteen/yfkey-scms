using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MRP;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MRP.NH
{
    public class NHCustomerScheduleBaseDao : NHDaoBase, ICustomerScheduleBaseDao
    {
        public NHCustomerScheduleBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCustomerSchedule(CustomerSchedule entity)
        {
            Create(entity);
        }

        public virtual IList<CustomerSchedule> GetAllCustomerSchedule()
        {
            return FindAll<CustomerSchedule>();
        }

        public virtual CustomerSchedule LoadCustomerSchedule(Int32 id)
        {
            return FindById<CustomerSchedule>(id);
        }

        public virtual void UpdateCustomerSchedule(CustomerSchedule entity)
        {
            Update(entity);
        }

        public virtual void DeleteCustomerSchedule(Int32 id)
        {
            string hql = @"from CustomerSchedule entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteCustomerSchedule(CustomerSchedule entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCustomerSchedule(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CustomerSchedule entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCustomerSchedule(IList<CustomerSchedule> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (CustomerSchedule entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteCustomerSchedule(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
