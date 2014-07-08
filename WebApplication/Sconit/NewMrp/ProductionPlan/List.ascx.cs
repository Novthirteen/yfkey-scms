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
using com.Sconit.Entity.MRP;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class NewMrp_ProductionPlan_List : ListModuleBase
{
    public EventHandler EditEvent;
    public EventHandler SearchDetailEvent;
    public EventHandler ShowErrorMsgEvent;


    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucShowErrorMsg.BackEvent += new System.EventHandler(this.Back_Render);
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            ProductionPlanMstr m = (ProductionPlanMstr)e.Row.DataItem;
            var runProductionPlanLogs = TheGenericMgr.FindAllWithCustomQuery<RunProductionPlanLog>("select r from RunProductionPlanLog as r where r.BatchNo=?", m.BatchNo);
            if (runProductionPlanLogs == null || runProductionPlanLogs.Count == 0)
            {
                System.Web.UI.WebControls.LinkButton lbtnShowErrorMsg = e.Row.FindControl("lbtnShowErrorMsg") as System.Web.UI.WebControls.LinkButton;
                lbtnShowErrorMsg.Visible = false;
            }

            string searchSql = "select  max(releaseno) from MRP_ProductionPlanMstr ";
            var maxReleaseNos = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
            int releaseNo = 0;
            foreach (System.Data.DataRow row in maxReleaseNos.Rows)
            {
                releaseNo = Convert.ToInt32(row[0]);
            }
            if (releaseNo == m.ReleaseNo)
            {

            }
            else
            {
                System.Web.UI.WebControls.LinkButton lbtRunProdPlan = e.Row.FindControl("lbtRunProdPlan") as System.Web.UI.WebControls.LinkButton;
                lbtRunProdPlan.Visible = false;
            }
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (SearchDetailEvent != null)
        {
            string releaseNo = ((LinkButton)sender).CommandArgument;
            SearchDetailEvent(releaseNo, e);
        }
    }

    protected void lbtnShowErrorMsg_Click(object sender, EventArgs e)
    {
        string batchNo = ((LinkButton)sender).CommandArgument;
        var runProductionPlanLogs = TheGenericMgr.FindAllWithCustomQuery<RunProductionPlanLog>("select r from RunProductionPlanLog as r where r.BatchNo=?", batchNo);
        this.ucShowErrorMsg.Visible = true;
        this.ucShowErrorMsg.InitPageParameter(runProductionPlanLogs);
        //this.ucShowErrorMsg.
    }


    protected void btnRunPurchasePlan_Click(object sender, EventArgs e)
    {
        try
        {
            TheMrpMgr.RunMrp(this.CurrentUser);
            ShowSuccessMessage("生成成功。");
        }
        catch (SqlException ex)
        {
            ShowErrorMessage(ex.Message);
        }
        catch (Exception ee)
        {
            ShowErrorMessage(ee.Message);
        }
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        this.ucShowErrorMsg.Visible = false;
    }

}
