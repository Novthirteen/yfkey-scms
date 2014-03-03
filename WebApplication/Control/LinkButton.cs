using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;

namespace com.Sconit.Control
{
    [ToolboxData("<{0}:LinkButton runat=server></{0}:LinkButton>")]
    public class LinkButton : System.Web.UI.WebControls.LinkButton
    {
        public String FunctionId
        {
            get
            {
                string id = (string)ViewState["FunctionId"];
                if (id != null && id.Trim() != string.Empty)
                {
                    return id;
                }
                else
                {
                    throw new TechnicalException("FunctionId not specified");
                }
            }
            set
            {
                ViewState["FunctionId"] = value;
            }

        }

        #region override && relier
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.Page.Session["Current_User"] != null)
            {
                User currentUser = this.Page.Session["Current_User"] as User;
                if (currentUser.PagePermission != null && currentUser.PagePermission.Count > 0)
                {
                    foreach (Permission permission in currentUser.PagePermission)
                    {
                        if (permission.Code == this.FunctionId)
                        {
                            this.Visible = true;
                            return;
                        }
                    }
                }

                this.Visible = false;
            }
        }
        #endregion
    }
}
