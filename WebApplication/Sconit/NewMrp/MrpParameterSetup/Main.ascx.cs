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
using com.Sconit.Entity;
using com.Sconit.Web;
using NHibernate.Expression;

public partial class NewMrp_MrpParameterSetup_Main : MainModuleBase
{                                                                                              

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            var dateType = this.rblDateType.SelectedValue;
            if (dateType == "ShipPlan")
            {
                var customerPlanList = TheMrpMgr.ReadCustomerPlanFromXls(fileUpload.PostedFile.InputStream, dateType, this.CurrentUser);
            }
            else if (dateType == "ProductionPlan")
            {
                var customerPlanList = TheMrpMgr.ReadCustomerPlanFromXls(fileUpload.PostedFile.InputStream, dateType, this.CurrentUser);
            }
            else if (dateType == "PurchasePlan")
            {
                var customerPlanList = TheMrpMgr.ReadCustomerPlanFromXls(fileUpload.PostedFile.InputStream, dateType, this.CurrentUser);
            }
            ShowSuccessMessage("导入成功。");
        }
        catch (com.Sconit.Entity.Exception.BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
        catch (Exception et)
        {
            ShowErrorMessage(et.Message);
        }
    }
}
