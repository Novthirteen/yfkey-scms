using System;
using System.Collections.Generic;

using com.Sconit.Persistence;
using com.Sconit.Entity.Exception;

namespace com.Sconit.Service
{
    /// <summary>
    /// The base class of all Session Class.
    /// </summary>
    public class ArchSessionBase : IArchSession
    {
        public IArchNHDao daoBase { get; set; }

        public void setDaoBase()
        {
            //this.daoBase = daoBase;
        }

        public IList<T> FindAll<T>()
        {
            return daoBase.FindAll<T>();
        }

        public IList<T> FindAll<T>(int firstRow, int maxRows)
        {
            return daoBase.FindAll<T>(firstRow, maxRows);
        }

        public T FindById<T>(object id)
        {
            return daoBase.FindById<T>(id);
        }

        public object Create(object instance)
        {
            return daoBase.Create(instance);
        }

        public void Update(object instance)
        {
            daoBase.Update(instance);
        }

        public void Delete(object instance)
        {
            daoBase.Delete(instance);

        }

        public void DeleteAll(Type type)
        {
            daoBase.DeleteAll(type);
        }

        public void Save(object instance)
        {
            daoBase.Save(instance);
        }

        public void FlushSession()
        {
            daoBase.FlushSession();
        }

        public void CleanSession()
        {
            daoBase.CleanSession();
        }
    }
}
