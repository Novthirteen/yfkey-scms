using System;
using System.Collections;
using System.Collections.Generic;
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
using com.Sconit.Entity.Exception;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class Order_BatchCheckIn_List : ModuleBase
{
   
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


    public string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string flowCode)
    {
        this.FlowCode = flowCode;
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(OrderHead));

        selectCriteria.Add(Expression.Eq("Flow", flowCode))
            .Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
        IList<OrderHead> orderHeadList = TheCriteriaMgr.FindAll<OrderHead>(selectCriteria);
        this.GV_List.DataSource = orderHeadList;
        this.GV_List.DataBind();

    }
    protected void btnStart_Click(object sender, EventArgs e)
    {

        IList<string> orderList = GetSelectOrder();
        if (orderList.Count == 0)
        {
            ShowErrorMessage("MasterData.Order.BatchCheckIn.PleaseSelectOrder");  
        }
        else
        {
            try
            {
                foreach (string orderNo in orderList)
                {
                    TheOrderMgr.StartOrder(orderNo, this.CurrentUser.Code);
                }
                ShowSuccessMessage("MasterData.Order.BatchStart.Successfully");
                InitPageParameter(this.FlowCode);
            }
            catch (BusinessErrorException ex)
            {
                this.ShowErrorMessage(ex);
            }

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        IList<string> orderList = GetSelectOrder();
        if (orderList.Count == 0)
        {
            ShowErrorMessage("MasterData.Order.BatchCheckIn.PleaseSelectOrder");
        }
        else
        {
            try
            {
                foreach (string orderNo in orderList)
                {
                    TheOrderMgr.CancelOrder(orderNo, this.CurrentUser.Code);
                }
                ShowSuccessMessage("MasterData.Order.BatchCancel.Successfully");
                InitPageParameter(this.FlowCode);
            }
            catch (BusinessErrorException ex)
            {
                this.ShowErrorMessage(ex);
            }

        }
    }

    private IList<string> GetSelectOrder()
    {
        IList<string> orderNoList = new List<string>();
        for (int i = 0; i < this.GV_List.Rows.Count; i++)
        {
            CheckBox cbOrderNo = (CheckBox)this.GV_List.Rows[i].FindControl("cbOrderNo");

            if (cbOrderNo.Checked == true)
            {
                orderNoList.Add(this.GV_List.DataKeys[i].Value.ToString());
            }
        }
        return orderNoList;
    }

  

   
}
