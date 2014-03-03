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
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class Order_OrderView_ActBillList : ListModuleBase
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

    public string OrderNo
    {
        get
        {
            return (string)ViewState["OrderNo"];
        }
        set
        {
            ViewState["OrderNo"] = value;
        }
    }
    public string TransactionType
    {
        get
        {
            return (string)ViewState["TransactionType"];
        }
        set
        {
            ViewState["TransactionType"] = value;
        }
    }
    public Boolean ShowTab
    {
        get
        {
            return (Boolean)ViewState["ShowTab"];
        }
        set
        {
            ViewState["ShowTab"] = value;
        }
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
        this.Visible = false;
        this.ShowTab = false;
        if (this.TransactionType == BusinessConstants.BILL_TRANS_TYPE_PO)
        {
            this.lTitle.InnerText = "${MasterData.Order.ActingBill.Po}";
        }else if(this.TransactionType == BusinessConstants.BILL_TRANS_TYPE_SO)
        {
            this.lTitle.InnerText = "${MasterData.Order.ActingBill.So}";
        }
        ArrayList dataSource = (ArrayList)this.GV_List.DataSource;
        if (dataSource != null && dataSource.Count > 0)
        {
            this.Visible = true;
            this.ShowTab = true;
        }
        else
        {
            OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo);
            bool isShowPrice = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_IS_SHOW_PRICE).Value);
            if (isShowPrice)
            {
                isShowPrice = orderHead.IsShowPrice;
            }
            if (isShowPrice)
            {
                this.GV_List.Columns[5].Visible = true;
                this.GV_List.Columns[6].Visible = true;
                this.GV_List.Columns[7].Visible = true;
                this.GV_List.Columns[8].Visible = true;
                this.GV_List.Columns[9].Visible = true;
                this.GV_List.Columns[10].Visible = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {  
        
    }

  
}
