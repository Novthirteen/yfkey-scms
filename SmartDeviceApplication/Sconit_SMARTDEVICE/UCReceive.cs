using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Sconit_SMARTDEVICE.SconitWS;

namespace Sconit_SD
{
    public partial class UCReceive : UCBase
    {
        public UCReceive(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            columnStorageBinCode.Width = 30;
            columnCurrentQty.HeaderText = "实收";
            if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)
            {
                this.btnOrder.Text = "收货";
            }
            else if (moduleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPCONFIRM)
            {
                this.btnOrder.Text = "确认";
            }
        }

        protected override void DataBind()
        {
            this.gvListDataBind();
            this.tbBarCode.Text = string.Empty;
            this.lblResult.Text = this.resolver.Result;
        }


        protected override void gvListDataBind()
        {
            List<Transformer> transformerList = new List<Transformer>();
            if (this.resolver.Transformers != null)
            {
                foreach (Transformer transformer in this.resolver.Transformers)
                {
                    if (transformer != null)
                    {
                        Transformer newTransformer = new Transformer();
                        newTransformer = transformer;
                        newTransformer.AdjustQty = newTransformer.Qty - newTransformer.CurrentQty;
                        if (newTransformer.AdjustQty > 0)
                        {
                            transformerList.Add(newTransformer);
                        }
                    }
                }
            }
            base.gvListDataBind();
            this.dgList.DataSource = transformerList;
            ts.MappingName = transformerList.GetType().Name;
        }

    }
}
