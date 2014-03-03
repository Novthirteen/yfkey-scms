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
    public class NHLocationBinDetailBaseDao : NHDaoBase, ILocationBinDetailBaseDao
    {
        public NHLocationBinDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLocationBinDetail(LocationBinDetail entity)
        {
            Create(entity);
        }

        public virtual IList<LocationBinDetail> GetAllLocationBinDetail()
        {
            return FindAll<LocationBinDetail>();
        }

        public virtual LocationBinDetail LoadLocationBinDetail(Int32 id)
        {
            return FindById<LocationBinDetail>(id);
        }

        public virtual void UpdateLocationBinDetail(LocationBinDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteLocationBinDetail(Int32 id)
        {
            string hql = @"from LocationBinDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteLocationBinDetail(LocationBinDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLocationBinDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from LocationBinDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLocationBinDetail(IList<LocationBinDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (LocationBinDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteLocationBinDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
