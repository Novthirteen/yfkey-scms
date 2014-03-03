using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Procurement.NH
{
    public class NHAutoOrderTrackBaseDao : NHDaoBase, IAutoOrderTrackBaseDao
    {
        public NHAutoOrderTrackBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateAutoOrderTrack(AutoOrderTrack entity)
        {
            Create(entity);
        }

        public virtual IList<AutoOrderTrack> GetAllAutoOrderTrack()
        {
            return FindAll<AutoOrderTrack>();
        }

        public virtual AutoOrderTrack LoadAutoOrderTrack(Int32 id)
        {
            return FindById<AutoOrderTrack>(id);
        }

        public virtual void UpdateAutoOrderTrack(AutoOrderTrack entity)
        {
            Update(entity);
        }

        public virtual void DeleteAutoOrderTrack(Int32 id)
        {
            string hql = @"from AutoOrderTrack entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteAutoOrderTrack(AutoOrderTrack entity)
        {
            Delete(entity);
        }

        public virtual void DeleteAutoOrderTrack(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from AutoOrderTrack entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteAutoOrderTrack(IList<AutoOrderTrack> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (AutoOrderTrack entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteAutoOrderTrack(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
