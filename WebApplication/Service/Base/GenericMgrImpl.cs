

namespace com.Sconit.Service
{
    #region retrive
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Castle.Services.Transaction;
    using com.Sconit.Persistence;
    using NHibernate.Type;
    using System.Data.SqlClient;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    [Transactional]
    public class GenericMgrImpl :  IGenericMgr
    {
        public GenericMgrImpl(INHDao dao)
        {
            this.dao = dao;
        }
        /// <summary>
        /// NHibernate数据获取对象
        /// </summary>
        private INHDao dao { get; set; }
        public ISqlDao sqlDao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        [Transaction(TransactionMode.Requires)]
        public void Save(object instance)
        {
            dao.Save(instance);
        }

        [Transaction(TransactionMode.Requires)]
        public void Create(object instance)
        {
            dao.Create(instance);
        }

        [Transaction(TransactionMode.Requires)]
        public void Update(object instance)
        {
            dao.Update(instance);
        }


        [Transaction(TransactionMode.Requires)]
        public void Delete(object instance)
        {
            dao.Delete(instance);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteById<T>(object id)
        {
            object instance = dao.FindById<T>(id);
            dao.Delete(instance);
        }

        [Transaction(TransactionMode.Requires)]
        public void Delete(IList instances)
        {
            if (instances != null && instances.Count > 0)
            {
                foreach (object inst in instances)
                {
                    dao.Delete(inst);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void Delete<T>(IList<T> instances)
        {
            if (instances != null && instances.Count > 0)
            {
                foreach (object inst in instances)
                {
                    dao.Delete(inst);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteAll(Type type)
        {
            dao.DeleteAll(type);
        }

        public void FlushSession()
        {
            this.dao.FlushSession();
        }

        public void CleanSession()
        {
            dao.CleanSession();
        }

        [Transaction(TransactionMode.Requires)]
        public void Delete(string hqlString)
        {
            dao.Delete(hqlString);
        }

        //public void Delete(string hqlString, object value)
        //{
        //    dao.Delete(hqlString, value);
        //}

        [Transaction(TransactionMode.Requires)]
        public void Delete(string hqlString, object value, IType type)
        {
            dao.Delete(hqlString, value, type);
        }

        //public void Delete(string hqlString, object[] values)
        //{
        //    dao.Delete(hqlString, values);
        //}

        [Transaction(TransactionMode.Requires)]
        public void Delete(string hqlString, object[] values, IType[] types)
        {
            dao.Delete(hqlString, values, types);
        }

        public IList<T> FindAllWithCustomQuery<T>(string queryString) 
        {
            return dao.FindAllWithCustomQuery<T>(queryString);
        }

        public IList<T> FindAllWithCustomQuery<T>(string queryString, object value)
        {
            return dao.FindAllWithCustomQuery<T>(queryString, value);
        }

        public IList<T> FindAllWithCustomQuery<T>(string queryString, object value, IType type)
        { 
             return dao.FindAllWithCustomQuery<T>(queryString,value,type);
        }

        public IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values)
        {
            return dao.FindAllWithCustomQuery<T>(queryString, values);
        }

        public IList<T> FindAllWithCustomQuery<T>(string queryString, object[] values, IType[] types)
        {
            return dao.FindAllWithCustomQuery<T>(queryString, values, types);
        }
    }
}
