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
using com.Sconit.Service.MasterData;
using System.IO;
using com.Sconit.Utility;
using com.Sconit.Entity.MRP;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using NHibernate.Expression;

public partial class Schedule_DemandSchedule_ActQtyList : ModuleBase
{
    public EventHandler EditEvent;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitParamater(string itemCode, DateTime date,DateTime effDate, bool isFlow, string flowOrLoc, bool isWinTime)
    {


        DetachedCriteria criteria = DetachedCriteria.For<ExpectTransitInventory>();
        criteria.Add(Expression.Eq("EffectiveDate", effDate));
        criteria.Add(Expression.Eq("Item", itemCode));
        if (isFlow)
        {
            criteria.Add(Expression.Eq("Flow", flowOrLoc));
        }
        else
        {
            criteria.Add(Expression.Eq("Location", flowOrLoc));
        }
        if (isWinTime)
        {
            criteria.Add(Expression.Le("WindowTime", date));
        }
        else
        {
            criteria.Add(Expression.Le("StartTime", date));
        }
        IList<ExpectTransitInventory> expectTransitInventoryList = this.TheCriteriaMgr.FindAll<ExpectTransitInventory>(criteria);

        this.GV_List.DataSource = expectTransitInventoryList;
        this.GV_List.DataBind();
        
    }


    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}
