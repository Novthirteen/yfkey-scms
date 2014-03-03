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

public partial class Mes_Shelf_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbShelfClickEvent;
    public event EventHandler lbShelfItemClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lbShelf_Click(object sender, EventArgs e)
    {
        if (lbShelfClickEvent != null)
        {
            lbShelfClickEvent(this, e);
        }

        this.tab_Shelf.Attributes["class"] = "ajax__tab_active";
        this.tab_ShelfItem.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbShelfItem_Click(object sender, EventArgs e)
    {
        if (lbShelfItemClickEvent != null)
        {
            lbShelfItemClickEvent(this, e);
        }

        this.tab_Shelf.Attributes["class"] = "ajax__tab_inactive";
        this.tab_ShelfItem.Attributes["class"] = "ajax__tab_active";
    }

    public void UpdateView()
    {
        lbShelf_Click(this, null);
    }
}
