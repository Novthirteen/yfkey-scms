using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Type;
using System.Collections;
using Castle.Facilities.NHibernateIntegration;

namespace com.Sconit.Persistence.Hql.NH
{
    public class NHHqlDao : NHDaoBase, IHqlDao
    {
        public NHHqlDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public virtual IList FindAll(string hqlString)
        {
            return FindAllWithCustomQuery(hqlString);
        }

        public virtual IList FindAll(string hqlString, object value)
        {
            return FindAllWithCustomQuery(hqlString, value);
        }

        public virtual IList FindAll(string hqlString, object value, IType type)
        {
            return FindAllWithCustomQuery(hqlString, value, type);
        }

        public virtual IList FindAll(string hqlString, object[] values)
        {
            return FindAllWithCustomQuery(hqlString, values);
        }

        public virtual IList FindAll(string hqlString, object[] values, IType[] types)
        {
            return FindAllWithCustomQuery(hqlString, values, types);
        }

        public virtual IList FindAll(string hqlString, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery(hqlString, firstRow, maxRows);
        }

        public virtual IList FindAll(string hqlString, object value, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery(hqlString, value, firstRow, maxRows);
        }

        public virtual IList FindAll(string hqlString, object value, IType type, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery(hqlString, value, type, firstRow, maxRows);
        }

        public virtual IList FindAll(string hqlString, object[] values, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery(hqlString, values, firstRow, maxRows);
        }

        public virtual IList FindAll(string hqlString, object[] values, IType[] types, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery(hqlString, values, types, firstRow, maxRows);
        }

        public virtual IList<T> FindAll<T>(string hqlString)
        {
            return FindAllWithCustomQuery<T>(hqlString);
        }

        public virtual IList<T> FindAll<T>(string hqlString, object value)
        {
            return FindAllWithCustomQuery<T>(hqlString, value);
        }

        public virtual IList<T> FindAll<T>(string hqlString, object value, IType type)
        {
            return FindAllWithCustomQuery<T>(hqlString, value, type);
        }

        public virtual IList<T> FindAll<T>(string hqlString, object[] values)
        {
            return FindAllWithCustomQuery<T>(hqlString, values);
        }

        public virtual IList<T> FindAll<T>(string hqlString, object[] values, IType[] types)
        {
            return FindAllWithCustomQuery<T>(hqlString, values, types);
        }

        public virtual IList<T> FindAll<T>(string hqlString, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery<T>(hqlString, firstRow, maxRows);
        }

        public virtual IList<T> FindAll<T>(string hqlString, object value, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery<T>(hqlString, value, firstRow, maxRows);
        }

        public virtual IList<T> FindAll<T>(string hqlString, object value, IType type, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery<T>(hqlString, value, type, firstRow, maxRows);
        }

        public virtual IList<T> FindAll<T>(string hqlString, object[] values, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery<T>(hqlString, values, firstRow, maxRows);
        }

        public virtual IList<T> FindAll<T>(string hqlString, object[] values, IType[] types, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery<T>(hqlString, values, types, firstRow, maxRows);
        }
    }
}
