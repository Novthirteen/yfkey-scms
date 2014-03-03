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
    public class NHFinanceCalendarBaseDao : NHDaoBase, IFinanceCalendarBaseDao
    {
        public NHFinanceCalendarBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateFinanceCalendar(FinanceCalendar entity)
        {
            Create(entity);
        }

        public virtual IList<FinanceCalendar> GetAllFinanceCalendar()
        {
            return FindAll<FinanceCalendar>();
        }

        public virtual FinanceCalendar LoadFinanceCalendar(Int32 id)
        {
            return FindById<FinanceCalendar>(id);
        }

        public virtual void UpdateFinanceCalendar(FinanceCalendar entity)
        {
            Update(entity);
        }

        public virtual void DeleteFinanceCalendar(Int32 id)
        {
            string hql = @"from FinanceCalendar entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteFinanceCalendar(FinanceCalendar entity)
        {
            Delete(entity);
        }

        public virtual void DeleteFinanceCalendar(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from FinanceCalendar entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteFinanceCalendar(IList<FinanceCalendar> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (FinanceCalendar entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteFinanceCalendar(pkList);
        }


        public virtual FinanceCalendar LoadFinanceCalendar(Int32 financeYear, Int32 financeMonth)
        {
            string hql = @"from FinanceCalendar entity where entity.FinanceYear = ? and entity.FinanceMonth = ?";
            IList<FinanceCalendar> result = FindAllWithCustomQuery<FinanceCalendar>(hql, new object[] { financeYear, financeMonth }, new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeleteFinanceCalendar(Int32 financeYear, Int32 financeMonth)
        {
            string hql = @"from FinanceCalendar entity where entity.FinanceYear = ? and entity.FinanceMonth = ?";
            Delete(hql, new object[] { financeYear, financeMonth }, new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 });
        }

        #endregion Method Created By CodeSmith
    }
}
