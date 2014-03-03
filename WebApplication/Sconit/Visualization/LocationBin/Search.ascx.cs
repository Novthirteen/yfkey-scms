using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.View;

public partial class Visualization_LocationBin_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            string location = this.tbLocation.Text.Trim() != string.Empty ? this.tbLocation.Text.Trim() : string.Empty;
            string area = this.tbStorageArea.Text.Trim() != string.Empty ? this.tbStorageArea.Text.Trim() : string.Empty;
            string bin = this.tbStorageBin.Text.Trim() != string.Empty ? this.tbStorageBin.Text.Trim() : string.Empty;
            string item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : string.Empty;
            string huId = this.tbHuId.Text.Trim() != string.Empty ? this.tbHuId.Text.Trim() : string.Empty;
            string lotNo = this.tbLotNo.Text.Trim() != string.Empty ? this.tbLotNo.Text.Trim() : string.Empty;

            #region DetachedCriteria
            //DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(LocationBin));
            //DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(LocationBin))
            //    .SetProjection(Projections.Count("Id"));

            //if (location != string.Empty)
            //{
            //    selectCriteria.Add(Expression.Eq("Location", location));
            //    selectCountCriteria.Add(Expression.Eq("Location", location));
            //}
            //if (item != string.Empty)
            //{
            //    selectCriteria.Add(Expression.Like("Item", item, MatchMode.Start));
            //    selectCountCriteria.Add(Expression.Like("Item", item, MatchMode.Start));
            //}
            //if (huId != string.Empty)
            //{
            //    selectCriteria.Add(Expression.Like("HuId", huId, MatchMode.Start));
            //    selectCountCriteria.Add(Expression.Like("HuId", huId, MatchMode.Start));
            //}
            //if (lotNo != string.Empty)
            //{
            //    selectCriteria.Add(Expression.Eq("LotNo", lotNo));
            //    selectCountCriteria.Add(Expression.Eq("LotNo", lotNo));
            //}

            #endregion

            SearchEvent((new object[] { location, area, bin, item, huId, lotNo }), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Location"))
        {
            this.tbLocation.Text = actionParameter["Location"];
        }
        if (actionParameter.ContainsKey("Item"))
        {
            this.tbItem.Text = actionParameter["Item"];
        }
        if (actionParameter.ContainsKey("HuId"))
        {
            this.tbHuId.Text = actionParameter["HuId"];
        }
        if (actionParameter.ContainsKey("LotNo"))
        {
            this.tbLotNo.Text = actionParameter["LotNo"];
        }
    }
}
