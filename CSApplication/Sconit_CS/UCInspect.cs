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
    public partial class UCInspect : UCBase
    {
        public UCInspect(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.gvHuList.Columns["StorageBinCode"].Visible = false;
            this.gvList.Columns["Qty"].HeaderText = "待验数";
            this.gvList.Columns["CurrentQty"].Visible = false;
            this.gvList.Columns["OrderNo"].Visible = false;
            this.gvList.Columns["CheckColumn"].Visible = true;
            this.btnQualified.Visible = true;
            this.btnConfirm.Text = "不合格";
            this.btnQualified.Text = "合格";
        }

        protected override void BarCodeScan()
        {
            this.tbBarCode.Text = this.tbBarCode.Text.Trim();
            #region 当输入框为空时,按回车焦点跳转

            if (this.tbBarCode.Text.Trim() == string.Empty && Utility.IsHasTransformerDetail(this.resolver))
            {
                if (this.gvHuList.Visible)
                {
                    this.btnConfirm.Focus();
                }
                return;
            }
            #endregion

            base.BarCodeScan();
        }

        /// <summary>
        /// 不合格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.gvHuList.Visible)
            {
                if (resolver.Transformers != null)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.TransformerDetails != null)
                        {
                            foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                            {
                                transformerDetail.CurrentRejectQty = transformerDetail.CurrentQty;
                                transformerDetail.CurrentQty = 0;
                            }
                        }
                    }
                }
            }
            else
            {
                this.CheckResolver(false);
            }
            base.btnConfirm_Click(sender, e);
        }

        /// <summary>
        /// 合格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnQualified_Click(object sender, EventArgs e)
        {
            this.CheckResolver(true);
            this.OrderConfirm();
        }
                
        /// <summary>
        /// 用于检验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckResolver(bool isQulified)
        {
            if (this.resolver.IsScanHu)
            {
                foreach (DataGridViewRow row in this.gvList.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["CheckColumn"].Value))
                    {
                        string itemCode = row.Cells["ItemCode"].Value.ToString();
                        string uomCode = row.Cells["UomCode"].Value.ToString();
                        decimal unitCount = Convert.ToDecimal(row.Cells["UnitCount"].Value.ToString());

                        if (this.resolver.Transformers != null)
                        {
                            foreach (Transformer transformer in this.resolver.Transformers)
                            {
                                if (transformer.TransformerDetails != null)
                                {
                                    foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                                    {
                                        if (transformerDetail.ItemCode == itemCode &&
                                            transformerDetail.UomCode == uomCode &&
                                           transformerDetail.UnitCount == unitCount)
                                        {
                                            if (isQulified)
                                            {
                                                transformerDetail.CurrentQty = transformerDetail.Qty;
                                            }
                                            else
                                            {
                                                transformerDetail.CurrentRejectQty = transformerDetail.Qty;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //protected override void gvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (this.gvList.Columns["CheckColumn"].Visible && this.resolver.IsScanHu)
        //    {
        //        int columnIndex = e.ColumnIndex;
        //        int rowIndex = e.RowIndex;
        //        if (columnIndex == 0 && e.RowIndex != -1)
        //        {
        //            string itemCode = this.gvList["ItemCode", rowIndex].Value.ToString();
        //            string uomCode = this.gvList["UomCode", rowIndex].Value.ToString();
        //            decimal unitCount = Convert.ToDecimal(this.gvList["UnitCount", rowIndex].Value.ToString());
        //            if (Convert.ToBoolean(this.gvList[0, rowIndex].Value))
        //            {
        //                if (this.resolver.Transformers != null)
        //                {
        //                    foreach (Transformer transformer in this.resolver.Transformers)
        //                    {
        //                        if (transformer.TransformerDetails != null)
        //                        {
        //                            foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
        //                            {
        //                                if (transformerDetail.ItemCode == itemCode &&
        //                                    transformerDetail.UomCode == uomCode &&
        //                                   transformerDetail.UnitCount == unitCount)
        //                                {
        //                                    transformerDetail.CurrentQty = transformerDetail.Qty;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        protected override void DataBind()
        {
            base.DataBind();
            if (this.resolver.IsScanHu)
            {
                this.gvList.Columns["CurrentRejectQty"].ReadOnly = true;
                this.gvList.Columns["CurrentQty"].ReadOnly = true;
            }
        }
        #region 可以修改Hu的数量
        //private void gvHuListDataBind()
        //{
        //    TransformerDetail[] transformerDetailArray = new TransformerDetail[] { };
        //    if (this.resolver.Transformers != null && this.resolver.Transformers.Length == 1
        //        && this.resolver.Transformers[0].TransformerDetails != null && this.resolver.Transformers[0].TransformerDetails.Length > 0)
        //    {
        //        transformerDetailArray = this.resolver.Transformers[0].TransformerDetails;
        //    }
        //    this.gvHuList.DataSource = new BindingList<TransformerDetail>(transformerDetailArray);
        //    //this.gvHuList.ClearSelection();
        //    int rowIndex = Utility.GetRowIndex(this.resolver, this.tbBarCode.Text);
        //    if (rowIndex > -1)
        //    {
        //        this.gvHuList.CurrentCell = this.gvHuList.Rows[rowIndex].Cells["CurrentQty"];
        //        this.gvHuList.BeginEdit(true);
        //        this.gvHuList.Columns["CurrentQty"].ReadOnly = false;
        //        this.gvHuList.Columns["CurrentRejectQty"].ReadOnly = false;
        //        this.gvHuList.Focus();
        //    }
        //    this.lblMessage.Text = this.resolver.Result;
        //    this.tbBarCode.Text = string.Empty;
        //    this.gvHuList = Utility.RenderDataGridViewBackColor(this.gvHuList);
        //    if (this.resolver.IsScanHu)
        //    {
        //        this.gvHuList.Columns["CurrentRejectQty"].Visible=false;
        //        this.gvHuList.Columns["HuId"].Visible = true;
        //    }
        //    else
        //    {
        //        this.gvHuList.Columns["CurrentRejectQty"].Visible = true;
        //        this.gvHuList.Columns["HuId"].Visible = false;
        //    }
        //}

        //private void gvHuList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    int rowIndex = this.gvHuList.CurrentCell.RowIndex;
        //    int currentQtyColumnIndex = this.gvHuList["CurrentQty", 0].ColumnIndex;

        //    DataGridViewTextBoxCell CurrentCell = (DataGridViewTextBoxCell)((DataGridView)(sender)).CurrentCell;
        //    decimal Qty = Convert.ToDecimal(this.gvHuList["Qty", rowIndex].Value);
        //    decimal CurrentQty = Convert.ToDecimal(this.gvHuList["CurrentQty", rowIndex].Value);
        //    decimal CurrentRejectQty = Convert.ToDecimal(this.gvHuList["CurrentRejectQty", rowIndex].Value);
        //    if (CurrentQty + CurrentRejectQty > Qty)
        //    {
        //        //CurrentCell.Value = 0M;
        //        MessageBox.Show(this, "检验总数不能大于待检数");
        //        this.gvHuList.CurrentCell = CurrentCell;
        //        //this.gvHuList.BeginEdit(true);
        //        if (rowIndex != this.gvHuList.Rows.Count - 1)
        //        {
        //            SendKeys.Send("{Up}");
        //        }
        //    }
        //    else
        //    {
        //        if (!this.resolver.IsScanHu && this.gvHuList.CurrentCell.ColumnIndex == currentQtyColumnIndex)
        //        {
        //            this.gvHuList.CurrentCell = this.gvHuList.Rows[rowIndex].Cells["CurrentRejectQty"];
        //            this.gvHuList.BeginEdit(true);
        //        }
        //        else
        //        {
        //            this.gvHuList.ClearSelection();  
        //            this.tbBarCode.Focus();
        //            this.gvHuList.EndEdit();
        //            this.gvHuList.Columns["CurrentQty"].ReadOnly = true;
        //            this.gvHuList.Columns["CurrentRejectQty"].ReadOnly = true;
        //            this.gvHuList["CurrentRejectQty", rowIndex].Value = Qty - CurrentQty;
        //        }
        //    }
        //}
        #endregion
    }
}
