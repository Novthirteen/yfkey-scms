using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Distribution;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Distribution.NH
{
    public class NHSalesPriceListBaseDao : NHDaoBase, ISalesPriceListBaseDao
    {
        public NHSalesPriceListBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateSalesPriceList(SalesPriceList entity)
        {
            Create(entity);
        }

        public virtual IList<SalesPriceList> GetAllSalesPriceList()
        {
            return GetAllSalesPriceList(false);
        }

        public virtual IList<SalesPriceList> GetAllSalesPriceList(bool includeInactive)
        {
            string hql = @"from SalesPriceList entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<SalesPriceList> result = FindAllWithCustomQuery<SalesPriceList>(hql);
            return result;
        }

        public virtual SalesPriceList LoadSalesPriceList(String code)
        {
            return FindById<SalesPriceList>(code);
        }

        public virtual void UpdateSalesPriceList(SalesPriceList entity)
        {
            Update(entity);
        }

        public virtual void DeleteSalesPriceList(String code)
        {
            string hql = @"from SalesPriceList entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteSalesPriceList(SalesPriceList entity)
        {
            Delete(entity);
        }

        public virtual void DeleteSalesPriceList(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from SalesPriceList entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteSalesPriceList(IList<SalesPriceList> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (SalesPriceList entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteSalesPriceList(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
