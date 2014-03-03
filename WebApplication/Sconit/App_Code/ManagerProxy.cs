using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Persistence;
using NHibernate;
using Castle.Facilities.NHibernateIntegration;
using Castle.Services.Transaction;
using com.Sconit.Service.Procurement;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Batch;
using com.Sconit.Service.Batch;
using com.Sconit.Service.Transportation;
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.Mes;
using com.Sconit.Entity.Mes;

/// <summary>
/// Summary description for ManagerProxy
/// </summary>
namespace com.Sconit.Web
{
    public class CriteriaMgrProxy
    {
        private ICriteriaMgr CriteriaMgr
        {
            get
            {
                return ServiceLocator.GetService<ICriteriaMgr>("CriteriaMgr.service");
            }
        }

        public CriteriaMgrProxy()
        {
        }

        public IList FindAll(DetachedCriteria selectCriteria, int firstRow, int maxRows)
        {
            return CriteriaMgr.FindAll(selectCriteria, firstRow, maxRows);
        }

        public int FindCount(DetachedCriteria selectCriteria)
        {
            IList list = CriteriaMgr.FindAll(selectCriteria);
            if (list != null && list.Count > 0)
            {
                if (list[0] is int)
                {
                    return int.Parse(list[0].ToString());
                }
                else if (list[0] is object[])
                {
                    return int.Parse(((object[])list[0])[0].ToString());
                }
                //由于性能问题,此后禁用该方法。
                //else if (list[0] is object)
                //{
                //    return list.Count;
                //}
                else
                {
                    throw new Exception("unknow result type");
                }
            }
            else
            {
                return 0;
            }
        }
    }

    public class CodeMasterMgrProxy
    {
        private ICodeMasterMgr CodeMasterMgr
        {
            get
            {
                return ServiceLocator.GetService<ICodeMasterMgr>("CodeMasterMgr.service");
            }
        }

        public CodeMasterMgrProxy()
        {
        }

        public IList<CodeMaster> GetCachedCodeMaster(string code)
        {
            return CodeMasterMgr.GetCachedCodeMaster(code);
        }

        public CodeMaster GetCachedCodeMaster(string code, string value)
        {
            return CodeMasterMgr.GetCachedCodeMaster(code, value);
        }

    }

    public class UserMgrProxy
    {
        private IUserMgr UserMgr
        {
            get
            {
                return ServiceLocator.GetService<IUserMgr>("UserMgr.service");
            }
        }

        private ICriteriaMgr CriteriaMgr
        {
            get
            {
                return ServiceLocator.GetService<ICriteriaMgr>("CriteriaMgr.service");
            }
        }

        public UserMgrProxy()
        {
        }

        public void CreateUser(User user)
        {
            UserMgr.CreateUser(user);
        }

        public List<User> FindUser(DetachedCriteria selectCriteria, int firstRow, int maxRows)
        {
            IList targetList = this.CriteriaMgr.FindAll(selectCriteria);
            List<User> resultList = new List<User>();
            return resultList.GetRange(firstRow, (firstRow + maxRows) > resultList.Count ? (resultList.Count - firstRow) : maxRows);
        }

        public int FindUserCount(DetachedCriteria selectCriteria)
        {
            IList list = CriteriaMgr.FindAll(selectCriteria);
            return list.Count;
        }

        public User LoadUser(string code)
        {
            return UserMgr.LoadUser(code);
        }

        public void UpdateUser(User user)
        {
            UserMgr.UpdateUser(user);
        }

        public void DeleteUser(User user)
        {
            UserMgr.DeleteUser(user);
        }
        public IList<Permission> GetAllPermissions(string code)
        {
            return UserMgr.GetAllPermissions(code);
        }
    }

    public class RegionMgrProxy
    {
        private IRegionMgr RegionMgr
        {
            get
            {
                return ServiceLocator.GetService<IRegionMgr>("RegionMgr.service");
            }
        }

        public RegionMgrProxy()
        {
        }

        public void CreateRegion(Region region)
        {
            RegionMgr.CreateRegion(region);
        }

        public Region LoadRegion(string code)
        {
            return RegionMgr.LoadRegion(code);
        }

        public void UpdateRegion(Region region)
        {
            RegionMgr.UpdateRegion(region);
        }

        public void DeleteRegion(Region region)
        {
            RegionMgr.DeleteRegion(region);
        }
    }

    public class RoleMgrProxy
    {
        private IRoleMgr RoleMgr
        {
            get
            {
                return ServiceLocator.GetService<IRoleMgr>("RoleMgr.service");
            }
        }

        public RoleMgrProxy()
        {
        }

        public void CreateRole(Role role)
        {
            RoleMgr.CreateRole(role);
        }

        public Role LoadRole(string code)
        {
            return RoleMgr.LoadRole(code);
        }

        public void UpdateRole(Role role)
        {
            RoleMgr.UpdateRole(role);
        }

        public void DeleteRole(Role role)
        {
            RoleMgr.DeleteRole(role);
        }
    }

    public class SupplierMgrProxy
    {
        private ISupplierMgr SupplierMgr
        {
            get
            {
                return ServiceLocator.GetService<ISupplierMgr>("SupplierMgr.service");
            }
        }

        public SupplierMgrProxy()
        {
        }

        public void CreateSupplier(Supplier supplier)
        {
            SupplierMgr.CreateSupplier(supplier);
        }

        public Supplier LoadSupplier(string code)
        {
            return SupplierMgr.LoadSupplier(code);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            SupplierMgr.UpdateSupplier(supplier);
        }

        public void DeleteSupplier(Supplier supplier)
        {
            SupplierMgr.DeleteSupplier(supplier);
        }
    }

    public class CustomerMgrProxy
    {
        private ICustomerMgr CustomerMgr
        {
            get
            {
                return ServiceLocator.GetService<ICustomerMgr>("CustomerMgr.service");
            }
        }

        public CustomerMgrProxy()
        {
        }

        public void CreateCustomer(Customer customer)
        {
            CustomerMgr.CreateCustomer(customer);
        }

