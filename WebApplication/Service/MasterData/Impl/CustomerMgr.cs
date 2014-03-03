using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;
using NHibernate.Expression;
using System.Data.SqlClient;
using System.Data;
using com.Sconit.Persistence;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class CustomerMgr : CustomerBaseMgr, ICustomerMgr
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
        public CustomerMgr(ICustomerDao entityDao,
            IAddressMgr addressMgr,
            IWorkCenterMgr workCenterMgr,
            ICriteriaMgr criteriaMgr,
            IPermissionMgr permissionMgr,
            IPermissionCategoryMgr permissionCategoryMgr,
            IUserPermissionMgr userPermissionMgr,
            IUserMgr userMgr,
            IPartyDao partyDao,
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
        public override void CreateCustomer(Customer entity)
        {
            CreateCustomer(entity, userMgr.GetMonitorUser());
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateCustomer(Customer entity, User currentUser)
        {
            if (partyDao.LoadParty(entity.Code) == null)
            {
                base.CreateCustomer(entity);
            }
            else
            {
                CreateCustomerOnly(entity);
            }
            Permission permission = new Permission();
            permission.Category = permissionCategoryMgr.LoadPermissionCategory(BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_CUSTOMER);
            permission.Code = entity.Code;
            permission.Description = entity.Name;
            permissionMgr.CreatePermission(permission);
            UserPermission userPermission = new UserPermission();
            userPermission.Permission = permission;
            userPermission.User = currentUser;
            userPermissionMgr.CreateUserPermission(userPermission);
        }

        [Transaction(TransactionMode.Unspecified)]
        public int CreateCustomerOnly(Customer entity)
        {
            string sql = "insert into Customer(code) values(@code) ";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@code", SqlDbType.NVarChar, 50);
            param[0].Value = entity.Code;
            return sqlHelperDao.Create(sql, param);
        }

        [Transaction(TransactionMode.Requires)]
        public override void DeleteCustomer(Customer customer)
        {
            DeleteCustomer(customer.Code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public override void DeleteCustomer(string code)
        {
            if (partyDao.LoadParty(code) == null)
            {
                workCenterMgr.DeleteWorkCenterByParent(code);
                addressMgr.DeleteAddressByParent(code);
                base.DeleteCustomer(code);
            }
            else
            {
                DeleteCustomerOnly(code);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public int DeleteCustomerOnly(string code)
        {
            string sql = "delete from Customer where code=@code ";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@code", SqlDbType.NVarChar, 50);
            param[0].Value = code;
            return sqlHelperDao.Delete(sql, param);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Customer> GetCustomer(string userCode)
        {
            return GetCustomer(userCode, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Customer> GetCustomer(string userCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Customer>();
            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            DetachedCriteria[] pCrieteria = SecurityHelper.GetCustomerPermissionCriteria(userCode);

            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("Code", pCrieteria[0]),
                    Subqueries.PropertyIn("Code", pCrieteria[1])));

            return criteriaMgr.FindAll<Customer>(criteria);
        }
        #endregion Customized Methods
    }
}