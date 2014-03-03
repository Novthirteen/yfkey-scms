using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

namespace com.Sconit.Utility
{
    public static class SecurityHelper
    {
        public static DetachedCriteria[] GetRegionPermissionCriteria(string userCode)
        {
            return GetPartyPermissionCriteria(userCode, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION);
        }

        public static DetachedCriteria[] GetSupplierPermissionCriteria(string userCode)
        {
            return GetPartyPermissionCriteria(userCode, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER);
        }

        public static DetachedCriteria[] GetCarrierPermissionCriteria(string userCode)
        {
            return GetPartyPermissionCriteria(userCode, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CARRIER);
        }

        public static DetachedCriteria[] GetCustomerPermissionCriteria(string userCode)
        {
            return GetPartyPermissionCriteria(userCode, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER);
        }

        public static DetachedCriteria[] GetPartyPermissionCriteria(string userCode)
        {
            return GetPartyPermissionCriteria(userCode, null);
        }

        public static DetachedCriteria[] GetPermissionCriteriaByCategory(User user, string category)
        {
            DetachedCriteria[] criteria = new DetachedCriteria[2];

            DetachedCriteria upSubCriteria = DetachedCriteria.For<UserPermission>();
            upSubCriteria.CreateAlias("User", "u");
            upSubCriteria.CreateAlias("Permission", "pm");
            upSubCriteria.CreateAlias("pm.Category", "pmc");
            upSubCriteria.Add(Expression.Eq("pmc.Type", category));
            upSubCriteria.Add(Expression.Eq("u.Code", user.Code));
            upSubCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("pm.Code")));

