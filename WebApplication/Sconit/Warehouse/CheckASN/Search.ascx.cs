using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using System.Web.UI.WebControls;
using com.Sconit.Entity.View;

public partial class Warehouse_CheckASN_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler BatchEvent;

    private IDictionary<string, string> dicParam;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InProcessLocation));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(InProcessLocation))
            .SetProjection(Projections.Count("IpNo"));
         
        selectCountCriteria.Add(Expression.Eq("PartyFrom.Code", "YFK-FG"));
        selectCriteria.Add(Expression.Eq("PartyFrom.Code", "YFK-FG"));
        if (RadioButton2.Checked)
        {
            selectCriteria.Add(Expression.IsNotNull("CurrentActivity"));
            selectCountCriteria.Add(Expression.IsNotNull("CurrentActivity"));
        }
        else if (RadioButton3.Checked)
        {
            selectCriteria.Add(Expression.IsNull("CurrentActivity"));
            selectCountCriteria.Add(Expression.IsNull("CurrentActivity"));
        }
        if (this.tbStartDate.Text != string.Empty)
        {
            selectCriteria.Add(Expression.Gt("CreateDate", DateTime.Parse(tbStartDate.Text.Trim())));
            selectCountCriteria.Add(Expression.Gt("CreateDate", DateTime.Parse(tbStartDate.Text.Trim())));

        }
        if (this.tbEndDate.Text != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(tbEndDate.Text.Trim())));
            selectCountCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(tbEndDate.Text.Trim())));
        }

        if (tbIpNo.Text != string.Empty)
        {
            selectCriteria.Add(Expression.Like("IpNo",tbIpNo.Text.Trim(),MatchMode.End));
            selectCountCriteria.Add(Expression.Like("IpNo", tbIpNo.Text.Trim(), MatchMode.End));
        }
        if (tbPartyTo.Text != string.Empty)
        {
            selectCountCriteria.Add(Expression.Eq("PartyTo.Code", tbPartyTo.Text.Trim()));
            selectCriteria.Add(Expression.Eq("PartyTo.Code", tbPartyTo.Text.Trim()));
        }

        SearchEvent(new object[] {selectCriteria,selectCountCriteria }, null);
      
    }

    protected override void DoSearch()
    {

    }



    private void FillParameter()
    {

    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //todo
    }


    protected void BatchConfirm_Click(object sender, EventArgs e)
    {
        BatchEvent(sender, e);
    }
}