        public Customer LoadCustomer(string code)
        {
            return CustomerMgr.LoadCustomer(code);
        }

        public void UpdateCustomer(Customer customer)
        {
            CustomerMgr.UpdateCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            CustomerMgr.DeleteCustomer(customer);
        }
    }

    public class WorkCenterMgrProxy
    {
        private IWorkCenterMgr WorkCenterMgr
        {
            get
            {
                return ServiceLocator.GetService<IWorkCenterMgr>("WorkCenterMgr.service");
            }
        }

        public WorkCenterMgrProxy()
        {
        }

        public void CreateWorkCenter(WorkCenter workCenter)
        {
            WorkCenterMgr.CreateWorkCenter(workCenter);
        }

        public WorkCenter LoadWorkCenter(string code)
        {
            return WorkCenterMgr.LoadWorkCenter(code);
        }

        public void UpdateWorkCenter(WorkCenter workCenter)
        {
            WorkCenterMgr.UpdateWorkCenter(workCenter);
        }

        public void DeleteWorkCenter(WorkCenter workCenter)
        {
            WorkCenterMgr.DeleteWorkCenter(workCenter);
        }
    }

    public class EntityPreferenceMgrProxy
    {
        private IEntityPreferenceMgr EntityPreferenceMgr
        {
            get
            {
                return ServiceLocator.GetService<IEntityPreferenceMgr>("EntityPreferenceMgr.service");
            }
        }

        public EntityPreferenceMgrProxy()
        {
        }

        public void UpdateEntityPreference()
        { }

        public IList<EntityPreference> LoadEntityPreference()
        {
            return EntityPreferenceMgr.GetAllEntityPreferenceOrderBySeq();
        }

        public void UpdateEntityPreference(EntityPreference entityPreference)
        {
            EntityPreferenceMgr.UpdateEntityPreference(entityPreference);
        }

    }

    public class WorkdayMgrProxy
    {
        private IWorkdayMgr WorkdayMgr
        {
            get
            {
                return ServiceLocator.GetService<IWorkdayMgr>("WorkdayMgr.service");
            }
        }

        public WorkdayMgrProxy()
        {
        }

        public void CreateWorkday(Workday workday)
        {
            WorkdayMgr.CreateWorkday(workday);
        }

        public Workday LoadWorkday(int ID)
        {
            return WorkdayMgr.LoadWorkday(ID);
        }

        public void UpdateWorkday(Workday workday)
        {
            WorkdayMgr.UpdateWorkday(workday);
        }

        public void DeleteWorkday(Workday workday)
        {
            WorkdayMgr.DeleteWorkday(workday.Id, true);
        }
    }

    public class UserRoleMgrProxy
    {
        private IUserRoleMgr UserRoleMgr
        {
            get
            {
                return ServiceLocator.GetService<IUserRoleMgr>("UserRoleMgr.service");
            }
        }

        public UserRoleMgrProxy()
        {
        }

        public IList<Role> GetRolesNotInUser(string code)
        {
            return UserRoleMgr.GetRolesNotInUser(code);
        }

        public IList<Role> GetRolesByUserCode(string code)
        {
            return UserRoleMgr.GetRolesByUserCode(code);
        }

        public IList<User> GetUsersNotInRole(string code)
        {
            return UserRoleMgr.GetUsersNotInRole(code);
        }

        public IList<User> GetUsersByRoleCode(string code)
        {
            return UserRoleMgr.GetUsersByRoleCode(code);
        }
    }

    public class ShiftMgrProxy
    {
        private IShiftMgr ShiftMgr
        {
            get
            {
                return ServiceLocator.GetService<IShiftMgr>("ShiftMgr.service");
            }
        }

        public ShiftMgrProxy()
        {
        }

        public void CreateShift(Shift shift)
        {
            ShiftMgr.CreateShift(shift);
        }

        public Shift LoadShift(String code)
        {
            return ShiftMgr.LoadShift(code);
        }

        public void UpdateShift(Shift shift)
        {
            ShiftMgr.UpdateShift(shift);
        }

        public void DeleteShift(Shift shift)
        {
            ShiftMgr.DeleteShift(shift);
        }
    }

    public class ShiftDetailMgrProxy
    {
        private IShiftDetailMgr ShiftDetailMgr
        {
            get
            {
                return ServiceLocator.GetService<IShiftDetailMgr>("ShiftDetailMgr.service");
            }
        }

        public ShiftDetailMgrProxy()
        {
        }

        public void CreateShiftDetail(ShiftDetail shiftDetail)
        {
            ShiftDetailMgr.CreateShiftDetail(shiftDetail);
        }

        public ShiftDetail LoadShiftDetail(int Id)
        {
            return ShiftDetailMgr.LoadShiftDetail(Id);
        }

        public void UpdateShiftDetail(ShiftDetail shiftDetail)
        {
            ShiftDetailMgr.UpdateShiftDetail(shiftDetail);
        }

        public void DeleteShiftDetail(ShiftDetail shiftDetail)
        {
            ShiftDetailMgr.DeleteShiftDetail(shiftDetail);
        }
    }

    public class SpecialTimeMgrProxy
    {
        private ISpecialTimeMgr SpecialTimeMgr
        {
            get
            {
                return ServiceLocator.GetService<ISpecialTimeMgr>("SpecialTimeMgr.service");
            }
        }

        public SpecialTimeMgrProxy()
        {
        }

        public void CreateSpecialTime(SpecialTime specialTime)
        {
            SpecialTimeMgr.CreateSpecialTime(specialTime);
        }

        public SpecialTime LoadSpecialTime(int ID)
        {
            return SpecialTimeMgr.LoadSpecialTime(ID);
        }

        public void UpdateSpecialTime(SpecialTime specialTime)
        {
            SpecialTimeMgr.UpdateSpecialTime(specialTime);
        }

        public void DeleteSpecialTime(SpecialTime specialTime)
        {
            SpecialTimeMgr.DeleteSpecialTime(specialTime);
        }
    }

