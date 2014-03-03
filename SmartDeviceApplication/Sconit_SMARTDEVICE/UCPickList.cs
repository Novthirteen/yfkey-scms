using Sconit_SMARTDEVICE.SconitWS;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System;

namespace Sconit_SD
{
    public partial class UCPickList : UCBase
    {
        public UCPickList(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "拣货";
            columnStorageBinCode.Width = 30;
            columnLotNo.Width = 30;
            columnAdjustQty.Width = 40;
            columnCurrentQty.Width = 0;
            columnUomCode.Width = 0;
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
