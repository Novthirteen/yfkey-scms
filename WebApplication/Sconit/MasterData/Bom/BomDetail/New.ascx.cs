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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;

public partial class MasterData_Bom_BomDetail_New : NewModuleBase
{
    private BomDetail bomdetail;
    private Item item;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public string BomCode
    {
        get
        {
            return (string)ViewState["BomCode"];
        }
        set
        {
            ViewState["BomCode"] = value;
        }
    }

    public void PageCleanup()
    {
        ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbParCode"))).Text = this.BomCode;
        ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbCompCode"))).Text = string.Empty;
        ((TextBox)(this.FV_BomDetail.FindControl("tbOp"))).Text = string.Empty;
        ((TextBox)(this.FV_BomDetail.FindControl("tbReference"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbUom"))).Text = string.Empty;
        ((TextBox)(this.FV_BomDetail.FindControl("tbRateQty"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbStruType"))).Text = TheCodeMasterMgr.GetDefaultCodeMaster(BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE).Value;
        ((TextBox)(this.FV_BomDetail.FindControl("tbScrapPercentage"))).Text = "0";
        ((TextBox)(this.FV_BomDetail.FindControl("tbStartTime"))).Text = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm");
        ((TextBox)(this.FV_BomDetail.FindControl("tbEndTime"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbLocation"))).Text = string.Empty;
        ((CheckBox)(this.FV_BomDetail.FindControl("cbNeedPrint"))).Checked = true;
        ((CheckBox)(this.FV_BomDetail.FindControl("cbIsShipScan"))).Checked = false;
        ((TextBox)(this.FV_BomDetail.FindControl("tbPriority"))).Text = "0";
        ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_BomDetail.FindControl("ddlBackFlushMethod"))).SelectedIndex = 0;

    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvParCode":
                if (TheBomMgr.LoadBom(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.BomDetail.WarningMessage.ParCodeError", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvCompCode":
                if (TheItemMgr.LoadItem(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.BomDetail.WarningMessage.CompCodeError", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvOp":
                try
                {
                    Convert.ToInt32(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("MasterData.BomDetail.WarningMessage.OpError");
                    args.IsValid = false;
                }
                break;
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
                    ShowWarningMessage("MasterData.BomDetail.WarningMessage.RateQtyError");
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

    protected void ODS_BomDetail_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string parcode = ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbParCode"))).Text.Trim();
        string compcode = ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbCompCode"))).Text.Trim();
        string uom = ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbUom"))).Text.Trim();
        string location = ((Controls_TextBox)(this.FV_BomDetail.FindControl("tbLocation"))).Text.Trim();

        com.Sconit.Control.CodeMstrDropDownList ddlBackFlushMethod = (com.Sconit.Control.CodeMstrDropDownList)this.FV_BomDetail.FindControl("ddlBackFlushMethod");

        bomdetail = (BomDetail)e.InputParameters[0];
        bomdetail.Bom = TheBomMgr.LoadBom(parcode);
        item = TheItemMgr.LoadItem(compcode);
        bomdetail.Item = item;
        if (item != null)
        {
            //default compcode and uom
            if (uom.Trim() == "")
            {
                bomdetail.Uom = item.Uom;
            }
            else
            {
                bomdetail.Uom = TheUomMgr.LoadUom(uom);
            }
        }
        if (location == "")
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

        bomdetail.ScrapPercentage = bomdetail.ScrapPercentage / 100;
        if (TheBomDetailMgr.CheckUniqueExist(bomdetail.Bom.Code, bomdetail.Item.Code, bomdetail.Operation, bomdetail.Reference, bomdetail.StartDate))
        {
            ShowWarningMessage("MasterData.BomDetail.WarningMessage.UniqueExistError");
            e.Cancel = true;
        }
    }

    protected void ODS_BomDetail_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(bomdetail.Id.ToString(), e);
            ShowSuccessMessage("Common.Business.Result.Insert.Successfully");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