    public class UserPermissionMgrProxy
    {
        private IUserPermissionMgr UserPermissionMgr
        {
            get
            {
                return ServiceLocator.GetService<IUserPermissionMgr>("UserPermissionMgr.service");
            }
        }

        public UserPermissionMgrProxy()
        {
        }

        public IList<Permission> GetPermissionsNotInUser(string code, string categoryCode)
        {
            if (code == string.Empty || code == null) return new List<Permission>();
            return UserPermissionMgr.GetPermissionsNotInUser(code, categoryCode);
        }

        public IList<Permission> GetPermissionsByUserCode(string code, string categoryCode)
        {
            if (code == string.Empty || code == null) return new List<Permission>();
            return UserPermissionMgr.GetPermissionsByUserCode(code, categoryCode);
        }
    }

    public class RolePermissionMgrProxy
    {
        private IRolePermissionMgr RolePermissionMgr
        {
            get
            {
                return ServiceLocator.GetService<IRolePermissionMgr>("RolePermissionMgr.service");
            }
        }

        public RolePermissionMgrProxy()
        {
        }

        public IList<Permission> GetPermissionsNotInRole(string code, string categoryCode)
        {
            if (code == string.Empty || code == null) return new List<Permission>();
            return RolePermissionMgr.GetPermissionsNotInRole(code, categoryCode);
        }

        public IList<Permission> GetPermissionsByRoleCode(string code, string categoryCode)
        {
            if (code == string.Empty || code == null) return new List<Permission>();
            return RolePermissionMgr.GetPermissionsByRoleCode(code, categoryCode);
        }
    }


    public class ItemMgrProxy
    {
        private IItemMgr ItemMgr
        {
            get
            {
                return ServiceLocator.GetService<IItemMgr>("ItemMgr.service");
            }
        }

        public ItemMgrProxy()
        {
        }

        public void CreateItem(Item item)
        {
            ItemMgr.CreateItem(item);
        }

        public Item LoadItem(string code)
        {
            return ItemMgr.LoadItem(code);
        }

        public void UpdateItem(Item item)
        {
            ItemMgr.UpdateItem(item);
        }

        public void DeleteItem(Item item)
        {
            ItemMgr.DeleteItem(item);
        }
    }

    public class AddressMgrProxy
    {
        private IAddressMgr AddressMgr
        {
            get
            {
                return ServiceLocator.GetService<IAddressMgr>("AddressMgr.service");
            }
        }

        public AddressMgrProxy()
        {
        }

        public void CreateAddress(Address address)
        {
            AddressMgr.CreateAddress(address);
        }

        public Address LoadAddress(string code)
        {
            return AddressMgr.LoadAddress(code);
        }

        public void UpdateAddress(Address address)
        {
            AddressMgr.UpdateAddress(address);
        }

        public void DeleteAddress(Address address)
        {
            AddressMgr.DeleteAddress(address);
        }
    }

    public class BillAddressMgrProxy
    {
        private IBillAddressMgr BillAddressMgr
        {
            get
            {
                return ServiceLocator.GetService<IBillAddressMgr>("BillAddressMgr.service");
            }
        }

        public BillAddressMgrProxy()
        {
        }

        public void CreateBillAddress(BillAddress address)
        {
            BillAddressMgr.CreateBillAddress(address);
        }

        public BillAddress LoadBillAddress(string code)
        {
            return BillAddressMgr.LoadBillAddress(code);
        }

        public void UpdateBillAddress(BillAddress address)
        {
            BillAddressMgr.UpdateBillAddress(address);
        }

        public void DeleteBillAddress(BillAddress address)
        {
            BillAddressMgr.DeleteBillAddress(address);
        }
    }

    public class ShipAddressMgrProxy
    {
        private IShipAddressMgr ShipAddressMgr
        {
            get
            {
                return ServiceLocator.GetService<IShipAddressMgr>("ShipAddressMgr.service");
            }
        }

        public ShipAddressMgrProxy()
        {
        }

        public void CreateShipAddress(ShipAddress address)
        {
            ShipAddressMgr.CreateShipAddress(address);
        }

        public ShipAddress LoadShipAddress(string code)
        {
            return ShipAddressMgr.LoadShipAddress(code);
        }

        public void UpdateShipAddress(ShipAddress address)
        {
            ShipAddressMgr.UpdateShipAddress(address);
        }

        public void DeleteShipAddress(ShipAddress address)
        {
            ShipAddressMgr.DeleteShipAddress(address);
        }
    }

    public class BomMgrProxy
    {
        private IBomMgr BomMgr
        {
            get
            {
                return ServiceLocator.GetService<IBomMgr>("BomMgr.service");
            }
        }

        public BomMgrProxy()
        {
        }

        public void CreateBom(Bom bom)
        {
            BomMgr.CreateBom(bom);
        }

        public Bom LoadBom(string code)
        {
            return BomMgr.LoadBom(code);
        }

        public void UpdateBom(Bom bom)
        {
            BomMgr.UpdateBom(bom);
        }

        public void DeleteBom(Bom bom)
        {
            BomMgr.DeleteBom(bom);
        }
    }

    public class BomDetailMgrProxy
    {
        private IBomDetailMgr BomDetailMgr
        {
            get
            {
                return ServiceLocator.GetService<IBomDetailMgr>("BomDetailMgr.service");
            }
        }

        public BomDetailMgrProxy()
        {
        }

        public void CreateBomDetail(BomDetail bomdetail)
        {
            BomDetailMgr.CreateBomDetail(bomdetail);
        }

        public BomDetail LoadBomDetail(int ID)
        {
            return BomDetailMgr.LoadBomDetail(ID);
        }

        public void UpdateBomDetail(BomDetail bomdetail)
        {
            BomDetailMgr.UpdateBomDetail(bomdetail);
        }

        public void DeleteBomDetail(BomDetail bomdetail)
        {
            BomDetailMgr.DeleteBomDetail(bomdetail);
        }
    }

    public class UomMgrProxy
    {
        private IUomMgr UomMgr
        {
            get
            {
                return ServiceLocator.GetService<IUomMgr>("UomMgr.service");
            }
        }

