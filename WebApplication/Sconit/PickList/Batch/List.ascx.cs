using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class PickList_Batch_List : ListModuleBase
{

    public void BindDataSource(IList<PickList> pickListList)
    {
        this.GV_List.DataSource = pickListList;
        this.UpdateView();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public override void UpdateView()
    {

        if (this.GV_List.DataSource != null && ((IList<PickList>)this.GV_List.DataSource).Count > 0)
        {
            this.lblNoRecordFound.Visible = false;
            this.GV_List.DataBind();
        }
        else
        {
            this.GV_List.DataBind();
            this.lblNoRecordFound.Visible = true;
        }
    }


    public IList<PickList> PopulateSelectedData()
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            IList<PickList> pickListList = new List<PickList>();
            foreach (GridViewRow row in this.GV_List.Rows)
            {

                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfPickListNo = row.FindControl("hfPickListNo") as HiddenField;

                    PickList pickList = ThePickListMgr.LoadPickList(hfPickListNo.Value);

                    pickListList.Add(pickList);
                }
            }
            return pickListList;
        }
        return null;
    }
}
