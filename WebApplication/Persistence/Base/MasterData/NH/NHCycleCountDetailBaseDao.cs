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
    public class NHCycleCountDetailBaseDao : NHDaoBase, ICycleCountDetailBaseDao
    {
        public NHCycleCountDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCycleCountDetail(CycleCountDetail entity)
        {
            Create(entity);
        }

        public virtual IList<CycleCountDetail> GetAllCycleCountDetail()
        {
            return FindAll<CycleCountDetail>();
        }

        public virtual CycleCountDetail LoadCycleCountDetail(Int32 id)
        {
            return FindById<CycleCountDetail>(id);
        }

        public virtual void UpdateCycleCountDetail(CycleCountDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteCycleCountDetail(Int32 id)
        {
            string hql = @"from CycleCountDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteCycleCountDetail(CycleCountDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCycleCountDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CycleCountDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCycleCountDetail(IList<CycleCountDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (CycleCountDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteCycleCountDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
