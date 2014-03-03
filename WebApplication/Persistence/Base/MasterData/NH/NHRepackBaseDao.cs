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
    public class NHRepackBaseDao : NHDaoBase, IRepackBaseDao
    {
        public NHRepackBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateRepack(Repack entity)
        {
            Create(entity);
        }

        public virtual IList<Repack> GetAllRepack()
        {
            return FindAll<Repack>();
        }

        public virtual Repack LoadRepack(String repackNo)
        {
            return FindById<Repack>(repackNo);
        }

        public virtual void UpdateRepack(Repack entity)
        {
            Update(entity);
        }

        public virtual void DeleteRepack(String repackNo)
        {
            string hql = @"from Repack entity where entity.RepackNo = ?";
            Delete(hql, new object[] { repackNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteRepack(Repack entity)
        {
            Delete(entity);
        }

        public virtual void DeleteRepack(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Repack entity where entity.RepackNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteRepack(IList<Repack> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Repack entity in entityList)
            {
                pkList.Add(entity.RepackNo);
            }

            DeleteRepack(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
