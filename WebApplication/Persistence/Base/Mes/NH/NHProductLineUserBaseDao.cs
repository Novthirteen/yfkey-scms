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
    public class NHProductLineUserBaseDao : NHDaoBase, IProductLineUserBaseDao
    {
        public NHProductLineUserBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateProductLineUser(ProductLineUser entity)
        {
            Create(entity);
        }

        public virtual IList<ProductLineUser> GetAllProductLineUser()
        {
            return FindAll<ProductLineUser>();
        }

        public virtual ProductLineUser LoadProductLineUser(Int32 id)
        {
            return FindById<ProductLineUser>(id);
        }

        public virtual void UpdateProductLineUser(ProductLineUser entity)
        {
            Update(entity);
        }

        public virtual void DeleteProductLineUser(Int32 id)
        {
            string hql = @"from ProductLineUser entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteProductLineUser(ProductLineUser entity)
        {
            Delete(entity);
        }

        public virtual void DeleteProductLineUser(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ProductLineUser entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteProductLineUser(IList<ProductLineUser> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ProductLineUser entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteProductLineUser(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