        public UomMgrProxy()
        {
        }

        public IList<Uom> GetAllUom()
        {
            return UomMgr.GetAllUom();
        }

        public void CreateUom(Uom location)
        {
            UomMgr.CreateUom(location);
        }

        public IList LoadUom(string code, string name, string desc)
        {
            return UomMgr.GetUom(code, name, desc);
        }

        public void UpdateUom(Uom location)
        {
            UomMgr.UpdateUom(location);
        }

        public void DeleteUom(Uom location)
        {
            UomMgr.DeleteUom(location);
        }
    }

    public class WorkdayShiftMgrProxy
    {
        private IWorkdayShiftMgr WorkdayShiftMgr
        {
            get
            {
                return ServiceLocator.GetService<IWorkdayShiftMgr>("WorkdayShiftMgr.service");
            }
        }

        public WorkdayShiftMgrProxy()
        {
        }

        public IList<Shift> GetShiftsNotInWorkday(int Id)
        {
            return WorkdayShiftMgr.GetShiftsNotInWorkday(Id);


        }

        public IList<Shift> GetShiftsByWorkdayId(int Id)
        {
            return WorkdayShiftMgr.GetShiftsByWorkdayId(Id);
        }
    }

    public class FlowMgrProxy
    {
        private IFlowMgr FlowMgr
        {
            get
            {
                return ServiceLocator.GetService<IFlowMgr>("FlowMgr.service");
            }
        }

        public Flow LoadFlow(string code)
        {
            if (code == null) return null;
            return FlowMgr.LoadFlow(code);
        }

        public void CreateFlow(Flow entity)
        {
            FlowMgr.CreateFlow(entity);
        }

        public void UpdateFlow(Flow entity)
        {
            FlowMgr.UpdateFlow(entity,true);
        }

        public void DeleteFlow(Flow entity)
        {
            FlowMgr.DeleteFlow(entity);
        }

        public IList<Flow> GetProductLinesNotInUser(string code)
        {
            return FlowMgr.GetProductLinesNotInUser(code);
        }

        public IList<Flow> GetProductLinesInUser(string code)
        {

            return FlowMgr.GetProductLinesInUser(code);
        }
    }

    public class LocationMgrProxy
    {
        private ILocationMgr LocationMgr
        {
            get
            {
                return ServiceLocator.GetService<ILocationMgr>("LocationMgr.service");
            }
        }

        public LocationMgrProxy()
        {
        }

        public void CreateLocation(Location location)
        {
            LocationMgr.CreateLocation(location);
        }

        public Location LoadLocation(string code)
        {
            return LocationMgr.LoadLocation(code);
        }

        public void UpdateLocation(Location location)
        {
            LocationMgr.UpdateLocation(location);
        }

        public void DeleteLocation(Location location)
        {
            LocationMgr.DeleteLocation(location);
        }
    }

    public class CurrencyMgrProxy
    {
        private ICurrencyMgr CurrencyMgr
        {
            get
            {
                return ServiceLocator.GetService<ICurrencyMgr>("CurrencyMgr.service");
            }
        }

        public CurrencyMgrProxy()
        {
        }

        public void CreateCurrency(Currency currency)
        {
            CurrencyMgr.CreateCurrency(currency);
        }

        public IList LoadCurrency(string code, string name)
        {
            return CurrencyMgr.GetCurrency(code, name);
        }

        public void UpdateCurrency(Currency currency)
        {
            CurrencyMgr.UpdateCurrency(currency);
        }

        public void DeleteCurrency(Currency currency)
        {
            CurrencyMgr.DeleteCurrency(currency);
        }
    }

    public class UomConversionMgrProxy
    {
        private IUomConversionMgr UomConversionMgr
        {
            get
            {
                return ServiceLocator.GetService<IUomConversionMgr>("UomConversionMgr.service");
            }
        }

        public UomConversionMgrProxy()
        {
        }

        public void CreateUomConversion(UomConversion uomConversion)
        {
            UomConversionMgr.CreateUomConversion(uomConversion);
        }

        public IList LoadUomConversion(string itemCode)
        {
            return UomConversionMgr.GetUomConversion(itemCode, null, null);
        }

        public void UpdateUomConversion(UomConversion uomConversion)
        {
            UomConversionMgr.UpdateUomConversion(uomConversion);
        }

        public void DeleteUomConversion(UomConversion uomConversion)
        {
            UomConversionMgr.DeleteUomConversion(uomConversion);
        }
    }

    public class PriceListMgrProxy
    {
        private IPriceListMgr PriceListMgr
        {
            get
            {
                return ServiceLocator.GetService<IPriceListMgr>("PriceListMgr.service");
            }
        }

        private IPurchasePriceListMgr PurchasePriceListMgr
        {
            get
            {
                return ServiceLocator.GetService<IPurchasePriceListMgr>("PurchasePriceListMgr.service");
            }
        }

        private ISalesPriceListMgr SalesPriceListMgr
        {
            get
            {
                return ServiceLocator.GetService<ISalesPriceListMgr>("SalesPriceListMgr.service");
            }
        }

        public PriceListMgrProxy()
        {
        }

        public void CreatePriceList(PriceList priceList)
        {
            PriceListMgr.CreatePriceList(priceList);
        }
        public void CreatePurchasePriceList(PurchasePriceList purchasePriceList)
        {
            PurchasePriceListMgr.CreatePurchasePriceList(purchasePriceList);
        }
        public void CreateSalesPriceList(SalesPriceList salesPriceList)
        {
            SalesPriceListMgr.CreateSalesPriceList(salesPriceList);
        }

        public PriceList LoadPriceList(string code)
        {
            return PriceListMgr.LoadPriceList(code);
        }
        public PurchasePriceList LoadPurchasePriceList(string code)
        {
            return PurchasePriceListMgr.LoadPurchasePriceList(code);
        }
        public SalesPriceList LoadSalesPriceList(string code)
        {
            return SalesPriceListMgr.LoadSalesPriceList(code);
        }

