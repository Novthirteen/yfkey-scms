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
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using com.Sconit.Utility;

public partial class MasterData_Item_New : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;

    private Item item;

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Controls_TextBox)(this.FV_Item.FindControl("tbLocation"))).ServiceParameter = "string:" + this.CurrentUser.Code + ",string:";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void PageCleanup()
    {
        ((TextBox)(this.FV_Item.FindControl("tbMaxStock"))).Text = string.Empty;
        ((TextBox)(this.FV_Item.FindControl("tbLeadTime"))).Text = string.Empty;
        ((TextBox)(this.FV_Item.FindControl("tbSafeStock"))).Text = string.Empty;
        ((TextBox)(this.FV_Item.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Item.FindControl("tbCount"))).Text = "1";
        ((CheckBox)(this.FV_Item.FindControl("tbIsActive"))).Checked = true;
        ((TextBox)(this.FV_Item.FindControl("tbDesc1"))).Text = string.Empty;
        ((TextBox)(this.FV_Item.FindControl("tbDesc2"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_Item.FindControl("tbUom"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_Item.FindControl("tbLocation"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_Item.FindControl("tbBom"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_Item.FindControl("tbRouting"))).Text = string.Empty;
        ((TextBox)(this.FV_Item.FindControl("tbMemo"))).Text = string.Empty;
        ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Item.FindControl("ddlItemCategory"))).SelectedIndex = 0;
    }

    protected void ODS_Item_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        item = (Item)e.InputParameters[0];

        item.Desc1 = item.Desc1.Trim();
        item.Desc2 = item.Desc2.Trim();
        item.Memo = item.Memo.Trim();

        if (item.Code == null || item.Code.Trim() == string.Empty)
        {
            ShowErrorMessage("MasterData.Item.Code.Empty");
            e.Cancel = true;
            return;
        }
        else
        {
            item.Code = item.Code.Trim();
        }

        if (TheItemMgr.LoadItem(item.Code) != null)
        {
            e.Cancel = true;
            ShowErrorMessage("MasterData.Item.CodeExist", item.Code);
            return;
        }

        string uom = ((Controls_TextBox)(this.FV_Item.FindControl("tbUom"))).Text.Trim() == string.Empty ? "EA"
            : ((Controls_TextBox)(this.FV_Item.FindControl("tbUom"))).Text.Trim();
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
        item.ImageUrl = UploadItemImage(item.Code);
        item.LastModifyDate = DateTime.Now;
        item.LastModifyUser = this.CurrentUser;
    }

    protected void ODS_Item_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(item.Code, e);
            ShowSuccessMessage("MasterData.Item.AddItem.Successfully", item.Code);
        }
    }

    protected void checkItemExists(object source, ServerValidateEventArgs args)
    {
        string code = ((TextBox)(this.FV_Item.FindControl("tbCode"))).Text;

        if (TheItemMgr.LoadItem(code) != null)
        {
            ShowErrorMessage("MasterData.Item.CodeExist", code);
            args.IsValid = false;
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
