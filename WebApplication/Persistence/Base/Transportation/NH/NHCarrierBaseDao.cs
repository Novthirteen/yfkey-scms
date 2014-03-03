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
    public class NHCarrierBaseDao : NHDaoBase, ICarrierBaseDao
    {
        public NHCarrierBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateCarrier(Carrier entity)
        {
            Create(entity);
        }

        public virtual IList<Carrier> GetAllCarrier()
        {
            return FindAll<Carrier>();
        }

        public virtual Carrier LoadCarrier(String code)
        {
            return FindById<Carrier>(code);
        }

        public virtual void UpdateCarrier(Carrier entity)
        {
            Update(entity);
        }

        public virtual void DeleteCarrier(String code)
        {
            string hql = @"from Carrier entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteCarrier(Carrier entity)
        {
            Delete(entity);
        }

        public virtual void DeleteCarrier(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Carrier entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteCarrier(IList<Carrier> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Carrier entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteCarrier(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
