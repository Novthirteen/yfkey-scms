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
    public class NHAddressBaseDao : NHDaoBase, IAddressBaseDao
    {
        public NHAddressBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateAddress(Address entity)
        {
            Create(entity);
        }

        public virtual IList<Address> GetAllAddress()
        {
            return GetAllAddress(false);
        }

        public virtual IList<Address> GetAllAddress(bool includeInactive)
        {
            string hql = @"from Address entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Address> result = FindAllWithCustomQuery<Address>(hql);
            return result;
        }

        public virtual Address LoadAddress(String code)
        {
            return FindById<Address>(code);
        }

        public virtual void UpdateAddress(Address entity)
        {
            Update(entity);
        }

        public virtual void DeleteAddress(String code)
        {
            string hql = @"from Address entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteAddress(Address entity)
        {
            Delete(entity);
        }

        public virtual void DeleteAddress(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Address entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteAddress(IList<Address> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Address entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteAddress(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
