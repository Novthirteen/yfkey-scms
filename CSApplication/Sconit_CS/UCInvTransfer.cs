using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Sconit_CS.SconitWS;
using System.ServiceModel;

namespace Sconit_CS
{
    public partial class UCInvTransfer : UCBase
    {
        public UCInvTransfer(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();

            this.btnConfirm.Text = "移库";

            this.gvList.Columns["Qty"].Visible = false;
            this.gvList.Columns["CurrentQty"].HeaderText = "数量";
            this.gvList.Columns["OrderNo"].Visible = false;
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


        protected override void OrderConfirm()
        {
            base.OrderConfirm();
            if (this.resolver.PrintUrl != null && this.resolver.PrintUrl != string.Empty && this.resolver.NeedPrintReceipt)
            {
                Utility.PrintOrder(this.resolver.PrintUrl, this,null);
            }
        }
    }
}
