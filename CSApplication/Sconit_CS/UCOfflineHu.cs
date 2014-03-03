using Sconit_CS.SconitWS;
using System.Windows.Forms;
using System;
using System.ServiceModel;

namespace Sconit_CS
{
    public partial class UCOfflineHu : UCBase
    {
        private string caption;
        private int rowIndex;
        private Transformer currentTransformer;
        private bool IsOdd;
        private string message
        {
            get
            {
                if (this.currentTransformer != null)
                {
                    return this.caption + "\n\n" +
                            "                                                                                " +
                            "\n\n成品号:" + this.currentTransformer.ItemCode +
                            "\n\n描述:" + this.currentTransformer.ItemDescription +
                            "\n\n单位:" + this.currentTransformer.UomCode +
                            "\n\n数量:" + this.currentTransformer.CurrentQty.ToString("0.########");
                }
                return null;
            }
        }

        public UCOfflineHu(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnConfirm.Text = "下线";
            this.gvList.Columns["Qty"].HeaderText = "收货批量";
            this.gvList.Columns["CurrentQty"].HeaderText = "收货数";
            this.gvList.Columns["LocationFromCode"].Visible = false;
            this.gvList.Columns["LocationToCode"].Visible = false;
            this.gvList.Columns["Cartons"].Visible = false;
            //this.gvList.Columns["CurrentQty"].Visible = false;
            this.gvList.Columns["OrderNo"].Visible = false;
            this.gvList.Columns["ReceivedQty"].Visible = true;
            this.gvList.Columns["OrderedQty"].Visible = true;
            this.gvList.Columns["ReceivedQty"].Visible = true;
            this.gvList.Columns["Qty"].Visible = false;

            this.IsOdd = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    this.gvList.EndEdit();
                    if (this.gvList.Focused)
                    {
                        this.btnConfirm.Focus();
                        return true;
                    }
                }
                else if (keyData == (Keys.Control | Keys.P))
                {
                    if (this.resolver.PrintUrl != null && this.resolver.PrintUrl != string.Empty)
                    {
                        Utility.PrintOrder(this.resolver.PrintUrl, this, null);
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                this.InitialAll();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void BarCodeScan()
        {
            base.BarCodeScan();
            this.gvList.Focus();
        }

        protected override void gvList_CurrentCellChanged(object sender, EventArgs e)
        {
            this.rowIndex = -1;
            if (this.gvList.SelectedRows.Count > 0)
            {
                this.gvList.BeginEdit(true);
                this.rowIndex = this.gvList.SelectedRows[0].Index;
            }
            if (rowIndex > -1)
            {
                this.gvList["CurrentQty", rowIndex].ReadOnly = false;
                this.gvList["CurrentQty", rowIndex].Value = this.gvList["Qty", rowIndex].Value;
                int Id = int.Parse(this.gvList["Id", rowIndex].Value.ToString());
                if (this.resolver != null && this.resolver.Transformers != null)
                {
                    foreach (Transformer transformer in this.resolver.Transformers)
                    {
                        if (transformer.OrderLocTransId == Id)
                        {
                            this.currentTransformer = transformer;
                        }
                        else
                        {
                            transformer.CurrentQty = 0;
                        }
                    }
                }
            }
        }

        protected override void OrderConfirm()
        {
            try
            {
                if (!Utility.IsHasTransformer(this.resolver))
                {
                    this.lblMessage.Text = "工单明细不能为空";
                    this.tbBarCode.Focus();
                    return;
                }
                MessageBoxButtons messageBoxButtons = MessageBoxButtons.OKCancel;
                MessageBoxDefaultButton messageBoxDefaultButton = MessageBoxDefaultButton.Button2;
                if (this.IsOdd)
                {
                    messageBoxButtons = MessageBoxButtons.YesNoCancel;
                    messageBoxDefaultButton = MessageBoxDefaultButton.Button3;
                }
                DialogResult dialogResult = MessageBox.Show(this, message, caption, messageBoxButtons, MessageBoxIcon.Question, messageBoxDefaultButton);
                switch (dialogResult)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.OK:
                    case DialogResult.Yes:
                        this.resolver.IsOddCreateHu = true;
                        this.OrderOffline();
                        break;
                    case DialogResult.No:
                        this.resolver.IsOddCreateHu = false;
                        this.OrderOffline();
                        break;
                    default:
                        break;
                }
            }
            catch (FaultException ex)
            {
                this.lblMessage.Text = Utility.FormatExMessage(ex.Message);
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message));
                this.tbBarCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InitialAll();
            }
        }

        private void OrderOffline()
        {
            if (!Utility.IsHasTransformer(this.resolver))
            {
                this.lblMessage.Text = "明细不能为空";
                this.tbBarCode.Focus();
                return;
            }
            Utility.RemoveLot(resolver);
            this.resolver.PrintUrl = string.Empty;
            this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_OK;
            this.resolver = TheClientMgr.ScanBarcode(this.resolver);
            if (this.resolver.AutoPrintHu && this.resolver.PrintUrl != null && this.resolver.PrintUrl != string.Empty)
            {
                Utility.PrintOrder(this.resolver.PrintUrl, this, null);
            }
            this.lblMessage.Text = this.resolver.Result;
            InitialAll();
        }

        protected override void gvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            base.gvList_CellValueChanged(sender, e);

            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                decimal currentQty = decimal.Parse(this.gvList[e.ColumnIndex, e.RowIndex].Value.ToString());
                decimal Qty = decimal.Parse(this.gvList["Qty", e.RowIndex].Value.ToString());
                if (currentQty > Qty && Qty >= 0)
                {
                    this.gvList[e.ColumnIndex, e.RowIndex].Value = this.gvList["Qty", e.RowIndex].Value;
                    MessageBox.Show(this, "收货数不能大于收货批量" + this.gvList["Qty", e.RowIndex].Value);
                }
                else if (currentQty < Qty && Qty > 0)
                {
                    this.IsOdd = true;
                    this.caption = "请确认是否强制打箱?";
                }
                else
                {
                    this.IsOdd = false;
                    this.caption = "请确认是否收此成品?";
                }
            }
        }

        protected override void gvList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl EditingControl = (DataGridViewTextBoxEditingControl)e.Control;
            EditingControl.KeyPress += new KeyPressEventHandler(Utility.DataGridViewIntFilter);
        }
    }
}
