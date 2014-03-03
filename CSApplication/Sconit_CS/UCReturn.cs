using System.Collections.Generic;
using System.ComponentModel;
using Sconit_CS.SconitWS;

namespace Sconit_CS
{
    public partial class UCReturn : UCBase
    {
        public UCReturn(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
            {
                this.btnConfirm.Text = "要货退货";
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN)
            {
                this.btnConfirm.Text = "发货退货";
            }

            this.gvList.Columns["Qty"].Visible = false;
            this.gvList.Columns["CurrentQty"].HeaderText = "退货数";
            this.gvList.Columns["OrderNo"].Visible = false;
            this.gvList.Columns["LocationFromCode"].HeaderText = "目的库位";
            this.gvList.Columns["LocationToCode"].HeaderText = "来源库位";

        }

        protected override void gvListDataBind()
        {
            base.gvListDataBind();

            List<Transformer> transformerList = new List<Transformer>();
            foreach (Transformer transformer in this.resolver.Transformers)
            {
                if (transformer.CurrentQty != 0)
                {
                    transformerList.Add(transformer);
                }
            }
            this.gvList.DataSource = new BindingList<Transformer>(transformerList);
        }
    }
}
