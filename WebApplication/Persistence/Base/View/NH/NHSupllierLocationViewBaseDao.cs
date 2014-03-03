using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.View;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.View.NH
{
    public class NHSupllierLocationViewBaseDao : NHDaoBase, ISupllierLocationViewBaseDao
    {
        public NHSupllierLocationViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateSupllierLocationView(SupllierLocationView entity)
        {
            Create(entity);
        }

        public virtual IList<SupllierLocationView> GetAllSupllierLocationView()
        {
            return FindAll<SupllierLocationView>();
        }

        public virtual SupllierLocationView LoadSupllierLocationView(Int32 id)
        {
            return FindById<SupllierLocationView>(id);
        }

        public virtual void UpdateSupllierLocationView(SupllierLocationView entity)
        {
            Update(entity);
        }

        public virtual void DeleteSupllierLocationView(Int32 id)
        {
            string hql = @"from SupllierLocationView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteSupllierLocationView(SupllierLocationView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteSupllierLocationView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from SupllierLocationView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteSupllierLocationView(IList<SupllierLocationView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (SupllierLocationView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteSupllierLocationView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
