using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.View;
using com.Sconit.Entity.MRP;
using System.Reflection;
using NHibernate.Expression;



public partial class MRP_Schedule_DemandSchedule_Main : MainModuleBase
{
   

  

    protected void Page_Load(object sender, EventArgs e)
    {

       
        if (!IsPostBack)
        {
            if (this.Action == "ViewActQty")
            {
                ActQty_Render(new string[] { this.ActionParameter["ItemCode"], this.ActionParameter["Date"], this.ActionParameter["EffDate"], this.ActionParameter["IsFlow"], this.ActionParameter["FlowOrLoc"], this.ActionParameter["IsWinTime"] }, e);
            }
            if (this.Action == "ViewRequiredQty")
            {
                RequiredQty_Render(new string[] { this.ActionParameter["ItemCode"], this.ActionParameter["Date"], this.ActionParameter["EffDate"], this.ActionParameter["IsFlow"], this.ActionParameter["FlowOrLoc"], this.ActionParameter["IsWinTime"] }, e);
            }
            
        }
    }

    void ActQty_Render(object sender, EventArgs e)
    {
        string itemCode = (string)((object[])sender)[0];
        DateTime date = DateTime.Parse((string)((object[])sender)[1]);
        DateTime effDate = DateTime.Parse((string)((object[])sender)[2]);
        bool isFlow = bool.Parse((string)((object[])sender)[3]);
        string flowOrLoc = ((string)((object[])sender)[4]).Replace('/', '-').Replace(')', '_');
        bool isWinTime = bool.Parse((string)((object[])sender)[5]);
        this.ucActQty.Visible = true;
        this.ucRequiredQty.Visible = false;
        this.ucActQty.InitParamater(itemCode, date,effDate, isFlow, flowOrLoc, isWinTime);
    }

    void RequiredQty_Render(object sender, EventArgs e)
    {
        string itemCode = (string)((object[])sender)[0];
        DateTime date = DateTime.Parse((string)((object[])sender)[1]);
        DateTime effDate = DateTime.Parse((string)((object[])sender)[2]);
        bool isFlow = bool.Parse((string)((object[])sender)[3]);
        string flowOrLoc = ((string)((object[])sender)[4]).Replace('/', '-').Replace(')', '_');
        bool isWinTime = bool.Parse((string)((object[])sender)[5]);

        this.ucActQty.Visible = false;
        this.ucRequiredQty.Visible = true;
        this.ucRequiredQty.InitParamater(itemCode, date, effDate, isFlow, flowOrLoc, isWinTime);
    }

 
}

