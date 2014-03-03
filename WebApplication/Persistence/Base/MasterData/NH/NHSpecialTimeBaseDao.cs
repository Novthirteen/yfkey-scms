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
    public class NHSpecialTimeBaseDao : NHDaoBase, ISpecialTimeBaseDao
    {
        public NHSpecialTimeBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateSpecialTime(SpecialTime entity)
        {
            Create(entity);
        }

        public virtual IList<SpecialTime> GetAllSpecialTime()
        {
            return FindAll<SpecialTime>();
        }

        public virtual SpecialTime LoadSpecialTime(Int32 iD)
        {
            return FindById<SpecialTime>(iD);
        }

        public virtual void UpdateSpecialTime(SpecialTime entity)
        {
            Update(entity);
        }

        public virtual void DeleteSpecialTime(Int32 iD)
        {
            string hql = @"from SpecialTime entity where entity.ID = ?";
            Delete(hql, new object[] { iD }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteSpecialTime(SpecialTime entity)
        {
            Delete(entity);
        }

        public virtual void DeleteSpecialTime(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from SpecialTime entity where entity.ID in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteSpecialTime(IList<SpecialTime> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (SpecialTime entity in entityList)
            {
                pkList.Add(entity.ID);
            }

            DeleteSpecialTime(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
