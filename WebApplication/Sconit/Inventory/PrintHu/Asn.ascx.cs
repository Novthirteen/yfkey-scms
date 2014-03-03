using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;

public partial class Inventory_PrintHu_Asn : ModuleBase
{
    private string CurrentIpNo
    {
        get
        {
            return (string)ViewState["CurrentIpNo"];
        }
        set
        {
            ViewState["CurrentIpNo"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void tbAsn_TextChanged(Object sender, EventArgs e)
    {
        try
        {
            if (this.CurrentIpNo == null || this.CurrentIpNo != this.tbAsn.Text.Trim())
            {
                InProcessLocation currentInProcessLocation = TheInProcessLocationMgr.LoadInProcessLocation(this.tbAsn.Text.Trim(), this.CurrentUser, true);

                this.CurrentIpNo = currentInProcessLocation.IpNo;

                this.ucList.InitPageParameter(currentInProcessLocation);
                this.ucList.Visible = true;
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }
}
