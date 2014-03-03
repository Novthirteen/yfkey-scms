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
    public class NHClientLogBaseDao : NHDaoBase, IClientLogBaseDao
    {
        public NHClientLogBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateClientLog(ClientLog entity)
        {
            Create(entity);
        }

        public virtual IList<ClientLog> GetAllClientLog()
        {
            return FindAll<ClientLog>();
        }

        public virtual ClientLog LoadClientLog(Int32 id)
        {
            return FindById<ClientLog>(id);
        }

        public virtual void UpdateClientLog(ClientLog entity)
        {
            Update(entity);
        }

        public virtual void DeleteClientLog(Int32 id)
        {
            string hql = @"from ClientLog entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteClientLog(ClientLog entity)
        {
            Delete(entity);
        }

        public virtual void DeleteClientLog(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ClientLog entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteClientLog(IList<ClientLog> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ClientLog entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteClientLog(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
