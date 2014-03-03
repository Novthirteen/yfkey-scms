using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.ServiceModel;
using Sconit_CS.SconitWS;

namespace Sconit_CS
{
    public partial class UCOnline : UserControl
    {
        private DataTable dt;
        private int maxRow;
        private Resolver resolver;

        public UCOnline(User user, string moduleType)
        {
            InitializeComponent();
            this.resolver = new Resolver();
            this.resolver.UserCode = user.Code;
            this.resolver.ModuleType = moduleType;
            this.maxRow = Convert.ToInt32(ConfigurationSettings.AppSettings["ScanOnline_MaxRow"]);
            this.dt = new DataTable();
            this.lblmessage.Text = string.Empty;
            if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_ONLINE)
            {
                this.btnOnline.Text = "生产上线";
                this.lblWO.Text = "工单号";
                this.gvWODetail.Columns["WONo"].HeaderText = "工单号";
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLISTONLINE)
            {
                this.btnOnline.Text = "拣货上线";
                this.lblWO.Text = "拣货单";
                this.gvWODetail.Columns["WONo"].HeaderText = "拣货单";
            }
        }

        private void UCWOScanOnline_Load(object sender, EventArgs e)
        {
            this.dt.Columns.Add(new DataColumn("WONo", Type.GetType("System.String")));
            this.dt.Columns.Add(new DataColumn("OnlineDate", Type.GetType("System.String")));
            //this.dt.DefaultView.Sort = "OnlineDate DESC";
            this.gvWODetail.DataSource = this.dt;
            this.tbWO.Focus();
        }

        private void tbWO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (this.tbWO.Text.Trim() != "" && this.tbWO.Text.Length > 4)
                {
                    this.CodeScan();
                }
                else
                {
                    this.tbWO.Text = string.Empty;
                    this.lblmessage.Text = "请输入正确的工单号!";
                    if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLISTONLINE)
                    {
                        this.lblmessage.Text = "请输入正确的拣货单号!";
                    }
                    this.lblmessage.ForeColor = Color.Red;
                    this.tbWO.Focus();
                }
            }
        }

        private void CodeScan()
        {
            try
            {
                this.tbWO.Text = this.tbWO.Text.Trim().ToUpper();
                string woNo = this.tbWO.Text;
                this.resolver.Input = woNo;
                ClientMgrWSSoapClient TheClientMgr = new ClientMgrWSSoapClient();

                if (this.resolver.Input == string.Empty || this.resolver.Input == null)
                {
                    this.lblmessage.Text = "请扫描工单";
                    this.tbWO.Focus();
                    return;
                }
                string onlineDate = TheClientMgr.ScanBarcode(resolver).Result;
                this.lblmessage.Text = "工单:" + woNo + "上线成功!";
                if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLISTONLINE)
                {
                    this.lblmessage.Text = "拣货单:" + woNo + "上线成功!";
                }
                this.lblmessage.ForeColor = Color.Green;
                this.ShowOrders(woNo, onlineDate);
                this.tbWO.ForeColor = Color.Black;
                this.tbWO.Text = string.Empty;
                this.tbWO.Focus();
            }
            catch (FaultException ex)
            {
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message));
                this.tbWO.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tbWO.Text = string.Empty;
            }
        }

        private void ShowOrders(string woNo, string onlineDate)
        {
            DataRow drr = dt.NewRow();
            drr[0] = woNo;
            drr[1] = onlineDate;
            dt.Rows.InsertAt(drr, dt.Rows.Count);
            int index = this.gvWODetail.Rows.Count - 1;
            //dt.DefaultView.Sort = "OnlineDate DESC";
            this.gvWODetail.ClearSelection();
            this.gvWODetail.Rows[index].Selected = true;
            this.gvWODetail.FirstDisplayedScrollingRowIndex = index;
            //todu print
        }

        private void tbWO_TextChanged(object sender, EventArgs e)
        {
            if (this.tbWO.Text != string.Empty)
            {
                this.lblmessage.Text = string.Empty;
            }
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            CodeScan();
        }
    }
}