            DetachedCriteria rpSubCriteria = DetachedCriteria.For<RolePermission>();
            rpSubCriteria.CreateAlias("Role", "r");
            rpSubCriteria.CreateAlias("Permission", "pm");
            rpSubCriteria.CreateAlias("pm.Category", "pmc");
            rpSubCriteria.Add(Expression.Eq("pmc.Type", category));
            rpSubCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("pm.Code")));

            DetachedCriteria urSubCriteria = DetachedCriteria.For<UserRole>();
            urSubCriteria.CreateAlias("User", "u");
            urSubCriteria.CreateAlias("Role", "r");
            urSubCriteria.Add(Expression.Eq("u.Code", user.Code));
            urSubCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("r.Code")));

            rpSubCriteria.Add(Subqueries.PropertyIn("r.Code", urSubCriteria));

            criteria[0] = upSubCriteria;
            criteria[1] = rpSubCriteria;

            return criteria;
        }

        public static DetachedCriteria[] GetPartyPermissionCriteria(string userCode, params string[] partyType)
        {
            DetachedCriteria[] criteria = new DetachedCriteria[2];

            DetachedCriteria upSubCriteria = DetachedCriteria.For<UserPermission>();
            upSubCriteria.CreateAlias("User", "u");
            upSubCriteria.CreateAlias("Permission", "pm");
            upSubCriteria.CreateAlias("pm.Category", "pmc");
            upSubCriteria.Add(Expression.Eq("pmc.Type", BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_ORGANIZATION));
            if (partyType != null && partyType.Length > 0)
            {
                upSubCriteria.Add(Expression.In("pmc.Code", partyType));
            }
            upSubCriteria.Add(Expression.Eq("u.Code", userCode));
            upSubCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("pm.Code")));

            DetachedCriteria rpSubCriteria = DetachedCriteria.For<RolePermission>();
            rpSubCriteria.CreateAlias("Role", "r");
            rpSubCriteria.CreateAlias("Permission", "pm");
            rpSubCriteria.CreateAlias("pm.Category", "pmc");
            rpSubCriteria.Add(Expression.Eq("pmc.Type", BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_ORGANIZATION));
            if (partyType != null && partyType.Length > 0)
            {
                rpSubCriteria.Add(Expression.In("pmc.Code", partyType));
            }
            rpSubCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("pm.Code")));

            DetachedCriteria urSubCriteria = DetachedCriteria.For<UserRole>();
            urSubCriteria.CreateAlias("User", "u");
            urSubCriteria.CreateAlias("Role", "r");
            urSubCriteria.Add(Expression.Eq("u.Code", userCode));
            urSubCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("r.Code")));

            rpSubCriteria.Add(Subqueries.PropertyIn("r.Code", urSubCriteria));

            criteria[0] = upSubCriteria;
            criteria[1] = rpSubCriteria;

            return criteria;
        }

        public static void SetPartyFromSearchCriteria(DetachedCriteria selectCriteria, DetachedCriteria selectCountCriteria, string partyFromCode, string orderType, string userCode)
        {
            if (partyFromCode != null && partyFromCode.Trim() != string.Empty)
            {
                //如果用户选择了PartyFrom，直接用用户选择的值查询
                selectCriteria.Add(Expression.Eq("pf.Code", partyFromCode.Trim()));
                selectCountCriteria.Add(Expression.Eq("pf.Code", partyFromCode.Trim()));
            }
            else  //如果用户没有选择PartyFrom，根据用户权限过滤
            {
                if (orderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                {
                    //供货路线
                    DetachedCriteria[] pfCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode,
                        BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER);

                    selectCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                            Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                    ));

                    selectCountCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                            Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                    ));
                }
                else if (orderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                {
                    //发货路线
                    DetachedCriteria[] pfCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode,
                        BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER);

                    selectCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                            Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                    ));

                    selectCountCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                            Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                    ));
                }
                else if (orderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                {
                    DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                    //生产
                    selectCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pf.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pf.Code", regionCrieteria[1])
                    ));

                    selectCountCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pf.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pf.Code", regionCrieteria[1])
                    ));
                }
                else if (orderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
                {
                    //移库路线
                    DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                    selectCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pf.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pf.Code", regionCrieteria[1])
                    ));

                    selectCountCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pf.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pf.Code", regionCrieteria[1])
                    ));
                }
            }
        }

        public static void SetPartyToSearchCriteria(DetachedCriteria selectCriteria, DetachedCriteria selectCountCriteria, string partyToCode, string orderType, string userCode)
        {
            if (partyToCode != null && partyToCode.Trim() != string.Empty)
            {
                //如果用户选择了PartyTo，直接用用户选择的值查询
                selectCriteria.Add(Expression.Eq("pt.Code", partyToCode.Trim()));
                selectCountCriteria.Add(Expression.Eq("pt.Code", partyToCode.Trim()));
            }
            else  //如果用户没有选择PartyFrom，根据用户权限过滤
            {
                if (orderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                {
                    //供货路线
                    DetachedCriteria[] ptCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode,
                        BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER);

                    selectCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                    ));

                    selectCountCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                    ));
                }
                else if (orderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                {
                    //发货路线
                    DetachedCriteria[] ptCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode,
                        BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER);

                    selectCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                    ));

                    selectCountCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                    ));
                }
                else if (orderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                {
                    DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                    //生产
                    selectCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[1])
                    ));

                    selectCountCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[1])
                    ));
                }
                else if (orderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_INSPECTION)
                {
                    //检验路线
                    DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                    selectCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[1])
                    ));

                    selectCountCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[1])
                    ));
                }
                else if (orderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
                {
                    //移库路线
                    DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
                    selectCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[1])
                    ));

                    selectCountCriteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", regionCrieteria[1])
                    ));
                }
                //else if (orderType == BusinessConstants.ORDER_MODULETYPE_VALUE_PROCUREMENTCONFIRM)
                //{
                //    //发货确认
                //    DetachedCriteria[] ptCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER);

                //    selectCriteria.Add(
                //        Expression.Or(
                //            Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                //            Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                //    ));

                //    selectCountCriteria.Add(
                //        Expression.Or(
                //            Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                //            Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                //    ));
                //}
            }
        }

        public static void SetRegionSearchCriteria(DetachedCriteria selectCriteria, DetachedCriteria selectCountCriteria, string propertyName, string userCode)
        {
            SetRegionSearchCriteria(selectCriteria, propertyName, userCode);
            SetRegionSearchCriteria(selectCountCriteria, propertyName, userCode);
        }

        public static void SetRegionSearchCriteria(DetachedCriteria criteria, string propertyName, string userCode)
        {
            DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);
            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn(propertyName, regionCrieteria[0]),
                    Subqueries.PropertyIn(propertyName, regionCrieteria[1])
            ));
        }

        public static void SetPartySearchCriteria(DetachedCriteria criteria, string propertyName, string userCode)
        {
            DetachedCriteria[] partyCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode);
            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn(propertyName, partyCrieteria[0]),
                    Subqueries.PropertyIn(propertyName, partyCrieteria[1])
            ));
        }
    }
}
