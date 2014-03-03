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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;

public partial class Transportation_TransportationFeesApply_List : ListModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void lbtnCreate_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        try
        {
            
            Expense ex = TheExpenseMgr.LoadExpense(code);
            string s_remark = ex.Remark;
            string[] remarks = ex.Remark.Split(new char[] { ';' });
            ex.Remark = "";
            ex.LastModifyUser = this.CurrentUser;
            ex.LastModifyDate = DateTime.Now;
            TheExpenseMgr.UpdateExpense(ex);
            TransportationOrder transportationOrder = TheTransportationOrderMgr.CreateTransportationOrder(code, this.CurrentUser);
            foreach (string s in remarks)
            {
                if (s.Split(new char[] { ':' })[0] == "PriceMethod")
                    transportationOrder.PricingMethod = s.Split(new char[] { ':' })[1];
                if (s.Split(new char[] { ':' })[0] == "VehicleType")
                   transportationOrder.VehicleType = s.Split(new char[] { ':' })[1];
            }
            ex.Remark = ex.Code;
            ex.Remark += s_remark;
            TheExpenseMgr.UpdateExpense(ex);
            transportationOrder.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE;
          
            TheTransportationOrderMgr.UpdateTransportationOrder(transportationOrder);
            ShowSuccessMessage("Transportation.TransportationOrder.AddTransportationOrder.Successfully", transportationOrder.OrderNo); 
            UpdateView();
        }
        catch
        {
            ShowErrorMessage("Transportation.TransportationOrder.AddTransportationOrder.Fail"); 
        }       
    }
}
