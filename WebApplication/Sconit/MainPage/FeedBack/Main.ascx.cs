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
using System.Net.Mail;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;

public partial class MainPage_FeedBack_Main : MainModuleBase
{

    protected string UrlReferrer
    {
        get
        {
            return (string)ViewState["UrlReferrer"];
        }
        set
        {
            ViewState["UrlReferrer"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
            {
                UrlReferrer = Request.UrlReferrer.ToString();
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(UrlReferrer.ToString());
    }

    protected void Submit_Click(object sender, EventArgs e)
    {        
        if (Page.IsValid)
        {
            if (this.tbsubject.Text.Trim() == string.Empty || this.Content.Content == string.Empty)
            {
                ShowErrorMessage("MasterData.FeedBack.Empty");
                return;
            }

            this.Submit.Enabled = false;

            string subject = string.Empty;
            string emailFrom = string.Empty;
            if (this.CurrentUser.Email == null || this.CurrentUser.Email.Trim() == string.Empty)
            {
                emailFrom = "Sconit@Sconit.com";
            }
            else
            {
                emailFrom = this.CurrentUser.Email.Trim();
            }

            subject = "[" + this.rblType.SelectedValue + "]";
            subject += this.tbsubject.Text;

            string fileName = Server.MapPath("~/App_Data/ContactForm.txt");
            string mailBody = System.IO.File.ReadAllText(fileName);

            mailBody = mailBody.Replace("##Company##", TheEntityPreferenceMgr.LoadEntityPreference("CompanyName").Value);
            mailBody = mailBody.Replace("##Account##", this.CurrentUser.Code);
            mailBody = mailBody.Replace("##Name##", this.CurrentUser.Name);
            mailBody = mailBody.Replace("##Email##", this.CurrentUser.Email);
            mailBody = mailBody.Replace("##Phone##", this.CurrentUser.Phone);
            mailBody = mailBody.Replace("##Mobile##", this.CurrentUser.MobliePhone);
            mailBody = mailBody.Replace("##Address##", this.CurrentUser.Address);
            mailBody = mailBody.Replace("##UrlReferrer##", this.UrlReferrer);
            mailBody = mailBody.Replace("##Type##", this.rblType.SelectedValue);
            mailBody = mailBody.Replace("##Comments##", this.Content.Content);

            if (SendSMTPEMail(subject, mailBody, emailFrom))
            {
                ShowSuccessMessage("MainPage.FeedBack.Success");
                //Response.Redirect(UrlReferrer.ToString());
                this.Timer1.Enabled = true;
            }
            else
            {
                ShowErrorMessage("MainPage.FeedBack.Error");
                this.Submit.Enabled = true;
            }
        }
    }
    
    private bool SendSMTPEMail(string Subject, string Body, string emailFrom)
    {
        try
        {
            string SmtpServer = TheEntityPreferenceMgr.LoadEntityPreference("SMTPEmailHost").Value;
            string MailFromPasswd = TheEntityPreferenceMgr.LoadEntityPreference("SMTPEmailPasswd").Value;
            string MailFrom = TheEntityPreferenceMgr.LoadEntityPreference("SMTPEmailAddr").Value;
            string MailTo = TheEntityPreferenceMgr.LoadEntityPreference("MailTo").Value;

            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient(SmtpServer);
            foreach (string mailTo in MailTo.Split(';'))
            {
                foreach (string mailto in mailTo.Split(','))
                {
                    message.To.Add(new MailAddress(mailto));
                }
            }
            message.Subject = Subject;
            message.Body = Body;
            message.From = new MailAddress(emailFrom);

            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(MailFrom, MailFromPasswd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            // System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment("D:\\logs\\" + filePath);
            // message.Attachments.Add(attachment);

            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            client.Send(message);
            // message.Dispose();
            // logger.Error(Subject);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        btnBack_Click(sender, e);
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvSubject":
                if (args.Value.Trim() == string.Empty)
                {
                    ShowWarningMessage("MasterData.FeedBack.Topic.Empty");
                    args.IsValid = false;
                }
                break;
            case "cvContent":
                if (args.Value.Trim() == string.Empty)
                {
                    ShowWarningMessage("MasterData.FeedBack.Content.Empty");
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }

    }
}
