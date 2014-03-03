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
    public class NHCycleCountBaseDao : NHDaoBase, ICycleCountBaseDao
    {
        public NHCycleCountBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCycleCount(CycleCount entity)
        {
            Create(entity);
        }

        public virtual IList<CycleCount> GetAllCycleCount()
        {
            return FindAll<CycleCount>();
        }

        public virtual CycleCount LoadCycleCount(String code)
        {
            return FindById<CycleCount>(code);
        }

        public virtual void UpdateCycleCount(CycleCount entity)
        {
            Update(entity);
        }

        public virtual void DeleteCycleCount(String code)
        {
            string hql = @"from CycleCount entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteCycleCount(CycleCount entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCycleCount(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CycleCount entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCycleCount(IList<CycleCount> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (CycleCount entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteCycleCount(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
