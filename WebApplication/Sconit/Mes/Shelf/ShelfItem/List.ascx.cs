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

public partial class Mes_Shelf_ShelfItem_List : ListModuleBase
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
      
    }
   

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string id = ((LinkButton)sender).CommandArgument;
        try
        {
            TheShelfItemMgr.DeleteShelfItem(Int32.Parse(id));
            ShowSuccessMessage("Common.Business.Result.Delete.Successfully");
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("Common.Business.Result.Delete.Failed");
        }
    }

 
}
