using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sconit_SMARTDEVICE.SconitWS;
using System.Web.Services.Protocols;

namespace Sconit_SD
{
    public partial class UCBase : UserControl
    {
        public event Sconit_SD.MainForm.ModuleSelectHandler ModuleSelectionEvent;

        protected Resolver resolver;
        protected Resolver originalResolver;
        protected DataGridTableStyle ts;
        protected DataGridTextBoxColumn columnHuId;
        protected DataGridTextBoxColumn columnUomCode;
        protected DataGridTextBoxColumn columnStorageBinCode;
        protected DataGridTextBoxColumn columnCurrentQty;
        protected DataGridTextBoxColumn columnItemDescription;
        protected DataGridTextBoxColumn columnCartons;
        protected DataGridTextBoxColumn columnItemCode;
        protected DataGridTextBoxColumn columnLotNo;
        protected DataGridTextBoxColumn columnAdjustQty;

        protected SmartDeviceMgrWS TheSmartDeviceMgr;
        private bool isKeyUp;
        private DataGridCell editCell;
        private bool inEditMode = false;
        private bool inUpdateMode = false;
        private Timer timerCtrl;
        private bool enableCache;

        public UCBase(User user, string moduleType)
        {
            InitializeComponent();
            this.resolver = new Resolver();
            this.resolver.UserCode = user.Code;
            this.resolver.ModuleType = moduleType;
            this.lblMessage.Text = string.Empty;
            this.lblMessage.Visible = false;
            this.timerCtrl = new Timer();
            this.TheSmartDeviceMgr = new SmartDeviceMgrWS();
            this.enableCache = moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST
                            || moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE
                            //|| moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING
                            ;
            if (this.enableCache)
            {
                this.originalResolver = new Resolver();
            }

            //
            this.timerCtrl.Tick += new System.EventHandler(this.TimerCtrl_Tick);

            //初始化表格
            columnItemCode = new DataGridTextBoxColumn();
            columnItemCode.MappingName = "ItemCode";
            columnItemCode.HeaderText = "物料";
            columnItemCode.Width = 100;

            columnUomCode = new DataGridTextBoxColumn();
            columnUomCode.MappingName = "UomCode";
            columnUomCode.HeaderText = "单位";
            columnUomCode.Width = 20;

            columnCurrentQty = new DataGridTextBoxColumn();
            columnCurrentQty.MappingName = "CurrentQty";
            columnCurrentQty.HeaderText = "数量";
            columnCurrentQty.Format = "0.##";
            columnCurrentQty.Width = 40;

            columnCartons = new DataGridTextBoxColumn();
            columnCartons.MappingName = "Cartons";
            columnCartons.HeaderText = "箱数";
            columnCartons.Width = 30;

            columnItemDescription = new DataGridTextBoxColumn();
            columnItemDescription.MappingName = "ItemDescription";
            columnItemDescription.HeaderText = "描述";
            columnItemDescription.Width = 150;

            //
            columnHuId = new DataGridTextBoxColumn();
            columnHuId.MappingName = "HuId";
            columnHuId.HeaderText = "条码";
            columnHuId.Width = 160;

            columnStorageBinCode = new DataGridTextBoxColumn();
            columnStorageBinCode.MappingName = "StorageBinCode";
            columnStorageBinCode.HeaderText = "库格";
            columnStorageBinCode.Width = 0;
            columnStorageBinCode.NullText = string.Empty;

            columnLotNo = new DataGridTextBoxColumn();
            columnLotNo.MappingName = "LotNo";
            columnLotNo.HeaderText = "批号";
            columnLotNo.Width = 0;

            columnAdjustQty = new DataGridTextBoxColumn();
            columnAdjustQty.MappingName = "AdjustQty";
            columnAdjustQty.HeaderText = "数量";
            columnAdjustQty.Format = "0.##";
            columnAdjustQty.Width = 0;
        }

        public virtual void InitialAll()
        {
            this.dgList.Visible = true;
            this.resolver.Transformers = null;
            this.resolver.BinCode = null;
            this.resolver.Code = null;
            this.resolver.CodePrefix = null;
            this.resolver.Result = null;
            this.resolver.LocationCode = null;
            this.tbBarCode.Text = string.Empty;
            this.DataBind();
            this.tbQty.Visible = false;
            this.isKeyUp = false;
            this.tbBarCode.Focus();
        }

