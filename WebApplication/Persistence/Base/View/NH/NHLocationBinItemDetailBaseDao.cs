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
    public class NHLocationBinItemDetailBaseDao : NHDaoBase, ILocationBinItemDetailBaseDao
    {
        public NHLocationBinItemDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLocationBinItemDetail(LocationBinItemDetail entity)
        {
            Create(entity);
        }

        public virtual IList<LocationBinItemDetail> GetAllLocationBinItemDetail()
        {
            return FindAll<LocationBinItemDetail>();
        }

        public virtual LocationBinItemDetail LoadLocationBinItemDetail(Int32 id)
        {
            return FindById<LocationBinItemDetail>(id);
        }

        public virtual void UpdateLocationBinItemDetail(LocationBinItemDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteLocationBinItemDetail(Int32 id)
        {
            string hql = @"from LocationBinItemDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteLocationBinItemDetail(LocationBinItemDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLocationBinItemDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from LocationBinItemDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLocationBinItemDetail(IList<LocationBinItemDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (LocationBinItemDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteLocationBinItemDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
