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

public partial class Visualization_GoodsTraceability_HuSearch_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
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
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Hu));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Hu))
                .SetProjection(Projections.Count("HuId"));

            if (item != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Item.Code", item, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Item.Code", item, MatchMode.Start));
            }
            if (huId != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("HuId", huId));
                selectCountCriteria.Add(Expression.Eq("HuId", huId));
            }
            if (lotNo != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("LotNo", lotNo));
                selectCountCriteria.Add(Expression.Eq("LotNo", lotNo));
            }
            if (orderNo != string.Empty)
            {
                selectCriteria.Add(Expression.Like("OrderNo", orderNo, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("OrderNo", orderNo, MatchMode.Start));
            }
            if (startDate != string.Empty)
            {
                selectCriteria.Add(Expression.Ge("ManufactureDate", DateTime.Parse(startDate)));
                selectCountCriteria.Add(Expression.Ge("ManufactureDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                selectCriteria.Add(Expression.Lt("ManufactureDate", DateTime.Parse(endDate).AddDays(1)));
                selectCountCriteria.Add(Expression.Lt("ManufactureDate", DateTime.Parse(endDate).AddDays(1)));
            }

            #endregion

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
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
