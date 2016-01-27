using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using NHibernate.Expression;

namespace com.Sconit.Persistence.Hql.NH
{
    public class NHHqlDao : NHDaoBase, IHqlDao
    {
        public NHHqlDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public virtual IList<T> FindAll<T>()
        {
            return FindAll<T>(int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAll<T>(int firstRow, int maxRows)
        {
            using (ISession session = GetSession())
            {
                try
                {
                    ICriteria criteria = session.CreateCriteria(typeof(T));

                    if (firstRow != int.MinValue) criteria.SetFirstResult(firstRow);
                    if (maxRows != int.MinValue) criteria.SetMaxResults(maxRows);
                    IList<T> result = criteria.List<T>();
                    //if (result == null || result.Count == 0) return null;

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform FindAll for " + typeof(T).Name, ex);
                }
            }
        }

        public virtual T FindById<T>(object id)
        {
            using (ISession session = GetSession())
            {
                try
                {
                    return session.Load<T>(id);
                }
                catch (ObjectNotFoundException)
                {
                    throw;
                    //return default(T);
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform FindByPrimaryKey for " + typeof(T).Name, ex);
                }
            }
        }

        public virtual IList FindAllWithCustomQuery(string queryString)
        {
            return FindAllWithCustomQuery(queryString, (object[])null, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithCustomQuery(string queryString, object value)
        {
            return FindAllWithCustomQuery(queryString, new object[] { value }, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithCustomQuery(string queryString, object value, IType type)
        {
            return FindAllWithCustomQuery(queryString, new object[] { value }, new IType[] { type }, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithCustomQuery(string queryString, object[] values)
        {
            return FindAllWithCustomQuery(queryString, values, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithCustomQuery(string queryString, object[] values, IType[] types)
        {
            return FindAllWithCustomQuery(queryString, values, types, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithCustomQuery(string queryString, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery(queryString, (object[])null, (IType[])null, firstRow, maxRows);
        }

        public virtual IList FindAllWithCustomQuery(string queryString, object value, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery(queryString, new object[] { value }, (IType[])null, firstRow, maxRows);
        }

        public virtual IList FindAllWithCustomQuery(string queryString, object value, IType type, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery(queryString, new object[] { value }, new IType[] { type }, firstRow, maxRows);
        }

        public virtual IList FindAllWithCustomQuery(string queryString, object[] values, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery(queryString, values, (IType[])null, firstRow, maxRows);
        }

        public virtual IList FindAllWithCustomQuery(string queryString, object[] values, IType[] types, int firstRow, int maxRows)
        {
            if (queryString == null || queryString.Length == 0) throw new ArgumentNullException("queryString");
            if (values != null && types != null && types.Length != values.Length) throw new ArgumentException("Length of values array must match length of types array");

            using (ISession session = GetSession())
            {
                try
                {
                    IQuery query = session.CreateQuery(queryString);
                    if (values != null)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (types != null && types[i] != null)
                            {
                                query.SetParameter(i, values[i], types[i]);
                            }
                            else
                            {
                                query.SetParameter(i, values[i]);
                            }
                        }
                    }

                    if (firstRow != int.MinValue) query.SetFirstResult(firstRow);
                    if (maxRows != int.MinValue) query.SetMaxResults(maxRows);
                    IList result = query.List();
                    //if (result == null || result.Count == 0) return null;

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform Find for custom query : " + queryString, ex);
                }
            }
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery)
        {
            return FindAllWithNamedQuery(namedQuery, (object[])null, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery, object value)
        {
            return FindAllWithNamedQuery(namedQuery, new object[] { value }, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery, object value, IType type)
        {
            return FindAllWithNamedQuery(namedQuery, new object[] { value }, new IType[] { type }, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery, object[] values)
        {
            return FindAllWithNamedQuery(namedQuery, values, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery, object[] values, IType[] types)
        {
            return FindAllWithNamedQuery(namedQuery, values, types, int.MinValue, int.MinValue);
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery, int firstRow, int maxRows)
        {
            return FindAllWithNamedQuery(namedQuery, (object[])null, (IType[])null, firstRow, maxRows);
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery, object value, int firstRow, int maxRows)
        {
            return FindAllWithNamedQuery(namedQuery, new object[] { value }, (IType[])null, firstRow, maxRows);
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery, object value, IType type, int firstRow, int maxRows)
        {
            return FindAllWithNamedQuery(namedQuery, new object[] { value }, new IType[] { type }, firstRow, maxRows);
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery, object[] values, int firstRow, int maxRows)
        {
            return FindAllWithNamedQuery(namedQuery, values, (IType[])null, firstRow, maxRows);
        }

        public virtual IList FindAllWithNamedQuery(string namedQuery, object[] values, IType[] types, int firstRow, int maxRows)
        {
            if (namedQuery == null || namedQuery.Length == 0) throw new ArgumentNullException("queryString");
            if (values != null && types != null && types.Length != values.Length) throw new ArgumentException("Length of values array must match length of types array");

            using (ISession session = GetSession())
            {
                try
                {
                    IQuery query = session.GetNamedQuery(namedQuery);
                    if (query == null) throw new ArgumentException("Cannot find named query", "namedQuery");
                    if (values != null)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (types != null && types[i] != null)
                            {
                                query.SetParameter(i, values[i], types[i]);
                            }
                            else
                            {
                                query.SetParameter(i, values[i]);
                            }
                        }
                    }

                    if (firstRow != int.MinValue) query.SetFirstResult(firstRow);
                    if (maxRows != int.MinValue) query.SetMaxResults(maxRows);
                    IList result = query.List();
                    //if (result == null || result.Count == 0) return null;

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform Find for named query : " + namedQuery, ex);
                }
            }
        }

        public virtual IList<T> FindAll<T>(ICriterion[] criterias)
        {
            return FindAll<T>(criterias, null, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAll<T>(ICriterion[] criterias, int firstRow, int maxRows)
        {
            return FindAll<T>(criterias, null, firstRow, maxRows);
        }

        public virtual IList<T> FindAll<T>(ICriterion[] criterias, Order[] sortItems)
        {
            return FindAll<T>(criterias, sortItems, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAll<T>(ICriterion[] criterias, Order[] sortItems, int firstRow, int maxRows)
        {
            using (ISession session = GetSession())
            {
                try
                {
                    ICriteria criteria = session.CreateCriteria(typeof(T));

                    if (criterias != null)
                    {
                        foreach (ICriterion cond in criterias)
                            criteria.Add(cond);
                    }

                    if (sortItems != null)
                    {
                        foreach (Order order in sortItems)
                            criteria.AddOrder(order);
                    }

                    if (firstRow != int.MinValue) criteria.SetFirstResult(firstRow);
                    if (maxRows != int.MinValue) criteria.SetMaxResults(maxRows);
                    IList<T> result = criteria.List<T>();
                    //if (result == null || result.Count == 0) return null;

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform FindAll for " + typeof(T).Name, ex);
                }
            }
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString)
        {
            return FindAllWithCustomQuery<T>(queryString, (object[])null, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, object value)
        {
            return FindAllWithCustomQuery<T>(queryString, new object[] { value }, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, object value, IType type)
        {
            return FindAllWithCustomQuery<T>(queryString, new object[] { value }, new IType[] { type }, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values)
        {
            return FindAllWithCustomQuery<T>(queryString, values, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values, IType[] types)
        {
            return FindAllWithCustomQuery<T>(queryString, values, types, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery<T>(queryString, (object[])null, (IType[])null, firstRow, maxRows);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, object value, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery<T>(queryString, new object[] { value }, (IType[])null, firstRow, maxRows);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, object value, IType type, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery<T>(queryString, new object[] { value }, new IType[] { type }, firstRow, maxRows);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values, int firstRow, int maxRows)
        {
            return FindAllWithCustomQuery<T>(queryString, values, (IType[])null, firstRow, maxRows);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values, IType[] types, int firstRow, int maxRows)
        {
            if (queryString == null || queryString.Length == 0) throw new ArgumentNullException("queryString");
            if (values != null && types != null && types.Length != values.Length) throw new ArgumentException("Length of values array must match length of types array");

            using (ISession session = GetSession())
            {
                try
                {
                    IQuery query = session.CreateQuery(queryString);
                    if (values != null)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (types != null && types[i] != null)
                            {
                                query.SetParameter(i, values[i], types[i]);
                            }
                            else
                            {
                                query.SetParameter(i, values[i]);
                            }
                        }
                    }

                    if (firstRow != int.MinValue) query.SetFirstResult(firstRow);
                    if (maxRows != int.MinValue) query.SetMaxResults(maxRows);
                    IList<T> result = query.List<T>();
                    //if (result == null || result.Count == 0) return null;

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform Find for custom query : " + queryString, ex);
                }
            }
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery)
        {
            return FindAllWithNamedQuery<T>(namedQuery, (object[])null, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value)
        {
            return FindAllWithNamedQuery<T>(namedQuery, new object[] { value }, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, IType type)
        {
            return FindAllWithNamedQuery<T>(namedQuery, new object[] { value }, new IType[] { type }, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values)
        {
            return FindAllWithNamedQuery<T>(namedQuery, values, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, IType[] types)
        {
            return FindAllWithNamedQuery<T>(namedQuery, values, types, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, int firstRow, int maxRows)
        {
            return FindAllWithNamedQuery<T>(namedQuery, (object[])null, (IType[])null, firstRow, maxRows);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, int firstRow, int maxRows)
        {
            return FindAllWithNamedQuery<T>(namedQuery, new object[] { value }, (IType[])null, firstRow, maxRows);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object value, IType type, int firstRow, int maxRows)
        {
            return FindAllWithNamedQuery<T>(namedQuery, new object[] { value }, new IType[] { type }, firstRow, maxRows);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, int firstRow, int maxRows)
        {
            return FindAllWithNamedQuery<T>(namedQuery, values, (IType[])null, firstRow, maxRows);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, IType[] types, int firstRow, int maxRows)
        {
            if (namedQuery == null || namedQuery.Length == 0) throw new ArgumentNullException("queryString");
            if (values != null && types != null && types.Length != values.Length) throw new ArgumentException("Length of values array must match length of types array");

            using (ISession session = GetSession())
            {
                try
                {
                    IQuery query = session.GetNamedQuery(namedQuery);
                    if (query == null) throw new ArgumentException("Cannot find named query", "namedQuery");
                    if (values != null)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (types != null && types[i] != null)
                            {
                                query.SetParameter(i, values[i], types[i]);
                            }
                            else
                            {
                                query.SetParameter(i, values[i]);
                            }
                        }
                    }

                    if (firstRow != int.MinValue) query.SetFirstResult(firstRow);
                    if (maxRows != int.MinValue) query.SetMaxResults(maxRows);
                    IList<T> result = query.List<T>();
                    //if (result == null || result.Count == 0) return null;

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform Find for named query : " + namedQuery, ex);
                }
            }
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string namedQuery, IDictionary<string, object> param)
        {
            return FindAllWithCustomQuery<T>(namedQuery, param, (IType[])null, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string namedQuery, IDictionary<string, object> param, IType[] types)
        {
            return FindAllWithCustomQuery<T>(namedQuery, param, types, int.MinValue, int.MinValue);
        }

        public virtual IList<T> FindAllWithCustomQuery<T>(string queryString, IDictionary<string, object> param, IType[] types, int firstRow, int maxRows)
        {

            if (queryString == null || queryString.Length == 0) throw new ArgumentNullException("queryString");
            if (param != null && types != null && types.Length != param.Count) throw new ArgumentException("Length of param array must match length of types array");

            using (ISession session = GetSession())
            {
                try
                {
                    IQuery query = session.CreateQuery(queryString);
                    if (param != null)
                    {
                        int i = 0;
                        foreach (string key in param.Keys)
                        {
                            if (param[key] is IList)
                            {
                                if (types != null && types[i] != null)
                                {
                                    query.SetParameterList(key, (IEnumerable)param[key], types[i]);
                                }
                                else
                                {
                                    query.SetParameterList(key, (IEnumerable)param[key]);
                                }
                            }
                            else
                            {
                                if (types != null && types[i] != null)
                                {
                                    query.SetParameter(key, param[key], types[i]);
                                }
                                else
                                {
                                    query.SetParameter(key, param[key]);
                                }
                            }
                            i++;
                        }

                    }

                    if (firstRow != int.MinValue) query.SetFirstResult(firstRow);
                    if (maxRows != int.MinValue) query.SetMaxResults(maxRows);
                    IList<T> result = query.List<T>();
                    //if (result == null || result.Count == 0) return null;

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform Find for custom query : " + queryString, ex);
                }
            }
        }

        public IList FindAll(DetachedCriteria criteria)
        {
            return criteria.GetExecutableCriteria(GetSession()).List();
        }

        public IList FindAll(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            ICriteria c = criteria.GetExecutableCriteria(GetSession());
            c.SetFirstResult(firstRow);
            c.SetMaxResults(maxRows);
            return c.List();
        }

        public IList<T> FindAll<T>(DetachedCriteria criteria)
        {
            return criteria.GetExecutableCriteria(GetSession()).List<T>();
        }

        public IList<T> FindAll<T>(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            ICriteria c = criteria.GetExecutableCriteria(GetSession());
            c.SetFirstResult(firstRow);
            c.SetMaxResults(maxRows);
            return c.List<T>();
        }

        public IList FindAllWithNativeSql(string sql)
        {
            return FindAllWithNativeSql(sql, (object[])null, (IType[])null);
        }

        public IList FindAllWithNativeSql(string sql, object value)
        {
            return FindAllWithNativeSql(sql, new object[] { value }, (IType[])null);
        }

        public IList FindAllWithNativeSql(string sql, object value, IType type)
        {
            return FindAllWithNativeSql(sql, new object[] { value }, new IType[] { type });

        }

        public IList FindAllWithNativeSql(string sql, object[] values)
        {
            return FindAllWithNativeSql(sql, values, (IType[])null);
        }

        public IList FindAllWithNativeSql(string sql, object[] values, IType[] types)
        {
            if (sql == null || sql.Length == 0) throw new ArgumentNullException("queryString");
            if (values != null && types != null && types.Length != values.Length) throw new ArgumentException("Length of values array must match length of types array");

            using (ISession session = GetSession())
            {
                try
                {
                    ISQLQuery query = session.CreateSQLQuery(sql);
                    if (values != null)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (types != null && types[i] != null)
                            {
                                query.SetParameter(i, values[i], types[i]);
                            }
                            else
                            {
                                query.SetParameter(i, values[i]);
                            }
                        }
                    }

                    query.AddScalar("maxId", NHibernateUtil.Int32);

                    IList result = query.List();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform Find for custom query : " + sql, ex);
                }
            }
        }

        public IList<T> FindAllWithNativeSql<T>(string sql, IDictionary<string, NullableType> scalars)
        {
            return FindAllWithNativeSql<T>(sql, (object[])null, (IType[])null, scalars);
        }

        public IList<T> FindAllWithNativeSql<T>(string sql, object value, IDictionary<string, NullableType> scalars)
        {
            return FindAllWithNativeSql<T>(sql, new object[] { value }, (IType[])null, scalars);
        }

        public IList<T> FindAllWithNativeSql<T>(string sql, object value, IType type, IDictionary<string, NullableType> scalars)
        {
            return FindAllWithNativeSql<T>(sql, new object[] { value }, new IType[] { type }, scalars);

        }

        public IList<T> FindAllWithNativeSql<T>(string sql, object[] values, IDictionary<string, NullableType> scalars)
        {
            return FindAllWithNativeSql<T>(sql, values, (IType[])null, scalars);
        }

        public IList<T> FindAllWithNativeSql<T>(string sql, object[] values, IType[] types, IDictionary<string, NullableType> scalars)
        {
            if (sql == null || sql.Length == 0) throw new ArgumentNullException("queryString");
            if (values != null && types != null && types.Length != values.Length) throw new ArgumentException("Length of values array must match length of types array");

            using (ISession session = GetSession())
            {
                try
                {
                    ISQLQuery query = session.CreateSQLQuery(sql);
                    if (values != null)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (types != null && types[i] != null)
                            {
                                query.SetParameter(i, values[i], types[i]);
                            }
                            else
                            {
                                query.SetParameter(i, values[i]);
                            }
                        }
                    }

                    foreach (string column in scalars.Keys)
                    {
                        query.AddScalar(column, scalars[column]);
                    }

                    IList<T> result = query.List<T>();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform Find for custom query : " + sql, ex);
                }
            }
        }

        public IList<T> FindEntityWithNativeSql<T>(string sql)
        {
            return FindEntityWithNativeSql<T>(sql, (object[])null, (IType[])null);
        }

        public IList<T> FindEntityWithNativeSql<T>(string sql, object value)
        {
            return FindEntityWithNativeSql<T>(sql, new object[] { value }, (IType[])null);
        }

        public IList<T> FindEntityWithNativeSql<T>(string sql, object value, IType type)
        {
            return FindEntityWithNativeSql<T>(sql, new object[] { value }, new IType[] { type });

        }

        public IList<T> FindEntityWithNativeSql<T>(string sql, object[] values)
        {
            return FindEntityWithNativeSql<T>(sql, values, (IType[])null);
        }

        public IList<T> FindEntityWithNativeSql<T>(string sql, object[] values, IType[] types)
        {
            if (sql == null || sql.Length == 0) throw new ArgumentNullException("queryString");
            if (values != null && types != null && types.Length != values.Length) throw new ArgumentException("Length of values array must match length of types array");

            using (ISession session = GetSession())
            {
                try
                {
                    ISQLQuery query = session.CreateSQLQuery(sql).AddEntity(typeof(T));
                    if (values != null)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (types != null && types[i] != null)
                            {
                                query.SetParameter(i, values[i], types[i]);
                            }
                            else
                            {
                                query.SetParameter(i, values[i]);
                            }
                        }
                    }

                    IList<T> result = query.List<T>();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform Find for custom query : " + sql, ex);
                }
            }
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, string[] parameterNames)
        {
            return FindAllWithNamedQuery<T>(namedQuery, values, (IType[])null, parameterNames);
        }

        public virtual IList<T> FindAllWithNamedQuery<T>(string namedQuery, object[] values, IType[] types, string[] parameterNames)
        {
            if (namedQuery == null || namedQuery.Length == 0) throw new ArgumentNullException("queryString");
            if (values != null && types != null && types.Length != values.Length) throw new ArgumentException("Length of values array must match length of types array");

            using (ISession session = GetSession())
            {
                try
                {
                    IQuery query = session.GetNamedQuery(namedQuery);
                    if (query == null) throw new ArgumentException("Cannot find named query", "namedQuery");
                    if (values != null)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (types != null && types[i] != null)
                            {
                                query.SetParameter(parameterNames[i], values[i], types[i]);
                            }
                            else
                            {
                                query.SetParameter(parameterNames[i], values[i]);
                            }
                        }
                    }

                    IList<T> result = query.List<T>();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new DataException("Could not perform Find for named query : " + namedQuery, ex);
                }
            }
        }
    }
}