        public void UpdatePriceList(PriceList priceList)
        {
            PriceListMgr.UpdatePriceList(priceList);
        }
        public void UpdatePurchasePriceList(PurchasePriceList purchasePriceList)
        {
            PurchasePriceListMgr.UpdatePurchasePriceList(purchasePriceList);
        }
        public void UpdateSalesPriceList(SalesPriceList salesPriceList)
        {
            SalesPriceListMgr.UpdateSalesPriceList(salesPriceList);
        }

        public void DeletePriceList(PriceList priceList)
        {
            PriceListMgr.DeletePriceList(priceList);
        }
        public void DeletePurchasePriceList(PurchasePriceList purchasePriceList)
        {
            PurchasePriceListMgr.DeletePurchasePriceList(purchasePriceList);
        }
        public void DeleteSalesPriceList(SalesPriceList salesPriceList)
        {
            SalesPriceListMgr.DeleteSalesPriceList(salesPriceList);
        }
    }

    public class PriceListDetailMgrProxy
    {
        private IPriceListDetailMgr PriceListDetailMgr
        {
            get
            {
                return ServiceLocator.GetService<IPriceListDetailMgr>("PriceListDetailMgr.service");
            }
        }

        public PriceListDetailMgrProxy()
        {
        }

        public void CreatePriceListDetail(PriceListDetail priceListDetail)
        {
            PriceListDetailMgr.CreatePriceListDetail(priceListDetail);
        }

        public PriceListDetail LoadPriceListDetail(int ID)
        {
            return PriceListDetailMgr.LoadPriceListDetail(ID);
        }

        public void UpdatePriceListDetail(PriceListDetail priceListDetail)
        {
            PriceListDetailMgr.UpdatePriceListDetail(priceListDetail);
        }

        public void DeletePriceListDetail(PriceListDetail priceListDetail)
        {
            PriceListDetailMgr.DeletePriceListDetail(priceListDetail);
        }
    }

    public class FlowDetailMgrProxy
    {
        private IFlowDetailMgr FlowDetailMgr
        {
            get
            {
                return ServiceLocator.GetService<IFlowDetailMgr>("FlowDetailMgr.service");
            }
        }

        public FlowDetailMgrProxy()
        {
        }

        public void CreateFlowDetail(FlowDetail itemFlowDetail)
        {
            FlowDetailMgr.CreateFlowDetail(itemFlowDetail);
        }

        public FlowDetail FindFlowDetail(Int32 id)
        {
            return FlowDetailMgr.LoadFlowDetail(id);
        }

        public void UpdateFlowDetail(FlowDetail itemFlowDetail)
        {
            FlowDetailMgr.UpdateFlowDetail(itemFlowDetail);
        }

        public void DeleteFlowDetail(FlowDetail itemFlowDetail)
        {
            FlowDetailMgr.DeleteFlowDetail(itemFlowDetail);
        }
    }

    public class FlowBindingMgrProxy
    {
        private IFlowBindingMgr FlowBindingMgr
        {
            get
            {
                return ServiceLocator.GetService<IFlowBindingMgr>("FlowBindingMgr.service");
            }
        }

        public FlowBinding LoadFlowBinding(int id)
        {
            return FlowBindingMgr.LoadFlowBinding(id);
        }

        public void CreateFlowBinding(FlowBinding entity)
        {
            FlowBindingMgr.CreateFlowBinding(entity);
        }

        public void UpdateFlowBinding(FlowBinding entity)
        {
            FlowBindingMgr.UpdateFlowBinding(entity);
        }

        public void DeleteFlowBinding(FlowBinding entity)
        {
            FlowBindingMgr.DeleteFlowBinding(entity);
        }

    }

    public class OrderMgrProxy : System.Web.UI.UserControl
    {
        private IOrderHeadMgr OrderHeadMgr
        {
            get
            {
                return ServiceLocator.GetService<IOrderHeadMgr>("OrderHeadMgr.service");
            }
        }

        public OrderHead LoadOrderHead(string orderNo)
        {
            return OrderHeadMgr.LoadOrderHead(orderNo);
        }
    }


    public class ItemReferenceMgrProxy
    {
        private IItemReferenceMgr ItemReferenceMgr
        {
            get
            {
                return ServiceLocator.GetService<IItemReferenceMgr>("ItemReferenceMgr.service");
            }
        }

        public ItemReferenceMgrProxy()
        {
        }

        public void CreateItemReference(ItemReference itemRef)
        {
            ItemReferenceMgr.CreateItemReference(itemRef);
        }

        public IList<ItemReference> LoadItemReference(string itemCode)
        {
            return ItemReferenceMgr.GetItemReference(itemCode);
        }

        public ItemReference LoadItemReference(string itemCode, string partyCode, string referenceCode)
        {
            return ItemReferenceMgr.LoadItemReference(itemCode, partyCode, referenceCode);
        }

        public void UpdateItemReference(ItemReference itemRef)
        {
            ItemReferenceMgr.UpdateItemReference(itemRef);
        }

        public void DeleteItemReference(ItemReference itemRef)
        {
            ItemReferenceMgr.DeleteItemReference(itemRef);
        }
    }


    public class InProcessLocationMgrProxy
    {
        private IInProcessLocationMgr InProcessLocationMgr
        {
            get
            {
                return ServiceLocator.GetService<IInProcessLocationMgr>("InProcessLocationMgr.service");
            }
        }

        private ICriteriaMgr CriteriaMgr
        {
            get
            {
                return ServiceLocator.GetService<ICriteriaMgr>("CriteriaMgr.service");
            }
        }

        public InProcessLocationMgrProxy()
        {
        }

        public void CreateInProcessLocation(InProcessLocation entity)
        {
            InProcessLocationMgr.CreateInProcessLocation(entity);
        }

        public InProcessLocation LoadInProcessLocation(string code)
        {
            if (code != null && code != string.Empty)
            {
                return InProcessLocationMgr.LoadInProcessLocation(code);
            }
            else
                return null;
        }

