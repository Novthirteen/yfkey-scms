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

public partial class Hu_Inbound_Upload : ModuleBase
{
    public event EventHandler ListResultEvent;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    IList<Hu> huList = TheHuMgr.InboundHu(fileUpload.PostedFile.InputStream, this.CurrentUser, this.tbOrderNo.Text.Trim());
        //    IList<HuDetail> huDetailList = new List<HuDetail>();

        //    if (huList != null && huList.Count > 0)
        //    {
        //        foreach (Hu hu in huList)
        //        {
        //            foreach (HuDetail huDetail in hu.HuDetails)
        //            {
        //                huDetailList.Add(huDetail);
        //            }
        //        }
        //    }

        //    ListResultEvent(huDetailList, e);

        //    if (huDetailList != null && huDetailList.Count > 0)
        //    {
        //        this.ShowSuccessMessage("Hu.Inbound.Successful");
        //    }
        //    else
        //    {
        //        this.ShowErrorMessage("Hu.Inbound.EmptyFileContent");
        //    }
        //}
        //catch (BusinessErrorException ex)
        //{
        //    this.ShowErrorMessage(ex);
        //}
    }
}
