using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Distribution;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Distribution.NH
{
    public class NHInProcessLocationDetailBaseDao : NHDaoBase, IInProcessLocationDetailBaseDao
    {
        public NHInProcessLocationDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateInProcessLocationDetail(InProcessLocationDetail entity)
        {
            Create(entity);
        }

        public virtual IList<InProcessLocationDetail> GetAllInProcessLocationDetail()
        {
            return FindAll<InProcessLocationDetail>();
        }

        public virtual InProcessLocationDetail LoadInProcessLocationDetail(Int32 id)
        {
            return FindById<InProcessLocationDetail>(id);
        }

        public virtual void UpdateInProcessLocationDetail(InProcessLocationDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteInProcessLocationDetail(Int32 id)
        {
            string hql = @"from InProcessLocationDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteInProcessLocationDetail(InProcessLocationDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteInProcessLocationDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from InProcessLocationDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteInProcessLocationDetail(IList<InProcessLocationDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (InProcessLocationDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteInProcessLocationDetail(pkList);
        }


        public virtual InProcessLocationDetail LoadInProcessLocationDetail(com.Sconit.Entity.Distribution.InProcessLocation inProcessLocation, com.Sconit.Entity.MasterData.OrderLocationTransaction orderLocationTransaction, String lotNo)
        {
            string hql = @"from InProcessLocationDetail entity where entity.InProcessLocation.IpNo = ? and entity.OrderLocationTransaction.Id = ? and entity.LotNo = ?";
            IList<InProcessLocationDetail> result = FindAllWithCustomQuery<InProcessLocationDetail>(hql, new object[] { inProcessLocation.IpNo, orderLocationTransaction.Id, lotNo }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeleteInProcessLocationDetail(String inProcessLocationIpNo, Int32 orderLocationTransactionId, String lotNo)
        {
            string hql = @"from InProcessLocationDetail entity where entity.InProcessLocation.IpNo = ? and entity.OrderLocationTransaction.Id = ? and entity.LotNo = ?";
            Delete(hql, new object[] { inProcessLocationIpNo, orderLocationTransactionId, lotNo }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32, NHibernateUtil.String });
        }

        public virtual InProcessLocationDetail LoadInProcessLocationDetail(String inProcessLocationIpNo, Int32 orderLocationTransactionId, String lotNo)
        {
            string hql = @"from InProcessLocationDetail entity where entity.InProcessLocation.IpNo = ? and entity.OrderLocationTransaction.Id = ? and entity.LotNo = ?";
            IList<InProcessLocationDetail> result = FindAllWithCustomQuery<InProcessLocationDetail>(hql, new object[] { inProcessLocationIpNo, orderLocationTransactionId, lotNo }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32, NHibernateUtil.String });
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
