using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Distribution;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Distribution.NH
{
    public class NHInProcessLocationTrackBaseDao : NHDaoBase, IInProcessLocationTrackBaseDao
    {
        public NHInProcessLocationTrackBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateInProcessLocationTrack(InProcessLocationTrack entity)
        {
            Create(entity);
        }

        public virtual IList<InProcessLocationTrack> GetAllInProcessLocationTrack()
        {
            return FindAll<InProcessLocationTrack>();
        }

        public virtual InProcessLocationTrack LoadInProcessLocationTrack(Int32 id)
        {
            return FindById<InProcessLocationTrack>(id);
        }

        public virtual void UpdateInProcessLocationTrack(InProcessLocationTrack entity)
        {
            Update(entity);
        }

        public virtual void DeleteInProcessLocationTrack(Int32 id)
        {
            string hql = @"from InProcessLocationTrack entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteInProcessLocationTrack(InProcessLocationTrack entity)
        {
            Delete(entity);
        }

        public virtual void DeleteInProcessLocationTrack(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from InProcessLocationTrack entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteInProcessLocationTrack(IList<InProcessLocationTrack> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (InProcessLocationTrack entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteInProcessLocationTrack(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
