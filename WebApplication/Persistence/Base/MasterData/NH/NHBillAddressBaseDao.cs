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
    public class NHBillAddressBaseDao : NHDaoBase, IBillAddressBaseDao
    {
        public NHBillAddressBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBillAddress(BillAddress entity)
        {
            Create(entity);
        }

        public virtual IList<BillAddress> GetAllBillAddress()
        {
            return GetAllBillAddress(false);
        }

        public virtual IList<BillAddress> GetAllBillAddress(bool includeInactive)
        {
            string hql = @"from BillAddress entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<BillAddress> result = FindAllWithCustomQuery<BillAddress>(hql);
            return result;
        }

        public virtual BillAddress LoadBillAddress(String code)
        {
            return FindById<BillAddress>(code);
        }

        public virtual void UpdateBillAddress(BillAddress entity)
        {
            Update(entity);
        }

        public virtual void DeleteBillAddress(String code)
        {
            string hql = @"from BillAddress entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteBillAddress(BillAddress entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBillAddress(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BillAddress entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBillAddress(IList<BillAddress> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (BillAddress entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteBillAddress(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
