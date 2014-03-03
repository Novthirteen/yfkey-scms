using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using NHibernate.Expression;

public partial class Inventory_Stocktaking_Result : ListModuleBase
{
    public string Code
    {
        get { return (string)ViewState["Code"]; }
        set { ViewState["Code"] = value; }
    }

    public string Status
    {
        get { return (string)ViewState["Status"]; }
        set { ViewState["Status"] = value; }
    }

    public bool isExport
    {
        get { return ViewState["isExport"] == null ? false : (bool)ViewState["isExport"]; }
        set { ViewState["isExport"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Code != null && Code.Length > 0)
        {
            UpdateView();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BoundGridView();
        Export();
    }

    public void Export()
    {
        this.isExport = true;
        GV_List.Columns[0].Visible = false;
        this.ExportXLS(GV_List);
    }

    public override void UpdateView()
    {
        CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(Code);
        this.Status = cycleCount.Status;
        if (cycleCount.IsScanHu)
        {
            tbBinCode.ServiceParameter = "string:" + cycleCount.Location.Code;
            tbBinCode.DataBind();
            tbBinCode.Visible = true;
            lblBinCode.Visible = true;
            btnExportDetail.Visible = true;
            this.GV_List.Columns[6].Visible = true;
            this.GV_List.Columns[7].Visible = true;
            this.GV_List.Columns[8].Visible = true;
            this.GV_List.Columns[9].Visible = false;
            this.GV_List.Columns[10].Visible = false;
            this.GV_List.Columns[11].Visible = false;
        }
        else
        {
            this.GV_List.Columns[2].Visible = false;
            this.GV_List.Columns[6].Visible = false;
            this.GV_List.Columns[7].Visible = false;
            this.GV_List.Columns[8].Visible = false;
            this.GV_List.Columns[9].Visible = true;
            this.GV_List.Columns[10].Visible = true;
            this.GV_List.Columns[11].Visible = true;
            tbBinCode.Visible = false;
            lblBinCode.Visible = false;
            btnExportDetail.Visible = false;
        }

        this.GV_List.Columns[0].Visible = cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE;

        if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
        {
            this.btnAdjust.Visible = true;
            this.btnClose.Visible = true;
            //this.GV_List.Columns[0].Visible = true;

        }
        else
        {
            this.btnAdjust.Visible = false;
            this.btnClose.Visible = false;
           // this.GV_List.Columns[0].Visible = false;
        }

        this.isExport = false;
    }

    protected void lbtnShortageQty_Click(object sender, EventArgs e)
    {
        OnDetailClick(((LinkButton)sender).CommandArgument, true, false, false);
    }
    protected void lbtnProfitQty_Click(object sender, EventArgs e)
    {
        OnDetailClick(((LinkButton)sender).CommandArgument, false, true, false);
    }
    protected void lbtnEqualQty_Click(object sender, EventArgs e)
    {
        OnDetailClick(((LinkButton)sender).CommandArgument, false, false, true);
    }

    private void OnDetailClick(string binItem, bool isShortage, bool isProfit, bool isEqual)
    {
        string[] argument = binItem.Split('|');

        IList<string> binList = new List<string>();
        IList<string> itemList = new List<string>();
        if (argument[0].Trim() != string.Empty)
        {
            binList.Add(argument[0]);
        }

        if (argument[1].Trim() != string.Empty)
        {
            itemList.Add(argument[1]);
        }

        IList<CycleCountResult> cycleCountResultList = TheCycleCountMgr.ListCycleCountResultDetail(this.Code, isShortage, isProfit, isEqual, binList, itemList, binList.Count == 0);

        this.ucDetail.UpdateView(cycleCountResultList, isShortage, isProfit, isEqual);
        this.ucDetail.Visible = true;
        //this.ucDetail.UpdateView();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            TheCycleCountMgr.ManualCloseCycleCount(Code, this.CurrentUser);
            UpdateView();
            ShowSuccessMessage("Common.Business.Result.Close.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnAdjust_Click(object sender, EventArgs e)
    {
        try
        {
            CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(Code);

            IList<int> cycleCountResultIdList = new List<int>();
            for (int i = 0; i < this.GV_List.Rows.Count; i++)
            {
                GridViewRow row = this.GV_List.Rows[i];
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    if (cycleCount.IsScanHu)
                    {
                        Label lblStorageBin = (Label)row.FindControl("lblStorageBin");
                        Label lblItemCode = (Label)row.FindControl("lblItemCode");

                        IList<string> binList = new List<string>();
                        IList<string> itemList = new List<string>();
                        if (lblStorageBin.Text.Trim() != string.Empty)
                        {
                            binList.Add(lblStorageBin.Text.Trim());
                        }

                        if (lblItemCode.Text.Trim() != string.Empty)
                        {
                            itemList.Add(lblItemCode.Text.Trim());
                        }

                        IList<CycleCountResult> cycleCountResultList = TheCycleCountMgr.ListCycleCountResultDetail(this.Code, true, true, false, binList, itemList, binList.Count == 0);

                        foreach (CycleCountResult cycleCountResult in cycleCountResultList)
                        {
                            cycleCountResultIdList.Add(cycleCountResult.Id);
                        }
                    }
                    else
                    {
                        HiddenField hfId = (HiddenField)row.FindControl("hfId");
                        cycleCountResultIdList.Add(Int32.Parse(hfId.Value));
                    }
                }

            }
            if (cycleCountResultIdList.Count == 0)
            {
                ShowErrorMessage("Common.Message.Record.Not.Select");
                return;
            }
            TheCycleCountMgr.ProcessCycleCountResult(cycleCountResultIdList, this.CurrentUser);
            UpdateView();
            DoSearch();
            ShowSuccessMessage("Common.Business.Result.Adjust.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    private void DoSearch()
    {
        try
        {
            BoundGridView();

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
            CheckBox checkBoxGroup = e.Row.FindControl("CheckBoxGroup") as CheckBox;

            if (this.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
            {
                if (cycleCountResult.CycleCount != null && !cycleCountResult.CycleCount.IsScanHu)
                {
                    checkBoxGroup.Visible = cycleCountResult.IsProcessed == null;
                }
                else
                {
                    //checkBoxGroup.Visible = cycleCountResult.ShortageQty > 0 || cycleCountResult.ProfitQty > 0;
                   // IList<CycleCountResult> cycleCountResultList = TheCycleCountMgr.ListCycleCountResultDetail(this.Code, this.cbShortage.Checked, this.cbProfit.Checked, false, cycleCountResult.StorageBin, cycleCountResult.Item.Code);
                   // var qCnt = cycleCountResultList.Where(c => c.IsProcessed == null).Count();
                   // checkBoxGroup.Visible = qCnt != 0;
                    //checkBoxGroup.Visible = true;
                }
               
            }
            if (isExport)
            {
                e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[6].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[7].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[8].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[9].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[10].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[11].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

                this.SetLinkButton(e.Row, "lblShortageQty", cycleCountResult.ShortageQty != null && cycleCountResult.ShortageQty != 0);
                this.SetLinkButton(e.Row, "lblProfitQty", cycleCountResult.ProfitQty != null && cycleCountResult.ProfitQty != 0);
                this.SetLinkButton(e.Row, "lblEqualQty", cycleCountResult.EqualQty != null && cycleCountResult.EqualQty != 0);
            }
        }

    }


    protected void btnExportDetail_Click(object sender, EventArgs e)
    {
        try
        {
            bool listShortage = this.cbShortage.Checked;
            bool listProfit = this.cbProfit.Checked;
            bool listEqual = this.cbEqual.Checked;

            IList<string> binList = new List<string>();
            IList<string> itemList = new List<string>();

            if (this.tbBinCode.Text.Trim() != string.Empty)
            {
                binList.Add(this.tbBinCode.Text.Trim());
            }

            if (this.tbItemCode.Text.Trim() != string.Empty)
            {
                itemList.Add(this.tbItemCode.Text.Trim());
            }

            IList<CycleCountResult> cycleCountResultList =
                this.TheCycleCountMgr.ListCycleCountResultDetail(this.Code, listShortage, listProfit, listEqual, binList, itemList, binList.Count == 0);


            this.isExport = true;

            this.GVResultDetail.DataSource = cycleCountResultList;
            this.GVResultDetail.DataBind();

            //UpdateView();
            this.GVResultDetail.Visible = true;
            this.ExportXLS(this.GVResultDetail);
            this.GVResultDetail.Visible = false;
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void GV_ResultDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (isExport && e.Row.RowType == DataControlRowType.DataRow)
        {
            CycleCountResult ccr = (CycleCountResult)e.Row.DataItem;
            Label lblDiffQty = (Label)e.Row.FindControl("lblDiffQty");
            Label lblQty = (Label)e.Row.FindControl("lblQty");
            if (ccr.DiffQty == 0)
            {
                lblDiffQty.Text = "${MasterData.Inventory.Stocktaking.Equal}";

            }
            else if (ccr.DiffQty > 0)
            {
                lblDiffQty.Text = "${MasterData.Inventory.Stocktaking.Profit}";
            }
            else
            {
                lblDiffQty.Text = "${MasterData.Inventory.Stocktaking.Shortage}";
            }

            lblQty.Text = ccr.Qty > ccr.InvQty ? ccr.Qty.ToString("0.######") : ccr.InvQty.ToString("0.######");

            e.Row.Cells[0].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[2].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[6].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        }
    }

    private void SetLinkButton(GridViewRow gvr, string id, bool enabled)
    {
        LinkButton linkButton = (LinkButton)gvr.FindControl(id);
        linkButton.Enabled = enabled && !isExport;
    }

    private void BoundGridView()
    {
        IList<string> binCodeList = new List<string>();
        if (this.tbBinCode.Text != null && this.tbBinCode.Text.Trim().Length > 0)
        {
            binCodeList.Add(this.tbBinCode.Text);
        }
        IList<string> itemCodeList = new List<string>();
        if (this.tbItemCode.Text != null && this.tbItemCode.Text.Trim().Length > 0)
        {
            itemCodeList.Add(this.tbItemCode.Text);
        }
        IList<CycleCountResult> cycleCountResultList = TheCycleCountMgr.ListCycleCountResult(Code, cbShortage.Checked, this.cbProfit.Checked, cbEqual.Checked, binCodeList, itemCodeList);

        this.GV_List.DataSource = cycleCountResultList;
        this.GV_List.DataBind();

        UpdateView();
    }

    public void clean()
    {
        this.btnAdjust.Visible = false;
        this.btnClose.Visible = false;
        this.tbBinCode.Text = string.Empty;
        this.tbItemCode.Text = string.Empty;
        this.cbEqual.Checked = true;
        this.cbProfit.Checked = true;
        this.cbShortage.Checked = true;
        this.GV_List.DataSource = null;
        this.GV_List.DataBind();
    }

}
