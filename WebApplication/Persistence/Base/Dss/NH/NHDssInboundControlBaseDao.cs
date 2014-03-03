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
    public class NHDssInboundControlBaseDao : NHDaoBase, IDssInboundControlBaseDao
    {
        public NHDssInboundControlBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateDssInboundControl(DssInboundControl entity)
        {
            Create(entity);
        }

        public virtual IList<DssInboundControl> GetAllDssInboundControl()
        {
            return FindAll<DssInboundControl>();
        }

        public virtual DssInboundControl LoadDssInboundControl(Int32 id)
        {
            return FindById<DssInboundControl>(id);
        }

        public virtual void UpdateDssInboundControl(DssInboundControl entity)
        {
            Update(entity);
        }

        public virtual void DeleteDssInboundControl(Int32 id)
        {
            string hql = @"from DssInboundControl entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteDssInboundControl(DssInboundControl entity)
        {
            Delete(entity);
        }

        public virtual void DeleteDssInboundControl(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DssInboundControl entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteDssInboundControl(IList<DssInboundControl> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (DssInboundControl entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteDssInboundControl(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
