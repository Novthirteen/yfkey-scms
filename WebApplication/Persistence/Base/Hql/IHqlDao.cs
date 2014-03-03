using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NHibernate.Type;

namespace com.Sconit.Persistence.Hql
{
    public interface IHqlDao
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
    }
}
