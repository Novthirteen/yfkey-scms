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
    public class NHRepackDetailBaseDao : NHDaoBase, IRepackDetailBaseDao
    {
        public NHRepackDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateRepackDetail(RepackDetail entity)
        {
            Create(entity);
        }

        public virtual IList<RepackDetail> GetAllRepackDetail()
        {
            return FindAll<RepackDetail>();
        }

        public virtual RepackDetail LoadRepackDetail(Int32 id)
        {
            return FindById<RepackDetail>(id);
        }

        public virtual void UpdateRepackDetail(RepackDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteRepackDetail(Int32 id)
        {
            string hql = @"from RepackDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteRepackDetail(RepackDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteRepackDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from RepackDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteRepackDetail(IList<RepackDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (RepackDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteRepackDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
