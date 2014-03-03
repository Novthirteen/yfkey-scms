using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using System.Collections;

public partial class Production_Backflush_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbProductLine.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            IDictionary<string, decimal> itemDictionary = GetItemDictionary();
            if (itemDictionary.Count == 0)
            {
                ShowErrorMessage("MasterData.BackFlush.Item.Empty");
                return;
            }
            this.TheProductLineInProcessLocationDetailMgr.RawMaterialBackflush(this.tbProductLine.Text, GetItemDictionary(), this.CurrentUser);
            this.ShowSuccessMessage("MasterData.BackFlush.Successfully", this.tbProductLine.Text);
            InitPageParameter();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    private void InitPageParameter()
    {
        IList<ProductLineInProcessLocationDetail> productLineIpList = new List<ProductLineInProcessLocationDetail>();
        if (tbProductLine.Text.Trim() != string.Empty)
        {
            productLineIpList = TheProductLineInProcessLocationDetailMgr.GetProductLineInProcessLocationDetailGroupByItem(this.tbProductLine.Text.Trim(), BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);
        }

        this.GV_List.DataSource = productLineIpList;
        this.GV_List.DataBind();
    }

    protected void tbProductLine_TextChanged(object sender, EventArgs e)
    {
        InitPageParameter();
    }


    private IDictionary<string, decimal> GetItemDictionary()
    {
        IDictionary<string, decimal> itemDictionary = new Dictionary<string, decimal>();

        for (int i = 0; i < this.GV_List.Rows.Count; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];
              CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
              if (checkBoxGroup.Checked)
              {
                  string itemCode = ((Label)row.FindControl("lblItemCode")).Text.Trim();

                  string remainQty = ((TextBox)row.FindControl("tbRemainQty")).Text.Trim();
                  if (remainQty == string.Empty)
                  {
                      remainQty = "0";
                  }

                  itemDictionary.Add(itemCode, decimal.Parse(remainQty));
              }

        }
        return itemDictionary;

    }
}
