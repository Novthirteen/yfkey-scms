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
    public class NHShipAddressBaseDao : NHDaoBase, IShipAddressBaseDao
    {
        public NHShipAddressBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateShipAddress(ShipAddress entity)
        {
            Create(entity);
        }

        public virtual IList<ShipAddress> GetAllShipAddress()
        {
            return GetAllShipAddress(false);
        }

        public virtual IList<ShipAddress> GetAllShipAddress(bool includeInactive)
        {
            string hql = @"from ShipAddress entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<ShipAddress> result = FindAllWithCustomQuery<ShipAddress>(hql);
            return result;
        }

        public virtual ShipAddress LoadShipAddress(String code)
        {
            return FindById<ShipAddress>(code);
        }

        public virtual void UpdateShipAddress(ShipAddress entity)
        {
            Update(entity);
        }

        public virtual void DeleteShipAddress(String code)
        {
            string hql = @"from ShipAddress entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteShipAddress(ShipAddress entity)
        {
            Delete(entity);
        }

        public virtual void DeleteShipAddress(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ShipAddress entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteShipAddress(IList<ShipAddress> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (ShipAddress entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteShipAddress(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
