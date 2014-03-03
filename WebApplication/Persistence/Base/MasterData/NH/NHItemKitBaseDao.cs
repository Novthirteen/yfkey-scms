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
    public class NHItemKitBaseDao : NHDaoBase, IItemKitBaseDao
    {
        public NHItemKitBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateItemKit(ItemKit entity)
        {
            Create(entity);
        }

        public virtual IList<ItemKit> GetAllItemKit()
        {
            return GetAllItemKit(false);
        }

        public virtual IList<ItemKit> GetAllItemKit(bool includeInactive)
        {
            string hql = @"from ItemKit entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<ItemKit> result = FindAllWithCustomQuery<ItemKit>(hql);
            return result;
        }

        public virtual ItemKit LoadItemKit(com.Sconit.Entity.MasterData.Item parentItem, com.Sconit.Entity.MasterData.Item childItem)
        {
            string hql = @"from ItemKit entity where entity.ParentItem.Code = ? and entity.ChildItem.Code = ?";
            IList<ItemKit> result = FindAllWithCustomQuery<ItemKit>(hql, new object[] { parentItem.Code, childItem.Code }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual ItemKit LoadItemKit(String parentItemCode, String childItemCode)
        {
            string hql = @"from ItemKit entity where entity.ParentItem.Code = ? and entity.ChildItem.Code = ?";
            IList<ItemKit> result = FindAllWithCustomQuery<ItemKit>(hql, new object[] { parentItemCode, childItemCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void UpdateItemKit(ItemKit entity)
        {
            Update(entity);
        }

        public virtual void DeleteItemKit(com.Sconit.Entity.MasterData.Item parentItem, com.Sconit.Entity.MasterData.Item childItem)
        {
            string hql = @"from ItemKit entity where entity.ParentItem.Code = ? and entity.ChildItem.Code = ?";
            Delete(hql, new object[] { parentItem.Code, childItem.Code }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
        }

        public virtual void DeleteItemKit(String parentItemCode, String childItemCode)
        {
            string hql = @"from ItemKit entity where entity.ParentItem.Code = ? and entity.ChildItem.Code = ?";
            Delete(hql, new object[] { parentItemCode, childItemCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
        }

        public virtual void DeleteItemKit(ItemKit entity)
        {
            Delete(entity);
        }

        public virtual void DeleteItemKit(IList<ItemKit> entityList)
        {
            foreach (ItemKit entity in entityList)
            {
                DeleteItemKit(entity);
            }
        }


        #endregion Method Created By CodeSmith
    }
}
