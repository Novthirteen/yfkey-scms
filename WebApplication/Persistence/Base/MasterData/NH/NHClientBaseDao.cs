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
    public class NHClientBaseDao : NHDaoBase, IClientBaseDao
    {
        public NHClientBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateClient(Client entity)
        {
            Create(entity);
        }

        public virtual IList<Client> GetAllClient()
        {
            return GetAllClient(false);
        }

        public virtual IList<Client> GetAllClient(bool includeInactive)
        {
            string hql = @"from Client entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Client> result = FindAllWithCustomQuery<Client>(hql);
            return result;
        }

        public virtual Client LoadClient(String clientId)
        {
            return FindById<Client>(clientId);
        }

        public virtual void UpdateClient(Client entity)
        {
            Update(entity);
        }

        public virtual void DeleteClient(String clientId)
        {
            string hql = @"from Client entity where entity.ClientId = ?";
            Delete(hql, new object[] { clientId }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteClient(Client entity)
        {
            Delete(entity);
        }

        public virtual void DeleteClient(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Client entity where entity.ClientId in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteClient(IList<Client> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Client entity in entityList)
            {
                pkList.Add(entity.ClientId);
            }

            DeleteClient(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
