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
    public class NHBomDetailBaseDao : NHDaoBase, IBomDetailBaseDao
    {
        public NHBomDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBomDetail(BomDetail entity)
        {
            Create(entity);
        }

        public virtual IList<BomDetail> GetAllBomDetail()
        {
            return FindAll<BomDetail>();
        }

        public virtual BomDetail LoadBomDetail(Int32 id)
        {
            return FindById<BomDetail>(id);
        }

        public virtual void UpdateBomDetail(BomDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteBomDetail(Int32 id)
        {
            string hql = @"from BomDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBomDetail(BomDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBomDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BomDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBomDetail(IList<BomDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BomDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBomDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
