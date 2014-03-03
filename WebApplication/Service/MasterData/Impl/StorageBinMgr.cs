using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class StorageBinMgr : StorageBinBaseMgr, IStorageBinMgr
    {
        private ICriteriaMgr criteriaMgr;

        public StorageBinMgr(IStorageBinDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        public IList<StorageBin> GetStorageBin(StorageArea Area)
        {
            return GetStorageBin(Area.Code);
        }

        public IList<StorageBin> GetStorageBinByLocation(string location)
        {
            DetachedCriteria criteria = DetachedCriteria.For<StorageBin>();
            criteria.CreateAlias("Area.Location", "location");
            criteria.Add(Expression.Eq("location.Code", location));
            return this.criteriaMgr.FindAll<StorageBin>(criteria);
        }

        public IList<StorageBin> GetStorageBin(string AreaCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<StorageBin>();
            criteria.Add(Expression.Eq("Area.Code", AreaCode));
            return this.criteriaMgr.FindAll<StorageBin>(criteria);
        }

        public IList<StorageBin> GetStorageBin(Location location)
        {
            DetachedCriteria criteria = DetachedCriteria.For<StorageBin>();
            criteria.CreateAlias("Area.Location", "location");
            criteria.Add(Expression.Eq("location.Code", location.Code));
            return this.criteriaMgr.FindAll<StorageBin>(criteria);
        }

        public StorageBin CheckAndLoadStorageBin(string binCode)
        {
            StorageBin storageBin = this.LoadStorageBin(binCode);
            if (storageBin == null)
            {
                throw new BusinessErrorException("Warehouse.Error.BinNotExist", binCode);
            }
            return storageBin;
        }

        public bool CheckExistAndPermission(string binCode, string userCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(StorageBin));
            criteria.CreateAlias("StorageArea", "sa");
            criteria.CreateAlias("sa.Location", "l");
            criteria.CreateAlias("l.Region", "r");
            SecurityHelper.SetRegionSearchCriteria(criteria, "r.Code", userCode); //ÇøÓòÈ¨ÏÞ
            criteria.Add(Expression.Eq("Code", binCode));
            criteria.Add(Expression.Eq("IsActive", true));

            criteria.SetProjection(Projections.RowCount());
            IList result = criteriaMgr.FindAll(criteria);
            if ((int)result[0] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Customized Methods
    }
}