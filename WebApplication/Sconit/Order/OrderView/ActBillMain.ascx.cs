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
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Web;

public partial class Order_OrderView_ActBillMain : MainModuleBase
{
    public event EventHandler BackEvent;

    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }

    public void InitPageParameter(string orderNo)
    {
        //#region OrderPOActingBill
        //DetachedCriteria selectActPOBillCriteria = DetachedCriteria.For(typeof(OrderActingBill));
        //DetachedCriteria selectActPOBillCountCriteria = DetachedCriteria.For(typeof(OrderActingBill))
        //    .SetProjection(Projections.Count("Id"));
        //selectActPOBillCriteria.CreateAlias("OrderDetail", "od");
        //selectActPOBillCriteria.CreateAlias("od.OrderHead", "oh");
        //selectActPOBillCountCriteria.CreateAlias("OrderDetail", "od");
        //selectActPOBillCountCriteria.CreateAlias("od.OrderHead", "oh");

        //selectActPOBillCriteria.Add(Expression.Eq("oh.OrderNo", orderNo));
        //selectActPOBillCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_PO));
        //selectActPOBillCountCriteria.Add(Expression.Eq("oh.OrderNo", orderNo));
        //selectActPOBillCountCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_PO));

        //selectActPOBillCriteria.AddOrder(Order.Asc("od.Sequence"));

        //this.ucActPOBillList.SetSearchCriteria(selectActPOBillCriteria, selectActPOBillCountCriteria);
        ////this.ucActPOBillList.Visible = true;
        //this.ucActPOBillList.OrderNo = orderNo;
        //this.ucActPOBillList.TransactionType = BusinessConstants.BILL_TRANS_TYPE_PO;
        //this.ucActPOBillList.UpdateView();
        //#endregion

        //#region OrderSOActingBill
        //DetachedCriteria selectActSOBillCriteria = DetachedCriteria.For(typeof(OrderActingBill));
        //DetachedCriteria selectActSOBillCountCriteria = DetachedCriteria.For(typeof(OrderActingBill))
        //    .SetProjection(Projections.Count("Id"));
        //selectActSOBillCriteria.CreateAlias("OrderDetail", "od");
        //selectActSOBillCriteria.CreateAlias("od.OrderHead", "oh");
        //selectActSOBillCountCriteria.CreateAlias("OrderDetail", "od");
        //selectActSOBillCountCriteria.CreateAlias("od.OrderHead", "oh");

        //selectActSOBillCriteria.Add(Expression.Eq("oh.OrderNo", orderNo));
        //selectActSOBillCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_SO));
        //selectActSOBillCountCriteria.Add(Expression.Eq("oh.OrderNo", orderNo));
        //selectActSOBillCountCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_SO));

        //selectActSOBillCriteria.AddOrder(Order.Asc("od.Sequence"));

        //this.ucActSOBillList.SetSearchCriteria(selectActSOBillCriteria, selectActSOBillCountCriteria);
        ////this.ucActSOBillList.Visible = true;
        //this.ucActSOBillList.OrderNo = orderNo;
        //this.ucActSOBillList.TransactionType = BusinessConstants.BILL_TRANS_TYPE_SO;
        //this.ucActSOBillList.UpdateView();


        //this.Visible = this.ucActPOBillList.ShowTab || this.ucActSOBillList.ShowTab;
       

        //#endregion
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ucActPOBillList.ModuleType = this.ModuleType;
            this.ucActPOBillList.ModuleSubType = this.ModuleSubType;

            this.ucActSOBillList.ModuleType = this.ModuleType;
            this.ucActSOBillList.ModuleSubType = this.ModuleSubType;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }
}
