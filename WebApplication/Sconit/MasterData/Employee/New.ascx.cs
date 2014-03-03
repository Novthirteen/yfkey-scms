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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Control;

public partial class MasterData_Employee_New : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;
    public event EventHandler NewEvent;

    private Employee employee;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void FV_Employee_OnDataBinding(object sender, EventArgs e)
    {

    }

    public void PageCleanup()
    {
        ((CheckBox)(this.FV_Employee.FindControl("tbIsActive"))).Checked = true;
        ((TextBox)(this.FV_Employee.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Employee.FindControl("tbName"))).Text = string.Empty;
        ((TextBox)(this.FV_Employee.FindControl("tbDepartment"))).Text = string.Empty;
        ((TextBox)(this.FV_Employee.FindControl("tbWorkGroup"))).Text = string.Empty;
        ((TextBox)(this.FV_Employee.FindControl("tbPost"))).Text = string.Empty;
        ((TextBox)(this.FV_Employee.FindControl("tbMemo"))).Text = string.Empty;
    }

    protected void ODS_Employee_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        employee = (Employee)e.InputParameters[0];
        if (employee != null)
        {
            employee.Code = employee.Code.Trim();
            employee.Name = employee.Name.Trim();
            employee.Department = employee.Department.Trim();
            employee.Memo = employee.Memo.Trim();
            employee.Post = employee.Post.Trim();
            employee.WorkGroup = employee.WorkGroup.Trim();
            employee.LastModifyDate = DateTime.Now;
            //employee.LastModifyUser = this.CurrentUser;
            employee.Gender = ((CodeMstrDropDownList)(this.FV_Employee.FindControl("ddlGender"))).SelectedValue;
        }
    }

    protected void ODS_Employee_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(employee.Code, e);
            ShowSuccessMessage("MasterData.Employee.AddEmployee.Successfully", employee.Code);
        }
    }

    protected void checkEmployee(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvInsert":
                if (TheEmployeeMgr.LoadEmployee(args.Value) != null)
                {
                    ShowErrorMessage("MasterData.Employee.CodeExist", args.Value);
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }
}
