using System;
using System.Windows.Forms;

namespace Sconit_SD
{
    public partial class UCLogin : UserControl
    {
        public event Sconit_SD.MainForm.LoginHandler LoginEvent;
        public event Sconit_SD.MainForm.ExitHandler ExitEvent;

        public UCLogin()
        {
            InitializeComponent();
            InitialLogin();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userCode = this.tbUserCode.Text;
                string password = Utility.Md5(this.tbPassword.Text);
                this.LoginEvent(userCode, password);
            }
            catch (Exception ex)
            {
                this.tbUserCode.Focus();
                Utility.ShowMessageBox("未知错误!请与管理员联系!" + ex.Message);
                InitialLogin();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.tbPassword.Text == "cr52401")
            {
                this.ExitEvent();
            }
            else
            {
                this.tbUserCode.Text = string.Empty;
                this.tbPassword.Text = string.Empty;
                this.tbUserCode.Focus();
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

        private void tbUserCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (((e.KeyData & Keys.KeyCode) == Keys.Enter) && this.tbUserCode.Text.Trim() != string.Empty)
            {
                this.tbPassword.Focus();
            }
        }

        private void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (((e.KeyData & Keys.KeyCode) == Keys.Enter) && this.tbPassword.Text.Trim() != string.Empty)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}
