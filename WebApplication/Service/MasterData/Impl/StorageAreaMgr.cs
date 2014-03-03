using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class StorageAreaMgr : StorageAreaBaseMgr, IStorageAreaMgr
    {
        private ICriteriaMgr criteriaMgr;

        public StorageAreaMgr(IStorageAreaDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public IList<StorageArea> GetStorageArea(Location location)
        {
            return GetStorageArea(location.Code,string.Empty);
        }

        public IList<StorageArea> GetStorageArea(string locationCode)
        {
            return GetStorageArea(locationCode, string.Empty);
        }

        public IList<StorageArea> GetStorageArea(string locationCode, string AreaCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<StorageArea>();
            criteria.Add(Expression.Eq("Location.Code", locationCode));
            if (AreaCode != null && AreaCode != string.Empty)
            {
                criteria.Add(Expression.Like("Code", AreaCode));
            }
            return this.criteriaMgr.FindAll<StorageArea>(criteria);
        }

        #endregion Customized Methods
    }
}