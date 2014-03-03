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
using System.Collections.Generic;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;

public partial class Visualization_Traceability_List : ListModuleBase
{
    private Visualization_GoodsTraceability_Traceability_View GetOrderViewControl(GridViewRow gvr)
    {
        return (Visualization_GoodsTraceability_Traceability_View)gvr.FindControl("ucView");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string huId)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(LocationTransaction));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(LocationTransaction))
            .SetProjection(Projections.Count("Id"));

        selectCriteria.Add(Expression.Eq("HuId", huId));
        selectCountCriteria.Add(Expression.Eq("HuId", huId));

        this.SetSearchCriteria(selectCriteria, selectCountCriteria);
        this.UpdateView();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LocationTransaction LocTrans = (LocationTransaction)e.Row.DataItem;
            if (LocTrans.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS))
            {
                GetOrderViewControl(e.Row).InitPageParameter(LocTrans.IpNo, BusinessConstants.CODE_PREFIX_ASN);
            }
            else if (LocTrans.TransactionType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT))
            {
                GetOrderViewControl(e.Row).InitPageParameter(LocTrans.ReceiptNo, BusinessConstants.CODE_PREFIX_RECEIPT);
            }
        }
    }

    protected void lbOrderNo_Click(object sender, EventArgs e)
    {
        string receiptNo = ((LinkButton)sender).CommandArgument;
        ShowSuccessMessage(receiptNo);
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }
}
