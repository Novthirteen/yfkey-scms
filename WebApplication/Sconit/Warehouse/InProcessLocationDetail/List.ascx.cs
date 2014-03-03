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
using com.Sconit.Service.Distribution;
using System.Collections.Generic;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity;
using com.Sconit.Entity.Procurement;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;

public partial class Warehouse_InProcessLocation_List : ModuleBase
{
    public event EventHandler BackEvent;

    #region ViewState
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string ipNo)
    {
        InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(ipNo, true);
        this.InitPageParameter(ip);
    }
    public void InitPageParameter(InProcessLocation ip)
    {
        bool isScanHu = (ip.InProcessLocationDetails[0].HuId != null);
        List<Transformer> transformerList = TransformerHelper.ConvertInProcessLocationDetailsToTransformers(ip.InProcessLocationDetails);
        this.ucTransformer.InitPageParameter(transformerList, this.ModuleType, BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPVIEW, isScanHu);
        this.ucTransformer.InitialUIForAsn(ip);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }
}
