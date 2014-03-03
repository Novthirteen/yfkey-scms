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
    public class NHClientOrderHeadBaseDao : NHDaoBase, IClientOrderHeadBaseDao
    {
        public NHClientOrderHeadBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateClientOrderHead(ClientOrderHead entity)
        {
            Create(entity);
        }

        public virtual IList<ClientOrderHead> GetAllClientOrderHead()
        {
            return FindAll<ClientOrderHead>();
        }

        public virtual ClientOrderHead LoadClientOrderHead(String id)
        {
            return FindById<ClientOrderHead>(id);
        }

        public virtual void UpdateClientOrderHead(ClientOrderHead entity)
        {
            Update(entity);
        }

        public virtual void DeleteClientOrderHead(String id)
        {
            string hql = @"from ClientOrderHead entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteClientOrderHead(ClientOrderHead entity)
        {
            Delete(entity);
        }

        public virtual void DeleteClientOrderHead(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ClientOrderHead entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteClientOrderHead(IList<ClientOrderHead> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (ClientOrderHead entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteClientOrderHead(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
