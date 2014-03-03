using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Configuration;
using Sconit_CS.SconitWS;

namespace Sconit_CS
{
    public partial class UCLogin : UserControl
    {
        public event Sconit_CS.MainForm.LoginHandler LoginEvent;
        public event Sconit_CS.MainForm.ExitHandler ExitEvent;

        public UCLogin()
        {
            InitializeComponent();
            InitialLogin();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ClientMgrWSSoapClient TheClientMgr = new ClientMgrWSSoapClient();
                string userCode = this.tbUserCode.Text;
                string password = Utility.Md5(this.tbPassword.Text);

                User user = TheClientMgr.LoadUser(userCode);
                if (user != null && user.Password.ToUpper() == password.ToUpper() && user.IsActive)
                {
                    this.lblMessage.Text = "登录成功!";
                    this.lblMessage.Visible = true;
                    this.LoginEvent(user, null);
                }
                else
                {
                    this.lblMessage.Text = "登录失败!";
                    this.tbPassword.Text = string.Empty;
                    this.lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "未知错误!请与管理员联系!" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InitialLogin();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string exitPassword = System.Configuration.ConfigurationSettings.AppSettings["ExitPassword"];
            if (this.tbPassword.Text == exitPassword)
            {
                this.ExitEvent();
            }
            else if (this.tbPassword.Text != string.Empty || this.tbUserCode.Text != string.Empty)
            {
                this.tbPassword.Text = string.Empty;
                this.tbUserCode.Text = string.Empty;
                this.tbUserCode.Focus();
            }
            else
            {
                this.tbPassword.Focus();
                this.lblMessage.Text = "请输入退出密码";
                this.lblMessage.Visible = true;
            }
        }

        private void UCLogin_Load(object sender, EventArgs e)
        {

        }

        public void InitialLogin()
        {
            this.tbUserCode.Focus();
            this.tbUserCode.Text = string.Empty;
            this.tbPassword.Text = string.Empty;
            this.lblMessage.Text = string.Empty;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.tbUserCode.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    this.tbPassword.Focus();
                    return true;
                }
            }
            if (this.tbPassword.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    this.btnLogin_Click(this, null);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