        public void UpdateItemReference(InProcessLocation entity)
        {
            InProcessLocationMgr.UpdateInProcessLocation(entity);
        }

        public void DeleteItemReference(InProcessLocation entity)
        {
            InProcessLocationMgr.DeleteInProcessLocation(entity);
        }
    }

    public class RoutingMgrProxy
    {
        private IRoutingMgr RoutingMgr
        {
            get
            {
                return ServiceLocator.GetService<IRoutingMgr>("RoutingMgr.service");
            }
        }

        public RoutingMgrProxy()
        {
        }

        public void CreateRouting(Routing routing)
        {
            RoutingMgr.CreateRouting(routing);
        }

        public Routing LoadRouting(string code)
        {
            return RoutingMgr.LoadRouting(code);
        }

        public void UpdateRouting(Routing routing)
        {
            RoutingMgr.UpdateRouting(routing);
        }

        public void DeleteRouting(Routing routing)
        {
            RoutingMgr.DeleteRouting(routing);
        }
    }

    public class RoutingDetailMgrProxy
    {
        private IRoutingDetailMgr RoutingDetailMgr
        {
            get
            {
                return ServiceLocator.GetService<IRoutingDetailMgr>("RoutingDetailMgr.service");
            }
        }

        public RoutingDetailMgrProxy()
        {
        }

        public void CreateRoutingDetail(RoutingDetail routingDetail)
        {
            RoutingDetailMgr.CreateRoutingDetail(routingDetail);
        }

        public RoutingDetail LoadRoutingDetail(int ID)
        {
            return RoutingDetailMgr.LoadRoutingDetail(ID);
        }

        public void UpdateRoutingDetail(RoutingDetail routingDetail)
        {
            RoutingDetailMgr.UpdateRoutingDetail(routingDetail);
        }

        public void DeleteRoutingDetail(RoutingDetail routingDetail)
        {
            RoutingDetailMgr.DeleteRoutingDetail(routingDetail);
        }
    }

    public class EmployeeMgrProxy
    {
        private IEmployeeMgr EmployeeMgr
        {
            get
            {
                return ServiceLocator.GetService<IEmployeeMgr>("EmployeeMgr.service");
            }
        }

        public EmployeeMgrProxy()
        {
        }

        public void CreateEmployee(Employee employee)
        {
            EmployeeMgr.CreateEmployee(employee);
        }

        public Employee LoadEmployee(string code)
        {
            return EmployeeMgr.LoadEmployee(code);
        }

        public void UpdateEmployee(Employee employee)
        {
            EmployeeMgr.UpdateEmployee(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            EmployeeMgr.DeleteEmployee(employee);
        }
    }

    public class ClientMgrProxy
    {
        private IClientMgr ClientMgr
        {
            get
            {
                return ServiceLocator.GetService<IClientMgr>("ClientMgr.service");
            }
        }

        public ClientMgrProxy()
        {
        }

        public void CreateClient(Client client)
        {
            ClientMgr.CreateClient(client);
        }

        public Client LoadClient(string ClientId)
        {
            return ClientMgr.LoadClient(ClientId);
        }

        public void UpdateClient(Client client)
        {
            ClientMgr.UpdateClient(client);
        }

        public void DeleteClient(Client client)
        {
            ClientMgr.DeleteClient(client);
        }
    }

    public class BillMgrProxy
    {
        private IBillMgr BillMgr
        {
            get
            {
                return ServiceLocator.GetService<IBillMgr>("BillMgr.service");
            }
        }

        public BillMgrProxy()
        {
        }

        public Bill LoadBill(string billNo, bool includeDetail)
        {
            return BillMgr.LoadBill(billNo, includeDetail);
        }
    }

    public class ReceiptMgrProxy
    {
        private IReceiptMgr ReceiptMgr
        {
            get
            {
                return ServiceLocator.GetService<IReceiptMgr>("ReceiptMgr.service");
            }
        }

        private ICriteriaMgr CriteriaMgr
        {
            get
            {
                return ServiceLocator.GetService<ICriteriaMgr>("CriteriaMgr.service");
            }
        }

        public ReceiptMgrProxy()
        {
        }

        public void CreateReceipt(Receipt entity)
        {
            ReceiptMgr.CreateReceipt(entity);
        }

        public Receipt LoadReceipt(string code)
        {
            if (code != null && code != string.Empty)
            {
                return ReceiptMgr.LoadReceipt(code);
            }
            else
                return null;
        }
    }

    public class HuMgrProxy
    {
        private IHuMgr HuMgr
        {
            get
            {
                return ServiceLocator.GetService<IHuMgr>("HuMgr.service");
            }
        }

        public HuMgrProxy()
        {
        }

        public void CreateHu(Hu hu)
        {
            HuMgr.CreateHu(hu);
        }

        public Hu LoadHu(string code)
        {
            return HuMgr.LoadHu(code);
        }

        public void UpdateHu(Hu hu)
        {
            HuMgr.UpdateHu(hu);
        }

        public void DeleteHu(Hu hu)
        {
            HuMgr.DeleteHu(hu);
        }
    }
    public class StorageAreaMgrProxy
    {
        private IStorageAreaMgr TheStorageAreaMgr
        {
            get
            {
                return ServiceLocator.GetService<IStorageAreaMgr>("StorageAreaMgr.service");
            }
        }

        public StorageAreaMgrProxy()
        {
        }

        public void CreateStorageArea(StorageArea storageArea)
        {
            TheStorageAreaMgr.CreateStorageArea(storageArea);
        }

        public StorageArea LoadStorageArea(string code)
        {
            return TheStorageAreaMgr.LoadStorageArea(code);
        }

        public void UpdateStorageArea(StorageArea storageArea)
        {
            TheStorageAreaMgr.UpdateStorageArea(storageArea);
        }

        public void DeleteStorageArea(StorageArea storageArea)
        {
            TheStorageAreaMgr.DeleteStorageArea(storageArea);
        }
    }

    public class StorageBinMgrProxy
    {
        private IStorageBinMgr TheStorageBinMgr
        {
            get
            {
                return ServiceLocator.GetService<IStorageBinMgr>("StorageBinMgr.service");
            }
        }

