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
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Utility;

public partial class Inventory_InvAdjust_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    public string code
    {
        get { return (string)ViewState["Code"]; }
        set { ViewState["Code"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.code = code;
        UpdateView();
    }

    public void UpdateView()
    {
        CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(code);
        this.ucInfo.InitPageParameter(cycleCount);

        this.btnRecalc.Visible = (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT);
        this.btnAdjust.Visible = (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT);

        //if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
        //{
        //    IList<CycleCountResult> cycleCountResultList = TheCycleCountMgr.ReCalcCycleCount(this.code, this.CurrentUser);
        //    this.BindData(cycleCountResultList);
        //}
        //else
        //{
            this.BindData();
        //}
    }

    protected void btnRecalc_Click(object sender, EventArgs e)
    {
        try
        {
            IList<CycleCountResult> cycleCountResultList = TheCycleCountMgr.ReCalcCycleCount(this.code, this.CurrentUser);
            this.BindData(cycleCountResultList);
            ShowSuccessMessage("Common.Business.Result.Update.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnAdjust_Click(object sender, EventArgs e)
    {
        try
        {
            IList<CycleCountResult> cycleCountResultList = TheCycleCountResultMgr.GetCycleCountResult(this.code);
            foreach (GridViewRow gvr in GV_List.Rows)
            {
                TextBox tbDiffReason = (TextBox)gvr.FindControl("tbDiffReason");
                string diffReason = tbDiffReason.Text.Trim() != string.Empty ? tbDiffReason.Text.Trim() : string.Empty;
                if (diffReason != string.Empty)
                {
                    cycleCountResultList[gvr.RowIndex].DiffReason = diffReason;
                }
            }
            TheCycleCountResultMgr.UpdateCycleCountResult(cycleCountResultList.Where(c => c.DiffReason != null).ToList<CycleCountResult>());
            TheCycleCountMgr.ProcessCycleCountResult(this.code, this.CurrentUser);
            this.UpdateView();
            ShowSuccessMessage("Common.Business.Result.Update.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CycleCountResult cycleCountResult = (CycleCountResult)e.Row.DataItem;
            ((TextBox)e.Row.FindControl("tbDiffReason")).ReadOnly = (cycleCountResult.CycleCount.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    private void BindData()
    {
        IList<CycleCountResult> cycleCountResultList = TheCycleCountResultMgr.GetCycleCountResult(this.code);
        BindData(cycleCountResultList);
    }
    private void BindData(IList<CycleCountResult> cycleCountResultList)
    {
        this.GV_List.DataSource = cycleCountResultList;
        this.GV_List.DataBind();
    }
}
