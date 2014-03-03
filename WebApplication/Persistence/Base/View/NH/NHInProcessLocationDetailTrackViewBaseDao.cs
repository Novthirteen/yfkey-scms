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
    public class NHInProcessLocationDetailTrackViewBaseDao : NHDaoBase, IInProcessLocationDetailTrackViewBaseDao
    {
        public NHInProcessLocationDetailTrackViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateInProcessLocationDetailTrackView(InProcessLocationDetailTrackView entity)
        {
            Create(entity);
        }

        public virtual IList<InProcessLocationDetailTrackView> GetAllInProcessLocationDetailTrackView()
        {
            return FindAll<InProcessLocationDetailTrackView>();
        }

        public virtual InProcessLocationDetailTrackView LoadInProcessLocationDetailTrackView(Int32 id)
        {
            return FindById<InProcessLocationDetailTrackView>(id);
        }

        public virtual void UpdateInProcessLocationDetailTrackView(InProcessLocationDetailTrackView entity)
        {
            Update(entity);
        }

        public virtual void DeleteInProcessLocationDetailTrackView(Int32 id)
        {
            string hql = @"from InProcessLocationDetailTrackView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteInProcessLocationDetailTrackView(InProcessLocationDetailTrackView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteInProcessLocationDetailTrackView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from InProcessLocationDetailTrackView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteInProcessLocationDetailTrackView(IList<InProcessLocationDetailTrackView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (InProcessLocationDetailTrackView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteInProcessLocationDetailTrackView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
