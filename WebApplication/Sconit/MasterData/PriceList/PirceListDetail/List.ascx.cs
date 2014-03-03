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

public partial class MasterData_PriceList_PriceListDetail_List : ListModuleBase
{
   
    public event EventHandler EditEvent;
 

    protected string code
    {
        get
        {
            return (string)ViewState["code"];
        }
        set
        {
            ViewState["code"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow && code == null)
        //{
        //    code = (string)(DataBinder.Eval(e.Row.DataItem, "PriceList.Code"));
        //    this.ltlTitle.Text = code;
        //}
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string code = ((LinkButton)sender).CommandArgument;
            EditEvent(code, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        try
        {
            ThePriceListDetailMgr.DeletePriceListDetail(Convert.ToInt32(code));
            ShowSuccessMessage("Common.Business.Result.Delete.Successfully");
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("Common.Business.Result.Delete.Failed");
        }
    }

 
}
