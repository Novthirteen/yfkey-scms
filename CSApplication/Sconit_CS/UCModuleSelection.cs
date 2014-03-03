using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sconit_CS.SconitWS;
using System.Configuration;
using System.Drawing.Printing;
using Sconit_CS.Properties;

namespace Sconit_CS
{
    public partial class UCModuleSelection : UserControl
    {
        public event Sconit_CS.MainForm.ModuleSelectionHandler ModuleSelectionEvent;
        private User user;
        private Resolver resolver;
        private int row;
        private int maxRow;
        private ClientMgrWSSoapClient TheClientMgr = new ClientMgrWSSoapClient();
        private Dictionary<string, ModuleSelection> dicModuleSelection = new Dictionary<string, ModuleSelection>();
        private Dictionary<string, ModuleSelection> dicUser = new Dictionary<string, ModuleSelection>();

        private class ModuleSelection
        {
            public ModuleSelection(Button Button, TabPage TabPage, Keys Key1, Keys Key2)
            {
                this.Button = Button;
                this.TabPage = TabPage;
                this.Key1 = Key1;
                this.Key2 = Key2;
            }
            public Button Button { get; set; }
            public TabPage TabPage { get; set; }
            public Keys Key1 { get; set; }
            public Keys Key2 { get; set; }
        }

        public UCModuleSelection(User user)
        {
            if (user != null)
            {
                this.user = user;
                this.resolver = new Resolver();
                this.resolver.UserCode = user.Code;
                this.row = 0;
                this.maxRow = Convert.ToInt32(ConfigurationSettings.AppSettings["ScanOnline_MaxRow"]);
                InitializeComponent();
                this.dgList.AutoGenerateColumns = false;

                #region Dictionary定义
                #region Procurement
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE, new ModuleSelection(btnReceive, tabProcurement, Keys.D1, Keys.NumPad1));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN, new ModuleSelection(btnReceiveReturn, tabProcurement, Keys.D2, Keys.NumPad2));
                #endregion

