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
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;
using NHibernate.Expression;

public partial class MasterData_FlowBinding_Main : ModuleBase
{
    public event EventHandler BackEvent;

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

    public void InitPageParameter(string flowCode)
    {
        this.FlowCode = flowCode;
        UpdateView();
    }

    public void UpdateView()
    {
        this.ucListBinding.Visible = true;
        this.ucListBinded.Visible = true;
        this.ucButton.Visible = true;
        this.ucNew.Visible = false;
        this.ucEdit.Visible = false;

        DetachedCriteria selectBindingCriteria = DetachedCriteria.For(typeof(FlowBinding));
        DetachedCriteria selectBindingCountCriteria = DetachedCriteria.For(typeof(FlowBinding))
            .SetProjection(Projections.Count("Id"));

        selectBindingCriteria.Add(Expression.Eq("MasterFlow.Code", this.FlowCode));
        selectBindingCountCriteria.Add(Expression.Eq("MasterFlow.Code", this.FlowCode));

        this.ucListBinding.SetSearchCriteria(selectBindingCriteria, selectBindingCountCriteria);
        this.ucListBinding.UpdateView();

        DetachedCriteria selectBindedCriteria = DetachedCriteria.For(typeof(FlowBinding));
        DetachedCriteria selectBindedCountCriteria = DetachedCriteria.For(typeof(FlowBinding))
            .SetProjection(Projections.Count("Id"));

        selectBindedCriteria.Add(Expression.Eq("SlaveFlow.Code", this.FlowCode));
        selectBindedCountCriteria.Add(Expression.Eq("SlaveFlow.Code", this.FlowCode));

        this.ucListBinded.SetSearchCriteria(selectBindedCriteria, selectBindedCountCriteria);
        this.ucListBinded.UpdateView();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucListBinding.ModuleType = this.ModuleType;
        this.ucListBinded.ModuleType = this.ModuleType;
        this.ucNew.ModuleType = this.ModuleType;
        this.ucEdit.ModuleType = this.ModuleType;

        this.ucListBinding.ListEditEvent += new System.EventHandler(this.Edit_Render);
        this.ucButton.NewEvent += new System.EventHandler(this.New_Render);
        this.ucButton.BackEvent += new System.EventHandler(this.BackFlow_Render);
        this.ucNew.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucEdit.BackEvent += new System.EventHandler(this.Back_Render);
    }

    void BackFlow_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    void Back_Render(object sender, EventArgs e)
    {
        UpdateView();
    }

    void New_Render(object sender, EventArgs e)
    {
        this.ucListBinding.Visible = false;
        this.ucListBinded.Visible = false;
        this.ucButton.Visible = false;
        this.ucNew.Visible = true;
        this.ucEdit.Visible = false;
        this.ucNew.FlowCode = this.FlowCode;
        this.ucNew.PageCleanup();
    }

    void Edit_Render(object sender, EventArgs e)
    {
        this.ucListBinding.Visible = false;
        this.ucListBinded.Visible = false;
        this.ucButton.Visible = false;
        this.ucNew.Visible = false;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter((Int32)sender);
      
    }
}
