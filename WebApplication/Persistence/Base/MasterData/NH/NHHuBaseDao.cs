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
    public class NHHuBaseDao : NHDaoBase, IHuBaseDao
    {
        public NHHuBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateHu(Hu entity)
        {
            Create(entity);
        }

        public virtual IList<Hu> GetAllHu()
        {
            return FindAll<Hu>();
        }

        public virtual Hu LoadHu(String huId)
        {
            return FindById<Hu>(huId);
        }

        public virtual void UpdateHu(Hu entity)
        {
            Update(entity);
        }

        public virtual void DeleteHu(String huId)
        {
            string hql = @"from Hu entity where entity.HuId = ?";
            Delete(hql, new object[] { huId }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteHu(Hu entity)
        {
            Delete(entity);
        }

        public virtual void DeleteHu(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Hu entity where entity.HuId in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteHu(IList<Hu> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Hu entity in entityList)
            {
                pkList.Add(entity.HuId);
            }

            DeleteHu(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
