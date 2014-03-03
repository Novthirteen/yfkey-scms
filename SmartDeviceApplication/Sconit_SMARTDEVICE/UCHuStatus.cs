using Sconit_SMARTDEVICE.SconitWS;
using System.Web.Services.Protocols;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sconit_SD
{
    public partial class UCHuStatus : UCBase
    {
        private Label lblHuId;
        private Label lblItemCode;
        private Label lblItemDescription;
        private Label lblQty;
        private Label lblLotNo;
        private Label lblLocationCode;
        private Label lblBinCode;
        private Label lblStatus;

        public UCHuStatus(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "条码";
        }

        public override void InitialAll()
        {
            base.InitialAll();
            this.dgList.Visible = false;
            this.lblMessage.Text = string.Empty;
            this.lblMessage.Location = new Point(6, 35);
            ///lblHuId
            this.lblHuId = new Label();
            this.lblHuId.Name = "lblHuId";
            this.lblHuId.Location = new Point(6, 60);
            this.lblHuId.Size = new Size(229, 20);
            this.lblHuId.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.Controls.Add(this.lblHuId);
            ///lblItemCode
            this.lblItemCode = new Label();
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Location = new Point(6, 90);
            this.lblItemCode.Size = new Size(229, 20);
            this.Controls.Add(this.lblItemCode);
            ///lblItemDescription
            this.lblItemDescription = new Label();
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Location = new Point(6, 120);
            this.lblItemDescription.Size = new Size(235, 30);
            this.lblItemDescription.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.Controls.Add(this.lblItemDescription);
            ///lblQty
            this.lblQty = new Label();
            this.lblQty.Name = "lblQty";
            this.lblQty.Location = new Point(6, 150);
            this.lblQty.Size = new Size(229, 20);
            this.Controls.Add(this.lblQty);
            ///lblLotNo
            this.lblLotNo = new Label();
            this.lblLotNo.Name = "lblLotNo";
            this.lblLotNo.Location = new Point(6, 180);
            this.lblLotNo.Size = new Size(229, 20);
            this.Controls.Add(this.lblLotNo);
            ///lblLocationCode
            this.lblLocationCode = new Label();
            this.lblLocationCode.Name = "lblLocationCode";
            this.lblLocationCode.Location = new Point(6, 210);
            this.lblLocationCode.Size = new Size(229, 20);
            this.Controls.Add(this.lblLocationCode);
            ///lblBinCode
            this.lblBinCode = new Label();
            this.lblBinCode.Name = "lblBinCode";
            this.lblBinCode.Location = new Point(6, 240);
            this.lblBinCode.Size = new Size(229, 20);
            this.Controls.Add(this.lblBinCode);
            ///lblStatus
            this.lblStatus = new Label();
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Location = new Point(6, 270);
            this.lblStatus.Size = new Size(229, 20);
            this.Controls.Add(this.lblStatus);
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
                this.dgList.Visible = false;
                this.resolver.Input = this.tbBarCode.Text.Trim();
                if (this.resolver.Input != string.Empty)
                {
                    this.resolver = TheSmartDeviceMgr.ScanBarcode(this.resolver);
                    if (this.resolver.Transformers != null
                        && this.resolver.Transformers.Length == 1
                        && this.resolver.Transformers[0].TransformerDetails != null
                        && this.resolver.Transformers[0].TransformerDetails.Length == 1)
                    {
                        TransformerDetail t = this.resolver.Transformers[0].TransformerDetails[0];
                        //string text = "条码信息:                                                \n条码:"
                        //    + t.HuId + "\n物料代码:" + t.ItemCode
                        //    + "\n物料描述:" + t.ItemDescription + "\n数量:" + t.Qty.ToString("0.########")
                        //    + "\n批号:" + t.LotNo + "\n库位:" + t.LocationCode + "\n状态:" + t.Status;
                        //string caption = "条码信息:";
                        //MessageBox.Show(text, caption);                        
                        this.lblMessage.Text = "条码状态信息:";
                        this.lblHuId.Text = "编号:" + t.HuId;
                        this.lblItemCode.Text = "物料代码:" + t.ItemCode;
                        this.lblItemDescription.Text = "物料描述:" + t.ItemDescription;
                        this.lblQty.Text = "数量:" + t.Qty.ToString("0.########");
                        this.lblLotNo.Text = "批号:" + t.LotNo;
                        this.lblLocationCode.Text = "库位:" + t.LocationCode;
                        this.lblBinCode.Text = "库格:" + t.StorageBinCode;
                        this.lblStatus.Text = "状态:" + t.Status;
                        if (t.Status != BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INVENTORY)
                        {
                            this.lblMessage.ForeColor = Color.Red;
                            this.lblHuId.ForeColor = Color.Red;
                            this.lblItemCode.ForeColor = Color.Red;
                            this.lblItemDescription.ForeColor = Color.Red;
                            this.lblQty.ForeColor = Color.Red;
                            this.lblLotNo.ForeColor = Color.Red;
                            this.lblLocationCode.ForeColor = Color.Red;
                            this.lblStatus.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.lblMessage.ForeColor = Color.Black;
                            this.lblHuId.ForeColor = Color.Black;
                            this.lblItemCode.ForeColor = Color.Black;
                            this.lblItemDescription.ForeColor = Color.Black;
                            this.lblQty.ForeColor = Color.Black;
                            this.lblLotNo.ForeColor = Color.Black;
                            this.lblLocationCode.ForeColor = Color.Black;
                            this.lblStatus.ForeColor = Color.Black;
                        }
                        this.resolver.Transformers[0].TransformerDetails = null;
                        this.tbBarCode.Text = string.Empty;
                        this.tbBarCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = Utility.FormatExMessage(ex.Message);
                this.lblMessage.Visible = true;
                Utility.ShowMessageBox(ex.Message);
                this.resolver.Transformers = null;
                this.tbBarCode.Focus();
                this.tbBarCode.Text = string.Empty;
            }
        }
        protected override void tbBarCode_TextChanged(object sender, EventArgs e)
        {
            this.lblMessage.Visible = true;
        }
    }
}
