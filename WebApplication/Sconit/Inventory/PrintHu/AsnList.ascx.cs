using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.Distribution;
using com.Sconit.Web;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class Inventory_PrintHu_AsnList : ModuleBase
{
    public event EventHandler PrintEvent;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(InProcessLocation inProcessLocation)
    {
        IList<InProcessLocationDetail> inProcessLocationDetailList = new List<InProcessLocationDetail>();
        if (inProcessLocation.InProcessLocationDetails != null && inProcessLocation.InProcessLocationDetails.Count > 0)
        {
            foreach(InProcessLocationDetail inProcessLocationDetail in inProcessLocation.InProcessLocationDetails)
            {
                if (inProcessLocationDetail.HuId != null)
                {
                    inProcessLocationDetailList.Add(inProcessLocationDetail);
                }
            }
        }
        this.GV_List.DataSource = inProcessLocationDetailList;
        this.GV_List.DataBind();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IList<InProcessLocationDetail> inProcessLocationDetailList = PopulateInProcessLocationDetail();
        
        if (inProcessLocationDetailList == null || inProcessLocationDetailList.Count == 0)
        {
            this.ShowErrorMessage("Inventory.Error.PrintHu.InProcessLocationDetail.Required");
            return;
        }

        String huTemplate = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_HU_TEMPLATE).Value;
        if (inProcessLocationDetailList != null
                && inProcessLocationDetailList.Count > 0
                && huTemplate != null
                && huTemplate.Length > 0 )
        {
            IList<Hu> huList = new List<Hu>();
            foreach (InProcessLocationDetail inProcessLocationDetail in inProcessLocationDetailList)
            {
                huList.Add(TheHuMgr.LoadHu(inProcessLocationDetail.HuId));
            }

            IList<object> huDetailObj = new List<object>();

            huDetailObj.Add(huList);
            huDetailObj.Add(CurrentUser.Code);

            //inProcessLocationDetailList[0].InProcessLocation.HuTemplate
            
            string barCodeUrl = TheReportMgr.WriteToFile(huTemplate, huDetailObj, huTemplate);

            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");

            this.ShowSuccessMessage("Inventory.PrintHu.Successful");
        }
    }

    private IList<InProcessLocationDetail> PopulateInProcessLocationDetail()
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            IList<InProcessLocationDetail> inProcessLocationDetailList = new List<InProcessLocationDetail>();

            foreach (GridViewRow row in this.GV_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfId = row.FindControl("hfId") as HiddenField;

                    InProcessLocationDetail inProcessLocationDetail = TheInProcessLocationDetailMgr.LoadInProcessLocationDetail(int.Parse(hfId.Value));
                    inProcessLocationDetailList.Add(inProcessLocationDetail);
                }
            }


            return inProcessLocationDetailList;
        }

        return null;
    }
}
