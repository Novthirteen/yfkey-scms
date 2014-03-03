using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Mes.Dss.Entity
{
    public static class MesDssConstants
    {
        //0: 表示SCMS已更新数据
        //1:  表示FTPC已更新数据
        //2: 表示SCMS已删除数据
        public static readonly int SCMS_MES_FLAG_SCMS_UPDATED = 0;
        public static readonly int SCMS_MES_FLAG_FTPC_UPDATED = 1;
        public static readonly int SCMS_MES_FLAG_SCMS_DELETE = 2;

        //0: 表示SCMS已更新数据
        //1: 表示FTPC已更新数据
        //2: 表示FTPC正在更新数据
        public static readonly int SCMS_TABLEINDEX_FLAG_SCMS_UPDATED = 0;
        public static readonly int SCMS_TABLEINDEX_FLAG_FTPC_UPDATED = 1;
        public static readonly int SCMS_TABLEINDEX_FLAG_FTPC_UPDATING = 2;

        //0: 表示SCMS已更新数据
        //1: 表示FTPC已更新数据
        //2: 表示SCMS正在更新数据
        public static readonly int MES_SCMS_TABLEINDEX_FLAG_SCMS_UPDATED = 0;
        public static readonly int MES_SCMS_TABLEINDEX_FLAG_FTPC_UPDATED = 1;
        public static readonly int MES_SCMS_TABLEINDEX_FLAG_FTPC_UPDATING = 2;

        //0:表示SCMS已更新数据
        //1:表示FTPC已更新数据
        //2: 表示FTPC已删除数据
        public static readonly int MES_SCMS_FLAG_SCMS_UPDATED = 0;
        public static readonly int MES_SCMS_FLAG_FTPC_UPDATED = 1;
        public static readonly int MES_SCMS_FLAG_FTPC_DELETED = 2;


        public static readonly string SCMS_TABLEINDEX_TABLE_NAME_SCMS_PART = "SCMS_PART";
        public static readonly string MES_SCMS_TABLEINDEX_TABLE_NAME_MES_SCMS_STATION_SHELF = "MES_SCMS_STATION_SHELF";
        public static readonly string MES_SCMS_TABLEINDEX_TABLE_NAME_MES_SCMS_STATION_BOX = "MES_SCMS_STATION_BOX";
        public static readonly string MES_SCMS_TABLEINDEX_TABLE_NAME_MES_SCMS_COMPLETED_ORDER = "MES_SCMS_COMPLETED_ORDER";
        public static readonly string MES_SCMS_TABLEINDEX_TABLE_NAME_MES_SCMS_COMPLETED_BOX = "MES_SCMS_COMPLETED_BOX";
        public static readonly string MES_DSS_PARAM_SERVICENAME = "ServiceName";


    }
}
