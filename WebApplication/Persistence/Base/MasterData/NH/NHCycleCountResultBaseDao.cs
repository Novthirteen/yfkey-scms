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
    public class NHCycleCountResultBaseDao : NHDaoBase, ICycleCountResultBaseDao
    {
        public NHCycleCountResultBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCycleCountResult(CycleCountResult entity)
        {
            Create(entity);
        }

        public virtual IList<CycleCountResult> GetAllCycleCountResult()
        {
            return FindAll<CycleCountResult>();
        }

        public virtual CycleCountResult LoadCycleCountResult(Int32 id)
        {
            return FindById<CycleCountResult>(id);
        }

        public virtual void UpdateCycleCountResult(CycleCountResult entity)
        {
            Update(entity);
        }

        public virtual void DeleteCycleCountResult(Int32 id)
        {
            string hql = @"from CycleCountResult entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteCycleCountResult(CycleCountResult entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCycleCountResult(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CycleCountResult entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCycleCountResult(IList<CycleCountResult> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (CycleCountResult entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteCycleCountResult(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
