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
    public class NHReceiptInProcessLocationBaseDao : NHDaoBase, IReceiptInProcessLocationBaseDao
    {
        public NHReceiptInProcessLocationBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateReceiptInProcessLocation(ReceiptInProcessLocation entity)
        {
            Create(entity);
        }

        public virtual IList<ReceiptInProcessLocation> GetAllReceiptInProcessLocation()
        {
            return FindAll<ReceiptInProcessLocation>();
        }

        public virtual ReceiptInProcessLocation LoadReceiptInProcessLocation(Int32 id)
        {
            return FindById<ReceiptInProcessLocation>(id);
        }

        public virtual void UpdateReceiptInProcessLocation(ReceiptInProcessLocation entity)
        {
            Update(entity);
        }

        public virtual void DeleteReceiptInProcessLocation(Int32 id)
        {
            string hql = @"from ReceiptInProcessLocation entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteReceiptInProcessLocation(ReceiptInProcessLocation entity)
        {
            Delete(entity);
        }

        public virtual void DeleteReceiptInProcessLocation(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReceiptInProcessLocation entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteReceiptInProcessLocation(IList<ReceiptInProcessLocation> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ReceiptInProcessLocation entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteReceiptInProcessLocation(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
