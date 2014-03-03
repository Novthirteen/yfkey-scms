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
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Control;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using System.Collections.Generic;
using com.Sconit.Utility;
using NHibernate.Expression;

public partial class Warehouse_CheckASN_Edit : SearchModuleBase
{

    public EventHandler BackEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["IpList"] = null;
            GV_List.DataSource = null;
            GV_List.DataBind();
        }
    }


    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (IpNo.Text != string.Empty)
        {
            IList<InProcessLocation> ipl = new List<InProcessLocation>();
            if (Session["IpList"] != null)
                ipl = (IList<InProcessLocation>)Session["IpList"];
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InProcessLocation));
            selectCriteria.Add(Expression.Like("IpNo", IpNo.Text.Trim(), MatchMode.End));
            selectCriteria.Add(Expression.Eq("PartyFrom.Code", "YFK-FG"));
            IList<InProcessLocation> result = TheCriteriaMgr.FindAll<InProcessLocation>(selectCriteria);
            if (result.Count == 0)
            {
                ShowWarningMessage("InProcessLocation.UnLoad.Warning", IpNo.Text.Trim());
            }
            int cnt = ipl.Count;
            foreach (InProcessLocation i in result)
            {
                if (checkIsExist(ipl, i.IpNo) == true) continue;
                if (i.CurrentActivity == null)
                    ipl.Add(i);
            }
            if (cnt == ipl.Count) ShowWarningMessage("InProcessLocation.UnLoad.Warning", IpNo.Text.Trim());
            GV_List.DataSource = ipl;
            GV_List.DataBind();
            Session["IpList"] = ipl;
            TheCriteriaMgr.CleanSession();
        }
    }

    bool checkIsExist(IList<InProcessLocation> l, string ipno)
    {
        var i = (from a in l where a.IpNo == ipno select a).ToList();
        return i.Count > 0 ? true : false;
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        if (Session["IpList"] != null)
        {
            IList<InProcessLocation> ipl = (IList<InProcessLocation>)Session["IpList"];

            string ipNo = ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument;
            InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(ipNo);
            ipl.Remove(ip);
            GV_List.DataSource = ipl;
            GV_List.DataBind();
            Session["IpList"] = ipl;
        }
    }
    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //todo
    }
    protected override void DoSearch()
    {

    }

    protected void btnBatchCon_Click(object sender, EventArgs e)
    {
        if (Session["IpList"] != null)
        {
            IList<InProcessLocation> ipl = (IList<InProcessLocation>)Session["IpList"];
            if (ipl.Count == 0)
            {
                ShowErrorMessage("InProcessLocation.DetailEmpty.Error");
                return;
            }
            foreach (InProcessLocation i in ipl)
            {
                if (i.CurrentActivity == null)
                    i.CurrentActivity = CurrentUser.Code + "|" + DateTime.Now.ToString("yyMMddHHmmss");
                TheInProcessLocationMgr.UpdateInProcessLocation(i);
            }
            Session["IpList"] = null;
            GV_List.DataSource = null;
            GV_List.DataBind();
            ShowSuccessMessage("InProcessLocation.BatchConfirm.Successfully");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["IpList"] = null;
        GV_List.DataSource = null;
        GV_List.DataBind();
        BackEvent(sender, e);
    }

}
