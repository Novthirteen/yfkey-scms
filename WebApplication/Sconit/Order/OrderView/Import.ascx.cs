using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;

public partial class Inventory_OrderView_Import : ModuleBase
{
    public event EventHandler BtnImportEvent;

    public string OrderNo
    {
        get
        {
            return (string)ViewState["OrderNo"];
        }
        set
        {
            ViewState["OrderNo"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
        
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            IList<OrderLocationTransaction> orderLoctransList = TheImportMgr.ReadOrderLocationTransactionFromXls(fileUpload.PostedFile.InputStream, OrderNo);
            TheOrderMgr.AddOrderLocationTransaction(orderLoctransList, this.CurrentUser);
            ShowSuccessMessage("Import.Result.Successfully");
            if (BtnImportEvent != null)
            {
                this.Visible = false;
                BtnImportEvent(sender, e);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

}
