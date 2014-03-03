using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Dss.Impl
{
    public class CustomerInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private ICustomerMgr customerMgr;
        private IPartyMgr partyMgr;
        private IUserMgr userMgr;
        private IShipAddressMgr shipAddressMgr;
        private IBillAddressMgr billAddressMgr;

        private string[] Customer2CustomerFields = new string[] 
            { 
                "Code",
                "Name"
            };

        private string[] ShipAddress2ShipAddressFields = new string[] 
            { 
                "Code",
                "Address",
                "ContactPersonName",
                "TelephoneNumber",
                "Fax",
                "MobilePhone",
                "PostalCode"
            };

        private string[] BillAddress2BillAddressFields = new string[] 
            { 
                "Code",
                "Address",
                "ContactPersonName",
                "TelephoneNumber",
                "Fax",
                "MobilePhone",
                "PostalCode"
            };


        public CustomerInboundMgr(ICustomerMgr customerMgr,
            IUserMgr userMgr,
            IShipAddressMgr shipAddressMgr,
            IBillAddressMgr billAddressMgr,
            IPartyMgr partyMgr,
            IDssImportHistoryMgr dssImportHistoryMgr)
            : base(dssImportHistoryMgr)
        {
            this.customerMgr = customerMgr;
            this.userMgr = userMgr;
            this.shipAddressMgr = shipAddressMgr;
            this.billAddressMgr = billAddressMgr;
            this.partyMgr = partyMgr;
        }

        protected override object DeserializeForDelete(DssImportHistory dssImportHistory)
        {
            return this.Deserialize(dssImportHistory);
        }

        protected override object DeserializeForCreate(DssImportHistory dssImportHistory)
        {
            return this.Deserialize(dssImportHistory);
        }

        private object Deserialize(DssImportHistory dssImportHistory)
        {
            Customer customer = new Customer();
            customer.Code = dssImportHistory[1].ToUpper();
            customer.Name = dssImportHistory[2];

            ShipAddress shipAddress = new ShipAddress();
            shipAddress.Code = "S_" + customer.Code.ToUpper();
            shipAddress.Address = dssImportHistory[3];
            shipAddress.ContactPersonName = dssImportHistory[12];
            shipAddress.TelephoneNumber = dssImportHistory[13] + dssImportHistory[14];
            shipAddress.Fax = dssImportHistory[15];
            shipAddress.MobilePhone = dssImportHistory[17] + dssImportHistory[18];
            shipAddress.PostalCode = dssImportHistory[8];
            shipAddress.Party = customer;

            BillAddress billAddress = new BillAddress();
            billAddress.Code = "B_"+customer.Code.ToUpper() ;
            billAddress.Address = dssImportHistory[3];
            billAddress.ContactPersonName = dssImportHistory[12];
            billAddress.TelephoneNumber = dssImportHistory[13] + dssImportHistory[14];
            billAddress.Fax = dssImportHistory[15];
            billAddress.MobilePhone = dssImportHistory[17] + dssImportHistory[18];
            billAddress.PostalCode = dssImportHistory[8];
            billAddress.Party = customer;

            IList<object> list = new List<object>();
            list.Add(customer);
            list.Add(shipAddress);
            list.Add(billAddress);
            return list;
        }

        protected override void CreateOrUpdateObject(object obj)
        {
            IList<object> list = (IList<object>)obj;
            Customer customer = (Customer)list[0];
            ShipAddress shipAddress = (ShipAddress)list[1];
            BillAddress billAddress = (BillAddress)list[2];

            //Party newParty = this.partyMgr.LoadParty(customer.Code);
            Customer oldCustomer = this.customerMgr.LoadCustomer(customer.Code);
            if (oldCustomer == null)
            {
                customer.IsActive = true;
                //customer.LastModifyUser = this.userMgr.GetMonitorUser();
                //customer.LastModifyDate = DateTime.Now;
                this.customerMgr.CreateCustomer(customer);
            }
            else
            {
                CloneHelper.CopyProperty(customer, oldCustomer, this.Customer2CustomerFields);
                //newCustomer.LastModifyUser = this.userMgr.GetMonitorUser();
                //newCustomer.LastModifyDate = DateTime.Now;
                this.customerMgr.UpdateCustomer(oldCustomer);
            }


            BillAddress newBillAddress = this.billAddressMgr.LoadBillAddress(billAddress.Code);
            if (newBillAddress == null)
            {

                billAddress.IsActive = true;
                //billAddress.LastModifyUser = this.userMgr.GetMonitorUser();
                // billAddress.LastModifyDate = DateTime.Now;
                this.billAddressMgr.CreateBillAddress(billAddress);
            }
            else
            {
                CloneHelper.CopyProperty(billAddress, newBillAddress, this.BillAddress2BillAddressFields);
                //newBillAddress.LastModifyUser = this.userMgr.GetMonitorUser();
                //newBillAddress.LastModifyDate = DateTime.Now;
                this.billAddressMgr.UpdateBillAddress(newBillAddress);
            }

            ShipAddress newShipAddress = this.shipAddressMgr.LoadShipAddress(shipAddress.Code);
            if (newShipAddress == null)
            {
                shipAddress.IsActive = true;
                //shipAddress.LastModifyUser = this.userMgr.GetMonitorUser();
                //shipAddress.LastModifyDate = DateTime.Now;
                this.shipAddressMgr.CreateShipAddress(shipAddress);
            }
            else
            {
                CloneHelper.CopyProperty(shipAddress, newShipAddress, this.ShipAddress2ShipAddressFields);
                // newShipAddress.LastModifyUser = this.userMgr.GetMonitorUser();
                // newShipAddress.LastModifyDate = DateTime.Now;
                this.shipAddressMgr.UpdateShipAddress(newShipAddress);
            }

        }

        protected override void DeleteObject(object obj)
        {
            IList<object> list = (IList<object>)obj;
            Customer customer = this.customerMgr.LoadCustomer(((Customer)list[0]).Code);
            ShipAddress shipAddress = (ShipAddress)list[1];
            BillAddress billAddress = (BillAddress)list[2];

            if (customer != null)
            {
                customer.IsActive = false;
                this.customerMgr.UpdateCustomer(customer);

                shipAddress.IsActive = false;
                this.shipAddressMgr.UpdateShipAddress(shipAddress);

                billAddress.IsActive = false;
                this.billAddressMgr.UpdateBillAddress(billAddress);
            }
        }
    }
}
