using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;

public partial class FileUpload : ModuleBase
{
    public event EventHandler UploadEvent;

    public string TargetControlId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (UploadEvent != null)
        {
            UploadEvent(fileUpload.PostedFile.InputStream, null);
        }

        //todo 需要从企业参数中获取上传文件路径
        //String filepath = Server.MapPath("~") + "\\UploadFiles";

        //if (this.fileUpload.FileName != "" && this.fileUpload.FileContent.Length != 0)
        //{
        //    string fileExtension = Path.GetExtension(this.fileUpload.FileName);
        //    string randomFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + fileExtension;
        //    string fileFullPath = filepath + "\\" + randomFileName;
        //    fileUpload.PostedFile.SaveAs(fileFullPath);

        //    com.Sconit.Entity.MasterData.FileUpload fu = new com.Sconit.Entity.MasterData.FileUpload();
        //    fu.FullPath = fileFullPath;
        //    fu.Name = this.fileUpload.FileName;
        //    fu.Extension = fileExtension;
        //    fu.CreateDate = DateTime.Now;
        //    fu.CreateUser = CurrentUser;
        //    TheFileUploadMgr.CreateFileUpload(fu);

        //    Control targetControl = FindControl(TargetControlId);

        //    if (targetControl != null)
        //    {
        //        if (targetControl.GetType() == typeof(HiddenField))
        //        {
        //            ((HiddenField)targetControl).Value = fu.Id.ToString();
        //        }
        //        else if (targetControl.GetType() == typeof(TextBox))
        //        {
        //            ((TextBox)targetControl).Text = fu.Id.ToString();
        //        }

        //        this.ShowSuccessMessage("Common.FileUpload.Successful", this.fileUpload.FileName);
        //    }
        //}
    }

    //private string GenerateRandomFileName()
    //{
    //    Random random = new Random();
    //    return DateTime.Now.ToString("yyyyMMddHHmmssffff");//修复文件名问题
    //}
}