        public StorageBinMgrProxy()
        {
        }

        public void CreateStorageBin(StorageBin storageBin)
        {
            TheStorageBinMgr.CreateStorageBin(storageBin);
        }

        public StorageBin LoadStorageBin(string code)
        {
            return TheStorageBinMgr.LoadStorageBin(code);
        }

        public void UpdateStorageBin(StorageBin storageBin)
        {
            TheStorageBinMgr.UpdateStorageBin(storageBin);
        }

        public void DeleteStorageBin(StorageBin storageBin)
        {
            TheStorageBinMgr.DeleteStorageBin(storageBin);
        }
    }

    public class BatchTriggerMgrProxy
    {
        private IBatchTriggerMgr BatchTriggerMgr
        {
            get
            {
                return ServiceLocator.GetService<IBatchTriggerMgr>("BatchTriggerMgr.service");
            }
        }

        public BatchTriggerMgrProxy()
        {
        }

        public IList<BatchTrigger> GetActiveTrigger()
        {
            return BatchTriggerMgr.GetActiveTrigger();
        }

        public void UpdateBatchTrigger(BatchTrigger entity)
        {
            BatchTriggerMgr.UpdateBatchTrigger(entity);
        }

    }

    public class CarrierMgrProxy
    {
        private ICarrierMgr CarrierMgr
        {
            get
            {
                return ServiceLocator.GetService<ICarrierMgr>("CarrierMgr.service");
            }
        }

        public CarrierMgrProxy()
        {
        }

        public void CreateCarrier(Carrier carrier)
        {
            CarrierMgr.CreateCarrier(carrier);
        }

        public Carrier LoadCarrier(string code)
        {
            return CarrierMgr.LoadCarrier(code);
        }

        public void UpdateCarrier(Carrier carrier)
        {
            CarrierMgr.UpdateCarrier(carrier);
        }

        public void DeleteCarrier(Carrier carrier)
        {
            CarrierMgr.DeleteCarrier(carrier);
        }
    }

    public class VehicleMgrProxy
    {
        private IVehicleMgr VehicleMgr
        {
            get
            {
                return ServiceLocator.GetService<IVehicleMgr>("VehicleMgr.service");
            }
        }

        public VehicleMgrProxy()
        {
        }

        public void CreateVehicle(Vehicle vehicle)
        {
            VehicleMgr.CreateVehicle(vehicle);
        }

        public Vehicle LoadVehicle(string code)
        {
            return VehicleMgr.LoadVehicle(code);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            VehicleMgr.UpdateVehicle(vehicle);
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            VehicleMgr.DeleteVehicle(vehicle);
        }
    }

    public class TransportationAddressMgrProxy
    {
        private ITransportationAddressMgr TransportationAddressMgr
        {
            get
            {
                return ServiceLocator.GetService<ITransportationAddressMgr>("TransportationAddressMgr.service");
            }
        }

        public TransportationAddressMgrProxy()
        {
        }

        public void CreateTransportationAddress(TransportationAddress transportationAddress)
        {
            TransportationAddressMgr.CreateTransportationAddress(transportationAddress);
        }

        public TransportationAddress LoadTransportationAddress(Int32 id)
        {
            return TransportationAddressMgr.LoadTransportationAddress(id);
        }

        public void UpdateTransportationAddress(TransportationAddress transportationAddress)
        {
            TransportationAddressMgr.UpdateTransportationAddress(transportationAddress);
        }

        public void DeleteTransportationAddress(TransportationAddress transportationAddress)
        {
            TransportationAddressMgr.DeleteTransportationAddress(transportationAddress);
        }
    }

    public class TransportationRouteMgrProxy
    {
        private ITransportationRouteMgr TransportationRouteMgr
        {
            get
            {
                return ServiceLocator.GetService<ITransportationRouteMgr>("TransportationRouteMgr.service");
            }
        }

        public TransportationRouteMgrProxy()
        {
        }

        public void CreateTransportationRoute(TransportationRoute transportationRoute)
        {
            TransportationRouteMgr.CreateTransportationRoute(transportationRoute);
        }

        public TransportationRoute LoadTransportationRoute(string code)
        {
            return TransportationRouteMgr.LoadTransportationRoute(code);
        }

        public void UpdateTransportationRoute(TransportationRoute transportationRoute)
        {
            TransportationRouteMgr.UpdateTransportationRoute(transportationRoute);
        }

        public void DeleteTransportationRoute(TransportationRoute transportationRoute)
        {
            TransportationRouteMgr.DeleteTransportationRoute(transportationRoute);
        }
    }

    public class TransportationRouteDetailMgrProxy
    {
        private ITransportationRouteDetailMgr TransportationRouteDetailMgr
        {
            get
            {
                return ServiceLocator.GetService<ITransportationRouteDetailMgr>("TransportationRouteDetailMgr.service");
            }
        }

        public TransportationRouteDetailMgrProxy()
        {
        }

        public void CreateTransportationRouteDetail(TransportationRouteDetail transportationRouteDetail)
        {
            TransportationRouteDetailMgr.CreateTransportationRouteDetail(transportationRouteDetail);
        }

        public TransportationRouteDetail LoadTransportationRouteDetail(Int32 id)
        {
            return TransportationRouteDetailMgr.LoadTransportationRouteDetail(id);
        }

        public void UpdateTransportationRouteDetail(TransportationRouteDetail transportationRouteDetail)
        {
            TransportationRouteDetailMgr.UpdateTransportationRouteDetail(transportationRouteDetail);
        }

        public void DeleteTransportationRouteDetail(TransportationRouteDetail transportationRouteDetail)
        {
            TransportationRouteDetailMgr.DeleteTransportationRouteDetail(transportationRouteDetail);
        }
    }

    public class TransportPriceListMgrProxy
    {
        private ITransportPriceListMgr TransportPriceListMgr
        {
            get
            {
                return ServiceLocator.GetService<ITransportPriceListMgr>("TransportPriceListMgr.service");
            }
        }

