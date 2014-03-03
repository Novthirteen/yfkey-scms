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
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Procurement;
using com.Sconit.Utility;

public partial class MasterData_PriceList_PriceList_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    public string PriceListType
    {
        get
        {
            return (string)ViewState["PriceListType"];
        }
        set
        {
            ViewState["PriceListType"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE)
        {
            this.ltlParty.Text = "${MasterData.Supplier.Code}:";
            tbParty.ServiceMethod = "GetSupplier";
            tbParty.ServicePath = "SupplierMgr.service";
            tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;
        }
        else if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES)
        {
            tbParty.ServiceMethod = "GetCustomer";
            tbParty.ServicePath = "CustomerMgr.service";
            tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;
        
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text.Trim() != string.Empty ? this.tbCode.Text.Trim() : string.Empty;
        string party = this.tbParty.Text.Trim() != string.Empty ? this.tbParty.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE)
            {
                #region DetachedCriteria
                DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(PurchasePriceList));
                DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(PurchasePriceList))
                    .SetProjection(Projections.Count("Code"));

                if (code != string.Empty)
                {
                    selectCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
                    selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
                }
                if (party != string.Empty)
                {
                    selectCriteria.Add(Expression.Like("Party.Code", party, MatchMode.Start));
                    selectCountCriteria.Add(Expression.Like("Party.Code", party, MatchMode.Start));
                }
                DetachedCriteria[] supplierCrieteria = SecurityHelper.GetSupplierPermissionCriteria(this.CurrentUser.Code);
                selectCriteria.Add(
                    Expression.Or(
                      Expression.Or(
                          Subqueries.PropertyIn("Party.Code", supplierCrieteria[0]),
                          Subqueries.PropertyIn("Party.Code", supplierCrieteria[1])
                                    ),
                          Expression.IsNull("Party.Code")
                                  )
                    );

                selectCountCriteria.Add(
                    Expression.Or(
                      Expression.Or(
                          Subqueries.PropertyIn("Party.Code", supplierCrieteria[0]),
                          Subqueries.PropertyIn("Party.Code", supplierCrieteria[1])
                                    ),
                          Expression.IsNull("Party.Code")
                                  )
                    );
                SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
                #endregion
            }
            else if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES)
            {
                #region DetachedCriteria
                DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(SalesPriceList));
                DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(SalesPriceList))
                    .SetProjection(Projections.Count("Code"));

                if (code != string.Empty)
                {
                    selectCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
                    selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
                }
                if (party != string.Empty)
                {
                    selectCriteria.Add(Expression.Like("Party.Code", party, MatchMode.Start));
                    selectCountCriteria.Add(Expression.Like("Party.Code", party, MatchMode.Start));
                }
                DetachedCriteria[] customerCrieteria = SecurityHelper.GetCustomerPermissionCriteria(this.CurrentUser.Code);
                selectCriteria.Add(
                   Expression.Or(
                     Expression.Or(
                         Subqueries.PropertyIn("Party.Code", customerCrieteria[0]),
                         Subqueries.PropertyIn("Party.Code", customerCrieteria[1])
                                   ),
                         Expression.IsNull("Party.Code")
                                 )
                   );

                selectCountCriteria.Add(
                    Expression.Or(
                      Expression.Or(
                          Subqueries.PropertyIn("Party.Code", customerCrieteria[0]),
                          Subqueries.PropertyIn("Party.Code", customerCrieteria[1])
                                    ),
                          Expression.IsNull("Party.Code")
                                  )
                    );
                SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
                #endregion
            }
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Code"))
        {
            this.tbCode.Text = actionParameter["Code"];
        }
        if (actionParameter.ContainsKey("PriceListType"))
        {
            this.tbParty.Text = actionParameter["PriceListType"];
        }
    }
}
