using System.ComponentModel;
using Sconit_CS.SconitWS;
using System.ServiceModel;
using System.Windows.Forms;
using System;

namespace Sconit_CS
{
    public partial class UCShip : UCBase
    {
        public UCShip(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.gvHuList.Columns["StorageBinCode"].Visible = false;
            this.btnConfirm.Text = "发货";
            this.gvList.Columns["Qty"].HeaderText = "待发数";
            this.gvList.Columns["CurrentQty"].HeaderText = "实发数";
            if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP)
            {
                this.btnConfirm.Text = "拣货发货";
            }
            else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER)
            {
                this.btnConfirm.Text = "订单发货";
            }
        }

        protected override void BarCodeScan() 
        {
            if (resolver.ModuleType==BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP
                &&this.tbBarCode.Text.StartsWith(BusinessConstants.CODE_PREFIX_ORDER))
            {
                this.lblMessage.Text = "只能使用拣货单发货";
                this.tbBarCode.Text = string.Empty;
                return;
            }
            else if(resolver.ModuleType==BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER
                &&this.tbBarCode.Text.StartsWith(BusinessConstants.CODE_PREFIX_PICKLIST))
            {
                this.lblMessage.Text = "只能使用订单发货";
                this.tbBarCode.Text = string.Empty;
                return;
            }
            base.BarCodeScan();
        }

        protected override void OrderConfirm()
        {
            try
            {
                this.resolver.Input = BusinessConstants.BARCODE_SPECIAL_MARK + BusinessConstants.BARCODE_HEAD_OK;
                this.resolver.PrintUrl = string.Empty;
                this.resolver = TheClientMgr.ScanBarcode(this.resolver);
                if (this.resolver.PrintUrl != null && this.resolver.PrintUrl != string.Empty && this.resolver.NeedPrintAsn)
                {
                    Utility.PrintOrder(this.resolver.PrintUrl,this, null);
                }
                this.lblMessage.Text = this.resolver.Result;
                InitialAll();
            }
            catch (FaultException ex)
            {
                this.lblMessage.Text = Utility.FormatExMessage(ex.Message);
                MessageBox.Show(this, Utility.FormatExMessage(ex.Message));
                this.tbBarCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "程序内部错误,请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InitialAll();
            }
        }
    }
}
