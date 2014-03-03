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
    public class NHInProcessLocationBaseDao : NHDaoBase, IInProcessLocationBaseDao
    {
        public NHInProcessLocationBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateInProcessLocation(InProcessLocation entity)
        {
            Create(entity);
        }

        public virtual IList<InProcessLocation> GetAllInProcessLocation()
        {
            return FindAll<InProcessLocation>();
        }

        public virtual InProcessLocation LoadInProcessLocation(String ipNo)
        {
            return FindById<InProcessLocation>(ipNo);
        }

        public virtual void UpdateInProcessLocation(InProcessLocation entity)
        {
            Update(entity);
        }

        public virtual void DeleteInProcessLocation(String ipNo)
        {
            string hql = @"from InProcessLocation entity where entity.IpNo = ?";
            Delete(hql, new object[] { ipNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteInProcessLocation(InProcessLocation entity)
        {
            Delete(entity);
        }

        public virtual void DeleteInProcessLocation(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from InProcessLocation entity where entity.IpNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteInProcessLocation(IList<InProcessLocation> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (InProcessLocation entity in entityList)
            {
                pkList.Add(entity.IpNo);
            }

            DeleteInProcessLocation(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
