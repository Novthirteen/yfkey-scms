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
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;

public partial class Security_Language : NewModuleBase
{
    private UserPreference up = null;
    private string tempvalue = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        up = TheUserPreferenceMgr.LoadUserPreference(this.CurrentUser.Code, BusinessConstants.CODE_MASTER_LANGUAGE);
        tempvalue = this.ddlLanguage.SelectedValue;
        if (up != null)
        {
            this.ddlLanguage.Text = up.Value;
        }
        else
        {
            this.ddlLanguage.Text = TheCodeMasterMgr.GetDefaultCodeMaster(BusinessConstants.CODE_MASTER_LANGUAGE).Value;
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        if (up == null)
        {
            up.User = this.CurrentUser;
            up.Code = BusinessConstants.CODE_MASTER_LANGUAGE;
            up.Value = tempvalue;
            TheUserPreferenceMgr.CreateUserPreference(up);
            this.ddlLanguage.Text = tempvalue;
            //this.CurrentUser.UserLanguage = tempvalue;
        }
        else
        {
            up.Value = tempvalue;
            TheUserPreferenceMgr.UpdateUserPreference(up);
            this.ddlLanguage.Text = tempvalue;
            //this.CurrentUser.UserLanguage = tempvalue;
        }

    }
}
