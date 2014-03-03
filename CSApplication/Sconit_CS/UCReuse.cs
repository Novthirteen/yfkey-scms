using Sconit_CS.SconitWS;

namespace Sconit_CS
{
    public partial class UCReuse : UCBase
    {
        public UCReuse(User user, string moduleType)
            : base(user, moduleType)
        {
            InitializeComponent();

            this.btnConfirm.Text = "材料回用";
            this.gvList.Columns["Qty"].Visible = false;
            this.gvList.Columns["CurrentQty"].HeaderText = "数量";
            this.gvList.Columns["LocationFromCode"].Visible = false; 
            this.gvList.Columns["UomCode"].Visible = false;
            this.gvList.Columns["BinCode"].Visible = false;
            this.gvList.Columns["LocationToCode"].HeaderText = "目的库位";
            this.gvList.Columns["LotNumber"].Visible = false;
            this.gvList.Columns["OrderNo"].Visible = false;
            this.gvHuList.Columns["StorageBinCode"].Visible = false;
        }

        protected override void DataBind()
        {
            this.resolver.IsScanHu = true;
            base.DataBind();
        }
    }
}
