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
    public partial class UCReceipt : UCBase
    {
        public UCReceipt(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnConfirm.Text = "收货";
            this.gvList.Columns["Qty"].HeaderText = "订单数";
            this.gvList.Columns["CurrentQty"].HeaderText = "实收数";
            this.gvList.Columns["LocationFromCode"].Visible = false;
        }

        protected override void gvHuList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = this.gvHuList.CurrentCell.RowIndex;
            this.gvHuList.CurrentCell = this.gvHuList.Rows[index].Cells["HuQty"];
            this.gvHuList.BeginEdit(true);
        }

        protected override void BarCodeScan()
        {
            base.BarCodeScan();
            if (resolver.Code == BusinessConstants.CODE_PREFIX_PICKLIST)
            {
                this.btnConfirm.Focus();
                this.tbBarCode.Text = resolver.Code;
            }
        }
    }
}