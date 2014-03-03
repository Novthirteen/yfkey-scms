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
    public class NHDssImportHistoryBaseDao : NHDaoBase, IDssImportHistoryBaseDao
    {
        public NHDssImportHistoryBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateDssImportHistory(DssImportHistory entity)
        {
            Create(entity);
        }

        public virtual IList<DssImportHistory> GetAllDssImportHistory()
        {
            return GetAllDssImportHistory(false);
        }

        public virtual IList<DssImportHistory> GetAllDssImportHistory(bool includeInactive)
        {
            string hql = @"from DssImportHistory entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<DssImportHistory> result = FindAllWithCustomQuery<DssImportHistory>(hql);
            return result;
        }

        public virtual DssImportHistory LoadDssImportHistory(Int32 id)
        {
            return FindById<DssImportHistory>(id);
        }

        public virtual void UpdateDssImportHistory(DssImportHistory entity)
        {
            Update(entity);
        }

        public virtual void DeleteDssImportHistory(Int32 id)
        {
            string hql = @"from DssImportHistory entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteDssImportHistory(DssImportHistory entity)
        {
            Delete(entity);
        }

        public virtual void DeleteDssImportHistory(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DssImportHistory entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteDssImportHistory(IList<DssImportHistory> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (DssImportHistory entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteDssImportHistory(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
