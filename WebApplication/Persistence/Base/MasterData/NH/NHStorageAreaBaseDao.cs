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
    public class NHStorageAreaBaseDao : NHDaoBase, IStorageAreaBaseDao
    {
        public NHStorageAreaBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateStorageArea(StorageArea entity)
        {
            Create(entity);
        }

        public virtual IList<StorageArea> GetAllStorageArea()
        {
            return FindAll<StorageArea>();
        }

        public virtual StorageArea LoadStorageArea(String code)
        {
            return FindById<StorageArea>(code);
        }

        public virtual void UpdateStorageArea(StorageArea entity)
        {
            Update(entity);
        }

        public virtual void DeleteStorageArea(String code)
        {
            string hql = @"from StorageArea entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteStorageArea(StorageArea entity)
        {
            Delete(entity);
        }

        public virtual void DeleteStorageArea(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from StorageArea entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteStorageArea(IList<StorageArea> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (StorageArea entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteStorageArea(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
