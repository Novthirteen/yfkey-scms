﻿using System;
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
using com.Sconit.Entity;
using NHibernate.Expression;

public partial class Procurement_RollingForecast_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ModuleParameter.ContainsKey("IsSupplier"))
        {
            if (bool.Parse(this.ModuleParameter["IsSupplier"]) == true)
            {
                this.ucTabNavigator.Visible = false;
            }
        }
    }

    protected void lbManual_Click(object sender, EventArgs e)
    {
        this.ucEditMain.Visible = true;
        this.ucImport.Visible = false;
    }

    protected void lbImport_Click(object sender, EventArgs e)
    {
        this.ucEditMain.Visible = false;
        this.ucImport.Visible = true;
    }

    protected void ucImport_BtnImportClick(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbManual_Click(null, null);
    }

}
