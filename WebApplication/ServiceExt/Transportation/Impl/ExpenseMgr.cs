using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Transportation;
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.Transportation;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class ExpenseMgr : ExpenseBaseMgr, IExpenseMgr
    {
        public ExpenseMgr(IExpenseDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public override void UpdateExpense(Expense expense)
        {
            Expense oldExpense = base.LoadExpense(expense.Code);

            oldExpense.Remark = expense.Remark;
            oldExpense.LastModifyDate = DateTime.Now;
            oldExpense.LastModifyUser = expense.LastModifyUser;

            base.UpdateExpense(oldExpense);
        }

        #endregion Customized Methods
    }
}