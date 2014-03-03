using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Dss;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Dss.NH
{
    public class NHDssSystemBaseDao : NHDaoBase, IDssSystemBaseDao
    {
        public NHDssSystemBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateDssSystem(DssSystem entity)
        {
            Create(entity);
        }

        public virtual IList<DssSystem> GetAllDssSystem()
        {
            return FindAll<DssSystem>();
        }

        public virtual DssSystem LoadDssSystem(String code)
        {
            return FindById<DssSystem>(code);
        }

        public virtual void UpdateDssSystem(DssSystem entity)
        {
            Update(entity);
        }

        public virtual void DeleteDssSystem(String code)
        {
            string hql = @"from DssSystem entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteDssSystem(DssSystem entity)
        {
            Delete(entity);
        }

        public virtual void DeleteDssSystem(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DssSystem entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteDssSystem(IList<DssSystem> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (DssSystem entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteDssSystem(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
