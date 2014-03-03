using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;
using NHibernate.Expression;
using com.Sconit.Persistence;
using System.Data.SqlClient;
using System.Data;
using com.Sconit.Entity.Exception;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class SupplierMgr : SupplierBaseMgr, ISupplierMgr
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
    
        public SupplierMgr(ISupplierDao entityDao,
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
        public override void CreateSupplier(Supplier entity)
        {
            CreateSupplier(entity, userMgr.GetMonitorUser());
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateSupplier(Supplier entity,User currentUser)
        {
            if (partyDao.LoadParty(entity.Code)== null)
            {
                base.CreateSupplier(entity);
            }
            else
            {
                CreateSupplierOnly(entity);
            }
            Permission permission = new Permission();
            permission.Category = permissionCategoryMgr.LoadPermissionCategory(BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_SUPPLIER);
            permission.Code = entity.Code;
            permission.Description = entity.Name;
            permissionMgr.CreatePermission(permission);
            UserPermission userPermission = new UserPermission();
            userPermission.Permission = permission;
            userPermission.User = currentUser;
            userPermissionMgr.CreateUserPermission(userPermission);
        }

        [Transaction(TransactionMode.Unspecified)]
        public int CreateSupplierOnly(Supplier entity)
        {
            string sql = "insert into Supplier(code) values(@code) ";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@code", SqlDbType.NVarChar, 50);
            param[0].Value = entity.Code;
            return sqlHelperDao.Create(sql, param);
        }

        [Transaction(TransactionMode.Requires)]
        public override void DeleteSupplier(string code)
        {
          
            IList<UserPermission> userPermissionList = userPermissionMgr.GetUserPermission(code);
            userPermissionMgr.DeleteUserPermission(userPermissionList);
            permissionMgr.DeletePermission(code);
            if (partyDao.LoadParty(code) == null)
            {
                workCenterMgr.DeleteWorkCenterByParent(code);
                addressMgr.DeleteAddressByParent(code);
                base.DeleteSupplier(code);
            }
            else
            {
                DeleteSupplierOnly(code);
            }

        }

        [Transaction(TransactionMode.Requires)]
        public override void DeleteSupplier(Supplier supplier)
        {
            DeleteSupplier(supplier.Code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public int DeleteSupplierOnly(string code)
        {
            string sql = "delete from Supplier where code=@code ";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@code", SqlDbType.NVarChar, 50);
            param[0].Value = code;
            return sqlHelperDao.Delete(sql, param);
        }


        [Transaction(TransactionMode.Unspecified)]
        public IList<Supplier> GetSupplier(string userCode)
        {
            return GetSupplier(userCode, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Supplier> GetSupplier(string userCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Supplier>();
            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            DetachedCriteria[] pCrieteria = SecurityHelper.GetSupplierPermissionCriteria(userCode);

            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("Code", pCrieteria[0]),
                    Subqueries.PropertyIn("Code", pCrieteria[1])));

            return criteriaMgr.FindAll<Supplier>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Supplier CheckAndLoadSupplier(string supplierCode)
        {
            Supplier supplier = this.LoadSupplier(supplierCode);
            if (supplier == null)
            {
                throw new BusinessErrorException("Supplier.Error.SupplierCodeNotExist", supplierCode);
            }

            return supplier;
        }

        #endregion Customized Methods
    }
}