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
    public class NHCustomerScheduleDetailBaseDao : NHDaoBase, ICustomerScheduleDetailBaseDao
    {
        public NHCustomerScheduleDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCustomerScheduleDetail(CustomerScheduleDetail entity)
        {
            Create(entity);
        }

        public virtual IList<CustomerScheduleDetail> GetAllCustomerScheduleDetail()
        {
            return FindAll<CustomerScheduleDetail>();
        }

        public virtual CustomerScheduleDetail LoadCustomerScheduleDetail(Int32 id)
        {
            return FindById<CustomerScheduleDetail>(id);
        }

        public virtual void UpdateCustomerScheduleDetail(CustomerScheduleDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteCustomerScheduleDetail(Int32 id)
        {
            string hql = @"from CustomerScheduleDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteCustomerScheduleDetail(CustomerScheduleDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCustomerScheduleDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CustomerScheduleDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCustomerScheduleDetail(IList<CustomerScheduleDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (CustomerScheduleDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteCustomerScheduleDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
