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
using com.Sconit.Entity.MasterData;
using com.Sconit.Control;

public partial class MasterData_Employee_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string EmployeeCode
    {
        get
        {
            return (string)ViewState["EmployeeCode"];
        }
        set
        {
            ViewState["EmployeeCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        
    }

    protected void FV_Employee_DataBound(object sender, EventArgs e)
    {
        if (EmployeeCode != null && EmployeeCode != string.Empty)
        {
            Employee employee = (Employee)((FormView)sender).DataItem;
            CodeMstrDropDownList ddlGender = (CodeMstrDropDownList)this.FV_Employee.FindControl("ddlGender");
            if (employee.Gender != null)
            {
                ddlGender.Text = employee.Gender;
            }
        }
    }

    public void InitPageParameter(string code)
    {
        this.EmployeeCode = code;
        this.ODS_Employee.SelectParameters["code"].DefaultValue = this.EmployeeCode;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Employee_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.Employee.UpdateEmployee.Successfully", EmployeeCode);
        btnBack_Click(this, e);
    }

    protected void ODS_Employee_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Employee employee = (Employee)e.InputParameters[0];
        if (employee != null)
        {
            CodeMstrDropDownList ddlGender = (CodeMstrDropDownList)this.FV_Employee.FindControl("ddlGender");
            employee.Gender = ddlGender.SelectedValue;
            employee.Name = employee.Name.Trim();
            employee.Department = employee.Department.Trim();
            employee.Memo = employee.Memo.Trim();
            employee.WorkGroup = employee.WorkGroup.Trim();
            employee.Post = employee.Post.Trim();
            employee.LastModifyDate = DateTime.Now;
            //employee.LastModifyUser = this.CurrentUser;
        }
    }

    protected void ODS_Employee_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Employee.DeleteEmployee.Successfully", EmployeeCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Employee.DeleteEmployee.Fail", EmployeeCode);
            e.ExceptionHandled = true;
        }
    }
}
