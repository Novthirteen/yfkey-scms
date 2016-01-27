using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Persistence.Hql;
using NHibernate.Type;
using System.Collections;

namespace com.Sconit.Service.Hql.Impl
{
    public class HqlMgr : SessionBase, IHqlMgr
    {
        private IHqlDao hqlDao;

        public HqlMgr(IHqlDao hqlDao)
        {
            this.hqlDao = hqlDao;
        }

        public void Delete(string hqlString)
        {
            this.hqlDao.Delete(hqlString);
        }

        public void Delete(string hqlString, object value, IType type) {
            this.hqlDao.Delete(hqlString, value, type);
        }

        public void Delete(string hqlString, object[] values, IType[] types) {
            this.hqlDao.Delete(hqlString, values, types);
        }

        public IList FindAll(string hqlString) {
            return this.hqlDao.FindAllWithCustomQuery(hqlString);
        }

        public IList FindAll(string hqlString, object value)
        {
            return this.hqlDao.FindAllWithCustomQuery(hqlString, value);
        }

        public IList FindAll(string hqlString, object value, IType type)
        {
            return this.hqlDao.FindAllWithCustomQuery(hqlString, value, type);
        }

        public IList FindAll(string hqlString, object[] values)
        {
            return this.hqlDao.FindAllWithCustomQuery(hqlString, values);
        }

        public IList FindAll(string hqlString, object[] values, IType[] types)
        {
            return this.hqlDao.FindAllWithCustomQuery(hqlString, values, types);
        }

        public IList FindAll(string hqlString, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery(hqlString, firstRow, maxRows);
        }

        public IList FindAll(string hqlString, object value, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery(hqlString, value, firstRow, maxRows);
        }

        public IList FindAll(string hqlString, object value, IType type, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery(hqlString, value, type, firstRow, maxRows);
        }

        public IList FindAll(string hqlString, object[] values, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery(hqlString, values, firstRow, maxRows);
        }

        public IList FindAll(string hqlString, object[] values, IType[] type, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery(hqlString, values, type, firstRow, maxRows);
        }

        public IList<T> FindAll<T>(string hqlString)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString);
        }

        public IList<T> FindAll<T>(string hqlString, object value)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString, value);
        }

        public IList<T> FindAll<T>(string hqlString, object value, IType type)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString, value, type);
        }

        public IList<T> FindAll<T>(string hqlString, object[] values)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString, values);
        }

        public IList<T> FindAll<T>(string hqlString, object[] values, IType[] types)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString, values, types);
        }

        public IList<T> FindAll<T>(string hqlString, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString, firstRow, maxRows);
        }

        public IList<T> FindAll<T>(string hqlString, object value, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString, value, firstRow, maxRows);
        }

        public IList<T> FindAll<T>(string hqlString, object value, IType type, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString, value, type, firstRow, maxRows);
        }

        public IList<T> FindAll<T>(string hqlString, object[] values, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString, values, firstRow, maxRows);
        }

        public IList<T> FindAll<T>(string hqlString, object[] values, IType[] type, int firstRow, int maxRows)
        {
            return this.hqlDao.FindAllWithCustomQuery<T>(hqlString, values, type, firstRow, maxRows);
        }

        public IList FindAllWithNamedQuery(string namedQuery)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery);
        }

        public IList FindAllWithNamedQuery(string namedQuery, object value)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery, value);
        }

        public IList FindAllWithNamedQuery(string namedQuery, object value, IType type)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery, value, type);
        }

        public IList FindAllWithNamedQuery(string namedQuery, object[] values)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery, values);
        }

        public IList FindAllWithNamedQuery(string namedQuery, object[] values, IType[] types)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery, values, types);
        }

        public IList FindAllWithNamedQuery(string namedQuery, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery, firstRow, maxRows);
        }

        public IList FindAllWithNamedQuery(string namedQuery, object value, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery, value, firstRow, maxRows);
        }

        public IList FindAllWithNamedQuery(string namedQuery, object value, IType type, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery, value, type, firstRow, maxRows);
        }

        public IList FindAllWithNamedQuery(string namedQuery, object[] values, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery, values, firstRow, maxRows);
        }

        public IList FindAllWithNamedQuery(string namedQuery, object[] values, IType[] types, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery(namedQuery, values, types, firstRow, maxRows);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery, value);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, IType type)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery, value, type);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery, values);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, IType[] types)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery, values, types);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery, firstRow, maxRows);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery, value, firstRow, maxRows);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, IType type, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery, value, type, firstRow, maxRows);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery, values, firstRow, maxRows);
        }

        public IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, IType[] types, int firstRow, int maxRows)
        {
            return hqlDao.FindAllWithNamedQuery<T>(namedQuery, values, types, firstRow, maxRows);
        }

        public IList FindAllWithNativeSql(string sql)
        {
            return hqlDao.FindAllWithNativeSql(sql);
        }

        public IList FindAllWithNativeSql(string sql, object value)
        {
            return hqlDao.FindAllWithNativeSql(sql, value);
        }

        public IList FindAllWithNativeSql(string sql, object value, IType type)
        {
            return hqlDao.FindAllWithNativeSql(sql, value, type);
        }

        public IList FindAllWithNativeSql(string sql, object[] values)
        {
            return hqlDao.FindAllWithNativeSql(sql, values);
        }

        public IList FindAllWithNativeSql(string sql, object[] values, IType[] types)
        {
            return hqlDao.FindAllWithNativeSql(sql, values, types);
        }

        public IList<T> FindAllWithNativeSql<T>(string sql, IDictionary<string, NullableType> scalars)
        {
            return hqlDao.FindAllWithNativeSql<T>(sql, scalars);
        }

        public IList<T> FindAllWithNativeSql<T>(string sql, object value, IDictionary<string, NullableType> scalars)
        {
            return hqlDao.FindAllWithNativeSql<T>(sql, value, scalars);
        }

        public IList<T> FindAllWithNativeSql<T>(string sql, object value, IType type, IDictionary<string, NullableType> scalars)
        {
            return hqlDao.FindAllWithNativeSql<T>(sql, value, type, scalars);
        }

        public IList<T> FindAllWithNativeSql<T>(string sql, object[] values, IDictionary<string, NullableType> scalars)
        {
            return hqlDao.FindAllWithNativeSql<T>(sql, values, scalars);
        }

        public IList<T> FindAllWithNativeSql<T>(string sql, object[] values, IType[] types, IDictionary<string, NullableType> scalars)
        {
            return hqlDao.FindAllWithNativeSql<T>(sql, values, types, scalars);
        }

        public IList<T> FindEntityWithNativeSql<T>(string sql)
        {
            return hqlDao.FindEntityWithNativeSql<T>(sql);
        }

        public IList<T> FindEntityWithNativeSql<T>(string sql, object value)
        {
            return hqlDao.FindEntityWithNativeSql<T>(sql, value);
        }

        public IList<T> FindEntityWithNativeSql<T>(string sql, object value, IType type)
        {
            return hqlDao.FindEntityWithNativeSql<T>(sql, value, type);
        }

        public IList<T> FindEntityWithNativeSql<T>(string sql, object[] values)
        {
            return hqlDao.FindEntityWithNativeSql<T>(sql, values);
        }

        public IList<T> FindEntityWithNativeSql<T>(string sql, object[] values, IType[] types)
        {
            return hqlDao.FindEntityWithNativeSql<T>(sql, values, types);
        }
    }
}