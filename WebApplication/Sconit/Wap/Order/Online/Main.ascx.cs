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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;

public partial class Wap_Order_Online_Main : MainModuleBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Todo工单权限过滤
            this.gvOnline.DataSource = TheOrderHeadMgr.GetOrderHead(10, BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS);
            this.gvOnline.DataBind();
        }
    }
    protected void btnOrderNo_Click(object sender, EventArgs e)
    {
        TheOrderMgr.StartOrder(this.tbOrderNo.Text.Trim(), this.CurrentUser);
        this.gvOnline.DataBind();
    }
}
