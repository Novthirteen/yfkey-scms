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

public partial class Inventory_BatchTransfer_Import : ModuleBase
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        }

        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
    }


    protected void btnImport_Click(object sender, EventArgs e)
    {
        this.Import();
    }


    private void Import()
    {
        try
        {
            string flowCode = this.tbFlow.Text.Trim() != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
            OrderHead orderHead = TheImportMgr.ReadBatchTransferFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser, flowCode);
            Receipt receipt = TheOrderMgr.QuickReceiveOrder(flowCode, orderHead.OrderDetails,this.CurrentUser.Code);
            this.ShowSuccessMessage("Receipt.Receive.Successfully", receipt.ReceiptNo);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }
}
