using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using Sconit_CS.SconitWS;
using System.Reflection;

namespace Sconit_CS
{
    public partial class MainForm : Form
    {
        public delegate void LoginHandler(User user, string LoginModule);
        public delegate void ExitHandler();
        public delegate void ModuleSelectionHandler(string moduleName);

        private ClientMgrWSSoapClient TheClientMgr = new ClientMgrWSSoapClient();
        private User user;
        private UserPreference userPreference;
        private int time, TimeOut;
        private Dictionary<string, Type> dicModuleControl = new Dictionary<string, Type>();
        private string isPrintMonitor;
        private string isBatchDevanning;

        public MainForm()
        {
            InitializeComponent();

            TimeOut = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["AutoLogoutInterval"]);
            isPrintMonitor = System.Configuration.ConfigurationSettings.AppSettings["isPrintMonitor"];
            isBatchDevanning = System.Configuration.ConfigurationSettings.AppSettings["isBatchDevanning"];

            if (TimeOut == 0)
            {
                this.autoLogoutTimer.Enabled = false;
            }
            this.ucLogin.LoginEvent += new LoginHandler(this.ProcessLoginEvent);
            this.ucLogin.ExitEvent += new ExitHandler(this.ProcessExitEvent);

            #region Dictionary定义
            #region Production
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_ONLINE, typeof(UCOnline));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_OFFLINE, typeof(UCOfflineHu));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_MATERIALIN, typeof(UCMaterialIn));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_FLUSHBACK, typeof(UCFlushBack));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK, typeof(UCRepack));
            #endregion

            #region Logistics
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP, typeof(UCShip));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER, typeof(UCShip));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE, typeof(UCReceipt));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN, typeof(UCReturn));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN, typeof(UCReturn));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_TRANSFER, typeof(UCInvTransfer));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPCONFIRM, typeof(UCReceipt));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLISTONLINE, typeof(UCOnline));
            #endregion

            #region Inventory
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING, typeof(UCDevanning));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST, typeof(UCPickList));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PUTAWAY, typeof(UCPutAway));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKUP, typeof(UCPickUp));
            //dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECTION, typeof(UCInspection));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECT, typeof(UCInspect));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING, typeof(UCStockTaking));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_HUSTATUS, typeof(UCHuStatus));
            dicModuleControl.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_REUSE, typeof(UCReuse));
            #endregion
            #endregion

            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.TopMost = true;
            //this.MinimizeBox = false;
            //this.MaximizeBox = false;
        }

        private void ProcessLoginEvent(User user, string LoginModule)
        {
            this.user = user;
            this.userPreference = TheClientMgr.LoadUserPerference(user.Code, BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_TERMINAL);
            if (this.userPreference == null || this.userPreference.Value == null
                || this.userPreference.Value == string.Empty || this.userPreference.Value == BusinessConstants.TRANSFORMER_MODULE_TYPE_SELECTION)
            {
                openModuleSelectionUserControl();
            }
            else
            {
                SwitchModule(userPreference.Value);
            }
        }

        private void ProcessExitEvent()
        {
            this.Dispose();
        }

        private void openModuleSelectionUserControl()
        {
            //如果没有SelectModule权限,则回到登录页面

            if (this.user != null &&
                (this.userPreference == null || this.userPreference.Value == null || this.userPreference.Value == string.Empty
                || this.userPreference.Value == BusinessConstants.TRANSFORMER_MODULE_TYPE_SELECTION))
            {
                if (isPrintMonitor.ToLower() == "true")
                {
                    this.autoLogoutTimer.Enabled = false;
                    this.lbCountDown.Visible = false;
                    this.RemoveOldControls();
                    this.WindowState = FormWindowState.Maximized;
                    UCPrintMonitor ucPrintMonitor = new UCPrintMonitor(this.user, BusinessConstants.TRANSFORMER_MODULE_TYPE_PRINTING);
                    ucPrintMonitor.Anchor = System.Windows.Forms.AnchorStyles.None;
                    ucPrintMonitor.Size = new System.Drawing.Size(this.Width, this.Height);
                    this.plMain.Controls.Add(ucPrintMonitor);
                    this.ActiveControl = ucPrintMonitor;
                    ucPrintMonitor.Focus();
                    return;
                }

                if (isBatchDevanning.ToLower() == "true")
                {
                    this.autoLogoutTimer.Enabled = false;
                    this.lbCountDown.Visible = false;
                    this.RemoveOldControls();
                    this.WindowState = FormWindowState.Maximized;
                    UCBatchDevanning ucBatchDevanning = new UCBatchDevanning(this.user, BusinessConstants.TRANSFORMER_MODULE_TYPE_BATCH_DEVANNING);
                    ucBatchDevanning.Anchor = System.Windows.Forms.AnchorStyles.None;
                    ucBatchDevanning.Size = new System.Drawing.Size(this.Width, this.Height);
                    this.plMain.Controls.Add(ucBatchDevanning);
                    this.ActiveControl = ucBatchDevanning;
                    ucBatchDevanning.Focus();
                    return;
                }

                this.lbCountDown.Visible = false;
                this.RemoveOldControls();
                this.WindowState = FormWindowState.Maximized;
                UCModuleSelection ucModuleSelection = new UCModuleSelection(this.user);
                ucModuleSelection.Anchor = System.Windows.Forms.AnchorStyles.None;
                // ucModuleSelection.Location = new System.Drawing.Point((this.Width - 400) / 2, (this.Height - 300) / 2);
                ucModuleSelection.Size = new System.Drawing.Size(this.Width, this.Height);
                ucModuleSelection.ModuleSelectionEvent += new ModuleSelectionHandler(this.ModuleSelectionCallback);
                this.plMain.Controls.Add(ucModuleSelection);
                this.ActiveControl = ucModuleSelection;
                ucModuleSelection.Focus();
            }
            else
            {
                this.Logout();
            }
        }

        private void ModuleSelectionCallback(string moduleType)
        {
            this.SwitchModule(moduleType);
        }

        private void SwitchModule(string moduleType)
        {
            if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_LOGOUT)
            {
                this.Logout();
            }
            else
            {
                Rectangle scrRect = SystemInformation.WorkingArea; //屏幕大小
                this.RemoveOldControls();
                this.lbCountDown.Visible = true;

                object[] param = new object[2];
                param[0] = this.user;
                param[1] = moduleType;

                Type type = dicModuleControl[moduleType];
                Assembly asm = Assembly.GetExecutingAssembly();
                object obj = asm.CreateInstance(type.FullName, true, BindingFlags.Default, null, param, null, null);

                this.WindowState = FormWindowState.Maximized;
                UserControl userControl = (UserControl)obj;
                this.plMain.Controls.Add(userControl);
                //userControl.Size = new System.Drawing.Size(scrRect.Width, scrRect.Height - 30);
                userControl.Size = new System.Drawing.Size(this.Width, this.Height);
                userControl.Focus();
            }
        }

        private void RemoveOldControls()
        {
            while (this.plMain.Controls.Count > 1)
            {
                this.plMain.Controls.RemoveAt(1);
                //this.plMain.Controls[1].Dispose();
            }

            if (this.plMain.Controls.Contains(this.ucLogin))
            {
                this.plMain.Controls.Remove(this.ucLogin);
            }
        }

        private void autoLogoutTimer_Tick(object sender, EventArgs e)
        {
            if (this.user != null)
            {
                time++;
                int CountDown = TimeOut - time;
                this.lbCountDown.Text = CountDown.ToString();

                //Color
                if (CountDown < 10)
                {
                    this.lbCountDown.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.lbCountDown.ForeColor = System.Drawing.Color.Blue;
                }

                //Logout
                if (CountDown < 0)
                {
                    this.openModuleSelectionUserControl();
                    time = 0;
                }
            }
        }

        protected override bool ProcessCmdKey(ref   Message msg, Keys keyData)
        {
            this.time = 0;

            if (keyData == (Keys.Control | Keys.Back))
            {
                this.openModuleSelectionUserControl();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Logout()
        {
            this.user = null;

            if (!this.plMain.Controls.Contains(this.ucLogin))
            {
                this.RemoveOldControls();
                this.plMain.Controls.Add(this.ucLogin);
            }

            this.TopLevel = true;
            this.Activate();
            this.lbCountDown.Visible = false;
            this.ucLogin.InitialLogin();
            //this.WindowState = FormWindowState.Normal;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }
    }
}
