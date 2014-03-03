using System.Windows.Forms;
using Sconit_SMARTDEVICE.SconitWS;
using System.Collections.Generic;

namespace Sconit_SD
{
    public partial class UCOnline : UCBase
    {
        private List<Transformer> transformerList;

        public UCOnline(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            transformerList = new List<Transformer>();
        }

        protected override void BarCodeScan()
        {
            base.BarCodeScan();
            this.lblMessage.Text = "拣货单" + this.resolver.Code + "上线成功!";
            this.lblMessage.Visible = true;
            this.lblResult.Text = string.Empty;
            Transformer transformer = new Transformer();
            transformer.OrderNo = this.resolver.Code;          //当前上线的Picklist
            transformer.ItemDescription = this.resolver.Result;//替代上线时间(当前上线的Picklist的时间)
            //倒序
            List<Transformer> transformers = new List<Transformer>();
            transformers.Add(transformer);
            transformers.AddRange(this.transformerList);
            this.transformerList = transformers;
            this.gvListDataBind();
        }

        protected override void gvListDataBind()
        {
            this.dgList.DataSource = this.transformerList;
            ts = new DataGridTableStyle();
            ts.MappingName = this.transformerList.GetType().Name;

            columnItemCode.MappingName = "OrderNo";
            columnItemCode.HeaderText = "拣货单号";
            columnItemCode.Width = 100;
            ts.GridColumnStyles.Add(columnItemCode);

            columnItemDescription.MappingName = "ItemDescription";
            columnItemDescription.HeaderText = "上线时间";
            columnItemDescription.Width = 130;
            ts.GridColumnStyles.Add(columnItemDescription);

            this.dgList.TableStyles.Clear();
            this.dgList.TableStyles.Add(ts);

            this.ResumeLayout();
        }
    }
}
