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
    public class NHBillBaseDao : NHDaoBase, IBillBaseDao
    {
        public NHBillBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBill(Bill entity)
        {
            Create(entity);
        }

        public virtual IList<Bill> GetAllBill()
        {
            return FindAll<Bill>();
        }

        public virtual Bill LoadBill(String billNo)
        {
            return FindById<Bill>(billNo);
        }

        public virtual void UpdateBill(Bill entity)
        {
            Update(entity);
        }

        public virtual void DeleteBill(String billNo)
        {
            string hql = @"from Bill entity where entity.BillNo = ?";
            Delete(hql, new object[] { billNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteBill(Bill entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBill(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Bill entity where entity.BillNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBill(IList<Bill> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Bill entity in entityList)
            {
                pkList.Add(entity.BillNo);
            }

            DeleteBill(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
