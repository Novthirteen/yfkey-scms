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
using com.Sconit.Entity.Distribution;
using com.Sconit.Utility;
using com.Sconit.Service.Distribution;

public partial class Distribution_OrderIssue_ViewShipList : ModuleBase
{
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(InProcessLocation ip)
    {
        this.InitPageParameter(ip, false);
    }
    public void InitPageParameter(InProcessLocation ip, bool printASN)
    {
        this.GV_List.DataSource = ip.InProcessLocationDetails;
        this.GV_List.DataBind();

        if (printASN)
        {
            this.PrintASN(ip);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(null, null);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        this.RePrintASN();
    }

    private void RePrintASN()
    {
        if (GV_List.Rows.Count == 0)
            return;

        HiddenField hfId = (HiddenField)GV_List.Rows[0].FindControl("hfId");
        InProcessLocationDetail ipDet = TheInProcessLocationDetailMgr.LoadInProcessLocationDetail(int.Parse(hfId.Value));
        InProcessLocation inProcessLocation = TheInProcessLocationMgr.LoadInProcessLocation(ipDet.InProcessLocation.IpNo, true);
        this.PrintASN(inProcessLocation);
    }
    private void PrintASN(InProcessLocation inProcessLocation)
    {
        IList<object> list = new List<object>();
        list.Add(inProcessLocation);
        list.Add(inProcessLocation.InProcessLocationDetails);

        
        //报表url
        string asnUrl = TheReportMgr.WriteToFile(inProcessLocation.AsnTemplate, list);

        //客户端打印
        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + asnUrl + "'); </script>");


        //Hu打印,todo
        //if (inProcessLocation != null)
        //{

        //    IList<object> huObj = new List<object>();
        //    IList<Hu> huList = new List<Hu>();
        //    IList<InProcessLocationDetail> inProcessLocationDetailList = inProcessLocation.InProcessLocationDetails;
        //    if (inProcessLocationDetailList != null && inProcessLocationDetailList.Count > 0)
        //    {
        //        foreach (InProcessLocationDetail inProcessLocationDetail in inProcessLocationDetailList)
        //        {
        //            Hu hu = TheHuMgr.LoadHu(inProcessLocationDetail.HuId);
        //            if (hu != null)
        //            {
        //                huList.Add(hu);
        //            }
        //        }
        //        if (huList != null && huList.Count > 0)
        //        {
        //            huObj.Add(huList);
        //            huObj.Add(CurrentUser.Code);
        //            string barCodeUrl = TheReportMgr.WriteToFile("BarCode.xls", huObj,"hu.xls");
        //            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");
        //        }
        //    }
        //}
    }
}
