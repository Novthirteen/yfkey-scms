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
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using com.Sconit.Utility;

public partial class MasterData_Item_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string ItemCode
    {
        get
        {
            return (string)ViewState["ItemCode"];
        }
        set
        {
            ViewState["ItemCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void FV_Item_DataBound(object sender, EventArgs e)
    {
        //Item item = ItemMgr.LoadItem(ItemCode);

        Item item = (Item)(ItemBase)(((FormView)(sender)).DataItem);
        if (item != null)
        {
            ((TextBox)(this.FV_Item.FindControl("tbUom"))).Text = (item.Uom == null) ? string.Empty : item.Uom.Code;
            ((CodeMstrDropDownList)(this.FV_Item.FindControl("ddlType"))).SelectedValue = item.Type;
            ((Controls_TextBox)(this.FV_Item.FindControl("tbLocation"))).Text = (item.Location) == null ? string.Empty : item.Location.Code;
            ((Controls_TextBox)(this.FV_Item.FindControl("tbBom"))).Text = (item.Bom == null) ? string.Empty : item.Bom.Code;
            ((Controls_TextBox)(this.FV_Item.FindControl("tbRouting"))).Text = (item.Routing == null) ? string.Empty : item.Routing.Code;
            ((System.Web.UI.WebControls.Image)(this.FV_Item.FindControl("imgUpload"))).ImageUrl = (item.ImageUrl == null || item.ImageUrl.Trim() == string.Empty) ? "~/Images/Transparent.gif" : item.ImageUrl;
            ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Item.FindControl("ddlItemCategory"))).SelectedValue = item.Category;
            ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Item.FindControl("ddlPlantType"))).SelectedValue = item.Plant;
            
            if (item.ImageUrl == null || item.ImageUrl.Trim() == string.Empty)
            {
                ((CheckBox)(this.FV_Item.FindControl("cbDeleteImage"))).Visible = false;
                ((Literal)(this.FV_Item.FindControl("ltlDeleteImage"))).Visible = false;
            }
        }
    }

    public void InitPageParameter(string code)
    {
        this.ItemCode = code;
        this.ODS_Item.SelectParameters["code"].DefaultValue = this.ItemCode;
        this.ODS_Item.DataBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Item_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.Item.UpdateItem.Successfully", ItemCode);
    }

    protected void ODS_Item_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Item item = (Item)e.InputParameters[0];

        item.Desc1 = item.Desc1.Trim();
        item.Desc2 = item.Desc2.Trim();
        item.Memo = item.Memo.Trim();

        item.Type = ((CodeMstrDropDownList)(this.FV_Item.FindControl("ddlType"))).SelectedValue;

        string uom = ((TextBox)(this.FV_Item.FindControl("tbUom"))).Text.Trim();
        item.Uom = TheUomMgr.LoadUom(uom);

        string location = ((Controls_TextBox)(this.FV_Item.FindControl("tbLocation"))).Text.Trim();
        item.Location = TheLocationMgr.LoadLocation(location);

        string bom = ((Controls_TextBox)(this.FV_Item.FindControl("tbBom"))).Text.Trim();
        item.Bom = TheBomMgr.LoadBom(bom);

        string routing = ((Controls_TextBox)(this.FV_Item.FindControl("tbRouting"))).Text.Trim();
        item.Routing = TheRoutingMgr.LoadRouting(routing);

        decimal uc = item.UnitCount;
        uc = System.Decimal.Round(uc, 8);
        if (uc == 0)
        {
            ShowErrorMessage("MasterData.Item.UC.Zero");
            e.Cancel = true;
        }

        com.Sconit.Control.CodeMstrDropDownList ddlItemCategory = (com.Sconit.Control.CodeMstrDropDownList)(this.FV_Item.FindControl("ddlItemCategory"));
        if (ddlItemCategory.SelectedIndex != 0)
        {
            item.Category = ddlItemCategory.SelectedValue;
        }

        string imageUrl;
        string imgUpload = ((System.Web.UI.WebControls.Image)(this.FV_Item.FindControl("imgUpload"))).ImageUrl;

        if (((CheckBox)(this.FV_Item.FindControl("cbDeleteImage"))).Checked == true)
        {
            imageUrl = null;
            if (File.Exists(Server.MapPath(imgUpload)))
            {
                File.Delete(Server.MapPath(imgUpload));
            }
        }
        else
        {
            imageUrl = UploadItemImage(item.Code);
            if (imageUrl == null)
            {
                imageUrl = imgUpload;
            }
        }

        item.ImageUrl = imageUrl;
        item.LastModifyDate = DateTime.Now;
        item.LastModifyUser = this.CurrentUser;
    }
    protected void ODS_Item_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //DeleteItem = (Item)e.InputParameters[0];
    }

    protected void ODS_Item_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            string imgUpload = ((System.Web.UI.WebControls.Image)(this.FV_Item.FindControl("imgUpload"))).ImageUrl;
            if (File.Exists(Server.MapPath(imgUpload)))
            {
                File.Delete(Server.MapPath(imgUpload));
            }
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Item.DeleteItem.Successfully", ItemCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Item.DeleteItem.Fail", ItemCode);
            e.ExceptionHandled = true;
        }
    }

    private string UploadItemImage(string itemCode)
    {
        string mapPath = TheEntityPreferenceMgr.LoadEntityPreference("ItemImageDir").Value;//"~/Images/Item/";
        string filePath = Server.MapPath(mapPath);
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        System.Web.UI.WebControls.FileUpload fileUpload = (System.Web.UI.WebControls.FileUpload)(this.FV_Item.FindControl("fileUpload"));
        Literal lblUploadMessage = (Literal)(this.FV_Item.FindControl("lblUploadMessage"));

        if (fileUpload.HasFile)
        {
            if (fileUpload.FileName != "" && fileUpload.FileContent.Length != 0)
            {
                string fileExtension = Path.GetExtension(fileUpload.FileName);
                if (fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".jpg")
                {
                    string fileName = itemCode + ".jpg";
                    string fileFullPath = filePath + "\\" + fileName;

                    #region 调整图片大小
                    AdjustImageHelper.AdjustImage(150, fileFullPath, fileUpload.FileContent);
                    #endregion

                    if (File.Exists(fileFullPath))
                    {
                        ShowWarningMessage("MasterData.Item.AddImage.Replace", fileName);
                    }
                    else
                    {
                        ShowSuccessMessage("MasterData.Item.AddImage.Successfully", fileName);
                    }
                    return mapPath + fileName;
                }
                else
                {
                    ShowWarningMessage("MasterData.Item.AddImage.UnSupportFormat");
                    return null;
                }
            }
        }
        return null;
    }
}
