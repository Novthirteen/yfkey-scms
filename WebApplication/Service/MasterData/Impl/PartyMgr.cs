using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;
//TODO: Add other using statements here.
using NHibernate.Expression;
using com.Sconit.Entity.Exception;
using com.Sconit.Service.Transportation;
using com.Sconit.Entity.Transportation;

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class PartyMgr : PartyBaseMgr, IPartyMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IRegionMgr RegionMgr;
        private ISupplierMgr SupplierMgr;
        private ICustomerMgr CustomerMgr;
        private ICarrierMgr CarrierMgr;
        public PartyMgr(IPartyDao entityDao, ICriteriaMgr criteriaMgr, IRegionMgr RegionMgr, ISupplierMgr SupplierMgr, ICustomerMgr CustomerMgr, ICarrierMgr CarrierMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.RegionMgr = RegionMgr;
            this.SupplierMgr = SupplierMgr;
            this.CustomerMgr = CustomerMgr;
            this.CarrierMgr = CarrierMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public Party CheckAndLoadParty(string partyCode)
        {
            Party party = this.LoadParty(partyCode);
            if (party == null)
            {
                throw new BusinessErrorException("Party.Error.PartyCodeNotExist", partyCode);
            }

            return party;
        }

        public IList<Party> GetFromParty(string orderType, string userCode)
        {
            return GetFromParty(orderType, userCode, false);
        }

        public IList<Party> GetFromParty(string orderType, string userCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Party>();

            if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT || orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetSupplierPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", regionCrieteria[0]),
                        Subqueries.PropertyIn("Code", regionCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetCustomerPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }

            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            return this.criteriaMgr.FindAll<Party>(criteria);
        }

        public IList<Party> GetOrderFromParty(string orderType, string userCode)
        {
            return GetOrderFromParty(orderType, userCode, false);
        }

        public IList<Party> GetOrderFromParty(string orderType, string userCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Party>();

            if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT || orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", regionCrieteria[0]),
                        Subqueries.PropertyIn("Code", regionCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetCustomerPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }

            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            return this.criteriaMgr.FindAll<Party>(criteria);
        }

        public IList<Party> GetToParty(string orderType, string userCode)
        {
            return GetToParty(orderType, userCode, false);
        }

        public IList<Party> GetToParty(string orderType, string userCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Party>();

            if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT || orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetCustomerPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", regionCrieteria[0]),
                        Subqueries.PropertyIn("Code", regionCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_INSPECTION)
            {
                DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", regionCrieteria[0]),
                        Subqueries.PropertyIn("Code", regionCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", regionCrieteria[0]),
                        Subqueries.PropertyIn("Code", regionCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }

            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            return this.criteriaMgr.FindAll<Party>(criteria);
        }

        public IList<Party> GetOrderToParty(string orderType, string userCode)
        {
            return GetOrderToParty(orderType, userCode, false);
        }

        public IList<Party> GetOrderToParty(string orderType, string userCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Party>();

            if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT || orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode, new string[] { BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION });
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", regionCrieteria[0]),
                        Subqueries.PropertyIn("Code", regionCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", regionCrieteria[0]),
                        Subqueries.PropertyIn("Code", regionCrieteria[1])
                ));
            }
            else if (orderType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                DetachedCriteria[] pfCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("Code", pfCrieteria[1])
                ));
            }

            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            return this.criteriaMgr.FindAll<Party>(criteria);
        }

        public IList<Party> GetAllParty(string type)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Party));
            criteria.Add(Expression.Eq("IsActive", true));
            IList<Party> listParty = this.criteriaMgr.FindAll<Party>(criteria);
            IList<Party> listParty0 = new List<Party>();
            foreach (Party party in listParty)
            {
                if (party.Type.Trim().ToLower() == type.Trim().ToLower())
                {
                    listParty0.Add(party);
                }
            }

            return listParty0;
        }

        public IList<Party> GetAllParty(string userCode, string type)
        {
            IList<Party> listParty = new List<Party>();
            if (type.Trim().ToLower() == BusinessConstants.PARTY_TYPE_SUPPLIER.Trim().ToLower())
            {
                IList<Supplier> listSupplier = SupplierMgr.GetSupplier(userCode);
                foreach (Supplier supplier in listSupplier)
                {
                    listParty.Add((Party)supplier);
                }
            }
            else if (type.Trim().ToLower() == BusinessConstants.PARTY_TYPE_CUSTOMER.Trim().ToLower())
            {
                IList<Customer> listCustomer = CustomerMgr.GetCustomer(userCode);
                foreach (Customer customer in listCustomer)
                {
                    listParty.Add((Party)customer);
                }
            }
            else if (type.Trim().ToLower() == BusinessConstants.PARTY_TYPE_CARRIER.Trim().ToLower())
            {
                IList<Carrier> listCarrier = CarrierMgr.GetCarrier(userCode);
                foreach (Carrier carrier in listCarrier)
                {
                    listParty.Add((Party)carrier);
                }
            }
            else
            {
                listParty = null;
            }

            return listParty;
        }

        public IList<Party> GetTransportationParty(string userCode)
        {
            return GetTransportationParty(userCode, false);
        }

        public IList<Party> GetTransportationParty( string userCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Party));
            DetachedCriteria[] pfCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode, new string[] { BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CARRIER, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION });
             
            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("Code", pfCrieteria[0]),
                    Subqueries.PropertyIn("Code", pfCrieteria[1])
            ));


            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            return this.criteriaMgr.FindAll<Party>(criteria);
        }

        public bool CheckPartyPermission(string userCode, string partyCode)
        {
            bool hasPermission = false;

            DetachedCriteria upSubCriteria = DetachedCriteria.For<UserPermission>();
            upSubCriteria.CreateAlias("User", "u");
            upSubCriteria.CreateAlias("Permission", "pm");
            upSubCriteria.CreateAlias("pm.Category", "pmc");
            upSubCriteria.Add(Expression.Eq("pmc.Type", BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_ORGANIZATION));
            upSubCriteria.Add(Expression.Eq("u.Code", userCode));
            upSubCriteria.Add(Expression.Eq("pm.Code", partyCode));

            IList<UserPermission> userPermissionList = criteriaMgr.FindAll<UserPermission>(upSubCriteria);

            if (userPermissionList != null && userPermissionList.Count > 0)
            {
                hasPermission = true;
            }
            if (!hasPermission)
            {
                DetachedCriteria rpSubCriteria = DetachedCriteria.For<RolePermission>();
                rpSubCriteria.CreateAlias("Role", "r");
                rpSubCriteria.CreateAlias("Permission", "pm");
                rpSubCriteria.CreateAlias("pm.Category", "pmc");
                rpSubCriteria.Add(Expression.Eq("pm.Code", partyCode));
                rpSubCriteria.Add(Expression.Eq("pmc.Type", BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_ORGANIZATION));
                // rpSubCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("pm.Code")));

                DetachedCriteria urSubCriteria = DetachedCriteria.For<UserRole>();
                urSubCriteria.CreateAlias("User", "u");
                urSubCriteria.CreateAlias("Role", "r");
                urSubCriteria.Add(Expression.Eq("u.Code", userCode));
                urSubCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("r.Code")));

                rpSubCriteria.Add(Subqueries.PropertyIn("r.Code", urSubCriteria));

                IList<RolePermission> rolePermissionList = criteriaMgr.FindAll<RolePermission>(rpSubCriteria);

                if (rolePermissionList != null && rolePermissionList.Count > 0)
                {
                    hasPermission = true;
                }

            }

            return hasPermission;
        }
        #endregion Customized Methods
    }
}