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
using NHibernate.Expression;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class Warehouse_CheckASN_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucSearch.BatchEvent += new EventHandler(Batch_Render);
        this.ucEdit.BackEvent += new EventHandler(Back_Render);
        //this.ucList.EditEvent += new System.EventHandler(this.ListEdit_Render);
        //this.ucList.ViewEvent += new System.EventHandler(this.ListView_Render);
        //this.ucList.CloseEvent += new System.EventHandler(this.ListClose_Render);
        //this.ucEditMain.RefreshListEvent += new System.EventHandler(this.RefreshList_Render);
        //this.ucEditMain.BackEvent += new System.EventHandler(this.Back_Render);
        //this.ucViewMain.CloseEvent += new System.EventHandler(this.Back_Render);
    }


    void Search_Render(object sender, EventArgs e)
    {
        
        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.UpdateView();
        this.ucList.Visible = true;
    }

    //The event handler when user click link "Edit" link of ucList
    void Batch_Render(object sender, EventArgs e)
    {
        
        this.ucSearch.Visible = false;
        this.ucList.Visible = false;
        this.ucEdit.Visible = true;
    }

    void Back_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucEdit.Visible = false;
    }
 

    
}
