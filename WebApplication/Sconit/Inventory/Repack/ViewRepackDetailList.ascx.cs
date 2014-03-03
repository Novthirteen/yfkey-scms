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
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Procurement;
using com.Sconit.Entity.Distribution;

public partial class Inventory_Repack_ViewRepackDetailList : ModuleBase
{

    public string RepackType
    {
        get
        {
            return (string)ViewState["RepackType"];
        }
        set
        {
            ViewState["RepackType"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public void InitPageParameter(string repackNo)
    {
        IList<TransformerDetail> repackDetailInList = new List<TransformerDetail>();
        IList<TransformerDetail> repackDetailOutList = new List<TransformerDetail>();
        if (repackNo != string.Empty)
        {
              Repack repack = TheRepackMgr.LoadRepack(repackNo, true);
              IList<TransformerDetail> transformerDetailList = TransformerHelper.ConvertRepackDetailListToTransformerDetailList(repack.RepackDetails);
              foreach (TransformerDetail transformerDetail in transformerDetailList)
              {
                  if (transformerDetail.IOType == BusinessConstants.IO_TYPE_IN)
                  {
                      repackDetailInList.Add(transformerDetail);
                  }
                  else if (transformerDetail.IOType == BusinessConstants.IO_TYPE_OUT)
                  {
                      repackDetailOutList.Add(transformerDetail);
                  }
              }
        }

        this.GV_InList.DataSource = repackDetailInList;
        this.GV_InList.DataBind();
        this.GV_OutList.DataSource = repackDetailOutList;
        this.GV_OutList.DataBind();

    }

  
}
