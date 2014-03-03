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
    public partial class UCShipReturn : UserControl
    {
        private ClientMgrWSSoapClient TheClientMgr;
        private User user;
        private Resolver resolver;

        public UCShipReturn(User user)
        {
            InitializeComponent();
            this.gvList.AutoGenerateColumns = false;
            this.gvHuList.AutoGenerateColumns = false;
            this.TheClientMgr = new ClientMgrWSSoapClient();
            this.user = user;
            this.InitialAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Enter))
            {
                this.btnShip_Click(null, null);
                return true;
            }
            else if (keyData == (Keys.Control | Keys.P))
            {
                this.PrintReceiver();
                return true;
            }

            if (this.tbBarCode.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    this.OrderNoScan();
                    return true;
                }
                if (keyData == Keys.Escape)
                {
                    this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_CANCEL;
                    this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                    this.DataBind();
                }
            }
            else if (this.btnShipReturn.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    this.OrderConfirm();
                    return true;
                }
            }
            else if (this.gvList.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.resolver != null && !this.resolver.IsScanHu)
                    {
                        int currentRowIndex = this.gvList.CurrentCell.RowIndex;
                        if (currentRowIndex == this.gvList.Rows.Count - 1)
                        {
                            this.gvHuList.ClearSelection();
                            this.btnShipReturn.Focus();
                        }
                    }
                    else
                    {
                        this.btnShipReturn.Focus();
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OrderNoScan()
        {
            try
            {
                this.gvList.Visible = true;
                this.gvHuList.Visible = false;
                this.tbBarCode.Text = this.tbBarCode.Text.Trim();

                #region 当输入框为空时,按回车焦点跳转
                if (this.tbBarCode.Text.Length == 0 && Utility.IsHasTransformerDetail(this.resolver))
                {
                    if (this.resolver.IsScanHu)
                    {
                        this.btnShipReturn.Focus();
                    }
                    else
                    {
                        this.gvList.Focus();
                        this.gvList.BeginEdit(true);
                    }
                    this.gvListDataBind();
                    return;
                }
                #endregion

                this.resolver.Input = this.tbBarCode.Text;
                this.resolver = TheClientMgr.ScanBarcode(this.resolver);

                this.DataBind();
            }
            catch (FaultException ex)
            {
                this.tbBarCode.Focus();
                this.tbBarCode.Text = string.Empty;
                this.lblMessage.Text = Utility.FormatExMessage(ex.Message);
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message), "扫描条码失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InitialAll();
            }
        }

        private void gvListDataBind()
        {
            List<Transformer> Transformers = new List<Transformer>();
            if (this.resolver.Transformers == null)
            {
                this.resolver.Transformers = new Transformer[] { };
            }
            foreach (Transformer transformer in this.resolver.Transformers)
            {
                if (transformer.Qty > 0)
                {
                    Transformers.Add(transformer);
                }
            }
            this.gvList.DataSource = new BindingList<Transformer>(Transformers);
            if (this.resolver != null && !this.resolver.IsScanHu)
            {
                this.gvList.Columns["Cartons"].Visible = false;
                if (this.gvList.CurrentCell != null)
                {
                    int index = this.gvList.CurrentCell.RowIndex;
                    this.gvList.CurrentCell = this.gvList.Rows[index].Cells["CurrentQty"];
                }
                //else
                //{
                //    this.gvList.CurrentCell = this.gvList.Rows[0].Cells["CurrentQty"];
                //}
                this.gvList.Columns["CurrentQty"].ReadOnly = false;
            }
            else
            {
                this.gvList.Columns["Cartons"].Visible = true;
                this.gvList.Columns["CurrentQty"].ReadOnly = true;
                this.gvHuList.ClearSelection();
            }

            this.gvList.Visible = true;
            this.gvHuList.Visible = false;
        }

        private void DataBind()
        {
            if (this.resolver.Command == BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL)
            {
                this.gvHuListDataBind();
            }
            else
            {
                this.gvListDataBind();
            }
            this.tbBarCode.Text = string.Empty;
            this.lblMessage.Text = this.resolver.Result;
        }

        private void PrintReceiver()
        {
            throw new NotImplementedException();
        }

        private void OrderConfirm()
        {
            try
            {
                if (this.resolver.Transformers != null && this.resolver.Transformers.Length > 0)
                {
                    this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_OK;
                    string ip = TheClientMgr.ScanBarcode(this.resolver).Result;
                    this.InitialAll();
                    if (ip != null || ip.Trim() != string.Empty)
                    {
                        this.lblMessage.Text = "发货成功,ASN号:" + ip;
                    }
                    else
                    {
                        this.lblMessage.Text = "货数失败";
                    }
                }
                else
                {
                    this.InitialAll();
                    this.lblMessage.Text = "发货数不能为空";
                }
                this.DataBind();
            }
            catch (FaultException ex)
            {
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message), "错误:发货失败!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.tbBarCode.Focus();
                //InitialAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InitialAll();
            }
        }

        private void InitialAll()
        {
            this.lblMessage.Text = this.resolver == null ? string.Empty : this.resolver.Result;
            this.resolver = new Resolver();
            this.resolver.UserCode = this.user.Code;
            this.resolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN;
            this.gvList.Visible = true;
            this.gvHuList.Visible = false;

            this.tbBarCode.Text = string.Empty;
            this.tbBarCode.Focus();
            this.DataBind();
        }

        private void gvHuListDataBind()
        {
            List<TransformerDetail> transformerDetailList = new List<TransformerDetail>();
            if (this.resolver.Transformers != null && this.resolver.Transformers.Length > 0)
            {
                foreach (Transformer transformer in this.resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail.CurrentQty != 0)
                            {
                                transformerDetailList.Add(transformerDetail);
                            }
                        }
                    }
                }
                this.gvHuList.DataSource = new BindingList<TransformerDetail>(transformerDetailList);
                this.gvHuList.ClearSelection();
            }

            this.gvList.Visible = false;
            this.gvHuList.Visible = true;
        }

        private void gvHuList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int index = this.gvHuList.CurrentCell.RowIndex;
            //this.gvHuList.CurrentCell = this.gvHuList.Rows[index].Cells["HuQty"];
            //this.gvHuList.BeginEdit(true);
        }

        private void gvHuList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.resolver.Transformers = Utility.SumCurrentQty(this.resolver.Transformers);
            this.DataBind();
            this.tbBarCode.Focus();
        }

        private void gvHuList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(this, "请输入数字");
        }

        private void gvList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!this.resolver.IsScanHu)
            {
                int index = this.gvList.CurrentCell.RowIndex;
                this.gvList.CurrentCell = this.gvList.Rows[index].Cells["CurrentQty"];
                this.gvList.BeginEdit(true);
            }
        }

        private void gvList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(this, "请输入数字");
        }

        private void UCIssue_Load(object sender, EventArgs e)
        {
            this.tbBarCode.Focus();
        }

        private void tbBarCode_TextChanged(object sender, EventArgs e)
        {
            if (this.tbBarCode.Text != string.Empty)
            {
                this.lblMessage.Text = string.Empty;
            }
        }

        private void btnShip_Click(object sender, EventArgs e)
        {
            if (resolver.Transformers != null && resolver.Transformers.Length > 0)
            {
                this.OrderConfirm();
            }
            else
            {
                this.tbBarCode.Focus();
                MessageBox.Show(this, "发货数不能为0");
            }
        }

    }
}
