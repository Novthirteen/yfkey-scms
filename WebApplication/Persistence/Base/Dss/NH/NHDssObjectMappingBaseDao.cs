using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Dss;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Dss.NH
{
    public class NHDssObjectMappingBaseDao : NHDaoBase, IDssObjectMappingBaseDao
    {
        public NHDssObjectMappingBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateDssObjectMapping(DssObjectMapping entity)
        {
            Create(entity);
        }

        public virtual IList<DssObjectMapping> GetAllDssObjectMapping()
        {
            return FindAll<DssObjectMapping>();
        }

        public virtual DssObjectMapping LoadDssObjectMapping(Int32 id)
        {
            return FindById<DssObjectMapping>(id);
        }

        public virtual void UpdateDssObjectMapping(DssObjectMapping entity)
        {
            Update(entity);
        }

        public virtual void DeleteDssObjectMapping(Int32 id)
        {
            string hql = @"from DssObjectMapping entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteDssObjectMapping(DssObjectMapping entity)
        {
            Delete(entity);
        }

        public virtual void DeleteDssObjectMapping(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DssObjectMapping entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteDssObjectMapping(IList<DssObjectMapping> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (DssObjectMapping entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteDssObjectMapping(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
