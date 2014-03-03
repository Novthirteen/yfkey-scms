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
    public class NHEmployeeBaseDao : NHDaoBase, IEmployeeBaseDao
    {
        public NHEmployeeBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateEmployee(Employee entity)
        {
            Create(entity);
        }

        public virtual IList<Employee> GetAllEmployee()
        {
            return FindAll<Employee>();
        }

        public virtual Employee LoadEmployee(String code)
        {
            return FindById<Employee>(code);
        }

        public virtual void UpdateEmployee(Employee entity)
        {
            Update(entity);
        }

        public virtual void DeleteEmployee(String code)
        {
            string hql = @"from Employee entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteEmployee(Employee entity)
        {
            Delete(entity);
        }

        public virtual void DeleteEmployee(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Employee entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteEmployee(IList<Employee> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Employee entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteEmployee(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
