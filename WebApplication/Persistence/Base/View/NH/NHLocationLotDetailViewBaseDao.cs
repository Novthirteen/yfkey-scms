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
    public class NHLocationLotDetailViewBaseDao : NHDaoBase, ILocationLotDetailViewBaseDao
    {
        public NHLocationLotDetailViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLocationLotDetailView(LocationLotDetailView entity)
        {
            Create(entity);
        }

        public virtual IList<LocationLotDetailView> GetAllLocationLotDetailView()
        {
            return FindAll<LocationLotDetailView>();
        }

        public virtual LocationLotDetailView LoadLocationLotDetailView(Int32 id)
        {
            return FindById<LocationLotDetailView>(id);
        }

        public virtual void UpdateLocationLotDetailView(LocationLotDetailView entity)
        {
            Update(entity);
        }

        public virtual void DeleteLocationLotDetailView(Int32 id)
        {
            string hql = @"from LocationLotDetailView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteLocationLotDetailView(LocationLotDetailView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLocationLotDetailView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from LocationLotDetailView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLocationLotDetailView(IList<LocationLotDetailView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (LocationLotDetailView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteLocationLotDetailView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
