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
    public partial class UCPickList : UCBase
    {
        public UCPickList(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();

            this.btnConfirm.Text = "拣货";
            this.gvList.Columns["Qty"].HeaderText = "待拣数";
            this.gvList.Columns["CurrentQty"].HeaderText = "拣货数";
            this.gvList.Columns["LocationFromCode"].HeaderText = "库位";
            this.gvList.Columns["UomCode"].Visible = false;
            this.gvList.Columns["BinCode"].Visible = true;
            this.gvList.Columns["LocationToCode"].Visible = false;
            this.gvList.Columns["Cartons"].Visible = true;
            this.gvList.Columns["LotNumber"].Visible = true;
            this.gvList.Columns["OrderNo"].Visible = false;
        }

        protected override void DataBind()
        {
            this.gvListDataBind();
            this.tbBarCode.Text = string.Empty;
            this.lblResult.Text = this.resolver.Result;
            if (this.resolver.IsScanHu)
            {
                this.gvList.Columns["CurrentRejectQty"].ReadOnly = true;
                this.gvList.Columns["CurrentQty"].ReadOnly = true;
            }
        }

        protected override void gvListDataBind()
        {
            this.gvHuList.Visible = false;
            this.gvList.Visible = true;
            List<Transformer> transformerList = new List<Transformer>();
            if (this.resolver.Transformers != null)
            {
                foreach (Transformer transformer in this.resolver.Transformers)
                {
                    if (transformer.Qty != 0)
                    {
                        transformerList.Add(transformer);
                    }
                }
            }
            this.gvList.DataSource = new BindingList<Transformer>(transformerList);
            this.gvList = Utility.RenderDataGridViewBackColor(this.gvList);
        }

    }
}
