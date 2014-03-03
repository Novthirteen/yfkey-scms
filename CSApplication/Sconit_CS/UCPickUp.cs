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
    public partial class UCPickUp : UCBase
    {
        public UCPickUp(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnConfirm.Text = "下架";
            this.gvList.Columns["LocationFromCode"].Visible = false;
            this.gvList.Columns["LocationToCode"].Visible = false;
            this.gvList.Columns["OrderNo"].Visible = false;
            this.gvList.Columns["Qty"].Visible = false;
            this.gvList.Columns["BinCode"].Visible = true;
            this.gvList.Columns["LocationCode"].Visible = true;
            this.resolver.IsScanHu = true;
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
