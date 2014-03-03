using Sconit_SMARTDEVICE.SconitWS;

namespace Sconit_SD
{
    public partial class UCInspection : UCBase
    {
        public UCInspection(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();
            this.btnOrder.Text = "报验";
            this.resolver.IsScanHu = true;
        }
    }
}
