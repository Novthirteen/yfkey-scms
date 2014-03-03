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
    public class NHStorageBinBaseDao : NHDaoBase, IStorageBinBaseDao
    {
        public NHStorageBinBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateStorageBin(StorageBin entity)
        {
            Create(entity);
        }

        public virtual IList<StorageBin> GetAllStorageBin()
        {
            return FindAll<StorageBin>();
        }

        public virtual StorageBin LoadStorageBin(String code)
        {
            return FindById<StorageBin>(code);
        }

        public virtual void UpdateStorageBin(StorageBin entity)
        {
            Update(entity);
        }

        public virtual void DeleteStorageBin(String code)
        {
            string hql = @"from StorageBin entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteStorageBin(StorageBin entity)
        {
            Delete(entity);
        }

        public virtual void DeleteStorageBin(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from StorageBin entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteStorageBin(IList<StorageBin> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (StorageBin entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteStorageBin(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
