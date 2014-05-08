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
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Utility;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

public partial class Transportation_ValuateOrder_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ValuateEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("OrderNo"))
        {
            this.tbOrderNo.Text = actionParameter["OrderNo"];
        }
    }

    protected override void DoSearch()
    {
        string orderNo = this.tbOrderNo.Text != string.Empty ? this.tbOrderNo.Text.Trim() : string.Empty;
        string routeNo = this.tbRoute.Text.Trim() != string.Empty ? this.tbRoute.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text.Trim() != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text.Trim() != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportationOrder));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportationOrder))
                .SetProjection(Projections.Count("OrderNo"));

            selectCriteria.CreateAlias("TransportationRoute", "r", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
            selectCountCriteria.CreateAlias("TransportationRoute", "r", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

            if (orderNo != string.Empty)
            {
                selectCriteria.Add(Expression.Like("OrderNo", orderNo, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("OrderNo", orderNo, MatchMode.Start));
            }
            if (routeNo != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("r.Code", routeNo));
                selectCountCriteria.Add(Expression.Eq("r.Code", routeNo));
            }
            if (startDate != string.Empty)
            {
                selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
                selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                selectCriteria.Add(Expression.Le("CreateDate", DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1)));
                selectCountCriteria.Add(Expression.Le("CreateDate", DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1)));
            }

            selectCriteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)));
            selectCountCriteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)));

            selectCriteria.Add(Expression.Eq("IsValuated", false));
            selectCountCriteria.Add(Expression.Eq("IsValuated", false));

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected void btnValuate_Click(object sender, EventArgs e)
    {
        ValuateEvent(sender, e);
    }

    public void ValuateOrder(IList<TransportationOrder> transportationOrderList)
    {
        try
        {
            if (transportationOrderList != null && transportationOrderList.Count > 0)
            {
                TheTransportationOrderMgr.ValuateTransportationOrder(transportationOrderList, this.CurrentUser);

                DoSearch();

                ShowSuccessMessage("Transportation.TransportationOrder.ValuateTransportationOrder.Successfully");
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        HSSFWorkbook excel = new HSSFWorkbook(fileUpload.PostedFile.InputStream);
        Sheet sheet = excel.GetSheetAt(0);
        IEnumerator rows = sheet.GetRowEnumerator();
      
        ImportHelper.JumpRows(rows, 1);
        IList<TransportationActBill> tactbillList = new List<TransportationActBill>();
        IList<string> orderNos = new List<string>();
        string supply = string.Empty;
        while (rows.MoveNext())
        {
            Row curow = (HSSFRow)rows.Current;
            string orderNo = curow.GetCell(0).StringCellValue;
            // decimal cur = decimal.Parse(curow.GetCell(1).NumericCellValue.ToString());
            if (string.IsNullOrEmpty(orderNo))
            {
                break;
            }
            else
            {
                if (orderNos.Contains(orderNo))
                {
                    continue;//避免重复
                }
                else
                {
                    orderNos.Add(orderNo);
                }
            }

        }
        string successMessage="计价成功的单号：";
        string errorMessage="计价失败的单号：";
        if (orderNos.Count > 0)
        {
            foreach (var orderNo in orderNos)
            {
                try
                {
                    TheTransportationOrderMgr.ValuateTransportationOrder(orderNo, this.CurrentUser);
                    successMessage += orderNo + ",";
                }
                catch (Exception ex)
                {
                    errorMessage += orderNo + ",";
                }
            }

            ShowErrorMessage(successMessage+" </br> "+errorMessage);
        }
        else
        {
            ShowErrorMessage(" 导入的有效数据为0行！");
        }
    }
}
