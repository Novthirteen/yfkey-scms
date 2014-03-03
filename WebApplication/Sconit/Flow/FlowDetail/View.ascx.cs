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
using com.Sconit.Service.Procurement;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity;

public partial class MasterData_FlowDetail_View : ModuleBase
{
    public event EventHandler BackEvent;
    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }

    public int FlowDetailId
    {
        get
        {
            if (ViewState["FlowDetailId"] == null)
            {
                return 0;
            }
            else
            {
                return (Int32)ViewState["FlowDetailId"];
            }
        }
        set
        {
            ViewState["FlowDetailId"] = value;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    protected void FV_FlowDetail_DataBound(object sender, EventArgs e)
    {
        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT
            || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
        {
            this.FV_FlowDetail.FindControl("fdProcurement").Visible = true;
            this.FV_FlowDetail.FindControl("lblNeedInspect").Visible = true;
            this.FV_FlowDetail.FindControl("cbNeedInspect").Visible = true;
            this.FV_FlowDetail.FindControl("lblIdMark").Visible = true; ;
            this.FV_FlowDetail.FindControl("tbIdMark").Visible = true;
            this.FV_FlowDetail.FindControl("lblBarCodeType").Visible = true;
            this.FV_FlowDetail.FindControl("ddlRMBarCodeType").Visible = true;

        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
        {
            this.FV_FlowDetail.FindControl("fdProcurement").Visible = true;
            this.FV_FlowDetail.FindControl("lblBillSettleTerm").Visible = false;
            this.FV_FlowDetail.FindControl("ddlBillSettleTerm").Visible = false;


        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
        {
            this.FV_FlowDetail.FindControl("fdDistribution").Visible = true;
            this.FV_FlowDetail.FindControl("lblBillSettleTerm").Visible = false;
            this.FV_FlowDetail.FindControl("ddlBillSettleTerm").Visible = false;
            this.FV_FlowDetail.FindControl("lblOddShipOption").Visible = true;
            this.FV_FlowDetail.FindControl("ddlOddShipOption").Visible = true;

        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
        {
            this.FV_FlowDetail.FindControl("fdProduction").Visible = true;
            this.FV_FlowDetail.FindControl("trBom").Visible = true;
            this.FV_FlowDetail.FindControl("trCustomer").Visible = true;
            this.FV_FlowDetail.FindControl("lblBillSettleTerm").Visible = false;
            this.FV_FlowDetail.FindControl("ddlBillSettleTerm").Visible = false;

            this.FV_FlowDetail.FindControl("lblNeedInspect").Visible = true;
            this.FV_FlowDetail.FindControl("cbNeedInspect").Visible = true;

            Literal lblIdMark = ((Literal)this.FV_FlowDetail.FindControl("lblIdMark"));
            this.FV_FlowDetail.FindControl("tbIdMark").Visible = true;
            this.FV_FlowDetail.FindControl("lblBarCodeType").Visible = true;
            this.FV_FlowDetail.FindControl("ddlFGBarCodeType").Visible = true;

            lblIdMark.Visible = true;
            lblIdMark.Text = "${MasterData.Flow.FlowDetail.IdMark.Production}";

          


        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
        {
            this.FV_FlowDetail.FindControl("fdTransfer").Visible = true;
            this.FV_FlowDetail.FindControl("lblBillSettleTerm").Visible = false;
            this.FV_FlowDetail.FindControl("ddlBillSettleTerm").Visible = false;
            //this.FV_FlowDetail.FindControl("lblShipOption").Visible = true;
            //this.FV_FlowDetail.FindControl("ddlShipOption").Visible = true;
        }

    }

    public void InitPageParameter(Int32 flowDetailId)
    {
        this.FlowDetailId = flowDetailId;
        this.ODS_FlowDetail.SelectParameters["id"].DefaultValue = flowDetailId.ToString();

    }




    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }




}
