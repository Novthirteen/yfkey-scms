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
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using System.Collections.Generic;

public partial class MasterData_Item_ItemKit_Main : ListModuleBase
{
    public EventHandler EditEvent;
    public EventHandler NewEvent;
    public EventHandler BackEvent;

    protected string ParentItemCode
    {
        get
        {
            return (string)ViewState["ParentItemCode"];
        }
        set
        {
            ViewState["ParentItemCode"] = value;
        }
    }


    public void InitPageParameter(string code)
    {
        this.ParentItemCode = code;
        DoSearch();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
              
    }

    private void DoSearch()
    {

        #region DetachedCriteria

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ItemKit));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ItemKit))
            .SetProjection(Projections.Count("ChildItem.Code"));

        if (ParentItemCode != string.Empty)
        {
            selectCriteria.Add(Expression.Like("ParentItem.Code", ParentItemCode));
            selectCountCriteria.Add(Expression.Like("ParentItem.Code", ParentItemCode));
        }

        SetSearchCriteria(selectCriteria, selectCountCriteria);
        #endregion    
        UpdateView();
    }

    public override void UpdateView()
    {
        Item item = TheItemMgr.LoadItem(ParentItemCode);
        this.tbCode.Text = item.Code;
        this.tbDesc.Text = item.Description;
        this.tbUc.Text = item.UnitCount.ToString("0.########");
        this.tbUom.Text = item.Uom.Code;
        this.GV_List.Execute();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string code = ((LinkButton)sender).CommandArgument;
            EditEvent(code, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        try
        {
            TheItemKitMgr.DeleteItemKit(ParentItemCode, code);
            ShowSuccessMessage("MasterData.Item.DeleteItem.Successfully", code);
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Item.DeleteItem.Fail", code);
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
