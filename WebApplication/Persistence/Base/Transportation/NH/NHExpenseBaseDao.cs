using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Transportation;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Transportation.NH
{
    public class NHExpenseBaseDao : NHDaoBase, IExpenseBaseDao
    {
        public NHExpenseBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateExpense(Expense entity)
        {
            Create(entity);
        }

        public virtual IList<Expense> GetAllExpense()
        {
            return FindAll<Expense>();
        }

        public virtual Expense LoadExpense(String code)
        {
            return FindById<Expense>(code);
        }

        public virtual void UpdateExpense(Expense entity)
        {
            Update(entity);
        }

        public virtual void DeleteExpense(String code)
        {
            string hql = @"from Expense entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteExpense(Expense entity)
        {
            Delete(entity);
        }

        public virtual void DeleteExpense(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Expense entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteExpense(IList<Expense> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Expense entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteExpense(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
