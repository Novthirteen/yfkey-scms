using Sconit_SMARTDEVICE.SconitWS;

namespace Sconit_SD
{
    public partial class UCShip : UCBase
    {
        public UCShip(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "发货";
            columnCurrentQty.HeaderText = "实发";
        }

        protected override void BarCodeScan()
        {
            if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP
                && this.tbBarCode.Text.StartsWith(BusinessConstants.CODE_PREFIX_ORDER))
            {
                this.lblMessage.Text = "只能使用拣货单发货";
                this.lblMessage.Visible = true;
                this.tbBarCode.Text = string.Empty;
                return;
            }
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER
                && this.tbBarCode.Text.StartsWith(BusinessConstants.CODE_PREFIX_PICKLIST))
            {
                this.lblMessage.Text = "只能使用订单发货";
                this.lblMessage.Visible = true;
                this.tbBarCode.Text = string.Empty;
                return;
            }
            base.BarCodeScan();
        }
    }
}
