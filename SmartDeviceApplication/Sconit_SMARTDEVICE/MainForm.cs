using System;
using System.Windows.Forms;
using Sconit_SMARTDEVICE.SconitWS;
using System.Drawing;

namespace Sconit_SD
{
    public partial class MainForm : Form
    {
        public delegate void ModuleSelectHandler(string moduleName);
        public delegate void LoginHandler(string userCode, string password);
        public delegate void ExitHandler();
        public delegate void ModuleSelectExitHandler();
        //private Dictionary<string, Type> dicModuleControl = new Dictionary<string, Type>();

        private UCLogin ucLogin;
        private UCModuleSelect ucModuleSelect;
        private UCShip ucShip;
        private UCReturn ucReturn;
        private UCReceive ucReceive;
        private UCTransfer ucTransfer;
        private UCPickList ucPickList;
        private UCPickUp ucPickUp;
        private UCPutAway ucPutAway;
        private UCDevanning ucDevanning;
        private UCStockTaking ucStockTaking;
        private UCOnline ucOnline;
        private UCHuStatus ucHuStatus;
        private UCReuse ucReuse;
        private UCInspection ucInspection;
        //private UCInspect ucInspect;

        private User user;
        private UserPreference userPreference;
        private SmartDeviceMgrWS TheSmartDeviceMgr;

        public MainForm()
        {
            TheSmartDeviceMgr = new SmartDeviceMgrWS();
            InitializeComponent();
            LoadUCLogin();
        }

        private void LoadUCLogin()
        {
            try
            {
                if (this.plMain.Controls.Count > 0)
                {
                    this.plMain.Controls.RemoveAt(0);
                }
                this.ucLogin = new UCLogin();
                //
                this.ucLogin.LoginEvent += new LoginHandler(this.ProcessLoginEvent);
                this.ucLogin.ExitEvent += new ExitHandler(this.ProcessExitEvent);

                this.plMain.Controls.Add(this.ucLogin);

            }
            catch (Exception ex)
            {
                Utility.ShowMessageBox("网络故障!");
                Application.Exit();
            }
        }

