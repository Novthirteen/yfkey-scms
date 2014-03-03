using System;
using Sconit_SMARTDEVICE.SconitWS;

namespace Sconit_SD
{
    public partial class UCInspect : UCBase
    {
        public UCInspect(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "检验";
            columnCurrentQty.HeaderText = "合格数";
        }

        protected override void BarCodeScan()
        {
            //不汇总 不考虑非HU检验
            this.tbBarCode.Text = this.tbBarCode.Text.Trim();

            #region 当输入框为空时,按回车焦点跳转
            if (Utility.IsHasTransformerDetail(this.resolver) && this.tbBarCode.Text.Trim() == string.Empty)
            {
                if (this.resolver.IsScanHu)
                {
                    this.btnOrder.Focus();
                    this.gvHuListDataBind();
                }
                return;
            }
            #endregion

            base.BarCodeScan();
        }

    }
}
