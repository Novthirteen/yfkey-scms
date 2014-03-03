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
using com.Sconit.Entity.MasterData;
using com.Sconit.Control;
using com.Sconit.Entity;
using com.Sconit.Entity.Mes;
using com.Sconit.Entity.Exception;

public partial class Mes_ByMaterial_New : NewModuleBase
{
  

    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void PageCleanup()
    {
        this.tbOrderNo.Text = string.Empty;
        this.tbTagNo.Text = string.Empty;
        this.tbItem.Text = string.Empty;
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            string orderNo = this.tbOrderNo.Text.Trim();
            string itemCode = this.tbItem.Text.Trim();
            string tagNo = this.tbTagNo.Text.Trim();

            if (!this.rfvItem.IsValid || !this.rfvOrderNo.IsValid || !this.rfvTagNo.IsValid)
            {
                return;
            }

            ByMaterial byMaterial = new ByMaterial();
            if (byMaterial != null)
            {
                byMaterial.Item = TheItemMgr.LoadItem(itemCode);
                byMaterial.OrderNo = orderNo;
                byMaterial.TagNo = tagNo;
                byMaterial.CreateDate = DateTime.Now;
                byMaterial.CreateUser = this.CurrentUser;
            }

            TheByMaterialMgr.CreateByMaterial(byMaterial);
            ShowSuccessMessage("Mes.ByMaterial.Insert.Successfully");
            if (CreateEvent != null)
            {
                CreateEvent(byMaterial.Id, e);
            }
        } 
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
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