        private void ProcessLoginEvent(string userCode, string password)
        {
            try
            {
                User user = TheSmartDeviceMgr.LoadUser(userCode);
                if (user != null && user.Password.ToUpper() == password.ToUpper())
                {
                    this.user = user;

                    userPreference = TheSmartDeviceMgr.LoadUserPerference(user.Code,
                        BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_TERMINAL);
                    string moduleType;
                    if (userPreference == null || userPreference.Value == null || userPreference.Value == string.Empty
                        || userPreference.Value == BusinessConstants.TRANSFORMER_MODULE_TYPE_SELECTION)
                    {
                        moduleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_SELECTION;
                    }
                    else
                    {
                        moduleType = userPreference.Value;
                    }
                    this.SwitchModule(moduleType);
                }
                else
                {
                    Utility.ShowMessageBox("验证失败");
                    this.ucLogin.InitialLogin();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMessageBox(ex.Message);
            }
        }

        private void ProcessExitEvent()
        {
            this.Dispose(true);
        }

        private void SwitchModule(string moduleType)
        {
            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            int width = rect.Width;
            //int height = this.Height;

            if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP
                || moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER)
            {
                this.ucShip = new UCShip(this.user, moduleType);
                this.ucShip.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucShip);
                this.ucShip.InitialAll();
                this.Text = "发货_Sconit_SD";
                this.ucShip.Width = width;
                //this.ucShip.Height = height;
            }
            if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN)
            {
                this.ucReturn = new UCReturn(this.user, moduleType);
                this.ucReturn.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucReturn);
                this.ucReturn.InitialAll();
                this.Text = "发货退货_Sconit_SD";
                this.ucReturn.Width = this.Width;
                //this.ucReturn.Height = this.Height;
            }       
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)
            {
                this.ucReceive = new UCReceive(this.user, moduleType);
                this.ucReceive.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucReceive);
                this.ucReceive.InitialAll();
                this.Text = "收货_Sconit_SD";
                this.ucReceive.Width = width;
                //this.ucReceive.Height = height;
            }
            if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
            {
                this.ucReturn = new UCReturn(this.user, moduleType);
                this.ucReturn.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucReturn);
                this.ucReturn.InitialAll();
                this.Text = "要货退货_Sconit_SD";
                this.ucReturn.Width = width;
                //this.ucReturn.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_TRANSFER)
            {
                this.ucTransfer = new UCTransfer(this.user, moduleType);
                this.ucTransfer.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucTransfer);
                this.ucTransfer.InitialAll();
                this.Text = "移库_Sconit_SD";
                this.ucTransfer.Width = width;
                //this.ucTransfer.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPCONFIRM)
            {
                this.ucReceive = new UCReceive(this.user, moduleType);
                this.ucReceive.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucReceive);
                this.ucReceive.InitialAll();
                this.Text = "发货确认_Sconit_SD";
                this.ucReceive.Width = width;
                //this.ucReceive.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST)
            {
                this.ucPickList = new UCPickList(this.user, moduleType);
                this.ucPickList.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucPickList);
                this.ucPickList.InitialAll();
                this.Text = "拣货_Sconit_SD";
                this.ucPickList.Width = width;
                //this.ucPickList.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PUTAWAY)
            {
                this.ucPutAway = new UCPutAway(this.user, moduleType);
                this.ucPutAway.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucPutAway);
                this.ucPutAway.InitialAll();
                this.Text = "上架_Sconit_SD";
                this.ucPutAway.Width = width;
                //this.ucPutAway.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKUP)
            {
                this.ucPickUp = new UCPickUp(this.user, moduleType);
                this.ucPickUp.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucPickUp);
                this.ucPickUp.InitialAll();
                this.Text = "下架_Sconit_SD";
                this.ucPickUp.Width = width;
                //this.ucPickUp.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING)
            {
                this.ucDevanning = new UCDevanning(this.user, moduleType);
                this.ucDevanning.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucDevanning);
                this.Text = "翻箱_Sconit_SD";
                this.ucDevanning.Width = width;
                this.ucDevanning.InitialAll();
                //this.ucDevanning.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING)
            {
                this.ucStockTaking = new UCStockTaking(this.user, moduleType);
                this.ucStockTaking.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucStockTaking);
                this.Text = "盘点_Sconit_SD";
                this.ucStockTaking.Width = width;
                this.ucStockTaking.InitialAll();
                //this.ucStockTaking.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECTION)
            {
                this.ucInspection = new UCInspection(this.user, moduleType);
                this.ucInspection.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucInspection);
                this.Text = "报验_Sconit_SD";
                this.ucInspection.InitialAll();
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLISTONLINE)
            {
                this.ucOnline = new UCOnline(this.user, moduleType);
                this.ucOnline.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucOnline);
                this.Text = "上线_Sconit_SD";
                this.ucOnline.Width = width;
                this.ucOnline.InitialAll();
                //this.ucOnline.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_HUSTATUS)
            {
                this.ucHuStatus = new UCHuStatus(this.user, moduleType);
                this.ucHuStatus.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucHuStatus);
                this.Text = "条码状态_Sconit_SD";
                this.ucHuStatus.Width = width;
                this.ucHuStatus.InitialAll();
                //this.ucHuStatus.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_REUSE)
            {
                this.ucReuse = new UCReuse(this.user, moduleType);
                this.ucReuse.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                this.SwitchModule(this.ucReuse);
                this.Text = "材料回用_Sconit_SD";
                this.ucReuse.InitialAll();
                this.ucReuse.Width = width;
                //this.ucHuStatus.Height = height;
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SELECTION)
            {
                if (this.user != null && (this.userPreference == null || this.userPreference.Value == null
                    || this.userPreference.Value == string.Empty || this.userPreference.Value == BusinessConstants.TRANSFORMER_MODULE_TYPE_SELECTION))
                {
                    this.ucModuleSelect = new UCModuleSelect(this.user);
                    this.ucModuleSelect.ModuleSelectionEvent += new ModuleSelectHandler(this.SwitchModule);
                    this.ucModuleSelect.ModuleSelectExitEvent += new ModuleSelectExitHandler(this.LoadUCLogin);
                    this.SwitchModule(this.ucModuleSelect);
                    this.Text = "模块选择_Sconit_SD";
                    this.ucModuleSelect.Width = width;
                    //this.ucModuleSelect.Height = height;
                }
                else
                {
                    this.LoadUCLogin();
                }
            }
        }

        private void SwitchModule(UserControl userControl)
        {
            userControl.Location = new System.Drawing.Point(0, 0);
            userControl.Size = new System.Drawing.Size(238, 320);
            this.plMain.Controls.RemoveAt(0);
            this.plMain.Controls.Add(userControl);
            this.Activate();
            userControl.Focus();
        }
    }
}