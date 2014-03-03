using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using System.Xml;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

public partial class MRP_ShiftPlan_Import_Import : ModuleBase
{
    public event EventHandler ImportEvent;
    public event EventHandler BtnBackClick;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.ucShift.Date = DateTime.Today;
        }

        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
    }
    /// <summary>
    /// 参考表单
    /// 同一零件号的第一张工单的编号
    /// 将成为后续工单的参考工单号
    /// djin 2012-06-21
    /// </summary>
    /// <param name="sender"></param>
    public void Create(object sender)
    {
        try
        {
            string sqlText = string.Empty;
            string ConnString = string.Empty;
            XmlTextReader reader = new XmlTextReader(Server.MapPath("Config/properties.config"));
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);//  
            reader.Close();//
            ConnString = doc.SelectSingleNode("/configuration/properties/connectionString").InnerText.Trim();

            IList<OrderHead> orderHeadList = (IList<OrderHead>)sender;
            if (orderHeadList != null && orderHeadList.Count > 0)
            {
                string shiftCode = this.ucShift.ShiftCode;
                Shift shift = TheShiftMgr.LoadShift(shiftCode);
                foreach (var item in orderHeadList)
                {
                    item.Shift = shift;
                }
                TheOrderMgr.CreateOrder(orderHeadList, this.CurrentUser.Code);
                var result = (from order in orderHeadList select order.OrderDetails[0].Item.Code).Distinct();

                foreach (var s in result)
                {
                    var _ordNo = (from order in orderHeadList
                                  where order.OrderDetails[0].Item.Code == s
                                  orderby order.WinDate, order.WindowTime
                                  select order.OrderNo);
                    int i = 0;
                    string refOrderNo = string.Empty;
                    foreach (var ord in _ordNo)
                    {
                        
                        UpdateOrderMstr(ord, refOrderNo, ConnString);
                        if (i == 0) refOrderNo = ord;

                        i++;
                    }
                }

                  

                ShowSuccessMessage("Common.Business.Result.Insert.Successfully");
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }
    /// <summary>
    /// 执行sql语句 djin 2012-06-21
    /// </summary>
    /// <param name="orderNo">工单号</param>
    /// <param name="refOrderNo">参考工单号</param>
    /// <param name="ConnString">连接字符串</param>
    protected void UpdateOrderMstr(string orderNo,string refOrderNo, string ConnString)
    {
        
        string buchong=refOrderNo==""?"":",IsAdditional='1'";
        string sqlText = "update ordermstr set reforderno='"
                            + refOrderNo
                            + "'" + buchong
                           + " where orderno='"
                           + orderNo
                           + "'";
        SqlHelper.ExecuteNonQuery(ConnString, CommandType.Text, sqlText);
     }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        this.Import();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BtnBackClick != null)
        {
            BtnBackClick(null, null);
        }
    }

    protected void tbRegion_TextChanged(object sender, EventArgs e)
    {
        this.BindShift();
    }

    protected void tbDate_TextChanged(object sender, EventArgs e)
    {
        this.BindShift();
    }

    private void BindShift()
    {
        this.ucShift.BindList(DateTime.Parse(this.tbDate.Text), this.tbRegion.Text.Trim());
    }

    private void Import()
    {
        try
        {
            string region = this.tbRegion.Text.Trim() != string.Empty ? this.tbRegion.Text.Trim() : string.Empty;
            string flowCode = this.tbFlow.Text.Trim() != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
            DateTime date = DateTime.Parse(this.tbDate.Text);
            string shiftCode = this.ucShift.ShiftCode;
            decimal leadTime = decimal.Parse(this.tbLeadTime.Text.Trim());
            IList<ShiftPlanSchedule> spsList = TheImportMgr.ReadPSModelFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser, region, flowCode, date, shiftCode);
            TheShiftPlanScheduleMgr.SaveShiftPlanSchedule(spsList, this.CurrentUser);
            IList<OrderHead> ohList = TheOrderMgr.ConvertShiftPlanScheduleToOrders(spsList,leadTime);
            if (ImportEvent != null)
            {
                ImportEvent(new object[] { ohList }, null);
            }
            ShowSuccessMessage("Import.Result.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }
}
