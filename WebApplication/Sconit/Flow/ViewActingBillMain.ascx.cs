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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;

public partial class MasterData_Flow_ViewActingBillMain : MainModuleBase
{

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

    public string OrderType
    {
        get
        {
            return (string)ViewState["OrderType"];
        }
        set
        {
            ViewState["OrderType"] = value;
        }
    }

    public void InitPageParameter()
    {
        if (this.OrderType == BusinessConstants.BILL_TRANS_TYPE_PO)
        {
            this.lTitle.InnerText = "${MasterData.Flow.ActingBill.Po}";
        }
        else if (this.OrderType == BusinessConstants.BILL_TRANS_TYPE_SO)
        {
            this.lTitle.InnerText = "${MasterData.Flow.ActingBill.So}";
        }
        this.ucViewActingBillList.OrderType = this.OrderType;
        this.ucViewActingBillList.FlowCode = this.FlowCode;
        this.ucViewActingBillList.UpdateView();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }




}
