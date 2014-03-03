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
    public class NHLocationTransactionBaseDao : NHDaoBase, ILocationTransactionBaseDao
    {
        public NHLocationTransactionBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLocationTransaction(LocationTransaction entity)
        {
            Create(entity);
        }

        public virtual IList<LocationTransaction> GetAllLocationTransaction()
        {
            return FindAll<LocationTransaction>();
        }

        public virtual LocationTransaction LoadLocationTransaction(Int32 id)
        {
            return FindById<LocationTransaction>(id);
        }

        public virtual void UpdateLocationTransaction(LocationTransaction entity)
        {
            Update(entity);
        }

        public virtual void DeleteLocationTransaction(Int32 id)
        {
            string hql = @"from LocationTransaction entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteLocationTransaction(LocationTransaction entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLocationTransaction(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from LocationTransaction entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLocationTransaction(IList<LocationTransaction> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (LocationTransaction entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteLocationTransaction(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
