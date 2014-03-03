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
    public class NHClientMonitorBaseDao : NHDaoBase, IClientMonitorBaseDao
    {
        public NHClientMonitorBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateClientMonitor(ClientMonitor entity)
        {
            Create(entity);
        }

        public virtual IList<ClientMonitor> GetAllClientMonitor()
        {
            return FindAll<ClientMonitor>();
        }

        public virtual ClientMonitor LoadClientMonitor(Int32 id)
        {
            return FindById<ClientMonitor>(id);
        }

        public virtual void UpdateClientMonitor(ClientMonitor entity)
        {
            Update(entity);
        }

        public virtual void DeleteClientMonitor(Int32 id)
        {
            string hql = @"from ClientMonitor entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteClientMonitor(ClientMonitor entity)
        {
            Delete(entity);
        }

        public virtual void DeleteClientMonitor(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ClientMonitor entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteClientMonitor(IList<ClientMonitor> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ClientMonitor entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteClientMonitor(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
