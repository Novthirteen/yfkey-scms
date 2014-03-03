using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class Inventory_PrintHu_ReceiptList : ModuleBase
{
    public event EventHandler PrintEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(Receipt receipt)
    {
        IList<ReceiptDetail> receiptDetailList = new List<ReceiptDetail>();
        if (receipt.ReceiptDetails != null && receipt.ReceiptDetails.Count > 0)
        {
            foreach (ReceiptDetail receiptDetail in receipt.ReceiptDetails)
            {
                if (receiptDetail.HuId != null)
                {
                    receiptDetailList.Add(receiptDetail);
                }
            }
        }
        this.GV_List.DataSource = receiptDetailList;
        this.GV_List.DataBind();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IList<ReceiptDetail> receiptDetailList = PopulateReceiptDetail();

        if (receiptDetailList == null || receiptDetailList.Count == 0)
        {
            this.ShowErrorMessage("Inventory.Error.PrintHu.ReceiptDetail.Required");
            return;
        }

        string huTemplate = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_HU_TEMPLATE).Value;
        if (receiptDetailList != null 
                            && receiptDetailList.Count > 0
                            && huTemplate != null
                            && huTemplate.Length > 0 )
        {
            IList<Hu> huList = new List<Hu>();
            foreach (ReceiptDetail receiptDetail in receiptDetailList)
            {
                huList.Add(TheHuMgr.LoadHu(receiptDetail.HuId));
            }

            IList<object> huDetailObj = new List<object>();

            huDetailObj.Add(huList);
            huDetailObj.Add(CurrentUser.Code);
            //receiptDetailList[0].Receipt.HuTemplate
            string barCodeUrl = TheReportMgr.WriteToFile(huTemplate, huDetailObj, huTemplate);

            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");

            this.ShowSuccessMessage("Inventory.PrintHu.Successful");
        }
    }
    
    private IList<ReceiptDetail> PopulateReceiptDetail()
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            IList<ReceiptDetail> receiptDetailList = new List<ReceiptDetail>();

            foreach (GridViewRow row in this.GV_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfId = row.FindControl("hfId") as HiddenField;

                    ReceiptDetail receiptDetail = TheReceiptDetailMgr.LoadReceiptDetail(int.Parse(hfId.Value));
                    receiptDetailList.Add(receiptDetail);
                }
            }

            return receiptDetailList;
        }

        return null;
    }
}
