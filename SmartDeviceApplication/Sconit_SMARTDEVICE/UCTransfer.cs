using Sconit_SMARTDEVICE.SconitWS;
using System.Collections.Generic;

namespace Sconit_SD
{
    public partial class UCTransfer : UCBase
    {
        public UCTransfer(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "移库";
        }

        protected override void gvListDataBind()
        {
            base.gvListDataBind();

            if (this.resolver != null && this.resolver.IsScanHu)
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