                #region Production
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_ONLINE, new ModuleSelection(btnOnline, tabProduction, Keys.D1, Keys.NumPad1));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_OFFLINE, new ModuleSelection(btnOffline, tabProduction, Keys.D2, Keys.NumPad2));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_MATERIALIN, new ModuleSelection(btnMaterialIn, tabProduction, Keys.D3, Keys.NumPad3));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_FLUSHBACK, new ModuleSelection(btnFlushBack, tabProduction, Keys.D4, Keys.NumPad4));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK, new ModuleSelection(btnRepack, tabProduction, Keys.D5, Keys.NumPad5));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_REUSE, new ModuleSelection(btnReuse, tabProduction, Keys.D6, Keys.NumPad6));
                #endregion

                #region Distribution
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLISTONLINE, new ModuleSelection(btnPickListOnline, tabDistribution, Keys.D1, Keys.NumPad1));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST, new ModuleSelection(btnPickList, tabDistribution, Keys.D2, Keys.NumPad2));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP, new ModuleSelection(btnShip, tabDistribution, Keys.D3, Keys.NumPad3));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER, new ModuleSelection(btnShipOrder, tabDistribution, Keys.D4, Keys.NumPad4));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN, new ModuleSelection(btnShipReturn, tabDistribution, Keys.D5, Keys.NumPad5));
                //dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPCONFIRM, new ModuleSelection(btnShipConfirm, tabDistribution, Keys.D5, Keys.NumPad5));
                #endregion

                #region Inventory
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_TRANSFER, new ModuleSelection(btnInvTransfer, tabInventory, Keys.D1, Keys.NumPad1));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PUTAWAY, new ModuleSelection(btnPutAway, tabInventory, Keys.D2, Keys.NumPad2));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKUP, new ModuleSelection(btnPickUp, tabInventory, Keys.D3, Keys.NumPad3));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING, new ModuleSelection(btnUnboxing, tabInventory, Keys.D4, Keys.NumPad4));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING, new ModuleSelection(btnStockTaking, tabInventory, Keys.D5, Keys.NumPad5));
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_HUSTATUS, new ModuleSelection(btnHuStatus, tabInventory, Keys.D6, Keys.NumPad6));
                //dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECTION, new ModuleSelection(btnInspection, tabInventory, Keys.D0, Keys.NumPad0));
                #endregion

                #region Inspection
                dicModuleSelection.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECT, new ModuleSelection(btnInspect, tabInspection, Keys.D1, Keys.NumPad1));
                #endregion
                #endregion

                dicUser.Clear();
                CheckAccessPermission();

                this.tabModule.SelectedTab = this.tabProduction;
                this.ShowdgList();

                foreach (string fPrinterName in LocalPrinter.GetLocalPrinters())
                {
                    this.comboBoxPrint.Items.Add(fPrinterName);
                }
                this.comboBoxPrint.SelectedItem = Settings.Default.DefaultPrintName1;
            }
            else
            {

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && this.tabModule.Focused)
            {
                if (this.tabModule.SelectedTab == this.tabProcurement)
                {
                    this.tabModule.SelectedTab = this.tabProduction;
                }
                else if (this.tabModule.SelectedTab == this.tabProduction)
                {
                    this.tabModule.SelectedTab = this.tabDistribution;
                }
                else if (this.tabModule.SelectedTab == this.tabDistribution)
                {
                    this.tabModule.SelectedTab = this.tabInventory;
                }
                else if (this.tabModule.SelectedTab == this.tabInventory)
                {
                    this.tabModule.SelectedTab = this.tabInspection;
                }
                else if (this.tabModule.SelectedTab == this.tabInspection)
                {
                    this.tabModule.SelectedTab = this.tabSettings;
                }
                else if (this.tabModule.SelectedTab == this.tabSettings)
                {
                    this.tabModule.SelectedTab = this.tabProcurement;
                }
                this.row = 0;
                this.tabModule.Focus();
                this.ShowdgList();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                ModuleSelectionEvent(BusinessConstants.TRANSFORMER_MODULE_TYPE_LOGOUT);
                return true;
            }
            else if (keyData == (Keys.Control | Keys.P))
            {
                return PrintReceiptNote();
            }
            else if (keyData == (Keys.Control | Keys.F))
            {
                this.row = 0;
                this.ShowdgList();
                return true;
            }
            else if (keyData == Keys.Left)
            {
                this.row -= this.maxRow;
                this.row = this.row < 0 ? 0 : this.row;
                ListReceiptsNotes(resolver.ModuleType);
                return true;
            }
            else if (keyData == Keys.Right)
            {
                this.row += this.maxRow;
                ListReceiptsNotes(resolver.ModuleType);
                return true;
            }
            else
            {
                SwitchModule(null, keyData);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CheckAccessPermission()
        {

            List<Permission> permissionList = new List<Permission>();

            Permission[] permissionArray = TheClientMgr.GetUserPermissions(BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_TERMINAL, user.Code);

            foreach (Permission permission in permissionArray)
            {
                string moduleType = permission.Code;
                if (dicModuleSelection.ContainsKey(moduleType))
                {
                    dicModuleSelection[moduleType].Button.Enabled = true;
                    dicUser.Add(moduleType, dicModuleSelection[moduleType]);
                }
            }
        }

        private void ModuleSelect_Click(object sender, EventArgs e)
        {
            SwitchModule((Button)sender, null);
        }

        private void SwitchModule(Button button, Keys? keyData)
        {
            foreach (KeyValuePair<string, ModuleSelection> moduleSelection in dicUser)
            {
                if (button != null && button == moduleSelection.Value.Button)
                {
                    ModuleSelectionEvent(moduleSelection.Key);
                    break;
                }
                else if (keyData.HasValue && this.tabModule.SelectedTab == moduleSelection.Value.TabPage)
                {
                    if (keyData.Value == moduleSelection.Value.Key1 || keyData.Value == moduleSelection.Value.Key2)
                    {
                        ModuleSelectionEvent(moduleSelection.Key);
                        break;
                    }
                }
            }
        }

        private void ListReceiptsNotes(string moduleType)
        {
            try
            {
                this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_NOTE;
                this.resolver.ModuleType = moduleType;
                this.resolver.Code = row.ToString() + "|" + maxRow.ToString();
                this.resolver.ReceiptNotes = null;
                this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                
                this.dgList.DataSource = resolver.ReceiptNotes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowdgList()
        {
            #region 移除gbList
            if (this.tabDistribution.Controls.Contains(this.gbList))
            {
                this.tabDistribution.Controls.Remove(this.gbList);
            }
            else if (this.tabInventory.Controls.Contains(this.gbList))
            {
                this.tabInventory.Controls.Remove(this.gbList);
            }
            else if (this.tabProcurement.Controls.Contains(this.gbList))
            {
                this.tabProcurement.Controls.Remove(this.gbList);
            }
            else if (this.tabProduction.Controls.Contains(this.gbList))
            {
                this.tabProduction.Controls.Remove(this.gbList);
            }
            #endregion

            //this.dgList.Columns["Sequence"].Visible = true;
            this.dgList.Columns["ReceiptNo"].Visible = false;
            this.dgList.Columns["IpNo"].Visible = false;
            this.dgList.Columns["OrderNo"].Visible = false;
            this.dgList.Columns["PartyFrom"].Visible = false;
            this.dgList.Columns["PartyTo"].Visible = false;
            this.dgList.Columns["Dock"].Visible = false;
            //this.dgList.Columns["CreateDate"].Visible = true;
            //this.dgList.Columns["CreateUser"].Visible = true;
            //hu
            this.dgList.Columns["HuId"].Visible = false;
            this.dgList.Columns["ItemDescription"].Visible = false;
            this.dgList.Columns["UnitCount"].Visible = false;
            this.dgList.Columns["Qty"].Visible = false;

            if (this.tabModule.SelectedTab == this.tabProcurement)
            {
                this.tabProcurement.Controls.Add(this.gbList);
                this.gbList.Text = "要货单";
                this.resolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE;
                this.dgList.Columns["ReceiptNo"].Visible = true;
                this.dgList.Columns["IpNo"].Visible = true;
                this.dgList.Columns["PartyFrom"].Visible = true;
                this.dgList.Columns["PartyTo"].Visible = true;
                this.dgList.Columns["Dock"].Visible = true;

            }
            else if (this.tabModule.SelectedTab == this.tabProduction)
            {
                this.tabProduction.Controls.Add(this.gbList);
                this.gbList.Text = "工单";
                this.resolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_OFFLINE;
                this.dgList.Columns["OrderNo"].Visible = true;
                this.dgList.Columns["HuId"].Visible = true;
                this.dgList.Columns["ItemDescription"].Visible = true;
                this.dgList.Columns["UnitCount"].Visible = true;
                this.dgList.Columns["Qty"].Visible = true;
            }
            else if (this.tabModule.SelectedTab == this.tabDistribution)
            {
                this.tabDistribution.Controls.Add(this.gbList);
                this.gbList.Text = "ASN";
                this.resolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP;
                this.dgList.Columns["IpNo"].Visible = true;
                this.dgList.Columns["PartyFrom"].Visible = true;
                this.dgList.Columns["PartyTo"].Visible = true;
            }
            else if (this.tabModule.SelectedTab == this.tabInventory)
            {
                this.tabInventory.Controls.Add(this.gbList);
                this.gbList.Text = "";
                this.resolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_TRANSFER;
            }
            else if (this.tabModule.SelectedTab == this.tabInspection)
            {

            }
            else if (this.tabModule.SelectedTab == this.tabSettings)
            {

            }
            this.ListReceiptsNotes(this.resolver.ModuleType);
        }

        private void tabModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.row = 0;
            this.ShowdgList();
        }

        private bool PrintReceiptNote()
        {
            if (this.dgList != null && this.dgList.CurrentCell != null)
            {
                int currentRowIndex = this.dgList.CurrentCell.RowIndex;
                if (currentRowIndex > -1)
                {
                    if (this.tabModule.SelectedTab == this.tabProcurement)
                    {
                        this.resolver.Code = this.dgList["ReceiptNo", currentRowIndex].Value.ToString();
                    }
                    else if (this.tabModule.SelectedTab == this.tabDistribution)
                    {
                        this.resolver.Code = this.dgList["IpNo", currentRowIndex].Value.ToString();
                    }
                    else if (this.tabModule.SelectedTab == this.tabProduction)
                    {
                        this.resolver.Code = this.dgList["HuId", currentRowIndex].Value.ToString();
                    }
                    else
                    {
                        return true;
                    }
                    this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_PRINT;
                    this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                    Utility.PrintOrder(this.resolver.PrintUrl,this, null);
                }
            }
            return true;
        }

        private void btnSavePrintSetting_Click(object sender, EventArgs e)
        {
            string printName = this.comboBoxPrint.SelectedItem == null ? string.Empty : this.comboBoxPrint.SelectedItem.ToString();
            Settings.Default.DefaultPrintName1 = printName;
            Settings.Default.Save();
        }

        public class LocalPrinter
        {
            private static PrintDocument fPrintDocument = new PrintDocument();
            /// <summary>
            /// 获取本机默认打印机名称
            /// </summary>
            public static String DefaultPrinter
            {
                get { return fPrintDocument.PrinterSettings.PrinterName; }
            }
            /// <summary>
            /// 获取本机的打印机列表。列表中的第一项就是默认打印机。
            /// </summary>
            public static List<String> GetLocalPrinters()
            {
                List<String> fPrinters = new List<string>();
                fPrinters.Add(DefaultPrinter); // 默认打印机始终出现在列表的第一项
                foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
                {
                    if (!fPrinters.Contains(fPrinterName))
                    {
                        fPrinters.Add(fPrinterName);
                    }
                }
                return fPrinters;
            }
        }
    }
}
