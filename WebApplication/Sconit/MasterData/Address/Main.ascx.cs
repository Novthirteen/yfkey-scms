using System;
using System.Collections;
using System.Collections.Generic;
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
using com.Sconit.Entity;

public partial class MasterData_Address_Main : MainModuleBase
{
    public event EventHandler BackEvent;
    public string PartyCode
    {
        get
        {
            return (string)ViewState["PartyCode"];
        }
        set
        {
            ViewState["PartyCode"] = value;
        }
    }

    public string AddrType
    {
        get
        {
            return (string)ViewState["AddrType"];
        }
        set
        {
            ViewState["AddrType"] = value;
        }
    }

    public void InitPageParameter()
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucNew.Visible = false;
        this.ucEdit.Visible = false;

        IDictionary<string, string> mpDic = new Dictionary<string, string>();
        mpDic.Add("PartyCode",this.PartyCode);
        mpDic.Add("AddrType", this.AddrType);
        this.ucSearch.QuickSearch(mpDic);
    }

    public MasterData_Address_Main()
    {

    }

    //public MasterData_Address_Main(IDictionary<string, string> mpDic, string act, IDictionary<string, string> apDic)
    //{
    //    if (mpDic.ContainsKey("AddrType"))
    //    {
    //        this.AddrType = mpDic["AddrType"];
    //    }
    //    if (mpDic.ContainsKey("PartyCode"))
    //    {
    //        this.PartyCode = mpDic["PartyCode"];
    //    }
    //    this.Action = act;
    //    this.ActionParameter = apDic;
    //}

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.AddrType = this.AddrType;
        this.ucNew.AddrType = this.AddrType;
        this.ucEdit.AddrType = this.AddrType;

        this.ucSearch.SearchEvent += new System.EventHandler(this.Search_Render);
        this.ucSearch.NewEvent += new System.EventHandler(this.New_Render);
        this.ucList.EditEvent += new System.EventHandler(this.ListEdit_Render);
        this.ucNew.BackEvent += new System.EventHandler(this.NewBack_Render);
        this.ucNew.CreateEvent += new System.EventHandler(this.CreateBack_Render);
        this.ucEdit.BackEvent += new System.EventHandler(this.EditBack_Render);
        this.ucSearch.BackEvent += new System.EventHandler(this.Back_Render);        

        if (!IsPostBack)
        {
            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                InitPageParameter();
            }
            if (this.Action == BusinessConstants.PAGE_NEW_ACTION)
            {
                New_Render(this, null);
            }
            if (this.Action == BusinessConstants.PAGE_EDIT_ACTION)
            {
                ListEdit_Render(this.ActionParameter["Code"], null);
            }
        }
    }

    //The event handler when user click button "Search" button
    void Search_Render(object sender, EventArgs e)
    {

        this.ucList.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucList.Visible = true;
        this.ucList.UpdateView();
       
        this.CleanMessage();
    }

    //The event handler when user click button "New" button
    void New_Render(object sender, EventArgs e)
    {
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucNew.Visible = true;      
        this.ucNew.PartyCode = this.PartyCode;
    
        this.ucNew.PageCleanup();
    }

    //The event handler when user click button "Back" button of ucNew
    void NewBack_Render(object sender, EventArgs e)
    {
        this.ucNew.Visible = false;
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

    //The event handler when user click button "Save" button of ucNew
    void CreateBack_Render(object sender, EventArgs e)
    {
        this.ucNew.Visible = false;
        this.ucEdit.Visible = false;
        this.ucEdit.PartyCode = this.PartyCode;
        this.ucEdit.InitPageParameter((string)sender);
        this.ucList.UpdateView();
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
    }

    //The event handler when user click link "Edit" link of ucList
    void ListEdit_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucEdit.PartyCode = this.PartyCode;
        this.ucEdit.InitPageParameter((string)sender);
    }

    //The event handler when user click button "Back" button of ucEdit
    void EditBack_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucSearch.Visible = true;
        this.ucList.Visible = true;
        this.ucList.UpdateView();
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
