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
    public class NHReceiptBaseDao : NHDaoBase, IReceiptBaseDao
    {
        public NHReceiptBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateReceipt(Receipt entity)
        {
            Create(entity);
        }

        public virtual IList<Receipt> GetAllReceipt()
        {
            return FindAll<Receipt>();
        }

        public virtual Receipt LoadReceipt(String receiptNo)
        {
            return FindById<Receipt>(receiptNo);
        }

        public virtual void UpdateReceipt(Receipt entity)
        {
            Update(entity);
        }

        public virtual void DeleteReceipt(String receiptNo)
        {
            string hql = @"from Receipt entity where entity.ReceiptNo = ?";
            Delete(hql, new object[] { receiptNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteReceipt(Receipt entity)
        {
            Delete(entity);
        }

        public virtual void DeleteReceipt(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Receipt entity where entity.ReceiptNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteReceipt(IList<Receipt> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Receipt entity in entityList)
            {
                pkList.Add(entity.ReceiptNo);
            }

            DeleteReceipt(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
