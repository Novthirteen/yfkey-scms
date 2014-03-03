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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.Mes;

public partial class Mes_ProductLineUser_ProductLineUser : ModuleBase
{
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string code)
    {

        this.tbUser.Text = code;

        this.ODS_ProductLinesInUser.SelectParameters["code"].DefaultValue = code;
        this.ODS_ProductLinesNotInUser.SelectParameters["code"].DefaultValue = code;
    
        this.CBL_NotInProductLine.DataBind();
        this.CBL_InProductLine.DataBind();

    }

    protected void ToInBT_Click(object sender, EventArgs e)
    {
        List<Flow> pList = new List<Flow>();
        foreach (ListItem item in this.CBL_NotInProductLine.Items)
        {
            if (item.Selected)
            {
                ProductLineUser pu = new ProductLineUser();
                pu.DeliveryUser = TheUserMgr.LoadUser(this.tbUser.Text.Trim());
                pu.ProductLine = TheFlowMgr.LoadFlow(item.Value);
                TheProductLineUserMgr.CreateProductLineUser(pu);
            }
        }

        this.CBL_NotInProductLine.DataBind();
        this.CBL_InProductLine.DataBind();
        this.cb_InProductLine.Checked = false;
        this.cb_NotInProductLine.Checked = false;
    }

    protected void ToOutBT_Click(object sender, EventArgs e)
    {
        List<ProductLineUser> upList = new List<ProductLineUser>();
        foreach (ListItem item in this.CBL_InProductLine.Items)
        {
            if (item.Selected)
            {
                ProductLineUser userProductLine = TheProductLineUserMgr.LoadProductLineUser(this.tbUser.Text.Trim(), item.Value);
                upList.Add(userProductLine);
            }
        }
        if (upList.Count > 0)
        {
            TheProductLineUserMgr.DeleteProductLineUser(upList);
        }
        this.CBL_NotInProductLine.DataBind();
        this.CBL_InProductLine.DataBind();
        this.cb_InProductLine.Checked = false;
        this.cb_NotInProductLine.Checked = false;
        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (this.rfvUser.IsValid)
        {
            this.ODS_ProductLinesInUser.SelectParameters["code"].DefaultValue = this.tbUser.Text;
            this.ODS_ProductLinesNotInUser.SelectParameters["code"].DefaultValue = this.tbUser.Text;

            this.CBL_NotInProductLine.DataBind();
            this.CBL_InProductLine.DataBind();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

  
}
