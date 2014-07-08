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

public partial class NewMrp_ShipPlan_List : ListModuleBase
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
            PurchasePlanMstr m = (PurchasePlanMstr)e.Row.DataItem;
            var runShipPlanLogs = TheGenericMgr.FindAllWithCustomQuery<RunPurchasePlanLog>("select r from RunPurchasePlanLog as r where r.BatchNo=?", m.BatchNo);
            if (runShipPlanLogs == null || runShipPlanLogs.Count == 0)
            {
                System.Web.UI.WebControls.LinkButton lbtnShowErrorMsg = e.Row.FindControl("lbtnShowErrorMsg") as System.Web.UI.WebControls.LinkButton;
                lbtnShowErrorMsg.Visible = false;
            }
            if (m.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                System.Web.UI.WebControls.LinkButton lbtSubmit = e.Row.FindControl("lbtSubmit") as System.Web.UI.WebControls.LinkButton;
                lbtSubmit.Visible = false;
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
        var runShipPlanLogs = TheGenericMgr.FindAllWithCustomQuery<RunPurchasePlanLog>("select r from RunPurchasePlanLog as r where r.BatchNo=?", batchNo);
        this.ucShowErrorMsg.Visible = true;
        this.ucShowErrorMsg.InitPageParameter(runShipPlanLogs);
        //this.ucShowErrorMsg.
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string releaseNo = ((LinkButton)sender).CommandArgument;
        IList<PurchasePlanMstr> mstr = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanMstr>(string.Format(" select m from PurchasePlanMstr as m where m.Status='{0}' and ReleaseNo={1} ", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, releaseNo));
        if (mstr != null && mstr.Count > 0)
        {
            PurchasePlanMstr m = mstr.First();
            DateTime dateNow = System.DateTime.Now;
            m.LastModifyUser = this.CurrentUser.Code;
            m.LastModifyDate = dateNow;
            m.ReleaseDate = dateNow;
            m.ReleaseUser = this.CurrentUser.Code;
            m.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
            m.Version += 1;
            TheGenericMgr.Update(m);
            ShowSuccessMessage("释放成功。");
        }
        else
        {
            ShowErrorMessage("没有需要释放的采购计划。");
        }
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        this.ucShowErrorMsg.Visible = false;
    }

}
