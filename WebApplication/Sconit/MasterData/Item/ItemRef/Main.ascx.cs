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
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;

public partial class MasterData_Item_ItemRef_Main : MainModuleBase
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

    public void InitPageParameter(string code)
    {
        this.ItemCode = code;
        this.fldGV.Visible = true;
        DoSearch();
        this.GV_List.Execute();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GV_List.EditIndex = -1;
        this.fldGV.Visible = true;
        this.ucNew.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucNew.CreateEvent += new System.EventHandler(this.CreateBack_Render);
    }

    public void DoSearch()
    {

        #region DetachedCriteria

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ItemReference));

        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ItemReference));
        selectCountCriteria.SetProjection(Projections.Count("Id"));

        selectCriteria.Add(Expression.Eq("Item.Code", ItemCode));
        selectCountCriteria.Add(Expression.Eq("Item.Code", ItemCode));

        #endregion

        this.SetSearchCriteria(selectCriteria, selectCountCriteria);

        this.GV_List.Execute();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        this.ucNew.Visible = true;
        this.ucNew.InitPageParameter(this.ItemCode);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }    

    protected void GV_List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GV_List.EditIndex = e.NewEditIndex;
        this.GV_List.Execute();
    }

    protected void GV_List_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ItemReference itemReference = new ItemReference();

        int id = (int)this.GV_List.DataKeys[e.RowIndex].Values[0];
        string itemCode = ((Label)this.GV_List.Rows[e.RowIndex].FindControl("lblItemCode")).Text.Trim();
        string partyCode = ((Label)this.GV_List.Rows[e.RowIndex].FindControl("lblPartyCode")).Text.Trim();

        itemReference = TheItemReferenceMgr.LoadItemReference(id);
        itemReference.Item = TheItemMgr.LoadItem(itemCode);
        itemReference.Party = ThePartyMgr.LoadParty(partyCode);
        itemReference.ReferenceCode = ((Label)this.GV_List.Rows[e.RowIndex].FindControl("lblItemReferenceCode")).Text.Trim();
        itemReference.Description = ((TextBox)this.GV_List.Rows[e.RowIndex].FindControl("tbDescription")).Text.Trim();
        itemReference.Remark = ((TextBox)this.GV_List.Rows[e.RowIndex].FindControl("tbRemark")).Text.Trim();
        itemReference.IsActive = ((CheckBox)this.GV_List.Rows[e.RowIndex].FindControl("cbActive")).Checked;
        TheItemReferenceMgr.UpdateItemReference(itemReference);
        this.GV_List.EditIndex = -1;
        this.GV_List.Execute();
        ShowSuccessMessage("MasterData.ItemReference.UpdateItemReference.Successfully", itemReference.ReferenceCode);
    }

    protected void GV_List_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.GV_List.EditIndex = -1;
        this.GV_List.Execute();
    }

    protected void GV_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)this.GV_List.DataKeys[e.RowIndex].Values[0];
        string itemReferenceCode = TheItemReferenceMgr.LoadItemReference(id).ReferenceCode;
        TheItemReferenceMgr.DeleteItemReference(id);
        this.GV_List.Execute();
        try
        {
            ShowSuccessMessage("MasterData.ItemReference.DeleteItemReference.Successfully", itemReferenceCode);
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.ItemReference.DeleteItemReference.Fail", itemReferenceCode);
        }
    }

    protected void GV_List_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
    }

    private void SetSearchCriteria(DetachedCriteria SelectCriteria, DetachedCriteria SelectCountCriteria)
    {
        new SessionHelper(this.Page).AddUserSelectCriteria(this.TemplateControl.AppRelativeVirtualPath, SelectCriteria, SelectCountCriteria);
    }

    private void Back_Render(object sender, EventArgs e)
    {
        this.ucNew.Visible = false;
    }

    private void CreateBack_Render(object sender, EventArgs e)
    {
        this.ucNew.Visible = false;
        this.GV_List.Execute();
    }
}
