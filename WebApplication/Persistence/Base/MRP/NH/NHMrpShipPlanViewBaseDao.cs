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
    public class NHMrpShipPlanViewBaseDao : NHDaoBase, IMrpShipPlanViewBaseDao
    {
        public NHMrpShipPlanViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateMrpShipPlanView(MrpShipPlanView entity)
        {
            Create(entity);
        }

        public virtual IList<MrpShipPlanView> GetAllMrpShipPlanView()
        {
            return FindAll<MrpShipPlanView>();
        }

        public virtual MrpShipPlanView LoadMrpShipPlanView(String flow, String item, String uom, Decimal uC, DateTime startTime, DateTime windowTime, DateTime effDate)
        {
            string hql = @"from MrpShipPlanView entity where entity.Flow = ? and entity.Item = ? and entity.Uom = ? and entity.UC = ? and entity.StartTime = ? and entity.WindowTime = ? and entity.EffDate = ?";
            IList<MrpShipPlanView> result = FindAllWithCustomQuery<MrpShipPlanView>(hql, new object[] { flow, item, uom, uC, startTime, windowTime, effDate }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.Decimal, NHibernateUtil.DateTime, NHibernateUtil.DateTime, NHibernateUtil.DateTime });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void UpdateMrpShipPlanView(MrpShipPlanView entity)
        {
            Update(entity);
        }

        public virtual void DeleteMrpShipPlanView(String flow, String item, String uom, Decimal uC, DateTime startTime, DateTime windowTime, DateTime effDate)
        {
            string hql = @"from MrpShipPlanView entity where entity.Flow = ? and entity.Item = ? and entity.Uom = ? and entity.UC = ? and entity.StartTime = ? and entity.WindowTime = ? and entity.EffDate = ?";
            Delete(hql, new object[] { flow, item, uom, uC, startTime, windowTime, effDate }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.Decimal, NHibernateUtil.DateTime, NHibernateUtil.DateTime, NHibernateUtil.DateTime });
        }

        public virtual void DeleteMrpShipPlanView(MrpShipPlanView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteMrpShipPlanView(IList<MrpShipPlanView> entityList)
        {
            foreach (MrpShipPlanView entity in entityList)
            {
                DeleteMrpShipPlanView(entity);
            }
        }


        #endregion Method Created By CodeSmith
    }
}
