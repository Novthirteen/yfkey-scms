using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class Inventory_PrintHu_InventoryList : ModuleBase
{
    public event EventHandler PrintEvent;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //按Hu单位显示库存数量
            LocationLotDetail locationLotDetail = (LocationLotDetail)e.Row.DataItem;

            decimal qty = decimal.Parse(e.Row.Cells[10].Text);  //库存单位
            e.Row.Cells[10].Text = (qty / locationLotDetail.Hu.UnitQty).ToString("F2"); //转为Hu单位
        }
    }

    public void InitPageParameter(IList<LocationLotDetail> huLocationLotDetailList)
    {
        this.GV_List.DataSource = huLocationLotDetailList;
        this.GV_List.DataBind();
    }

    public void PrintCallBack()
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            IList<Hu> huList = new List<Hu>();

            foreach (GridViewRow row in this.GV_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfHuId = row.FindControl("hfHuId") as HiddenField;

                    Hu hu = TheHuMgr.LoadHu(hfHuId.Value);
                    huList.Add(hu);
                }
            }

            this.PrintEvent(huList, null);

            return;
        }

        this.PrintEvent(null, null);
    }

}
