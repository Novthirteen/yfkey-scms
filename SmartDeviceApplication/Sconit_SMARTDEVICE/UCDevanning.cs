using Sconit_SMARTDEVICE.SconitWS;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sconit_SD
{
    public partial class UCDevanning : UCBase
    {
        public UCDevanning(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "拆箱";
            this.lblBarCode.Text = "大箱";
        }

        protected override void DataBind()
        {
            this.gvHuListDataBind();
            this.lblResult.Text = this.resolver.Result;
        }

        public override void InitialAll()
        {
            base.InitialAll();
            this.resolver.IOType = BusinessConstants.IO_TYPE_IN;
            this.resolver.Transformers = new Transformer[2];
            //this.resolver.Transformers[0] = new Transformer();
            //this.resolver.Transformers[1] = new Transformer();
            this.tbBarCode.Focus();
        }

        protected override void BarCodeScan()
        {
            #region 重写当输入框为空时,按回车焦点跳转
            this.tbBarCode.Text = this.tbBarCode.Text.Trim();
            if (isHasOutDetail
                && this.tbBarCode.Text.Trim() == string.Empty
                && this.resolver.IOType == BusinessConstants.IO_TYPE_OUT)
            {
                this.btnOrder.Focus();
                return;
            }
            if (this.tbBarCode.Text == string.Empty)
            {
                return;
            }
            #endregion

            base.BarCodeScan();
        }

        protected override void gvHuListDataBind()
        {
            base.gvHuListDataBind();
            TransformerDetail[] transformerArray = new TransformerDetail[] { };
            this.lblBarCode.Text = "大箱";
            if (this.resolver.Transformers != null
                && this.resolver.Transformers.Length > 0
                && this.resolver.Transformers[0].TransformerDetails != null)
            {
                transformerArray = this.resolver.Transformers[0].TransformerDetails;
            }
            if (this.resolver.IOType == BusinessConstants.IO_TYPE_OUT)
            {
                if (isHasOutDetail)
                {
                    transformerArray = this.resolver.Transformers[1].TransformerDetails;
                }
                this.lblBarCode.Text = "小箱";
            }
            ts.MappingName = transformerArray.GetType().Name;
            this.dgList.DataSource = transformerArray;
            //ts = new DataGridTableStyle();
            //ts.GridColumnStyles.Add(columnHuId);
            //ts.GridColumnStyles.Add(columnStorageBinCode);
            //ts.GridColumnStyles.Add(columnCurrentQty);
            //ts.GridColumnStyles.Add(columnItemDescription);

            //this.dgList.TableStyles.Clear();
            //this.dgList.TableStyles.Add(ts);

            //this.ResumeLayout();
            //this.tbBarCode.Text = string.Empty;
        }


        private bool isHasOutDetail
        {
            get
            {
                if (this.resolver.Transformers != null
                 && this.resolver.Transformers.Length == 2
                 && this.resolver.Transformers[1] != null
                 && this.resolver.Transformers[1].TransformerDetails != null
                 && this.resolver.Transformers[1].TransformerDetails.Length > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
