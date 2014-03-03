using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows.Forms;
using Sconit_CS.SconitWS;
using System.Collections.Generic;

namespace Sconit_CS
{
    public partial class UCFlushBack : UCBase
    {
        public UCFlushBack(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnConfirm.Text = "投料回冲";
            this.gvList.Columns["CurrentQty"].HeaderText = "剩余数量";
            this.gvList.Columns["OrderedQty"].HeaderText = "累计回冲";
            this.gvList.Columns["Qty"].HeaderText = "总投数";
            this.gvList.Columns["LocationCode"].Visible = false;
            this.gvList.Columns["OrderNo"].Visible = false;
            this.gvList.Columns["LocationFromCode"].Visible = false;
            this.gvList.Columns["LocationToCode"].Visible = false;
            this.gvList.Columns["UomCode"].Visible = false;
            this.gvList.Columns["BinCode"].Visible = false;
            this.gvList.Columns["LotNumber"].Visible = false;
            this.gvList.Columns["Cartons"].Visible = false;
            this.gvList.Columns["Qty"].Visible = true;
            this.gvList.Columns["OrderedQty"].Visible = true;
            this.gvList.Columns["CheckColumn"].Visible = true;
            this.resolver.IsScanHu = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.tbBarCode.Focused)
            {
                if (keyData == Keys.Escape)
                {
                    this.InitialAll();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void BarCodeScan()
        {
            base.BarCodeScan();
            this.gvList.Focus();
            //this.gvList.BeginEdit(true);
        }

        protected override void DataBind()
        {
            base.DataBind();
            this.gvList.Columns["Cartons"].Visible = false;
            if (this.gvList.CurrentCell != null)
            {
                int index = this.gvList.CurrentCell.RowIndex;
                this.gvList.Columns["CurrentQty"].Visible = true;
                this.gvList.CurrentCell = this.gvList.Rows[index].Cells["CurrentQty"];
                this.gvList.Columns["CurrentQty"].ReadOnly = false;
                this.gvList.ReadOnly = false;
                this.gvList.BeginEdit(true);
            }
        }

        protected override void gvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = this.gvList.CurrentCell.RowIndex;
            int currentQtyColumnIndex = this.gvList["CurrentQty", 0].ColumnIndex;

            DataGridViewCell CurrentCell = ((DataGridView)(sender)).CurrentCell;
            decimal Qty = Convert.ToDecimal(this.gvList["Qty", rowIndex].Value);
            decimal CurrentQty = Convert.ToDecimal(this.gvList["CurrentQty", rowIndex].Value);
            if (CurrentQty > Qty)
            {
                //CurrentCell.Value = 0M;
                MessageBox.Show(this, "回冲数不能大于总数");
                this.gvList.CurrentCell = CurrentCell;
                //this.gvHuList.BeginEdit(true);
                if (rowIndex != this.gvList.Rows.Count - 1)
                {
                    SendKeys.Send("{Up}");
                }
            }
        }

        protected override void OrderConfirm()
        {
            int i = 0;
            List<Transformer> transformers = new List<Transformer>();
            foreach (DataGridViewRow row in this.gvList.Rows)
            {
                if (Convert.ToBoolean(row.Cells["CheckColumn"].Value))
                {
                    transformers.Add(this.resolver.Transformers[i]);
                }
                i++;
            }
            this.resolver.Transformers = transformers.ToArray();
            base.OrderConfirm();
        }
    }
}
