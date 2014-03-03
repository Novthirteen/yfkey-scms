using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Mes;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Mes.NH
{
    public class NHMesBomDetailBaseDao : NHDaoBase, IMesBomDetailBaseDao
    {
        public NHMesBomDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBomDetail(MesBomDetail entity)
        {
            Create(entity);
        }

        public virtual IList<MesBomDetail> GetAllBomDetail()
        {
            return FindAll<MesBomDetail>();
        }

        public virtual MesBomDetail LoadBomDetail(Int32 id)
        {
            return FindById<MesBomDetail>(id);
        }

        public virtual void UpdateBomDetail(MesBomDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteBomDetail(Int32 id)
        {
            string hql = @"from MesBomDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBomDetail(MesBomDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBomDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from MesBomDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBomDetail(IList<MesBomDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (MesBomDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBomDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
