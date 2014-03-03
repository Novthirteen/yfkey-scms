using System;
using System.Collections;
using System.Collections.Generic;
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
using com.Sconit.Entity.Exception;
using System.Text;
using System.IO;
using com.Sconit.Web;

public partial class Message : ControlBase, IMessage
{
    private static readonly string SUCCESS_MESSAGE = "Success";
    private static readonly string WARNING_MESSAGE = "Warning";
    private static readonly string ERROR_MESSAGE = "Error";

    protected void Page_Load(object sender, EventArgs e)
    {
        CleanMessage();
    }

    public void ShowSuccessMessage(string message)
    {
        ShowMessage(ProcessMessage(message, null), SUCCESS_MESSAGE);
    }

    public void ShowSuccessMessage(string message, params string[] paramters)
    {
        ShowMessage(ProcessMessage(message, paramters), SUCCESS_MESSAGE);
    }

    public void ShowWarningMessage(string message)
    {
        ShowMessage(ProcessMessage(message, null), WARNING_MESSAGE);
    }

    public void ShowWarningMessage(string message, params string[] paramters)
    {
        ShowMessage(ProcessMessage(message, paramters), WARNING_MESSAGE);
    }

    public void ShowErrorMessage(string message)
    {
        ShowMessage(ProcessMessage(message, null), ERROR_MESSAGE);
    }

    public void ShowErrorMessage(string message, params string[] paramters)
    {
        ShowMessage(ProcessMessage(message, paramters), ERROR_MESSAGE);
    }

    public void ShowWarningMessage(BusinessWarningException ex)
    {
        string messageParams = string.Empty;
        ShowMessage(ProcessMessage(ex.Message, ex.MessageParams), WARNING_MESSAGE);
    }

    public void ShowErrorMessage(BusinessErrorException ex)
    {
        string messageParams = string.Empty;
        ShowMessage(ProcessMessage(ex.Message, ex.MessageParams), ERROR_MESSAGE);
    }

    public void CleanMessage()
    {
        lblMessage.Text = string.Empty;
        //lblMessage.Visible = false;
        this.divmessages.Visible = false;
    }

    private string ProcessMessage(string message, string[] paramters)
    {
        string messageParams = string.Empty;
        if (paramters != null && paramters.Length > 0)
        {
            //处理Message参数
            foreach (string para in paramters)
            {
                messageParams += "," + para;
            }
        }
        message = "${" + message + messageParams + "}";

        return message;
    }

    private void ShowMessage(string message, string status)
    {
        lblMessage.Text = message;
        lblMessage.Visible = true;
        lblMessage.Attributes.Add("title", status);
        this.divmessages.Visible = true;
        switch (status)
        {
            case "Error":
                this.divmessages.Attributes.Add("style", "background:transparent url(Images/Error.gif) no-repeat scroll 6px");
                break;
            case "Warning":
                this.divmessages.Attributes.Add("style", "background:transparent url(Images/Warning.gif) no-repeat scroll 6px");
                break;
            default:
                this.divmessages.Attributes.Add("style", "background:transparent url(Images/Success.gif) no-repeat scroll 6px");
                break;
        }
    }    
}
