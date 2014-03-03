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

public partial class MasterData_UomConversion_Main : ModuleBase
{

    private string[] itemMessage = new string[5];

    protected void GV_UomConversion_OnDataBind(object sender, EventArgs e)
    {
        this.fldGV.Visible = true;
        if (((GridView)(sender)).Rows.Count == 0)
            this.lblResult.Visible = true;
        else
            this.lblResult.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.GV_UomConversion.DataBind();
        this.fldSearch.Visible = true;
        this.fldInsert.Visible = false;
        this.fldGV.Visible = true;
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        this.fldInsert.Visible = true;
        ((Controls_TextBox)(this.FV_UomConversion.FindControl("tbItemCode"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_UomConversion.FindControl("tbBaseUom"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_UomConversion.FindControl("tbAltUom"))).Text = string.Empty;
        ((TextBox)(this.FV_UomConversion.FindControl("tbBaseQty"))).Text = string.Empty;
        ((TextBox)(this.FV_UomConversion.FindControl("tbAltQty"))).Text = string.Empty;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.fldSearch.Visible = true;
        this.fldGV.Visible = true;
        this.fldInsert.Visible = false;
        // this.GV_UomConversion.DataBind();
    }

    protected void ODS_GV_UomConversion_OnUpdating(object source, ObjectDataSourceMethodEventArgs e)
    {
        UomConversion uomConversion = (UomConversion)e.InputParameters[0];
        UomConversion oldConversion = TheUomConversionMgr.LoadUomConversion(uomConversion.Id);
        uomConversion.BaseUom = oldConversion.BaseUom;
        uomConversion.AlterUom = oldConversion.AlterUom;
        uomConversion.Item = oldConversion.Item;

        itemMessage[0] = oldConversion.Item == null ? string.Empty : oldConversion.Item.Code;
        itemMessage[1] = uomConversion.BaseQty.ToString();
        itemMessage[2] = oldConversion.BaseUom.Code;
        itemMessage[3] = uomConversion.AlterQty.ToString();
        itemMessage[4] = oldConversion.AlterUom.Code;
        ShowSuccessMessage("MasterData.UomConversion.UpdateUomConversion.Successfully", itemMessage);
    }

 

    protected void ODS_UomConversion_Inserting(object source, ObjectDataSourceMethodEventArgs e)
    {
        UomConversion uomConversion = (UomConversion)e.InputParameters[0];

        string itemCode = ((Controls_TextBox)(this.FV_UomConversion.FindControl("tbItemCode"))).Text;
        string altUom = ((Controls_TextBox)(this.FV_UomConversion.FindControl("tbAltUom"))).Text;
        string baseUom = ((Controls_TextBox)(this.FV_UomConversion.FindControl("tbBaseUom"))).Text;

        itemMessage[0] = itemCode;
        itemMessage[1] = uomConversion.BaseQty.ToString();
        itemMessage[2] = baseUom;
        itemMessage[3] = uomConversion.AlterQty.ToString();
        itemMessage[4] = altUom;

        //if (itemCode == null || itemCode.Trim() == string.Empty)
        //{
        //    ShowWarningMessage("MasterData.UomConversion.Required.itemCode", "");
        //    e.Cancel = true;
        //    return;
        //}
        if (altUom == null || altUom.Trim() == string.Empty)
        {
            ShowWarningMessage("MasterData.UomConversion.Required.altUom", "");
            e.Cancel = true;
            return;
        }
        if (altUom == baseUom)
        {
            ShowWarningMessage("MasterData.UomConversion.Same.Uom", baseUom);
            e.Cancel = true;
            return;
        }
        if (baseUom == null || baseUom.Trim() == string.Empty)
        {
            ShowWarningMessage("MasterData.UomConversion.Required.baseUom", "");
            e.Cancel = true;
            return;
        }
        if (TheUomConversionMgr.LoadUomConversion(itemCode, altUom, baseUom) == null && TheUomConversionMgr.LoadUomConversion(itemCode, baseUom, altUom) == null)
        {
            uomConversion.Item = TheItemMgr.LoadItem(itemCode);
            uomConversion.AlterUom = TheUomMgr.LoadUom(altUom);
            uomConversion.BaseUom = TheUomMgr.LoadUom(baseUom);
            ShowSuccessMessage("MasterData.UomConversion.AddUomConversion.Successfully", itemMessage);
        }
        else
        {
            e.Cancel = true;
            ShowErrorMessage("MasterData.UomConversion.AddUomConversion.Error", itemMessage);
            return;
        }
    }

    protected void ODS_UomConversion_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        btnSearch_Click(this, null);
    }

    protected void ODS_GV_UomConversion_OnDeleting(object sender, ObjectDataSourceMethodEventArgs e)
    {

        UomConversion uomConversion = (UomConversion)e.InputParameters[0];
        UomConversion delUomConversion = TheUomConversionMgr.LoadUomConversion(uomConversion.Id);
        itemMessage[0] = delUomConversion.Item == null ? string.Empty : delUomConversion.Item.Code;
        itemMessage[1] = delUomConversion.BaseQty.ToString();
        itemMessage[2] = delUomConversion.BaseUom.Code;
        itemMessage[3] = delUomConversion.AlterQty.ToString();
        itemMessage[4] = delUomConversion.AlterUom.Code;
    }

    protected void ODS_GV_UomConversion_OnDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            ShowSuccessMessage("MasterData.UomConversion.DeleteUomConversion.Successfully", itemMessage);
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.UomConversion.DeleteUomConversion.Fail", itemMessage);
        }
    }


    protected void GV_UomConversion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string BaseQty = ((TextBox)GV_UomConversion.Rows[e.RowIndex].Cells[3].Controls[1]).Text.ToString().Trim();
        string AlterQty = ((TextBox)GV_UomConversion.Rows[e.RowIndex].Cells[5].Controls[1]).Text.ToString().Trim();


        try
        {
            Convert.ToDecimal(BaseQty);
        }
        catch (Exception)
        {
            e.Cancel = true;
            ShowErrorMessage("Common.Decimal.Error", itemMessage);
            return;
        }

        try
        {
            Convert.ToDecimal(AlterQty);
        }
        catch (Exception)
        {
            e.Cancel = true;
            ShowErrorMessage("Common.Decimal.Error", itemMessage);
            return;
        }


    }
}
