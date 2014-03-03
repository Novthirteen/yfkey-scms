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
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;

public partial class MasterData_Flow_List : ListModuleBase
{
    public EventHandler EditEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    public override void UpdateView()
    {

        //this.GV_List.Columns[3].HeaderText = FlowHelper.GetFlowPartyFromLabel(this.ModuleType);
        //this.GV_List.Columns[4].HeaderText = FlowHelper.GetFlowPartyToLabel(this.ModuleType);
        //this.GV_List.Columns[7].HeaderText = FlowHelper.GetFlowStrategyLabel(this.ModuleType);
        //if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        //{
        //    this.GV_List.Columns[4].Visible = false;
            
        //}
        this.GV_List.Execute();
    }


    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string flowCode = ((LinkButton)sender).CommandArgument;
            EditEvent(flowCode, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string flowCode = ((LinkButton)sender).CommandArgument;
        try
        {
            TheFlowMgr.DeleteFlow(flowCode);
            ShowSuccessMessage("MasterData.Flow.DeleteFlow.Successfully", flowCode);
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Flow.DeleteFlow.Failed", flowCode);
        }
    }
}
