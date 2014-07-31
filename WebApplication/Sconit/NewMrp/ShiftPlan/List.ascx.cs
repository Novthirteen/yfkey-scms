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

public partial class NewMrp_ShiftPlan_List : ListModuleBase
{
    public EventHandler EditEvent;
    public EventHandler SearchDetailEvent;
    public EventHandler ShowErrorMsgEvent;


    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            ShiftPlanMstr m = (ShiftPlanMstr)e.Row.DataItem;
           
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string releaseNo = ((LinkButton)sender).CommandArgument;
        IList<ShiftPlanMstr> mstr = TheGenericMgr.FindAllWithCustomQuery<ShiftPlanMstr>(string.Format(" select m from ShiftPlanMstr as m where m.Status='{0}' and ReleaseNo={1} ", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE, releaseNo));
        if (mstr != null && mstr.Count > 0)
        {
            ShiftPlanMstr m = mstr.First();
            DateTime dateNow = System.DateTime.Now;
            m.LastModifyUser = this.CurrentUser.Code;
            m.LastModifyDate = dateNow;
            m.ReleaseDate = dateNow;
            m.ReleaseUser = this.CurrentUser.Code;
            m.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
            TheGenericMgr.Update(m);
            ShowSuccessMessage("释放成功。");
        }
        else
        {
            ShowErrorMessage("没有需要释放的班产计划。");
        }
    }

}
