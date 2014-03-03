using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace com.Sconit.Persistence
{
    /// <summary>
    /// Summary description for GenericDao.
    /// </summary>
    /// <remarks>
    ///  Contributed by Jackey <Jackey.ding@atosorigin.com>
    /// </remarks>
    /// 
    public class SqlDaoBase : ISqlDao
    {
        private SqlHelperDao sqlHelperDao;
        public SqlDaoBase(SqlHelperDao sqlHelperDao)
		{
            this.sqlHelperDao = sqlHelperDao;
		}

        #region ISqlDAOBase Members

        public virtual int Delete(string sqlString, SqlParameter[] commandParameters)
        {
            return sqlHelperDao.ExecuteSql(sqlString, commandParameters);
        }

        public virtual int Create(string sqlString, SqlParameter[] commandParameters)
        {
            return sqlHelperDao.ExecuteSql(sqlString, commandParameters);
        }

        public virtual int Update(string sqlString, SqlParameter[] commandParameters)
        {
            return sqlHelperDao.ExecuteSql(sqlString, commandParameters);
        }

        #endregion


        #region protected methods

        #endregion
    }
}
