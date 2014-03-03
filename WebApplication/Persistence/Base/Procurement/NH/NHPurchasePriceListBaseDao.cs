using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Procurement.NH
{
    public class NHPurchasePriceListBaseDao : NHDaoBase, IPurchasePriceListBaseDao
    {
        public NHPurchasePriceListBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePurchasePriceList(PurchasePriceList entity)
        {
            Create(entity);
        }

        public virtual IList<PurchasePriceList> GetAllPurchasePriceList()
        {
            return GetAllPurchasePriceList(false);
        }

        public virtual IList<PurchasePriceList> GetAllPurchasePriceList(bool includeInactive)
        {
            string hql = @"from PurchasePriceList entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<PurchasePriceList> result = FindAllWithCustomQuery<PurchasePriceList>(hql);
            return result;
        }

        public virtual PurchasePriceList LoadPurchasePriceList(String code)
        {
            return FindById<PurchasePriceList>(code);
        }

        public virtual void UpdatePurchasePriceList(PurchasePriceList entity)
        {
            Update(entity);
        }

        public virtual void DeletePurchasePriceList(String code)
        {
            string hql = @"from PurchasePriceList entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeletePurchasePriceList(PurchasePriceList entity)
        {
            Delete(entity);
        }

        public virtual void DeletePurchasePriceList(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PurchasePriceList entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePurchasePriceList(IList<PurchasePriceList> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (PurchasePriceList entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeletePurchasePriceList(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
