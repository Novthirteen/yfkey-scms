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
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using com.Sconit.Utility;
using System.Text;

public partial class Visualization_InvVisualBoard_List : ReportModuleBase
{
    private Visualization_InvVisualBoard_Kanban GetKanbanControl(GridViewRow gvr)
    {
        return (Visualization_InvVisualBoard_Kanban)gvr.FindControl("ucKanban");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void InitPageParameter(object sender)
    {
        this._criteriaParam = (CriteriaParam)sender;
        this.UpdateView();
    }

    public override void UpdateView()
    {
        this.SetCriteria();
        this.GV_List.Execute();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //生成看板图片
            FlowView flowView = (FlowView)e.Row.DataItem;
            LocationDetail ld = flowView.LocationDetail;
            GetKanbanControl(e.Row).InitPageParameter(flowView);

            this.SetLinkButton(e.Row, "lbQtyToBeIn", new string[] { ld.Item.Code, ld.Location.Code, BusinessConstants.IO_TYPE_IN }, ld.QtyToBeIn != 0);
            this.SetLinkButton(e.Row, "lbQtyToBeOut", new string[] { ld.Item.Code, ld.Location.Code, BusinessConstants.IO_TYPE_OUT }, ld.QtyToBeOut != 0);
            this.SetLinkButton(e.Row, "lbInTransitQty", new string[] { ld.Item.Code, ld.Location.Code, BusinessConstants.IO_TYPE_IN }, ld.InTransitQty != 0);

            if (e.Row.RowIndex == 0)
                this.ltlLocation.Text = StringHelper.GetCodeDescriptionString(ld.Location.Code, ld.Location.Name);
        }
    }

    protected override void SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowView));
        criteria.CreateAlias("LocationTo", "l");
        criteria.CreateAlias("FlowDetail", "fd");

        SecurityHelper.SetRegionSearchCriteria(criteria, "l.Region.Code", this.CurrentUser.Code); //区域权限

        #region Select Parameters
        CriteriaHelper.SetLocationCriteria(criteria, "LocationTo.Code", this._criteriaParam);
        CriteriaHelper.SetItemCriteria(criteria, "fd.Item.Code", this._criteriaParam, MatchMode.Exact);
        CriteriaHelper.SetFlowCriteria(criteria, "Flow.Code", this._criteriaParam);
        #endregion

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(criteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        SetSearchCriteria(criteria, selectCountCriteria);
    }

    protected void lbQtyToBeIn_Click(object sender, EventArgs e)
    {
        string[] str = ((LinkButton)sender).CommandArgument.Split(',');
        if (str.Length > 0)
        {
            this.ucOrderInList.Visible = true;
            this.ucOrderInList.InitPageParameter(str[0], str[1], str[2], this._criteriaParam.EndDate);
        }
    }

    protected void lbQtyToBeOut_Click(object sender, EventArgs e)
    {
        string[] str = ((LinkButton)sender).CommandArgument.Split(',');
        if (str.Length > 0)
        {
            this.ucOrderOutList.Visible = true;
            this.ucOrderOutList.InitPageParameter(str[0], str[1], str[2], this._criteriaParam.EndDate);
        }
    }

    protected void lbInTransitQty_Click(object sender, EventArgs e)
    {
        string[] str = ((LinkButton)sender).CommandArgument.Split(',');
        if (str.Length > 0)
        {
            this.ucInTransitList.Visible = true;
            this.ucInTransitList.InitPageParameter(str[0], str[1], BusinessConstants.IO_TYPE_IN);
        }
    }

    public override void PostProcess(IList list)
    {
        TheLocationDetailMgr.PostProcessInvVisualBoard(list, this._criteriaParam.EndDate);
    }

    private void SetLinkButton(GridViewRow gvr, string id, string[] commandArgument, bool enabled)
    {
        LinkButton linkButton = (LinkButton)gvr.FindControl(id);
        linkButton.Enabled = enabled;
        if (enabled)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < commandArgument.Length; i++)
            {
                if (i > 0)
                    str.Append(",");

                str.Append(commandArgument[i]);
            }
            linkButton.CommandArgument = str.ToString();
        }
    }
}
