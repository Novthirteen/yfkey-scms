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
    public class NHLeanEngineViewBaseDao : NHDaoBase, ILeanEngineViewBaseDao
    {
        public NHLeanEngineViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLeanEngineView(LeanEngineView entity)
        {
            Create(entity);
        }

        public virtual IList<LeanEngineView> GetAllLeanEngineView()
        {
            return FindAll<LeanEngineView>();
        }

        public virtual LeanEngineView LoadLeanEngineView(Int32 flowDetId)
        {
            return FindById<LeanEngineView>(flowDetId);
        }

        public virtual void UpdateLeanEngineView(LeanEngineView entity)
        {
            Update(entity);
        }

        public virtual void DeleteLeanEngineView(Int32 flowDetId)
        {
            string hql = @"from LeanEngineView entity where entity.FlowDetId = ?";
            Delete(hql, new object[] { flowDetId }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteLeanEngineView(LeanEngineView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLeanEngineView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from LeanEngineView entity where entity.FlowDetId in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLeanEngineView(IList<LeanEngineView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (LeanEngineView entity in entityList)
            {
                pkList.Add(entity.FlowDetId);
            }

            DeleteLeanEngineView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
