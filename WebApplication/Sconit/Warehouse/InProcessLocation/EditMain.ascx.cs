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
using com.Sconit.Entity.Distribution;
using System.Collections.Generic;
using com.Sconit.Entity.Production;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Procurement;

public partial class Warehouse_InProcessLocation_EditMain : MainModuleBase
{
    public event EventHandler RefreshListEvent;
    public event EventHandler BackEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }
    private string Action
    {
        get { return (string)ViewState["Action"]; }
        set { ViewState["Action"] = value; }
    }
    private string IpNo
    {
        get { return (string)ViewState["IpNo"]; }
        set { ViewState["IpNo"] = value; }
    }


    public void InitPageParameter(string ipNo, string action)
    {
        this.IpNo = ipNo;
        this.Action = action;
        this.ucEdit.Action = action;
        this.ucEdit.InitPageParameter(ipNo);
        this.ucDetailList.InitPageParameter(ipNo);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucDetailList.ReceiveEvent += new System.EventHandler(this.ReceiveRender);
        this.ucDetailList.BackEvent += new EventHandler(this.DetailListBack_Render);
        this.ucViewMain.BackEvent += new EventHandler(this.ViewMainBack_Render);

        if (!IsPostBack)
        {
            this.ucDetailList.ModuleType = this.ModuleType;
        }
    }

    private void ReceiveRender(object sender, EventArgs e)
    {
        this.ucViewMain.Visible = true;
        this.ucViewMain.InitPageParameter((string)((object[])sender)[0], (bool)((object[])sender)[1]);
    }

    void DetailListBack_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, null);
        }
    }

    void ViewMainBack_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, null);
        }
    }

}
