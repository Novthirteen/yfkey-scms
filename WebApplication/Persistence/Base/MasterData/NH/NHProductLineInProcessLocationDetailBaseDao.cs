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
    public class NHProductLineInProcessLocationDetailBaseDao : NHDaoBase, IProductLineInProcessLocationDetailBaseDao
    {
        public NHProductLineInProcessLocationDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateProductLineInProcessLocationDetail(ProductLineInProcessLocationDetail entity)
        {
            Create(entity);
        }

        public virtual IList<ProductLineInProcessLocationDetail> GetAllProductLineInProcessLocationDetail()
        {
            return FindAll<ProductLineInProcessLocationDetail>();
        }

        public virtual ProductLineInProcessLocationDetail LoadProductLineInProcessLocationDetail(Int32 id)
        {
            return FindById<ProductLineInProcessLocationDetail>(id);
        }

        public virtual void UpdateProductLineInProcessLocationDetail(ProductLineInProcessLocationDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteProductLineInProcessLocationDetail(Int32 id)
        {
            string hql = @"from ProductLineInProcessLocationDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteProductLineInProcessLocationDetail(ProductLineInProcessLocationDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteProductLineInProcessLocationDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ProductLineInProcessLocationDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteProductLineInProcessLocationDetail(IList<ProductLineInProcessLocationDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ProductLineInProcessLocationDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteProductLineInProcessLocationDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
