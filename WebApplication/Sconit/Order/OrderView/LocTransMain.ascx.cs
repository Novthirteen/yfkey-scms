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
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using System.Collections.Generic;

public partial class Order_OrderView_LocTransMain : MainModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler UpdateRoutingEvent;

    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }

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

    //新品
    public bool NewItem
    {
        get
        {
            return (bool)ViewState["NewItem"];
        }
        set
        {
            ViewState["NewItem"] = value;
        }
    }

    //报废
    public bool IsScrap
    {
        get
        {
            return (bool)ViewState["IsScrap"];
        }
        set
        {
            ViewState["IsScrap"] = value;
        }
    }

    //原材料回用
    public bool IsReuse
    {
        get
        {
            return (bool)ViewState["IsReuse"];
        }
        set
        {
            ViewState["IsReuse"] = value;
        }
    }

    public void InitPageParameter(string orderNo)
    {

        this.OrderNo = orderNo;

        #region OrderLocationInTransaction
        this.ucLocInTransList.OrderNo = orderNo;
        this.ucLocInTransList.Visible = true;
        this.ucLocInTransList.IOType = BusinessConstants.IO_TYPE_IN;
        this.ucLocInTransList.UpdateView();
        #endregion

        #region OrderLocationOutTransaction
        this.ucLocOutTransList.OrderNo = orderNo;
        this.ucLocOutTransList.Visible = true;
        this.ucLocOutTransList.IOType = BusinessConstants.IO_TYPE_OUT;
        this.ucLocOutTransList.UpdateView();
        #endregion

        #region 保存按钮
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            OrderHead orderhead = TheOrderHeadMgr.LoadOrderHead(orderNo);

            this.btnSave.Visible = orderhead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
            this.btnImport.Visible = this.IsScrap && !this.IsReuse && (orderhead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);

        }
        #endregion
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucLocOutTransList.EditEvent += new System.EventHandler(this.Edit_Render);
        this.ucLocOutTransList.UpdateRoutingEvent += new System.EventHandler(this.UpdateRouting_Render);
        this.ucAbstractItemBomDetail.SaveEvent += new System.EventHandler(this.AbstractItemSave_Render);
        this.ucImport.BtnImportEvent += new System.EventHandler(this.ImportClick_Render);
        if (!IsPostBack)
        {
            this.ucLocInTransList.ModuleType = this.ModuleType;
            this.ucLocInTransList.ModuleSubType = this.ModuleSubType;

            this.ucLocOutTransList.ModuleType = this.ModuleType;
            this.ucLocOutTransList.ModuleSubType = this.ModuleSubType;

            if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                this.IsScrap = false;
                this.IsReuse = false;
            }

            this.ucLocInTransList.IsScrap = this.IsScrap;
            this.ucLocOutTransList.IsScrap = this.IsScrap;
            this.ucLocInTransList.IsReuse = this.IsReuse;
            this.ucLocOutTransList.IsReuse = this.IsReuse;
            this.ucLocInTransList.NewItem = this.NewItem;
            this.ucLocOutTransList.NewItem = this.NewItem;
        }


    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.ucLocOutTransList.SaveAllDetail();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        this.ucImport.OrderNo = this.OrderNo;
        this.ucImport.Visible = true;
    }


    protected void UpdateRouting_Render(object sender, EventArgs e)
    {
        UpdateRoutingEvent(sender, e);

    }

    protected void ImportClick_Render(object sender, EventArgs e)
    {
        InitPageParameter(this.OrderNo);

    }

    protected void Edit_Render(object sender, EventArgs e)
    {
        this.ucAbstractItemBomDetail.Visible = true;
        this.ucAbstractItemBomDetail.LocTransId = (int)((object[])sender)[1];
        this.ucAbstractItemBomDetail.InitPageParameter((string)((object[])sender)[0]);
    }

    protected void AbstractItemSave_Render(object sender, EventArgs e)
    {
        InitPageParameter((string)sender);
    }

    public IList<Hu> GetLocTransHuList()
    {
        return this.ucLocOutTransList.huList;
    }

}
