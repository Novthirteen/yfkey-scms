using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using System.Collections;

public partial class Reports_BillAging_Detail : EditModuleBase
{
    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ucDetail.ModuleType = this.ModuleType;
        }
    }

    public void SetSearchCriteria(DetachedCriteria SelectCriteria, DetachedCriteria SelectCountCriteria, IDictionary<string, string> alias)
    {
        this.ucDetail.SetSearchCriteria(SelectCriteria, SelectCountCriteria, alias);
        this.ucDetail.UpdateView();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }
}