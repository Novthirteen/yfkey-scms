using System;
using System.Collections;
using System.Web.UI.WebControls;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Web;
using System.Drawing;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using NHibernate.Expression;

public partial class Inventory_InspectOrder_InspectDetailList : ModuleBase
{

    public event EventHandler BackEvent;

    public bool IsPartQualified
    {
        get
        {
            return (bool)ViewState["IsPartQualified"];
        }
        set
        {
            ViewState["IsPartQualified"] = value;
        }
    }
    private bool IsDetailHasHu
    {
        get
        {
            return (bool)ViewState["IsDetailHasHu"];
        }
        set
        {
            ViewState["IsDetailHasHu"] = value;
        }
    }
    private string InspectNo
    {
        get
        {
            return (string)ViewState["InspectNo"];
        }
        set
        {
            ViewState["InspectNo"] = value;
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InspectOrderDetail inspectOrderDetail = (InspectOrderDetail)e.Row.DataItem;

            decimal qualifiedQty = inspectOrderDetail.QualifiedQty.HasValue ? (decimal)inspectOrderDetail.QualifiedQty : 0;
            decimal rejectedQty = inspectOrderDetail.RejectedQty.HasValue ? (decimal)inspectOrderDetail.RejectedQty : 0;
            if (inspectOrderDetail.InspectQty > qualifiedQty + rejectedQty + inspectOrderDetail.PendingQualifiedQty + inspectOrderDetail.PendingRejectedQty)
            {
                ((TextBox)e.Row.FindControl("tbCurrentQualifiedQty")).ReadOnly = false;
                ((TextBox)e.Row.FindControl("tbCurrentRejectedQty")).ReadOnly = false;

                ((com.Sconit.Control.CodeMstrLabel)e.Row.FindControl("lblDisposition")).Visible = false;

            }
            else
            {
                //((CheckBox)e.Row.FindControl("CheckBoxGroup")).Visible = false;

                ((com.Sconit.Control.CodeMstrDropDownList)e.Row.FindControl("ddlDisposition")).Visible = false;


            }
        }
    }

    public void InitPageParameter(string inspectNo)
    {

        InitPageParameter(inspectNo, false);
    }

    public void InitPageParameter(string inspectNo, bool isWorkShop)
    {
        this.InspectNo = inspectNo;
        InspectOrder inspectOrder = TheInspectOrderMgr.LoadInspectOrder(inspectNo, true);
        this.GV_List.DataSource = inspectOrder.InspectOrderDetails;
        this.GV_List.DataBind();

        this.GV_List.Columns[7].Visible = inspectOrder.IsDetailHasHu;

        this.IsDetailHasHu = inspectOrder.IsDetailHasHu;

        if (this.IsPartQualified || !inspectOrder.IsDetailHasHu)
        {
            this.GV_List.Columns[13].Visible = true;
            this.GV_List.Columns[14].Visible = true;
            //  this.GV_List.Columns[0].Visible = false;
        }
        else
        {
            //this.GV_List.Columns[0].Visible = true;
            this.GV_List.Columns[13].Visible = false;
            this.GV_List.Columns[14].Visible = false;
        }

        if (isWorkShop)
        {
            this.GV_List.Columns[0].Visible = false;
            this.GV_List.Columns[9].Visible = false;
            this.GV_List.Columns[10].Visible = false;
            this.GV_List.Columns[11].Visible = false;
            this.GV_List.Columns[12].Visible = false;
            this.GV_List.Columns[13].Visible = false;
            this.GV_List.Columns[14].Visible = false;
            this.GV_List.Columns[17].Visible = false;
        }


    }


