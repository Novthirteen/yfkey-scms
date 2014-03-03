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
    public class NHInspectOrderDetailBaseDao : NHDaoBase, IInspectOrderDetailBaseDao
    {
        public NHInspectOrderDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateInspectOrderDetail(InspectOrderDetail entity)
        {
            Create(entity);
        }

        public virtual IList<InspectOrderDetail> GetAllInspectOrderDetail()
        {
            return FindAll<InspectOrderDetail>();
        }

        public virtual InspectOrderDetail LoadInspectOrderDetail(Int32 id)
        {
            return FindById<InspectOrderDetail>(id);
        }

        public virtual void UpdateInspectOrderDetail(InspectOrderDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteInspectOrderDetail(Int32 id)
        {
            string hql = @"from InspectOrderDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteInspectOrderDetail(InspectOrderDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteInspectOrderDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from InspectOrderDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteInspectOrderDetail(IList<InspectOrderDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (InspectOrderDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteInspectOrderDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
