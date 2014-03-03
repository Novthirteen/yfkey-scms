using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MasterData.NH
{
    public class NHShiftPlanScheduleBaseDao : NHDaoBase, IShiftPlanScheduleBaseDao
    {
        public NHShiftPlanScheduleBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateShiftPlanSchedule(ShiftPlanSchedule entity)
        {
            Create(entity);
        }

        public virtual IList<ShiftPlanSchedule> GetAllShiftPlanSchedule()
        {
            return FindAll<ShiftPlanSchedule>();
        }

        public virtual ShiftPlanSchedule LoadShiftPlanSchedule(Int32 id)
        {
            return FindById<ShiftPlanSchedule>(id);
        }

        public virtual void UpdateShiftPlanSchedule(ShiftPlanSchedule entity)
        {
            Update(entity);
        }

        public virtual void DeleteShiftPlanSchedule(Int32 id)
        {
            string hql = @"from ShiftPlanSchedule entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteShiftPlanSchedule(ShiftPlanSchedule entity)
        {
            Delete(entity);
        }

        public virtual void DeleteShiftPlanSchedule(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ShiftPlanSchedule entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteShiftPlanSchedule(IList<ShiftPlanSchedule> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ShiftPlanSchedule entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteShiftPlanSchedule(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
