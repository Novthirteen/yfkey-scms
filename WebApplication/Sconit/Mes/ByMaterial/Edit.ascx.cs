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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Control;
using com.Sconit.Entity;
using com.Sconit.Service.Procurement;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Mes;

public partial class Mes_ByMaterial_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(int id)
    {
        ByMaterial byMaterial = TheByMaterialMgr.LoadByMaterial(id);
        this.tbOrderNo.Text = byMaterial.OrderNo;
        this.tbTagNo.Text = byMaterial.TagNo;
        this.tbItem.Text = byMaterial.Item.Code;
        this.tbCreateDate.Text = byMaterial.CreateDate.ToShortDateString();
        this.tbCreateUser.Text = byMaterial.CreateUser.Name;
    }


}
