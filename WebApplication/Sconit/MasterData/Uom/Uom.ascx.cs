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
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;

public partial class MasterData_Uom_Uom : ModuleBase
{

    private string uomCode = string.Empty;

    public void QuickSearch()
    {
        this.GV_Uom.DataBind();
        this.fldSearch.Visible = true;
        this.fldInsert.Visible = false;
        this.fldGV.Visible = true;
    }

    protected void GV_Uom_OnDataBind(object sender, EventArgs e)
    {
        this.fldGV.Visible = true;
        if (((GridView)(sender)).Rows.Count == 0)
        {
            this.lblResult.Visible = true;
        }
        else
        {
            this.lblResult.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        QuickSearch();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        //this.fldSearch.Visible = false;
        //this.fldGV.Visible = false;
        this.fldInsert.Visible = true;
        ((TextBox)(this.FV_Uom.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Uom.FindControl("tbName"))).Text = string.Empty;
        ((TextBox)(this.FV_Uom.FindControl("tbDescription"))).Text = string.Empty;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.fldSearch.Visible = true;
        this.fldGV.Visible = true;
        this.fldInsert.Visible = false;
        // this.GV_Uom.DataBind();
    }

    protected void ODS_GV_Uom_OnUpdating(object source, ObjectDataSourceMethodEventArgs e)
    {
        Uom uom = (Uom)e.InputParameters[0];
        if (uom!=null)
        {
            uom.Description = uom.Description.Trim();
            uom.Name = uom.Name.Trim();
        }
    }
    protected void ODS_Uom_Inserting(object source, ObjectDataSourceMethodEventArgs e)
    {
        Uom uom = (Uom)e.InputParameters[0];

        uom.Description = uom.Description.Trim();
        uom.Name = uom.Name.Trim();

        if (uom.Code == null || uom.Code.Trim() == string.Empty)
        {
            ShowWarningMessage("MasterData.Uom.Code.Empty", "");
            e.Cancel = true;
            return;
        }
        else
        {
            uom.Code = uom.Code.Trim();
        }

        if (TheUomMgr.LoadUom(uom.Code) == null)
        {
            ShowSuccessMessage("MasterData.Uom.AddUom.Successfully", uom.Code);
        }
        else
        {
            e.Cancel = true;
            ShowErrorMessage("MasterData.Uom.AddUom.Error", uom.Code);
        }
    }

    protected void ODS_Uom_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        QuickSearch();
    }

    protected void ODS_GV_Uom_OnDeleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Uom uom = (Uom)e.InputParameters[0];
        uomCode = uom.Code;
    }

    protected void ODS_GV_Uom_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            ShowSuccessMessage("MasterData.Uom.DeleteUom.Successfully", uomCode);
        }
        catch (Exception ex)
        {
            ShowErrorMessage("MasterData.Uom.DeleteUom.Fail", uomCode);
        }

    }

    protected void GV_Uom_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //校验
    }
}
