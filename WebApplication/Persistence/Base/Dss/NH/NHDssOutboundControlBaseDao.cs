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
    public class NHDssOutboundControlBaseDao : NHDaoBase, IDssOutboundControlBaseDao
    {
        public NHDssOutboundControlBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateDssOutboundControl(DssOutboundControl entity)
        {
            Create(entity);
        }

        public virtual IList<DssOutboundControl> GetAllDssOutboundControl()
        {
            return GetAllDssOutboundControl(false);
        }

        public virtual IList<DssOutboundControl> GetAllDssOutboundControl(bool includeInactive)
        {
            string hql = @"from DssOutboundControl entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<DssOutboundControl> result = FindAllWithCustomQuery<DssOutboundControl>(hql);
            return result;
        }

        public virtual DssOutboundControl LoadDssOutboundControl(Int32 id)
        {
            return FindById<DssOutboundControl>(id);
        }

        public virtual void UpdateDssOutboundControl(DssOutboundControl entity)
        {
            Update(entity);
        }

        public virtual void DeleteDssOutboundControl(Int32 id)
        {
            string hql = @"from DssOutboundControl entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteDssOutboundControl(DssOutboundControl entity)
        {
            Delete(entity);
        }

        public virtual void DeleteDssOutboundControl(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DssOutboundControl entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteDssOutboundControl(IList<DssOutboundControl> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (DssOutboundControl entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteDssOutboundControl(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
