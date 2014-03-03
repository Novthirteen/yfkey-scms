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
    public class NHInProcessLocationDetailViewBaseDao : NHDaoBase, IInProcessLocationDetailViewBaseDao
    {
        public NHInProcessLocationDetailViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateInProcessLocationDetailView(InProcessLocationDetailView entity)
        {
            Create(entity);
        }

        public virtual IList<InProcessLocationDetailView> GetAllInProcessLocationDetailView()
        {
            return FindAll<InProcessLocationDetailView>();
        }

        public virtual InProcessLocationDetailView LoadInProcessLocationDetailView(Int32 id)
        {
            return FindById<InProcessLocationDetailView>(id);
        }

        public virtual void UpdateInProcessLocationDetailView(InProcessLocationDetailView entity)
        {
            Update(entity);
        }

        public virtual void DeleteInProcessLocationDetailView(Int32 id)
        {
            string hql = @"from InProcessLocationDetailView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteInProcessLocationDetailView(InProcessLocationDetailView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteInProcessLocationDetailView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from InProcessLocationDetailView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteInProcessLocationDetailView(IList<InProcessLocationDetailView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (InProcessLocationDetailView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteInProcessLocationDetailView(pkList);
        }


        public virtual InProcessLocationDetailView LoadInProcessLocationDetailView(com.Sconit.Entity.Distribution.InProcessLocation inProcessLocation)
        {
            string hql = @"from InProcessLocationDetailView entity where entity.InProcessLocation.IpNo = ?";
            IList<InProcessLocationDetailView> result = FindAllWithCustomQuery<InProcessLocationDetailView>(hql, new object[] { inProcessLocation.IpNo }, new IType[] { NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeleteInProcessLocationDetailView(String inProcessLocationIpNo)
        {
            string hql = @"from InProcessLocationDetailView entity where entity.InProcessLocation.IpNo = ?";
            Delete(hql, new object[] { inProcessLocationIpNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual InProcessLocationDetailView LoadInProcessLocationDetailView(String inProcessLocationIpNo)
        {
            string hql = @"from InProcessLocationDetailView entity where entity.InProcessLocation.IpNo = ?";
            IList<InProcessLocationDetailView> result = FindAllWithCustomQuery<InProcessLocationDetailView>(hql, new object[] { inProcessLocationIpNo }, new IType[] { NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeleteInProcessLocationDetailView(IList<com.Sconit.Entity.Distribution.InProcessLocation> UniqueList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from InProcessLocationDetailView entity where entity.InProcessLocation in (");
            hql.Append(UniqueList[0]);
            for (int i = 1; i < UniqueList.Count; i++)
            {
                hql.Append(",");
                hql.Append(UniqueList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        #endregion Method Created By CodeSmith
    }
}
