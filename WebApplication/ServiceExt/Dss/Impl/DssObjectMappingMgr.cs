using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Dss;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.Dss;
using NHibernate.Expression;
using com.Sconit.Entity;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssObjectMappingMgr : DssObjectMappingBaseMgr, IDssObjectMappingMgr
    {
        private ICriteriaMgr criteriaMgr;

        public DssObjectMappingMgr(IDssObjectMappingDao entityDao,
             ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<DssObjectMapping> GetDssObjectMapping(string entity, string externalSystem, string code, string externalEntity)
        {
            if (code == null)
                code = string.Empty;

            DetachedCriteria criteria = DetachedCriteria.For(typeof(DssObjectMapping))
                .Add(Expression.Eq("Entity", entity))
                .Add(Expression.Eq("ExternalSystem.Code", externalSystem))
                .Add(Expression.Eq("ExternalEntity", externalEntity))
                .Add(Expression.Eq("Code", code));

            return criteriaMgr.FindAll<DssObjectMapping>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public string GetExternalCode(string entity, string externalSystem, string code, string defaultResult)
        {
            return this.GetExternalCode(entity, externalSystem, code, entity, defaultResult);
        }

        [Transaction(TransactionMode.Unspecified)]
        public string GetExternalCode(string entity, string externalSystem, string code, string externalEntity, string defaultResult)
        {
            IList<DssObjectMapping> dssObjectMappingList = this.GetDssObjectMapping(entity, externalSystem, code, externalEntity);
            if (dssObjectMappingList != null && dssObjectMappingList.Count == 1)
            {
                return dssObjectMappingList[0].ExternalCode;
            }
            else
            {
                return defaultResult;
            }
        }

        #endregion Customized Methods
    }
}