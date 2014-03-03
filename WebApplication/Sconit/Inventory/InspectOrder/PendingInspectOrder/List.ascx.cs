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

public partial class Inventory_PendingInspectOrder_List : ModuleBase
{
    private DetachedCriteria InspectCriteria
    {
        get
        {
            return (DetachedCriteria)ViewState["InspectCriteria"];
        }
        set
        {
            ViewState["InspectCriteria"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(DetachedCriteria selectCriteria)
    {

        this.InspectCriteria = selectCriteria;
        IList<InspectOrderDetail> pendingInspectOrderDetailList = TheCriteriaMgr.FindAll<InspectOrderDetail>(selectCriteria);

        this.GV_List.DataSource = pendingInspectOrderDetailList;
        this.GV_List.DataBind();
        if (pendingInspectOrderDetailList.Count == 0)
        {
            this.lblMessage.Visible = true;
            this.lblMessage.Text = "${Common.GridView.NoRecordFound}";
        }
        else
        {
            this.lblMessage.Visible = false;
        }

    }



    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InspectOrderDetail inspectOrderDetail = (InspectOrderDetail)e.Row.DataItem;
            //if (inspectOrderDetail.LocationLotDetail.Hu != null)
            //{
                ((TextBox)e.Row.FindControl("tbCurrentQualifiedQty")).ReadOnly = true;
                ((TextBox)e.Row.FindControl("tbCurrentRejectedQty")).ReadOnly = true;
                ((TextBox)e.Row.FindControl("tbCurrentQualifiedQty")).Text = inspectOrderDetail.PendingQualifiedQty.ToString("F2");
                ((TextBox)e.Row.FindControl("tbCurrentRejectedQty")).Text = inspectOrderDetail.PendingRejectedQty.ToString("F2");
            //}
        }
    }

    public IList<InspectOrderDetail> PopulateInspectOrderDetailList()
    {
        IList<InspectOrderDetail> inspectOrderDetailList = new List<InspectOrderDetail>();
        for (int i = 0; i < this.GV_List.Rows.Count; i++)
        {

            GridViewRow row = this.GV_List.Rows[i];


            HiddenField hfId = (HiddenField)row.FindControl("hfId");
            InspectOrderDetail inspectOrderDetail = TheInspectOrderDetailMgr.LoadInspectOrderDetail(int.Parse(hfId.Value));
            //if (inspectOrderDetail.LocationLotDetail.Hu == null)
            //{

            //    TextBox tbCurrentQualifiedQty = (TextBox)row.FindControl("tbCurrentQualifiedQty");
            //    decimal currentQualifiedQty = 0;
            //    if (tbCurrentQualifiedQty.Text.Trim() != string.Empty)
            //    {
            //        currentQualifiedQty = decimal.Parse(tbCurrentQualifiedQty.Text.Trim());
            //    }

            //    TextBox tbCurrentRejectedQty = (TextBox)row.FindControl("tbCurrentRejectedQty");
            //    decimal currentRejectedQty = 0;
            //    if (tbCurrentRejectedQty.Text.Trim() != string.Empty)
            //    {
            //        currentRejectedQty = decimal.Parse(tbCurrentRejectedQty.Text.Trim());
            //    }
            //    if (currentQualifiedQty != 0 || currentRejectedQty != 0)
            //    {
            //        inspectOrderDetail.CurrentQualifiedQty = currentQualifiedQty;
            //        inspectOrderDetail.CurrentRejectedQty = currentRejectedQty;
            //        inspectOrderDetailList.Add(inspectOrderDetail);
            //    }
            //}
            //else
            //{
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    inspectOrderDetail.CurrentQualifiedQty = inspectOrderDetail.PendingQualifiedQty;
                    inspectOrderDetail.CurrentRejectedQty = inspectOrderDetail.PendingRejectedQty;
                    inspectOrderDetailList.Add(inspectOrderDetail);
                }
            //}
        }

        return inspectOrderDetailList;

    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {

        try
        {
            TheInspectOrderMgr.ProcessInspectOrder(this.PopulateInspectOrderDetailList(), this.CurrentUser);
            ShowSuccessMessage("MasterData.Inventory.InspectOrder.Confirm.Successful");
            InitPageParameter(this.InspectCriteria);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }
}
