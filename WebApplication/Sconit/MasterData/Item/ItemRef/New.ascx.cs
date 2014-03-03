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
using com.Sconit.Web;
using com.Sconit.Service.MasterData;

public partial class MasterData_Item_ItemRef_New : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;

    protected string ItemCode
    {
        get
        {
            return (string)ViewState["ItemCode"];
        }
        set
        {
            ViewState["ItemCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Controls_TextBox)(this.FV_ItemReference.FindControl("tbPartyCode"))).ServiceParameter = "string:" + this.CurrentUser.Code + ",string:#ddlPartyType";
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_ItemReference_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(this, null);
        }
    }

    protected void ODS_ItemReference_Inserting(object source, ObjectDataSourceMethodEventArgs e)
    {
        ItemReference itemReference = (ItemReference)e.InputParameters[0];
        string partyCode = ((Controls_TextBox)(this.FV_ItemReference.FindControl("tbPartyCode"))).Text;
        string referenceCode = ((TextBox)(this.FV_ItemReference.FindControl("tbReferenceCode"))).Text;
        if (ItemCode == null || ItemCode.Trim() == string.Empty)
        {
            ShowWarningMessage("MasterData.ItemReference.Required.ItemCode");
            e.Cancel = true;
            return;
        }

        if (itemReference.ReferenceCode == null || itemReference.ReferenceCode.Trim() == string.Empty)
        {
            ShowWarningMessage("MasterData.ItemReference.Required.ReferenceCode");
            e.Cancel = true;
            return;
        }
        itemReference.Item = TheItemMgr.LoadItem(ItemCode);
        itemReference.Party = ThePartyMgr.LoadParty(partyCode);
        ShowSuccessMessage("MasterData.ItemReference.AddItemReference.Successfully",itemReference.ReferenceCode);
    }

    public void InitPageParameter(string code)
    {
        this.ItemCode = code;
        ((Label)(this.FV_ItemReference.FindControl("lblItemCode"))).Text = this.ItemCode;
        ((CheckBox)(this.FV_ItemReference.FindControl("tbIsActive"))).Checked = true ;
        ((TextBox)(this.FV_ItemReference.FindControl("tbReferenceCode"))).Text = string.Empty;
        ((TextBox)(this.FV_ItemReference.FindControl("tbDescription"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_ItemReference.FindControl("tbPartyCode"))).Text = string.Empty;
        ((TextBox)(this.FV_ItemReference.FindControl("tbRemark"))).Text = string.Empty;
        ((DropDownList)(this.FV_ItemReference.FindControl("ddlPartyType"))).SelectedValue = string.Empty;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvReferenceCode":
                string partyCode = ((Controls_TextBox)(this.FV_ItemReference.FindControl("tbPartyCode"))).Text;
                if (TheItemReferenceMgr.LoadItemReference(ItemCode, partyCode, args.Value.Trim()) != null)
                {
                    ShowErrorMessage("MasterData.ItemReference.AddItemReference.Error", args.Value.Trim());
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }
}
