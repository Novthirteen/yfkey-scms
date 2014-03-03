using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Expression;
using NHibernate.Type;
using System.Collections;

namespace com.Sconit.Persistence
{
    /// <summary>
    /// Summary description for INHDaoBase.
    /// </summary>
    /// <remarks>
    /// Contributed by Jackey <Jackey.ding@atosorigin.com>
    /// </remarks>
    
    public interface INHDao : IDao
    {
        void Delete(string hqlString);

        void Delete(string hqlString, object value, IType type);

        void Delete(string hqlString, object[] values, IType[] type);

        IList FindAllWithCustomQuery(string queryString);

        IList FindAllWithCustomQuery(string queryString, object value);

        IList FindAllWithCustomQuery(string queryString, object value, IType type);

        IList FindAllWithCustomQuery(string queryString, object[] values);

        IList FindAllWithCustomQuery(string queryString, object[] values, IType[] types);

        IList FindAllWithCustomQuery(string queryString, int firstRow, int maxRows);

        IList FindAllWithCustomQuery(string queryString, object value, int firstRow, int maxRows);

        IList FindAllWithCustomQuery(string queryString, object value, IType type, int firstRow, int maxRows);

        IList FindAllWithCustomQuery(string queryString, object[] values, int firstRow, int maxRows);

        IList FindAllWithCustomQuery(string queryString, object[] values, IType[] type, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery);

        IList FindAllWithNamedQuery(string namedQuery, object value);

        IList FindAllWithNamedQuery(string namedQuery, object value, IType type);

        IList FindAllWithNamedQuery(string namedQuery, object[] values);

        IList FindAllWithNamedQuery(string namedQuery, object[] values, IType[] types);

        IList FindAllWithNamedQuery(string namedQuery, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery, object value, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery, object value, IType type, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery, object[] values, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery, object[] values, IType[] type, int firstRow, int maxRows);

        IList<T> FindAll<T>(ICriterion[] criterias);

        IList<T> FindAll<T>(ICriterion[] criterias, int firstRow, int maxRows);

        IList<T> FindAll<T>(ICriterion[] criterias, Order[] sortItems);

        IList<T> FindAll<T>(ICriterion[] criterias, Order[] sortItems, int firstRow, int maxRows);

        IList<T> FindAllWithCustomQuery<T>(string queryString);

        IList<T> FindAllWithCustomQuery<T>(string queryString, object value);

        IList<T> FindAllWithCustomQuery<T>(string queryString, object value, IType type);

        IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values);

        IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values, IType[] types);

        IList<T> FindAllWithCustomQuery<T>(string queryString, int firstRow, int maxRows);

        IList<T> FindAllWithCustomQuery<T>(string queryString, object value, int firstRow, int maxRows);

        IList<T> FindAllWithCustomQuery<T>(string queryString, object value, IType type, int firstRow, int maxRows);

        IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values, int firstRow, int maxRows);

        IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values, IType[] type, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, IType type);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, IType[] types);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, IType type, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, IType[] type, int firstRow, int maxRows);

        void InitializeLazyProperties(object instance);

        void InitializeLazyProperty(object instance, string propertyName);

        void FlushSession();

        void CleanSession();
    }
}
