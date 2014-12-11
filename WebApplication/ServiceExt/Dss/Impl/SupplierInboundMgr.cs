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
    public class SupplierInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private ISupplierMgr supplierMgr;
        private IPartyMgr partyMgr;
        private IUserMgr userMgr;
        private IShipAddressMgr shipAddressMgr;
        private IBillAddressMgr billAddressMgr;

        private string[] Supplier2SupplierFields = new string[] 
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


        public SupplierInboundMgr(ISupplierMgr supplierMgr,
            IUserMgr userMgr,
            IShipAddressMgr shipAddressMgr,
            IBillAddressMgr billAddressMgr,
            IPartyMgr partyMgr,
            IDssImportHistoryMgr dssImportHistoryMgr,
            IGenericMgr genericMgr)
            : base(dssImportHistoryMgr, genericMgr)
        {
            this.supplierMgr = supplierMgr;
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

            Supplier supplier = new Supplier();
            supplier.Code = dssImportHistory[1].ToUpper();
            supplier.Name = dssImportHistory[3];

            ShipAddress shipAddress = new ShipAddress();
            shipAddress.Code = "S_" + supplier.Code.ToUpper();
            shipAddress.Address = dssImportHistory[4];
            shipAddress.ContactPersonName = dssImportHistory[13];
            shipAddress.TelephoneNumber = dssImportHistory[14] + dssImportHistory[15];
            shipAddress.Fax = dssImportHistory[16];
            shipAddress.MobilePhone = dssImportHistory[18] + dssImportHistory[19];
            shipAddress.PostalCode = dssImportHistory[9];
            shipAddress.Party = supplier;

            BillAddress billAddress = new BillAddress();
            billAddress.Code = "B_" + supplier.Code.ToUpper() ;
            billAddress.Address = dssImportHistory[4];
            billAddress.ContactPersonName = dssImportHistory[13];
            billAddress.TelephoneNumber = dssImportHistory[14] + dssImportHistory[15];
            billAddress.Fax = dssImportHistory[16];
            billAddress.MobilePhone = dssImportHistory[18] + dssImportHistory[19];
            billAddress.PostalCode = dssImportHistory[9];
            billAddress.Party = supplier;

            IList<object> list = new List<object>();
            list.Add(supplier);
            list.Add(shipAddress);
            list.Add(billAddress);
            return list;
        }

        protected override void CreateOrUpdateObject(object obj)
        {
            IList<object> list = (IList<object>)obj;
            Supplier supplier = (Supplier)list[0];
            ShipAddress shipAddress = (ShipAddress)list[1];
            BillAddress billAddress = (BillAddress)list[2];

            Supplier oldSupplier = supplierMgr.LoadSupplier(supplier.Code);

            if (oldSupplier == null)
            {
                supplier.IsActive = true;
                this.supplierMgr.CreateSupplier(supplier);
            }
            else
            {
                CloneHelper.CopyProperty(supplier, oldSupplier, this.Supplier2SupplierFields);
                this.supplierMgr.UpdateSupplier(oldSupplier);
            }


            BillAddress newBillAddress = this.billAddressMgr.LoadBillAddress(billAddress.Code);
            if (newBillAddress == null)
            {

                billAddress.IsActive = true;
                this.billAddressMgr.CreateBillAddress(billAddress);
            }
            else
            {
                CloneHelper.CopyProperty(billAddress, newBillAddress, this.BillAddress2BillAddressFields);
                this.billAddressMgr.UpdateBillAddress(newBillAddress);
            }

            ShipAddress newShipAddress = this.shipAddressMgr.LoadShipAddress(shipAddress.Code);
            if (newShipAddress == null)
            {
                shipAddress.IsActive = true;
                this.shipAddressMgr.CreateShipAddress(shipAddress);
            }
            else
            {
                CloneHelper.CopyProperty(shipAddress, newShipAddress, this.ShipAddress2ShipAddressFields);
                this.shipAddressMgr.UpdateShipAddress(newShipAddress);
            }

        }

        protected override void DeleteObject(object obj)
        {
            IList<object> list = (IList<object>)obj;
            Supplier supplier = this.supplierMgr.LoadSupplier(((Supplier)list[0]).Code);
            ShipAddress shipAddress = (ShipAddress)list[1];
            BillAddress billAddress = (BillAddress)list[2];

            if (supplier != null)
            {
                supplier.IsActive = false;
                this.supplierMgr.UpdateSupplier(supplier);

                shipAddress.IsActive = false;
                this.shipAddressMgr.UpdateShipAddress(shipAddress);

                billAddress.IsActive = false;
                this.billAddressMgr.UpdateBillAddress(billAddress);
            }
        }
    }
}
