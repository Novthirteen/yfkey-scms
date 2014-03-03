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
    public class NHDssFtpControlBaseDao : NHDaoBase, IDssFtpControlBaseDao
    {
        public NHDssFtpControlBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateDssFtpControl(DssFtpControl entity)
        {
            Create(entity);
        }

        public virtual IList<DssFtpControl> GetAllDssFtpControl()
        {
            return FindAll<DssFtpControl>();
        }

        public virtual DssFtpControl LoadDssFtpControl(Int32 id)
        {
            return FindById<DssFtpControl>(id);
        }

        public virtual void UpdateDssFtpControl(DssFtpControl entity)
        {
            Update(entity);
        }

        public virtual void DeleteDssFtpControl(Int32 id)
        {
            string hql = @"from DssFtpControl entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteDssFtpControl(DssFtpControl entity)
        {
            Delete(entity);
        }

        public virtual void DeleteDssFtpControl(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DssFtpControl entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteDssFtpControl(IList<DssFtpControl> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (DssFtpControl entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteDssFtpControl(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
