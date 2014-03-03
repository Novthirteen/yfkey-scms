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
    public class NHByMaterialBaseDao : NHDaoBase, IByMaterialBaseDao
    {
        public NHByMaterialBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateByMaterial(ByMaterial entity)
        {
            Create(entity);
        }

        public virtual IList<ByMaterial> GetAllByMaterial()
        {
            return FindAll<ByMaterial>();
        }

        public virtual ByMaterial LoadByMaterial(Int32 id)
        {
            return FindById<ByMaterial>(id);
        }

        public virtual void UpdateByMaterial(ByMaterial entity)
        {
            Update(entity);
        }

        public virtual void DeleteByMaterial(Int32 id)
        {
            string hql = @"from ByMaterial entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteByMaterial(ByMaterial entity)
        {
            Delete(entity);
        }

        public virtual void DeleteByMaterial(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ByMaterial entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteByMaterial(IList<ByMaterial> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ByMaterial entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteByMaterial(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
