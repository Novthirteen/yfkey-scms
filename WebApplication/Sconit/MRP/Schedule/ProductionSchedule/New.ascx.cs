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
using com.Sconit.Utility;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.View;
using System.Text;
using com.Sconit.Entity.MRP;

public partial class MRP_Schedule_ProductionSchedule_New : EditModuleBase
{
    public event EventHandler backClickEvent;

    private CustomerSchedule customerSchedule
    {
        get { return (CustomerSchedule)ViewState["CustomerSchedule"]; }
        set { ViewState["CustomerSchedule"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
    }



    public void InitPageParameter(int customerScheduleId)
    {
        if (customerScheduleId > 0)
        {
            this.customerSchedule = TheCustomerScheduleMgr.LoadCustomerSchedule(customerScheduleId, true);

            this.tbFlow1.Text = customerSchedule.Flow;
            this.tbFlow1.Visible = true;
            this.tbFlow.Visible = false;

            this.tbScheduleNo.Text = customerSchedule.ReferenceScheduleNo;
            this.tbScheduleNo.ReadOnly = true;
            this.trTemplate.Visible = false;

        }
        else
        {
            this.customerSchedule = null;


            this.tbFlow.Visible = true;
            this.tbFlow.Text = string.Empty;
            this.tbFlow1.Visible = false;

            this.tbScheduleNo.ReadOnly = false;
            this.tbScheduleNo.Text = string.Empty;


            this.trTemplate.Visible = true;



        }
        this.GVDataBind();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.tbScheduleNo.Text.Trim() == string.Empty)
            {
                this.rfvScheduleNo.IsValid = false;
                return;
            }

            string flowCode = this.tbFlow.Text.Trim() != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
            IList<CustomerSchedule> customerScheduleList = TheImportMgr.ReadCustomerScheduleFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser,
                 null, null, flowCode, this.tbScheduleNo.Text.Trim(), false);
            if (customerScheduleList.Count > 0)
            {
                foreach (CustomerSchedule c in customerScheduleList)
                {
                    c.CreateDate = DateTime.Now;
                    c.CreateUser = this.CurrentUser.Code;
                    c.LastModifyDate = DateTime.Now;
                    c.LastModifyUser = this.CurrentUser.Code;
                    c.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                    TheCustomerScheduleMgr.CreateCustomerSchedule(c);
                    TheCustomerScheduleMgr.ReleaseCustomerSchedule(c.Id, this.CurrentUser.Code);
                }
                ShowSuccessMessage("MRP.Schedule.Create.Successfully");

                if (this.backClickEvent != null)
                {
                    this.backClickEvent(this, e);
                }
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (this.customerSchedule.Id > 0)
            {
                this.customerSchedule.LastModifyDate = DateTime.Now;
                this.customerSchedule.LastModifyUser = this.CurrentUser.Code;

                foreach (GridViewRow gvr in this.GV_List_Detail.Rows)
                {
                    TextBox tbQty = (TextBox)gvr.FindControl("tbQty");
                    decimal qty = decimal.Parse(tbQty.Text.Trim());
                    this.customerSchedule.CustomerScheduleDetails[gvr.RowIndex].Qty = qty;
                }

                TheCustomerScheduleMgr.UpdateCustomerSchedule(this.customerSchedule);
                ShowSuccessMessage("MRP.Schedule.Update.Successfully");
            }
            else
            {

                this.customerSchedule.CreateDate = DateTime.Now;
                this.customerSchedule.CreateUser = this.CurrentUser.Code;
                this.customerSchedule.LastModifyDate = DateTime.Now;
                this.customerSchedule.LastModifyUser = this.CurrentUser.Code;
                this.customerSchedule.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;

                foreach (GridViewRow gvr in this.GV_List_Detail.Rows)
                {
                    TextBox tbQty = (TextBox)gvr.FindControl("tbQty");
                    decimal qty = decimal.Parse(tbQty.Text.Trim());
                    this.customerSchedule.CustomerScheduleDetails[gvr.RowIndex].Qty = qty;
                }

                TheCustomerScheduleMgr.CreateCustomerSchedule(this.customerSchedule);
                ShowSuccessMessage("MRP.Schedule.Create.Successfully");
            }

            this.InitPageParameter(customerSchedule.Id);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.backClickEvent != null)
        {
            this.backClickEvent(this, e);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            CustomerSchedule customerSchedule = TheCustomerScheduleMgr.LoadCustomerSchedule(this.customerSchedule.Id, true);
            TheCustomerScheduleMgr.DeleteCustomerSchedule(customerSchedule);
            this.backClickEvent(this, e);
            ShowSuccessMessage("MRP.Schedule.Delete.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
        catch (Exception)
        {
            ShowErrorMessage("MRP.Schedule.Delete.Fail");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            TheCustomerScheduleMgr.CancelCustomerSchedule(this.customerSchedule.Id, this.CurrentUser.Code);
            this.backClickEvent(this, e);
            ShowSuccessMessage("MRP.Schedule.Cancel.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
        catch (Exception)
        {
            ShowErrorMessage("MRP.Schedule.Cancel.Fail");
        }
    }

    protected void btnRelease_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.customerSchedule != null && this.customerSchedule.Id > 0)
            {
                TheCustomerScheduleMgr.ReleaseCustomerSchedule(this.customerSchedule.Id, this.CurrentUser.Code);
                this.customerSchedule = TheCustomerScheduleMgr.LoadCustomerSchedule(this.customerSchedule.Id, true);
                ShowSuccessMessage("MRP.Schedule.Release.Successfully");
                this.backClickEvent(this, e);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    protected void GV_List_Detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (this.customerSchedule.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE
                || this.customerSchedule.Id == 0)
            {
                ((TextBox)(e.Row.FindControl("tbQty"))).Enabled = true;
            }
            else
            {
                ((TextBox)(e.Row.FindControl("tbQty"))).Enabled = false;
            }
        }
    }

    private void GVDataBind()
    {
        if (this.customerSchedule != null)
        {
            if (this.customerSchedule.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                this.btnDelete.Visible = true;
                this.btnRelease.Visible = true;
                this.btnSave.Visible = true;
                this.btnCancel.Visible = false;
                this.btnImport.Visible = false;
            }
            else if (this.customerSchedule.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
            {
                this.btnDelete.Visible = false;
                this.btnRelease.Visible = false;
                this.btnSave.Visible = false;
                this.btnCancel.Visible = true;
                this.btnImport.Visible = false;
            }
            else if (this.customerSchedule.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)
            {
                this.btnDelete.Visible = false;
                this.btnRelease.Visible = false;
                this.btnSave.Visible = false;
                this.btnCancel.Visible = false;
                this.btnImport.Visible = false;
            }
            else
            {
                this.btnDelete.Visible = false;
                this.btnRelease.Visible = false;
                this.btnSave.Visible = true;
                this.btnCancel.Visible = false;
                this.btnImport.Visible = true;
            }
        }
        else
        {
            this.customerSchedule = new CustomerSchedule();
            this.customerSchedule.CustomerScheduleDetails = new List<CustomerScheduleDetail>();
            this.btnSave.Visible = true;
            this.btnImport.Visible = true;
        }
        this.GV_List_Detail.DataSource = this.customerSchedule.CustomerScheduleDetails;
        this.GV_List_Detail.Visible = true;
        this.GV_List_Detail.DataBind();
        this.ltllegend.Text = customerSchedule.ReferenceScheduleNo;
        this.div_List_Detail.Visible = this.customerSchedule.CustomerScheduleDetails.Count > 0;
        if (customerSchedule.CustomerScheduleDetails.Count > 500)
        {
            ShowWarningMessage("Common.ListCount.Warning.GreatThan500");
        }
    }
}