        protected virtual void tbBarCode_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyData & Keys.KeyCode) == Keys.Enter)
                {
                    this.BarCodeScan();
                }
                else if (((e.KeyData & Keys.KeyCode) == Keys.Escape))
                {
                    if (!Utility.IsHasTransformerDetail(this.resolver))
                    {
                        return;
                    }
                    if (this.enableCache)
                    {
                        Utility.CancelOperation(this.resolver);
                    }
                    else
                    {
                        this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_CANCEL;
                        this.resolver = TheSmartDeviceMgr.ScanBarcode(this.resolver);
                    }
                    this.DataBind();
                }
                else if ((e.KeyData & Keys.KeyCode) == Keys.F1)
                {
                    string message = HelpMessage();
                    Utility.ShowMessageBox(message);
                }

                this.tbBarCode.Text = this.tbBarCode.Text.Trim();
                this.tbBarCode.Select(this.tbBarCode.TextLength, 0);
                if (!this.timerCtrl.Enabled)
                {
                    //this.timerCtrl.Enabled = true;
                    this.timerCtrl.Interval = 2000;
                }
            }
            catch (SoapException ex)
            {
                this.btnHidden.Focus();
                this.tbBarCode.Text = string.Empty;
                Utility.ShowMessageBox(ex.Message);
            }
            catch (Exception ex)
            {
                this.InitialAll();
                this.btnHidden.Focus();
                Utility.ShowMessageBox(ex.Message);
            }
        }

        private void tbQty_KeyUp(object sender, KeyEventArgs e)
        {
            int rowCount = this.dgList.VisibleRowCount;
            int columnCount = this.dgList.VisibleColumnCount;
            if ((e.KeyData & Keys.KeyCode) == Keys.Enter && this.dgList.DataSource == this.resolver.Transformers)
            {
                InUpdateMode(columnCount);
                int rowIndex = this.dgList.CurrentCell.RowNumber;
                if (rowCount > rowIndex + 1)
                {
                    this.dgList.UnSelect(rowIndex);
                    this.dgList.Select(rowIndex + 1);
                    this.dgList.CurrentRowIndex = rowIndex + 1;
                    InEditMode(columnCount);
                }
                else if (rowCount == rowIndex + 1)
                {
                    this.btnOrder.Focus();
                }
            }
            else if ((e.KeyData & Keys.KeyCode) == Keys.Enter && this.dgList.DataSource != this.resolver.Transformers)
            {
                InUpdateMode(columnCount);
                this.tbBarCode.Focus();
                Utility.SumCurrentQty(this.resolver.Transformers); //更新汇总数量
            }
        }

        private void tbQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.ValidatorDecimal(sender, e);
        }

        private void tbQty_LostFocus(object sender, EventArgs e)
        {
            int columnCount = this.dgList.VisibleColumnCount;
            InUpdateMode(columnCount);
        }

        protected virtual void dgList_Click(object sender, EventArgs e)
        {
            int rowCount = this.dgList.VisibleRowCount;
            int columnCount = this.dgList.VisibleColumnCount;
            if (rowCount > 0)
            {
                if (this.dgList.DataSource == this.resolver.Transformers
                    && !(this.resolver.IsScanHu /*|| this.resolver.IsDetailContainHu*/))
                {
                    this.InEditMode(columnCount);
                }
            }
        }

        private void btnOrder_KeyUp(object sender, KeyEventArgs e)
        {
            if (((e.KeyData & Keys.KeyCode) == Keys.Enter))
            {
                this.isKeyUp = true;
                this.OrderConfirm();
            }
            else if (((e.KeyData & Keys.KeyCode) == Keys.Escape))
            {
                if (this.ModuleSelectionEvent != null)
                {
                    this.ModuleSelectionEvent(BusinessConstants.TRANSFORMER_MODULE_TYPE_SELECTION);
                }
            }
            else
            {
                this.UCBase_KeyUp(sender, e);
            }
        }

        protected virtual void BarCodeScan()
        {
            this.tbBarCode.Text = this.tbBarCode.Text.Trim();

            #region 当输入框为空时,按回车焦点跳转
            if (Utility.IsHasTransformerDetail(this.resolver) && this.tbBarCode.Text == string.Empty)
            {
                this.gvListDataBind();
                //if (this.resolver.IsScanHu /*|| this.resolver.IsDetailContainHu*/)
                //{
                this.btnOrder.Focus();
                //}
                //else
                //{
                //    this.dgList.Focus();
                //    this.dgList.Select(0);
                //    dgList_Click(null, null);
                //}
                return;
            }
            if (this.tbBarCode.Text == string.Empty)
            {
                return;
            }
            #endregion
            else
            {
                this.resolver.Input = this.tbBarCode.Text;
                if (this.enableCache)
                {
                    this.originalResolver = Utility.ProcessOriginalResolver(this.resolver, this.originalResolver);
                    this.originalResolver = TheSmartDeviceMgr.ScanBarcode(this.originalResolver);
                    this.resolver = Utility.MergeResolver(this.resolver, this.originalResolver);
                }
                else
                {
                    this.resolver = TheSmartDeviceMgr.ScanBarcode(this.resolver);
                }
                this.DataBind();
            }
        }

        protected virtual void gvListDataBind()
        {
            if (this.resolver.Transformers == null)
            {
                this.resolver.Transformers = new Transformer[] { };
            }
            this.dgList.DataSource = this.resolver.Transformers;
            ts = new DataGridTableStyle();
            ts.MappingName = this.resolver.Transformers.GetType().Name;
            if (this.resolver != null && !(this.resolver.IsScanHu /*|| this.resolver.IsDetailContainHu*/))
            {
                ts.GridColumnStyles.Add(columnItemCode);
                ts.GridColumnStyles.Add(columnUomCode);
                ts.GridColumnStyles.Add(columnCurrentQty);
                ts.GridColumnStyles.Add(columnItemDescription);
            }
            else
            {
                ts.GridColumnStyles.Add(columnItemCode);
                //ts.GridColumnStyles.Add(columnUomCode);
                ts.GridColumnStyles.Add(columnLotNo);
                ts.GridColumnStyles.Add(columnStorageBinCode);
                ts.GridColumnStyles.Add(columnAdjustQty);
                ts.GridColumnStyles.Add(columnCurrentQty);
                ts.GridColumnStyles.Add(columnCartons);
                ts.GridColumnStyles.Add(columnItemDescription);
            }
            this.dgList.TableStyles.Clear();
            this.dgList.TableStyles.Add(ts);

            this.ResumeLayout();
        }

        protected virtual void gvHuListDataBind()
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
            }
            ts = new DataGridTableStyle();
            ts.MappingName = transformerDetailList.GetType().Name;

            ts.GridColumnStyles.Add(columnHuId);
            //ts.GridColumnStyles.Add(columnUomCode);
            ts.GridColumnStyles.Add(columnStorageBinCode);
            ts.GridColumnStyles.Add(columnCurrentQty);
            ts.GridColumnStyles.Add(columnItemDescription);

            this.dgList.TableStyles.Clear();
            this.dgList.TableStyles.Add(ts);

            this.ResumeLayout();
            this.dgList.DataSource = transformerDetailList;
            this.tbBarCode.Text = string.Empty;
        }

        protected virtual void DataBind()
        {
            if (this.resolver.Command == BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL)
            {
                this.gvHuListDataBind();
            }
            else if (this.resolver.Command == BusinessConstants.CS_BIND_VALUE_TRANSFORMER)
            {
                this.gvListDataBind();
                this.tbBarCode.Text = string.Empty;
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
                if (Utility.IsHasTransformerDetail(this.resolver))
                {
                    this.isKeyUp = false;
                    this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_OK;
                    this.resolver = TheSmartDeviceMgr.ScanBarcode(this.resolver);
                    this.lblMessage.Text = this.resolver.Result;
                    this.lblMessage.Visible = true;
                    this.InitialAll();
                }
                else
                {
                    this.btnHidden.Focus();
                    Utility.ShowMessageBox("明细不能为空");
                }
            }
            catch (SoapException ex)
            {
                this.btnHidden.Focus();
                this.lblMessage.Text = Utility.FormatExMessage(ex.Message);
                this.lblMessage.Visible = true;
                Utility.ShowMessageBox(ex.Message);
            }
            catch (Exception ex)
            {
                this.InitialAll();
                this.btnHidden.Focus();
                Utility.ShowMessageBox(ex.Message);
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (!this.isKeyUp)
            {
                this.OrderConfirm();
            }
            this.isKeyUp = false;
        }

        protected void InEditMode(int columnCount)
        {
            this.editCell = this.dgList.CurrentCell;
            this.tbQty.Text = Convert.ToDecimal(this.dgList[this.editCell.RowNumber, columnCount - 2].ToString()).ToString("0.########");
            Rectangle cellPos = this.dgList.GetCellBounds(this.editCell.RowNumber, columnCount - 2);
            this.tbQty.Left = cellPos.Left - 1;
            this.tbQty.Top = cellPos.Top + dgList.Top - 1;
            this.tbQty.Width = cellPos.Width + 2;
            this.tbQty.Height = cellPos.Height + 2;
            this.tbQty.Visible = true;
            this.inEditMode = true;
            this.tbQty.Focus();
            this.tbQty.SelectAll();
        }

        private void InUpdateMode(int columnCount)
        {
            this.tbQty.Text = this.tbQty.Text == string.Empty ? "0" : this.tbQty.Text;
            this.inUpdateMode = true;
            this.dgList.Visible = false;
            DataGridCell currentCell = this.dgList.CurrentCell;
            this.dgList[this.editCell.RowNumber, columnCount - 2] = Convert.ToDecimal(this.tbQty.Text);
            this.dgList.CurrentCell = currentCell;
            this.dgList.Visible = true;
            this.inUpdateMode = false;
            this.tbQty.Visible = false;
            this.inEditMode = false;
        }

        //修正关闭警告窗口后的焦点
        private void btnHidden_KeyUp(object sender, KeyEventArgs e)
        {
            if (((e.KeyData & Keys.KeyCode) == Keys.Enter))
            {
                this.tbBarCode.Focus();
            }
        }

        protected virtual void tbBarCode_TextChanged(object sender, EventArgs e)
        {
            if (this.tbBarCode.Text != string.Empty)
            {
                this.lblMessage.Text = string.Empty;
                this.lblMessage.Visible = false;
            }
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

        private void TimerCtrl_Tick(object sender, EventArgs e)
        {
            this.tbBarCode.Text = string.Empty;
            this.timerCtrl.Enabled = false;
        }

        protected virtual string HelpMessage()
        {
            return null;
        }

        private void pbQuestion_Click(object sender, EventArgs e)
        {
            string message = HelpMessage();
            Utility.ShowMessageBox(message);
        }
    }
}
