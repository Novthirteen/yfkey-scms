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
    public class NHMesBomBaseDao : NHDaoBase, IMesBomBaseDao
    {
        public NHMesBomBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBom(MesBom entity)
        {
            Create(entity);
        }

        public virtual IList<MesBom> GetAllBom()
        {
            return GetAllBom(false);
        }

        public virtual IList<MesBom> GetAllBom(bool includeInactive)
        {
            string hql = @"from MesBom entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<MesBom> result = FindAllWithCustomQuery<MesBom>(hql);
            return result;
        }

        public virtual MesBom LoadBom(String code)
        {
            return FindById<MesBom>(code);
        }

        public virtual void UpdateBom(MesBom entity)
        {
            Update(entity);
        }

        public virtual void DeleteBom(String code)
        {
            string hql = @"from MesBom entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteBom(MesBom entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBom(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from MesBom entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBom(IList<MesBom> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (MesBom entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteBom(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
