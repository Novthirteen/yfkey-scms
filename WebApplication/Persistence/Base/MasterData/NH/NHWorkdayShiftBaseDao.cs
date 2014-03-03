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
    public class NHWorkdayShiftBaseDao : NHDaoBase, IWorkdayShiftBaseDao
    {
        public NHWorkdayShiftBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateWorkdayShift(WorkdayShift entity)
        {
            Create(entity);
        }

        public virtual IList<WorkdayShift> GetAllWorkdayShift()
        {
            return FindAll<WorkdayShift>();
        }

        public virtual WorkdayShift LoadWorkdayShift(Int32 id)
        {
            return FindById<WorkdayShift>(id);
        }

        public virtual void UpdateWorkdayShift(WorkdayShift entity)
        {
            Update(entity);
        }

        public virtual void DeleteWorkdayShift(Int32 id)
        {
            string hql = @"from WorkdayShift entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteWorkdayShift(WorkdayShift entity)
        {
            Delete(entity);
        }

        public virtual void DeleteWorkdayShift(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from WorkdayShift entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteWorkdayShift(IList<WorkdayShift> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (WorkdayShift entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteWorkdayShift(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
