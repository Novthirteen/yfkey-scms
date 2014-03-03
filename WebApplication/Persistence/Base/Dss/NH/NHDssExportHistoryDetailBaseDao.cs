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
    public class NHDssExportHistoryDetailBaseDao : NHDaoBase, IDssExportHistoryDetailBaseDao
    {
        public NHDssExportHistoryDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateDssExportHistoryDetail(DssExportHistoryDetail entity)
        {
            Create(entity);
        }

        public virtual IList<DssExportHistoryDetail> GetAllDssExportHistoryDetail()
        {
            return FindAll<DssExportHistoryDetail>();
        }

        public virtual DssExportHistoryDetail LoadDssExportHistoryDetail(Int32 id)
        {
            return FindById<DssExportHistoryDetail>(id);
        }

        public virtual void UpdateDssExportHistoryDetail(DssExportHistoryDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteDssExportHistoryDetail(Int32 id)
        {
            string hql = @"from DssExportHistoryDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteDssExportHistoryDetail(DssExportHistoryDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteDssExportHistoryDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DssExportHistoryDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteDssExportHistoryDetail(IList<DssExportHistoryDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (DssExportHistoryDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteDssExportHistoryDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
