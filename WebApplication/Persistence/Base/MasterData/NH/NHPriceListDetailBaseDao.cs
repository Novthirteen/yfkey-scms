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
    public class NHPriceListDetailBaseDao : NHDaoBase, IPriceListDetailBaseDao
    {
        public NHPriceListDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePriceListDetail(PriceListDetail entity)
        {
            Create(entity);
        }

        public virtual IList<PriceListDetail> GetAllPriceListDetail()
        {
            return FindAll<PriceListDetail>();
        }

        public virtual PriceListDetail LoadPriceListDetail(Int32 id)
        {
            return FindById<PriceListDetail>(id);
        }

        public virtual void UpdatePriceListDetail(PriceListDetail entity)
        {
            Update(entity);
        }

        public virtual void DeletePriceListDetail(Int32 id)
        {
            string hql = @"from PriceListDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeletePriceListDetail(PriceListDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeletePriceListDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PriceListDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePriceListDetail(IList<PriceListDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (PriceListDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeletePriceListDetail(pkList);
        }


        public virtual PriceListDetail LoadPriceListDetail(com.Sconit.Entity.MasterData.PriceList priceList, DateTime startDate, com.Sconit.Entity.MasterData.Item item, Currency currency)
        {
            string hql = @"from PriceListDetail entity where entity.PriceList.Code = ? and entity.StartDate = ? and entity.Item.Code = ? and entity.Currency.Code = ?";
            IList<PriceListDetail> result = FindAllWithCustomQuery<PriceListDetail>(hql, new object[] { priceList.Code, startDate, item.Code, currency.Code }, new IType[] { NHibernateUtil.String, NHibernateUtil.DateTime, NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeletePriceListDetail(String priceListCode, DateTime startDate, String itemCode, String currencyCode)
        {
            string hql = @"from PriceListDetail entity where entity.PriceList.Code = ? and entity.StartDate = ? and entity.Item.Code = ? and entity.Currency.Code = ?";
            Delete(hql, new object[] { priceListCode, startDate, itemCode, currencyCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.DateTime, NHibernateUtil.String, NHibernateUtil.String });
        }

        public virtual PriceListDetail LoadPriceListDetail(String priceListCode, DateTime startDate, String itemCode, String currencyCode)
        {
            string hql = @"from PriceListDetail entity where entity.PriceList.Code = ? and entity.StartDate = ? and entity.Item.Code = ? and entity.Currency.Code = ?";
            IList<PriceListDetail> result = FindAllWithCustomQuery<PriceListDetail>(hql, new object[] { priceListCode, startDate, itemCode, currencyCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.DateTime, NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        #endregion Method Created By CodeSmith
    }
}
