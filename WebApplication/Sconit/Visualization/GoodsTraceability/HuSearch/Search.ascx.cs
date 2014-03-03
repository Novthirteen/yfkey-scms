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
using System.Collections.Generic;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.View;

public partial class Visualization_GoodsTraceability_HuSearch_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbLotNo.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch(false);
    }
    protected override void DoSearch()
    {
        
    }
    protected   void DoSearch(bool isExport)
    {
        if (SearchEvent != null)
        {
            string item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : string.Empty;
            string huId = this.tbHuId.Text.Trim() != string.Empty ? this.tbHuId.Text.Trim() : string.Empty;
            string lotNo = this.tbLotNo.Text.Trim() != string.Empty ? this.tbLotNo.Text.Trim() : string.Empty;
            string orderNo = this.tbOrderNo.Text.Trim() != string.Empty ? this.tbOrderNo.Text.Trim() : string.Empty;
            string startDate = this.tbStartDate.Text.Trim() != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
            string endDate = this.tbEndDate.Text.Trim() != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;

            #region DetachedCriteria
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(GoodsTraceabilityViewBase));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(GoodsTraceabilityViewBase))
                .SetProjection(Projections.Count("HuId"));
            
            if (item != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Item", item, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Item", item, MatchMode.Start));
            }
            if (huId != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("HuId", huId));
                selectCountCriteria.Add(Expression.Eq("HuId", huId));
            }
            if (lotNo != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Location", lotNo));
                selectCountCriteria.Add(Expression.Eq("Location", lotNo));
            }
            if (orderNo != string.Empty)
            {
                selectCriteria.Add(Expression.Like("OrderNo", orderNo, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("OrderNo", orderNo, MatchMode.Start));
            }
            if (startDate != string.Empty)
            {
                selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
                selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1)));
                selectCountCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1)));
            }

            #endregion

            if (isExport)
            {
                ExportEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            }
            else
            {
                SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.DoSearch(true);
    }
    
      
           
        
     

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Item"))
        {
            this.tbItem.Text = actionParameter["Item"];
        }
        if (actionParameter.ContainsKey("StartDate"))
        {
            this.tbStartDate.Text = actionParameter["StartDate"];
        }
        if (actionParameter.ContainsKey("EndDate"))
        {
            this.tbEndDate.Text = actionParameter["EndDate"];
        }
    }
}
