using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sconit_CS.SconitWS;
using System.ServiceModel;

namespace Sconit_CS
{
    public partial class UCHuStatus : UCBase
    {
        public UCHuStatus(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.gvHuList.Visible = false;
            this.gvList.Visible = false;
            this.btnConfirm.Text = "条码状态";
        }

        protected override void BarCodeScan()
        {
            this.ShowHuStatus();
        }

        protected override void OrderConfirm()
        {
            this.ShowHuStatus();
        }

        private void ShowHuStatus()
        {
            try
            {
                this.resolver.Input = this.tbBarCode.Text.Trim();
                if (this.resolver.Input != string.Empty)
                {
                    this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                    if (this.resolver.Transformers != null
                        && this.resolver.Transformers.Length == 1
                        && this.resolver.Transformers[0].TransformerDetails != null
                        && this.resolver.Transformers[0].TransformerDetails.Length == 1)
                    {
                        TransformerDetail t = this.resolver.Transformers[0].TransformerDetails[0];
                        string text = "条码信息:                                                \n条码:" 
                            + t.HuId + "\n物料代码:" + t.ItemCode
                            + "\n物料描述:" + t.ItemDescription + "\n数量:" + t.Qty.ToString("0.########")
                            + "\n批号:" + t.LotNo + "\n库位:" + t.LocationCode + "\n库格:" + t.StorageBinCode + "\n状态:" + t.Status;
                        string caption = "条码信息:";
                        MessageBox.Show(this, text, caption, MessageBoxButtons.OK);
                        this.resolver.Transformers[0].TransformerDetails = null;
                        this.tbBarCode.Text = string.Empty;
                        this.tbBarCode.Focus();
                    }
                }
            }
            catch (FaultException ex)
            {
                string messageText = Utility.FormatExMessage(ex.Message);
                this.lblMessage.Text = messageText;
                MessageBox.Show(this, messageText);
                this.InitialAll();
                this.gvHuList.Visible = false;
                this.gvList.Visible = false;
                this.tbBarCode.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InitialAll();
                this.gvHuList.Visible = false;
                this.gvList.Visible = false;
                this.tbBarCode.Text = string.Empty;
            }
        }
    }
}
