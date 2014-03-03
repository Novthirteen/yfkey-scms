using System;
using System.Collections.Generic;
using System.Text;

namespace com.Sconit.Persistence
{
    /// <summary>
    /// Summary description for IDaoBase.
    /// </summary>
    /// <remarks>
    /// Contributed by Jackey <Jackey.ding@atosorigin.com>
    /// </remarks>

    public interface IDao
    {
        IList<T> FindAll<T>();

        IList<T> FindAll<T>(int firstRow, int maxRows);

        T FindById<T>(object id);

        object Create(object instance);

        void Update(object instance);       

        void Delete(object instance);
       
        void DeleteAll(Type type);

        void Save(object instance);
    }
}
