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
    public class NHCurrencyBaseDao : NHDaoBase, ICurrencyBaseDao
    {
        public NHCurrencyBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCurrency(Currency entity)
        {
            Create(entity);
        }

        public virtual IList<Currency> GetAllCurrency()
        {
            return FindAll<Currency>();
        }

        public virtual Currency LoadCurrency(String code)
        {
            return FindById<Currency>(code);
        }

        public virtual void UpdateCurrency(Currency entity)
        {
            Update(entity);
        }

        public virtual void DeleteCurrency(String code)
        {
            string hql = @"from Currency entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteCurrency(Currency entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCurrency(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Currency entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCurrency(IList<Currency> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Currency entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteCurrency(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
