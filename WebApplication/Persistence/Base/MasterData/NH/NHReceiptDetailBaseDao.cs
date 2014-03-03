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
    public class NHReceiptDetailBaseDao : NHDaoBase, IReceiptDetailBaseDao
    {
        public NHReceiptDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateReceiptDetail(ReceiptDetail entity)
        {
            Create(entity);
        }

        public virtual IList<ReceiptDetail> GetAllReceiptDetail()
        {
            return FindAll<ReceiptDetail>();
        }

        public virtual ReceiptDetail LoadReceiptDetail(Int32 id)
        {
            return FindById<ReceiptDetail>(id);
        }

        public virtual void UpdateReceiptDetail(ReceiptDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteReceiptDetail(Int32 id)
        {
            string hql = @"from ReceiptDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteReceiptDetail(ReceiptDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteReceiptDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReceiptDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteReceiptDetail(IList<ReceiptDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ReceiptDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteReceiptDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
