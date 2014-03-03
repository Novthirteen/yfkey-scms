using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace com.Sconit.Persistence
{
    /// <summary>
    /// Summary description for IDaoBase.
    /// </summary>
    /// <remarks>
    /// Contributed by Jackey <Jackey.ding@atosorigin.com>
    /// </remarks>

    public interface ISqlHelperDao
    {

        int Create(string sql, SqlParameter[] commandParameters);

        int Update(string sql, SqlParameter[] commandParameters);

        int Delete(string sql, SqlParameter[] commandParameters);

        int SearchCount(string sql, SqlParameter[] commandParameters);

        int ExecuteSql(string commandText, SqlParameter[] commandParameters);

        int ExecuteStoredProcedure(string commandText, SqlParameter[] commandParameters);

        DataSet GetDatasetBySql(string commandText, SqlParameter[] commandParameters);

        DataSet GetDatasetByStoredProcedure(string commandText, SqlParameter[] commandParameters);
    }
}
