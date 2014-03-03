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
using System.Data.SqlClient;
public partial class Reports_Inv : MainModuleBase
{
    private string ConnString = string.Empty;
    string sqlText = string.Empty;
    DataSet dsOrderQuery = new DataSet();



    public Reports_Inv()
    {
    }

    protected DataSet sqltods(string sqltext)
    {
        if (sqltext == string.Empty)
            sqltext = "select * from reportsinv";
        XmlTextReader reader = new XmlTextReader(Server.MapPath("Config/properties.config"));
        XmlDocument doc = new XmlDocument();
        doc.Load(reader);//  
        reader.Close();//
        ConnString = doc.SelectSingleNode("/configuration/properties/connectionString").InnerText.Trim();
        SqlConnection con = new SqlConnection(ConnString);
        SqlCommand cmd = new SqlCommand(sqltext, con);
        cmd.CommandTimeout = 0;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsOrderQuery);
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
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
        Label1.Text = "";
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
            //BindData(sqltods(sqlText));
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        btn1_Click(sender, e);
    }

    protected void btn1_Click(object sender, EventArgs e)
    {




        sqlText = "select * from reportsinv where 1=1";
        if (tbLocation.Text.Trim() != string.Empty)
            sqlText += " and location='" + tbLocation.Text.Trim() + "'";
        else
        {

            var i = (from a in TheLocationMgr.GetLocationByUserCode(CurrentUser.Code) select a.Code).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var t in i)
            {
                sb.Append("'"+t + "',");
            }
            string regions = sb.ToString().TrimEnd(new char[] { ',' });
            sqlText += " and location in (" + regions + ")";
        }
        if (tbItem.Text.Trim() != string.Empty)
            sqlText += " and item='" + tbItem.Text.Trim() + "'";

        BindData(sqltods(sqlText));
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        tbItem.Text = "";
        tbLocation.Text = "";
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        btn1_Click(sender, e);
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        Sheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
        sheet1.CreateRow(0).CreateCell(0).SetCellValue("NO");
        sheet1.GetRow(0).CreateCell(1).SetCellValue("Site");
        sheet1.GetRow(0).CreateCell(2).SetCellValue("Item");
        sheet1.GetRow(0).CreateCell(3).SetCellValue("Desc");
        sheet1.GetRow(0).CreateCell(4).SetCellValue("Uom");
        sheet1.GetRow(0).CreateCell(5).SetCellValue("Location");
        sheet1.GetRow(0).CreateCell(6).SetCellValue("Bin");
        sheet1.GetRow(0).CreateCell(7).SetCellValue("LotNo");
        sheet1.GetRow(0).CreateCell(8).SetCellValue("Qty");


        DataSet ds = (DataSet)Session["ds"];
        for (int i = 1; i <= ds.Tables[0].Rows.Count; i++)
        {
            sheet1.CreateRow(i).CreateCell(0).SetCellValue(Convert.ToString(i));
            sheet1.GetRow(i).CreateCell(1).SetCellValue(ds.Tables[0].Rows[i - 1][1].ToString());
            sheet1.GetRow(i).CreateCell(2).SetCellValue(ds.Tables[0].Rows[i - 1][2].ToString());
            sheet1.GetRow(i).CreateCell(3).SetCellValue(ds.Tables[0].Rows[i - 1][3].ToString());
            sheet1.GetRow(i).CreateCell(4).SetCellValue(ds.Tables[0].Rows[i - 1][4].ToString());
            sheet1.GetRow(i).CreateCell(5).SetCellValue(ds.Tables[0].Rows[i - 1][5].ToString());
            sheet1.GetRow(i).CreateCell(6).SetCellValue(ds.Tables[0].Rows[i - 1][6].ToString());
            sheet1.GetRow(i).CreateCell(7).SetCellValue(ds.Tables[0].Rows[i - 1][7].ToString());
            sheet1.GetRow(i).CreateCell(8).SetCellValue(ds.Tables[0].Rows[i - 1][8].ToString());
        }
        sheet1.AutoSizeColumn(0);
        sheet1.AutoSizeColumn(1);
        sheet1.AutoSizeColumn(2);
        sheet1.AutoSizeColumn(3);
        sheet1.AutoSizeColumn(4);
        sheet1.AutoSizeColumn(5);
        sheet1.AutoSizeColumn(6);
        sheet1.AutoSizeColumn(7);
        sheet1.AutoSizeColumn(8);

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