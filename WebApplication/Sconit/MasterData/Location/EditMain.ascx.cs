using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;

public partial class MasterData_Location_EditMain : MainModuleBase
{
    public event EventHandler BackEvent;

    protected string LocationCode
    {
        get { return (string)ViewState["LocationCode"]; }
        set { ViewState["LocationCode"] = value; }
    }

    protected string AreaCode
    {
        get { return (string)ViewState["AreaCode"]; }
        set { ViewState["AreaCode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbLocationAdvClickEvent += new System.EventHandler(this.TabLocationAdvClick_Render);
        this.ucTabNavigator.lbLocationBinClickEvent += new System.EventHandler(this.TabLocationBinClick_Render);
        this.ucTabNavigator.lbLocationClickEvent += new System.EventHandler(this.TabLocationClick_Render);
        this.ucEdit.BackEvent += new System.EventHandler(this.EditBack_Render);
        this.ucArea.EditEvent += new System.EventHandler(this.AeraEdit_Render);
        this.ucArea.BackEvent += new System.EventHandler(this.EditBack_Render);
        this.ucBin.BackEvent += new System.EventHandler(this.EditBack_Render);
    }

    public void InitPageParameter(string code)
    {
        this.LocationCode = code.Split(',')[0];
        this.ucEdit.InitPageParameter(code.Split(',')[0]);
        this.ucTabNavigator.ShowTabKit(Convert.ToBoolean(code.Split(',')[1]));
        this.ucTabNavigator.UpdateView();
    }

    private void EditBack_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    private void AeraEdit_Render(object sender, EventArgs e)
    {
        this.AreaCode = (string)sender;
    }

    private void TabLocationAdvClick_Render(object sender, EventArgs e)
    {
        this.ucArea.Visible = true;
        this.ucEdit.Visible = false;
        this.ucBin.Visible = false;
        this.ucArea.InitPageParameter(this.LocationCode);
    }

    private void TabLocationBinClick_Render(object sender, EventArgs e)
    {
        this.ucArea.Visible = false;
        this.ucEdit.Visible = false;
        this.ucBin.Visible = true;
        this.ucBin.InitPageParameter(this.LocationCode, this.AreaCode);
    }

    private void TabLocationClick_Render(object sender, EventArgs e)
    {
        this.ucArea.Visible = false;
        this.ucEdit.Visible = true;
        this.ucBin.Visible = false;
    }

}
