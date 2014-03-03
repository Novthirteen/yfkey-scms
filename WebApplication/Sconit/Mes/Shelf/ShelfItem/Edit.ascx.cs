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
using com.Sconit.Entity;
using com.Sconit.Entity.Mes;

public partial class Mes_Shelf_ShelfItem_Edit : EditModuleBase
{
   
    public event EventHandler BackEvent;

    protected string ShelfCode
    {
        get
        {
            return (string)ViewState["ShelfCode"];
        }
        set
        {
            ViewState["ShelfCode"] = value;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string shelfCode)
    {
        this.ShelfCode = shelfCode;
        this.ODS_ShelfItem.SelectParameters["Id"].DefaultValue = shelfCode;
        this.ODS_ShelfItem.DeleteParameters["Id"].DefaultValue = shelfCode;
        this.UpdateView();
    }

    protected void FV_ShelfItem_DataBound(object sender, EventArgs e)
    {
        this.UpdateView();
    }

    private void UpdateView()
    {
        

    }

    protected void ODS_ShelfItem_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string shelfCode = ((TextBox)(this.FV_ShelfItem.FindControl("tbShelf"))).Text.Trim();
        string itemCode = ((TextBox)(this.FV_ShelfItem.FindControl("tbItem"))).Text.Trim();
       

        ShelfItem shelfItem = (ShelfItem)e.InputParameters[0];
        if (shelfItem != null)
        {
            shelfItem.Shelf = TheShelfMgr.LoadShelf(shelfCode);
            shelfItem.Item = TheItemMgr.LoadItem(itemCode);
        }
    }

    protected void ODS_ShelfItem_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        btnBack_Click(this, e);
        ShowSuccessMessage("Common.Business.Result.Update.Successfully");
    }

    protected void ODS_ShelfItem_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Common.Business.Result.Delete.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Common.Business.Result.Delete.Failed");
            e.ExceptionHandled = true;
        }
    }
}
