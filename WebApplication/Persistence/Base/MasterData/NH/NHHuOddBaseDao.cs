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
    public class NHHuOddBaseDao : NHDaoBase, IHuOddBaseDao
    {
        public NHHuOddBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateHuOdd(HuOdd entity)
        {
            Create(entity);
        }

        public virtual IList<HuOdd> GetAllHuOdd()
        {
            return FindAll<HuOdd>();
        }

        public virtual HuOdd LoadHuOdd(Int32 id)
        {
            return FindById<HuOdd>(id);
        }

        public virtual void UpdateHuOdd(HuOdd entity)
        {
            Update(entity);
        }

        public virtual void DeleteHuOdd(Int32 id)
        {
            string hql = @"from HuOdd entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteHuOdd(HuOdd entity)
        {
            Delete(entity);
        }

        public virtual void DeleteHuOdd(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from HuOdd entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteHuOdd(IList<HuOdd> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (HuOdd entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteHuOdd(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
