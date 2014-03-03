using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Control;

public partial class MasterData_Client_New : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;
    public event EventHandler NewEvent;

    private Client client;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void FV_Client_OnDataBinding(object sender, EventArgs e)
    {

    }

    public void PageCleanup()
    {
        ((CheckBox)(this.FV_Client.FindControl("tbIsActive"))).Checked = true;
        ((TextBox)(this.FV_Client.FindControl("tbClientId"))).Text = string.Empty;
        ((TextBox)(this.FV_Client.FindControl("tbDescription"))).Text = string.Empty;
    }

    protected void ODS_Client_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        client = (Client)e.InputParameters[0];
        if (client != null)
        {
            client.ClientId = client.ClientId.Trim();
            client.Description = client.Description.Trim();
        }
    }

    protected void ODS_Client_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(client.ClientId, e);
            ShowSuccessMessage("MasterData.Client.AddClient.Successfully", client.ClientId);
        }
    }

    protected void checkClient(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvClientId":
                if (TheClientMgr.LoadClient(args.Value) != null)
                {
                    ShowErrorMessage("MasterData.Client.CodeExist", args.Value);
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }
}
