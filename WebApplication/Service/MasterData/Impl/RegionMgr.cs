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
    public class RegionMgr : RegionBaseMgr, IRegionMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IWorkCenterMgr workCenterMgr;
        private IAddressMgr addressMgr;
        private IPermissionMgr permissionMgr;
        private IPermissionCategoryMgr permissionCategoryMgr;
        private IUserPermissionMgr userPermissionMgr;
        private IPartyDao partyDao;
        private ISqlHelperDao sqlHelperDao;
        private IUserMgr userMgr;
        public RegionMgr(IRegionDao entityDao,
            ICriteriaMgr criteriaMgr,
            IWorkCenterMgr workCenterMgr,
            IAddressMgr addressMgr,
            IPermissionMgr permissionMgr,
            IPermissionCategoryMgr permissionCategoryMgr,
            IUserPermissionMgr userPermissionMgr,
            IUserMgr userMgr,
            IPartyDao partyDao,
            ISqlHelperDao sqlHelperDao)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.workCenterMgr = workCenterMgr;
            this.addressMgr = addressMgr;
            this.permissionMgr = permissionMgr;
            this.permissionCategoryMgr = permissionCategoryMgr;
            this.userPermissionMgr = userPermissionMgr;
            this.userMgr = userMgr;
            this.partyDao = partyDao;
            this.sqlHelperDao = sqlHelperDao;

        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public override void DeleteRegion(string code)
        {
            IList<UserPermission> userPermissionList = userPermissionMgr.GetUserPermission(code);
            userPermissionMgr.DeleteUserPermission(userPermissionList);
            permissionMgr.DeletePermission(code);
            if (partyDao.LoadParty(code) == null)
            {
                workCenterMgr.DeleteWorkCenterByParent(code);
                addressMgr.DeleteAddressByParent(code);
                base.DeleteRegion(code);
            }
            else
            {
                DeleteRegionOnly(code);
            }

        }


        [Transaction(TransactionMode.Unspecified)]
        public int DeleteRegionOnly(string code)
        {
            string sql = "delete from Region where code=@code ";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@code", SqlDbType.NVarChar, 50);
            param[0].Value = code;
            return sqlHelperDao.Delete(sql, param);
        }

        [Transaction(TransactionMode.Unspecified)]
        public override void DeleteRegion(Region region)
        {
            DeleteRegion(region.Code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public override void CreateRegion(Region entity)
        {
            CreateRegion(entity, userMgr.GetMonitorUser());
        }

        [Transaction(TransactionMode.Unspecified)]
        public void CreateRegion(Region entity, User currentUser)
        {
            if (partyDao.LoadParty(entity.Code) == null)
            {
                base.CreateRegion(entity);
            }
            else
            {
                CreateRegionOnly(entity);
            }
            Permission permission = new Permission();
            permission.Category = permissionCategoryMgr.LoadPermissionCategory(BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_REGION);
            permission.Code = entity.Code;
            permission.Description = entity.Name;
            permissionMgr.CreatePermission(permission);
            UserPermission userPermission = new UserPermission();
            userPermission.User = currentUser;
            userPermission.Permission = permission;
            userPermissionMgr.CreateUserPermission(userPermission);
        }

        [Transaction(TransactionMode.Unspecified)]
        public int CreateRegionOnly(Region entity)
        {
            string sql = "insert into Region(code) values(@code) ";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@code", SqlDbType.NVarChar, 50);
            param[0].Value = entity.Code;
            return sqlHelperDao.Create(sql, param);
        }


        [Transaction(TransactionMode.Unspecified)]
        public IList<Region> GetRegion(string userCode)
        {
            return GetRegion(userCode, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Region> GetRegion(string userCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Region>();
            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            DetachedCriteria[] pCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("Code", pCrieteria[0]),
                    Subqueries.PropertyIn("Code", pCrieteria[1])));

            return criteriaMgr.FindAll<Region>(criteria);
        }

        #endregion Customized Methods
    }
}