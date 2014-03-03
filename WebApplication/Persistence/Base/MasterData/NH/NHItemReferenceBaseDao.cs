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
    public class NHItemReferenceBaseDao : NHDaoBase, IItemReferenceBaseDao
    {
        public NHItemReferenceBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateItemReference(ItemReference entity)
        {
            Create(entity);
        }

        public virtual IList<ItemReference> GetAllItemReference()
        {
            return GetAllItemReference(false);
        }

        public virtual IList<ItemReference> GetAllItemReference(bool includeInactive)
        {
            string hql = @"from ItemReference entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<ItemReference> result = FindAllWithCustomQuery<ItemReference>(hql);
            return result;
        }

        public virtual ItemReference LoadItemReference(Int32 id)
        {
            return FindById<ItemReference>(id);
        }

        public virtual void UpdateItemReference(ItemReference entity)
        {
            Update(entity);
        }

        public virtual void DeleteItemReference(Int32 id)
        {
            string hql = @"from ItemReference entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteItemReference(ItemReference entity)
        {
            Delete(entity);
        }

        public virtual void DeleteItemReference(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ItemReference entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteItemReference(IList<ItemReference> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ItemReference entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteItemReference(pkList);
        }


        public virtual ItemReference LoadItemReference(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Party party, String referenceCode)
        {
            string hql = @"from ItemReference entity where entity.Item.Code = ? and entity.Party.Code = ? and entity.ReferenceCode = ?";
            IList<ItemReference> result = FindAllWithCustomQuery<ItemReference>(hql, new object[] { item.Code, party.Code, referenceCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeleteItemReference(String itemCode, String partyCode, String referenceCode)
        {
            string hql = @"from ItemReference entity where entity.Item.Code = ? and entity.Party.Code = ? and entity.ReferenceCode = ?";
            Delete(hql, new object[] { itemCode, partyCode, referenceCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
        }

        public virtual ItemReference LoadItemReference(String itemCode, String partyCode, String referenceCode)
        {
            string hql = @"from ItemReference entity where entity.Item.Code = ? and entity.Party.Code = ? and entity.ReferenceCode = ?";
            IList<ItemReference> result = FindAllWithCustomQuery<ItemReference>(hql, new object[] { itemCode, partyCode, referenceCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        #endregion Method Created By CodeSmith
    }
}
