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
    public partial class UCBase : UserControl
    {
        protected ClientMgrWSSoapClient TheClientMgr;
        protected Resolver resolver;
        protected Resolver originalResolver;
        private bool enableCache;
        private Timer timerCtrl;

        public UCBase(User user, string moduleType)
        {
            InitializeComponent();
            this.resolver = new Resolver();
            this.resolver.UserCode = user.Code;
            this.resolver.ModuleType = moduleType;
            this.gvHuList.AutoGenerateColumns = false;
            this.gvList.AutoGenerateColumns = false;
            this.TheClientMgr = new ClientMgrWSSoapClient();
            this.lblMessage.Text = string.Empty;
            this.enableCache = moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST
                            || moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE
                            //|| moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING
                            ;
            if (this.enableCache)
            {
                this.originalResolver = new Resolver();
            }
            this.timerCtrl = new Timer();
            this.timerCtrl.Tick += new System.EventHandler(this.TimerCtrl_Tick);
            this.InitialAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Enter))
                {
                    this.OrderConfirm();
                    return true;
                }
                else if (keyData == Keys.F1)
                {
                    string message = HelpMessage();
                    if (message != null)
                    {
                        MessageBox.Show(this, message);
                    }
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
                        if (this.enableCache)
                        {
                            Utility.CancelOperation(this.resolver);
                        }
                        else
                        {
                            this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_CANCEL;
                            this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                        }
                        this.DataBind();
                        return true;

                    }
                }
                else if (this.btnConfirm.Focused)
                {
                    if (keyData == Keys.Enter)
                    {
                        if (this.tbBarCode.Text.Trim() != string.Empty)
                        {
                            this.BarCodeScan();
                        }
                        else
                        {
                            this.OrderConfirm();
                        }
                        return true;
                    }
                }
                else if (this.btnQualified.Visible && this.btnQualified.Focused)
                {
                    if (keyData == Keys.Enter)
                    {
                        this.btnQualified_Click(null, null);
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
                                this.btnConfirm.Focus();
                            }
                        }
                        else
                        {
                            this.btnConfirm.Focus();
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.InitialAll();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected virtual void BarCodeScan()
        {
            try
            {
                this.tbBarCode.Text = this.tbBarCode.Text.Trim();
                #region 当输入框为空时,按回车焦点跳转
                if (this.tbBarCode.Text.Trim() == string.Empty && this.gvList.Rows.Count > 0)
                {
                    if (this.resolver.IsScanHu || this.gvHuList.Rows.Count > 0)
                    {
                        this.btnConfirm.Focus();
                        this.gvListDataBind();
                        //this.gvHuList.Visible = false;
                        //this.gvList.Visible = true;
                    }
                    else
                    {
                        this.gvList.Focus();
                        this.gvList.BeginEdit(true);
                    }
                    return;
                }
                if (this.tbBarCode.Text.Trim() == string.Empty)
                {
                    return;
                }
                #endregion

                this.resolver.Input = this.tbBarCode.Text;
                if (this.enableCache)
                {
                    this.originalResolver = Utility.ProcessOriginalResolver(this.resolver, this.originalResolver);
                    this.originalResolver = TheClientMgr.ScanBarcode(this.originalResolver);
                    this.resolver = Utility.MergeResolver(this.resolver, this.originalResolver);
                }
                else
                {
                    this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                }
                this.DataBind();
            }
            catch (FaultException ex)
            {
                string messageText = Utility.FormatExMessage(ex.Message);
                this.lblMessage.Text = messageText;
                MessageBox.Show(this, messageText);
                this.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InitialAll();
            }
        }

        protected virtual void DataBind()
        {
            if (this.resolver.Command == BusinessConstants.CS_BIND_VALUE_TRANSFORMER)
            {
                this.gvListDataBind();
                this.tbBarCode.Text = string.Empty;
            }
            else if (this.resolver.Command == BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL)
            {
                this.gvListDataBind();
                this.gvHuListDataBind();
            }
            else
            {
                this.tbBarCode.Text = string.Empty;
            }
            this.lblResult.Text = this.resolver.Result;
        }

        protected virtual void OrderConfirm()
        {
            try
            {
                if (!Utility.IsHasTransformer(this.resolver))
                {
                    this.DataBind();
                    this.lblMessage.Text = "明细不能为空";
                    this.tbBarCode.Focus();
                    return;
                }
                Utility.RemoveLot(resolver);
                this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_OK;
                this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                this.lblMessage.Text = this.resolver.Result;
                InitialAll();
            }
            catch (FaultException ex)
            {
                this.DataBind();
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

        protected virtual void InitialAll()
        {
            this.gvHuList.Visible = false;
            this.gvList.Visible = true;
            this.resolver.Transformers = null;
            this.resolver.Result = string.Empty;
            this.resolver.BinCode = string.Empty;
            this.resolver.Code = string.Empty;
            this.resolver.CodePrefix = string.Empty;
            this.tbBarCode.Text = string.Empty;
            this.tbBarCode.Focus();
            this.DataBind();
        }

        private void UCBase_Load(object sender, EventArgs e)
        {
            this.tbBarCode.Focus();
        }

        protected virtual void tbBarCode_TextChanged(object sender, EventArgs e)
        {
            if (this.tbBarCode.Text != string.Empty)
            {
                this.lblMessage.Text = string.Empty;
            }
        }

        private void gvHuList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.resolver.Transformers = Utility.SumCurrentQty(this.resolver.Transformers);
            this.gvListDataBind();
            this.tbBarCode.Focus();
        }

        protected virtual void gvHuList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int index = this.gvHuList.CurrentCell.RowIndex;
            //this.gvHuList.CurrentCell = this.gvHuList.Rows[index].Cells["HuQty"];
            //this.gvHuList.BeginEdit(true);
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

        protected virtual void btnConfirm_Click(object sender, EventArgs e)
        {
            this.OrderConfirm();
        }

        protected virtual void gvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.gvList = Utility.RenderDataGridViewBackColor(this.gvList);
        }

        protected virtual void gvHuListDataBind()
        {
            this.gvHuList.Visible = true;
            this.gvList.Visible = false;
            List<TransformerDetail> transformerDetailList = new List<TransformerDetail>();
            //if (this.resolver.Transformers != null && this.resolver.Transformers.Length == 1
            //    && this.resolver.Transformers[0].TransformerDetails != null && this.resolver.Transformers[0].TransformerDetails.Length > 0)
            //{
            //    transformerDetailList = this.resolver.Transformers[0].TransformerDetails;
            //}

            if (Utility.IsHasTransformer(resolver))
            {
                foreach (Transformer transformer in resolver.Transformers)
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
            }

            this.gvHuList.DataSource = new BindingList<TransformerDetail>(transformerDetailList);
            this.gvHuList.ClearSelection();

            // this.lblMessage.Text = this.resolver.Result;
            this.tbBarCode.Text = string.Empty;
        }

        protected virtual void gvListDataBind()
        {
            this.gvHuList.Visible = false;
            this.gvList.Visible = true;
            if (this.resolver.Transformers == null)
            {
                this.resolver.Transformers = new Transformer[] { };
            }
            this.gvList.DataSource = new BindingList<Transformer>(this.resolver.Transformers);
            if (this.resolver != null && !(this.resolver.IsScanHu || this.gvHuList.Rows.Count > 0 ))
            {
                this.gvList.Columns["Cartons"].Visible = false;
                if (this.gvList.CurrentCell != null)
                {
                    int index = this.gvList.CurrentCell.RowIndex;
                    this.gvList.Columns["CurrentQty"].Visible = true;
                    this.gvList.CurrentCell = this.gvList.Rows[index].Cells["CurrentQty"];
                }
                this.gvList.Columns["CurrentQty"].ReadOnly = false;
                //生产下线改为不能修改数量
               // this.gvList.Columns["CurrentQty"].ReadOnly =  this.resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_OFFLINE;
            }
            else
            {
                this.gvList.Columns["Cartons"].Visible = true;
                this.gvList.Columns["CurrentQty"].ReadOnly = true;
                this.gvHuList.ClearSelection();
                this.gvList.ClearSelection();
                this.tbBarCode.Text = string.Empty;
            }
            this.gvList = Utility.RenderDataGridViewBackColor(this.gvList);
            // this.lblMessage.Text = this.resolver.Result;
            this.gvHuList.ClearSelection();
        }

        protected virtual void gvList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl EditingControl = (DataGridViewTextBoxEditingControl)e.Control;
            EditingControl.KeyPress += new KeyPressEventHandler(Utility.DataGridViewDecimalFilter);
        }

        protected virtual void btnQualified_Click(object sender, EventArgs e)
        {

        }

        protected virtual void gvList_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        protected virtual void gvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void UCBase_KeyUp(object sender, KeyEventArgs e)
        {
            //Keys.D0;
            //Keys.NumPad0;
            string key = e.KeyData.ToString();
            if (key.Length == 1)
            {
                this.tbBarCode.Text += key;
            }
            else if (key.Length == 2 && key.StartsWith("D"))
            {
                this.tbBarCode.Text += key.Remove(0, 1);
            }
            else if (key.Length == 7 && key.StartsWith("NUMPAD"))
            {
                this.tbBarCode.Text += key.Remove(0, 6);
            }
            else if (key.ToUpper() == "TAB" || key.ToUpper() == "RETURN")
            {
                return;
            }
            this.tbBarCode.Focus();
            this.tbBarCode.Select(this.tbBarCode.TextLength, 0);
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

        protected virtual string HelpMessage()
        {
            return null;
        }
    }
}
