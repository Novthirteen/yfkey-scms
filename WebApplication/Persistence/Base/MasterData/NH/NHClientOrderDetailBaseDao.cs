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
    public class NHClientOrderDetailBaseDao : NHDaoBase, IClientOrderDetailBaseDao
    {
        public NHClientOrderDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateClientOrderDetail(ClientOrderDetail entity)
        {
            Create(entity);
        }

        public virtual IList<ClientOrderDetail> GetAllClientOrderDetail()
        {
            return FindAll<ClientOrderDetail>();
        }

        public virtual ClientOrderDetail LoadClientOrderDetail(Int32 id)
        {
            return FindById<ClientOrderDetail>(id);
        }

        public virtual void UpdateClientOrderDetail(ClientOrderDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteClientOrderDetail(Int32 id)
        {
            string hql = @"from ClientOrderDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteClientOrderDetail(ClientOrderDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteClientOrderDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ClientOrderDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteClientOrderDetail(IList<ClientOrderDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ClientOrderDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteClientOrderDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
