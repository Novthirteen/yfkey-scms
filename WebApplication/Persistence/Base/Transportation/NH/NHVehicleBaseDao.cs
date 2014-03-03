using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Transportation;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Transportation.NH
{
    public class NHVehicleBaseDao : NHDaoBase, IVehicleBaseDao
    {
        public NHVehicleBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateVehicle(Vehicle entity)
        {
            Create(entity);
        }

        public virtual IList<Vehicle> GetAllVehicle()
        {
            return GetAllVehicle(false);
        }

        public virtual IList<Vehicle> GetAllVehicle(bool includeInactive)
        {
            string hql = @"from Vehicle entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Vehicle> result = FindAllWithCustomQuery<Vehicle>(hql);
            return result;
        }

        public virtual Vehicle LoadVehicle(String code)
        {
            return FindById<Vehicle>(code);
        }

        public virtual void UpdateVehicle(Vehicle entity)
        {
            Update(entity);
        }

        public virtual void DeleteVehicle(String code)
        {
            string hql = @"from Vehicle entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteVehicle(Vehicle entity)
        {
            Delete(entity);
        }

        public virtual void DeleteVehicle(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Vehicle entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteVehicle(IList<Vehicle> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Vehicle entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteVehicle(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
