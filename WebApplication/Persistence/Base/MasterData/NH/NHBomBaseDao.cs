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
    public class NHBomBaseDao : NHDaoBase, IBomBaseDao
    {
        public NHBomBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBom(Bom entity)
        {
            Create(entity);
        }

        public virtual IList<Bom> GetAllBom()
        {
            return GetAllBom(false);
        }

        public virtual IList<Bom> GetAllBom(bool includeInactive)
        {
            string hql = @"from Bom entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Bom> result = FindAllWithCustomQuery<Bom>(hql);
            return result;
        }

        public virtual Bom LoadBom(String code)
        {
            return FindById<Bom>(code);
        }

        public virtual void UpdateBom(Bom entity)
        {
            Update(entity);
        }

        public virtual void DeleteBom(String code)
        {
            string hql = @"from Bom entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteBom(Bom entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBom(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Bom entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBom(IList<Bom> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Bom entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteBom(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
