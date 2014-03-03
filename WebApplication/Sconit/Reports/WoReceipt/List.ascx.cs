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
using com.Sconit.Service.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.View;
using com.Sconit.Utility;
using NHibernate.Transform;
using System.Collections.Generic;

public partial class MasterData_Reports_WoReceipt_List : ListModuleBase
{
    private DetachedCriteria Criteria
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

    private DetachedCriteria DetailCriteria
    {
        get
        {
            return (DetachedCriteria)ViewState["DetailCriteria"];
        }
        set
        {
            ViewState["DetailCriteria"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //UpdateView();
        }
    }



    public void InitPageParameter(DetachedCriteria criteria, DetachedCriteria detailCriteria)
    {
        this.Criteria = criteria;
        this.DetailCriteria = detailCriteria;
        this.UpdateView();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }

    public override void UpdateView()
    {
      
        this.GV_List.DataSource =TheCriteriaMgr.FindAll<WoReceiptView>(this.Criteria);
        this.GV_List.DataBind();
    }

    public void Export()
    {
        this.ExportXLS(GV_List);
    }



    protected void lbtnQty_Click(object sender, EventArgs e)
    {
        string flowItem = ((LinkButton)sender).CommandArgument;
        string[] argument = flowItem.Split('|');
        string flow = argument[0];
        string item = argument[1];

        DetachedCriteria criteria = CloneHelper.DeepClone<DetachedCriteria>(this.DetailCriteria);
        criteria.Add(Expression.Eq("Item", item));
        criteria.Add(Expression.Eq("Flow", flow));



        IList<WoReceiptView> woReceiptDetailList = TheCriteriaMgr.FindAll<WoReceiptView>(criteria);

        this.ucList.UpdateView(woReceiptDetailList);
        this.ucList.Visible = true;
    }


}