    public IList<InspectOrderDetail> PopulateInspectOrder(bool? isQualified)
    {
        IList<InspectOrderDetail> inspectOrderDetailList = new List<InspectOrderDetail>();
        if (this.IsPartQualified || !this.IsDetailHasHu)
        {
            #region 允许部分合格/按照数量
            for (int i = 0; i < this.GV_List.Rows.Count; i++)
            {
                GridViewRow row = this.GV_List.Rows[i];
                TextBox tbCurrentQualifiedQty = (TextBox)row.FindControl("tbCurrentQualifiedQty");
                decimal currentQualifiedQty = 0;
                if (tbCurrentQualifiedQty.Text.Trim() != string.Empty)
                {
                    currentQualifiedQty = decimal.Parse(tbCurrentQualifiedQty.Text.Trim());
                }


                TextBox tbCurrentRejectedQty = (TextBox)row.FindControl("tbCurrentRejectedQty");
                decimal currentRejectedQty = 0;
                if (tbCurrentRejectedQty.Text.Trim() != string.Empty)
                {
                    currentRejectedQty = decimal.Parse(tbCurrentRejectedQty.Text.Trim());
                }
                if (currentQualifiedQty != 0 || currentRejectedQty != 0)
                {
                    HiddenField hfId = (HiddenField)row.FindControl("hfId");
                    InspectOrderDetail inspectOrderDetail = TheInspectOrderDetailMgr.LoadInspectOrderDetail(int.Parse(hfId.Value));
                    inspectOrderDetail.CurrentQualifiedQty = currentQualifiedQty;
                    inspectOrderDetail.CurrentRejectedQty = currentRejectedQty;
                    com.Sconit.Control.DropDownList ddlDisposition = (com.Sconit.Control.DropDownList)row.FindControl("ddlDisposition");
                    if (ddlDisposition.SelectedIndex != -1 && inspectOrderDetail.CurrentRejectedQty > 0)
                    {
                        inspectOrderDetail.Disposition = ddlDisposition.SelectedValue;
                    }
                    inspectOrderDetailList.Add(inspectOrderDetail);
                }
            }
            #endregion
        }
        else
        {
            #region 全部合格/不合格
            for (int i = 0; i < this.GV_List.Rows.Count; i++)
            {
                GridViewRow row = this.GV_List.Rows[i];
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfId = (HiddenField)row.FindControl("hfId");
                    InspectOrderDetail inspectOrderDetail = TheInspectOrderDetailMgr.LoadInspectOrderDetail(int.Parse(hfId.Value));
                    if ((bool)isQualified)
                    {
                        inspectOrderDetail.CurrentQualifiedQty = inspectOrderDetail.InspectQty;
                    }
                    else
                    {
                        inspectOrderDetail.CurrentRejectedQty = inspectOrderDetail.InspectQty;
                    }

                    com.Sconit.Control.DropDownList ddlDisposition = (com.Sconit.Control.DropDownList)row.FindControl("ddlDisposition");
                    if (ddlDisposition.SelectedIndex != -1 && inspectOrderDetail.CurrentRejectedQty > 0)
                    {
                        inspectOrderDetail.Disposition = ddlDisposition.SelectedValue;
                    }

                    inspectOrderDetailList.Add(inspectOrderDetail);
                }

            }
            #endregion
        }

        return inspectOrderDetailList;

    }

    public IList<InspectResult> PopulateUnqualifiedInspectOrder()
    {

        DetachedCriteria maxRecCriteria = DetachedCriteria.For(typeof(InspectResult));
        maxRecCriteria.CreateAlias("InspectOrderDetail", "id");
        maxRecCriteria.CreateAlias("id.InspectOrder", "i");
        maxRecCriteria.Add(Expression.Eq("i.InspectNo", this.InspectNo));
        maxRecCriteria.Add(Expression.IsNotNull("RejectedQty"));
        maxRecCriteria.Add(Expression.Gt("RejectedQty",decimal.Zero));
        maxRecCriteria.SetProjection(Projections.ProjectionList().Add(Projections.Max("InspectResultNo").As("InspectResultNo")));

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InspectResult));
        selectCriteria.CreateAlias("InspectOrderDetail", "id");
        selectCriteria.CreateAlias("id.InspectOrder", "i");
        selectCriteria.Add(Expression.Eq("i.InspectNo",this.InspectNo));
        selectCriteria.Add(Subqueries.PropertyIn("InspectResultNo", maxRecCriteria));
        

        return TheCriteriaMgr.FindAll<InspectResult>(selectCriteria);
    }

    //public IList<InspectOrderDetail> PopulateUnqualifiedInspectOrder()
    //{
    //    IList<InspectOrderDetail> inspectDetailList = new List<InspectOrderDetail>();

    //    DetachedCriteria maxRecCriteria = DetachedCriteria.For(typeof(LocationTransaction));

    //    maxRecCriteria.Add(Expression.Eq("OrderNo", this.InspectNo));
    //    maxRecCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP));
    //    maxRecCriteria.Add(Expression.Eq("Location", BusinessConstants.SYSTEM_LOCATION_REJECT));
    //    maxRecCriteria.SetProjection(Projections.ProjectionList().Add(Projections.Max("ReceiptNo").As("ReceiptNo")));

    //    DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(LocationTransaction));
    //    selectCriteria.Add(Expression.Eq("OrderNo", this.InspectNo));
    //    selectCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP));
    //    selectCriteria.Add(Expression.Eq("Location", BusinessConstants.SYSTEM_LOCATION_REJECT));
    //    selectCriteria.Add(Subqueries.PropertyIn("ReceiptNo", maxRecCriteria));

    //    IList<LocationTransaction> locTransList = TheCriteriaMgr.FindAll<LocationTransaction>(selectCriteria);

    //    if (locTransList != null && locTransList.Count > 0)
    //    {
    //        foreach (LocationTransaction locTrans in locTransList)
    //        {
    //            if (locTrans.OrderDetailId != 0)
    //            {
    //                InspectOrderDetail inspectDetail = TheInspectOrderDetailMgr.LoadInspectOrderDetail(locTrans.OrderDetailId);
    //                TheCriteriaMgr.CleanSession();
    //                inspectDetail.RejectedQty = locTrans.Qty;
    //                inspectDetailList.Add(inspectDetail);
    //            }
    //        }

    //    }

    //    return inspectDetailList;
    //}
}
