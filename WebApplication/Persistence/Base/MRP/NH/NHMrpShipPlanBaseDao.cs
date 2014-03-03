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
    public class NHMrpShipPlanBaseDao : NHDaoBase, IMrpShipPlanBaseDao
    {
        public NHMrpShipPlanBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateMrpShipPlan(MrpShipPlan entity)
        {
            Create(entity);
        }

        public virtual IList<MrpShipPlan> GetAllMrpShipPlan()
        {
            return FindAll<MrpShipPlan>();
        }

        public virtual MrpShipPlan LoadMrpShipPlan(Int32 id)
        {
            return FindById<MrpShipPlan>(id);
        }

        public virtual void UpdateMrpShipPlan(MrpShipPlan entity)
        {
            Update(entity);
        }

        public virtual void DeleteMrpShipPlan(Int32 id)
        {
            string hql = @"from MrpShipPlan entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteMrpShipPlan(MrpShipPlan entity)
        {
            Delete(entity);
        }

        public virtual void DeleteMrpShipPlan(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from MrpShipPlan entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteMrpShipPlan(IList<MrpShipPlan> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (MrpShipPlan entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteMrpShipPlan(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
