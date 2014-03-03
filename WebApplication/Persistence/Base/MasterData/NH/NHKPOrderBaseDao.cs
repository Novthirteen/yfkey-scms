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
    public class NHKPOrderBaseDao : NHDaoBase, IKPOrderBaseDao
    {
        public NHKPOrderBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateKPOrder(KPOrder entity)
        {
            Create(entity);
        }

        public virtual IList<KPOrder> GetAllKPOrder()
        {
            return FindAll<KPOrder>();
        }

        public virtual KPOrder LoadKPOrder(Decimal oRDER_ID)
        {
            return FindById<KPOrder>(oRDER_ID);
        }

        public virtual void UpdateKPOrder(KPOrder entity)
        {
            Update(entity);
        }

        public virtual void DeleteKPOrder(Decimal oRDER_ID)
        {
            string hql = @"from KPOrder entity where entity.ORDER_ID = ?";
            Delete(hql, new object[] { oRDER_ID }, new IType[] { NHibernateUtil.Decimal });
        }

        public virtual void DeleteKPOrder(KPOrder entity)
        {
            Delete(entity);
        }

        public virtual void DeleteKPOrder(IList<Decimal> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from KPOrder entity where entity.ORDER_ID in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteKPOrder(IList<KPOrder> entityList)
        {
            IList<Decimal> pkList = new List<Decimal>();
            foreach (KPOrder entity in entityList)
            {
                pkList.Add(entity.ORDER_ID);
            }

            DeleteKPOrder(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
