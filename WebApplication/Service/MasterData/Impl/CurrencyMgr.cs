using System.Collections;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity.Exception;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class CurrencyMgr : CurrencyBaseMgr, ICurrencyMgr
    {
        private ICriteriaMgr criteriaMgr;
        public CurrencyMgr(ICurrencyDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public Currency CheckAndLoadCurrency(string currencyCode)
        {
            Currency currency = this.LoadCurrency(currencyCode);
            if (currency == null)
            {
                throw new BusinessErrorException("Currency.Error.CurrencyCodeNotExist", currencyCode);
            }

            return currency;
        }
        [Transaction(TransactionMode.Unspecified)]
        public IList GetCurrency(string code, string name)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Currency));
            if (code != string.Empty && code != null)
                criteria.Add(Expression.Like("Code", code, MatchMode.Start));
            if (name != string.Empty && name != null)
                criteria.Add(Expression.Like("Name", name, MatchMode.Start));
            return criteriaMgr.FindAll(criteria);
        }

        #endregion Customized Methods
    }
}