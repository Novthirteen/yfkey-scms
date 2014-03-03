using System;
using System.Collections;
using System.Collections.Generic;
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
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using System.Text;
using com.Sconit.Entity.View;

public partial class Inventory_UnqualifiedGoods_List : ModuleBase
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(DetachedCriteria selectCriteria)
    {

        IList<InspectResult> unqualifiedGoodsList = TheCriteriaMgr.FindAll<InspectResult>(selectCriteria);

        this.GV_List.DataSource = unqualifiedGoodsList;
        this.GV_List.DataBind();
        if (unqualifiedGoodsList.Count == 0)
        {
            this.lblMessage.Visible = true;
            this.lblMessage.Text = "${Common.GridView.NoRecordFound}";
            this.btnPrint.Visible = false;
        }
        else
        {
            this.lblMessage.Visible = false;
            this.btnPrint.Visible = true;
        }

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        IList<object> list = new List<object>();
        IList<InspectResult> inspectResultList = PopulateUnqualifiedInspectOrder();
        if (inspectResultList.Count == 0)
        {
            ShowErrorMessage("Common.Business.Warn.DetailEmpty");
            return;
        }

        string rejNo = string.Empty;
        if (inspectResultList[0].PrintNo == null || inspectResultList[0].PrintNo == string.Empty)
        {
            rejNo = TheNumberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_INSPECTION_REJECT);  //不合格品单号
        }
        else
        {
            rejNo = inspectResultList[0].PrintNo;
        }
        InspectOrder inspectOrder = new InspectOrder();
        inspectOrder.InspectNo = rejNo;
        inspectOrder.CreateUser = this.CurrentUser;
        inspectOrder.CreateDate = DateTime.Now;
       

        list.Add(inspectOrder);
        list.Add(inspectResultList);
        string printUrl = TheReportMgr.WriteToFile("UnqualifiedGoods.xls", list);
        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
        this.ShowSuccessMessage("MasterData.Inventory.InspectOrder.Unqualified.Print.Successful");

        this.GV_List.DataSource = null;
        this.GV_List.DataBind();
    }


    public IList<InspectResult> PopulateUnqualifiedInspectOrder()
    {
        IList<InspectResult> inspectResultList = new List<InspectResult>();
        for (int i = 0; i < this.GV_List.Rows.Count; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];
            CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
            if (checkBoxGroup.Checked)
            {

                InspectResult unqualifiedGoods = new InspectResult();


                HiddenField hfId = (HiddenField)row.FindControl("hfId");

                InspectResult inspectResult = TheInspectResultMgr.LoadInspectResult(Int32.Parse(hfId.Value));

                inspectResultList.Add(inspectResult);
            }
        }
        return inspectResultList;
    }




}
