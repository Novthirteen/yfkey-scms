using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sconit_CS.SconitWS;
using System.ServiceModel;

namespace Sconit_CS
{
    public partial class UCDevanning : UserControl
    {
        private Resolver resolver;
        private ClientMgrWSSoapClient TheClientMgr;
        private User user;
        private Timer timerCtrl;

        public UCDevanning(User user, string moduleType)
        {
            InitializeComponent();
            this.dgInList.AutoGenerateColumns = false;
            this.dgOutList.AutoGenerateColumns = false;
            TheClientMgr = new ClientMgrWSSoapClient();
            this.user = user;
            this.timerCtrl = new Timer();
            this.timerCtrl.Tick += new System.EventHandler(this.TimerCtrl_Tick);
            this.InitialAll();
        }
        private void InitialAll()
        {
            this.tbBarCode.Text = string.Empty;
            this.tbBarCode.Focus();
            this.lblMessage.Text = string.Empty;
            this.resolver = new Resolver();
            this.resolver.UserCode = this.user.Code;
            this.resolver.IOType = BusinessConstants.IO_TYPE_IN;
            this.resolver.Transformers = new Transformer[2];
            this.resolver.Transformers[0] = new Transformer();
            this.resolver.Transformers[1] = new Transformer();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Enter))
            {
                this.Devanning();
                return true;
            }
            if (this.tbBarCode.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    this.BarCodeScan();
                    return true;
                }
                if (keyData == Keys.Escape)
                {
                    try
                    {
                        if (this.resolver != null)
                        {
                            this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_CANCEL;
                            this.resolver = this.TheClientMgr.ScanBarcode(this.resolver);
                            this.BindTransformer();
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        return true;
                    }
                }
            }
            else if (this.btnDevanning.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.tbBarCode.Text.Trim() != string.Empty)
                    {
                        this.BarCodeScan();
                    }
                    else
                    {
                        this.Devanning();
                    }
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BarCodeScan()
        {
            try
            {
                if (Utility.IsHasTransformer(this.resolver) && this.tbBarCode.Text.Trim() == string.Empty)
                {
                    if (this.resolver.IOType == BusinessConstants.IO_TYPE_IN)
                    {
                        this.resolver.IOType = BusinessConstants.IO_TYPE_OUT;
                        this.lblBarCode.Text = "拆箱后条码";
                        this.tbBarCode.Focus();
                    }
                    else if (this.resolver.Transformers[1] != null && this.resolver.Transformers[1].TransformerDetails != null
                        && this.resolver.Transformers[1].TransformerDetails.Length > 0)
                    {
                        this.btnDevanning.Focus();
                    }
                    return;
                }
                if (this.tbBarCode.Text.Trim() == string.Empty)
                {
                    return;
                }
                this.tbBarCode.Text = this.tbBarCode.Text.Trim();
                this.resolver.Input = this.tbBarCode.Text;
                this.resolver.UserCode = this.user.Code;
                this.resolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING;
                this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                this.BindTransformer();
            }
            catch (FaultException ex)
            {
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message), "获取扫条码信息失败!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.tbBarCode.Text = string.Empty;
                this.tbBarCode.Focus();
                this.lblMessage.Text = Utility.FormatExMessage(ex.Message);
                this.lblMessage.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误, 请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InitialAll();
            }
        }

        private void Devanning()
        {
            try
            {
                this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_OK;
                this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                this.InitialAll();
                this.BindTransformer();
                this.lblMessage.Text = "拆箱成功!";
            }
            catch (FaultException ex)
            {
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message), "错误:拆箱失败!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.tbBarCode.Focus();
                //InitialAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InitialAll();
            }
        }

        private void BindTransformer()
        {
            if (this.resolver != null && this.resolver.Transformers != null && this.resolver.Transformers.Length == 2)
            {
                this.dgInList.DataSource = this.resolver.Transformers[0].TransformerDetails;
                this.dgOutList.DataSource = this.resolver.Transformers[1].TransformerDetails;
            }
            if (this.resolver.IOType == BusinessConstants.IO_TYPE_OUT)
            {
                this.lblBarCode.Text = "拆箱后条码";
            }
            else if (this.resolver.IOType == BusinessConstants.IO_TYPE_IN)
            {
                this.lblBarCode.Text = "拆箱前条码";
            }
            this.tbBarCode.Text = string.Empty;
        }

        private void btnDevanning_Click(object sender, EventArgs e)
        {
            this.Devanning();
        }
        private void tbBarCode_TextChanged(object sender, EventArgs e)
        {
            if (this.tbBarCode.Text != string.Empty)
            {
                this.lblMessage.Text = string.Empty;
            }
        }

        private void tbBarCode_KeyUp(object sender, KeyEventArgs e)
        {

            if (!this.timerCtrl.Enabled)
            {
                this.timerCtrl.Enabled = true;
                this.timerCtrl.Interval = 1000;
            }
        }


        private void TimerCtrl_Tick(object sender, EventArgs e)
        {
            this.tbBarCode.Text = string.Empty;
            this.timerCtrl.Enabled = false;
        }
    }
}
