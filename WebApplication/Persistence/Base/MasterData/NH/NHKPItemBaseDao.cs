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
    public class NHKPItemBaseDao : NHDaoBase, IKPItemBaseDao
    {
        public NHKPItemBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateKPItem(KPItem entity)
        {
            Create(entity);
        }

        public virtual IList<KPItem> GetAllKPItem()
        {
            return FindAll<KPItem>();
        }

        public virtual KPItem LoadKPItem(Decimal iTEM_SEQ_ID)
        {
            return FindById<KPItem>(iTEM_SEQ_ID);
        }

        public virtual void UpdateKPItem(KPItem entity)
        {
            Update(entity);
        }

        public virtual void DeleteKPItem(Decimal iTEM_SEQ_ID)
        {
            string hql = @"from KPItem entity where entity.ITEM_SEQ_ID = ?";
            Delete(hql, new object[] { iTEM_SEQ_ID }, new IType[] { NHibernateUtil.Decimal });
        }

        public virtual void DeleteKPItem(KPItem entity)
        {
            Delete(entity);
        }

        public virtual void DeleteKPItem(IList<Decimal> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from KPItem entity where entity.ITEM_SEQ_ID in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteKPItem(IList<KPItem> entityList)
        {
            IList<Decimal> pkList = new List<Decimal>();
            foreach (KPItem entity in entityList)
            {
                pkList.Add(entity.ITEM_SEQ_ID);
            }

            DeleteKPItem(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
