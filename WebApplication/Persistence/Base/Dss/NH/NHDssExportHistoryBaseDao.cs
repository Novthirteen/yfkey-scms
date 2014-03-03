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
    public class NHDssExportHistoryBaseDao : NHDaoBase, IDssExportHistoryBaseDao
    {
        public NHDssExportHistoryBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateDssExportHistory(DssExportHistory entity)
        {
            Create(entity);
        }

        public virtual IList<DssExportHistory> GetAllDssExportHistory()
        {
            return GetAllDssExportHistory(false);
        }

        public virtual IList<DssExportHistory> GetAllDssExportHistory(bool includeInactive)
        {
            string hql = @"from DssExportHistory entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<DssExportHistory> result = FindAllWithCustomQuery<DssExportHistory>(hql);
            return result;
        }

        public virtual DssExportHistory LoadDssExportHistory(Int32 id)
        {
            return FindById<DssExportHistory>(id);
        }

        public virtual void UpdateDssExportHistory(DssExportHistory entity)
        {
            Update(entity);
        }

        public virtual void DeleteDssExportHistory(Int32 id)
        {
            string hql = @"from DssExportHistory entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteDssExportHistory(DssExportHistory entity)
        {
            Delete(entity);
        }

        public virtual void DeleteDssExportHistory(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DssExportHistory entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteDssExportHistory(IList<DssExportHistory> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (DssExportHistory entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteDssExportHistory(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
