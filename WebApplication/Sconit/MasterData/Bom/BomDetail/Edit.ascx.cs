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
using com.Sconit.Entity;

public partial class MasterData_Bom_BomDetail_Edit : EditModuleBase
{
    private BomDetail bomdetail;
    private Item item;
    public event EventHandler BackEvent;

    protected string code
    {
        get
        {
            return (string)ViewState["code"];
        }
        set
        {
            ViewState["code"] = value;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string code)
    {
        this.code = code;
        this.ODS_BomDetail.SelectParameters["ID"].DefaultValue = code;
        this.ODS_BomDetail.DeleteParameters["ID"].DefaultValue = code;
        this.UpdateView();
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvUom":
                if (TheUomMgr.LoadUom(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.Bom.WarningMessage.UomInvalid", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvRateQty":
                try
                {
                    Convert.ToDecimal(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("MasterData.BomDetail.WarningMessage.RateQtyError", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvStruType":
                if (TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE, args.Value) == null)
                {
                    ShowWarningMessage("MasterData.BomDetail.WarningMessage.StruTypeError");
                    args.IsValid = false;
                }
                break;
            case "cvScrapPercentage":
                try
                {
                    Convert.ToDecimal(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("MasterData.BomDetail.WarningMessage.ScrapPercentageError");
                    args.IsValid = false;
                }
                break;
            case "cvStartTime":
                try
                {
                    Convert.ToDateTime(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("MasterData.BomDetail.WarningMessage.StartTimeError");
                    args.IsValid = false;
                }
                break;
            case "cvEndTime":
                try
                {
                    if (args.Value.Trim() != "")
                    {
                        Convert.ToDateTime(args.Value);
                    }
                }
                catch (Exception)
                {
                    ShowWarningMessage("MasterData.BomDetail.WarningMessage.EndTimeError");
                    args.IsValid = false;
                }
                break;
            case "cvLocation":
                if (args.Value.Trim() != "")
                {
                    if (TheLocationMgr.LoadLocation(args.Value) == null)
                    {
                        ShowWarningMessage("MasterData.BomDetail.WarningMessage.LocationError", args.Value);
                        args.IsValid = false;
                    }
                }
                break;
            default:
                break;
        }
    }

    protected void FV_BomDetail_DataBound(object sender, EventArgs e)
    {
        this.UpdateView();
    }

    private void UpdateView()
    {
        bomdetail = TheBomDetailMgr.LoadBomDetail(Convert.ToInt32(this.code));
        TextBox tbParCode = (TextBox)(this.FV_BomDetail.FindControl("tbParCode"));
        TextBox tbCompCode = (TextBox)(this.FV_BomDetail.FindControl("tbCompCode"));
        TextBox tbOp = (TextBox)(this.FV_BomDetail.FindControl("tbOp"));
        TextBox tbReference = (TextBox)(this.FV_BomDetail.FindControl("tbReference"));
        TextBox tbStartTime = (TextBox)(this.FV_BomDetail.FindControl("tbStartTime"));
        TextBox tbEndTime = (TextBox)(this.FV_BomDetail.FindControl("tbEndTime"));
        TextBox tbRateQty = (TextBox)(this.FV_BomDetail.FindControl("tbRateQty"));
        Controls_TextBox tbUom = (Controls_TextBox)(this.FV_BomDetail.FindControl("tbUom"));
        Controls_TextBox tbStruType = (Controls_TextBox)(this.FV_BomDetail.FindControl("tbStruType"));
        TextBox tbScrapPercentage = (TextBox)(this.FV_BomDetail.FindControl("tbScrapPercentage"));
        Controls_TextBox tbLocation = (Controls_TextBox)(this.FV_BomDetail.FindControl("tbLocation"));
        CheckBox cbNeedPrint = (CheckBox)(this.FV_BomDetail.FindControl("cbNeedPrint"));
        CheckBox cbIsShipScan = (CheckBox)(this.FV_BomDetail.FindControl("cbIsShipScan"));
        TextBox tbPriority = (TextBox)(this.FV_BomDetail.FindControl("tbPriority"));
        com.Sconit.Control.CodeMstrDropDownList ddlBackFlushMethod = (com.Sconit.Control.CodeMstrDropDownList)this.FV_BomDetail.FindControl("ddlBackFlushMethod");

        tbParCode.Text = bomdetail.Bom.Code;
        tbCompCode.Text = bomdetail.Item.Code;
        tbOp.Text = bomdetail.Operation.ToString();
        tbReference.Text = bomdetail.Reference;
        tbStartTime.Text = bomdetail.StartDate.ToString("yyyy-MM-dd HH:mm");
        if (bomdetail.EndDate != null)
        {
            tbEndTime.Text = ((DateTime)bomdetail.EndDate).ToString("yyyy-MM-dd HH:mm");
        }
        tbRateQty.Text = bomdetail.RateQty.ToString("0.########");
        tbUom.Text = bomdetail.Uom.Code;
        tbStruType.Text = bomdetail.StructureType;
        tbScrapPercentage.Text = (100 * bomdetail.ScrapPercentage).ToString("0.########");
        if (bomdetail.Location != null)
        {
            tbLocation.Text = bomdetail.Location.Code;
        }
        tbPriority.Text = bomdetail.Priority.ToString();
        if (bomdetail.BackFlushMethod != string.Empty)
        {
            ddlBackFlushMethod.SelectedValue = bomdetail.BackFlushMethod;
        }
    }

    protected void ODS_BomDetail_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string parcode = ((TextBox)(this.FV_BomDetail.FindControl("tbParCode"))).Text.Trim();
        string compcode = ((TextBox)(this.FV_BomDetail.FindControl("tbCompCode"))).Text.Trim();
        string uom = ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbUom"))).Text.Trim();
        string location = ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbLocation"))).Text.Trim();
        string endTime = ((TextBox)(this.FV_BomDetail.FindControl("tbEndTime"))).Text.Trim();
        com.Sconit.Control.CodeMstrDropDownList ddlBackFlushMethod = (com.Sconit.Control.CodeMstrDropDownList)this.FV_BomDetail.FindControl("ddlBackFlushMethod");

        bomdetail = (BomDetail)e.InputParameters[0];
        bomdetail.Bom = TheBomMgr.LoadBom(parcode);
        item = TheItemMgr.LoadItem(compcode);
        bomdetail.Item = item;
        if (item != null)
        {
            //default compcode and uom
            if (uom.Trim() == string.Empty)
            {
                bomdetail.Uom = item.Uom;
            }
            else
            {
                bomdetail.Uom = TheUomMgr.LoadUom(uom);
            }
        }
        if (location == string.Empty)
        {
            bomdetail.Location = null;
        }
        else
        {
            bomdetail.Location = TheLocationMgr.LoadLocation(location);
        }
        if (ddlBackFlushMethod.SelectedIndex != -1)
        {
            bomdetail.BackFlushMethod = ddlBackFlushMethod.SelectedValue;
        }
        if (endTime != string.Empty)
        {
            bomdetail.EndDate = DateTime.Parse(endTime);
        }
        bomdetail.ScrapPercentage = bomdetail.ScrapPercentage / 100;
    }

    protected void ODS_BomDetail_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Common.Business.Result.Update.Successfully");
    }

    protected void ODS_BomDetail_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Common.Business.Result.Delete.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Common.Business.Result.Delete.Failed");
            e.ExceptionHandled = true;
        }
    }
}
