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
    public class NHLocationDetailBaseDao : NHDaoBase, ILocationDetailBaseDao
    {
        public NHLocationDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLocationDetail(LocationDetail entity)
        {
            Create(entity);
        }

        public virtual IList<LocationDetail> GetAllLocationDetail()
        {
            return FindAll<LocationDetail>();
        }

        public virtual LocationDetail LoadLocationDetail(Int32 id)
        {
            return FindById<LocationDetail>(id);
        }

        public virtual void UpdateLocationDetail(LocationDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteLocationDetail(Int32 id)
        {
            string hql = @"from LocationDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteLocationDetail(LocationDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLocationDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from LocationDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLocationDetail(IList<LocationDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (LocationDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteLocationDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
