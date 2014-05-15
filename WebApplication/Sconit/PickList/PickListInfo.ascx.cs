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
using NHibernate.Expression;


public partial class Distribution_PickList_PickListInfo : ModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler DeleteEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucDetailList.ConfirmEvent += new System.EventHandler(this.Confirm_Render);

    }
    void Confirm_Render(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected void tbPickList_TextChanged(Object sender, EventArgs e)
    {
        DoSearch();

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string pickListNo = this.tbPickListNo.Text;
        IList<object> list = new List<object>();
        PickList pickList = ThePickListMgr.LoadPickList(pickListNo, true);
        pickList.PickListDetails = pickList.PickListDetails.OrderBy(pd => pd.PrintLocationCode).ToList();
        list.Add(pickList);
        string printUrl = TheReportMgr.WriteToFile("PickList.xls", list);
        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
    }

    protected void btnShip_Click(object sender, EventArgs e)
    {
        string pickListNo = this.tbPickListNo.Text;
        try
        {
            TheOrderMgr.ShipOrder(pickListNo, this.CurrentUser);
            ShowSuccessMessage("MasterData.PickList.ShipPickList.Successfully", this.tbPickListNo.Text.Trim());
            DoSearch();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        this.ucDetailList.DoPick();

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ThePickListMgr.DeletePickList(this.tbPickListNo.Text.Trim(), this.CurrentUser);
            ShowSuccessMessage("MasterData.PickList.DeletePickList.Successfully", this.tbPickListNo.Text.Trim());
            if (DeleteEvent != null)
            {
                DeleteEvent(sender, e);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }


    protected void btnStart_Click(object sender, EventArgs e)
    {
        try
        {
            ThePickListMgr.StartPickList(this.tbPickListNo.Text.Trim(), this.CurrentUser);
            ShowSuccessMessage("MasterData.PickList.StartPickList.Successfully", this.tbPickListNo.Text.Trim());
            DoSearch();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ThePickListMgr.CancelPickList(this.tbPickListNo.Text.Trim(), this.CurrentUser);
            ShowSuccessMessage("MasterData.PickList.CancelPickList.Successfully", this.tbPickListNo.Text.Trim());
            DoSearch();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            ThePickListMgr.ManualClosePickList(this.tbPickListNo.Text.Trim(), this.CurrentUser);
            ShowSuccessMessage("MasterData.PickList.ClosePickList.Successfully", this.tbPickListNo.Text.Trim());
            DoSearch();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void DoSearch()
    {

        if (this.tbPickListNo.Text != string.Empty)
        {

            string pickListNo = this.tbPickListNo.Text.Trim();
            PickList pickList = ThePickListMgr.LoadPickList(pickListNo, true);
            if (pickList != null)
            {
                this.lbStatus.Text = pickList.Status;
                this.lblStatus.Visible = true;
                if (pickList.PickListDetails != null && pickList.PickListDetails.Count > 0)
                {

                    if (pickList.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
                    {
                        this.ucResultList.Visible = false;
                        this.ucDetailList.InitPageParameter(pickList);
                        this.ucDetailList.Visible = true;
                        this.btnStart.Visible = true;
                        this.btnConfirm.Visible = false;
                        this.btnCancel.Visible = true;
                        this.btnClose.Visible = false;
                        this.btnPrint.Visible = true;
                        this.btnShip.Visible = false;
                    }
                    else if (pickList.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                    {
                        this.ucDetailList.InitPageParameter(pickList);
                        this.ucResultList.InitPageParameter(pickList);
                        this.ucResultList.Visible = false;
                        this.ucDetailList.Visible = true;
                        this.btnStart.Visible = false;
                        this.btnConfirm.Visible = true;
                        this.btnCancel.Visible = false;
                        this.btnClose.Visible = true;
                        this.btnPrint.Visible = true;
                        this.btnShip.Visible = false;
                    }
                    else if (pickList.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
                    {
                        this.ucResultList.InitPageParameter(pickList);
                        this.ucDetailList.Visible = false;
                        this.ucResultList.Visible = true;
                        this.btnStart.Visible = false;
                        this.btnConfirm.Visible = false;
                        this.btnCancel.Visible = false;
                        this.btnClose.Visible = true;
                        this.btnPrint.Visible = false;
                        this.btnShip.Visible = true;

                    }
                    else if (pickList.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
                    {
                        this.ucResultList.InitPageParameter(pickList);
                        this.ucResultList.Visible = true;
                        this.ucDetailList.Visible = false;
                        this.btnConfirm.Visible = false;
                        this.btnCancel.Visible = false;
                        this.btnClose.Visible = false;
                        this.btnPrint.Visible = false;
                        this.btnShip.Visible = false;
                        this.btnStart.Visible = false;
                      
                    }

                    else if (pickList.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)
                    {
                        this.ucResultList.Visible = false;
                        this.ucDetailList.InitPageParameter(pickList);
                        this.ucDetailList.Visible = true;
                        this.btnConfirm.Visible = false;
                        this.btnCancel.Visible = false;
                        this.btnClose.Visible = false;
                        this.btnPrint.Visible = false;
                        this.btnShip.Visible = false;
                        this.btnStart.Visible = false;

                    }
                }
                else
                {
                    this.ucResultList.Visible = false;
                    this.ucDetailList.Visible = false;
                    this.btnConfirm.Visible = false;
                    this.btnCancel.Visible = false;
                    this.btnPrint.Visible = false;
                    this.btnShip.Visible = false;
                  
                }
            }
            else
            {
                this.lblStatus.Visible = false;
                this.lbStatus.Text = string.Empty;
                this.ucDetailList.Visible = false;
                this.btnCancel.Visible = false;
                this.btnPrint.Visible = false;
                this.btnShip.Visible = false;
                ShowErrorMessage("MasterData.PickList.Not.Found", pickListNo);
            }

        }
        else
        {
            ShowErrorMessage("MasterData.No.PickListNo.Input");
        }
    }

    public void InitPageParameter(string pickListNo)
    {
        this.tbPickListNo.Text = pickListNo;
        this.tbPickListNo.ReadOnly = true;
        this.btnBack.Visible = true;
        DoSearch();
    }


}
