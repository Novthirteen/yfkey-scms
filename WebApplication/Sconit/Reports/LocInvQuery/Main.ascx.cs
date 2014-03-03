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
using com.Sconit.Entity;
using System.Xml;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

public partial class LocInvQuery : MainModuleBase
{
    private string ConnString = string.Empty;
    string sqlText = string.Empty;
    DataSet dsOrderQuery = new DataSet();

    public LocInvQuery()
    {
    }

    protected DataSet sqltods(string sqltext)
    {
        XmlTextReader reader = new XmlTextReader(Server.MapPath("Config/properties.config"));
        XmlDocument doc = new XmlDocument();
        doc.Load(reader);//  
        reader.Close();//
        ConnString = doc.SelectSingleNode("/configuration/properties/connectionString").InnerText.Trim();
        dsOrderQuery = SqlHelper.ExecuteDataset(ConnString, CommandType.Text, sqlText);

        Session["ds"] = dsOrderQuery;
        return dsOrderQuery;
    }
    protected void BindData(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //if (CurrentUser.HasPermission("btnTransportationOrderQuery"))
            //{
            //    ddlStatus.Items.Add("Checked");
            //    sqlText = "select * from TransportationOrderQuery  order by CreateDate desc";
            //}
            //else
            //{
            //    sqlText = "select * from TransportationOrderQuery where isnull(status,' ') not in ('Checked') order by CreateDate desc";

            //}
            sqlText = "select * from locinvqueryview";

            BindData(sqltods(sqlText));
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        btn1_Click(sender, e);
    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        string TRoute = txtTRoute.Text;
        //string IpNo = txtIpNo.Text;
        //string OrderNo = txtOrderNo.Text;
        //string Status = ddlStatus.Text;
        //string Flow = txtFlow.Text;

        sqlText = "select * from locinvqueryview where 1=1";
        if (TRoute != "")
        {
            sqlText += " and item like '%" + TRoute + "%'";
        }
        if (location.Value != "")
        {
            sqlText += " and location='" + location.Value + "'";
        }
        //if (IpNo != "")
        //{
        //    sqlText += " and IpNo like '%" + IpNo + "%'";
        //}
        //if (OrderNo != "")
        //{
        //    sqlText += " and OrderNo like '%" + OrderNo + "%'";
        //}
        //if (Flow != "")
        //{
        //    sqlText += " and Flow like '%" + Flow + "%'";
        //}
        //if (Status == "empty")
        //{
        //    sqlText += " and Status is null";
        //}
        //else
        //    if (Status == "all")
        //    {
        //    }
        //    else
        //if (Status != "")
        //{
        //    sqlText += " and Status ='" + Status + "'";
        //}
        
        //if (txtCreateDate.Text != "")
        //{
        //    sqlText += " and CreateDate > '" + Convert.ToDateTime(txtCreateDate.Text).AddDays(-2).Date + "'";
        //}
        //sqlText += " order by CreateDate desc";
        BindData(sqltods(sqlText));
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtTRoute.Text = "";
        location.Value = "";
        //txtIpNo.Text = "";
        //txtOrderNo.Text = "";
        //ddlStatus.SelectedIndex = 0;
        //txtFlow.Text = "";
        //txtCreateDate.Text = "";
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        Sheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
        sheet1.CreateRow(0).CreateCell(0).SetCellValue("编号");
        sheet1.GetRow(0).CreateCell(1).SetCellValue("物料代码");
        sheet1.GetRow(0).CreateCell(2).SetCellValue("物料名称");
        sheet1.GetRow(0).CreateCell(3).SetCellValue("库位");
        sheet1.GetRow(0).CreateCell(4).SetCellValue("库位描述");
        sheet1.GetRow(0).CreateCell(5).SetCellValue("单位");
        sheet1.GetRow(0).CreateCell(6).SetCellValue("数量");

        

        DataSet ds = (DataSet)Session["ds"];
        for (int i = 1; i <= ds.Tables[0].Rows.Count; i++)
        {
            sheet1.CreateRow(i).CreateCell(0).SetCellValue(Convert.ToString(i));
            sheet1.GetRow(i).CreateCell(1).SetCellValue(ds.Tables[0].Rows[i - 1][0].ToString());
            sheet1.GetRow(i).CreateCell(2).SetCellValue(ds.Tables[0].Rows[i - 1][1].ToString());
            sheet1.GetRow(i).CreateCell(3).SetCellValue(ds.Tables[0].Rows[i - 1][2].ToString());
            sheet1.GetRow(i).CreateCell(4).SetCellValue(ds.Tables[0].Rows[i - 1][3].ToString());
            sheet1.GetRow(i).CreateCell(5).SetCellValue(ds.Tables[0].Rows[i - 1][4].ToString());
            sheet1.GetRow(i).CreateCell(6).SetCellValue(ds.Tables[0].Rows[i - 1][5].ToString());
        }
        sheet1.AutoSizeColumn(0);
        sheet1.AutoSizeColumn(1);
        sheet1.AutoSizeColumn(2);
        sheet1.AutoSizeColumn(3);
        sheet1.AutoSizeColumn(4);
        sheet1.AutoSizeColumn(5);
        sheet1.AutoSizeColumn(6);

      //  string fileplace = Request.PhysicalApplicationPath;
      //  string filename = "temp(" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ").xls";
      //  FileStream file = new FileStream(@fileplace+"\\Reports\\Templates\\TempFiles\\"+filename, FileMode.Create);       
    //    hssfworkbook.Write(file);       
     //   file.Close();
        MemoryStream ms = new MemoryStream();
        hssfworkbook.Write(ms);
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=TempWorkBook.xls"));
        Response.BinaryWrite(ms.ToArray());

        hssfworkbook = null;
        ms.Close();
        ms.Dispose();
    }
}