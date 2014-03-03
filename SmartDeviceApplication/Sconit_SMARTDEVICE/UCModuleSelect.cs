using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sconit_SMARTDEVICE.SconitWS;

namespace Sconit_SD
{
    public partial class UCModuleSelect : UserControl
    {
        public event Sconit_SD.MainForm.ModuleSelectHandler ModuleSelectionEvent;
        public event Sconit_SD.MainForm.ModuleSelectExitHandler ModuleSelectExitEvent;
        private User user;
        private Dictionary<string, List<object>> dicModule = new Dictionary<string, List<object>>();

        public UCModuleSelect(User user)
        {
            InitializeComponent();

            this.user = user;
            this.CheckAccessPermission();
        }

        private void CheckAccessPermission()
        {
            #region Object Dictionary
            Dictionary<string, List<object>> dicObject = new Dictionary<string, List<object>>();
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE, new List<object> { this.btnReceive, 0, Keys.D1, Keys.NumPad1 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN, new List<object> { this.btnReceiveReturn, 0, Keys.D2, Keys.NumPad2 });

            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLISTONLINE, new List<object> { this.btnPickListOnline, 1, Keys.D1, Keys.NumPad1 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST, new List<object> { this.btnPickList, 1, Keys.D2, Keys.NumPad2 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP, new List<object> { this.btnShip, 1, Keys.D3, Keys.NumPad3 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER, new List<object> { this.btnShipOrder, 1, Keys.D4, Keys.NumPad4 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN, new List<object> { this.btnShipReturn, 1, Keys.D5, Keys.NumPad5 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPCONFIRM, new List<object> { this.btnShipConfirm, 1, Keys.D6, Keys.NumPad6 });

            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_TRANSFER, new List<object> { this.btnTransfer, 2, Keys.D1, Keys.NumPad1 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PUTAWAY, new List<object> { this.btnPutAway, 2, Keys.D2, Keys.NumPad2 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKUP, new List<object> { this.btnPickUp, 2, Keys.D3, Keys.NumPad3 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING, new List<object> { this.btnDevanning, 2, Keys.D4, Keys.NumPad4 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING, new List<object> { this.btnStockTaking, 2, Keys.D5, Keys.NumPad5 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_HUSTATUS, new List<object> { this.btnHuStatus, 2, Keys.D6, Keys.NumPad6 });
            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_REUSE, new List<object> { this.btnReuse, 2, Keys.D7, Keys.NumPad7 });

            dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECTION, new List<object> { this.btnInspection, 3, Keys.D1, Keys.NumPad1 });
            //dicObject.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECT, new List<object> { this.btnInspect, 3, Keys.D2, Keys.NumPad2 });
            #endregion

            SmartDeviceMgrWS TheSmartDeviceMgr = new SmartDeviceMgrWS();
            Permission[] permissionArray = TheSmartDeviceMgr.GetUserPermissions(BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_TERMINAL, user.Code);

            foreach (KeyValuePair<string, List<object>> keyValuePair in dicObject)
            {
                ((Button)((keyValuePair.Value)[0])).Enabled = false;
                foreach (Permission permission in permissionArray)
                {
                    if (permission.Code == keyValuePair.Key)
                    {
                        ((Button)((keyValuePair.Value)[0])).Enabled = true;
                        dicModule.Add(keyValuePair.Key, keyValuePair.Value);
                        break;
                    }
                }
            }
        }

        private void UCModuleSelect_Click(object sender, EventArgs e)
        {
            Button moduleButton = (Button)sender;
            foreach (KeyValuePair<string, List<object>> keyValuePair in dicModule)
            {
                if (moduleButton == (Button)((keyValuePair.Value)[0]))
                {
                    this.ModuleSelectionEvent(keyValuePair.Key);
                    break;
                }
            }
        }

        private void UCModuleSelect_KeyUp(object sender, KeyEventArgs e)
        {
            Keys keyEventArgs = e.KeyData & Keys.KeyCode;

            if (this.tabModuleSelect.Focused && keyEventArgs == Keys.Enter)
            {
                if (this.tabModuleSelect.SelectedIndex == 0)
                {
                    this.tabModuleSelect.SelectedIndex = 1;
                }
                else if (this.tabModuleSelect.SelectedIndex == 1)
                {
                    this.tabModuleSelect.SelectedIndex = 2;
                }
                else if (this.tabModuleSelect.SelectedIndex == 2)
                {
                    this.tabModuleSelect.SelectedIndex = 3;
                }
                else if (this.tabModuleSelect.SelectedIndex == 3)
                {
                    this.tabModuleSelect.SelectedIndex = 0;
                }
            }
            else
            {
                foreach (KeyValuePair<string, List<object>> keyValuePair in dicModule)
                {
                    Button dicButton = (Button)((keyValuePair.Value)[0]);
                    int tabPageIndex = (int)((keyValuePair.Value)[1]);
                    Keys keyD = (Keys)((keyValuePair.Value)[2]);
                    Keys keyNumPad = (Keys)((keyValuePair.Value)[3]);

                    if ((keyEventArgs == keyD || keyEventArgs == keyNumPad) && (tabPageIndex == this.tabModuleSelect.SelectedIndex)
                        || (!this.tabModuleSelect.Focused && (Button)sender == dicButton && keyEventArgs == Keys.Enter))
                    {
                        this.ModuleSelectionEvent(keyValuePair.Key);
                        break;
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.ModuleSelectExitEvent();
        }
    }
}
