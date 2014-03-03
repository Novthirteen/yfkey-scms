using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using NHibernate.Expression;

public partial class Finance_PlanBill_Main : MainModuleBase
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

    public DetachedCriteria Criteria
    {
        get
        {
            return (DetachedCriteria)ViewState["Criteria"];
        }
        set
        {
            ViewState["Criteria"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucSearch.SearchEvent += new EventHandler(Search_Render);
        this.ucSearch.ConfirmEvent += new EventHandler(Confirm_Render);

        if (!IsPostBack)
        {
            if (this.ModuleParameter.ContainsKey("ModuleType"))
            {
                this.ModuleType = this.ModuleParameter["ModuleType"];
            }
            this.ucSearch.ModuleType = this.ModuleType;
            this.ucList.ModuleType = this.ModuleType;
        }
    }

    void Search_Render(object sender, EventArgs e)
    {

        this.ucList.Visible = true;
        this.Criteria = (DetachedCriteria)sender;
        this.ucList.BindDataSource(this.Criteria);
    }

    void Confirm_Render(object sender, EventArgs e)
    {

        IList<PlannedBill> plannedBillList = this.ucList.PopulateSelectedData();
        if (plannedBillList == null || plannedBillList.Count == 0)
        {
            ShowErrorMessage("MasterData.PlannedBill.Empty");
        }

        try
        {
            TheBillMgr.ManualCreateActingBill(plannedBillList, this.CurrentUser);
            ShowSuccessMessage("MasterData.PlannedBill.Create.Successfully");
            this.ucList.BindDataSource(this.Criteria);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    protected void ucSearch_MatchClick(object sender, EventArgs e)
    {
        this.ucMatch.Visible = true;
        this.ucMatch.InitPageParameter((DetachedCriteria)sender);
    }

    protected void ucMatch_Saved(object sender, EventArgs e)
    {
        this.ucList.Visible = true;
        this.ucList.BindDataSource((IList<PlannedBill>)sender);
    }

}
