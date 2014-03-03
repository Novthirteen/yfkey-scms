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
    public class NHPickListDetailBaseDao : NHDaoBase, IPickListDetailBaseDao
    {
        public NHPickListDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePickListDetail(PickListDetail entity)
        {
            Create(entity);
        }

        public virtual IList<PickListDetail> GetAllPickListDetail()
        {
            return FindAll<PickListDetail>();
        }

        public virtual PickListDetail LoadPickListDetail(Int32 id)
        {
            return FindById<PickListDetail>(id);
        }

        public virtual void UpdatePickListDetail(PickListDetail entity)
        {
            Update(entity);
        }

        public virtual void DeletePickListDetail(Int32 id)
        {
            string hql = @"from PickListDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeletePickListDetail(PickListDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeletePickListDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PickListDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePickListDetail(IList<PickListDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (PickListDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeletePickListDetail(pkList);
        }

        #endregion Method Created By CodeSmith
    }
}
