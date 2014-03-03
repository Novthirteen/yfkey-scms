using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Utility;

public partial class Inventory_UnWHQuery_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbEffectiveDateFrom.Text = DateTime.Now.AddDays(-1).ToString();
            tbEffectiveDateTo.Text = DateTime.Now.ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            object[] param = this.CollectParam();
            if (param != null)
                ExportEvent(param, null);
        }
    }

    protected override void DoSearch()
    {

        if (SearchEvent != null)
        {
            object[] param = CollectParam();
            if (param != null)
                SearchEvent(param, null);
        }
    }

    private object[] CollectParam()
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(UnWHQueryView));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(UnWHQueryView))
            .SetProjection(Projections.Count("HuId"));

        if (this.tbLocation.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Location", this.tbLocation.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("Location", this.tbLocation.Text.Trim()));
        }

        if (this.tbItemCode.Text.Trim() != string.Empty)
        {

            selectCriteria.Add(Expression.Eq("ItemCode", this.tbItemCode.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("ItemCode", this.tbItemCode.Text.Trim()));
           
        }
        if (this.tbHuId.Text.Trim() != string.Empty)
        {

            selectCriteria.Add(Expression.Eq("HuId", this.tbHuId.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("HuId", this.tbHuId.Text.Trim()));

        }
        if (this.tbLotNo.Text.Trim() != string.Empty)
        {

            selectCriteria.Add(Expression.Eq("LotNo", this.tbLotNo.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("LotNo", this.tbLotNo.Text.Trim()));

        }
        if (this.tbEffectiveDateFrom.Text.Trim() != string.Empty)
        {

            selectCriteria.Add(Expression.Gt("CreateDate",DateTime.Parse( this.tbEffectiveDateFrom.Text.Trim())));
            selectCountCriteria.Add(Expression.Gt("CreateDate",DateTime.Parse( this.tbEffectiveDateFrom.Text.Trim())));

        }
        if (this.tbEffectiveDateTo.Text.Trim() != string.Empty)
        {

            selectCriteria.Add(Expression.Lt("CreateDate",DateTime.Parse( this.tbEffectiveDateTo.Text.Trim())));
            selectCountCriteria.Add(Expression.Lt("CreateDate",DateTime.Parse( this.tbEffectiveDateTo.Text.Trim())));

        }

        return new object[] { selectCriteria, selectCountCriteria };

    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //if (actionParameter.ContainsKey("Location"))
        //{
        //    this.tbLocation.Text = actionParameter["Location"];
        //}
        //if (actionParameter.ContainsKey("Item"))
        //{
        //    this.tbItem.Text = actionParameter["Item"];
        //}
        //if (actionParameter.ContainsKey("EffDate"))
        //{
        //    this.tbEffDate.Text = actionParameter["EffDate"];
        //}
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        tbItemCode.Text = "";
        tbHuId.Text = "";
        tbLotNo.Text = "";
        tbEffectiveDateFrom.Text = "";
        tbEffectiveDateTo.Text = "";
    }
}