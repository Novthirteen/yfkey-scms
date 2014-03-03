using System.Windows.Forms;
using Sconit_SMARTDEVICE.SconitWS;
using System.Collections.Generic;

namespace Sconit_SD
{
    public partial class UCReuse : UCBase
    {
        public UCReuse(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "回用";
        }

        protected override void BarCodeScan()
        {
            this.resolver.IsScanHu = true;
            base.BarCodeScan();
        }

        //protected override void gvListDataBind()
        //{
        //    this.resolver.IsScanHu = true;
        //    base.gvListDataBind();

        //    if (this.resolver != null)
        //    {
        //        List<Transformer> transformerList = new List<Transformer>();
        //        foreach (Transformer transformer in this.resolver.Transformers)
        //        {
        //            if (transformer.CurrentQty != 0)
        //            {
        //                transformerList.Add(transformer);
        //            }
        //        }
        //        this.dgList.DataSource = transformerList;
        //        ts.MappingName = transformerList.GetType().Name;
        //    }
        //}
    }

}