        public TransportPriceListMgrProxy()
        {
        }

        public void CreateTransportPriceList(TransportPriceList transportPriceList)
        {
            TransportPriceListMgr.CreateTransportPriceList(transportPriceList);
        }

        public TransportPriceList LoadTransportPriceList(string code)
        {
            return TransportPriceListMgr.LoadTransportPriceList(code);
        }

        public void UpdateTransportPriceList(TransportPriceList transportPriceList)
        {
            TransportPriceListMgr.UpdateTransportPriceList(transportPriceList);
        }

        public void DeleteTransportPriceList(TransportPriceList transportPriceList)
        {
            TransportPriceListMgr.DeleteTransportPriceList(transportPriceList);
        }
    }

    public class TransportPriceListDetailMgrProxy
    {
        private ITransportPriceListDetailMgr TransportPriceListDetailMgr
        {
            get
            {
                return ServiceLocator.GetService<ITransportPriceListDetailMgr>("TransportPriceListDetailMgr.service");
            }
        }

        public TransportPriceListDetailMgrProxy()
        {
        }

        public void CreateTransportPriceListDetail(TransportPriceListDetail transportPriceListDetail)
        {
            TransportPriceListDetailMgr.CreateTransportPriceListDetail(transportPriceListDetail);
        }

        public TransportPriceListDetail LoadTransportPriceListDetail(int id)
        {
            return TransportPriceListDetailMgr.LoadTransportPriceListDetail(id);
        }

        public void UpdateTransportPriceListDetail(TransportPriceListDetail transportPriceListDetail)
        {
            TransportPriceListDetailMgr.UpdateTransportPriceListDetail(transportPriceListDetail);
        }

        public void DeleteTransportPriceListDetail(TransportPriceListDetail transportPriceListDetail)
        {
            TransportPriceListDetailMgr.DeleteTransportPriceListDetail(transportPriceListDetail);
        }
    }

    public class ExpenseMgrProxy
    {
        private IExpenseMgr ExpenseMgr
        {
            get
            {
                return ServiceLocator.GetService<IExpenseMgr>("ExpenseMgr.service");
            }
        }

        public ExpenseMgrProxy()
        {
        }

        public void CreateExpense(Expense expense)
        {
            ExpenseMgr.CreateExpense(expense);
        }

        public Expense LoadExpense(string code)
        {
            return ExpenseMgr.LoadExpense(code);
        }

        public void UpdateExpense(Expense expense)
        {
            ExpenseMgr.UpdateExpense(expense);
        }

        public void DeleteExpense(Expense expense)
        {
            ExpenseMgr.DeleteExpense(expense);
        }
    }
    public class TransportationOrderMgrProxy
    {
        private ITransportationOrderMgr TransportationOrderMgr
        {
            get
            {
                return ServiceLocator.GetService<ITransportationOrderMgr>("TransportationOrderMgr.service");
            }
        }

        public TransportationOrder LoadTransportationOrder(string orderNo)
        {
            return TransportationOrderMgr.LoadTransportationOrder(orderNo);
        }
    }


    public class TransportationBillMgrProxy
    {
        private ITransportationBillMgr TransportationBillMgr
        {
            get
            {
                return ServiceLocator.GetService<ITransportationBillMgr>("TransportationBillMgr.service");
            }
        }

        public TransportationBillMgrProxy()
        {
        }

        public TransportationBill LoadTransportationBill(string billNo, bool includeDetail)
        {
            return TransportationBillMgr.LoadTransportationBill(billNo, includeDetail);
        }
    }


    public class FlowTrackMgrProxy
    {
        private IFlowTrackMgr FlowTrackMgr
        {
            get
            {
                return ServiceLocator.GetService<IFlowTrackMgr>("FlowTrackMgr.service");
            }
        }

        public FlowTrack LoadFlowTrack(int id)
        {
            if (id == 0) return null;
            return FlowTrackMgr.LoadFlowTrack(id);
        }

    }

    public class FlowDetailTrackMgrProxy
    {
        private IFlowDetailTrackMgr FlowDetailTrackMgr
        {
            get
            {
                return ServiceLocator.GetService<IFlowDetailTrackMgr>("FlowDetailTrackMgr.service");
            }
        }

        public FlowDetailTrack LoadFlowDetailTrack(int id)
        {
            if (id == 0) return null;
            return FlowDetailTrackMgr.LoadFlowDetailTrack(id);
        }

    }



    public class ShelfMgrProxy
    {
        private IShelfMgr ShelfMgr
        {
            get
            {
                return ServiceLocator.GetService<IShelfMgr>("ShelfMgr.service");
            }
        }

        public ShelfMgrProxy()
        {
        }

        public void CreateShelf(Shelf shelf)
        {
            ShelfMgr.CreateShelf(shelf);
        }

        public Shelf LoadShelf(string code)
        {
            return ShelfMgr.LoadShelf(code);
        }

        public void UpdateShelf(Shelf shelf)
        {
            ShelfMgr.UpdateShelf(shelf);
        }

        public void DeleteShelf(Shelf shelf)
        {
            ShelfMgr.DeleteShelf(shelf);
        }
    }


    public class ShelfItemMgrProxy
    {
        private IShelfItemMgr ShelfItemMgr
        {
            get
            {
                return ServiceLocator.GetService<IShelfItemMgr>("ShelfItemMgr.service");
            }
        }

        public ShelfItemMgrProxy()
        {
        }

        public void CreateShelfItem(ShelfItem shelfItem)
        {
            ShelfItemMgr.CreateShelfItem(shelfItem);
        }

        public ShelfItem LoadShelfItem(int id)
        {
            return ShelfItemMgr.LoadShelfItem(id);
        }

        public void UpdateShelfItem(ShelfItem shelfItem)
        {
            ShelfItemMgr.UpdateShelfItem(shelfItem);
        }

        public void DeleteShelfItem(ShelfItem shelfItem)
        {
            ShelfItemMgr.DeleteShelfItem(shelfItem);
        }
    }

}