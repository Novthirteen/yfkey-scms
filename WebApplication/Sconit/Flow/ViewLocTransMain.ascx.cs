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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;

public partial class MasterData_Flow_ViewLocTransMain : MainModuleBase
{

    public string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }

    public void InitPageParameter()
    {
        this.ucViewLocInTransList.IOType = BusinessConstants.IO_TYPE_IN;
        this.ucViewLocInTransList.FlowCode = this.FlowCode;
        this.ucViewLocInTransList.UpdateView();
       

        this.ucViewLocOutTransList.IOType = BusinessConstants.IO_TYPE_OUT;
        this.ucViewLocOutTransList.FlowCode = this.FlowCode;
        this.ucViewLocOutTransList.UpdateView();

        if (!this.ucViewLocInTransList.Visible && !this.ucViewLocOutTransList.Visible)
        {
            this.Parent.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }




}
