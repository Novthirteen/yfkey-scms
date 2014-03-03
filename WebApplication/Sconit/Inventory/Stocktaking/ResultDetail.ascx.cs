using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using System.Collections;
using com.Sconit.Entity.Exception;

public partial class Inventory_Stocktaking_ResultDetail : ListModuleBase
{

    public override void UpdateView()
    {
        //this.GV_List.Execute();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public void UpdateView(IList<CycleCountResult> cycleCountResultList, bool isShortage, bool isProfit, bool isEqual)
    {
        this.GV_List.DataSource = cycleCountResultList;
        this.GV_List.DataBind();

        if (isShortage)
        {
            this.GV_List.Columns[7].Visible = false;
            this.GV_List.Columns[8].Visible = true;
        }

        if (isProfit)
        {
            this.GV_List.Columns[7].Visible = true;
            this.GV_List.Columns[8].Visible = false;
        }

        if (isEqual)
        {
            this.GV_List.Columns[7].Visible = false;
            this.GV_List.Columns[8].Visible = true;
        }

        if (cycleCountResultList.Count > 0 && cycleCountResultList[0].CycleCount != null && cycleCountResultList[0].CycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
        {
            this.GV_List.Columns[0].Visible = true;
            this.btnAdjust.Visible = true;
        }
        else
        {
            this.GV_List.Columns[0].Visible = false;
            this.btnAdjust.Visible = false;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox checkBoxGroup = e.Row.FindControl("CheckBoxGroup") as CheckBox;
            CycleCountResult cycleCountResult = (CycleCountResult)e.Row.DataItem;
            if (cycleCountResult.CycleCount != null && cycleCountResult.CycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
            {
                checkBoxGroup.Visible = cycleCountResult.IsProcessed == null;
            }
        }
    }

    protected void btnAdjust_Click(object sender, EventArgs e)
    {
        try
        {
            IList<int> cycleCountResultIdList = new List<int>();
            for (int i = 0; i < this.GV_List.Rows.Count; i++)
            {
                GridViewRow row = this.GV_List.Rows[i];
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfId = (HiddenField)row.FindControl("hfId");
                    cycleCountResultIdList.Add(Int32.Parse(hfId.Value));
                }

            }
            if (cycleCountResultIdList.Count == 0)
            {
                ShowErrorMessage("Common.Message.Record.Not.Select");
                return;
            }

            TheCycleCountMgr.ProcessCycleCountResult(cycleCountResultIdList, this.CurrentUser);

            ShowSuccessMessage("Common.Business.Result.Adjust.Successfully");
            this.Visible = false;
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    public void CheckAllChanged(object sender, EventArgs e)
    {
        CheckBox cbCheckAll = (CheckBox)sender;
        for (int i = 0; i < this.GV_List.Rows.Count; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];
            CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
            checkBoxGroup.Checked = cbCheckAll.Checked;

        }
    }
}