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

public partial class MasterData_Flow_ViewLocTransList : ListModuleBase
{
    public EventHandler EditEvent;

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
    public string IOType
    {
        get
        {
            return (string)ViewState["IOType"];
        }
        set
        {
            ViewState["IOType"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public override void UpdateView()
    {

        List<OrderLocationTransaction> orderLocationTransactionInList = new List<OrderLocationTransaction>();
        List<OrderLocationTransaction> orderLocationTransactionOutList = new List<OrderLocationTransaction>();
        try
        {
            OrderHead orderHead = TheOrderMgr.TransferFlow2Order(this.FlowCode, true);
            if (orderHead.OrderDetails != null)
            {
                foreach (OrderDetail orderDetail in orderHead.OrderDetails)
                {
                    if (orderDetail.OrderLocationTransactions != null)
                    {
                        foreach (OrderLocationTransaction orderLocationTransaction in orderDetail.OrderLocationTransactions)
                        {
                            if (orderLocationTransaction.IOType == BusinessConstants.IO_TYPE_IN)
                            {
                                orderLocationTransactionInList.Add(orderLocationTransaction);
                            }
                            else if (
                                orderLocationTransaction.IOType == BusinessConstants.IO_TYPE_OUT)
                            {
                                orderLocationTransactionOutList.Add(orderLocationTransaction);
                            }
                        }
                    }
                }
            }
            if (this.IOType == BusinessConstants.IO_TYPE_IN)
            {
                this.GV_List.DataSource = orderLocationTransactionInList;
                
                this.Parent.Visible = orderLocationTransactionInList.Count == 0 ? false : true;

            }
            else if (this.IOType == BusinessConstants.IO_TYPE_OUT)
            {
                this.GV_List.DataSource = orderLocationTransactionOutList;
                this.GV_List.Columns[6].Visible = false;
                this.Parent.Visible = orderLocationTransactionOutList.Count == 0 ? false : true;
            }
            this.GV_List.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }



}
