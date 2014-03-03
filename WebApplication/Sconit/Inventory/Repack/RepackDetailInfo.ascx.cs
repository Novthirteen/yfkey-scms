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
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Inventory_Repack_RepackDetailInfo : ModuleBase
{
    public event EventHandler BackEvent;

    public string RepackType
    {
        get
        {
            return (string)ViewState["RepackType"];
        }
        set
        {
            ViewState["RepackType"] = value;
        }
    }

    public string HuId
    {
        get
        {
            return (string)ViewState["HuId"];
        }
        set
        {
            ViewState["HuId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        decimal unitCount = decimal.Parse(this.tbUnitCount.Text.Trim());
        int count = int.Parse(this.tbQty.Text.Trim());
       IList<Hu> huList = TheHuMgr.CloneHu(HuId, unitCount, count, this.CurrentUser);

        IList<object> huDetailObj = new List<object>();

        huDetailObj.Add(huList);
        huDetailObj.Add(CurrentUser.Code);

        string huTemplate = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_HU_TEMPLATE).Value;
        if (huTemplate != null && huTemplate.Length > 0)
        {
            string barCodeUrl = TheReportMgr.WriteToFile(huTemplate, huDetailObj, "BarCode.xls");
            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");
            this.ShowSuccessMessage("Inventory.PrintHu.Successful");
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    public void InitPageParameter(TransformerDetail transformDetail)
    {
        tbUnitCount.Text = transformDetail.UnitCount.ToString("F2");
        tbQty.Text = "1";
        tbUnitCount.Attributes["oldValue"] = tbUnitCount.Text;
        tbQty.Attributes["oldValue"] = tbQty.Text;
        this.HuId = transformDetail.HuId;
    }




}
