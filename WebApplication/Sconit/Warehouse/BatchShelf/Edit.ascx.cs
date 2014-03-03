using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;

using NHibernate.Expression;
public partial class Warehouse_BatchShelf_Edit : BusinessModuleBase
{


    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbMiscOrderRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.InitialAll();
            InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKUP);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected override void BindTransformer()
    {
    }

    protected void LoadItem(object sender, EventArgs e)
    {
           
        DetachedCriteria criter = DetachedCriteria.For(typeof(LocationLotDetail));
        criter.Add(Expression.Eq("Location",TheLocationMgr.LoadLocation(tbMiscOrderLocation.Text)));
        criter.Add(Expression.In("StorageBin", TheStorageBinMgr.GetStorageBin(tbStorageBin.Text).ToList()));
        criter.Add(Expression.IsNotNull("StorageBin"));
        IList<LocationLotDetail> result = TheCriteriaMgr.FindAll<LocationLotDetail>(criter);
          


        GridView1.DataSource = result;
        GridView1.DataBind();
    }


    protected override void BindTransformerDetail()
    {
       // this.ucList.BindList(this.CacheResolver.Transformers[0].TransformerDetails);
    }

    protected void tbScanBarcode_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;
       // this.HuInput(this.tbScanBarcode.Text.Trim());
      //  this.tbScanBarcode.Text = string.Empty;
        //this.tbScanBarcode.Focus();
    }

    protected void HuInput(string huId)
    {
        try
        {
            ResolveInput(huId);
        }
        catch (BusinessErrorException ex)
        {
            this.lblMessage.Text = TheLanguageMgr.TranslateMessage(ex.Message, this.CurrentUser, ex.MessageParams);
        }
    }

    protected void btnPickup_Click(object sender, EventArgs e)
    {
        try
        {
          //  ExecuteSubmit();
            DetachedCriteria criter = DetachedCriteria.For(typeof(LocationLotDetail));
            criter.Add(Expression.Eq("Location", TheLocationMgr.LoadLocation(tbMiscOrderLocation.Text)));
            criter.Add(Expression.In("StorageBin", TheStorageBinMgr.GetStorageBin(tbStorageBin.Text).ToList()));
            criter.Add(Expression.IsNotNull("StorageBin"));
            IList<LocationLotDetail> result = TheCriteriaMgr.FindAll<LocationLotDetail>(criter);
            foreach (LocationLotDetail lot in result)
            {
                lot.StorageBin = null;
                lot.Hu.StorageBin = null;
                TheLocationLotDetailMgr.UpdateLocationLotDetail(lot);
               
            }
                
            ShowSuccessMessage("Warehouse.PickUp.Successfully");
            GridView1.DataSource = null;
            GridView1.DataBind();
            this.InitialAll();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    private void InitialAll()
    {
        this.lblMessage.Text = string.Empty;
        this.ucList.BindList(null);
      //  this.tbScanBarcode.Focus();
    }
}
