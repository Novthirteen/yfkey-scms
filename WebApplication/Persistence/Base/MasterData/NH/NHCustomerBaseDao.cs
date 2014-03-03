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
    public class NHCustomerBaseDao : NHDaoBase, ICustomerBaseDao
    {
        public NHCustomerBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCustomer(Customer entity)
        {
            Create(entity);
        }

        public virtual IList<Customer> GetAllCustomer()
        {
            return GetAllCustomer(false);
        }

        public virtual IList<Customer> GetAllCustomer(bool includeInactive)
        {
            string hql = @"from Customer entity ";
            if (!includeInactive)
            {
                hql += "where entity.IsActive = 1";
            }
            IList<Customer> result = FindAllWithCustomQuery<Customer>(hql);
            return result;
        }

        public virtual Customer LoadCustomer(String code)
        {
            return FindById<Customer>(code);
        }

        public virtual void UpdateCustomer(Customer entity)
        {
            Update(entity);
        }

        public virtual void DeleteCustomer(String code)
        {
            string hql = @"from Customer entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteCustomer(Customer entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCustomer(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Customer entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCustomer(IList<Customer> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Customer entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteCustomer(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
