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
    public class NHPriceListBaseDao : NHDaoBase, IPriceListBaseDao
    {
        public NHPriceListBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePriceList(PriceList entity)
        {
            Create(entity);
        }

        public virtual IList<PriceList> GetAllPriceList()
        {
            return GetAllPriceList(false);
        }

        public virtual IList<PriceList> GetAllPriceList(bool includeInactive)
        {
            string hql = @"from PriceList entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<PriceList> result = FindAllWithCustomQuery<PriceList>(hql);
            return result;
        }

        public virtual PriceList LoadPriceList(String code)
        {
            return FindById<PriceList>(code);
        }

        public virtual void UpdatePriceList(PriceList entity)
        {
            Update(entity);
        }

        public virtual void DeletePriceList(String code)
        {
            string hql = @"from PriceList entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeletePriceList(PriceList entity)
        {
            Delete(entity);
        }

        public virtual void DeletePriceList(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PriceList entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePriceList(IList<PriceList> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (PriceList entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeletePriceList(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
