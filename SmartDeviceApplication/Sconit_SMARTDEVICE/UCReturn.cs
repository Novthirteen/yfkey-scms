using Sconit_SMARTDEVICE.SconitWS;
using System.Collections.Generic;

namespace Sconit_SD
{
    public partial class UCReturn : UCBase
    {
        public UCReturn(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "退货";
        }

        protected override void gvListDataBind()
        {
            base.gvListDataBind();
            if (this.resolver != null && this.resolver.IsScanHu)
            {
                if (this.resolver.Transformers == null)
                {
                    List<Transformer> transformerList = new List<Transformer>();
                    foreach (Transformer transformer in this.resolver.Transformers)
                    {
                        if (transformer.CurrentQty != 0)
                        {
                            transformerList.Add(transformer);
                        }
                    }
                    this.dgList.DataSource = transformerList;
                    ts.MappingName = transformerList.GetType().Name;
                }
            }
        }
    }
}
