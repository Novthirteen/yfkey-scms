using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.View;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.View.NH
{
    public class NHLocationBinBaseDao : NHDaoBase, ILocationBinBaseDao
    {
        public NHLocationBinBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLocationBin(LocationBin entity)
        {
            Create(entity);
        }

        public virtual IList<LocationBin> GetAllLocationBin()
        {
            return FindAll<LocationBin>();
        }

        public virtual LocationBin LoadLocationBin(Int32 id)
        {
            return FindById<LocationBin>(id);
        }

        public virtual void UpdateLocationBin(LocationBin entity)
        {
            Update(entity);
        }

        public virtual void DeleteLocationBin(Int32 id)
        {
            string hql = @"from LocationBin entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteLocationBin(LocationBin entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLocationBin(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from LocationBin entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLocationBin(IList<LocationBin> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (LocationBin entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteLocationBin(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
