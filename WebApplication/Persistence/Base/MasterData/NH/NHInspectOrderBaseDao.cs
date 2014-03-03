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
    public class NHInspectOrderBaseDao : NHDaoBase, IInspectOrderBaseDao
    {
        public NHInspectOrderBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateInspectOrder(InspectOrder entity)
        {
            Create(entity);
        }

        public virtual IList<InspectOrder> GetAllInspectOrder()
        {
            return FindAll<InspectOrder>();
        }

        public virtual InspectOrder LoadInspectOrder(String inspectNo)
        {
            return FindById<InspectOrder>(inspectNo);
        }

        public virtual void UpdateInspectOrder(InspectOrder entity)
        {
            Update(entity);
        }

        public virtual void DeleteInspectOrder(String inspectNo)
        {
            string hql = @"from InspectOrder entity where entity.InspectNo = ?";
            Delete(hql, new object[] { inspectNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteInspectOrder(InspectOrder entity)
        {
            Delete(entity);
        }

        public virtual void DeleteInspectOrder(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from InspectOrder entity where entity.InspectNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteInspectOrder(IList<InspectOrder> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (InspectOrder entity in entityList)
            {
                pkList.Add(entity.InspectNo);
            }

            DeleteInspectOrder(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
