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
    public class NHActingBillBaseDao : NHDaoBase, IActingBillBaseDao
    {
        public NHActingBillBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateActingBill(ActingBill entity)
        {
            Create(entity);
        }

        public virtual IList<ActingBill> GetAllActingBill()
        {
            return FindAll<ActingBill>();
        }

        public virtual ActingBill LoadActingBill(Int32 id)
        {
            return FindById<ActingBill>(id);
        }

        public virtual void UpdateActingBill(ActingBill entity)
        {
            Update(entity);
        }

        public virtual void DeleteActingBill(Int32 id)
        {
            string hql = @"from ActingBill entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteActingBill(ActingBill entity)
        {
            Delete(entity);
        }

        public virtual void DeleteActingBill(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ActingBill entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteActingBill(IList<ActingBill> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ActingBill entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteActingBill(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
