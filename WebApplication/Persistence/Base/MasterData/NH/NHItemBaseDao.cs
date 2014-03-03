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
    public class NHItemBaseDao : NHDaoBase, IItemBaseDao
    {
        public NHItemBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateItem(Item entity)
        {
            Create(entity);
        }

        public virtual IList<Item> GetAllItem()
        {
            return GetAllItem(false);
        }

        public virtual IList<Item> GetAllItem(bool includeInactive)
        {
            string hql = @"from Item entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Item> result = FindAllWithCustomQuery<Item>(hql);
            return result;
        }

        public virtual Item LoadItem(String code)
        {
            return FindById<Item>(code);
        }

        public virtual void UpdateItem(Item entity)
        {
            Update(entity);
        }

        public virtual void DeleteItem(String code)
        {
            string hql = @"from Item entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteItem(Item entity)
        {
            Delete(entity);
        }

        public virtual void DeleteItem(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Item entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteItem(IList<Item> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Item entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteItem(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
