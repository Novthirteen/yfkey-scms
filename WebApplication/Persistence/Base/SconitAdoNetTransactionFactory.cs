using System.Collections;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Transaction;

namespace com.Sconit.Persistence
{
    public class SconitAdoNetTransactionFactory : ITransactionFactory
    {
        public ITransaction CreateTransaction(ISessionImplementor session)
        {
            return new SconitAdoTransaction(session);
        }

        public void Configure(IDictionary props)
        {
        }
    }
}
