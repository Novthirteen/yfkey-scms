using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Type;
using System.Collections;

namespace com.Sconit.Service.Hql
{
    public interface IHqlMgr : ISession
    {
        void Delete(string hqlString);

        void Delete(string hqlString, object value, IType type);

        void Delete(string hqlString, object[] values, IType[] types);

        IList FindAll(string hqlString);

        IList FindAll(string hqlString, object value);

        IList FindAll(string hqlString, object value, IType type);

        IList FindAll(string hqlString, object[] values);

        IList FindAll(string hqlString, object[] values, IType[] types);

        IList FindAll(string hqlString, int firstRow, int maxRows);

        IList FindAll(string hqlString, object value, int firstRow, int maxRows);

        IList FindAll(string hqlString, object value, IType type, int firstRow, int maxRows);

        IList FindAll(string hqlString, object[] values, int firstRow, int maxRows);

        IList FindAll(string hqlString, object[] values, IType[] type, int firstRow, int maxRows);

        IList<T> FindAll<T>(string hqlString);

        IList<T> FindAll<T>(string hqlString, object value);

        IList<T> FindAll<T>(string hqlString, object value, IType type);

        IList<T> FindAll<T>(string hqlString, object[] values);

        IList<T> FindAll<T>(string hqlString, object[] values, IType[] types);

        IList<T> FindAll<T>(string hqlString, int firstRow, int maxRows);

        IList<T> FindAll<T>(string hqlString, object value, int firstRow, int maxRows);

        IList<T> FindAll<T>(string hqlString, object value, IType type, int firstRow, int maxRows);

        IList<T> FindAll<T>(string hqlString, object[] values, int firstRow, int maxRows);

        IList<T> FindAll<T>(string hqlString, object[] values, IType[] type, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery);

        IList FindAllWithNamedQuery(string namedQuery, object value);

        IList FindAllWithNamedQuery(string namedQuery, object value, IType types);

        IList FindAllWithNamedQuery(string namedQuery, object[] values);

        IList FindAllWithNamedQuery(string namedQuery, object[] values, IType[] types);

        IList FindAllWithNamedQuery(string namedQuery, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery, object value, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery, object value, IType type, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery, object[] values, int firstRow, int maxRows);

        IList FindAllWithNamedQuery(string namedQuery, object[] values, IType[] type, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, IType type);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, IType[] types);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, IType type, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, int firstRow, int maxRows);

        IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, IType[] types, int firstRow, int maxRows);

        IList FindAllWithNativeSql(string sql);

        IList FindAllWithNativeSql(string sql, object value);

        IList FindAllWithNativeSql(string sql, object value, IType type);

        IList FindAllWithNativeSql(string sql, object[] values);

        IList FindAllWithNativeSql(string sql, object[] values, IType[] types);

        IList<T> FindAllWithNativeSql<T>(string sql);

        IList<T> FindAllWithNativeSql<T>(string sql, object value);

        IList<T> FindAllWithNativeSql<T>(string sql, object value, IType type);

        IList<T> FindAllWithNativeSql<T>(string sql, object[] values);

        IList<T> FindAllWithNativeSql<T>(string sql, object[] values, IType[] types);

        IList<T> FindEntityWithNativeSql<T>(string sql);

        IList<T> FindEntityWithNativeSql<T>(string sql, object value);

        IList<T> FindEntityWithNativeSql<T>(string sql, object value, IType type);

        IList<T> FindEntityWithNativeSql<T>(string sql, object[] values);

        IList<T> FindEntityWithNativeSql<T>(string sql, object[] values, IType[] types);
    }
}
