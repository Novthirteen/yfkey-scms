using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace com.Sconit.Persistence
{
    /// <summary>
    /// Summary description for IDaoBase.
    /// </summary>
    /// <remarks>
    /// Contributed by Jackey <Jackey.ding@atosorigin.com>
    /// </remarks>

    public interface ISqlDao
    {

        int Create(string sql, SqlParameter[] commandParameters);

        int Update(string sql, SqlParameter[] commandParameters);

        int Delete(string sql, SqlParameter[] commandParameters);
       
    }
}
