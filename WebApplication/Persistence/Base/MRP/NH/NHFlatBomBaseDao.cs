using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MRP;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MRP.NH
{
    public class NHFlatBomBaseDao : NHDaoBase, IFlatBomBaseDao
    {
        public NHFlatBomBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateFlatBom(FlatBom entity)
        {
            Create(entity);
        }

        public virtual IList<FlatBom> GetAllFlatBom()
        {
            return FindAll<FlatBom>();
        }

        public virtual FlatBom LoadFlatBom(Int32 id)
        {
            return FindById<FlatBom>(id);
        }

        public virtual void UpdateFlatBom(FlatBom entity)
        {
            Update(entity);
        }

        public virtual void DeleteFlatBom(Int32 id)
        {
            string hql = @"from FlatBom entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteFlatBom(FlatBom entity)
        {
            Delete(entity);
        }

        public virtual void DeleteFlatBom(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from FlatBom entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteFlatBom(IList<FlatBom> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (FlatBom entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteFlatBom(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
