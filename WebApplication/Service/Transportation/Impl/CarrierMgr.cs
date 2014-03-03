using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Transportation;
using com.Sconit.Service.Transportation;
using com.Sconit.Entity.Transportation;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Persistence;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity;
using System.Data.SqlClient;
using System.Data;
using NHibernate.Expression;
using com.Sconit.Utility;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class CarrierMgr : CarrierBaseMgr, ICarrierMgr
    {
        private IAddressMgr addressMgr;
        private IWorkCenterMgr workCenterMgr;
        private ICriteriaMgr criteriaMgr;
        private IPermissionMgr permissionMgr;
        private IPermissionCategoryMgr permissionCategoryMgr;
        private IUserPermissionMgr userPermissionMgr;
        private IPartyDao partyDao;
        private ISqlHelperDao sqlHelperDao;
        private IUserMgr userMgr;

        public CarrierMgr(ICarrierDao entityDao,
            IAddressMgr addressMgr,
            IWorkCenterMgr workCenterMgr,
            ICriteriaMgr criteriaMgr,
            IPermissionMgr permissionMgr,
            IPermissionCategoryMgr permissionCategoryMgr,
            IUserPermissionMgr userPermissionMgr,
            IPartyDao partyDao,
            IUserMgr userMgr,
            ISqlHelperDao sqlHelperDao)
            : base(entityDao)
        {
            this.addressMgr = addressMgr;
            this.workCenterMgr = workCenterMgr;
            this.criteriaMgr = criteriaMgr;
            this.permissionMgr = permissionMgr;
            this.permissionCategoryMgr = permissionCategoryMgr;
            this.userPermissionMgr = userPermissionMgr;
            this.userMgr = userMgr;
            this.partyDao = partyDao;
            this.sqlHelperDao = sqlHelperDao;            
        }

        #region Customized Methods
        [Transaction(TransactionMode.Requires)]
        public override void CreateCarrier(Carrier entity)
        {
            CreateCarrier(entity, userMgr.GetMonitorUser());
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateCarrier(Carrier entity, User currentUser)
        {
            if (partyDao.LoadParty(entity.Code) == null)
            {
                base.CreateCarrier(entity);
            }
            else
            {
                CreateCarrierOnly(entity);
            }
            Permission permission = new Permission();
            permission.Category = permissionCategoryMgr.LoadPermissionCategory(BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_CARRIER);
            permission.Code = entity.Code;
            permission.Description = entity.Name;
            permissionMgr.CreatePermission(permission);
            UserPermission userPermission = new UserPermission();
            userPermission.Permission = permission;
            userPermission.User = currentUser;
            userPermissionMgr.CreateUserPermission(userPermission);
        }

        [Transaction(TransactionMode.Unspecified)]
        public int CreateCarrierOnly(Carrier entity)
        {
            string sql = "insert into Carrier(code,RefSupplier) values(@code,@RefSupplier) ";

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@code", SqlDbType.NVarChar, 50);
            param[0].Value = entity.Code;
            param[1] = new SqlParameter("@RefSupplier", SqlDbType.NVarChar, 50);
            param[1].Value = entity.ReferenceSupplier;
            return sqlHelperDao.Create(sql, param);
        }

        [Transaction(TransactionMode.Requires)]
        public override void UpdateCarrier(Carrier entity)
        {
            Permission permission = permissionMgr.GetPermission(entity.Code);
            permission.Description = entity.Name;
            permissionMgr.UpdatePermission(permission);

            base.UpdateCarrier(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public override void DeleteCarrier(string code)
        {
            IList<UserPermission> userPermissionList = userPermissionMgr.GetUserPermission(code);
            userPermissionMgr.DeleteUserPermission(userPermissionList);
            permissionMgr.DeletePermission(code);
            if (partyDao.LoadParty(code) == null)
            {
                addressMgr.DeleteAddressByParent(code);
                base.DeleteCarrier(code);
            }
            else
            {
                DeleteCarrierOnly(code);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public override void DeleteCarrier(Carrier carrier)
        {
            DeleteCarrier(carrier.Code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public int DeleteCarrierOnly(string code)
        {
            string sql = "delete from Carrier where code=@code ";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@code", SqlDbType.NVarChar, 50);
            param[0].Value = code;
            return sqlHelperDao.Delete(sql, param);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Carrier> GetCarrier(string userCode)
        {
            return GetCarrier(userCode, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Carrier> GetCarrier(string userCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Carrier>();
            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            DetachedCriteria[] pCrieteria = SecurityHelper.GetCarrierPermissionCriteria(userCode);

            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("Code", pCrieteria[0]),
                    Subqueries.PropertyIn("Code", pCrieteria[1])));

            return criteriaMgr.FindAll<Carrier>(criteria);
        }

        #endregion Customized Methods
    }
}

