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
    public class NHLocationLotDetailBaseDao : NHDaoBase, ILocationLotDetailBaseDao
    {
        public NHLocationLotDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLocationLotDetail(LocationLotDetail entity)
        {
            Create(entity);
        }

        public virtual IList<LocationLotDetail> GetAllLocationLotDetail()
        {
            return FindAll<LocationLotDetail>();
        }

        public virtual LocationLotDetail LoadLocationLotDetail(Int32 id)
        {
            return FindById<LocationLotDetail>(id);
        }

        public virtual void UpdateLocationLotDetail(LocationLotDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteLocationLotDetail(Int32 id)
        {
            string hql = @"from LocationLotDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteLocationLotDetail(LocationLotDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLocationLotDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from LocationLotDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLocationLotDetail(IList<LocationLotDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (LocationLotDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteLocationLotDetail(pkList);
        }

        #endregion Method Created By CodeSmith
    }
}
