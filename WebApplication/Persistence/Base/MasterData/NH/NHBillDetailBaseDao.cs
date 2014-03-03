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
    public class NHBillDetailBaseDao : NHDaoBase, IBillDetailBaseDao
    {
        public NHBillDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBillDetail(BillDetail entity)
        {
            Create(entity);
        }

        public virtual IList<BillDetail> GetAllBillDetail()
        {
            return FindAll<BillDetail>();
        }

        public virtual BillDetail LoadBillDetail(Int32 id)
        {
            return FindById<BillDetail>(id);
        }

        public virtual void UpdateBillDetail(BillDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteBillDetail(Int32 id)
        {
            string hql = @"from BillDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBillDetail(BillDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBillDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BillDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBillDetail(IList<BillDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BillDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBillDetail(pkList);
        }


        public virtual BillDetail LoadBillDetail(com.Sconit.Entity.MasterData.Bill billNo, com.Sconit.Entity.MasterData.ActingBill actingBill)
        {
            string hql = @"from BillDetail entity where entity.BillNo.BillNo = ? and entity.ActingBill.Id = ?";
            IList<BillDetail> result = FindAllWithCustomQuery<BillDetail>(hql, new object[] { billNo.BillNo, actingBill.Id }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeleteBillDetail(String billNoBillNo, Int32 actingBillId)
        {
            string hql = @"from BillDetail entity where entity.BillNo.BillNo = ? and entity.ActingBill.Id = ?";
            Delete(hql, new object[] { billNoBillNo, actingBillId }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 });
        }

        public virtual BillDetail LoadBillDetail(String billNoBillNo, Int32 actingBillId)
        {
            string hql = @"from BillDetail entity where entity.BillNo.BillNo = ? and entity.ActingBill.Id = ?";
            IList<BillDetail> result = FindAllWithCustomQuery<BillDetail>(hql, new object[] { billNoBillNo, actingBillId }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 });
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
