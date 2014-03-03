using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Mes;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Mes.NH
{
    public class NHShelfBaseDao : NHDaoBase, IShelfBaseDao
    {
        public NHShelfBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateShelf(Shelf entity)
        {
            Create(entity);
        }

        public virtual IList<Shelf> GetAllShelf()
        {
            return FindAll<Shelf>();
        }

        public virtual IList<Shelf> GetAllShelf(bool includeInactive)
        {
            string hql = @"from Shelf entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Shelf> result = FindAllWithCustomQuery<Shelf>(hql);
            return result;
        }

        public virtual Shelf LoadShelf(String code)
        {
            return FindById<Shelf>(code);
        }

        public virtual void UpdateShelf(Shelf entity)
        {
            Update(entity);
        }

        public virtual void DeleteShelf(String code)
        {
            string hql = @"from Shelf entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteShelf(Shelf entity)
        {
            Delete(entity);
        }

        public virtual void DeleteShelf(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Shelf entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteShelf(IList<Shelf> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Shelf entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteShelf(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
